using Oracle.ManagedDataAccess.Client;
using Proyectofinal.Models;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Proyectofinal.Data
{
    public class HotelData
    {
        public List<HOTEL> Listar()
        {
            var oLista = new List<HOTEL>();
            var cn = new Conexion();

            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                var query = "SELECT * FROM HOTEL";

                using (var cmd = new OracleCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new HOTEL()
                            {
                                COD_HOTEL = dr["COD_HOTEL"].ToString(),
                                NOMBRE = dr["NOMBRE"].ToString(),
                                DIRECCION = dr["DIRECCION"].ToString(),
                                CIUDAD = dr["CIUDAD"].ToString(),
                                TELEFONO = dr["TELEFONO"].ToString(),
                                NUM_PLAZAS_DISPONIBLES = Convert.ToInt32(dr["NUM_PLAZAS_DISPONIBLES"].ToString())

                            });
                        }
                    }
                }
            }

            return oLista;
        }
        public HOTEL Obtener(string codigo)
        {

            var obj = new HOTEL();

            var cn = new Conexion();
            var query = "select * from HOTEL where COD_HOTEL=:COD_HOTEL";
            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                OracleCommand cmd = new OracleCommand(query, conexion);
                cmd.Parameters.Add(new OracleParameter("COD_HOTEL", codigo));
                cmd.CommandType = CommandType.Text;
                //cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        obj.COD_HOTEL = dr["COD_HOTEL"].ToString();
                        obj.NOMBRE = dr["NOMBRE"].ToString();
                        obj.DIRECCION = dr["DIRECCION"].ToString();
                        obj.CIUDAD = dr["CIUDAD"].ToString();
                        obj.TELEFONO = dr["TELEFONO"].ToString();
                        obj.NUM_PLAZAS_DISPONIBLES = Convert.ToInt32(dr["NUM_PLAZAS_DISPONIBLES"].ToString());
                    }
                }
            }

            return obj;
        }
        public bool Guardar(HOTEL model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"INSERT INTO HOTEL
                     (COD_HOTEL, NOMBRE, DIRECCION,CIUDAD,TELEFONO,NUM_PLAZAS_DISPONIBLES)
                     VALUES (:COD_HOTEL, :NOMBRE, :DIRECCION,:CIUDAD,:TELEFONO,:NUM_PLAZAS_DISPONIBLES)";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("COD_HOTEL", model.COD_HOTEL));
                        cmd.Parameters.Add(new OracleParameter("NOMBRE", model.NOMBRE));
                        cmd.Parameters.Add(new OracleParameter("DIRECCION", model.DIRECCION));
                        cmd.Parameters.Add(new OracleParameter("CIUDAD", model.CIUDAD));
                        cmd.Parameters.Add(new OracleParameter("TELEFONO", model.TELEFONO));
                        cmd.Parameters.Add(new OracleParameter("NUM_PLAZAS_DISPONIBLES", model.NUM_PLAZAS_DISPONIBLES));

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


        public bool Editar(HOTEL model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"UPDATE HOTEL SET 
                        NOMBRE = :NOMBRE,
                        DIRECCION = :DIRECCION,
                        CIUDAD = :CIUDAD,
                        TELEFONO = :TELEFONO,
                        NUM_PLAZAS_DISPONIBLES = :NUM_PLAZAS_DISPONIBLES 
                      WHERE COD_HOTEL = :COD_HOTEL";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        
                        cmd.Parameters.Add(new OracleParameter("NOMBRE", model.NOMBRE));
                        cmd.Parameters.Add(new OracleParameter("DIRECCION", model.DIRECCION));
                        cmd.Parameters.Add(new OracleParameter("CIUDAD", model.CIUDAD));
                        cmd.Parameters.Add(new OracleParameter("TELEFONO", model.TELEFONO));
                        cmd.Parameters.Add(new OracleParameter("NUM_PLAZAS_DISPONIBLES", model.NUM_PLAZAS_DISPONIBLES));
                        cmd.Parameters.Add(new OracleParameter("COD_HOTEL", model.COD_HOTEL));

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
                var query = "DELETE FROM HOTEL WHERE COD_HOTEL = :COD_HOTEL";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new OracleParameter("COD_HOTEL", codigo));
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
