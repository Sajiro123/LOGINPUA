using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class SalidaLN
    {
        private Object bdConn;
        private SalidaAD salida;
        public SalidaLN()
        {
            salida = new SalidaAD(ref bdConn);
        }

        public List<CC_PARADERO> getParaderosByIdRuta(int id_ruta, ref string mensaje, ref int tipo)
        {
            List<CC_PARADERO> resultado = new List<CC_PARADERO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.getParaderosByIdRuta(id_ruta);
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

        public RPTA_GENERAL VerificarMaestroSalida(int idRuta, string fecha, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.VerificarMaestroSalida(idRuta, fecha);
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
        public RPTA_GENERAL registrarMaestroSalida(int idRuta, string fecha, string nomUsuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.registrarMaestroSalida(idRuta, fecha, nomUsuario);
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

        public RPTA_GENERAL registrarSalidaEjecutada(int idMaestroSalida, string padron,
                                                        string placa, string sentido, string horaSalida,
                                                        string horaLlegada, int nroServicio, string conductor,
                                                        string dniConductor, string comentario, string estadoViaje,
                                                        string usuarioRegistra, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.registrarSalidaEjecutada(idMaestroSalida, padron,
                                                        placa, sentido, horaSalida,
                                                        horaLlegada, nroServicio, conductor,
                                                        dniConductor, comentario, estadoViaje,
                                                        usuarioRegistra);
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

        public RPTA_GENERAL registrarSalidaHoraPasoParadero(int idSalidaEjecutada, int idParadero,
                                                    string horaPaso, string lado, string usuarioRegistra, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.registrarSalidaHoraPasoParadero(idSalidaEjecutada, idParadero, horaPaso, lado, usuarioRegistra);
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

        public DataSet getDataComparativos(string fechaComparativoA, string fechaComparativoB, int idRuta, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.getDataComparativos(fechaComparativoA, fechaComparativoB, idRuta);
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
        public DataSet getViajesPorRuta(int _idRuta, string _fechaInicio, string _fechaFin, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.getViajesPorRuta(_idRuta, _fechaInicio, _fechaFin);
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

        public RPTA_GENERAL AnularMaestroSalida(int Id_maestro_salida, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.AnularMaestroSalida(Id_maestro_salida);
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
        public List<CC_REPORTE_DESPACHO> Consultar_DespachoFecha(int mes, int año, string idruta, ref string mensaje, ref int tipo)
        {
            List<CC_REPORTE_DESPACHO> resultado = new List<CC_REPORTE_DESPACHO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = salida.Consultar_DespachoFecha(mes, año, idruta);
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
