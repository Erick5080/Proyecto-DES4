using System;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto1
{
    public class ConexionDB
    {
        // La cadena de conexión
        private readonly string _connectionString =
             $"Data Source=DESKTOP-HAE2KFA\\SQLEXPRESS;Initial Catalog=Proyecto;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=60";

        // 1. Método para abrir la conexión
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

        // 2. Método para INSERTAR Registros
        public void InsertarRegistro(
            string num1,           
            string num2,          
            string suma,            
            string resta,           
            string multiplicacion,  
            string division)      
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
                    // Los parámetros se envían como string
                    comando.Parameters.AddWithValue("@p_num1", num1);
                    comando.Parameters.AddWithValue("@p_num2", num2);
                    comando.Parameters.AddWithValue("@p_suma", suma);
                    comando.Parameters.AddWithValue("@p_resta", resta);
                    comando.Parameters.AddWithValue("@p_multiplicacion", multiplicacion);

                    if (!string.IsNullOrEmpty(division))
                    {
                        comando.Parameters.AddWithValue("@p_division", division);
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

        // 3. Método para OBTENER Registros
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
}