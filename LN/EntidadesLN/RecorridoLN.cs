using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class RecorridoLN
    {
        private Object bdConn;
        private RecorridoAD recorrido;

        public RecorridoLN()
        {
            recorrido = new RecorridoAD(ref bdConn);
        }

        public List<CC_RECORRIDO> getRecorridosByRuta(int idRuta, ref string mensaje, ref int tipo)
        {
            List<CC_RECORRIDO> resultado = new List<CC_RECORRIDO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = recorrido.getRecorridosByRuta(idRuta);
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
