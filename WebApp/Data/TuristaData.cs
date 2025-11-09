using Oracle.ManagedDataAccess.Client;
using Proyectofinal.Models;
using System.Data;

namespace Proyectofinal.Data
{
    public class TuristaData
    {
        public List<TURISTA> Listar()
        {
            var oLista = new List<TURISTA>();
            var cn = new Conexion();

            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                var query = "select a.COD_TURISTA, a.NOMBRE1, a.NOMBRE2, a.NOMBRE3, a.APELLIDO1, a.APELLIDO2, a.DIRECCION, b.NOMBRE_PAIS,c.NOMBRE from turista a " +
                    "inner join PAIS b on a.COD_PAIS=b.COD_PAIS " +
                    "inner join SUCURSAL c on a.COD_SUCURSAL=c.COD_SUCURSAL";

                using (var cmd = new OracleCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new TURISTA()
                            {
                                COD_TURISTA = dr["COD_TURISTA"].ToString(),
                                NOMBRE1 = dr["NOMBRE1"].ToString(),
                                NOMBRE2 = dr["NOMBRE2"].ToString(),
                                NOMBRE3 = dr["NOMBRE3"].ToString(),
                                APELLIDO1 = dr["APELLIDO1"].ToString(),
                                APELLIDO2 = dr["APELLIDO2"].ToString(),
                                DIRECCION = dr["DIRECCION"].ToString(),
                                PAIS = dr["NOMBRE_PAIS"].ToString(),
                                SUCURSAL = dr["NOMBRE"].ToString(),
                                NombreCompleto = dr["NOMBRE1"].ToString() +' '+ dr["APELLIDO1"].ToString(),
                            });
                        }
                    }
                }
            }

            return oLista;
        }
        public TURISTA Obtener(string codigo)
        {
            TURISTA turista = null;
            var cn = new Conexion();

            var query = "SELECT * FROM TURISTA WHERE COD_TURISTA = :COD_TURISTA";

            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                using (var cmd = new OracleCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new OracleParameter("COD_TURISTA", codigo));

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            turista = new TURISTA
                            {
                                COD_TURISTA = dr["COD_TURISTA"].ToString(),
                                NOMBRE1 = dr["NOMBRE1"].ToString(),
                                NOMBRE2 = dr["NOMBRE2"].ToString(),
                                NOMBRE3 = dr["NOMBRE3"].ToString(),
                                APELLIDO1 = dr["APELLIDO1"].ToString(),
                                APELLIDO2 = dr["APELLIDO2"].ToString(),
                                DIRECCION = dr["DIRECCION"].ToString(),
                                COD_PAIS = dr["COD_PAIS"].ToString(),
                                COD_SUCURSAL = dr["COD_SUCURSAL"].ToString(),
                            };
                        }
                    }
                }
            }

            return turista;
        }

        public bool Guardar(TURISTA turista)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"INSERT INTO TURISTA
                     (COD_TURISTA, COD_SUCURSAL, COD_PAIS, DIRECCION, APELLIDO2, APELLIDO1, NOMBRE3, NOMBRE2, NOMBRE1)
                     VALUES (:COD_TURISTA, :COD_SUCURSAL, :COD_PAIS, :DIRECCION, :APELLIDO2, :APELLIDO1, :NOMBRE3, :NOMBRE2, :NOMBRE1)";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("COD_TURISTA", turista.COD_TURISTA));
                        cmd.Parameters.Add(new OracleParameter("COD_SUCURSAL", turista.COD_SUCURSAL));
                        cmd.Parameters.Add(new OracleParameter("COD_PAIS", turista.COD_PAIS));
                        cmd.Parameters.Add(new OracleParameter("DIRECCION", turista.DIRECCION));
                        cmd.Parameters.Add(new OracleParameter("APELLIDO2", turista.APELLIDO2));
                        cmd.Parameters.Add(new OracleParameter("APELLIDO1", turista.APELLIDO1));
                        cmd.Parameters.Add(new OracleParameter("NOMBRE3", turista.NOMBRE3));
                        cmd.Parameters.Add(new OracleParameter("NOMBRE2", turista.NOMBRE2));
                        cmd.Parameters.Add(new OracleParameter("NOMBRE1", turista.NOMBRE1));

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


        public bool Editar(TURISTA turista)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"UPDATE TURISTA SET 
                        COD_SUCURSAL = :COD_SUCURSAL,
                        COD_PAIS = :COD_PAIS,
                        DIRECCION = :DIRECCION,
                        APELLIDO2 = :APELLIDO2,
                        APELLIDO1 = :APELLIDO1,
                        NOMBRE3 = :NOMBRE3,
                        NOMBRE2 = :NOMBRE2,
                        NOMBRE1 = :NOMBRE1 
                      WHERE COD_TURISTA = :COD_TURISTA";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("COD_SUCURSAL", turista.COD_SUCURSAL));
                        cmd.Parameters.Add(new OracleParameter("COD_PAIS", turista.COD_PAIS));
                        cmd.Parameters.Add(new OracleParameter("DIRECCION", turista.DIRECCION));
                        cmd.Parameters.Add(new OracleParameter("APELLIDO2", turista.APELLIDO2));
                        cmd.Parameters.Add(new OracleParameter("APELLIDO1", turista.APELLIDO1));
                        cmd.Parameters.Add(new OracleParameter("NOMBRE3", turista.NOMBRE3));
                        cmd.Parameters.Add(new OracleParameter("NOMBRE2", turista.NOMBRE2));
                        cmd.Parameters.Add(new OracleParameter("NOMBRE1", turista.NOMBRE1));
                        cmd.Parameters.Add(new OracleParameter("COD_TURISTA", turista.COD_TURISTA));

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
                var query = "DELETE FROM TURISTA WHERE COD_TURISTA = :COD_TURISTA";

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
