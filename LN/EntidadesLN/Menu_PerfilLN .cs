using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class Menu_PerfilLN
    {
        private Object bdConn;
        private MenuPerfilAD _menuperfil;


        public Menu_PerfilLN()
        {
            _menuperfil = new MenuPerfilAD(ref bdConn);
        }

        public List<CC_MENU_PERFIL> ListarPermisos(int idperfil, ref string mensaje, ref int tipo)
        {
            List<CC_MENU_PERFIL> resultado = new List<CC_MENU_PERFIL>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _menuperfil.ListarPermisos(idperfil);
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

        public List<CC_MENUPERFIL_ACCION> listarAcceso(ref string mensaje, ref int tipo)
        {
            List<CC_MENUPERFIL_ACCION> resultado = new List<CC_MENUPERFIL_ACCION>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _menuperfil.listarAcceso();
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

        public List<CC_MENUPERFIL_ACCION> accesoslist_menu_idperfil(int idperfil, ref string mensaje, ref int tipo)
        {
            List<CC_MENUPERFIL_ACCION> resultado = new List<CC_MENUPERFIL_ACCION>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _menuperfil.accesoslist_menu_idperfil(idperfil);
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
        public List<CC_MENUPERFIL_ACCION> accesoslist_X_idmenuperfil(int idmenuperfil, ref string mensaje, ref int tipo)
        {
            List<CC_MENUPERFIL_ACCION> resultado = new List<CC_MENUPERFIL_ACCION>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _menuperfil.accesoslist_X_idmenuperfil(idmenuperfil);
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

        public List<CC_MENU_PERFIL> ListarMenu_x_ID(int idmenu, int idperfil, ref string mensaje, ref int tipo)
        {
            List<CC_MENU_PERFIL> resultado = new List<CC_MENU_PERFIL>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _menuperfil.ListarMenu_x_ID(idmenu, idperfil);
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

        public RPTA_GENERAL AgregarPermiso_Perfil(int idperfil, int idmenu, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _menuperfil.AgregarPermiso_Perfil(idperfil, idmenu, session_usuario);
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
        public RPTA_GENERAL Agregar_AccionMenu(int id_menuperfil, int id_accion, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
             
                    resultado = _menuperfil.Agregar_AccionMenu(id_menuperfil, id_accion, session_usuario);
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

        public RPTA_GENERAL Desactivar_Permisos(int idmenu, int idestado, int id_perfil, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _menuperfil.Desactivar_Permisos(idmenu, idestado, id_perfil, session_usuario);
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

        public RPTA_GENERAL Desactivar_Acciones(int idmenu, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _menuperfil.Desactivar_Acciones(idmenu);
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

        public List<CC_MENU_PERFIL> listarModalidad_Perfiles(ref string mensaje, ref int tipo)
        {
            List<CC_MENU_PERFIL> resultado = new List<CC_MENU_PERFIL>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _menuperfil.listarModalidad_Perfiles();
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

