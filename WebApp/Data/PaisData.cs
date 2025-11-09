using Oracle.ManagedDataAccess.Client;
using Proyectofinal.Models;
using System.Data;

namespace Proyectofinal.Data
{
    public class PaisData
    {
        public List<Pais> Listar()
        {
            var oLista = new List<Pais>();
            var cn = new Conexion();

            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                var query = "SELECT * FROM pais";

                using (var cmd = new OracleCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Pais()
                            {
                                CodigoPais = dr["COD_PAIS"].ToString(),
                                Nombre = dr["NOMBRE_PAIS"].ToString(),
                            });
                        }
                    }
                }
            }

            return oLista;
        }

        public Pais Obtener(string codigo)
        {
            Pais obj = null;
            var cn = new Conexion();

            var query = "select * from pais where COD_PAIS = :COD_PAIS";

            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                using (var cmd = new OracleCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new OracleParameter("COD_PAIS", codigo));

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            obj = new Pais
                            {
                                CodigoPais = dr["COD_PAIS"].ToString(),
                                Nombre = dr["NOMBRE_PAIS"].ToString()
                            };
                        }
                    }
                }
            }

            return obj;
        }


        public bool Guardar(Pais model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"INSERT INTO Pais
                     (COD_PAIS, NOMBRE_PAIS)
                     VALUES (:COD_PAIS, :NOMBRE_PAIS)";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("COD_PAIS", model.CodigoPais));
                        cmd.Parameters.Add(new OracleParameter("NOMBRE_PAIS", model.Nombre));

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


        public bool Editar(Pais model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"UPDATE pais SET 
                        NOMBRE_PAIS = :NOMBRE_PAIS 
                      WHERE COD_PAIS = :COD_PAIS";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        
                        cmd.Parameters.Add(new OracleParameter("NOMBRE_PAIS", model.Nombre));
                        cmd.Parameters.Add(new OracleParameter("COD_PAIS", model.CodigoPais));

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
                var query = "DELETE FROM pais WHERE COD_PAIS = :COD_PAIS";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new OracleParameter("COD_PAIS", codigo));
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

