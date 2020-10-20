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
using System.Data.SqlClient;

namespace AD.EntidadesAD
{
    public class PasajeroValidacionAD
    {
        OracleConnection conn;

        public PasajeroValidacionAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }
        public List<CC_PARADERO> paraderos_x_ID(int idRuta)
        {
            List<CC_PARADERO> resultado = new List<CC_PARADERO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_RUTA", OracleDbType.Varchar2) { Value = idRuta };

            using (var bdCmd = new OracleCommand("PKG_PASAJEROVAL_DET.SP_LISTARPARADEROS_SAP", conn))
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
                            if (!DBNull.Value.Equals(bdRd["ID_RUTA"])) { item.ID_RUTA = Convert.ToInt32(bdRd["ID_RUTA"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ETIQUETA_NOMBRE"])) { item.ETIQUETA_NOMBRE = bdRd["ETIQUETA_NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NRO_ORDEN"])) { item.NRO_ORDEN = Convert.ToInt32(bdRd["NRO_ORDEN"]); }
                            if (!DBNull.Value.Equals(bdRd["LADO"])) { item.LADO = bdRd["LADO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["SENTIDO"])) { item.SENTIDO = bdRd["SENTIDO"].ToString(); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }
        public List<CC_REPORTE_DESPACHO> Consultar_DespachoFecha(int mes, int año, string idruta)
        {
            List<CC_REPORTE_DESPACHO> List = new List<CC_REPORTE_DESPACHO>();
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_NUM_MES", OracleDbType.Int32) { Value = mes };
            bdParameters[1] = new OracleParameter("P_NUM_ANIO", OracleDbType.Int32) { Value = año };
            bdParameters[2] = new OracleParameter("P_RUTA", OracleDbType.Varchar2) { Value = idruta };
            bdParameters[3] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);


            using (var bdCmd = new OracleCommand("PKG_PASAJEROVAL_DET.listarMaestroPasajeros", conn))
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


                            if (!DBNull.Value.Equals(bdRd["ID_MAESTROPASAJERO"])) { item.ID_MAESTROPASAJERO = Convert.ToInt32(bdRd["ID_MAESTROPASAJERO"]); }
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

        public RPTA_GENERAL AnularMaestroSalida(int Id_maestro_salida)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_ID_MAESTRO_PASAJERO", OracleDbType.Varchar2) { Value = Id_maestro_salida };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PASAJEROVAL_DET.AnularMaestroPasaje", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
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

        public RPTA_GENERAL RegistrarMaestro(int idruta, string fecha, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[5];
            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idruta };
            bdParameters[1] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha };
            bdParameters[2] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[3] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[4] = new OracleParameter("P_ID_MAESTRO", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PASAJEROVAL_DET.SP_REGISTRAR_MAESTROVAL", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();

                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                    r.AUX = int.Parse(bdCmd.Parameters["P_ID_MAESTRO"].Value.ToString());

                }
            }
            catch (Exception ex)
            {
                r.AUX = 0;
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
            }
            return r;
        }

        public RPTA_GENERAL RegistrarDetalleValidacion(int id_maestropasajero, int id_paradero, int nro_servicio, string placa, string hora, string n_tarjeta, string nombre_chofer, string perfil, string padron, string operador, int id_carrera, double monto, string bus, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");
            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[15];
            bdParameters[0] = new OracleParameter("P_ID_MAESTROPASAJERO", OracleDbType.Int32) { Value = id_maestropasajero };
            bdParameters[1] = new OracleParameter("P_ID_PARADERO", OracleDbType.Int32) { Value = id_paradero };
            bdParameters[2] = new OracleParameter("P_NRO_SERVICIO", OracleDbType.Int32) { Value = nro_servicio };
            bdParameters[3] = new OracleParameter("P_BUS", OracleDbType.Varchar2) { Value = bus };
            bdParameters[4] = new OracleParameter("P_PLACA", OracleDbType.Varchar2) { Value = placa };
            bdParameters[5] = new OracleParameter("P_HORA", OracleDbType.Varchar2) { Value = hora };
            bdParameters[6] = new OracleParameter("P_N_TARJETA", OracleDbType.Varchar2) { Value = n_tarjeta };
            bdParameters[7] = new OracleParameter("P_NOMBRE_CHOFER", OracleDbType.Varchar2) { Value = nombre_chofer };
            bdParameters[8] = new OracleParameter("P_PERFIL", OracleDbType.Varchar2) { Value = perfil };
            bdParameters[9] = new OracleParameter("P_PADRON", OracleDbType.Varchar2) { Value = padron };
            bdParameters[10] = new OracleParameter("P_OPERADOR", OracleDbType.Varchar2) { Value = operador };
            bdParameters[11] = new OracleParameter("P_ID_CARRERA", OracleDbType.Int32) { Value = id_carrera };
            bdParameters[12] = new OracleParameter("P_MONTO", OracleDbType.Double) { Value = monto };
            bdParameters[13] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[14] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };



            try
            {
                using (var bdCmd = new OracleCommand("PKG_PASAJEROVAL_DET.SP_REGISTRAR_MAESTROVALDET", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();

                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                }
            }
            catch (Exception ex)
            {
                r.AUX = 0;
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
            }
            return r;
        }

        public DataSet Listarvalida_ruta_franjas(int franja, string fecha)
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_PASAJEROVAL_DET.LISTAR_DIA_RUTA_FRANJAVAL";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter cursor_A = new OracleParameter();
                    cursor_A.ParameterName = "P_CURSOR";
                    cursor_A.OracleDbType = OracleDbType.RefCursor;
                    cursor_A.Direction = ParameterDirection.Output;

                    OracleParameter comparativoA = new OracleParameter();
                    comparativoA.ParameterName = "P_FECHA";
                    comparativoA.OracleDbType = OracleDbType.Varchar2;
                    comparativoA.Direction = ParameterDirection.Input;
                    comparativoA.Value = fecha;

                    OracleParameter comparativoB = new OracleParameter();
                    comparativoB.ParameterName = "P_FRANJA";
                    comparativoB.OracleDbType = OracleDbType.Int32;
                    comparativoB.Direction = ParameterDirection.Input;
                    comparativoB.Value = franja;

                    command.Parameters.Add(cursor_A);
                    command.Parameters.Add(comparativoA);
                    command.Parameters.Add(comparativoB);

                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
                return ds;
            }
        }

        public DataSet listar_paraderos_ruta_val(int franja, int id_ruta, string fecha, string lado)
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_PASAJEROVAL_DET.LISTAR_PARADEROS_RUTA_VAL";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter cursor_A = new OracleParameter();
                    cursor_A.ParameterName = "P_CURSOR";
                    cursor_A.OracleDbType = OracleDbType.RefCursor;
                    cursor_A.Direction = ParameterDirection.Output;

                    OracleParameter comparativoA = new OracleParameter();
                    comparativoA.ParameterName = "P_ID_RUTA";
                    comparativoA.OracleDbType = OracleDbType.Int32;
                    comparativoA.Direction = ParameterDirection.Input;
                    comparativoA.Value = id_ruta;

                    OracleParameter comparativoB = new OracleParameter();
                    comparativoB.ParameterName = "P_FECHA";
                    comparativoB.OracleDbType = OracleDbType.Varchar2;
                    comparativoB.Direction = ParameterDirection.Input;
                    comparativoB.Value = fecha;

                    OracleParameter comparativoC = new OracleParameter();
                    comparativoC.ParameterName = "P_LADO";
                    comparativoC.OracleDbType = OracleDbType.Varchar2;
                    comparativoC.Direction = ParameterDirection.Input;
                    comparativoC.Value = lado;


                    OracleParameter comparativoD = new OracleParameter();
                    comparativoD.ParameterName = "P_FRANJA";
                    comparativoD.OracleDbType = OracleDbType.Int32;
                    comparativoD.Direction = ParameterDirection.Input;
                    comparativoD.Value = franja;


                    command.Parameters.Add(cursor_A);
                    command.Parameters.Add(comparativoA);
                    command.Parameters.Add(comparativoB);
                    command.Parameters.Add(comparativoC);
                    command.Parameters.Add(comparativoD);

                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
                return ds;
            }
        }

        public DataSet Calcular_franja(int id_ruta, string fecha, string lado)
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_PASAJEROVAL_DET.SP_GET_DATA_INTERVALOS";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter cursor_A = new OracleParameter();
                    cursor_A.ParameterName = "P_CURSOR";
                    cursor_A.OracleDbType = OracleDbType.RefCursor;
                    cursor_A.Direction = ParameterDirection.Output;

                    OracleParameter comparativoB = new OracleParameter();
                    comparativoB.ParameterName = "P_FECHA";
                    comparativoB.OracleDbType = OracleDbType.Varchar2;
                    comparativoB.Direction = ParameterDirection.Input;
                    comparativoB.Value = fecha;



                    OracleParameter comparativoC = new OracleParameter();
                    comparativoC.ParameterName = "P_LADO";
                    comparativoC.OracleDbType = OracleDbType.Varchar2;
                    comparativoC.Direction = ParameterDirection.Input;
                    comparativoC.Value = lado;

                    OracleParameter comparativoA = new OracleParameter();
                    comparativoA.ParameterName = "P_ID_RUTA";
                    comparativoA.OracleDbType = OracleDbType.Int32;
                    comparativoA.Direction = ParameterDirection.Input;
                    comparativoA.Value = id_ruta;




                    command.Parameters.Add(cursor_A);
                    command.Parameters.Add(comparativoB);
                    command.Parameters.Add(comparativoC);
                    command.Parameters.Add(comparativoA);

                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
                return ds;
            }
        }

        public DataSet Listarfechas_ruta_franjas(int franja, int idruta, string inicio, string fin)
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_PASAJEROVAL_DET.LISTAR_FECHAS_RUTA_FRANJAVAL";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter cursor_A = new OracleParameter();
                    cursor_A.ParameterName = "P_CURSOR";
                    cursor_A.OracleDbType = OracleDbType.RefCursor;
                    cursor_A.Direction = ParameterDirection.Output;

                    OracleParameter comparativoA = new OracleParameter();
                    comparativoA.ParameterName = "P_ID_RUTA";
                    comparativoA.OracleDbType = OracleDbType.Int32;
                    comparativoA.Direction = ParameterDirection.Input;
                    comparativoA.Value = idruta;

                    OracleParameter comparativoB = new OracleParameter();
                    comparativoB.ParameterName = "P_INIC";
                    comparativoB.OracleDbType = OracleDbType.Varchar2;
                    comparativoB.Direction = ParameterDirection.Input;
                    comparativoB.Value = inicio;

                    OracleParameter comparativoC = new OracleParameter();
                    comparativoC.ParameterName = "P_FIN";
                    comparativoC.OracleDbType = OracleDbType.Varchar2;
                    comparativoC.Direction = ParameterDirection.Input;
                    comparativoC.Value = fin;

                    OracleParameter comparativoD = new OracleParameter();
                    comparativoD.ParameterName = "P_FRANJA";
                    comparativoD.OracleDbType = OracleDbType.Int32;
                    comparativoD.Direction = ParameterDirection.Input;
                    comparativoD.Value = franja;


                    command.Parameters.Add(cursor_A);
                    command.Parameters.Add(comparativoA);
                    command.Parameters.Add(comparativoB);
                    command.Parameters.Add(comparativoC);
                    command.Parameters.Add(comparativoD);



                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);

                }
                return ds;

            }
        }
        public RPTA_GENERAL verificarRegistroPasajeroValExistente(int idRuta, string fecha)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            var id_maestro_salida = 0;
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_FECHA_REGISTRO", OracleDbType.Varchar2) { Value = fecha };
            bdParameters[1] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idRuta };
            bdParameters[2] = new OracleParameter("P_ID_MAESTRO_SALIDA", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PASAJEROVAL_DET.VerificaMaestroPasajeroDet", conn))
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

        public void Agregar_Temporal_POG(int id_temporal, string lado, string intervalo)
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_PASAJEROVAL_DET.AGREGAR_TEMPORAL_INTEVALO";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter comparativoB = new OracleParameter();
                    comparativoB.ParameterName = "P_ID_TEMPORAL";
                    comparativoB.OracleDbType = OracleDbType.Int32;
                    comparativoB.Direction = ParameterDirection.Input;
                    comparativoB.Value = id_temporal;

                    OracleParameter comparativoC = new OracleParameter();
                    comparativoC.ParameterName = "P_LADO";
                    comparativoC.OracleDbType = OracleDbType.Varchar2;
                    comparativoC.Direction = ParameterDirection.Input;
                    comparativoC.Value = lado;

                    OracleParameter comparativoD = new OracleParameter();
                    comparativoD.ParameterName = "P_INTERVALO";
                    comparativoD.OracleDbType = OracleDbType.Varchar2;
                    comparativoD.Direction = ParameterDirection.Input;
                    comparativoD.Value = intervalo;

                    command.Parameters.Add(comparativoB);
                    command.Parameters.Add(comparativoC);
                    command.Parameters.Add(comparativoD);


                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
            }
        }


        public RPTA_GENERAL RegistrarDetall_Hora(int id_maestro, int id_paradero, int num04, int num05, int num06, int num07, int num08, int num09, int num10, int num11, int num12, int num13, int num14, int num15, int num16, int num17, int num18, int num19, int num20, int num21, int num22, int num23, string usuario_session)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[24];
            bdParameters[0] = new OracleParameter("P_ID_MAESTROPASAJERO", OracleDbType.Int32) { Value = id_maestro };
            bdParameters[1] = new OracleParameter("P_ID_PARADERO_SAP", OracleDbType.Int32) { Value = id_paradero };
            bdParameters[2] = new OracleParameter("P_VAL04", OracleDbType.Int32) { Value = num04 };
            bdParameters[3] = new OracleParameter("P_VAL05", OracleDbType.Int32) { Value = num05 };
            bdParameters[4] = new OracleParameter("P_VAL06", OracleDbType.Int32) { Value = num06 };
            bdParameters[5] = new OracleParameter("P_VAL07", OracleDbType.Int32) { Value = num07 };
            bdParameters[6] = new OracleParameter("P_VAL08", OracleDbType.Int32) { Value = num08 };
            bdParameters[7] = new OracleParameter("P_VAL09", OracleDbType.Int32) { Value = num09 };
            bdParameters[8] = new OracleParameter("P_VAL10", OracleDbType.Int32) { Value = num10 };
            bdParameters[9] = new OracleParameter("P_VAL11", OracleDbType.Int32) { Value = num11 };
            bdParameters[10] = new OracleParameter("P_VAL12", OracleDbType.Int32) { Value = num12 };
            bdParameters[11] = new OracleParameter("P_VAL13", OracleDbType.Int32) { Value = num13 };
            bdParameters[12] = new OracleParameter("P_VAL14", OracleDbType.Int32) { Value = num14 };
            bdParameters[13] = new OracleParameter("P_VAL15", OracleDbType.Int32) { Value = num15 };
            bdParameters[14] = new OracleParameter("P_VAL16", OracleDbType.Int32) { Value = num16 };
            bdParameters[15] = new OracleParameter("P_VAL17", OracleDbType.Int32) { Value = num17 };
            bdParameters[16] = new OracleParameter("P_VAL18", OracleDbType.Int32) { Value = num18 };
            bdParameters[17] = new OracleParameter("P_VAL19", OracleDbType.Int32) { Value = num19 };
            bdParameters[18] = new OracleParameter("P_VAL20", OracleDbType.Int32) { Value = num20 };
            bdParameters[19] = new OracleParameter("P_VAL21", OracleDbType.Int32) { Value = num21 };
            bdParameters[20] = new OracleParameter("P_VAL22", OracleDbType.Int32) { Value = num22 };
            bdParameters[21] = new OracleParameter("P_VAL23", OracleDbType.Int32) { Value = num23 };
            bdParameters[22] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario_session };
            bdParameters[23] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };


            try
            {
                using (var bdCmd = new OracleCommand("PKG_PASAJEROVAL_DET.SP_REG_PASAJERODET_HORA", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";

                }
            }
            catch (Exception ex)
            {
                r.AUX = 0;
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
            }
            return r;

        }

        public RPTA_GENERAL RegistrarDetall_30min(int id_maestro, int id_paradero, int min30, int min59, int franja, string usuario_session)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[7];
            bdParameters[0] = new OracleParameter("P_ID_MAESTROPASAJERO", OracleDbType.Int32) { Value = id_maestro };
            bdParameters[1] = new OracleParameter("P_ID_PARADERO_SAP", OracleDbType.Int32) { Value = id_paradero };
            bdParameters[2] = new OracleParameter("P_MIN0_30", OracleDbType.Int32) { Value = min30 };
            bdParameters[3] = new OracleParameter("P_MIN30_59", OracleDbType.Int32) { Value = min59 };
            bdParameters[4] = new OracleParameter("P_HORARIO_FRANJA", OracleDbType.Int32) { Value = franja };
            bdParameters[5] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario_session };
            bdParameters[6] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PASAJEROVAL_DET.SP_REG_PASAJERODET_30MIN", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                }
            }
            catch (Exception ex)
            {
                r.AUX = 0;
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
            }
            return r;

        }
        public RPTA_GENERAL RegistrarDetall_10min(int id_maestro, int id_paradero, int min10, int min20, int min30, int min40, int min50, int min60, int franja, string usuario_session)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[11];
            bdParameters[0] = new OracleParameter("P_ID_MAESTROPASAJERO", OracleDbType.Int32) { Value = id_maestro };
            bdParameters[1] = new OracleParameter("P_ID_PARADERO_SAP", OracleDbType.Int32) { Value = id_paradero };
            bdParameters[2] = new OracleParameter("P_HORARIO_FRANJA", OracleDbType.Int32) { Value = franja };
            bdParameters[3] = new OracleParameter("P_MIN10", OracleDbType.Int32) { Value = min10 };
            bdParameters[4] = new OracleParameter("P_MIN20", OracleDbType.Int32) { Value = min20 };
            bdParameters[5] = new OracleParameter("P_MIN30", OracleDbType.Int32) { Value = min30 };
            bdParameters[6] = new OracleParameter("P_MIN40", OracleDbType.Int32) { Value = min40 };
            bdParameters[7] = new OracleParameter("P_MIN50", OracleDbType.Int32) { Value = min50 };
            bdParameters[8] = new OracleParameter("P_MIN60", OracleDbType.Int32) { Value = min60 };
            bdParameters[9] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario_session };
            bdParameters[10] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PASAJEROVAL_DET.SP_REG_PASAJERODET_10MIN", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                }
            }
            catch (Exception ex)
            {
                r.AUX = 0;
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
            }
            return r;

        }
    }
}








