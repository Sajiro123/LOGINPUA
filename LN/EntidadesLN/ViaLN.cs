using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class ViaLN
    {
        private Object bdConn;
        private ViaAD via;
        public ViaLN()
        {
            via = new ViaAD(ref bdConn);
        }

        public List<CC_VIA> getVias(ref string mensaje, ref int tipo)
        {
            List<CC_VIA> resultado = new List<CC_VIA>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = via.getVias();
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
