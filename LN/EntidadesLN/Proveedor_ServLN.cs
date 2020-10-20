using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class Proveedor_ServLN
    {
        private Object bdConn;
        private Proveedor_ServAD _Proovedor_Serv;
 

        public Proveedor_ServLN()
        {
            _Proovedor_Serv = new Proveedor_ServAD(ref bdConn);
        }
      
        public List<CC_CORREDOR> ListCorredores(int id_prov_serv, ref string mensaje, ref int tipo)
        {
            List<CC_CORREDOR> resultado = new List<CC_CORREDOR>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Proovedor_Serv.ListCorredores(id_prov_serv);
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

        public List<CC_PROVUSU_CORREDOR> ListCorredor_X_Usuprov(int id_prov_corredor, ref string mensaje, ref int tipo)
        {
            List<CC_PROVUSU_CORREDOR> resultado = new List<CC_PROVUSU_CORREDOR>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Proovedor_Serv.ListCorredor_X_Usuprov(id_prov_corredor);
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
        public List<CC_PROVEEDORSERV> ListarProveedorServ(ref string mensaje, ref int tipo)
        {
            List<CC_PROVEEDORSERV> resultado = new List<CC_PROVEEDORSERV>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Proovedor_Serv.ListarProveedorServ();
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
        public List<CC_PROVEEDORSERV_USUARIO> ListarProvServ_Usuario(ref string mensaje, ref int tipo)
        {
            List<CC_PROVEEDORSERV_USUARIO> resultado = new List<CC_PROVEEDORSERV_USUARIO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Proovedor_Serv.ListarProvServ_Usuario();
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
                    resultado = _Proovedor_Serv.ListarUsuarios();
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
        public RPTA_GENERAL Desactivar_ProvUsuarios(int idpersona, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Proovedor_Serv.Desactivar_Usuarios(idpersona, session_usuario);
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
        public RPTA_GENERAL Desactivar_Cuenta_Usuario(int idpersona, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Proovedor_Serv.Desactivar_Cuenta_Usuario(idpersona, session_usuario);
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

        public RPTA_GENERAL Actualizar_ContraseñaAnti(int idprov_usu, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Proovedor_Serv.Actualizar_ContraseñaAnti(idprov_usu);
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

        public CC_USUARIO_PERSONA Buscar_x_idusuario(int idusuario, ref string mensaje, ref int tipo)
        {
            CC_USUARIO_PERSONA resultado = new CC_USUARIO_PERSONA();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Proovedor_Serv.Buscar_x_idusuario(idusuario);
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
        public RPTA_GENERAL AgregarUsuario_Proveedor(int idprov, string usuario, string contraseña, string usuario_session, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Proovedor_Serv.AgregarUsuario_Proveedor(idprov, usuario, contraseña, usuario_session);
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
        public RPTA_GENERAL AgregarUsuario_Proveedor_Usuario(int idprov,int idusuario, string usuario, string contraseña, string usuario_session, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Proovedor_Serv.AgregarUsuario_Proveedor_Usuario(idprov, idusuario, usuario, contraseña, usuario_session);
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
        public RPTA_GENERAL AgregarUsuario_Corredor(int idcorredores, int idusuario, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Proovedor_Serv.AgregarUsuario_Corredor(idcorredores, idusuario, session_usuario);
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

        public RPTA_GENERAL Actualizar_Usuario_Prov(int idcorredores, int idusuario, string session_usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Proovedor_Serv.Actualizar_Usuario_Prov(idcorredores, idusuario, session_usuario);
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
        public RPTA_GENERAL Rpta_Usuario_Existente(int idcorredores, int idusuario, int id_prov_serv, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Proovedor_Serv.Rpta_Usuario_Existente(idcorredores, idusuario, id_prov_serv);
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
        public RPTA_GENERAL Rpta_Cuenta_Existente(int idprov, string cuenta, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _Proovedor_Serv.Rpta_Cuenta_Existente(idprov, cuenta);
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

