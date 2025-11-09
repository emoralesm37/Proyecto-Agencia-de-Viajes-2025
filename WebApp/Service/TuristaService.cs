using Oracle.ManagedDataAccess.Client;
using Proyectofinal.Models;
using System.Data;

namespace Proyectofinal.Service
{
    public class TuristaService
    {
        private readonly string _connectionString;

        public TuristaService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnection");
        }

        public async Task<string> InsertarTurista(TURISTA turista)
        {
            try
            {
                using var connection = new OracleConnection(_connectionString);
                await connection.OpenAsync();

                using var command = new OracleCommand("sp_insertar_turista", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Parámetros
                command.Parameters.Add("p_cod_turista", OracleDbType.Varchar2).Value = turista.COD_TURISTA;
                command.Parameters.Add("p_nombre1", OracleDbType.Varchar2).Value = turista.NOMBRE1;
                command.Parameters.Add("p_nombre2", OracleDbType.Varchar2).Value = turista.NOMBRE2 ?? (object)DBNull.Value;
                command.Parameters.Add("p_nombre3", OracleDbType.Varchar2).Value = turista.NOMBRE3 ?? (object)DBNull.Value;
                command.Parameters.Add("p_apellido1", OracleDbType.Varchar2).Value = turista.APELLIDO1;
                command.Parameters.Add("p_apellido2", OracleDbType.Varchar2).Value = turista.APELLIDO2 ?? (object)DBNull.Value;
                command.Parameters.Add("p_direccion", OracleDbType.Varchar2).Value = turista.DIRECCION;
                command.Parameters.Add("p_cod_pais", OracleDbType.Varchar2).Value = turista.COD_PAIS;
                command.Parameters.Add("p_cod_sucursal", OracleDbType.Varchar2).Value = turista.COD_SUCURSAL;

                await command.ExecuteNonQueryAsync();
                return "Turista insertado correctamente";
            }
            catch (OracleException ex)
            {
                return $"Error de Oracle: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Error general: {ex.Message}";
            }
        }

        public async Task<List<TURISTA>> ObtenerTodos()
        {
            var lista = new List<TURISTA>();

            using var connection = new OracleConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new OracleCommand("SELECT * FROM turista", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new TURISTA
                {
                    COD_TURISTA = reader["cod_turista"].ToString(),
                    NOMBRE1 = reader["nombre1"].ToString(),
                    NOMBRE2 = reader["nombre2"] == DBNull.Value ? null : reader["nombre2"].ToString(),
                    NOMBRE3 = reader["nombre3"] == DBNull.Value ? null : reader["nombre3"].ToString(),
                    APELLIDO1 = reader["apellido1"].ToString(),
                    APELLIDO2 = reader["apellido2"] == DBNull.Value ? null : reader["apellido2"].ToString(),
                    DIRECCION = reader["direccion"].ToString(),
                    COD_PAIS = reader["cod_pais"].ToString(),
                    COD_SUCURSAL = reader["cod_sucursal"].ToString()
                });
            }

            return lista;
        }

    }
}
