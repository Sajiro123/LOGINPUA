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
using Entidades;
using LOGINPUA.Util;
using System.Collections;
using LOGINPUA.Util.Seguridad;

namespace CORREDORES_COMPLEMENTARIOS.Controllers
{
    public class ProgramacionController : Controller
    {

        //ReadExcel readExcel = new ReadExcel();

        //private readonly ProgramacionLN _ProgramacionLN = new ProgramacionLN();
        //private readonly BUSESLN _BusesLN;
        //private readonly ConductoresLN _ConductoresLN;

        //private readonly CorredoresLN _CorredoresLN;
        //private readonly RutaLN _rutaLN;
        //private readonly TipoDiaLN _tipoDiaLN;
        //private readonly SalidaProgramadaLN _salidaProgramadaLN;
        //private readonly Generar_pogLN _Generar_pogLN;

        //Util utilidades = new Util();

        //public ProgramacionController()
        //{
        //    _ProgramacionLN = new ProgramacionLN();
        //    _CorredoresLN = new CorredoresLN();
        //    _rutaLN = new RutaLN();
        //    _tipoDiaLN = new TipoDiaLN();
        //    _salidaProgramadaLN = new SalidaProgramadaLN();
        //    _BusesLN = new BUSESLN();
        //    _ConductoresLN = new ConductoresLN();
        //    _Generar_pogLN = new Generar_pogLN();

        //}
        //RUTAS
        public ActionResult Subir_Rutas()
        {

            Util utilidades = new Util();

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
        public ActionResult exportarPDF(string fechasA, string fechasB, string promedioVelocidadesA_M, string promedioVelocidadesB_M, string promedioVelocidadesA_T, string promedioVelocidadesB_T, string sumaTiemposViajeA_M, string sumaTiemposViajeB_M, string sumaTiemposViajeA_T, string sumaTiemposViajeB_T, string CORREDOR)
        {
            RutaLN _rutaLN = new RutaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var direccion = "";
            CC_REPORTE_PICO_PLACA model = new CC_REPORTE_PICO_PLACA();

            //model.VEL_PROMEDIO_A_MAÑANA_IDA = VEL_PROMEDIO_A_MANANA_IDA;


            model.VEL_PROMEDIO_A_MAÑANA_IDA = promedioVelocidadesA_M;
            model.TIEMPO_PROM_A_1 = sumaTiemposViajeA_M;

            model.VEL_PROMEDIO_B_MAÑANA_IDA = promedioVelocidadesB_M;
            model.TIEMPO_PROM_B_2 = sumaTiemposViajeB_M;

            model.VEL_PROMEDIO_A_TARDE_IDA = promedioVelocidadesA_T;
            model.TIEMPO_PROM_A_3 = sumaTiemposViajeA_T;

            model.VEL_PROMEDIO_B_TARDE_IDA = promedioVelocidadesB_T;
            model.TIEMPO_PROM_B_4 = sumaTiemposViajeB_T;


            model.FECHA_INICIO = fechasA;
            model.FECHA_FIN = fechasB;
            model.CORREDOR = CORREDOR;


            string ruta_base = Server.MapPath("~");
            if (ruta_base.Substring(ruta_base.Length - 1) != "\\")
            {
                ruta_base = ruta_base + "\\";
            }

            Reportes_Comparativo model_rp = new Reportes_Comparativo();

            try
            {
                direccion = model_rp.Insertar_rp_comparativo_Salida(model, ruta_base);

            }
            catch (Exception e)
            {
                string mensaje2 = e.Message;
                var rutas_ = _rutaLN.getRutaCorredor(ref mensaje2, ref tipo);
                ViewBag.Error = "Abrir Archivo";
                return PartialView("_ValidacionPdf");
            }



            ViewBag.Message = "Abrir Archivo";
            ViewBag.archivo = "~/Download/" + direccion;

            var rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(ViewBag.archivo));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Reporte_PDF_COMPARATIVO_.pdf");

        }


