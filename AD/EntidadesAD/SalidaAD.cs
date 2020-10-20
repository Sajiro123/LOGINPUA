using DA;
using Dapper;
using Entidades;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio;
using ENTIDADES;

namespace AD.EntidadesAD
{
    public class SalidaAD
    {
        OracleConnection conn;
        public SalidaAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public DataSet getViajesPorRuta(int _idRuta, string _fechaInicio, string _fechaFin)
        {
            DataSet ds = new DataSet();
            try
            {
                using (OracleCommand command = new OracleCommand())
                {
                    command.CommandText = "PKG_GESTION_SALIDA.getViajesPorRuta";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter idRuta = new OracleParameter();
                    idRuta.ParameterName = "P_ID_RUTA";
                    idRuta.OracleDbType = OracleDbType.Int32;
                    idRuta.Direction = ParameterDirection.Input;
                    idRuta.Value = _idRuta;

                    OracleParameter fechaInicio = new OracleParameter();
                    fechaInicio.ParameterName = "P_FECHA_INICIO";
                    fechaInicio.OracleDbType = OracleDbType.Varchar2;
                    fechaInicio.Direction = ParameterDirection.Input;
                    fechaInicio.Value = _fechaInicio;

                    OracleParameter fechaFin = new OracleParameter();
                    fechaFin.ParameterName = "P_FECHA_FIN";
                    fechaFin.OracleDbType = OracleDbType.Varchar2;
                    fechaFin.Direction = ParameterDirection.Input;
                    fechaFin.Value = _fechaFin;

                    OracleParameter cursor = new OracleParameter();
                    cursor.ParameterName = "P_CURSOR";
                    cursor.OracleDbType = OracleDbType.RefCursor;
                    cursor.Direction = ParameterDirection.Output;

                    command.Parameters.Add(idRuta);
                    command.Parameters.Add(fechaInicio);
                    command.Parameters.Add(fechaFin);
                    command.Parameters.Add(cursor);

                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet getDataComparativos(string fechaComparativoA, string fechaComparativoB, int _idRuta)
        {
            DataSet ds = new DataSet();
            using (OracleCommand command = new OracleCommand())
            {
                command.CommandText = "PKG_GESTION_SALIDA.getDataComparativa";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = conn;

                OracleParameter idRuta = new OracleParameter();
                idRuta.ParameterName = "P_ID_RUTA";
                idRuta.OracleDbType = OracleDbType.Int32;
                idRuta.Direction = ParameterDirection.Input;
                idRuta.Value = _idRuta;

                OracleParameter comparativoA = new OracleParameter();
                comparativoA.ParameterName = "P_COMPARATIVO_A";
                comparativoA.OracleDbType = OracleDbType.Varchar2;
                comparativoA.Direction = ParameterDirection.Input;
                comparativoA.Value = fechaComparativoA;

                OracleParameter comparativoB = new OracleParameter();
                comparativoB.ParameterName = "P_COMPARATIVO_B";
                comparativoB.OracleDbType = OracleDbType.Varchar2;
                comparativoB.Direction = ParameterDirection.Input;
                comparativoB.Value = fechaComparativoB;

                OracleParameter cursor_A = new OracleParameter();
                cursor_A.ParameterName = "P_CURSOR_A";
                cursor_A.OracleDbType = OracleDbType.RefCursor;
                cursor_A.Direction = ParameterDirection.Output;

                OracleParameter cursor_B = new OracleParameter();
                cursor_B.ParameterName = "P_CURSOR_B";
                cursor_B.OracleDbType = OracleDbType.RefCursor;
                cursor_B.Direction = ParameterDirection.Output;

                command.Parameters.Add(idRuta);
                command.Parameters.Add(comparativoA);
                command.Parameters.Add(comparativoB);
                command.Parameters.Add(cursor_A);
                command.Parameters.Add(cursor_B);

                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = command;
                da.Fill(ds);
            }
            return ds;
        }

        public List<CC_PARADERO> getParaderosByIdRuta(int idRuta)
        {
            List<CC_PARADERO> resultado = new List<CC_PARADERO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Double) { Value = idRuta };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_GESTION_SALIDA.getParaderosByIdRuta", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_PARADERO();

                            if (!DBNull.Value.Equals(bdRd["ID_PARADERO"])) { item.ID_PARADERO = Convert.ToInt32(bdRd["ID_PARADERO"]); }
                            if (!DBNull.Value.Equals(bdRd["LADO"])) { item.LADO = bdRd["LADO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NRO_ORDEN"])) { item.NRO_ORDEN = Convert.ToInt32(bdRd["NRO_ORDEN"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = Convert.ToString(bdRd["NOMBRE"]); }
                            if (!DBNull.Value.Equals(bdRd["ETIQUETA_NOMBRE"])) { item.ETIQUETA_NOMBRE = bdRd["ETIQUETA_NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["DISTANCIA_PARCIAL"])) { item.DISTANCIA_PARCIAL = Double.Parse(bdRd["DISTANCIA_PARCIAL"].ToString()); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }
        public RPTA_GENERAL VerificarMaestroSalida(int idRuta, string fecha)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            var id_maestro_salida = 0;
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_FECHA_REGISTRO", OracleDbType.Varchar2) { Value = fecha };
            bdParameters[1] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idRuta };
            bdParameters[2] = new OracleParameter("P_ID_MAESTRO_SALIDA", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_GESTION_SALIDA.VerificaDataEnMaestroSalida", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    id_maestro_salida = int.Parse(bdCmd.Parameters["P_ID_MAESTRO_SALIDA"].Value.ToString());
                    if (id_maestro_salida == 0) { r.COD_ESTADO = 0; }
                    else
                    {
                        r.AUX = id_maestro_salida;
                        r.COD_ESTADO = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }

        public RPTA_GENERAL registrarMaestroSalida(int idRuta, string fecha, string nomUsuario)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            var id_maestro_salida = 0;
            OracleParameter[] bdParameters = new OracleParameter[5];
            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idRuta };
            bdParameters[1] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha };
            bdParameters[2] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = nomUsuario };
            bdParameters[3] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[4] = new OracleParameter("P_ID_MAESTROSALIDA", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_GESTION_SALIDA.guardarMaestroSalida", conn))
                {
                    bdCmd.Connection.Open();
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    id_maestro_salida = int.Parse(bdCmd.Parameters["P_ID_MAESTROSALIDA"].Value.ToString());
                    r.AUX = id_maestro_salida;
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }

        public RPTA_GENERAL registrarSalidaEjecutada(int idMaestroSalida, string padron,
                                                        string placa, string sentido, string horaSalida,
                                                        string horaLlegada, int nroServicio, string conductor,
                                                        string dniConductor, string comentario, string estadoViaje,
                                                        string usuarioRegistra)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            var id_salida_ejecutada = 0;
            OracleParameter[] bdParameters = new OracleParameter[14];
            bdParameters[0] = new OracleParameter("P_ID_MAE_SALIDA", OracleDbType.Int32) { Value = idMaestroSalida };
            bdParameters[1] = new OracleParameter("P_PADRON", OracleDbType.Varchar2) { Value = padron };
            bdParameters[2] = new OracleParameter("P_PLACA", OracleDbType.Varchar2) { Value = placa };
            bdParameters[3] = new OracleParameter("P_SENTIDO", OracleDbType.Varchar2) { Value = sentido };
            bdParameters[4] = new OracleParameter("P_HORA_SALIDA", OracleDbType.Varchar2) { Value = horaSalida };
            bdParameters[5] = new OracleParameter("P_HORA_LLEGADA", OracleDbType.Varchar2) { Value = horaLlegada };
            bdParameters[6] = new OracleParameter("P_NRO_SERVICIO", OracleDbType.Int32) { Value = nroServicio };
            bdParameters[7] = new OracleParameter("P_CONDUCTOR", OracleDbType.Varchar2) { Value = conductor };
            bdParameters[8] = new OracleParameter("P_DNI_COND", OracleDbType.Varchar2) { Value = dniConductor };
            bdParameters[9] = new OracleParameter("P_COMENTARIO", OracleDbType.Varchar2) { Value = comentario };
            bdParameters[10] = new OracleParameter("P_ESTADO_VIAJE", OracleDbType.Varchar2) { Value = estadoViaje };
            bdParameters[11] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuarioRegistra };
            bdParameters[12] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[13] = new OracleParameter("P_ID_SALIDA_EJEC", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_GESTION_SALIDA.guardarSalidaEjecutada", conn))
                {
                    bdCmd.Connection.Open();
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    id_salida_ejecutada = int.Parse(bdCmd.Parameters["P_ID_SALIDA_EJEC"].Value.ToString());
                    r.AUX = id_salida_ejecutada;
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }

        public RPTA_GENERAL registrarSalidaHoraPasoParadero(int idSalidaEjecutada, int idParadero,
                                                    string horaPaso, string lado, string usuarioRegistra)
        {
            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[6];
            bdParameters[0] = new OracleParameter("P_ID_SALIDEJEC", OracleDbType.Int32) { Value = idSalidaEjecutada };
            bdParameters[1] = new OracleParameter("P_ID_PARADERO", OracleDbType.Int32) { Value = idParadero };
            bdParameters[2] = new OracleParameter("P_HORA_PASO", OracleDbType.Varchar2) { Value = horaPaso };
            bdParameters[3] = new OracleParameter("P_LADO", OracleDbType.Varchar2) { Value = lado };
            bdParameters[4] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuarioRegistra };
            bdParameters[5] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_GESTION_SALIDA.guardarSalidaHoraPasoParadero", conn))
                {
                    bdCmd.Connection.Open();
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }

        public RPTA_GENERAL AnularMaestroSalida(int Id_maestro_salida)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            var Count = 0;
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_ID_MAESTRO_SALIDA", OracleDbType.Varchar2) { Value = Id_maestro_salida };
            bdParameters[1] = new OracleParameter("P_CANTIDAD_REGISTROS", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_GESTION_SALIDA.AnularMaestroSalida", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    Count = int.Parse(bdCmd.Parameters["P_CANTIDAD_REGISTROS"].Value.ToString());
                    r.AUX = Count;
                    r.COD_ESTADO = 1;
                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }


        public List<CC_REPORTE_DESPACHO> Consultar_DespachoFecha(int mes, int año, string idruta)
        {
            List<CC_REPORTE_DESPACHO> List = new List<CC_REPORTE_DESPACHO>();
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_NUM_MES", OracleDbType.Int32) { Value = mes };
            bdParameters[1] = new OracleParameter("P_NUM_ANIO", OracleDbType.Int32) { Value = año };
            bdParameters[2] = new OracleParameter("P_RUTA", OracleDbType.Varchar2) { Value = idruta };
            bdParameters[3] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);


            using (var bdCmd = new OracleCommand("PKG_GESTION_SALIDA.getResumenRegistroMaestro", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_REPORTE_DESPACHO();
                            if (!DBNull.Value.Equals(bdRd["NRO_RUTA"])) { item.NRO_RUTA = Convert.ToInt32(bdRd["NRO_RUTA"]); }
                            if (!DBNull.Value.Equals(bdRd["FECHA"])) { item.FECHA = bdRd["FECHA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CANTIDAD_VIAJES"])) { item.CANTIDAD_VIAJES = Convert.ToInt32(bdRd["CANTIDAD_VIAJES"]); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = Convert.ToDateTime(bdRd["FECHA_REG"]); }
                            List.Add(item);
                        }
                    }
                }
            }
            return List;
        }
    }

}
