

using ENTIDADES;
using LN.EntidadesLN;
using LOGINPUA.Util.Seguridad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class Menu_PerfilController : Controller
    {
        //private readonly Menu_PerfilLN _Menu_perfilLN;
        //private readonly PerfilLN _Perfil;
        //private readonly Menu_UsuarioLN _MenuUsuario;
        //Util.Util utilidades = new Util.Util();


        //// GET: Usuario_Personas
        //public Menu_PerfilController()
        //{

        //    _Menu_perfilLN =new Menu_PerfilLN();
        //    _Perfil = new PerfilLN();
        //    _MenuUsuario = new Menu_UsuarioLN();
        //}

        public ActionResult Asignar_MenuPerfi()
        {
            Util.Util utilidades = new Util.Util();

            var listaVistas = Session["menu_modulo"] as DataTable;
            var listacciones = Session["menu_acciones"] as List<CC_MENUPERFIL_ACCION>;
            //Session["menu_modulo"] = dt4;
            var nombreActionCurrent = this.ControllerContext.RouteData.Values["action"].ToString();
            bool auxValida = utilidades.validaVistaxPerfil(nombreActionCurrent, listaVistas);

            ViewBag.currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.currentRoute = this.ControllerContext.RouteData.Values["controller"].ToString();

            var Lista_acciones = utilidades.validadAccionMenu(listacciones, nombreActionCurrent, this.ControllerContext.RouteData.Values["controller"].ToString());


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
        public string listarMenuPadre(int idmodalidad)
        {
            Menu_UsuarioLN _MenuUsuario = new Menu_UsuarioLN();

            String mensaje = "";
            Int32 tipo = 0;
            var listar = _MenuUsuario.listarMenuPadre(idmodalidad, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string listarModalidad_Perfiles()
        {
            Menu_PerfilLN _Menu_perfilLN = new Menu_PerfilLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Menu_perfilLN.listarModalidad_Perfiles(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string AgregarPermiso_Perfil(int idperfil, int idmenu)
        {
            Menu_PerfilLN _Menu_perfilLN = new Menu_PerfilLN();
            String mensaje = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();
            var rpta = _Menu_perfilLN.AgregarPermiso_Perfil(idperfil, idmenu, usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(rpta);
            return result;
        }

        public string ListarPermisos(int idperfil)
        {
            Menu_PerfilLN _Menu_perfilLN = new Menu_PerfilLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Menu_perfilLN.ListarPermisos(idperfil, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }
        public string ListarMenu_x_ID(int idmenu, int idperfil)
        {
            Menu_PerfilLN _Menu_perfilLN = new Menu_PerfilLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Menu_perfilLN.ListarMenu_x_ID(idmenu, idperfil, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }
        public string Desactivar_Permisos(int idmenu, int idestado, int id_perfil)
        {
            Menu_PerfilLN _Menu_perfilLN = new Menu_PerfilLN();
            String mensaje = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();
            var rpta = _Menu_perfilLN.Desactivar_Permisos(idmenu, idestado, id_perfil, usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(rpta);
            return result;
        }
        public string Agregar_AccionMenu(int idmenu_perfil, string acciones)
        {
            Menu_PerfilLN _Menu_perfilLN = new Menu_PerfilLN();
            String mensaje = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();
            RPTA_GENERAL rpt = new RPTA_GENERAL();

            string[] array_accion = new string[40];
            array_accion = acciones.Split('|');

            rpt = _Menu_perfilLN.Desactivar_Acciones(idmenu_perfil, ref mensaje, ref tipo);

            if (acciones != "")
            {
                foreach (var id_accion in array_accion)
                {
                    if (id_accion != "")
                    {
                        int id_accion_ = Convert.ToInt32(id_accion);
                        rpt = _Menu_perfilLN.Agregar_AccionMenu(idmenu_perfil, id_accion_, usuario, ref mensaje, ref tipo);

                    }
                }
            }

            var result = JsonConvert.SerializeObject(rpt); //para la lista principal
            return result;
        }

        public string accesoslist_menu_idperfil(int idperfil)
        {
            Menu_PerfilLN _Menu_perfilLN = new Menu_PerfilLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Menu_perfilLN.accesoslist_menu_idperfil(idperfil, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string accesoslist_X_idmenuperfil(int idmenuperfil)
        {
            Menu_PerfilLN _Menu_perfilLN = new Menu_PerfilLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Menu_perfilLN.accesoslist_X_idmenuperfil(idmenuperfil, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }


        public string listarAcceso()
        {
            Menu_PerfilLN _Menu_perfilLN = new Menu_PerfilLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Menu_perfilLN.listarAcceso(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }


    }
}