        public ActionResult Reporte_Caracterizacion()
        {
            Util utilidades = new Util();
            CorredoresLN _CorredoresLN = new CorredoresLN();
            String mensaje = "";
            Int32 tipo = 0;
            //var listaVistas = Session["menu_modulo"] as DataTable;
            //var listacciones = Session["menu_acciones"] as List<CC_MENUPERFIL_ACCION>;
            //Session["menu_modulo"] = dt4;
            //var nombreActionCurrent = this.ControllerContext.RouteData.Values["action"].ToString();
            //bool auxValida = utilidades.validaVistaxPerfil(nombreActionCurrent, listaVistas);
            //var Lista_acciones = utilidades.validadAccionMenu(listacciones, nombreActionCurrent, this.ControllerContext.RouteData.Values["controller"].ToString());

            //ViewBag.currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            //ViewBag.currentRoute = this.ControllerContext.RouteData.Values["controller"].ToString();
            //if (auxValida)
            //{
            //    ViewBag.Accionesview = Lista_acciones;
            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
            return View();
            //}
            //else
            //{
            //    return RedirectToAction("Sistemas_", "Home");
            //}
        }

        public ActionResult SubirProgramacion()
        {
            Util utilidades = new Util();
            CorredoresLN _CorredoresLN = new CorredoresLN();
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
                ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);

