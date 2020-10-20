using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using AD.EntidadesAD;
using ENTIDADES.InnerJoin;
using System.Data;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class LoginLN 
    {
        string mensaje = "";
        int tipo = 0;
        bool valor = true;
        private LoginAD _LoginAD;
        private Object bdConn;
        public LoginLN()
        {
            _LoginAD = new LoginAD(ref bdConn);
        }

        public DataSet IngresarIn(string usuario, string clave, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {

                    resultado = _LoginAD.IngresarIn(usuario, clave);
                
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
        public CC_USUARIO_PERSONA Verificar_Usuario(string usuario, string clave, ref string mensaje, ref int tipo)
        {
            CC_USUARIO_PERSONA resultado = new CC_USUARIO_PERSONA();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _LoginAD.Verificar_Usuario(usuario, clave);
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
