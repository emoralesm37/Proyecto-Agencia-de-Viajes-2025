using Oracle.ManagedDataAccess.Client;
using Proyectofinal.Models;
using System.Data;

namespace Proyectofinal.Data
{
    public class HospedajeData
    {
        public List<Hospedaje> Listar()
        {
            var oLista = new List<Hospedaje>();
            var cn = new Conexion();

            using (var conexion = new OracleConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                var query = "SELECT * FROM HOSPEDAJE";

                using (var cmd = new OracleCommand(query, conexion))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Hospedaje()
                            {
                                CodigoHospedaje = dr["COD_HOSPEDAJE"].ToString(),
                                CodigoTurista = dr["COD_TURISTA"].ToString(),
                                CodigoHotel = dr["COD_HOTEL"].ToString(),
                                //FechaLlegada = dr["FECHA_LLEGADA"].ToString(),
                                FechaLlegada = dr.GetDateTime(dr.GetOrdinal("FECHA_LLEGADA")),
                                //FechaPartida = dr["FECHA_PARTIDA"].ToString(),
                                FechaPartida = dr.GetDateTime(dr.GetOrdinal("FECHA_PARTIDA")),
                                Regimen = dr["REGIMEN"].ToString(),
                                Estado = dr["ESTADO"].ToString(),
                                FechaReserva = dr.GetDateTime(dr.GetOrdinal("FECHA_RESERVA"))
                                // FechaReserva = dr["FECHA_RESERVA"].ToString(),

                            });
                        }
                    }
                }
            }

            return oLista;
        }
        public Hospedaje Obtener(string codigo)
        {

            var obj = new Hospedaje();

            var cn = new Conexion();
            var query = "select * from HOSPEDAJE where COD_TURISTA=:COD_HOSPEDAJE";
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
                        obj.CodigoHospedaje = dr["COD_HOSPEDAJE"].ToString();
                        obj.CodigoTurista = dr["COD_TURISTA"].ToString();
                        obj.CodigoHotel = dr["COD_HOTEL"].ToString();
                        obj.FechaLlegada = dr.GetDateTime(dr.GetOrdinal("FECHA_LLEGADA"));
                        obj.FechaPartida = dr.GetDateTime(dr.GetOrdinal("FECHA_PARTIDA"));
                        obj.Regimen = dr["REGIMEN"].ToString();
                        obj.Estado = dr["ESTADO"].ToString();
                        obj.FechaReserva = dr.GetDateTime(dr.GetOrdinal("FECHA_RESERVA"));
                    }
                }
            }

            return obj;
        }
        public bool Guardar(Hospedaje model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"INSERT INTO HOSPEDAJE
                     (COD_HOSPEDAJE, COD_TURISTA, COD_HOTEL, FECHA_LLEGADA, FECHA_PARTIDA, REGIMEN, FECHA_RESERVA, ESTADO)
                     VALUES (:COD_HOSPEDAJE, :COD_TURISTA, :COD_HOTEL,:FECHA_LLEGADA,:FECHA_PARTIDA, :REGIMEN,:FECHA_RESERVA,:ESTADO)";
                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new OracleParameter("COD_HOSPEDAJE", model.CodigoHospedaje));
                        cmd.Parameters.Add(new OracleParameter("COD_TURISTA", model.CodigoTurista));
                        cmd.Parameters.Add(new OracleParameter("COD_HOTEL", model.CodigoHotel));
                        cmd.Parameters.Add(new OracleParameter("FECHA_LLEGADA", model.FechaLlegada));
                        cmd.Parameters.Add(new OracleParameter("FECHA_PARTIDA", model.FechaPartida));
                        cmd.Parameters.Add(new OracleParameter("REGIMEN", model.Regimen));
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


        public bool Editar(Hospedaje model)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                var query = @"UPDATE HOSPEDAJE SET 
                        COD_TURISTA = :COD_TURISTA,
                        COD_HOTEL = :COD_HOTEL,
                        FECHA_LLEGADA = :FECHA_LLEGADA,
                        FECHA_PARTIDA = :FECHA_PARTIDA,
                        REGIMEN = :REGIMEN,
                        FECHA_RESERVA = :FECHA_RESERVA ,
                        ESTADO = :ESTADO 
                      WHERE COD_TURISTA = :COD_TURISTA";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;

                        
                        cmd.Parameters.Add(new OracleParameter("COD_TURISTA", model.CodigoTurista));
                        cmd.Parameters.Add(new OracleParameter("COD_HOTEL", model.CodigoHotel));
                        cmd.Parameters.Add(new OracleParameter("FECHA_LLEGADA", model.FechaLlegada));
                        cmd.Parameters.Add(new OracleParameter("FECHA_PARTIDA", model.FechaPartida));
                        cmd.Parameters.Add(new OracleParameter("REGIMEN", model.Regimen));
                        cmd.Parameters.Add(new OracleParameter("FECHA_RESERVA", model.FechaReserva));
                        cmd.Parameters.Add(new OracleParameter("ESTADO", model.Estado));
                        cmd.Parameters.Add(new OracleParameter("COD_HOSPEDAJE", model.CodigoHospedaje));

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
                var query = "DELETE FROM HOSPEDAJE WHERE COD_HOSPEDAJE = :COD_HOSPEDAJE";

                using (var conexion = new OracleConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    using (var cmd = new OracleCommand(query, conexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new OracleParameter("COD_HOSPEDAJE", codigo));
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
