using AD.EntidadesAD;
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


    public class RolPersonaController : Controller
    {

        //private readonly RolPersonaLN _RolPersona;
        //Util.Util utilidades = new Util.Util();

        //public RolPersonaController()
        //{
        //    _RolPersona = new RolPersonaLN();
        //}
        // GET: TbRol
        public ActionResult RolPersona()
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


        public string AgregarRol(string nombre)
        {
            RolPersonaLN _RolPersona = new RolPersonaLN();

            String mensaje = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();

            //Se registra el Perfil
            var rpt = _RolPersona.AgregarRolPersona(nombre, usuario, ref mensaje, ref tipo);
            var id_perfil = rpt.AUX;

            var result = JsonConvert.SerializeObject(rpt); //para agregar
            return result;
        }

        public string ListarRol()
        {
            RolPersonaLN _RolPersona = new RolPersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _RolPersona.ListarRol(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string AnularRol(int idrol)
        {
            RolPersonaLN _RolPersona = new RolPersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = Session["user_login"].ToString();

            var datos = _RolPersona.AnularRol(idrol, session_usuario, ref mensaje, ref tipo);


            var result = JsonConvert.SerializeObject(datos); //paraa anular
            return result;
        }

        public string ModificarRol(int id_rolpersona, string nombre)
        {
            RolPersonaLN _RolPersona = new RolPersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = Session["user_login"].ToString();
            var listar = _RolPersona.ModificarRol(id_rolpersona, nombre, session_usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

    }
}