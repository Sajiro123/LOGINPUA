using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using AD;
using LOGINPUA.Util;
using System.Data;

namespace LN.EntidadesLN
{
    public class SalidaProgramadaLN
    {
        private Object bdConn;
        private SalidaProgramadaAD salida;
        public SalidaProgramadaLN()
        {
            salida = new SalidaProgramadaAD(ref bdConn);
        }

        public RPTA_GENERAL registrarMaestroSalidaProgramada(int idTipoDia, int idRutaTipoServicio, string fechaProgramacion, int semana, string nomUsuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    Conexion.finalizar(ref bdConn);
                    resultado = salida.registrarMaestroSalidaProgramada(idTipoDia, idRutaTipoServicio, fechaProgramacion, semana, nomUsuario);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }

            return resultado;
        }

        public RPTA_GENERAL registrarProgrViajDespacho(int idsalidamaestro, string fechaprogramacion, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.registrarProgrViajDespacho(idsalidamaestro, fechaprogramacion, session_usuario);
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
        public RPTA_GENERAL guardarMSalidaProgramadaDet(int idMaestroSalidaProg, string tipodia, string servicio,
                                                        string pog, string pot, string fnode, string hSalida,
                                                        string hLlegada, string tNode, string PIG, double layover, string acumulado,
                                                        string sentido, string turno, int idtiposerv, string trip_time, string distancia, string placa,
                                                        string cacConductor, string usuarioRegistro, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    Conexion.finalizar(ref bdConn);
                    resultado = salida.guardarMSalidaProgramadaDet(idMaestroSalidaProg, tipodia, servicio, pog,
                                                           pot, fnode, hSalida, hLlegada, tNode, PIG, layover,
                                                           acumulado, sentido, turno, idtiposerv, trip_time, distancia, placa,
                                                           cacConductor, usuarioRegistro);
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

        public RPTA_GENERAL getId_ProgViaje(int idMaestroSalidaProg,string fecha, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    Conexion.finalizar(ref bdConn);
                    resultado = salida.getId_ProgViaje(idMaestroSalidaProg, fecha);
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

        public DataSet ListServicioMaestroProg(int idMaestroSalidaProg, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    Conexion.finalizar(ref bdConn);
                    resultado = salida.ListServicioMaestroProg(idMaestroSalidaProg);
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


        public RPTA_GENERAL guardarMViajesProgramadaDet(int id_prog_viajes, int id_msalida_prog_det, string usuarioRegistro, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.guardarMViajesProgramadaDet(id_prog_viajes, id_msalida_prog_det, usuarioRegistro);
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
        public RPTA_GENERAL verifica_maestro_prog(int idTipoDia, string fechaProgramacion, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.verifica_maestro_prog(idTipoDia, fechaProgramacion);
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


        public RPTA_GENERAL anular_maestro_prog(int idmaestro, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.anular_maestro_prog(idmaestro);
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

        public List<CC_RESUMEN_PROGRAMADO> getResumenProgramado(int idServicio, ref string mensaje, ref int tipo)
        {
            List<CC_RESUMEN_PROGRAMADO> resultado = new List<CC_RESUMEN_PROGRAMADO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.getResumenProgramado(idServicio);
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
        public List<CC_RUTA_TIPO_SERVICIO> getTipoServicioByCorredor(int idCorredor, ref string mensaje, ref int tipo)
        {
            List<CC_RUTA_TIPO_SERVICIO> resultado = new List<CC_RUTA_TIPO_SERVICIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.getTipoServicioByCorredor(idCorredor);
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

        public List<CC_DATA_VIAJE_PROGRAMADO> getDataViajesProgramacion(int idMaestroSalidaProg, ref string mensaje, ref int tipo)
        {
            List<CC_DATA_VIAJE_PROGRAMADO> resultado = new List<CC_DATA_VIAJE_PROGRAMADO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.getDataViajesProgramacion(idMaestroSalidaProg);
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


        public DataSet getResumenViajesRuta(int id_ruta, string fecha, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.getResumenViajesRuta(id_ruta, fecha);
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
