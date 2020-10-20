using ENTIDADES;
using LN.EntidadesLN;
using LOGINPUA.Util.Seguridad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LOGINPUA.Util;
using System.Data;

namespace LOGINPUA.Controllers
{
    public class Usuario_PersonasController : Controller
    {
        //private readonly Usuario_PersonaLN _Usuario_PersonaLN;
        //private readonly Proveedor_ServLN _Proveedor_ServLN;

        //private readonly LoginLN _LoginLN;
        //Util.Util utilidades = new Util.Util();

        //// GET: Usuario_Personas
        //public Usuario_PersonasController()
        //{
        //    _Usuario_PersonaLN = new Usuario_PersonaLN();
        //    _LoginLN = new LoginLN();
        //    _Proveedor_ServLN = new Proveedor_ServLN();
        //}

        public ActionResult MantenimientoUsuario()
        {
            Util.Util utilidades = new Util.Util();
            var listaVistas = Session["menu_modulo"] as DataTable;
            var listacciones = Session["menu_acciones"] as List<CC_MENUPERFIL_ACCION>;
            //Session["menu_modulo"] = dt4;
            var nombreActionCurrent = this.ControllerContext.RouteData.Values["action"].ToString();
            bool auxValida = utilidades.validaVistaxPerfil(nombreActionCurrent, listaVistas);
            var Lista_acciones = utilidades.validadAccionMenu(listacciones, nombreActionCurrent, this.ControllerContext.RouteData.Values["controller"].ToString());

            ViewBag.currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.currentRoute = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (auxValida)
            {
                ViewBag.Accionesview = Lista_acciones;
                return View();
            }
            else
            {
                return RedirectToAction("Sistemas_", "Home");
            }
        }

        public string ListarPersonas()
        {
            Usuario_PersonaLN _Usuario_PersonaLN = new Usuario_PersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listaPersona = _Usuario_PersonaLN.ListarPersona(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listaPersona); //para la lista principal
            return result;
        }

