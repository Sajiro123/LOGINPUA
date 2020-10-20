using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class PerfilLN
    {
        private Object bdConn;
        private PerfilAD _perfil;

        public PerfilLN()
        {
            _perfil = new PerfilAD(ref bdConn);
        }
        public List<CC_PERFIL_USUARIO> ListarPerfil(int idmodalidad, ref string mensaje, ref int tipo)
        {
            List<CC_PERFIL_USUARIO> resultado = new List<CC_PERFIL_USUARIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _perfil.ListarPerfil(idmodalidad);
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
        //public List<CC_PERSONA> ListarPersona()
        //{
        //    var datos = usuario.ListarPersona();
        //    return datos;
        //}

        //public List<CC_USUARIO_PERSONA> ListarUsuarios()
        //{
        //    var datos = usuario.ListarUsuarios();
        //    return datos;
        //}


        //public RPTA_GENERAL AgregarUsuario(int idpersona, int idmodalidad, int idperfil, string usuarios, string contraseña, string session_usuario)
        //{
        //    var datos = usuario.AgregarUsuario(idpersona,idmodalidad,idperfil, usuarios, contraseña,session_usuario);
        //    return datos;
        //}

        public RPTA_GENERAL AnularPerfil(int idperfil, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    Conexion.finalizar(ref bdConn);
                    resultado = _perfil.AnularPerfil(idperfil, session_usuario);
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

        public CC_PERFIL_USUARIO Validar_Perfil(int idperfil, ref string mensaje, ref int tipo)
        {
            CC_PERFIL_USUARIO resultado = new CC_PERFIL_USUARIO();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _perfil.Validar_Perfil(idperfil);
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
        
        public RPTA_GENERAL ModificarPerfil(int idmodalidad, int id_modalidad_perfil, string nombre_perfil, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _perfil.ModificarPerfil(idmodalidad, id_modalidad_perfil, nombre_perfil, session_usuario);
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

        public RPTA_GENERAL AgregarPerfil(string nombre, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _perfil.AgregarPerfil(nombre, session_usuario);
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

        public RPTA_GENERAL AgregarPerfil_modalidad(int id_perfil, int id_modalidad, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    Conexion.finalizar(ref bdConn);
                    resultado = _perfil.AgregarPerfil_modalidad(id_perfil, id_modalidad, session_usuario);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
             
            return resultado;

        }
    }
}

