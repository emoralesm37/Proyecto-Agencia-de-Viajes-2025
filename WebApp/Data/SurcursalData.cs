using Oracle.ManagedDataAccess.Client;
using Proyectofinal.Models;
using System.Data;

namespace Proyectofinal.Data
{
    public class SurcursalData
    {
        
        public List<Sucursal> Listar()
        {
            var oLista = new List<Sucursal>();
            var cn = new Conexion();

            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                var query = "SELECT * FROM SUCURSAL";

                using (var cmd = new OracleCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Sucursal()
                            {
                                CodigoSucursal = dr["COD_SUCURSAL"].ToString(),
                                Nombre = dr["NOMBRE"].ToString(),
                                Direccion = dr["DIRECCION"].ToString(),
                                Telefon = dr["TELEFONO"].ToString(),
                            });
                        }
                    }
                }
            }

            return oLista;
        }
        public Sucursal Obtener(string codigo)
        {
            var obj = new Sucursal();
            var cn = new Conexion();

            var query = "SELECT * FROM SUCURSAL WHERE COD_SUCURSAL = :COD_SUCURSAL";

            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();

                using (var cmd = new OracleCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new OracleParameter("COD_SUCURSAL", codigo));

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read()) // Solo un registro
                        {
                            obj.CodigoSucursal = dr["COD_SUCURSAL"].ToString();
                            obj.Nombre = dr["NOMBRE"].ToString();
                            obj.Direccion = dr["DIRECCION"].ToString();
                            obj.Telefon = dr["TELEFONO"].ToString();
                        }
                    }
                }
            }

            return obj;
        }

        public bool Guardar(Sucursal model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"INSERT INTO SUCURSAL
                     (COD_SUCURSAL,NOMBRE, DIRECCION,TELEFONO)
                     VALUES (:COD_SUCURSAL,:NOMBRE, :DIRECCION,:TELEFONO)";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("COD_SUCURSAL", model.CodigoSucursal));
                        cmd.Parameters.Add(new OracleParameter("NOMBRE", model.Nombre));
                        cmd.Parameters.Add(new OracleParameter("DIRECCION", model.Direccion));
                        cmd.Parameters.Add(new OracleParameter("TELEFONO", model.Telefon));

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


        public bool Editar(Sucursal model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"UPDATE SUCURSAL SET 
                        NOMBRE = :NOMBRE,
                        DIRECCION = :DIRECCION,
                        TELEFONO = :TELEFONO 
                      WHERE COD_SUCURSAL = :COD_SUCURSAL";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        
                        cmd.Parameters.Add(new OracleParameter("NOMBRE", model.Nombre));
                        cmd.Parameters.Add(new OracleParameter("DIRECCION", model.Direccion));
                        cmd.Parameters.Add(new OracleParameter("TELEFONO", model.Telefon));
                        cmd.Parameters.Add(new OracleParameter("COD_SUCURSAL", model.CodigoSucursal));

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
                var query = "DELETE FROM SUCURSAL WHERE COD_SUCURSAL = :COD_SUCURSAL";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new OracleParameter("COD_SUCURSAL", codigo));
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
