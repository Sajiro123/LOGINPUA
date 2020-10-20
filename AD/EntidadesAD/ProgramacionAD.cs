using DA;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using ENTIDADES;
using Oracle.DataAccess.Client;
using System.Data;
using System;

namespace AD.EntidadesAD
{


    public class ProgramacionAD
    {
        OracleConnection conn;

        public ProgramacionAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public List<CC_DATOS_CORREDOR> getDataCorredor(int idCorredor) //cabecera de la programación
        {
            List<CC_DATOS_CORREDOR> resultado = new List<CC_DATOS_CORREDOR>();
            string horaactual = DateTime.Now.ToString("HH:mm:ss");

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = idCorredor }; ;



            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.getParametrosCorredor", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_DATOS_CORREDOR();
                            if (!DBNull.Value.Equals(bdRd["CORREDOR_NOMBRE"])) { item.CORREDOR_NOMBRE = bdRd["CORREDOR_NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["DIAS"])) { item.DIAS = bdRd["DIAS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["HORA_INICIO"])) { item.HORARIO_RUTA_INICIO = bdRd["HORA_INICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["HORA_FIN"])) { item.HORARIO_RUTA_FIN = bdRd["HORA_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TIPOLOGIA_NOMBRE"])) { item.TIPOLOGIA_NOMBRE = bdRd["TIPOLOGIA_NOMBRE"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }
        public RPTA_GENERAL ValidarFecha_Programados(string fecha, int idServicio)
        {
            var item = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha };
            bdParameters[2] = new OracleParameter("P_IDSERVICIO", OracleDbType.Int32) { Value = idServicio };



            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.VALIDARFECHA_PROGRAMACION", conn))
            {
                bdCmd.Connection.Open();
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            if (!DBNull.Value.Equals(bdRd["AUX"])) { item.DES_ESTADO = bdRd["AUX"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID"])) { item.AUX = Convert.ToInt32(bdRd["ID"]); }

                        }
                    }
                }
            }
            return item;

        }

        public List<CC_PROGRAMA_RUTA> getViajes_ProgrUnidades(string fecha, int id_ruta, string sentido)
        {
            List<CC_PROGRAMA_RUTA> resultado = new List<CC_PROGRAMA_RUTA>();
            string horaactual = DateTime.Now.ToString("HH:mm:ss");

            OracleParameter[] bdParameters = new OracleParameter[5];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha }; ;
            bdParameters[2] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = id_ruta }; ;
            bdParameters[3] = new OracleParameter("P_SENTIDO", OracleDbType.Varchar2) { Value = sentido }; ;
            bdParameters[4] = new OracleParameter("P_HORA_ACTUAL", OracleDbType.Varchar2) { Value = horaactual }; ;

            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.GET_VIAJES_PROGR_UNIDADES", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_PROGRAMA_RUTA();

