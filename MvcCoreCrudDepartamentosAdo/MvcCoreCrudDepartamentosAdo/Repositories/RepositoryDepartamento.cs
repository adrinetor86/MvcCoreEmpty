using System.Data;
using Microsoft.Data.SqlClient;
using MvcCoreCrudDepartamentosAdo.Models;

namespace MvcCoreCrudDepartamentosAdo.Repositories;

public class RepositoryDepartamento
{
    
    private SqlConnection cn;
    private SqlCommand com;
    private SqlDataReader reader;
    
    public RepositoryDepartamento()
    {
        string connectionString = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
        this.cn = new SqlConnection(connectionString);
        this.com = new SqlCommand();
        this.com.Connection = this.cn;
        
    }


    public async Task<List<Departamento>> GetDepartamentosAsync()
    {
        string sql = "SELECT * FROM DEPT";

        this.com.CommandType = CommandType.Text;
        this.com.CommandText = sql;
        List<Departamento> departamentos = new List<Departamento>();

        await this.cn.OpenAsync();
        this.reader = await this.com.ExecuteReaderAsync();
        while (await this.reader.ReadAsync())
        {
            Departamento departamento = new Departamento();

            departamento.IdDept = int.Parse(this.reader["DEPT_NO"].ToString());
            departamento.Nombre = this.reader["DNOMBRE"].ToString();
            departamento.Localidad = this.reader["LOC"].ToString();
         

            departamentos.Add(departamento);
        }

        await this.cn.CloseAsync();
        await this.reader.CloseAsync();
        return departamentos;
    }
    
    public async Task<Departamento> GetDepartamentoByIdAsync(int idDept)
    {
        string sql = "SELECT * FROM DEPT where  DEPT_NO = @DEPT_NO";

        this.com.CommandType = CommandType.Text;
        this.com.CommandText = sql;
        this.com.Parameters.AddWithValue("@DEPT_NO", idDept);
        
       

        await this.cn.OpenAsync();
        this.reader = await this.com.ExecuteReaderAsync();
        Departamento departamento = new Departamento(); 
        while (await this.reader.ReadAsync())
        {
            
            departamento.IdDept = int.Parse(this.reader["DEPT_NO"].ToString());
            departamento.Nombre = this.reader["DNOMBRE"].ToString();
            departamento.Localidad = this.reader["LOC"].ToString();
 
        }

        await this.cn.CloseAsync();
        await this.reader.CloseAsync();
        this.com.Parameters.Clear();
        return departamento;
    }
    
    public async Task UpdateDepartamentoByIdAsync(int idDept,string nombre,string localidad)
    {
        string sql = "UPDATE DEPT SET DNOMBRE = @nombre, LOC= @localidad where DEPT_NO = @iddept";
    
        this.com.CommandType = CommandType.Text;
        this.com.CommandText = sql;
        this.com.Parameters.AddWithValue("@iddept", idDept);
        this.com.Parameters.AddWithValue("@nombre", nombre);
        this.com.Parameters.AddWithValue("@localidad", localidad);
        
        await this.cn.OpenAsync();
        await this.com.ExecuteNonQueryAsync();
        
        await this.cn.CloseAsync();
        this.com.Parameters.Clear();
    }
    
    public async Task CreateDepartamentoByIdAsync(int idDept,string nombre,string localidad)
    {
        string sql = "INSERT INTO DEPT VALUES(@idDept,@nombre,@localidad)";
    
        this.com.CommandType = CommandType.Text;
        this.com.CommandText = sql;
        this.com.Parameters.AddWithValue("@iddept", idDept);
        this.com.Parameters.AddWithValue("@nombre", nombre);
        this.com.Parameters.AddWithValue("@localidad", localidad);
        
        await this.cn.OpenAsync();
        await this.com.ExecuteNonQueryAsync();
        
        await this.cn.CloseAsync();
        this.com.Parameters.Clear();
    }

}