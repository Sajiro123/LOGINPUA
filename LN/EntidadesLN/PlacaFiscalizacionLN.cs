using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class PlacaFiscalizacionLN
    {
        private Object bdConn;
        private PlacaFiscalizacion placaFiscalizacion;

        public PlacaFiscalizacionLN()
        {
            placaFiscalizacion = new PlacaFiscalizacion(ref bdConn);
        }

        public List<CONADISPLACA> getPlacasConadis(ref string mensaje, ref int tipo)
        {
            List<CONADISPLACA> resultado = new List<CONADISPLACA>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = placaFiscalizacion.getPlacasConadis();
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

        public List<VECINOSPLACA> getplacasVecinosEmp(ref string mensaje, ref int tipo)
        {
            List<VECINOSPLACA> resultado = new List<VECINOSPLACA>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = placaFiscalizacion.getplacasVecinosEmp();
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
