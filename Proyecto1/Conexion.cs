using System.Data.SqlClient; 
using System.Data;
using System;

public class ConexionDB
{

    private const string Servidor = ""; 
    private const string BaseDeDatos = "Proyecto";

    // Aqui poner nombre de tu servidor
    private readonly string _connectionString = 
        $"Data Source={Servidor};Initial Catalog={BaseDeDatos};Integrated Security=True;TrustServerCertificate=True";

    public SqlConnection AbrirConexion()
    {
        try
        {
            var conexion = new SqlConnection(_connectionString);
            conexion.Open();
            return conexion;
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Error al abrir la conexión: {ex.Message}");
            throw; 
        }
    }
        public void InsertarRegistro(
        decimal num1, 
        decimal num2, 
        decimal suma, 
        decimal resta, 
        decimal multiplicacion, 
        decimal? division) 
    {
        string insertQuery = @"
            INSERT INTO registros (
                numero1, 
                numero2, 
                resultado_suma, 
                resultado_resta, 
                resultado_multiplicacion, 
                resultado_division
            )
            VALUES (
                @p_num1, 
                @p_num2, 
                @p_suma, 
                @p_resta, 
                @p_multiplicacion, 
                @p_division
            );";

        using (SqlConnection conexion = AbrirConexion()) 
        {
            using (SqlCommand comando = new SqlCommand(insertQuery, conexion))
            {
                comando.Parameters.AddWithValue("@p_num1", num1);
                comando.Parameters.AddWithValue("@p_num2", num2);
                comando.Parameters.AddWithValue("@p_suma", suma);
                comando.Parameters.AddWithValue("@p_resta", resta);
                comando.Parameters.AddWithValue("@p_multiplicacion", multiplicacion);
                
                if (division.HasValue)
                {
                    comando.Parameters.AddWithValue("@p_division", division.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@p_division", DBNull.Value);
                }

                try
                {
                    comando.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error al insertar el registro: {ex.Message}");
                    throw;
                }
            }
        }
    }

    public DataTable ObtenerRegistros()
    {
        string selectQuery = "SELECT * FROM registros ORDER BY fecha_operacion DESC";
        DataTable dataTable = new DataTable();

        using (SqlConnection conexion = AbrirConexion()) 
        {
            try
            {
                using (SqlDataAdapter adaptador = new SqlDataAdapter(selectQuery, conexion))
                {
                    adaptador.Fill(dataTable);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw;
            }
        } 
        
        return dataTable;
    }
}