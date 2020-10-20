using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
 using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class AnalisisProgramacionLN
    {
        private Object bdConn;
        private AnalisisProgramacionAD analisisProgramacion;

        public AnalisisProgramacionLN()
        {
            analisisProgramacion = new AnalisisProgramacionAD(ref bdConn);
        }
        public List<CC_RUTA_TIPO_SERVICIO> getRutasByModalidadTransporte(int idModalidad,ref string mensaje, ref int tipo)
        {
            List<CC_RUTA_TIPO_SERVICIO> resultado = new List<CC_RUTA_TIPO_SERVICIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                        resultado = analisisProgramacion.getRutasByModalidadTransporte(idModalidad);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }

        public List<CC_DATA_ANALISIS_PROG_COSAC> getDataMuestraViajesCOSAC(int idRutatipoServicio, string fechaConsultaIni, string fechaConsultaFin,ref string mensaje, ref int tipo)
        {
            List<CC_DATA_ANALISIS_PROG_COSAC> resultado = new List<CC_DATA_ANALISIS_PROG_COSAC>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = analisisProgramacion.getDataMuestraViajesCOSAC(idRutatipoServicio, fechaConsultaIni, fechaConsultaFin);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }

        public List<CC_DATA_ANALISIS_PROG_CORREDORES> getDataMuestraViajesCORREDORES(int idRutatipoServicio, string fechaConsultaIni, string fechaConsultaFin, ref string mensaje, ref int tipo)
        {
            List<CC_DATA_ANALISIS_PROG_CORREDORES> resultado = new List<CC_DATA_ANALISIS_PROG_CORREDORES>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = analisisProgramacion.getDataMuestraViajesCORREDORES(idRutatipoServicio, fechaConsultaIni, fechaConsultaFin);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;

        }

        public RPTA_GENERAL registrarMaestroViajeCOSAC(int idRutaTipoServicio, string fecha, string usuarioRegistra, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                        resultado = analisisProgramacion.registrarMaestroViajeCOSAC(idRutaTipoServicio, fecha, usuarioRegistra);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;

        }

        public RPTA_GENERAL registrarMDetalleViajeCOSAC(int idMaestroViajeCosac, string hProgramada, string hEjecutada, string estacion, string abrevEstacion,
                                                        string nomRuta, int nroBloque, string sentido, int seq_viaje, int idBus, int idViaje, string fechaHoraPasoRegistro, string usuarioRegistra, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = analisisProgramacion.registrarMDetalleViajeCOSAC(idMaestroViajeCosac, hProgramada, hEjecutada, estacion, abrevEstacion, nomRuta, nroBloque, sentido, seq_viaje, idBus, idViaje, fechaHoraPasoRegistro, usuarioRegistra);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;

        }

        public List<CC_RECORRIDO_TSERVICIO> getRecorridosTServByRutaServ(int idTipoServicioOper, ref string mensaje, ref int tipo)
        {
            List<CC_RECORRIDO_TSERVICIO> resultado = new List<CC_RECORRIDO_TSERVICIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = analisisProgramacion.getRecorridosTServByRutaServ(idTipoServicioOper);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;

        }

        public List<CC_PARADERO> getParaderoTServByRecTserv(int idRecorridoTipoServicio, ref string mensaje, ref int tipo)
        {
            List<CC_PARADERO> resultado = new List<CC_PARADERO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = analisisProgramacion.getParaderoTServByRecTserv(idRecorridoTipoServicio);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;

        }

        public List<CC_DATA_ANALISIS_PROG_COSAC> verificarDataParaAnalisis(string fechaConsulta, int idRutaTipoServicio, ref string mensaje, ref int tipo)
        {
            List<CC_DATA_ANALISIS_PROG_COSAC> resultado = new List<CC_DATA_ANALISIS_PROG_COSAC>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = analisisProgramacion.verificarDataParaAnalisis(fechaConsulta, idRutaTipoServicio);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;

        }

        public DataSet getViajesPorRuta(int id_ruta, string fechaConsulta_cambiada, string lado, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = analisisProgramacion.getViajesPorRuta(id_ruta, fechaConsulta_cambiada, lado);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }

        public RPTA_GENERAL actualizarPromTemp(int id_temporal, double vel_prom, string tiempo_prom, string lado, ref string mensaje, ref int tipo)                                               
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = analisisProgramacion.actualizarPromTemp(id_temporal, vel_prom, tiempo_prom, lado);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
                    
        }

    }
}
