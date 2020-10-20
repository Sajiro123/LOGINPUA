using System;
using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class RutaLN
    {
        private RutaAD rutaAD;
        private Object bdConn;

        public RutaLN()
        {
            rutaAD = new RutaAD(ref bdConn);
        }

        public List<CC_RUTA> obtenerRutasPorCorredor(int idCorredor, ref string mensaje, ref int tipo)
        {
            List<CC_RUTA> resultado = new List<CC_RUTA>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = rutaAD.getRutaxCorredor(idCorredor);
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

        public List<CC_RUTA_CORREDOR> getRutaCorredor(ref string mensaje, ref int tipo)
        {
            List<CC_RUTA_CORREDOR> resultado = new List<CC_RUTA_CORREDOR>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = rutaAD.getRutaCorredor();
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

        public List<CC_RUTA_CORREDOR> getRuta_X_modalidad(int modalidad, ref string mensaje, ref int tipo)
        {
            List<CC_RUTA_CORREDOR> resultado = new List<CC_RUTA_CORREDOR>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = rutaAD.getRuta_X_modalidad(modalidad);
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

        public List<CC_RUTA_TIPO_SERVICIO> getRutaSerOpe_X_modalidad(int id_rtuta, ref string mensaje, ref int tipo)
        {
            List<CC_RUTA_TIPO_SERVICIO> resultado = new List<CC_RUTA_TIPO_SERVICIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = rutaAD.getRutaSerOpe_X_modalidad(id_rtuta);
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
        public List<CC_RECORRIDO> getLado_X_Ruta(int id_rtuta, ref string mensaje, ref int tipo)
        {
            List<CC_RECORRIDO> resultado = new List<CC_RECORRIDO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = rutaAD.getLado_X_Ruta(id_rtuta);
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
