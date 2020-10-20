using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class ModuloLN
    {
        private Object bdConn;
        private ModuloAD _perfil;
        public ModuloLN()
        {
            _perfil = new ModuloAD(ref bdConn);
        }
        public List<CC_MODULO> ListarModulo(int idmodalidad, ref string mensaje, ref int tipo)
        {
            List<CC_MODULO> resultado = new List<CC_MODULO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _perfil.ListarModulo(idmodalidad);
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
        public RPTA_GENERAL AnularModulo(int idperfil, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _perfil.AnularModulo(idperfil, session_usuario);
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
        public List<CC_MODALIDAD> ListarModalidad(ref string mensaje, ref int tipo)
        {
            List<CC_MODALIDAD> resultado = new List<CC_MODALIDAD>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _perfil.ListarModalidad();
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

        public RPTA_GENERAL modificarModulo(int idmodulo, int id_modalidad, string nombre, string url, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _perfil.modificarModulo(idmodulo, id_modalidad, nombre, url, session_usuario);
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
    
        public RPTA_GENERAL AgregarModulo(int idmodalidad, string nombre, string url, string img, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _perfil.AgregarModulo(idmodalidad, nombre, url, img, session_usuario);
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
        public RPTA_GENERAL modificarModulo_IMG(int idmodalidad, string nombre,string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _perfil.modificarModulo_IMG(idmodalidad, nombre, session_usuario);
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

