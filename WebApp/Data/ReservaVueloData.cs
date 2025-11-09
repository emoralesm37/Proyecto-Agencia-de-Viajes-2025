using Oracle.ManagedDataAccess.Client;
using Proyectofinal.Models;
using System.Data;

namespace Proyectofinal.Data
{
    public class ReservaVueloData
    {
        public List<ReservaVuelo> Listar()
        {
            var oLista = new List<ReservaVuelo>();
            var cn = new Conexion();

            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                var query = "SELECT * FROM RESERVA_VUELO";

                using (var cmd = new OracleCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new ReservaVuelo()
                            {
                                CodigoReservaVuelo = dr["COD_RESERVA_VUELO"].ToString(),
                                CodigoTurista = dr["COD_TURISTA"].ToString(),
                                NumeroVuelo = dr["NUM_VUELO"].ToString(),
                                ClaseVuelo = dr["CLASE_VUELO"].ToString(),
                                FechaReserva = dr["FECHA_RESERVA"].ToString(),
                                Estado = dr["ESTADO"].ToString(),

                            });
                        }
                    }
                }
            }

            return oLista;
        }
        public ReservaVuelo Obtener(string codigo)
        {

            var obj = new ReservaVuelo();

            var cn = new Conexion();
            var query = "select * from RESERVA_VUELO where COD_RESERVA_VUELO=:COD_RESERVA_VUELO";
            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                OracleCommand cmd = new OracleCommand(query, conexion);
                cmd.Parameters.Add(new OracleParameter("COD_HOSPEDAJE", codigo));
                cmd.CommandType = CommandType.Text;
                //cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        obj.CodigoReservaVuelo = dr["COD_RESERVA_VUELO"].ToString();
                        obj.CodigoTurista = dr["COD_TURISTA"].ToString();
                        obj.NumeroVuelo = dr["NUM_VUELO"].ToString();
                        obj.ClaseVuelo = dr["CLASE_VUELO"].ToString();
                        obj.FechaReserva = dr["FECHA_RESERVA"].ToString();
                        obj.Estado = dr["ESTADO"].ToString();
                    }
                }
            }

            return obj;
        }
        public bool Guardar(ReservaVuelo model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"INSERT INTO RESERVA_VUELO
                     (COD_RESERVA_VUELO, COD_TURISTA, NUM_VUELO,CLASE_VUELO,FECHA_RESERVA,ESTADO)
                     VALUES (:COD_RESERVA_VUELO, :COD_TURISTA, :NUM_VUELO,:CLASE_VUELO,:FECHA_RESERVA,:ESTADO)";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("COD_RESERVA_VUELO", model.CodigoReservaVuelo));
                        cmd.Parameters.Add(new OracleParameter("COD_TURISTA", model.CodigoTurista));
                        cmd.Parameters.Add(new OracleParameter("NUM_VUELO", model.NumeroVuelo));
                        cmd.Parameters.Add(new OracleParameter("CLASE_VUELO", model.ClaseVuelo));
                        cmd.Parameters.Add(new OracleParameter("FECHA_RESERVA", model.FechaReserva));
                        cmd.Parameters.Add(new OracleParameter("ESTADO", model.Estado));

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


        public bool Editar(ReservaVuelo model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"UPDATE RESERVA_VUELO SET 
                        COD_TURISTA = :COD_TURISTA,
                        NUM_VUELO = :NUM_VUELO,
                        CLASE_VUELO = :CLASE_VUELO,
                        FECHA_RESERVA = :FECHA_RESERVA,
                        ESTADO = :ESTADO 
                      WHERE COD_RESERVA_VUELO = :COD_RESERVA_VUELO";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("COD_RESERVA_VUELO", model.CodigoReservaVuelo));
                        cmd.Parameters.Add(new OracleParameter("COD_TURISTA", model.CodigoTurista));
                        cmd.Parameters.Add(new OracleParameter("NUM_VUELO", model.NumeroVuelo));
                        cmd.Parameters.Add(new OracleParameter("CLASE_VUELO", model.ClaseVuelo));
                        cmd.Parameters.Add(new OracleParameter("FECHA_RESERVA", model.FechaReserva));
                        cmd.Parameters.Add(new OracleParameter("ESTADO", model.Estado));

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
                var query = "DELETE FROM RESERVA_VUELO WHERE COD_RESERVA_VUELO = :COD_RESERVA_VUELO";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new OracleParameter("COD_RESERVA_VUELO", codigo));
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
