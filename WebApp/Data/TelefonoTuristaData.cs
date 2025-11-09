using Oracle.ManagedDataAccess.Client;
using Proyectofinal.Models;
using System.Data;

namespace Proyectofinal.Data
{
    public class TelefonoTuristaData
    {
        public List<TelefonoTurista> Listar()
        {
            var oLista = new List<TelefonoTurista>();
            var cn = new Conexion();

            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                var query = "SELECT * FROM TELEFONO_TURISTA";

                using (var cmd = new OracleCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new TelefonoTurista()
                            {
                                CodigoTurista = dr["COD_TURISTA"].ToString(),
                                Telefono = dr["TELEFONO"].ToString(),
                                TipoTelefono = dr["TIPO_TELEFONO"].ToString(),

                            });
                        }
                    }
                }
            }

            return oLista;
        }
        public TelefonoTurista Obtener(string codigo)
        {

            var obj = new TelefonoTurista();

            var cn = new Conexion();
            var query = "select * from TELEFONO_TURISTA where COD_TURISTA=@COD_TURISTA;";
            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                OracleCommand cmd = new OracleCommand(query, conexion);
                cmd.Parameters.Add(new OracleParameter("COD_TURISTA", codigo));
                cmd.CommandType = CommandType.Text;
                //cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        obj.CodigoTurista = dr["COD_TURISTA"].ToString();
                        obj.Telefono = dr["TELEFONO"].ToString();
                        obj.TipoTelefono = dr["TIPO_TELEFONO"].ToString();
                    }
                }
            }

            return obj;
        }
        public bool Guardar(TelefonoTurista model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"INSERT INTO TELEFONO_TURISTA
                     (COD_TURISTA, TELEFONO, TIPO_TELEFONO)
                     VALUES (:COD_TURISTA, :TELEFONO,:TIPO_TELEFONO)";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("COD_TURISTA", model.CodigoTurista));
                        cmd.Parameters.Add(new OracleParameter("TELEFONO", model.Telefono));
                        cmd.Parameters.Add(new OracleParameter("TIPO_TELEFONO", model.TipoTelefono));

                        cmd.ExecuteNonQuery();
                    }
                }

                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message; // Aquí puedes hacer log o mostrarlo
                rpta = false;
            }

            return rpta;
        }


        public bool Editar(TelefonoTurista model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"UPDATE TELEFONO_TURISTA SET 
                        TELEFONO = :TELEFONO,
                        TIPO_TELEFONO = :TIPO_TELEFONO 
                      WHERE COD_TURISTA = :COD_TURISTA";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("TELEFONO", model.Telefono));
                        cmd.Parameters.Add(new OracleParameter("COD_TURISTA", model.CodigoTurista));
                        cmd.Parameters.Add(new OracleParameter("TIPO_TELEFONO", model.TipoTelefono));

                        cmd.ExecuteNonQuery();
                    }
                }

                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        public bool Eliminar(string codigo)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = "DELETE FROM TELEFONO_TURISTA WHERE COD_TURISTA = :COD_TURISTA";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new OracleParameter("COD_TURISTA", codigo));
                        cmd.ExecuteNonQuery();
                    }
                }

                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }
    }
}
