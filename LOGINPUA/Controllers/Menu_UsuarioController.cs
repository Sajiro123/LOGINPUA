using ENTIDADES;
using LN.EntidadesLN;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class Menu_UsuarioController : Controller
    {
        //private readonly Menu_UsuarioLN _Menu_UsuarioLN;
        //Util.Util utilidades = new Util.Util();

        //public Menu_UsuarioController()
        //{
        //    _Menu_UsuarioLN =new Menu_UsuarioLN();
        //}
        public ActionResult Menu_usuario()
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
        public string ListarModulos(int id_modalidad)
        {
            Menu_UsuarioLN _Menu_UsuarioLN = new Menu_UsuarioLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Menu_UsuarioLN.ListarModulos(id_modalidad, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string listarMenuPadre(int id_modalidad)
        {
            Menu_UsuarioLN _Menu_UsuarioLN = new Menu_UsuarioLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Menu_UsuarioLN.listarMenuPadre(id_modalidad, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string AgregarMenu(string nombre, string url, string icono, int tipomenu, int menupadre, int modulo, int idmodalidad)
        {
            Menu_UsuarioLN _Menu_UsuarioLN = new Menu_UsuarioLN();
            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = Session["user_login"].ToString();

            var rpta = _Menu_UsuarioLN.AgregarMenu(nombre, url, icono, tipomenu, menupadre, modulo, idmodalidad, session_usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(rpta); //para la lista principal
            return result;
        }


        public string listarMODULOS_X_ID(int id, int idmodalidad)
        {
            Menu_UsuarioLN _Menu_UsuarioLN = new Menu_UsuarioLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Menu_UsuarioLN.listarMODULOS_X_ID(id, idmodalidad, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string AnularMenu(int id)
        {
            Menu_UsuarioLN _Menu_UsuarioLN = new Menu_UsuarioLN();
            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = Session["user_login"].ToString();

            var listar = _Menu_UsuarioLN.AnularMenu(id, session_usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }
        public string EditarMenu(int id, string nombre)
        {
            Menu_UsuarioLN _Menu_UsuarioLN = new Menu_UsuarioLN();
            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = Session["user_login"].ToString();

            var listar = _Menu_UsuarioLN.EditarMenu(id, nombre, session_usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }
    }
}