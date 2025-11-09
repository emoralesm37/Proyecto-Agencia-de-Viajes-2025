using Oracle.ManagedDataAccess.Client;
using Proyectofinal.Models;
using System.Data;

namespace Proyectofinal.Data
{
    public class EmailTuristaData
    {
        public List<EmailTurista> Listar()
        {
            var oLista = new List<EmailTurista>();
            var cn = new Conexion();

            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                var query = "SELECT * FROM EMAIL_TURISTA";

                using (var cmd = new OracleCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new EmailTurista()
                            {
                                CodigoTurista = dr["COD_TURISTA"].ToString(),
                                Email = dr["EMAIL"].ToString(),
                                TipoEmail = dr["TIPO_EMAIL"].ToString()
                            });
                        }
                    }
                }
            }

            return oLista;
        }
        public EmailTurista Obtener(string codigo)
        {

            var obj = new EmailTurista();

            var cn = new Conexion();
            var query = "select * from EMAIL_TURISTA where COD_TURISTA=:COD_TURISTA";
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
                        obj.Email = dr["EMAIL"].ToString();
                        obj.TipoEmail = dr["TIPO_EMAIL"].ToString();
                    }
                }
            }

            return obj;
        }
        public bool Guardar(EmailTurista model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"INSERT INTO EMAIL_TURISTA
                     (COD_TURISTA, EMAIL, TIPO_EMAIL)
                     VALUES (:COD_TURISTA, :EMAIL, :TIPO_EMAIL)";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("COD_TURISTA", model.CodigoTurista));
                        cmd.Parameters.Add(new OracleParameter("EMAIL", model.Email));
                        cmd.Parameters.Add(new OracleParameter("TIPO_EMAIL", model.TipoEmail));

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


        public bool Editar(EmailTurista model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"UPDATE EMAIL_TURISTA SET 
                        EMAIL = :EMAIL,
                        TIPO_EMAIL = :TIPO_EMAIL 
                      WHERE COD_TURISTA = :COD_TURISTA";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("COD_TURISTA", model.CodigoTurista));
                        cmd.Parameters.Add(new OracleParameter("EMAIL", model.Email));
                        cmd.Parameters.Add(new OracleParameter("TIPO_EMAIL", model.TipoEmail));

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
                var query = "DELETE FROM EMAIL_TURISTA WHERE COD_TURISTA = :COD_TURISTA";

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
