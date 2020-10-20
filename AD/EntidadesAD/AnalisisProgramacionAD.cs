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
    public class AnalisisProgramacionAD
    {
        OracleConnection conn;
        public AnalisisProgramacionAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public List<CC_RUTA_TIPO_SERVICIO> getRutasByModalidadTransporte(int idModalidad)
        {
            List<CC_RUTA_TIPO_SERVICIO> resultado = new List<CC_RUTA_TIPO_SERVICIO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_ID_MODALIDAD", OracleDbType.Int32) { Value = idModalidad };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_ANALISIS_PROGRAMACION.getRutasByModalidadTransporte", conn))
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
                            if (!DBNull.Value.Equals(bdRd["NOMBRE_ETIQUETA"])) { item.NOMBRE_ETIQUETA = bdRd["NOMBRE_ETIQUETA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_DATA_ANALISIS_PROG_COSAC> getDataMuestraViajesCOSAC(int idRutaTipoServicio, string fechaConsultaIni, string fechaConsultaFin)
        {
            List<CC_DATA_ANALISIS_PROG_COSAC> resultado = new List<CC_DATA_ANALISIS_PROG_COSAC>();

            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_ID_RUTA_TIPO_SERV", OracleDbType.Int32) { Value = idRutaTipoServicio };
            bdParameters[1] = new OracleParameter("P_FINI", OracleDbType.Varchar2) { Value = fechaConsultaIni };
            bdParameters[2] = new OracleParameter("P_FFIN", OracleDbType.Varchar2) { Value = fechaConsultaFin };
            bdParameters[3] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_ANALISIS_PROGRAMACION.getDataMuestraViajesCOSAC", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                try
                {
                    using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                    {
                        if (bdRd.HasRows)
                        {
                            while (bdRd.Read())
                            {
                                var item = new CC_DATA_ANALISIS_PROG_COSAC();
                                //if (!DBNull.Value.Equals(bdRd["ID_RUTA_TIPO_SERVICIO"])) { item.ID_RUTA_TIPO_SERVICIO = Convert.ToInt32(bdRd["ID_RUTA_TIPO_SERVICIO"]); }
                                if (!DBNull.Value.Equals(bdRd["ABREV_ESTACION"])) { item.ABREV_ESTACION = bdRd["ABREV_ESTACION"].ToString(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA"])) { item.FECHA = bdRd["FECHA"].ToString(); }
                                if (!DBNull.Value.Equals(bdRd["HEJECUTADA"])) { item.HEJECUTADA = bdRd["HEJECUTADA"].ToString(); }
                                if (!DBNull.Value.Equals(bdRd["HPROGRAMADA"])) { item.HPROGRAMADA = bdRd["HPROGRAMADA"].ToString(); }
                                if (!DBNull.Value.Equals(bdRd["SENTIDO"])) { item.SENTIDO = bdRd["SENTIDO"].ToString(); }
                                if (!DBNull.Value.Equals(bdRd["NUMBLOCK"])) { item.NUMBLOCK = Int32.Parse(bdRd["NUMBLOCK"].ToString()); }
                                if (!DBNull.Value.Equals(bdRd["SEQ_VIAJE"])) { item.SEQ_VIAJE = Int32.Parse(bdRd["SEQ_VIAJE"].ToString()); }
                                if (!DBNull.Value.Equals(bdRd["ID_BUS"])) { item.ID_BUS = Int32.Parse(bdRd["ID_BUS"].ToString()); }
                                if (!DBNull.Value.Equals(bdRd["ID_VIAJE"])) { item.ID_VIAJE = Int32.Parse(bdRd["ID_VIAJE"].ToString()); }
                                if (!DBNull.Value.Equals(bdRd["FEC_HORAPASO_REG"])) { item.FEC_HORAPASO_REG = bdRd["FEC_HORAPASO_REG"].ToString(); }
                                resultado.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    throw;
                }
            }
            return resultado;
        }

        public List<CC_DATA_ANALISIS_PROG_CORREDORES> getDataMuestraViajesCORREDORES(int idRuta, string fechaConsultaIni, string fechaConsultaFin)
        {
            List<CC_DATA_ANALISIS_PROG_CORREDORES> resultado = new List<CC_DATA_ANALISIS_PROG_CORREDORES>();

            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idRuta };
            bdParameters[1] = new OracleParameter("P_FINI", OracleDbType.Varchar2) { Value = fechaConsultaIni };
            bdParameters[2] = new OracleParameter("P_FFIN", OracleDbType.Varchar2) { Value = fechaConsultaFin };
            bdParameters[3] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);


            using (var bdCmd = new OracleCommand("PKG_ANALISIS_PROGRAMACION.getDataMuestraViajesCORREDORES", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                try
                {
                    using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                    {
                        if (bdRd.HasRows)
                        {
                            while (bdRd.Read())
                            {
                                var item = new CC_DATA_ANALISIS_PROG_CORREDORES();

                                if (!DBNull.Value.Equals(bdRd["ID_RUTA"])) { item.ID_RUTA = bdRd["ID_RUTA"].ToString(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA"])) { item.FECHA = bdRd["FECHA"].ToString(); }
                                if (!DBNull.Value.Equals(bdRd["HORA_PASO"])) { item.HORA_PASO = bdRd["HORA_PASO"].ToString(); }
                                if (!DBNull.Value.Equals(bdRd["SENTIDO"])) { item.SENTIDO = bdRd["SENTIDO"].ToString(); }
                                if (!DBNull.Value.Equals(bdRd["ID_SALIDAEJECUTADA"])) { item.ID_SALIDAEJECUTADA = bdRd["ID_SALIDAEJECUTADA"].ToString(); }
                                if (!DBNull.Value.Equals(bdRd["ID_PARADERO"])) { item.ID_PARADERO = bdRd["ID_PARADERO"].ToString(); }
                                resultado.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    throw;
                }
            }
            return resultado;
        }

        public RPTA_GENERAL registrarMaestroViajeCOSAC(int idRutaTipoServicio, string fecha, string usuarioRegistra)
        {
            RPTA_GENERAL r = new RPTA_GENERAL();
            var id_maestroviajecosac = 0;
            OracleParameter[] bdParameters = new OracleParameter[5];
            bdParameters[0] = new OracleParameter("P_ID_RUTA_TIPO_SERVICIO", OracleDbType.Int32) { Value = idRutaTipoServicio };
            bdParameters[1] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha };
            bdParameters[2] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuarioRegistra };
            bdParameters[3] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[4] = new OracleParameter("P_IDMAESTROVIAJECOSAC", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_ANALISIS_PROGRAMACION.RegistraMaestroViajeCOSAC", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    id_maestroviajecosac = int.Parse(bdCmd.Parameters["P_IDMAESTROVIAJECOSAC"].Value.ToString());
                    r.AUX = id_maestroviajecosac;
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


        public List<CC_DATA_ANALISIS_PROG_COSAC> verificarDataParaAnalisis(string fechaConsulta, int idRutaTipoServicio)
        {
            List<CC_DATA_ANALISIS_PROG_COSAC> resultado = new List<CC_DATA_ANALISIS_PROG_COSAC>();
            var id_maestroviajecosac = 0;
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_FECHA_CONSULTA", OracleDbType.Varchar2) { Value = fechaConsulta };
            bdParameters[1] = new OracleParameter("P_ID_RUTA_T_SERVICIO", OracleDbType.Int32) { Value = idRutaTipoServicio };
            bdParameters[2] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            //
            using (var bdCmd = new OracleCommand("PKG_ANALISIS_PROGRAMACION.verificarDataParaAnalisis", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_DATA_ANALISIS_PROG_COSAC();
                            if (!DBNull.Value.Equals(bdRd["FECHA"])) { item.FECHA = bdRd["FECHA"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public RPTA_GENERAL registrarMDetalleViajeCOSAC(int idMaestroViajeCosac, string hProgramada, string hEjecutada, string estacion, string abrevEstacion,
                                                        string nomRuta, int nroBloque, string sentido, int seq_viaje, int idBus, int idViaje, string fechaHoraPasoRegistro, string usuarioRegistra)
        {
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[14];
            bdParameters[0] = new OracleParameter("P_IDMAESTROVIAJECOSAC", OracleDbType.Int32) { Value = idMaestroViajeCosac };
            bdParameters[1] = new OracleParameter("P_HPROGRAMADA", OracleDbType.Varchar2) { Value = hProgramada };
            bdParameters[2] = new OracleParameter("P_HEJECUTADA", OracleDbType.Varchar2) { Value = hEjecutada };
            bdParameters[3] = new OracleParameter("P_ESTACION", OracleDbType.Varchar2) { Value = estacion };
            bdParameters[4] = new OracleParameter("P_ABREV_ESTACION", OracleDbType.Varchar2) { Value = abrevEstacion };
            bdParameters[5] = new OracleParameter("P_NOM_RUTA", OracleDbType.Varchar2) { Value = nomRuta };
            bdParameters[6] = new OracleParameter("P_NUMBLOCK", OracleDbType.Int32) { Value = nroBloque };
            bdParameters[7] = new OracleParameter("P_SENTIDO", OracleDbType.Varchar2) { Value = sentido };
            bdParameters[8] = new OracleParameter("P_SEQ_VIAJE", OracleDbType.Int32) { Value = seq_viaje };
            bdParameters[9] = new OracleParameter("P_IDBUS", OracleDbType.Int32) { Value = idBus };
            bdParameters[10] = new OracleParameter("P_IDVIAJE", OracleDbType.Int32) { Value = idViaje };
            bdParameters[11] = new OracleParameter("P_FEC_HORAPASO_REG", OracleDbType.Varchar2) { Value = fechaHoraPasoRegistro };
            bdParameters[12] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuarioRegistra };
            bdParameters[13] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_ANALISIS_PROGRAMACION.registraMViajeDetalleCOSAC", conn))
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
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }

        public List<CC_RECORRIDO_TSERVICIO> getRecorridosTServByRutaServ(int idTipoServicioOper)
        {
            List<CC_RECORRIDO_TSERVICIO> resultado = new List<CC_RECORRIDO_TSERVICIO>();
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_ID_RUTA_TIPO_SERVICIO", OracleDbType.Int32) { Value = idTipoServicioOper };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_RUTA_TIPO_SRV.getRecorridosTServByRutaServ", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_RECORRIDO_TSERVICIO();

                            if (!DBNull.Value.Equals(bdRd["IDRECORRIDOTIPOSERVICIO"])) { item.IDRECORRIDOTIPOSERVICIO = Int32.Parse(bdRd["IDRECORRIDOTIPOSERVICIO"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["ID_RECORRIDO"])) { item.ID_RECORRIDO = Int32.Parse(bdRd["ID_RECORRIDO"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["LADO"])) { item.LADO = bdRd["LADO"].ToString(); }
                             if (!DBNull.Value.Equals(bdRd["SENTIDO"])) { item.SENTIDO = bdRd["SENTIDO"].ToString(); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_PARADERO> getParaderoTServByRecTserv(int idRecorridoTipoServicio)
        {
            List<CC_PARADERO> resultado = new List<CC_PARADERO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_IDRECORRIDOTIPOSERVICIO", OracleDbType.Int32) { Value = idRecorridoTipoServicio };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_ANALISIS_PROGRAMACION.getParaderoTServByRecTserv", conn))
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

                            if (!DBNull.Value.Equals(bdRd["ID_PARADERO_TIPOSERVICIO"])) { item.ID_PARADERO_TIPOSERVICIO = Int32.Parse(bdRd["ID_PARADERO_TIPOSERVICIO"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["NRO_ORDEN"])) { item.NRO_ORDEN = Int32.Parse(bdRd["NRO_ORDEN"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["ID_PARADERO"])) { item.ID_PARADERO = Int32.Parse(bdRd["ID_PARADERO"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ETIQUETA_NOMBRE"])) { item.ETIQUETA_NOMBRE = bdRd["ETIQUETA_NOMBRE"].ToString(); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public DataSet getViajesPorRuta(int id_ruta, string fechaConsulta_cambiada, string lado)
        {
            DataSet ds = new DataSet();
            try
            {
                using (OracleCommand command = new OracleCommand())
                {
                    command.CommandText = "PKG_ANALISIS_PROGRAMACION.getViajesporRuta";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter idRuta = new OracleParameter();
                    idRuta.ParameterName = "P_ID_RUTA";
                    idRuta.OracleDbType = OracleDbType.Int32;
                    idRuta.Direction = ParameterDirection.Input;
                    idRuta.Value = id_ruta;

                    OracleParameter fechaConsulta = new OracleParameter();
                    fechaConsulta.ParameterName = "P_FECHA";
                    fechaConsulta.OracleDbType = OracleDbType.Varchar2;
                    fechaConsulta.Direction = ParameterDirection.Input;
                    fechaConsulta.Value = fechaConsulta_cambiada;

                    OracleParameter Lado = new OracleParameter();
                    Lado.ParameterName = "P_LADO";
                    Lado.OracleDbType = OracleDbType.Varchar2;
                    Lado.Direction = ParameterDirection.Input;
                    Lado.Value = lado;

                    OracleParameter cursor = new OracleParameter();
                    cursor.ParameterName = "P_CURSOR";
                    cursor.OracleDbType = OracleDbType.RefCursor;
                    cursor.Direction = ParameterDirection.Output;

                    command.Parameters.Add(idRuta);
                    command.Parameters.Add(fechaConsulta);
                    command.Parameters.Add(Lado);
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

        public RPTA_GENERAL actualizarPromTemp(int id_temporal, double vel_prom, string tiempo_prom, string lado)
        {
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_ID_TEMPORAL", OracleDbType.Int32) { Value = id_temporal };
            bdParameters[1] = new OracleParameter("P_VEL_PROM", OracleDbType.Double) { Value = vel_prom };
            bdParameters[2] = new OracleParameter("P_TIEMPO_PROM", OracleDbType.Varchar2) { Value = tiempo_prom };
            bdParameters[3] = new OracleParameter("P_LADO", OracleDbType.Varchar2) { Value = lado };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_ANALISIS_PROGRAMACION.actualizarPromTemp", conn))
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
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }

    }
}
