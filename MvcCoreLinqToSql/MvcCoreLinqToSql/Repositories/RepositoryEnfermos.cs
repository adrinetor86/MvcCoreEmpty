using System.Data;
using Microsoft.Data.SqlClient;
using MvcCoreLinqToSql.Models;

namespace MvcCoreLinqToSql.Repositories;

public class RepositoryEnfermos
{

    private DataTable tablaEnfermos;
    private SqlConnection cn;
    private SqlCommand com;
    private SqlDataReader reader;
    
    
    public RepositoryEnfermos()
    {
        string connectionString = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
        
        cn= new SqlConnection(connectionString);
        com = new SqlCommand();
        com.Connection = cn;
        
        string sql = "SELECT * FROM ENFERMO";
        
        SqlDataAdapter ad= new SqlDataAdapter(sql, connectionString);
        tablaEnfermos = new DataTable();
        ad.Fill(tablaEnfermos);
        
    }

    public List<Enfermo> GetEnfermos()
    {
        var consulta= from datos in tablaEnfermos.AsEnumerable()
                      select datos;

        List<Enfermo> enfermos = new List<Enfermo>();
        if (consulta.Count() == 0)
        {
            return null;
        }
        foreach(var row in consulta)
        {
            Enfermo enfermo = new Enfermo
            {
                Inscripcion = row.Field<string>("INSCRIPCION"),
                Apellido = row.Field<string>("APELLIDO"),
                Direccion = row.Field<string>("DIRECCION"),
                Fecha_Nac = row.Field<DateTime>("FECHA_NAC"),
                Genero = row.Field<string>("S"),
                Nss = row.Field<string>("NSS"),
            };
            enfermos.Add(enfermo);
        }
        return enfermos;
    }


    public async Task DeleteEnfermoByInscripcion(string inscripcion)
    {
        string sql="DELETE FROM ENFERMO WHERE INSCRIPCION=@inscripcion";
        com.CommandType=CommandType.Text;
        com.CommandText=sql;
        com.Parameters.AddWithValue("@inscripcion", inscripcion);
        
        await cn.OpenAsync();
        
        await com.ExecuteNonQueryAsync();

        await cn.CloseAsync();
        await cn.CloseAsync();
        com.Parameters.Clear();
    } 
    
    public async Task<Enfermo> GetDetailsEnfermoByInscripcion(string inscripcion)
    {
        string sql="SELECT * FROM ENFERMO WHERE  INSCRIPCION=@inscripcion";
        com.CommandType=CommandType.Text;
        com.CommandText=sql;
        com.Parameters.AddWithValue("@inscripcion", inscripcion);
        
        await cn.OpenAsync();
        
        reader= await com.ExecuteReaderAsync();
        Enfermo enfermo = new Enfermo();
        while (await reader.ReadAsync())
        {
            enfermo.Inscripcion = reader["INSCRIPCION"].ToString();
            enfermo.Apellido = reader["APELLIDO"].ToString();
            enfermo.Direccion = reader["DIRECCION"].ToString();
            enfermo.Fecha_Nac = (DateTime)reader["FECHA_NAC"];
            enfermo.Genero = reader["S"].ToString();
            enfermo.Nss = reader["NSS"].ToString();

        }

        await cn.CloseAsync();
        await cn.CloseAsync();
        com.Parameters.Clear();
        return enfermo;
    }
    
}