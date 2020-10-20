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
    public class ParaderoController : Controller
    {
         
        public ActionResult Inicio()
        {
            Util.Util utilidades = new Util.Util();
            CorredoresLN _CorredoresLN = new CorredoresLN();
            RutaLN _rutaLN = new RutaLN();
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
                var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
                ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
                ViewBag.modalidadUsuario = 2; //[1 : CORREDOR ] -- [2: COSAC] esto viene del usuario
                ViewBag.rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);
                ViewBag.modalidadTransporte = _modalidadTransporteLN.getModalidadTransporte(ref mensaje, ref tipo);
                return View();
            }
            else
            {
                return RedirectToAction("Sistemas_", "Home");
            }
        }

        public string getRecorridoByRuta(int idRuta)
        {
            RecorridoLN _recorridoLN = new RecorridoLN();
            String mensaje = "";
            Int32 tipo = 0;
            var dataRecorrido = JsonConvert.SerializeObject(_recorridoLN.getRecorridosByRuta(idRuta, ref mensaje, ref tipo));
            return dataRecorrido;
        }

        public string registrarParadero(int idRecorrido, int idTipoParadero, int idVia, string nombre, string nombreEtiqueta,
                                        double distanciaParcial, double latitud, double longitud, int numero_orden)
        {
            ParaderoLN _paraderoLN = new ParaderoLN();
            String mensaje = "";
            Int32 tipo = 0;
            var registroParadero = JsonConvert.SerializeObject(_paraderoLN.registrarParadero(idRecorrido, idTipoParadero, idVia, nombre, nombreEtiqueta,
                                        distanciaParcial, latitud, longitud, numero_orden, Session["user_login"].ToString(),ref mensaje, ref tipo));
            return registroParadero;
        }

        public string anularParadero(int idParadero)
        {
            ParaderoLN _paraderoLN = new ParaderoLN();
            String mensaje = "";
            Int32 tipo = 0;
            var anulaParadero = JsonConvert.SerializeObject(_paraderoLN.anularParadero(idParadero, Session["user_login"].ToString(), ref mensaje, ref tipo));
            return anulaParadero;
        }

        public string modificarParadero(int idParadero, string nombre, string nombreEtiqueta,
                                                int idTipoParadero, double distanciaParcial,
                                                int numero_orden,
                                                double latitud, double longitud, int idVia)
        {
            ParaderoLN _paraderoLN = new ParaderoLN();
            String mensaje = "";
            Int32 tipo = 0;
            var modificaParadero = JsonConvert.SerializeObject(_paraderoLN.modificarParadero(idParadero, nombre, nombreEtiqueta,
                                                                                            idTipoParadero, distanciaParcial,
                                                                                            latitud, longitud, idVia, numero_orden,
                                                                                            Session["user_login"].ToString(), ref mensaje, ref tipo));
            return modificaParadero;
        }

        public int verificarCodRecorridoBylado(Dictionary<int, string> recorridos, string lado)
        {
            var idRecorrido = 0;
            foreach (var item in recorridos)
            {
                if (lado == item.Value)
                {
                    idRecorrido = item.Key;
                }
            }

            return idRecorrido;
        }
        public string importarDataParaderos(string strRecorridos)
        {
            
            ParaderoLN _paraderoLN = new ParaderoLN();
            String mensaje = "";
            Int32 tipo = 0;
            RPTA_GENERAL e = new RPTA_GENERAL();
            var arrRecorridos = strRecorridos.Split('|');
            var recorridos = new Dictionary<int, string>(); //columna, nombre

            for (int i = 0; i < arrRecorridos.Count(); i++)
            {
                var item = arrRecorridos[i].Split('~');
                var lado = item[0].ToString();
                var idRecorrido = int.Parse(item[1].ToString());
                recorridos.Add(idRecorrido, lado);
            }
            //
            var ob = Request.Files;
            var archivoSubido = Request.Files[0];
            var nombreArchivo = Path.GetFileName(archivoSubido.FileName);
            var extensionArchivo = Path.GetExtension(archivoSubido.FileName);
            var arrFileName = nombreArchivo.Split('.');
            var nuevoNombreArchivoExcel = arrFileName[0] + "_FILE_" + DateTime.Now.ToString("dd_MM_yyyy_h_mm_ss") + extensionArchivo;
            var pathArchivo = Server.MapPath("~/Adjuntos/Files_Despacho/" + nuevoNombreArchivoExcel);
            string pathFinal = Path.Combine(pathArchivo);
            archivoSubido.SaveAs(pathFinal);

            try
            {
                using (FileStream fs = new FileStream(pathFinal, FileMode.Open))
                {
                    SLDocument xlDoc = new SLDocument(fs);
                    var hojasExcel = xlDoc.GetSheetNames();
                    SLDocument hojaParaderos = new SLDocument(fs, hojasExcel[0]);//leyendo la hoja 
                    SLWorksheetStatistics hojaDespachoStadistics = xlDoc.GetWorksheetStatistics();
                    string fechaTemporal = DateTime.Now.ToString("dd/MM/yyyy");
                    //verificando formato archivo excel
                    var textoColumnaLado = hojaParaderos.GetCellValueAsString(6, 1).ToString(); //texto 
                    var textoColumnaEtiqueta = hojaParaderos.GetCellValueAsString(6, 4).ToString(); //texto Estado viaje
                    var cantidadRegistrosExitosos = 0;
                    var cantidadNoRegistrados = 0;
                    if (textoColumnaLado == "LADO" && textoColumnaEtiqueta == "ETIQUETA NOMBRE")
                    {
                        for (int row = 7; row < hojaDespachoStadistics.EndRowIndex; row++)
                        {

                            var lado = hojaParaderos.GetCellValueAsString(row, 1).ToString();
                            var nro_orden = hojaParaderos.GetCellValueAsString(row, 2).ToString();
                            var nombre = hojaParaderos.GetCellValueAsString(row, 3).ToString();
                            var etiqueta_nombre = hojaParaderos.GetCellValueAsString(row, 4).ToString();
                            var codTipo = (hojaParaderos.GetCellValueAsString(row, 5).ToString() == "" ? 0 : Int32.Parse(hojaParaderos.GetCellValueAsString(row, 5).ToString()));
                            var distanciaParcial = (hojaParaderos.GetCellValueAsString(row, 6).ToString() == "" ? 0 : Double.Parse(hojaParaderos.GetCellValueAsString(row, 6).ToString()));

                            var latitud = (hojaParaderos.GetCellValueAsString(row, 7).ToString() == "" ? 0 : Double.Parse(hojaParaderos.GetCellValueAsString(row, 7).ToString()));
                            var longitud = (hojaParaderos.GetCellValueAsString(row, 8).ToString() == "" ? 0 : Double.Parse(hojaParaderos.GetCellValueAsString(row, 8).ToString()));
                            var codVia = (hojaParaderos.GetCellValueAsString(row, 9).ToString() == "" ? 0 : Int32.Parse(hojaParaderos.GetCellValueAsString(row, 9).ToString()));

                            var idRecorridoRegistra = verificarCodRecorridoBylado(recorridos, lado);
                            if (nombre != "" && codTipo != 0 && codVia != 0 && nro_orden != "")
                            {
                                var registroParadero = _paraderoLN.registrarParadero(idRecorridoRegistra, codTipo, codVia, nombre, etiqueta_nombre, distanciaParcial, latitud, longitud, Int32.Parse(nro_orden), Session["user_login"].ToString(), ref mensaje, ref tipo);
                                //
                                if (registroParadero.COD_ESTADO == 1)
                                {
                                    cantidadRegistrosExitosos++;
                                }
                                else
                                {
                                    cantidadNoRegistrados++;
                                }
                            }
                        }
                        e.COD_ESTADO = 1;
                        e.DES_ESTADO = cantidadRegistrosExitosos + " paraderos registrados correctamente, " + (cantidadNoRegistrados > 0 ? " y " + cantidadNoRegistrados + " FALLIDOS." : "");
                    }
                    else
                    {
                        e.COD_ESTADO = 0;
                        e.DES_ESTADO = "Verificar el formato del archivo.";
                    }
                }
            }
            catch (Exception ex)
            {
                e.COD_ESTADO = 0;
                e.DES_ESTADO = "Error-> " + ex.Message;
            }

            return JsonConvert.SerializeObject(e);
        }

        public string getVias()
        {
            ViaLN _viaLN = new ViaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var dataVias = JsonConvert.SerializeObject(_viaLN.getVias(ref mensaje, ref tipo));
            return dataVias;
        }

        public string getListaParaderos(string strCodRecorrido)
        {
            ParaderoLN _paraderoLN = new ParaderoLN();
            String mensaje = "";
            Int32 tipo = 0;
            var dataVias = JsonConvert.SerializeObject(_paraderoLN.getParaderosByStrRecorrido(strCodRecorrido, ref mensaje, ref tipo));
            return dataVias;
        }
    }
}