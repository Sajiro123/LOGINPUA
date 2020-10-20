using System;
using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class TipoDiaLN
    {
        private TipoDiaAD tipoDiaAD;
        private Object bdConn;

        public TipoDiaLN()
        {
            tipoDiaAD = new TipoDiaAD(ref bdConn);
        }

        public List<CC_TIPO_DIA> obtenertipoDias(ref string mensaje, ref int tipo)
        {
            List<CC_TIPO_DIA> resultado = new List<CC_TIPO_DIA>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = tipoDiaAD.getTipoDias();
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
