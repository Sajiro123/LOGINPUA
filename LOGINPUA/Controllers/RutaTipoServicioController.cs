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
    public class RutaTipoServicioController : Controller
    {
        // GET: RutaTipoServicio
        //private readonly CorredoresLN _CorredoresLN;
        //private readonly RutaLN _rutaLN;
        //private readonly RegistroPicoPlacaLN _registroPicoPlacaLN;
        //private readonly RecorridoLN _recorridoLN;
        //private readonly ParaderoLN _paraderoLN;
        //private readonly ViaLN _viaLN;
        //private readonly TipoServicioLN _tipoServicioLN;
        //private readonly ModalidadTransporteLN _modalidadTransporteLN;
        //Util.Util utilidades = new Util.Util();


        //public RutaTipoServicioController()
        //{
        //    _CorredoresLN = new CorredoresLN();
        //    _rutaLN = new RutaLN();
        //    _registroPicoPlacaLN = new RegistroPicoPlacaLN();
        //    _recorridoLN = new RecorridoLN();
        //    _paraderoLN = new ParaderoLN();
        //    _viaLN = new ViaLN();
        //    _tipoServicioLN = new TipoServicioLN();
        //    _modalidadTransporteLN = new ModalidadTransporteLN();
        //}

        public ActionResult Inicio()
        {
            Util.Util utilidades = new Util.Util();
            ModalidadTransporteLN _modalidadTransporteLN = new ModalidadTransporteLN();
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
                ViewBag.modalidadTransporte = _modalidadTransporteLN.getModalidadTransporte(ref mensaje, ref tipo);
                ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
                ViewBag.Accionesview = Lista_acciones;


                ViewBag.modalidadUsuario = 2; //[1 : CORREDOR ] -- [2: COSAC] esto viene del usuario
                return View();
            }
            else
            {
                return RedirectToAction("Sistemas_", "Home");
            }
        }

        public string getTiposServicioByIdRuta(int idRuta)
        {
            TipoServicioLN _tipoServicioLN = new TipoServicioLN();

            String mensaje = "";
            Int32 tipo = 0;
            var datos = _tipoServicioLN.getRutaTipoDeServicioOper(idRuta, ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(datos);
        }

        public string getTiposServicio()
        {
            TipoServicioLN _tipoServicioLN = new TipoServicioLN();
            String mensaje = "";
            Int32 tipo = 0;
            var datos = _tipoServicioLN.getTiposServicio(ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(datos);
        }

        public string getTiposServicioOperacional()
        {
            TipoServicioLN _tipoServicioLN = new TipoServicioLN();
            String mensaje = "";
            Int32 tipo = 0;
            var datos = _tipoServicioLN.getTiposServicioOperacional(ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(datos);
        }

        public string registrarRutaTipoServicio(int idRuta, int idTipoServicio, int idTipoServicioOper, string nombre,string color)
        {
            TipoServicioLN _tipoServicioLN = new TipoServicioLN();
            String mensaje = "";
            Int32 tipo = 0;
            var datosRegistro = _tipoServicioLN.registrarRutaTipoServicio(idRuta, idTipoServicio, idTipoServicioOper, nombre, color, Session["user_login"].ToString(), ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(datosRegistro);
        }

        public string editarRutaTservic(int idRutaTipoServ, int idTipoServicio, int idTipoServicioOper, string nombre,string color)
        {
            TipoServicioLN _tipoServicioLN = new TipoServicioLN();
            String mensaje = "";
            Int32 tipo = 0;
            var editarRuta = JsonConvert.SerializeObject(_tipoServicioLN.editarRutaTservic(idRutaTipoServ, idTipoServicio, idTipoServicioOper, nombre,color, Session["user_login"].ToString(), ref mensaje, ref tipo));
            return editarRuta;
        }


        public string anularRutaTipoServicio(int idRutaServicioOperacional)
        {
            TipoServicioLN _tipoServicioLN = new TipoServicioLN();
            String mensaje = "";
            Int32 tipo = 0;
            var datosAnula = _tipoServicioLN.anularRutaTipoServicio(idRutaServicioOperacional, Session["user_login"].ToString(), ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(datosAnula);
        }
        public string registraRecorridoServicioOperacional(int idRutaTipoServicio, string lado, string sentido)
        {
            TipoServicioLN _tipoServicioLN = new TipoServicioLN();
            String mensaje = "";
            Int32 tipo = 0;
            var datosRegistra = _tipoServicioLN.registraRecorridoServicioOperacional(idRutaTipoServicio, lado, sentido, Session["user_login"].ToString(), ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(datosRegistra);
        }

        public string getParaderoByIdRecorridTServ(int idRecorridoTipoServicio)
        {
            TipoServicioLN _tipoServicioLN = new TipoServicioLN();
            String mensaje = "";
            Int32 tipo = 0;
            var datosParaderos = _tipoServicioLN.getParaderoByIdRecorridTServ(idRecorridoTipoServicio, ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(datosParaderos);
        }

        public string actualizarParaderoTservic(int idParaderoTipoServicio, int idEstado)
        {
            TipoServicioLN _tipoServicioLN = new TipoServicioLN();
            String mensaje = "";
            Int32 tipo = 0;
            var actualizaParadero = _tipoServicioLN.actualizarParaderoTservic(idParaderoTipoServicio, idEstado, Session["user_login"].ToString(), ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(actualizaParadero);
        }
    }
}