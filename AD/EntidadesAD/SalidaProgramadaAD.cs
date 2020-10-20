using DA;
using Dapper;
using Entidades;
using Oracle.DataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio;
using ENTIDADES;
using System;

namespace AD.EntidadesAD
{
    public class SalidaProgramadaAD
    {
        OracleConnection conn;
        public SalidaProgramadaAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public RPTA_GENERAL registrarMaestroSalidaProgramada(int idTipoDia, int idRutaTipoServicio, string fechaProgramacion, int semana, string nomUsuario)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            var id_maestro_salida_prog = 0;
            OracleParameter[] bdParameters = new OracleParameter[7];

            bdParameters[0] = new OracleParameter("P_ID_TIPO_DIA", OracleDbType.Int32) { Value = idTipoDia };
            bdParameters[1] = new OracleParameter("P_ID_RUTA_TIPO_SERVICIO", OracleDbType.Int32) { Value = idRutaTipoServicio };
            bdParameters[2] = new OracleParameter("P_FECHA_PROGRAMACION", OracleDbType.Varchar2) { Value = fechaProgramacion };
            bdParameters[3] = new OracleParameter("P_SEMANA", OracleDbType.Int32) { Value = semana };
            bdParameters[4] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = nomUsuario };
            bdParameters[5] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[6] = new OracleParameter("P_ID_MAESTRO_SALIDA_PROG", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_GESTION_SALIDA.guardarMaestroSalidaProgramada", conn))
                {

                    bdCmd.Connection.Open();
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    id_maestro_salida_prog = int.Parse(bdCmd.Parameters["P_ID_MAESTRO_SALIDA_PROG"].Value.ToString());
                    r.AUX = id_maestro_salida_prog;
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

        public RPTA_GENERAL registrarProgrViajDespacho(int idsalidamaestro, string fechaprogramacion, string session_usuario)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            var id_maestro_salida_prog = 0;
            OracleParameter[] bdParameters = new OracleParameter[5];

            bdParameters[0] = new OracleParameter("P_ID_MSALIDA_PROG", OracleDbType.Int32) { Value = idsalidamaestro };
            bdParameters[1] = new OracleParameter("P_FECHA_PROGRAMACION", OracleDbType.Varchar2) { Value = fechaprogramacion };
            bdParameters[2] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[3] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[4] = new OracleParameter("P_ID_PROG_VIAJES", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_GESTION_SALIDA.guardarMaestroViajesDespacho", conn))
                {
                    bdCmd.Connection.Open();
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    id_maestro_salida_prog = int.Parse(bdCmd.Parameters["P_ID_PROG_VIAJES"].Value.ToString());
                    r.AUX = id_maestro_salida_prog;
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


        public RPTA_GENERAL verifica_maestro_prog(int idRutaTipoServicio, string fechaProgramacion)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            var id_maestro_salida_prog = 0;
            OracleParameter[] bdParameters = new OracleParameter[3];

            bdParameters[0] = new OracleParameter("P_ID_R_TIPO", OracleDbType.Int32) { Value = idRutaTipoServicio };
            bdParameters[1] = new OracleParameter("P_FECHA_PROG", OracleDbType.Varchar2) { Value = fechaProgramacion };
            bdParameters[2] = new OracleParameter("P_ID_MAESTRO_PROG", OracleDbType.Int32, direction: ParameterDirection.Output);
            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.VERIFICA_DATA_MAESTRO_PROG", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    id_maestro_salida_prog = int.Parse(bdCmd.Parameters["P_ID_MAESTRO_PROG"].Value.ToString());
                    r.AUX = id_maestro_salida_prog;
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


        public RPTA_GENERAL anular_maestro_prog(int maestro_salida)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            var id_cantidad = 0;
            OracleParameter[] bdParameters = new OracleParameter[2];

            bdParameters[0] = new OracleParameter("P_MAESTRO_SALIDA_PROG", OracleDbType.Int32) { Value = maestro_salida };
            bdParameters[1] = new OracleParameter("P_CANTIDAD_REGISTROS", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.ANULA_MAESTRO_PROG", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    id_cantidad = int.Parse(bdCmd.Parameters["P_CANTIDAD_REGISTROS"].Value.ToString());
                    r.AUX = id_cantidad;
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



        public List<CC_RESUMEN_PROGRAMADO> getResumenProgramado(int idServicio)
        {

            List<CC_RESUMEN_PROGRAMADO> resultado = new List<CC_RESUMEN_PROGRAMADO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idServicio }; ;
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.GET_RESUMEN_PROGRAMADO", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_RESUMEN_PROGRAMADO();

                            if (!DBNull.Value.Equals(bdRd["ID_MAESTRO_SALIDA_PROG"])) { item.ID_MAESTRO_SALIDA_PROG = bdRd["ID_MAESTRO_SALIDA_PROG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NRO_RUTA"])) { item.NRO_RUTA = bdRd["NRO_RUTA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TIPO_DIA"])) { item.TIPO_DIA = bdRd["TIPO_DIA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CANTIDAD_VIAJE"])) { item.CANTIDAD_VIAJE = bdRd["CANTIDAD_VIAJE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_PROGRAMACION"])) { item.FECHA_PROGRAMACION = bdRd["FECHA_PROGRAMACION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["SEMANA"])) { item.SEMANA = Convert.ToInt32(bdRd["SEMANA"]); }
                            if (!DBNull.Value.Equals(bdRd["ABREVIATURA"])) { item.ABREVIATURA = bdRd["ABREVIATURA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }




        public DataSet getResumenViajesRuta(int id_ruta, string fecha)
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_PROGRAMACION_BUSES.getViajesDiaRuta";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;



                    OracleParameter cursor_A = new OracleParameter();
                    cursor_A.ParameterName = "P_CURSOR";
                    cursor_A.OracleDbType = OracleDbType.RefCursor;
                    cursor_A.Direction = ParameterDirection.Output;


                    OracleParameter cursor_B = new OracleParameter();
                    cursor_B.ParameterName = "P_FECHA";
                    cursor_B.OracleDbType = OracleDbType.Varchar2;
                    cursor_B.Direction = ParameterDirection.Input;
                    cursor_B.Value = fecha;

                    OracleParameter cursor_c = new OracleParameter();
                    cursor_c.ParameterName = "P_ID_RUTA";
                    cursor_c.OracleDbType = OracleDbType.Int32;
                    cursor_c.Direction = ParameterDirection.Input;
                    cursor_c.Value = id_ruta;


                    command.Parameters.Add(cursor_A);
                    command.Parameters.Add(cursor_B);
                    command.Parameters.Add(cursor_c);

                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
                return ds;
            }
        }

        public DataSet ListServicioMaestroProg(int id_maestro)
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {
                    command.CommandText = "PKG_PROGRAMACION_BUSES.getServicioMaestroProg";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter cursor_A = new OracleParameter();
                    cursor_A.ParameterName = "P_CURSOR";
                    cursor_A.OracleDbType = OracleDbType.RefCursor;
                    cursor_A.Direction = ParameterDirection.Output;

                    OracleParameter cursor_c = new OracleParameter();
                    cursor_c.ParameterName = "P_ID_MAESTRO_SALIDA_PROG";
                    cursor_c.OracleDbType = OracleDbType.Int32;
                    cursor_c.Direction = ParameterDirection.Input;
                    cursor_c.Value = id_maestro;

                    command.Parameters.Add(cursor_A);
                    command.Parameters.Add(cursor_c);

                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
                return ds;
            }
        }



        public List<CC_RUTA_TIPO_SERVICIO> getTipoServicioByCorredor(int idCorredor)
        {

            List<CC_RUTA_TIPO_SERVICIO> resultado = new List<CC_RUTA_TIPO_SERVICIO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = idCorredor }; ;
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.GET_TIPO_SERV", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_RUTA_TIPO_SERVICIO();

                            if (!DBNull.Value.Equals(bdRd["ID_RUTA_TIPO_SERVICIO"])) { item.ID_RUTA_TIPO_SERVICIO = Convert.ToInt32(bdRd["ID_RUTA_TIPO_SERVICIO"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_TIPO_SERVICIO"])) { item.ID_TIPO_SERVICIO = Convert.ToInt32(bdRd["ID_TIPO_SERVICIO"]); }
                            if (!DBNull.Value.Equals(bdRd["TIPO_OPERACIONAL"])) { item.TIPO_OPERACIONAL = bdRd["TIPO_OPERACIONAL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NRO_RUTA"])) { item.NRO_RUTA = bdRd["NRO_RUTA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_RUTA"])) { item.ID_RUTA = Convert.ToInt32(bdRd["ID_RUTA"]); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }



        public List<CC_DATA_VIAJE_PROGRAMADO> getDataViajesProgramacion(int idMaestroSalidaProg)
        {

            List<CC_DATA_VIAJE_PROGRAMADO> resultado = new List<CC_DATA_VIAJE_PROGRAMADO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_ID_MAESTRO_SALIDA_PROG", OracleDbType.Int32) { Value = idMaestroSalidaProg };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.GET_VIAJES_PROGRAMACION", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader())
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_DATA_VIAJE_PROGRAMADO();

                            if (!DBNull.Value.Equals(bdRd["ID_MSALIDA_PROG_DET"])) { item.ID_MSALIDA_PROG_DET = Convert.ToInt32(bdRd["ID_MSALIDA_PROG_DET"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_MAESTRO_SALIDA_PROG"])) { item.TIPO_SERVICIO = bdRd["ID_MAESTRO_SALIDA_PROG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TIPO_DIA"])) { item.TIPO_DIA = bdRd["TIPO_DIA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["SERVICIO"])) { item.SERVICIO = bdRd["SERVICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["POG"])) { item.POG = bdRd["POG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["POT"])) { item.POT = bdRd["POT"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FNODE"])) { item.FNODE = bdRd["FNODE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["HSALIDA"])) { item.HSALIDA = bdRd["HSALIDA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TNODE"])) { item.TNODE = bdRd["TNODE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PIG"])) { item.PIG = bdRd["PIG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["LAYOVER"])) { item.LAYOVER = bdRd["LAYOVER"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ACUMULADO"])) { item.ACUMULADO = bdRd["ACUMULADO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["SENTIDO"])) { item.SENTIDO = bdRd["SENTIDO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TURNO"])) { item.TURNO = bdRd["TURNO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_TIPO_SERVICIO"])) { item.ID_TIPO_SERVICIO = Convert.ToInt32(bdRd["ID_TIPO_SERVICIO"]); }
                            if (!DBNull.Value.Equals(bdRd["PLACA"])) { item.PLACA = bdRd["PLACA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CAC_CONDUCTOR"])) { item.CAC_CONDUCTOR = bdRd["CAC_CONDUCTOR"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["HLLEGADA"])) { item.HLLEGADA = bdRd["HLLEGADA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TRIP_TIME"])) { item.TRIP_TIME = bdRd["TRIP_TIME"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["DISTANCIA"])) { item.DISTANCIA = bdRd["DISTANCIA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FNODE_A"])) { item.FNODE_A = bdRd["FNODE_A"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TNODE_A"])) { item.TNODE_A = bdRd["TNODE_A"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FNODE_B"])) { item.FNODE_B = bdRd["FNODE_B"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TNODE_B"])) { item.TNODE_B = bdRd["TNODE_B"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["DISTANCIA_A"])) { item.DISTANCIA_A = bdRd["DISTANCIA_A"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["DISTANCIA_B"])) { item.DISTANCIA_B = bdRd["DISTANCIA_B"].ToString(); }

                            resultado.Add(item);
                        }

                    }
                }
            }
            return resultado;
        }


        public RPTA_GENERAL guardarMSalidaProgramadaDet(int idMaestroSalidaProg, string tipodia, string servicio,
                                                        string pog, string pot, string fnode, string hSalida,
                                                        string hLlegada, string tNode, string PIG, double layover, string acumulado,
                                                        string sentido, string turno, int idtiposerv, string trip_time, string distancia, string placa,
                                                        string cacConductor, string usuarioRegistro)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[22];

            bdParameters[0] = new OracleParameter("P_ID_MAESTRO_SALIDA_PROG", OracleDbType.Int32) { Value = idMaestroSalidaProg };
            bdParameters[1] = new OracleParameter("P_TIPO_DIA", OracleDbType.Varchar2) { Value = tipodia };
            bdParameters[2] = new OracleParameter("P_SERVICIO", OracleDbType.Varchar2) { Value = servicio };
            bdParameters[3] = new OracleParameter("P_POG", OracleDbType.Varchar2) { Value = pog };
            bdParameters[4] = new OracleParameter("P_POT", OracleDbType.Varchar2) { Value = pot };
            bdParameters[5] = new OracleParameter("P_FNODE", OracleDbType.Varchar2) { Value = fnode };
            bdParameters[6] = new OracleParameter("P_HSALIDA", OracleDbType.Varchar2) { Value = hSalida };
            bdParameters[7] = new OracleParameter("P_HLLEGADA", OracleDbType.Varchar2) { Value = hLlegada };
            bdParameters[8] = new OracleParameter("P_TNODE", OracleDbType.Varchar2) { Value = tNode };
            bdParameters[9] = new OracleParameter("P_PIG", OracleDbType.Varchar2) { Value = PIG };
            bdParameters[10] = new OracleParameter("P_LAYOVER", OracleDbType.Decimal) { Value = layover };
            bdParameters[11] = new OracleParameter("P_ACUMULADO", OracleDbType.Varchar2) { Value = acumulado };
            bdParameters[12] = new OracleParameter("P_SENTIDO", OracleDbType.Varchar2) { Value = sentido };
            bdParameters[13] = new OracleParameter("P_TURNO", OracleDbType.Varchar2) { Value = turno };
            bdParameters[14] = new OracleParameter("P_TIPO_SERVICIO", OracleDbType.Int32) { Value = idtiposerv };
            bdParameters[15] = new OracleParameter("P_TRIP_TIME", OracleDbType.Varchar2) { Value = trip_time };
            bdParameters[16] = new OracleParameter("P_DISTANCIA", OracleDbType.Varchar2) { Value = distancia };
            bdParameters[17] = new OracleParameter("P_PLACA", OracleDbType.Varchar2) { Value = placa };
            bdParameters[18] = new OracleParameter("P_CAC_CONDUCTOR", OracleDbType.Varchar2) { Value = cacConductor };
            bdParameters[19] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuarioRegistro };
            bdParameters[20] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[21] = new OracleParameter("P_ID_MSALIDA_PROG_DET_", OracleDbType.Int32, direction: ParameterDirection.Output);


            try
            {
                using (var bdCmd = new OracleCommand("PKG_GESTION_SALIDA.guardarMSalidaProgramadaDet", conn))
                {
                    bdCmd.Connection.Open();
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                    r.AUX = int.Parse(bdCmd.Parameters["P_ID_MSALIDA_PROG_DET_"].Value.ToString());

                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
            }
            return r;
        }


        public RPTA_GENERAL getId_ProgViaje(int idMaestroSalidaProg, string fecha)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_ID_MAESTRO", OracleDbType.Int32, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_MAESTRO_SALIDA_PROG", OracleDbType.Int32) { Value = idMaestroSalidaProg };
            bdParameters[2] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha };


            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.getID_PROG_VIAJES", conn))
                {
                    bdCmd.Connection.Open();
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.AUX = int.Parse(bdCmd.Parameters["P_ID_MAESTRO"].Value.ToString());

                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
            }
            return r;
        }

        public RPTA_GENERAL guardarMViajesProgramadaDet(int id_prog_viajes, int id_msalida_prog_det, string usuarioRegistro)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[5];

            bdParameters[0] = new OracleParameter("P_ID_PROG_VIAJES", OracleDbType.Int32) { Value = id_prog_viajes };
            bdParameters[1] = new OracleParameter("P_ID_MSALIDA_PROG_DET", OracleDbType.Int32) { Value = id_msalida_prog_det };
            bdParameters[2] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuarioRegistro };
            bdParameters[3] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[4] = new OracleParameter("P_ID_PROG_VIAJES_", OracleDbType.Int32, direction: ParameterDirection.Output);


            try
            {
                using (var bdCmd = new OracleCommand("PKG_GESTION_SALIDA.guardarViajeDetall", conn))
                {
                    bdCmd.Connection.Open();
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                    r.AUX = int.Parse(bdCmd.Parameters["P_ID_PROG_VIAJES_"].Value.ToString());

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


    }
}
