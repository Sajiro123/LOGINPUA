using System;
using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using AD;
 using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class CorredoresLN
    {
        private CorredorAD corredorAD;
        private Object bdConn;
        public CorredoresLN()
        {
            corredorAD = new CorredorAD(ref bdConn);
        }

        public List<CC_CORREDOR> obtenerListaCorredores(ref string mensaje, ref int tipo)
        {
            List<CC_CORREDOR> resultado = new List<CC_CORREDOR>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    Conexion.finalizar(ref bdConn);
                    resultado = corredorAD.getListaCorredor();
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
        public List<CC_CORREDOR> getRutaCorredor(ref string mensaje, ref int tipo)
        {
            List<CC_CORREDOR> resultado = new List<CC_CORREDOR>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = corredorAD.getRutaCorredor();
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
