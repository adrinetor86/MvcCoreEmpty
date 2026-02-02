using Microsoft.Data.SqlClient;
using MvcCoreAdoNetOk.Models;
using System.Data;

namespace MvcCoreAdoNetOk.Repositories
{
    public class RepositoryHospital
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;


        public RepositoryHospital()
        {
            string connectionString = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }


        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            string sql = "SELECT * FROM HOSPITAL";

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            List<Hospital> hospitales = new List<Hospital>();

            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            while (await this.reader.ReadAsync())
            {
                Hospital hospital = new Hospital();

                hospital.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                hospital.Nombre = this.reader["NOMBRE"].ToString();
                hospital.Direccion = this.reader["DIRECCION"].ToString();
                hospital.Telefono = this.reader["TELEFONO"].ToString();
                hospital.Camas = int.Parse(this.reader["NUM_CAMA"].ToString());

                hospitales.Add(hospital);
            }

            await this.cn.CloseAsync();
            await this.reader.CloseAsync();
            return hospitales;
        }


        public async Task<Hospital> FindHospitalAsync(int idHospital)
        {
            string sql = "SELECT * FROM HOSPITAL WHERE HOSPITAL_COD=@idHospital";

            this.com.Parameters.AddWithValue("@idHospital", idHospital);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();

            this.reader = await this.com.ExecuteReaderAsync();

            Hospital hospital = new Hospital();

            //Leemos solo una linea
            await this.reader.ReadAsync();

            hospital.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
            hospital.Nombre = this.reader["NOMBRE"].ToString();
            hospital.Direccion = this.reader["DIRECCION"].ToString();
            hospital.Telefono = this.reader["TELEFONO"].ToString();
            hospital.Camas = int.Parse(this.reader["NUM_CAMA"].ToString());

            await this.cn.CloseAsync();
            await this.reader.CloseAsync();
            this.com.Parameters.Clear();

            return hospital;
        }
        
        
    }
}
