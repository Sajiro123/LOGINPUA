using AD;
using AD.EntidadesAD;
using Entidades;
using ENTIDADES;
using LOGINPUA.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LN.EntidadesLN
{
    public class ConductoresLN  
    {
        private ConductoresAD _loginRepositorio;
        private Object bdConn;

        public ConductoresLN()
        {
            _loginRepositorio = new ConductoresAD(ref bdConn);
        }

        public List<CC_CONDUCTORES> Listar_Conductores(ref string mensaje, ref int tipo)
        {
            List<CC_CONDUCTORES> resultado = new List<CC_CONDUCTORES>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _loginRepositorio.Listar_Conductores();
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

        public CC_CONDUCTORES Conductor_x_DNI(string nrodocumento, ref string mensaje, ref int tipo)
        {
            CC_CONDUCTORES resultado = new CC_CONDUCTORES();
            try
            {
                
                    resultado = _loginRepositorio.Conductor_x_DNI(nrodocumento);
                
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

        public CC_PERSONAL Personal_x_DNI(string nrodocumento, ref string mensaje, ref int tipo)
        {
            CC_PERSONAL resultado = new CC_PERSONAL();
            try
            {
               
                    resultado = _loginRepositorio.Personal_x_DNI(nrodocumento);
                
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
