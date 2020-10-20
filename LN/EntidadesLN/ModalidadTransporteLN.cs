using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class ModalidadTransporteLN
    {
        private Object bdConn;
        private ModalidadTransporteAD modalidadTransporte;
        public ModalidadTransporteLN()
        {
            modalidadTransporte = new ModalidadTransporteAD(ref bdConn);
        }
        public List<CC_MODALIDAD_TRANSPORTE> getModalidadTransporte(ref string mensaje, ref int tipo)
        {
            List<CC_MODALIDAD_TRANSPORTE> resultado = new List<CC_MODALIDAD_TRANSPORTE>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = modalidadTransporte.getModalidadTransporte();
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
