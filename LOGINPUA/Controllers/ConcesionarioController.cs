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
    public class ConcesionarioController : Controller
    {
        // GET: Concesionario
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Buses_Conductores_Programados()
        {
            var session_Perfil = int.Parse(Session["perfil_corredor"].ToString());
            ViewBag.id_perfilcc = session_Perfil;

            CorredoresLN _CorredoresLN = new CorredoresLN();
            String mensaje = "";
            Int32 tipo = 0;

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
                var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
                ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
                return View();
            }
            else
            {
                return RedirectToAction("Sistemas_", "Home");
            }

        }

    }
}