                ViewBag.Accionesview = Lista_acciones;
                return View();
            }
            else
            {
                return RedirectToAction("Sistemas_", "Home");
            }
        }

        public ActionResult Reporte_Versus()
        {
            CorredoresLN _CorredoresLN = new CorredoresLN();
            String mensaje = "";
            Int32 tipo = 0;
            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
            return View();
        }

        public string getResumenViajesRuta(int id_ruta, string fecha)
        {
            SalidaProgramadaLN _salidaProgramadaLN = new SalidaProgramadaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var rpta = JsonConvert.SerializeObject(_salidaProgramadaLN.getResumenViajesRuta(id_ruta, fecha, ref mensaje, ref tipo));
            return rpta;
        }


        public string getIdMaestro_x_fecha(int id_ruta, string fecha)
        {
            ProgramacionLN _ProgramacionLN = new ProgramacionLN();

            String mensaje = "";
            Int32 tipo = 0;
            var List_rutas = _ProgramacionLN.getIdMaestro_x_fecha(id_ruta, fecha, ref mensaje, ref tipo);
            var resultado = JsonConvert.SerializeObject(List_rutas);
            return resultado;
        }


        public string cargarArchivo(string fecha, int TipoDia, int reemplazar, int ruta_ajax, int ruta_text, int semana)
        {
            ProgramacionLN _ProgramacionLN = new ProgramacionLN();
            fecha = ReducirEspaciado(fecha);
            var ob = Request.Files;
            var idruta = ruta_ajax;
            var archivoSubido = Request.Files[0];
            var nombreArchivo = Path.GetFileName(archivoSubido.FileName);
            var extensionArchivo = Path.GetExtension(archivoSubido.FileName);
            var arrFileName = nombreArchivo.Split('.');
            var nuevoNombreArchivoExcel = arrFileName[0] + "_POD_" + DateTime.Now.ToString("dd_MM_yyyy_h_mm_ss") + extensionArchivo;
            var pathArchivo = Server.MapPath("~/Adjuntos/Programacion/" + nuevoNombreArchivoExcel);
            string pathFinal = Path.Combine(pathArchivo);
            archivoSubido.SaveAs(pathFinal);
            //return verificarFormatoExcel(Server.MapPath("~/Download/programacion_demo/" + "POD TGA 301 HABIL 50 BUSES.xlsx"), Request);
            var result = _ProgramacionLN.verificarFormatoExcel(pathFinal, Request, fecha, TipoDia, reemplazar, idruta, ruta_text, semana);
            return JsonConvert.SerializeObject(result);
        }

        public string getRutaxCorredor(int idCorredor)
        {
            RutaLN _rutaLN = new RutaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var result = JsonConvert.SerializeObject(_rutaLN.obtenerRutasPorCorredor(idCorredor, ref mensaje, ref tipo));
            return result;
        }
        public string getTipoDias()
        {
            TipoDiaLN _tipoDiaLN = new TipoDiaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var result = JsonConvert.SerializeObject(_tipoDiaLN.obtenertipoDias(ref mensaje, ref tipo));
            return result;
        }

        public string AgregarCamposPOG(int idruta, string FnodeA, string TnodeA, string FnodeB, string TnodeB, string DistanciaA, string DistanciaB)
        {
            ProgramacionLN _ProgramacionLN = new ProgramacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();
            var result = JsonConvert.SerializeObject(_ProgramacionLN.AgregarCamposPOG(idruta, FnodeA, TnodeA, FnodeB, TnodeB, DistanciaA, DistanciaB, usuario, ref mensaje, ref tipo));
            return result;
        }


        public string getResumenProgramado(int idServicio)
        {
            SalidaProgramadaLN _salidaProgramadaLN = new SalidaProgramadaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var rpta = JsonConvert.SerializeObject(_salidaProgramadaLN.getResumenProgramado(idServicio, ref mensaje, ref tipo));
            return rpta;
        }

        public string getDataViajesProgramacion(int idMaestroSalidaProg)
        {
            SalidaProgramadaLN _salidaProgramadaLN = new SalidaProgramadaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var rpta = JsonConvert.SerializeObject(_salidaProgramadaLN.getDataViajesProgramacion(idMaestroSalidaProg, ref mensaje, ref tipo));
            return rpta;
        }

        public RPTA_GENERAL verificarRegistroMaestro(string fechaConsulta, int idServicio)
        {
            SalidaProgramadaLN _salidaProgramadaLN = new SalidaProgramadaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var data = new RPTA_GENERAL();
            RPTA_GENERAL r = new RPTA_GENERAL();
            string fechausada = "";

            string fechaFinal = fechaConsulta.Replace(" ", "");

            string[] array = new string[4];

            array = fechaFinal.Split('-');

            foreach (var item in array)
            {
                data = _salidaProgramadaLN.verifica_maestro_prog(idServicio, item, ref mensaje, ref tipo);
                if (data.COD_ESTADO == 1)
                {
                    fechausada = item;
                    break;
                }
            }

            if (data.AUX > 0)
            {
                r.COD_ESTADO = 1;
                r.DES_ESTADO = "Ya existe información para la fecha " + fechausada + ", ¿Desea reemplazarlo?";
                r.AUX = data.AUX;

            }
            else
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = "No hay info en esta fecha";
            }
            return r;
        }

        public string getTipoServicioByCorredor(int idCorredor)
        {
            SalidaProgramadaLN _salidaProgramadaLN = new SalidaProgramadaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var rpta = JsonConvert.SerializeObject(_salidaProgramadaLN.getTipoServicioByCorredor(idCorredor, ref mensaje, ref tipo));
            return rpta;
        }
        public static string ReducirEspaciado(string Cadena)
        {
            while (Cadena.Contains(" "))
            {
                Cadena = Cadena.Replace(" ", "");
            }

            return Cadena;
        }



        //PROGRAMACION MOBIL 

        public ActionResult ProgracionDetallado()
        {

            var session_Perfil = int.Parse(Session["perfil_corredor"].ToString());
            ViewBag.id_perfilcc = session_Perfil;

            RutaLN _rutaLN = new RutaLN();
            String mensaje = "";
            Int32 tipo = 0;
            ViewBag.name_title = "Viajes Despacho";
            var List_rutas = _rutaLN.getRuta_X_modalidad(1, ref mensaje, ref tipo);
            List_rutas = List_rutas.OrderBy(o => o.NRO_RUTA).ToList();
            ViewBag.Rutas = List_rutas;
            return View();
        }


        public string RegistrarNuevoViaje(int id_maestro_salidaProgramada, string tipoDia, string nroServicio, string pog, string pot, string fnode, string hsalida, string hllegada, string tnode, string PIG, double minutosDiferenciaLayover, string acumulado, string sentido, string turno, int idtiposerv, string trip_time, string distancia, string placa, string cacConductor, string fecha)
        {

            SalidaProgramadaLN _salidaProgramadaLN = new SalidaProgramadaLN();
            ProgramacionLN _ProgramacionLN = new ProgramacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            string usureg_real = Session["user_login"].ToString();

            var registraDetalle = _salidaProgramadaLN.guardarMSalidaProgramadaDet(id_maestro_salidaProgramada, tipoDia, nroServicio, pog, pot, fnode, hsalida, hllegada, tnode, PIG, minutosDiferenciaLayover, acumulado, sentido, turno, idtiposerv, trip_time, distancia, placa, cacConductor, usureg_real, ref mensaje, ref tipo);

            var Maestro_Inicial = _salidaProgramadaLN.getId_ProgViaje(id_maestro_salidaProgramada, fecha, ref mensaje, ref tipo);

            var RPTA = _salidaProgramadaLN.guardarMViajesProgramadaDet(Maestro_Inicial.AUX, registraDetalle.AUX, usureg_real, ref mensaje, ref tipo);

            var resultado = JsonConvert.SerializeObject(RPTA);
            return resultado;
        }


        public string ListServicioMaestroProg(int id_maestro_salidaProgramada)
        {

            SalidaProgramadaLN _salidaProgramadaLN = new SalidaProgramadaLN();
            ProgramacionLN _ProgramacionLN = new ProgramacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            string usureg_real = Session["user_login"].ToString();
            var registraDetalle = _salidaProgramadaLN.ListServicioMaestroProg(id_maestro_salidaProgramada, ref mensaje, ref tipo);
            var resultado = JsonConvert.SerializeObject(registraDetalle);
            return resultado;
        }

        public string getRutaSerOpe_X_modalidad(int id_ruta)
        {
            RutaLN _rutaLN = new RutaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var List_rutas = _rutaLN.getRutaSerOpe_X_modalidad(id_ruta, ref mensaje, ref tipo);
            var resultado = JsonConvert.SerializeObject(List_rutas);
            return resultado;
        }

        public string getViajes_ProgrUnidades(int id_ruta, string sentido)
        {
            ProgramacionLN _ProgramacionLN = new ProgramacionLN();

            String mensaje = "";
            Int32 tipo = 0;
            DateTime dateTime = DateTime.Now;
            string fecha = dateTime.ToString("dd/MM/yyyy");
            //string fecha = "30/08/2020";

            var List_rutas = _ProgramacionLN.getViajes_ProgrUnidades(fecha, id_ruta, sentido, ref mensaje, ref tipo);
            var resultado = JsonConvert.SerializeObject(List_rutas);
            return resultado;
        }

        public string getviajesProgrServ(int id_ruta, string sentido, int idservicio)
        {
            ProgramacionLN _ProgramacionLN = new ProgramacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            DateTime dateTime = DateTime.Now;
            string fecha = dateTime.ToString("dd/MM/yyyy");
            //string fecha = "01/04/2020";

            var List_rutas = _ProgramacionLN.getviajesProgrServ(fecha, id_ruta, sentido, idservicio, ref mensaje, ref tipo);
            var resultado = JsonConvert.SerializeObject(List_rutas);
            return resultado;
        }

        public string Actulizar_ViajeTiempoReal(int idsalida, string unidad, int cac_temporal, string hora_salida, string observacion)
        {
            ProgramacionLN _ProgramacionLN = new ProgramacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            string usureg_real = Session["user_login"].ToString();
            var List_rutas = _ProgramacionLN.Actulizar_ViajeTiempoReal(idsalida, unidad, cac_temporal, hora_salida, observacion, usureg_real, ref mensaje, ref tipo);
            var resultado = JsonConvert.SerializeObject(List_rutas);
            return resultado;
        }

        public string Actualizar_Incidencias(int id_maestro, string observacion, string horaejecutada)
        {
            ProgramacionLN _ProgramacionLN = new ProgramacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            string usureg_real = Session["user_login"].ToString();
            var List_rutas = _ProgramacionLN.Actualizar_Incidencias(id_maestro, observacion, horaejecutada, usureg_real, ref mensaje, ref tipo);
            var resultado = JsonConvert.SerializeObject(List_rutas);
            return resultado;
        }

        public string Limpiar_ViajeDet(int id_maestro)
        {
            ProgramacionLN _ProgramacionLN = new ProgramacionLN();
            String mensaje = "";
            Int32 tipo = 0;

            var List_rutas = _ProgramacionLN.Limpiar_ViajeDet(id_maestro, ref mensaje, ref tipo);
            var resultado = JsonConvert.SerializeObject(List_rutas);
            return resultado;
        }




        public string getLado_X_Ruta(int id_ruta)
        {
            RutaLN _rutaLN = new RutaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var List_rutas = _rutaLN.getLado_X_Ruta(id_ruta, ref mensaje, ref tipo);
            var resultado = JsonConvert.SerializeObject(List_rutas);
            return resultado;
        }

        public string getPlaca_Buses()
        {
            BUSESLN _BusesLN = new BUSESLN();
            String mensaje = "";
            Int32 tipo = 0;
            var ListPlacas = _BusesLN.Listar_Buses(ref mensaje, ref tipo);
            var resultado = JsonConvert.SerializeObject(ListPlacas);
            return resultado;
        }

        public string getConductores()
        {
            ConductoresLN _ConductoresLN = new ConductoresLN();
            String mensaje = "";
            Int32 tipo = 0;
            var ListConductores = _ConductoresLN.Listar_Conductores(ref mensaje, ref tipo);
            var resultado = JsonConvert.SerializeObject(ListConductores);
            return resultado;
        }

        // FINALIZAR MOBIL


        // INICIAR POD

        public ActionResult ProgrDetPod()
        {
            Util utilidades = new Util();
            ViewBag.ACTION_REEMPLAZAR_DATOS = false;
            var listaVistas = Session["menu_modulo"] as DataTable;
            var listacciones = Session["menu_acciones"] as List<CC_MENUPERFIL_ACCION>;
            //Session["menu_modulo"] = dt4;
            var nombreActionCurrent = this.ControllerContext.RouteData.Values["action"].ToString();
            bool auxValida = utilidades.validaVistaxPerfil(nombreActionCurrent, listaVistas);
            var Lista_acciones = utilidades.validadAccionMenu(listacciones, nombreActionCurrent, this.ControllerContext.RouteData.Values["controller"].ToString());

            ViewBag.currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.currentRoute = this.ControllerContext.RouteData.Values["controller"].ToString();
            //if (auxValida)
            //{
            //    ViewBag.Accionesview = Lista_acciones;
            return View();
            //}
            //else
            //{
            //    return RedirectToAction("Sistemas_", "Home");
            //}
        }
        public string CargarArchivoZip(string fecha, int reemplazar)
        {
            ProgramacionLN ProgramacionDespachoLN = new ProgramacionLN();
            var Rpta = new RPTA_GENERAL();
            ViewBag.ACTION_REEMPLAZAR_DATOS = true;
            String mensaje = "";
            Int32 tipo = 0;
            var nuevoNombreArchivoExcel = "";
            var rpt_result = ProgramacionDespachoLN.ValidarFecha_Programados(fecha, ref mensaje, ref tipo);

            if (rpt_result.AUX > 0)
            {
                var id = rpt_result.AUX;
                if (reemplazar == 0)
                {
                    Rpta.AUX = rpt_result.AUX;
                    Rpta.COD_ESTADO = 3;
                    Rpta.DES_ESTADO = "¿Se reemplazara el archivo de la fecha " + rpt_result.DES_ESTADO + " ?";
                    return JsonConvert.SerializeObject(Rpta);
                }
                else
                {
                    var nombre = ProgramacionDespachoLN.nombre_zip(id, ref mensaje, ref tipo);
                    System.IO.File.Delete(Server.MapPath("~/Adjuntos/ProgramacionDetPod/" + nombre.DES_ESTADO));
                    ProgramacionDespachoLN.eliminar_Archivo(id, ref mensaje, ref tipo);

                    var ob = Request.Files;
                    var archivoSubido = Request.Files[0];
                    var nombreArchivo = Path.GetFileName(archivoSubido.FileName);
                    var extensionArchivo = Path.GetExtension(archivoSubido.FileName);
                    var arrFileName = nombreArchivo.Split('.');
                    nuevoNombreArchivoExcel = arrFileName[0] + extensionArchivo;
                    var pathArchivo = Server.MapPath("~/Adjuntos/ProgramacionDetPod/" + nuevoNombreArchivoExcel);
                    string pathFinal = Path.Combine(pathArchivo);
                    archivoSubido.SaveAs(pathFinal);

                    Rpta = ProgramacionDespachoLN.RegistroInforme(nuevoNombreArchivoExcel, fecha, ref mensaje, ref tipo);
                    return JsonConvert.SerializeObject(Rpta);
                }
            }
            else
            {
                var ob = Request.Files;
                var archivoSubido = Request.Files[0];
                var nombreArchivo = Path.GetFileName(archivoSubido.FileName);
                var extensionArchivo = Path.GetExtension(archivoSubido.FileName);
                var arrFileName = nombreArchivo.Split('.');
                nuevoNombreArchivoExcel = arrFileName[0] + extensionArchivo;
                var pathArchivo = Server.MapPath("~/Adjuntos/ProgramacionDetPod/" + nuevoNombreArchivoExcel);
                string pathFinal = Path.Combine(pathArchivo);
                archivoSubido.SaveAs(pathFinal);
                Rpta = ProgramacionDespachoLN.RegistroInforme(nuevoNombreArchivoExcel, fecha, ref mensaje, ref tipo);

            }
            var adjunto = Server.MapPath("~/Adjuntos/ProgramacionDetPod/" + nuevoNombreArchivoExcel);

            //Lista de correos para ya definida por el usuario
            var Listacorreospara = new List<string>();
            //correo prueba
            Listacorreospara.Add("willy.espinoza@atu.gob.pe");

            //Listacorreospara.Add("mvizcarra@atu.gob.pe");

            //Lista de correos con copia ya definida por el usuario
            var Listacopiapara = new List<string>();
            //correo prueba
            Listacopiapara.Add("pierre.rodriguez@atu.gob.pe");

            //Listacopiapara.Add("jose.hermitano@atu.gob.pe");
            //Listacopiapara.Add("do10@atu.gob.pe");
            //Listacopiapara.Add("do30@atu.gob.pe");
            //Listacopiapara.Add("do25@atu.gob.pe");
            //Listacopiapara.Add("sstr57@atu.gob.pe");
            //Listacopiapara.Add("sstr44@atu.gob.pe");
            //Listacopiapara.Add("sstr81@atu.gob.pe");
            //Listacopiapara.Add("sstr83@atu.gob.pe");
            //Listacopiapara.Add("raul.vasquez@atu.gob.pe");
            //Listacopiapara.Add("supervmttosit@atu.gob.pe");

            //Asunto predefinido
            var asunto = "Placas Programadas Corredores Complementarios " + nuevoNombreArchivoExcel.Substring(0, 2) + "/" + nuevoNombreArchivoExcel.Substring(2, 2) + "/" + nuevoNombreArchivoExcel.Substring(4, 4);

            //Cuerpo del mensaje
            var cuerpo = System.IO.File.ReadAllText(Server.MapPath("~/Util/CuerpoCorreo.txt"));

            //Parametros del cuerpo
            var parametro_cuerpo = new List<String>();

            //Saludo dependiendo de la hora, parametro 0
            var hora_del_dia_saludo = DateTime.Now;
            if (hora_del_dia_saludo.Hour >= 5 && hora_del_dia_saludo.Hour < 12)
            {
                parametro_cuerpo.Add("Buenos días");
            }
            else if (hora_del_dia_saludo.Hour >= 12 && hora_del_dia_saludo.Hour < 18)
            {
                parametro_cuerpo.Add("Buenas tardes");
            }
            else
            {
                parametro_cuerpo.Add("Buenas noches");
            }
            var dia_sig = hora_del_dia_saludo.AddDays(1);
            var dia_sig_prueba = dia_sig.ToString("dd/mm/yyyy");
            //fecha del dia siguiente para el parametro 1
            parametro_cuerpo.Add(dia_sig_prueba);
            //correo y nombres del usuario, parametro 3 y 4
            var usuario = Session["user_datos"] as List<ENTIDADES.CC_PERSONA>;
            var correo = "";
            var nombre_usu = "";
            foreach (var item in usuario)
            {
                correo = item.CORREO;
                nombre_usu = item.NOMBRES + " " + item.APEPAT + " " + item.APEMAT;
            }
            parametro_cuerpo.Add(nombre_usu);
            parametro_cuerpo.Add(correo);

            String user_pass = Encriptador.Desencriptar(Session["user_password"].ToString());

            LOGINPUA.Util.Servicio.EnvioCorreo(Listacorreospara, Listacopiapara, correo, user_pass, asunto, parametro_cuerpo, cuerpo, adjunto, ref tipo, ref mensaje);
            return JsonConvert.SerializeObject(Rpta);
        }

        public string Listar_Informes(string mes_año)
        {
            ProgramacionLN ProgramacionDespachoLN = new ProgramacionLN();
            CorredoresLN _CorredoresLN = new CorredoresLN();
            RPTA_GENERAL e = new RPTA_GENERAL();

            String mensaje = "";
            Int32 tipo = 0;
            int mes = 0;
            int año = 0;

            string[] array_a;
            if (mes_año == "")
            {
                e.COD_ESTADO = 0;
                return JsonConvert.SerializeObject(e);

            }
            else
            {
                array_a = mes_año.Split('/');
                mes = Convert.ToInt32(array_a[0]);
                año = Convert.ToInt32(array_a[1]);
            }


            var Lista_informes = ProgramacionDespachoLN.Listar_Informes(mes, año, ref mensaje, ref tipo);
            var rpta = JsonConvert.SerializeObject(Lista_informes);
            return rpta;
        }

        public JsonResult EnviarCorreo(string para, string copia, string asunto, string nombreadjunto, string cuerpo)
        {

            var usuario = Session["user_datos"] as List<ENTIDADES.CC_PERSONA>;
            var correo = "";
            var nombre = "";
            foreach (var item in usuario)
            {
                correo = item.CORREO;
                nombre = item.NOMBRES + " " + item.APEPAT + " " + item.APEMAT;
            }

            var adjunto = Server.MapPath("~/Adjuntos/ProgramacionDetPod/" + nombreadjunto);
            Int32 tipo = 0;
            String mensaje = "";

            //Se divide mediante la coma los diferentes correos, se limpia espacios en blanco y se valida que no haya vacio entre comas
            var correos_para = para.Split(',').Select(x => x.Trim()).Where(x => x != "").ToArray();
            var copia_para = copia.Split(',').Select(x => x.Trim()).Where(x => x != "").ToArray();

            //se agrega los correos en una lista
            var Listacorreospara = new List<string>();
            for (int i = 0; i < correos_para.Length; i++)
            {
                Listacorreospara.Add(correos_para[i]);
            }
            //se agrega las copias ocultas en una lista
            var Listacopiapara = new List<string>();
            for (int i = 0; i < copia_para.Length; i++)
            {
                Listacopiapara.Add(copia_para[i]);
            }

            //se obtiene la clave del usuario
            String user_pass = Encriptador.Desencriptar(Session["user_password"].ToString());

            //se hace uso del servicio para enviar correo
            LOGINPUA.Util.Servicio.EnvioCorreo(Listacorreospara, Listacopiapara, correo, user_pass, asunto, new List<string> { }, cuerpo, adjunto, ref tipo, ref mensaje);


            return Json(new { tipo = tipo, mensaje = mensaje });
        }
        //public string Get_IdMaestro_Pog(int id_ruta ,string fecha)
        //{
        //    String mensaje = "";
        //    Int32 tipo = 0;
        //    var lista = _Generar_pogLN.Get_IdMaestro_Pog(id_ruta,fecha, ref mensaje, ref tipo);
        //    var result = JsonConvert.SerializeObject(lista); //para la lista principal
        //    return result;
        //}


    }
}