                            if (!DBNull.Value.Equals(bdRd["ID_MSALIDA_PROG_DET"])) { item.ID_MSALIDA_PROG_DET = Convert.ToInt32(bdRd["ID_MSALIDA_PROG_DET"]); }
                            if (!DBNull.Value.Equals(bdRd["SERVICIO"])) { item.SERVICIO = Convert.ToInt32(bdRd["SERVICIO"]); }
                            if (!DBNull.Value.Equals(bdRd["HSALIDA"])) { item.HSALIDA = bdRd["HSALIDA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PLACA"])) { item.PLACA = bdRd["PLACA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CAC_CONDUCTOR"])) { item.CAC_CONDUCTOR = bdRd["CAC_CONDUCTOR"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["HORA_SALIDA_REAL"])) { item.HSALIDA_REAL = bdRd["HORA_SALIDA_REAL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PLACA_TIEMPOREAL"])) { item.PLACA_TIEMPOREAL = bdRd["PLACA_TIEMPOREAL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CAC_TIEMPOREAL"])) { item.CAC_TIEMPOREAL = Convert.ToInt32(bdRd["CAC_TIEMPOREAL"]); }


                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_PROGRAMA_RUTA> getviajesProgrServ(string fecha, int id_ruta, string sentido, int idservicio)
        {
            List<CC_PROGRAMA_RUTA> resultado = new List<CC_PROGRAMA_RUTA>();

            OracleParameter[] bdParameters = new OracleParameter[5];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_SERVICIO", OracleDbType.Int32) { Value = idservicio };
            bdParameters[2] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha }; ;
            bdParameters[3] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = id_ruta }; ;
            bdParameters[4] = new OracleParameter("P_SENTIDO", OracleDbType.Varchar2) { Value = sentido }; ;


            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.GET_VIAJES_PROGR_X_SERV", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_PROGRAMA_RUTA();

                            if (!DBNull.Value.Equals(bdRd["ID_MSALIDA_PROG_DET"])) { item.ID_MSALIDA_PROG_DET = Convert.ToInt32(bdRd["ID_MSALIDA_PROG_DET"]); }
                            if (!DBNull.Value.Equals(bdRd["SERVICIO"])) { item.SERVICIO = Convert.ToInt32(bdRd["SERVICIO"]); }
                            if (!DBNull.Value.Equals(bdRd["HSALIDA"])) { item.HSALIDA = bdRd["HSALIDA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PLACA"])) { item.PLACA = bdRd["PLACA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CAC_CONDUCTOR"])) { item.CAC_CONDUCTOR = bdRd["CAC_CONDUCTOR"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["HORA_SALIDA_REAL"])) { item.HSALIDA_REAL = bdRd["HORA_SALIDA_REAL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PLACA_TIEMPOREAL"])) { item.PLACA_TIEMPOREAL = bdRd["PLACA_TIEMPOREAL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CAC_TIEMPOREAL"])) { item.CAC_TIEMPOREAL = Convert.ToInt32(bdRd["CAC_TIEMPOREAL"]); }
                            if (!DBNull.Value.Equals(bdRd["OBSERVACIONES"])) { item.OBSERVACIONES = bdRd["OBSERVACIONES"].ToString(); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public RPTA_GENERAL Actulizar_ViajeTiempoReal(int idsalida, string unidad, int cac_temporal, string hora_salida, string observacion, string usureg_real)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[6];
            bdParameters[0] = new OracleParameter("P_ID_MSALIDA_PROG_DET", OracleDbType.Int32) { Value = idsalida };
            bdParameters[1] = new OracleParameter("P_UNIDAD", OracleDbType.Varchar2) { Value = unidad };
            bdParameters[2] = new OracleParameter("P_CAC_TIEMPOREAL", OracleDbType.Int32) { Value = cac_temporal };
            bdParameters[3] = new OracleParameter("P_HORA_SALIDA", OracleDbType.Varchar2) { Value = hora_salida };
            bdParameters[4] = new OracleParameter("P_OBSERVACIONES", OracleDbType.Varchar2) { Value = observacion };
            bdParameters[5] = new OracleParameter("P_USU_REG_REAL", OracleDbType.Varchar2) { Value = usureg_real };



            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.P_ACTUALIZAR_VIAJETIEMPOREAL", conn))
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


        public RPTA_GENERAL Actualizar_Incidencias(int id_maestro, string observacion, string horaejecutada, string usureg_real)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy HH:mm:ss");
            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_ID_MSALIDA_PROG_DET", OracleDbType.Int32) { Value = id_maestro };
            bdParameters[1] = new OracleParameter("P_OBSERVACIONES", OracleDbType.Varchar2) { Value = observacion };
            bdParameters[2] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[3] = new OracleParameter("P_USU_REG_REAL", OracleDbType.Varchar2) { Value = usureg_real };
            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.ACTUALIZAR_OBSERVIAJES", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se Actualizo correctamente";
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

        public RPTA_GENERAL Limpiar_ViajeDet(int id_maestro)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy HH:mm:ss");
            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_ID_MSALIDA_PROG_DET", OracleDbType.Int32) { Value = id_maestro };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.P_LIMPIAR_VIAJE_DET", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se Actualizo correctamente";
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



        public RPTA_GENERAL AgregarCamposPOG(int idruta, string FnodeA, string TnodeA, string FnodeB, string TnodeB, string DistanciaA, string DistanciaB, string usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[9];
            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idruta };
            bdParameters[1] = new OracleParameter("P_FNODE_A", OracleDbType.Varchar2) { Value = FnodeA };
            bdParameters[2] = new OracleParameter("P_TNODE_A", OracleDbType.Varchar2) { Value = TnodeA };
            bdParameters[3] = new OracleParameter("P_FNODE_B", OracleDbType.Varchar2) { Value = FnodeB };
            bdParameters[4] = new OracleParameter("P_TNODE_B", OracleDbType.Varchar2) { Value = TnodeB };
            bdParameters[5] = new OracleParameter("P_DISTANCIA_A", OracleDbType.Varchar2) { Value = DistanciaA };
            bdParameters[6] = new OracleParameter("P_DISTANCIA_B", OracleDbType.Varchar2) { Value = DistanciaB };
            bdParameters[7] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[8] = new OracleParameter("P_USU_REG_REAL", OracleDbType.Varchar2) { Value = usuario };


            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.REGISTRO_POG_CAMPOS", conn))
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
        public CC_PROGRAMA_RUTA getIdMaestro_x_fecha(int id_ruta, string fecha)
        {
            CC_PROGRAMA_RUTA resultado = new CC_PROGRAMA_RUTA();

            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = id_ruta }; ;
            bdParameters[2] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha }; ;


            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.GET_IDMAESTRO_X_FECHA", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            resultado = new CC_PROGRAMA_RUTA();

                            if (!DBNull.Value.Equals(bdRd["ID_MAESTRO_SALIDA_PROG"])) { resultado.ID_MAESTRO_SALIDA_PROG = Convert.ToInt32(bdRd["ID_MAESTRO_SALIDA_PROG"]); }

                        }
                    }
                }
            }
            return resultado;
        }

        public RPTA_GENERAL ValidarFecha_Programados(string fecha)
        {
            var item = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha };



            using (var bdCmd = new OracleCommand("PKG_PROG_POD.VALIDARFECHA", conn))
            {

                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            if (!DBNull.Value.Equals(bdRd["AUX"])) { item.DES_ESTADO = bdRd["AUX"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID"])) { item.AUX = Convert.ToInt32(bdRd["ID"]); }
                        }
                    }
                }
            }
            return item;

        }

        public RPTA_GENERAL nombre_zip(int id)
        {
            var item = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID", OracleDbType.Varchar2) { Value = id };



            using (var bdCmd = new OracleCommand("PKG_PROG_POD.NOM_ZIP", conn))
            {

                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Connection.Open();
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            if (!DBNull.Value.Equals(bdRd["AUX"])) { item.DES_ESTADO = bdRd["AUX"].ToString(); }
                        }
                    }
                }
            }
            return item;

        }

        public RPTA_GENERAL eliminar_Archivo(int id)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_NOMBRE", OracleDbType.Int32) { Value = id };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROG_POD.ELIMINAR_ARCHIVO", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.Connection.Open();
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se actualizo correctamente";
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

        public RPTA_GENERAL RegistroInforme(string nombre, string fecha, string usuario_session)

        {
            DateTime dateTime = DateTime.UtcNow.Date;
            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[1] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha };
            bdParameters[2] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario_session };
            bdParameters[3] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROG_POD.SP_INSERTAR_INFORMES", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.Connection.Open();
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

        public DataSet Listar_Informes(int mes, int año)
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_PROG_POD.SP_LISTAR_INFORMES";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter cursor_A = new OracleParameter();
                    cursor_A.ParameterName = "P_CURSOR";
                    cursor_A.OracleDbType = OracleDbType.RefCursor;
                    cursor_A.Direction = ParameterDirection.Output;

                    OracleParameter comparativoB = new OracleParameter();
                    comparativoB.ParameterName = "P_NUM_MES";
                    comparativoB.OracleDbType = OracleDbType.Varchar2;
                    comparativoB.Direction = ParameterDirection.Input;
                    comparativoB.Value = mes;

                    OracleParameter comparativoC = new OracleParameter();
                    comparativoC.ParameterName = "P_NUM_ANIO";
                    comparativoC.OracleDbType = OracleDbType.Varchar2;
                    comparativoC.Direction = ParameterDirection.Input;
                    comparativoC.Value = año;



                    command.Parameters.Add(cursor_A);
                    command.Parameters.Add(comparativoB);
                    command.Parameters.Add(comparativoC);



                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
                return ds;
            }
        }

    }

}