        public string ListarPerfil_modalidad()
        {
            Usuario_PersonaLN _Usuario_PersonaLN = new Usuario_PersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Usuario_PersonaLN.ListarPerfil_modalidad(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }
        public string ListarUsuario_x_ID(int idusuario)
        {
            Usuario_PersonaLN _Usuario_PersonaLN = new Usuario_PersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Usuario_PersonaLN.ListarUsuario_x_ID(idusuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }
        public string Consultar_CuentaProvUsu(int id_usuario)
        {
            Usuario_PersonaLN _Usuario_PersonaLN = new Usuario_PersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Usuario_PersonaLN.Consultar_CuentaProvUsu(id_usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }
        public string ListCuentas_x_id(int id_prov_serv)
        {
            Usuario_PersonaLN _Usuario_PersonaLN = new Usuario_PersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Usuario_PersonaLN.ListCuentas_x_id(id_prov_serv, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string AgregarUsuario(int idpersona, string perfiles, string usuarios, string contraseña, int txtUsuarioProv, string idcorredores, string empresa)
        {
            Proveedor_ServLN _Proveedor_ServLN = new Proveedor_ServLN();
            Usuario_PersonaLN _Usuario_PersonaLN = new Usuario_PersonaLN();
            LoginLN _LoginLN = new LoginLN();
            String mensaje = "";
            Int32 tipo = 0;
            var txtUsuarioProvAbexa = new RPTA_GENERAL();
            var result = "";
            var IdprovABEXA = 0;
            var msj = _LoginLN.Verificar_Usuario(usuarios, contraseña, ref mensaje, ref tipo);

            var listaPersona = _Usuario_PersonaLN.ListarPersona(ref mensaje, ref tipo);
            ViewBag.listaPersona = listaPersona;

            string Session_usuario = Session["user_login"].ToString();
            string[] s_perfiles = new string[10];
            s_perfiles = perfiles.Split('|');

            var msj_final = msj == null ? 0 : msj.ID_USUARIO;
            if (msj_final == 0)
            {
                //ENCRIPTAR CONTRASEÑA
                var contraseña_ = Encriptador.Encriptar(contraseña);

                //REGISTRAR AGREGAR USUARIO
                var rpta = _Usuario_PersonaLN.AgregarUsuario(idpersona, usuarios, contraseña_, Session_usuario, ref mensaje, ref tipo);

                //ID DEL USUARIO CREADO
                var id_usuario = rpta.AUX;

                //AGREGAR PERFILES
                foreach (var id_perfil in s_perfiles)
                {
                    int id_perfil_ = Convert.ToInt32(id_perfil);
                    var rpta_ = _Usuario_PersonaLN.registrarUsuario_Perfil(id_usuario, id_perfil_, Session_usuario, ref mensaje, ref tipo);
                }


                //CREAR (ACTUALIZAR) CUENTA MATRIX
                if (empresa == "MATRIX" || empresa == "MATRIX Y ABEXA")
                {
                 var rpt=_Proveedor_ServLN.Actualizar_Usuario_Prov(txtUsuarioProv, id_usuario, Session_usuario, ref mensaje, ref tipo);
                }
                //CREAR CUENTA ABEXA
                if (empresa == "ABEXA" || empresa == "MATRIX Y ABEXA")
                {
                    txtUsuarioProvAbexa = _Proveedor_ServLN.AgregarUsuario_Proveedor_Usuario(1, id_usuario, usuarios, contraseña_, Session_usuario, ref mensaje, ref tipo);
                    IdprovABEXA = txtUsuarioProvAbexa.AUX;
                }

                if (idcorredores != "")
                {
                    if (empresa == "MATRIX")
                    {
                        //REGISTRAR EL CORREDOR
                        string[] id_corredor_nuevo = idcorredores.Split('|');
                        foreach (var idcorredor in id_corredor_nuevo)
                        {
                            int id_corredores_final = Convert.ToInt32(idcorredor);
                            _Proveedor_ServLN.AgregarUsuario_Corredor(id_corredores_final, txtUsuarioProv, Session_usuario, ref mensaje, ref tipo);
                        }
                    }
                    else if (empresa == "ABEXA")
                    {
                        //REGISTRAR EL CORREDOR
                        string[] id_corredor_nuevo = idcorredores.Split('|');
                        foreach (var idcorredor in id_corredor_nuevo)
                        {
                            int id_corredores_final = Convert.ToInt32(idcorredor);
                            _Proveedor_ServLN.AgregarUsuario_Corredor(id_corredores_final, IdprovABEXA, Session_usuario, ref mensaje, ref tipo);
                        }
                    }
                    else if (empresa == "MATRIX Y ABEXA")
                    {
                        string[] id_corredor_principal = idcorredores.Split(',');

                        //corredor matrix
                        string[] corredor_matrix = id_corredor_principal[0].Split('|');
                        foreach (var idcorredor in corredor_matrix)
                        {
                            int id_corredores_final = Convert.ToInt32(idcorredor);
                            _Proveedor_ServLN.AgregarUsuario_Corredor(id_corredores_final, txtUsuarioProv, Session_usuario, ref mensaje, ref tipo);
                        }
                        //corredor abexa
                        string[] corredor_abexa = id_corredor_principal[1].Split('|');
                        foreach (var idcorredor in corredor_abexa)
                        {
                            int id_corredores_final = Convert.ToInt32(idcorredor);
                            _Proveedor_ServLN.AgregarUsuario_Corredor(id_corredores_final, IdprovABEXA, Session_usuario, ref mensaje, ref tipo);
                        }
                    }
                }

                result = JsonConvert.SerializeObject(rpta);
            }
            else
            {
                RPTA_GENERAL rpt = new RPTA_GENERAL();
                rpt.DES_ESTADO = "Usuario Existente Cambiar";
                rpt.COD_ESTADO = 0;
                result = JsonConvert.SerializeObject(rpt);
            }

            return result;
        }

        public string ListarUsuarios()
        {
            Usuario_PersonaLN _Usuario_PersonaLN = new Usuario_PersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Usuario_PersonaLN.ListarUsuarios(ref mensaje, ref tipo);

            //Para Saber la clave desencriptada
            List<CC_USUARIO_PERSONA> Lista_Usuario = new List<CC_USUARIO_PERSONA>();
            //foreach (var item in listar)
            //{
            //    CC_USUARIO_PERSONA usuario = new CC_USUARIO_PERSONA();
            //    usuario.ID_USUARIO = item.ID_USUARIO;

            //    var Clave = Encriptador.Desencriptar(item.CLAVE);
            //    usuario.CLAVE = Clave;
            //    usuario.NOMBRE = item.NOMBRE;
            //    usuario.APEPAT = item.APEPAT;
            //    usuario.APEMAT = item.APEMAT;
            //    usuario.USU_REG = item.USU_REG;
            //    usuario.ID_ESTADO = item.ID_ESTADO;
            //    usuario.ID_USUARIO = item.ID_USUARIO;
            //    usuario.USUARIO = item.USUARIO;
            //    usuario.USU_REG = item.USU_REG;
            //    usuario.FECHA_REG = item.FECHA_REG;
            //    Lista_Usuario.Add(usuario);
            //}



            //var result = JsonConvert.SerializeObject(Lista_Usuario); //para la lista principal

            var result = JsonConvert.SerializeObject(listar); //para la lista principal


            return result;
        }
        public string Desactivar_Usuarios(int idusuario)
        {
            Usuario_PersonaLN _Usuario_PersonaLN = new Usuario_PersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();
            var rpta = _Usuario_PersonaLN.Desactivar_Usuarios(idusuario, usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(rpta);
            return result;
        }
        public string ModificarUsuarios(int idusuario, string clave, string estado_contraseña, int estado_usuario, string idperfiles)
        {
            Usuario_PersonaLN _Usuario_PersonaLN = new Usuario_PersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            string contraseñafinal = "";
            RPTA_GENERAL rpta = new RPTA_GENERAL();
            string usuario_ssesion = Session["user_login"].ToString();
            if (estado_contraseña == "encriptado")
            {
                contraseñafinal = clave;
            }
            else
            {
                var clave_encriptada = Encriptador.Encriptar(clave);
                contraseñafinal = clave_encriptada;
            }

            rpta = _Usuario_PersonaLN.ModificarUsuarios_Perfil(idusuario, idperfiles, contraseñafinal, estado_usuario, usuario_ssesion,ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(rpta);
            return result;
        }
    }
}