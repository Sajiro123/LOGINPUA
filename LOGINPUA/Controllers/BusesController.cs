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
    public class BusesController : Controller
    {

       
        ReadExcel readExcel = new ReadExcel();

        public ActionResult Buses()
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

      

        public ActionResult Buses_Desafectados(int? id_bus_desafec,string ObservacionBus)
        {
            if (id_bus_desafec != null)
            {
                ViewBag.placa_desafec = id_bus_desafec;
                 
            }
            return View();
        }

        public ActionResult Consulta_PNP(string placa)
        {
            return View();
        }

        public string getRutaCorredor()
        {
            CorredoresLN _CorredoresLN = new CorredoresLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _CorredoresLN.getRutaCorredor(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }

        public string ListarBuses()
        {
            BUSESLN _BusesLN = new BUSESLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _BusesLN.Listar_Buses(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }

        public string ListarBusesbyId(int idCorredor)
        {
            BUSESLN _BusesLN = new BUSESLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _BusesLN.Listar_Buses_by_Id(idCorredor, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }

        public string Subir_Excel()
        {
            //RPTA_GENERAL e = new RPTA_GENERAL();
            RPTA_GENERAL rpta = new RPTA_GENERAL();

            RegistroPicoPlacaLN _registroPicoPlacaLN = new RegistroPicoPlacaLN();
            BUSESLN _BusesLN = new BUSESLN();
            String mensaje = "";
            Int32 tipo = 0;
            var data = "";
            string usuario = Session["user_login"].ToString();
            var ob = Request.Files;
            var archivoSubido = Request.Files[0];
            var nombreArchivo = Path.GetFileName(archivoSubido.FileName);
            var extensionArchivo = Path.GetExtension(archivoSubido.FileName);
            var arrFileName = nombreArchivo.Split('.');
            var nuevoNombreArchivoExcel = arrFileName[0] + "_FILE_" + DateTime.Now.ToString("dd_MM_yyyy_h_mm_ss") + extensionArchivo;
            var pathArchivo = Server.MapPath("~/Adjuntos/Files_Despacho/" + nuevoNombreArchivoExcel);
            string pathFinal = Path.Combine(pathArchivo);
            archivoSubido.SaveAs(pathFinal);
            _BusesLN.LimpiarTabla(ref mensaje, ref tipo);
            data = readExcel.Matriz_Placas(pathFinal, usuario); 
            return data;
        }


        public string Registro_Bus_Indiv(CC_BUSES Model_Bus)
        {
            BUSESLN _BusesLN = new BUSESLN();
            RPTA_GENERAL r = new RPTA_GENERAL();
            var result = "";
            String mensaje = "";
            Int32 tipo = 0;
            //Validar si Existe la placa
            var rpt_placa = _BusesLN.Verifica_Placa_Existente(Model_Bus.BS_PLACA, ref mensaje, ref tipo);

            if (rpt_placa.AUX == 0)
            {

                string usuario = Session["user_login"].ToString();
                Model_Bus.ID_ESTADO = 1;
                Model_Bus.USU_REG = usuario;
                Model_Bus.ESTADO_VEHICULO = "NORMAL";
                
                var data = _BusesLN.Insertar_Buses_Nuevos(Model_Bus, ref mensaje, ref tipo);
                result = JsonConvert.SerializeObject(data);
            }
            else
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = "Placa Existente";
                result = JsonConvert.SerializeObject(r);
            }
            return result;
        }

        public string anularBus(int idBus)
        {
            BUSESLN _BusesLN = new BUSESLN();
            String mensaje = "";
            Int32 tipo = 0;
            var anularBus = JsonConvert.SerializeObject(_BusesLN.anularBus(idBus, Session["user_login"].ToString(), ref mensaje, ref tipo));
            return anularBus;
        }

        public string Modificar_Bus(CC_BUSES Model_Bus)
        {
            BUSESLN _BusesLN = new BUSESLN();
            string usuario = Session["user_login"].ToString();

            Model_Bus.USU_MODIF = usuario;

            String mensaje = "";
            Int32 tipo = 0;
            var data = _BusesLN.Modificar_Bus(Model_Bus, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(data);
            return result;
        }

        public string listarBusesDesafectados()
        {
            BUSESLN _BusesLN = new BUSESLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _BusesLN.Listar_Buses_Desafectados(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }

        public string Registro_Bus_Afectado(CC_BUSES Model_Bus, int id_placa_reemplazada)
        {
            BUSESLN _BusesLN = new BUSESLN();
            String mensaje = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();
            Model_Bus.ID_ESTADO = 1;
            Model_Bus.USU_REG = usuario;
            Model_Bus.ESTADO_VEHICULO = "NORMAL";


            var data = _BusesLN.Insertar_Buses_Afectados(Model_Bus, ref mensaje, ref tipo);

            //PLACA NUEVA DEL BUS
            var placa_nueva = data.DES_ESTADO;
            var datas = _BusesLN.desafectarBus(id_placa_reemplazada, placa_nueva, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(datas);
            return result;
        }
    }

}