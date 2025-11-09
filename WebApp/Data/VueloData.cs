using Oracle.ManagedDataAccess.Client;
using Proyectofinal.Models;
using System.Data;

namespace Proyectofinal.Data
{
    public class VueloData
    {
        public List<Vuelo> Listar()
        {
            var oLista = new List<Vuelo>();
            var cn = new Conexion();

            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                var query = "SELECT * FROM VUELO";

                using (var cmd = new OracleCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Vuelo()
                            {
                                Num_vuelo = dr["NUM_VUELO"].ToString(),
                                FechaHora = dr["FECHA_HORA"].ToString(),
                                Origen = dr["ORIGEN"].ToString(),
                                Destino = dr["DESTINO"].ToString(),
                                PlazasTotales = Convert.ToInt16( dr["PLAZAS_TOTALES"].ToString()),
                                PlazasClaseTurista = Convert.ToInt16( dr["PLAZAS_CLASE_TURISTA"].ToString()),
                            });
                        }
                    }
                }
            }

            return oLista;
        }
        public Vuelo Obtener(string codigo)
        {

            var obj = new Vuelo();

            var cn = new Conexion();
            var query = "select * from VUELO where NUM_VUELO=:NUM_VUELO";
            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                OracleCommand cmd = new OracleCommand(query, conexion);
                cmd.Parameters.Add(new OracleParameter("NUM_VUELO", codigo));
                cmd.CommandType = CommandType.Text;
                //cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        obj.Num_vuelo = dr["NUM_VUELO"].ToString();
                        obj.FechaHora = dr["FECHA_HORA"].ToString();
                        obj.Origen = dr["ORIGEN"].ToString();
                        obj.Destino = dr["DESTINO"].ToString();
                        obj.PlazasTotales = Convert.ToInt16(dr["PLAZAS_TOTALES"].ToString());
                        obj.PlazasClaseTurista = Convert.ToInt16(dr["PLAZAS_CLASE_TURISTA"].ToString());
                    }
                }
            }

            return obj;
        }
        public bool Guardar(Vuelo model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"INSERT INTO VUELO
                     (NUM_VUELO, FECHA_HORA, ORIGEN,DESTINO,PLAZAS_TOTALES,PLAZAS_CLASE_TURISTA)
                     VALUES (:NUM_VUELO, :FECHA_HORA, :ORIGEN,:DESTINO,:PLAZAS_TOTALES,:PLAZAS_CLASE_TURISTA)";
                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("NUM_VUELO", model.Num_vuelo));
                        cmd.Parameters.Add(new OracleParameter("FECHA_HORA", model.FechaHora));
                        cmd.Parameters.Add(new OracleParameter("ORIGEN", model.Origen));
                        cmd.Parameters.Add(new OracleParameter("DESTINO", model.Destino));
                        cmd.Parameters.Add(new OracleParameter("PLAZAS_TOTALES", model.PlazasTotales));
                        cmd.Parameters.Add(new OracleParameter("PLAZAS_CLASE_TURISTA", model.PlazasClaseTurista));

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


        public bool Editar(Vuelo model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"UPDATE VUELO SET 
                        FECHA_HORA = :FECHA_HORA,
                        ORIGEN = :ORIGEN,
                        DESTINO = :DESTINO,
                        PLAZAS_TOTALES = :PLAZAS_TOTALES,
                        PLAZAS_CLASE_TURISTA = :PLAZAS_CLASE_TURISTA 
                      WHERE NUM_VUELO = :NUM_VUELO";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        
                        cmd.Parameters.Add(new OracleParameter("FECHA_HORA", model.FechaHora));
                        cmd.Parameters.Add(new OracleParameter("ORIGEN", model.Origen));
                        cmd.Parameters.Add(new OracleParameter("DESTINO", model.Destino));
                        cmd.Parameters.Add(new OracleParameter("PLAZAS_TOTALES", model.PlazasTotales));
                        cmd.Parameters.Add(new OracleParameter("PLAZAS_CLASE_TURISTA", model.PlazasClaseTurista));
                        cmd.Parameters.Add(new OracleParameter("NUM_VUELO", model.Num_vuelo));

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
                var query = "DELETE FROM VUELO WHERE NUM_VUELO = :NUM_VUELO";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new OracleParameter("NUM_VUELO", codigo));
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
