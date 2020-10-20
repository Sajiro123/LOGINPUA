using ENTIDADES;
using LN.EntidadesLN;
using LOGINPUA.Models;
using LOGINPUA.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class CorrelativoController : Controller
    {

        //private readonly PerfilLN _PerfilLN;
        //Util.Util utilidades = new Util.Util();

        //private readonly LoginLN _LoginLN;
        //private readonly RutaLN _rutaLN;
        //private readonly CorredoresLN _CorredoresLN;
        //private readonly CorrelativoLN _CorrelativoLN;
        //public CorrelativoController()
        //{
        //    _LoginLN = new LoginLN();
        //    _rutaLN = new RutaLN();
        //    _CorredoresLN = new CorredoresLN();
        //    _CorrelativoLN = new CorrelativoLN();

        //}
 
        public ActionResult Informe_Correlativo()
        {
            Util.Util utilidades = new Util.Util();
            CorredoresLN _CorredoresLN = new CorredoresLN();
            RutaLN _rutaLN = new RutaLN();

            String mensaje = "";
            Int32 tipo = 0;
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
                var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
                ViewBag.rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);
                ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);

                ViewBag.Accionesview = Lista_acciones;
                return View();
            }
            else
            {
                return RedirectToAction("Sistemas_", "Home");
            }
        }

        public string Registrar_InfCorr(CC_CORRELATIVO_INFORME Model_Inf_Corr)
        {
            CorrelativoLN _CorrelativoLN = new CorrelativoLN();

            var result = "";
            String mensaje = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();
            Model_Inf_Corr.ID_ESTADO = 1;
            Model_Inf_Corr.USU_REG = usuario;
            var data = _CorrelativoLN.Registrar_InfCorr(Model_Inf_Corr, ref mensaje, ref tipo);
            result = JsonConvert.SerializeObject(data);

            return result;
        }
        public string Listar_InfCorr()
        {
            CorrelativoLN _CorrelativoLN = new CorrelativoLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _CorrelativoLN.Listar_InfCorr(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }

        public string Editar_InfCorr(CC_CORRELATIVO_INFORME Model_Inf_Corr)
        {
            CorrelativoLN _CorrelativoLN = new CorrelativoLN();
            string usuario = Session["user_login"].ToString();
            String mensaje = "";
            Int32 tipo = 0;
            Model_Inf_Corr.USU_MODIF = usuario;

            var data = _CorrelativoLN.Editar_InfCorr(Model_Inf_Corr, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(data);
            return result;
        }

        public string Actualizar_InfCorr(CC_CORRELATIVO_INFORME Model_Inf_Corr)
        {
            CorrelativoLN _CorrelativoLN = new CorrelativoLN();
            string usuario = Session["user_login"].ToString();
            String mensaje = "";
            Int32 tipo = 0;
            Model_Inf_Corr.USU_MODIF = usuario;

            var data = _CorrelativoLN.Actualizar_InfCorr(Model_Inf_Corr, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(data);
            return result;
        }
        public string Anular_InfCorr(int idInfCorr)
        {
            CorrelativoLN _CorrelativoLN = new CorrelativoLN();
            String mensaje = "";
            Int32 tipo = 0;
            var anularBus = JsonConvert.SerializeObject(_CorrelativoLN.Anular_InfCorr(idInfCorr, Session["user_login"].ToString(), ref mensaje, ref tipo));
            return anularBus;
        }

        public string Ultimo_InfCorr()
        {
            CorrelativoLN _CorrelativoLN = new CorrelativoLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _CorrelativoLN.Ultimo_InfCorr(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }
    }

}