using ENTIDADES;
using LN.EntidadesLN;
using LOGINPUA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpreadsheetLight;
using Newtonsoft.Json;
using System.Globalization;
using System.Transactions;
using System.IO.Compression;
using LN.Reportes;

namespace LOGINPUA.Controllers
{
    public class RutaController : Controller
    {
        //private readonly RutasLN _rutasLN;
        //private readonly CorredoresLN _corredorLN;
        //private readonly ModalidadTransporteLN _modalidadTransporteLN;
        //Util.Util utilidades = new Util.Util();
        //// GET: Ruta
        //public RutaController()
        //{
        //    _rutasLN = new RutasLN();
        //    _corredorLN = new CorredoresLN();
        //    _modalidadTransporteLN = new ModalidadTransporteLN();
        //}

        public ActionResult Inicio()
        {
            Util.Util utilidades = new Util.Util();
            CorredoresLN _corredorLN = new CorredoresLN();
            ModalidadTransporteLN _modalidadTransporteLN = new ModalidadTransporteLN();

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
                ViewBag.Accionesview = Lista_acciones;
                var datosCorredores = _corredorLN.obtenerListaCorredores(ref mensaje, ref tipo);
                ViewBag.modalidadUsuario = 2; //[1 : CORREDOR ] -- [2: COSAC] esto viene del usuario
                ViewBag.modalidadTransporte = _modalidadTransporteLN.getModalidadTransporte(ref mensaje, ref tipo);
                ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
                return View();
            }
            else
            {
                return RedirectToAction("Sistemas_", "Home");
            }
        }

        public string listarRutasByIdCorredor(int idCorredor)
        {
            RutasLN _rutasLN = new RutasLN();

            String mensaje = "";
            Int32 tipo = 0;
            var dataRecorrido = JsonConvert.SerializeObject(_rutasLN.listarRutasByIdCorredor(idCorredor, ref mensaje, ref tipo));
            return dataRecorrido;
        }

        public string registrarRuta(int idCorredor, string nombre, string nroRuta, string distritos)
        {
            RutasLN  _rutasLN = new RutasLN();

            String mensaje = "";
            Int32 tipo = 0;
            var registraRuta = JsonConvert.SerializeObject(_rutasLN.registrarRuta(idCorredor, nombre, nroRuta, distritos, Session["user_login"].ToString(), ref mensaje, ref tipo));
            return registraRuta;
        }

        public string registrarRecorrido(int idRuta, string sentido, string lado)
        {
            RutasLN _rutasLN = new RutasLN();

            String mensaje = "";
            Int32 tipo = 0;
            var registrarRecorrido = JsonConvert.SerializeObject(_rutasLN.registrarRecorrido(idRuta, sentido, lado, Session["user_login"].ToString(), ref mensaje, ref tipo));
            return registrarRecorrido;
        }

        public string anularRuta(int idRuta)
        {
            RutasLN _rutasLN = new RutasLN();

            String mensaje = "";
            Int32 tipo = 0;
            var anulaRecorrido = JsonConvert.SerializeObject(_rutasLN.anularRuta(idRuta, Session["user_login"].ToString(), ref mensaje, ref tipo));
            return anulaRecorrido;

        }

        public string editarRuta(int idRuta, string nroRuta, string nombre, string distritos)
        {
            RutasLN _rutasLN = new RutasLN();
            String mensaje = "";
            Int32 tipo = 0;
            var editarRuta = JsonConvert.SerializeObject(_rutasLN.editarRuta(idRuta, nroRuta, nombre, distritos, Session["user_login"].ToString(), ref mensaje, ref tipo));
            return editarRuta;
        }

        public string ActualizarRecorrido(int idRecorrido,string nombre)
        {
            RutasLN _rutasLN = new RutasLN();
            String mensaje = "";
            Int32 tipo = 0;
            var editarRuta = JsonConvert.SerializeObject(_rutasLN.ActualizarRecorrido(idRecorrido, nombre, ref mensaje, ref tipo));
            return editarRuta;
        }



    }
}
