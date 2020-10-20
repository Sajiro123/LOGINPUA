using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class Usuario_PersonaLN
    {
        private Object bdConn;
        private Usuario_PersonaAD usuario;

        public Usuario_PersonaLN()
        {
            usuario = new Usuario_PersonaAD(ref bdConn);
        }

        public List<CC_PERFIL_USUARIO> ListarPerfil_modalidad(ref string mensaje, ref int tipo)
        {
            List<CC_PERFIL_USUARIO> resultado = new List<CC_PERFIL_USUARIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = usuario.ListarPerfil_modalidad();
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

        public List<CC_PERFIL_USUARIO> ListarUsuario_x_ID(int id, ref string mensaje, ref int tipo)
        {
            List<CC_PERFIL_USUARIO> resultado = new List<CC_PERFIL_USUARIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = usuario.ListarUsuario_x_ID(id);
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
        public List<CC_PROVEEDORSERV_USUARIO> Consultar_CuentaProvUsu(int id_usuario, ref string mensaje, ref int tipo)
        {
            List<CC_PROVEEDORSERV_USUARIO> resultado = new List<CC_PROVEEDORSERV_USUARIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = usuario.Consultar_CuentaProvUsu(id_usuario);
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

        public List<CC_PROVEEDORSERV_USUARIO> ListCuentas_x_id(int id_prov, ref string mensaje, ref int tipo)
        {
            List<CC_PROVEEDORSERV_USUARIO> resultado = new List<CC_PROVEEDORSERV_USUARIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = usuario.ListCuentas_x_id(id_prov);
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

        public List<CC_PERSONA> ListarPersona(ref string mensaje, ref int tipo)
        {
            List<CC_PERSONA> resultado = new List<CC_PERSONA>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = usuario.ListarPersona();
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

        public List<CC_USUARIO_PERSONA> ListarUsuarios(ref string mensaje, ref int tipo)
        {
            List<CC_USUARIO_PERSONA> resultado = new List<CC_USUARIO_PERSONA>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = usuario.ListarUsuarios();
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

        public List<CC_PROVEEDORSERV_USUARIO> Listar_Cuentas_x_Usuario(int id_usuario, ref string mensaje, ref int tipo)
        {
            List<CC_PROVEEDORSERV_USUARIO> resultado = new List<CC_PROVEEDORSERV_USUARIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = usuario.Listar_Cuentas_x_Usuario(id_usuario);
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

        public RPTA_GENERAL AgregarUsuario(int idpersona, string usuarios, string contraseña, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = usuario.AgregarUsuario(idpersona, usuarios, contraseña, session_usuario);
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
        public RPTA_GENERAL registrarUsuario_Perfil(int idusuario, int idperfil, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = usuario.registrarUsuario_Perfil(idusuario, idperfil, session_usuario);
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

        public RPTA_GENERAL Desactivar_Usuarios(int idpersona, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = usuario.Desactivar_Usuarios(idpersona, session_usuario);
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

        public RPTA_GENERAL ModificarUsuarios_Perfil(int idusuario, string idperfil, string clave, int estado_usuario, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = usuario.ModificarUsuarios_Perfil(idusuario, idperfil, clave, estado_usuario, session_usuario);
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

