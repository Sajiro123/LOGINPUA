using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using LOGINPUA.Util;


namespace LN.EntidadesLN
{
    public class Menu_UsuarioLN
    {
        private Object bdConn;
        private Menu_UsuarioAD menus;

        public Menu_UsuarioLN()
        {
            menus = new Menu_UsuarioAD(ref bdConn);
        }


        public List<CC_MENU_USUARIO> ListarModulos(int id_modalidad, ref string mensaje, ref int tipo)
        {
            List<CC_MENU_USUARIO> resultado = new List<CC_MENU_USUARIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = menus.ListarModulos(id_modalidad);
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

        public List<CC_MENU_USUARIO> listarMenuPadre(int id_modalidad, ref string mensaje, ref int tipo)
        {
            List<CC_MENU_USUARIO> resultado = new List<CC_MENU_USUARIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = menus.listarMenuPadre(id_modalidad);
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

        public List<CC_MENU_USUARIO> listarMODULOS_X_ID(int id, int idmodalidad, ref string mensaje, ref int tipo)
        {
            List<CC_MENU_USUARIO> resultado = new List<CC_MENU_USUARIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = menus.listarMODULOS_X_ID(id, idmodalidad);
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


        public RPTA_GENERAL AgregarMenu(string nombre, string url, string icono, int tipomenu, int menupadre, int modulo, int idmodalidad, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = menus.AgregarMenu(nombre, url, icono, tipomenu, menupadre, modulo, idmodalidad, session_usuario);
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

        public RPTA_GENERAL AnularMenu(int idmenu, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado =  menus.AnularMenu(idmenu, session_usuario);
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


        public RPTA_GENERAL EditarMenu(int idmenu, string nombre, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = menus.EditarMenu(idmenu, nombre, session_usuario);
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

