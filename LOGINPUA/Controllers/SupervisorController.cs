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


namespace LOGINPUA.Controllers
{
    public class SupervisorController : Controller
    {
        //Util.Util utilidades = new Util.Util();
        //private readonly CorredoresLN _CorredoresLN;
        //private readonly BUSESLN _BusesLN;
        //private readonly SupervisorLN _SupervisorLN;


        //CC_CONDUCTORES model = new CC_CONDUCTORES();
        //BusesModel buse_model = new BusesModel();

        //public SupervisorController()
        //{
        //    _BusesLN = new BUSESLN();
        //    _SupervisorLN = new SupervisorLN();
        //    _CorredoresLN = new CorredoresLN();

        //}
        public ActionResult Index()
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



        public ActionResult Fuera_Campo()
        {
            BUSESLN _BusesLN = new BUSESLN();
            String mensaje = "";
            Int32 tipo = 0;
            List<CC_CONDUCTORES> lista = _BusesLN.Listar_Placa(ref mensaje, ref tipo);
            ViewBag.lista = new SelectList(lista, "PLACA", "PLACA");

            DateTime fecha_Actual = DateTime.Now;
            string fecha_nombre = fecha_Actual.ToString("dd/MM/yyyy");
            string hora_actual = fecha_Actual.ToString("HH:mm:ss");

            ViewBag.fecha_actual = fecha_nombre;
            ViewBag.Hora_actual = hora_actual;
            ViewBag.accion = "REGISTRAR";


            return View();
        }

        public ActionResult Estado_Bus()
        {
            return View();
        }


        public ActionResult Estado_Bus_consulta(string placa, string direccion, int? km, string latitud, string longitud, string accion, string id_maestro)
        {
            BUSESLN _BusesLN = new BUSESLN();
            SupervisorLN _SupervisorLN = new SupervisorLN();

            String mensaje = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();

            if (usuario == null)
            {
                return View("../Home/Sistemas");
            }

            if (placa == "")
            {
                List<CC_CONDUCTORES> lista = _BusesLN.Listar_Placa(ref mensaje, ref tipo);
                ViewBag.lista = new SelectList(lista, "PLACA", "PLACA");
                ViewBag.Message = "Ingrese Placa";
                ViewBag.fecha_actual = DateTime.Now.ToString("dd/MM/yyyy");
                ViewBag.Hora_actual = DateTime.Now.ToString("HH:mm:ss"); ;
                return View("Fuera_Campo");
            }

            if (latitud == "" && longitud == "")
            {
                List<CC_CONDUCTORES> lista = _BusesLN.Listar_Placa(ref mensaje, ref tipo);
                ViewBag.lista = new SelectList(lista, "PLACA", "PLACA");
                ViewBag.GPS = "Debe Activar GPS";
                ViewBag.fecha_actual = DateTime.Now.ToString("dd/MM/yyyy");
                ViewBag.Hora_actual = DateTime.Now.ToString("HH:mm:ss"); ;
                return View("Fuera_Campo");
            }



            var vigencias_autos = new CC_BUSES();
            vigencias_autos = _BusesLN.Estado_Vigencias(placa, ref mensaje, ref tipo);

            string[] vigencias = new string[] { vigencias_autos.BS_CVS_FEC_FIN,
                vigencias_autos.COD_CAC,
                vigencias_autos.BS_RTV_FIN,
                vigencias_autos.BS_SOAT_FEC_FIN,
                vigencias_autos.BS_RC_FIN,
                vigencias_autos.BS_VEHICULOS_FIN,
                vigencias_autos.ID_CORREDOR.ToString()};
            ViewBag.vigencias = vigencias;



            //LISTAR 
            var Lista_Dentro_vehiculo = _SupervisorLN.Lista_Conceptos_Dentro(id_maestro, accion, ref mensaje, ref tipo);
            var Lista_Exterior_Vehiculo = _SupervisorLN.Lista_Conceptos_Exterior(id_maestro, accion, ref mensaje, ref tipo);
            var Lista_Cabina_Vehiculo = _SupervisorLN.Lista_Cabina_Vehiculo(id_maestro, accion, ref mensaje, ref tipo);
            var Lista_Revisiones_Mecanicas = _SupervisorLN.Lista_Revisiones_Mecanicas(id_maestro, accion, ref mensaje, ref tipo);


            BusesModel modelo_buses = new BusesModel();
            modelo_buses.Lista_Dentro_Vehiculo = Lista_Dentro_vehiculo;
            modelo_buses.Lista_Exterior_Vehiculo = Lista_Exterior_Vehiculo;
            modelo_buses.Lista_Cabina_Vehiculo = Lista_Cabina_Vehiculo;
            modelo_buses.Lista_Revisiones_Mecanicas = Lista_Revisiones_Mecanicas;
            modelo_buses.CC_BUSES = vigencias_autos;


            ViewBag.direccion = direccion;
            ViewBag.km = km;
            ViewBag.placa = vigencias_autos.BS_PLACA;
            ViewBag.concesionario = vigencias_autos.BS_NOM_EMPRE;
            ViewBag.latitud = latitud;
            ViewBag.longitud = longitud;
            ViewBag.Estado = "PROGRAMABLE";
            ViewBag.Corredor = vigencias_autos.ID_CORREDOR;
            ViewBag.accion = accion;
            ViewBag.id_maestro = id_maestro;


            return View("Estado_Bus", modelo_buses);
        }


        [HttpPost]
        public ActionResult Insertar_Buses_Despacho_FINAL(int[] Conceptos, string[] estado, string[] calidad, string[] observacion, string placa, string Concesionario, int? km, string direccion, string longitud, string latitud, HttpPostedFileBase Foto_Cargar1, HttpPostedFileBase Foto_Cargar2, HttpPostedFileBase Foto_Cargar3, HttpPostedFileBase Foto_Cargar4, string mensaje, string comentario, int corredor, string accion, string id_maestro)
        {
            SupervisorLN _SupervisorLN = new SupervisorLN();
            BUSESLN _BusesLN = new BUSESLN();
            CorredoresLN _CorredoresLN = new CorredoresLN();


            String mensaje2 = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();
            if (usuario == null)
            {
                return View("../Home/Sistemas");
            }

            Random r = new Random(DateTime.Now.Millisecond);

            int codigo_archivo = 0;
            string vista = "";

            //ACTUALIZAR DE BAJA ID_MAESTRO ANTIGUO
            /* IMAGEN */

            string ficheroNombre1 = "";
            string ficheroNombre2 = "";
            string ficheroNombre3 = "";
            string ficheroNombre4 = "";


            string urlArchivos = "~/Util/img_despacho";


            if (Foto_Cargar1 != null)
            {
                codigo_archivo = r.Next();
                ficheroNombre1 = "000" + codigo_archivo + "-1" + placa + "." + Path.GetFileName(Foto_Cargar1.ContentType);
                string r1 = Path.Combine(Server.MapPath(urlArchivos), ficheroNombre1);
                Foto_Cargar1.SaveAs(r1);
            }
            if (Foto_Cargar2 != null)
            {
                codigo_archivo = r.Next();
                ficheroNombre2 = "000" + codigo_archivo + "-2" + placa + "." + Path.GetFileName(Foto_Cargar2.ContentType);
                string r2 = Path.Combine(Server.MapPath(urlArchivos), ficheroNombre2);
                Foto_Cargar2.SaveAs(r2);
            }
            if (Foto_Cargar3 != null)
            {
                codigo_archivo = r.Next();
                ficheroNombre3 = "000" + codigo_archivo + "-3" + placa + "." + Path.GetFileName(Foto_Cargar3.ContentType);
                string r3 = Path.Combine(Server.MapPath(urlArchivos), ficheroNombre3);
                Foto_Cargar3.SaveAs(r3);
            }
            if (Foto_Cargar4 != null)
            {
                codigo_archivo = r.Next();
                ficheroNombre4 = "000" + codigo_archivo + "-4" + placa + "." + Path.GetFileName(Foto_Cargar4.ContentType);
                string r4 = Path.Combine(Server.MapPath(urlArchivos), ficheroNombre4);
                Foto_Cargar4.SaveAs(r4);
            }

            //INSERTAR EL FORMATO COMPLETO
            var rpta = _SupervisorLN.Insertar_Formato_campo(usuario, Conceptos, estado, calidad, observacion, placa, Concesionario, km, direccion, longitud, latitud, ficheroNombre1, ficheroNombre2, ficheroNombre3, ficheroNombre4, mensaje2, comentario, corredor, accion, id_maestro, ref mensaje, ref tipo);

            if (rpta.COD_ESTADO == 1)
            {
                ViewBag.Message = "Registrado Correctamente";

            }


            List<CC_CONDUCTORES> lista = _BusesLN.Listar_Placa(ref mensaje, ref tipo);
            ViewBag.lista = new SelectList(lista, "PLACA", "PLACA");

            DateTime fecha_Actual = DateTime.Now;
            string fecha_nombre = fecha_Actual.ToString("dd/MM/yyyy");
            string hora_actual = fecha_Actual.ToString("HH:mm:ss");

            ViewBag.fecha_actual = fecha_nombre;
            ViewBag.Hora_actual = hora_actual;

            if (accion == "EDITAR")
            {
                vista = "Reportes_Despacho";
                var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
                ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
                ViewBag.Message = "Se ha actualizado Correctamente";


            }
            else
            {
                ViewBag.Message = "Se ha registrado Correctamente";
                vista = "Fuera_Campo";
            }

            return View(vista);

        }

        public ActionResult Reportes_Despacho()
        {
            CorredoresLN _CorredoresLN = new CorredoresLN();
 
            String mensaje = "";
            Int32 tipo = 0;
            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);

            return View();

        }
        public string Listar_Maestro_Filtros(int id_corredor, string fecha)
        {
            SupervisorLN _SupervisorLN = new SupervisorLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _SupervisorLN.Listar_Maestro_Filtros(id_corredor, fecha, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }


        //[HttpPost]
        //public ActionResult Descargar_Formato_Despacho(string placa, int codigo_despacho)
        //{

        //    string usuario = "";
        //    if (Session["user_login"] == null)
        //    {
        //        return View("../Home/Sistemas");
        //    }
        //    else
        //    {
        //        usuario = Session["user_login"].ToString();
        //    }

        //    List<CC_CONDUCTORES> lista = _BusesLN.Listar_Placa();
        //    ViewBag.lista = new SelectList(lista, "PLACA", "PLACA");

        //    ReportesBuses reportes = new ReportesBuses();

        //    BusesModel buse_model = new BusesModel();

        //    var reportes_despacho = _BusesLN.Listar_Informes_Despacho();
        //    buse_model.Lista_Buses_despacho = reportes_despacho;

        //    try
        //    {
        //        string ruta_base = Server.MapPath("~");
        //        if (ruta_base.Substring(ruta_base.Length - 1) != "\\")
        //        {
        //            ruta_base = ruta_base + "\\";
        //        }
        //        var direccion = reportes.Insertar_Buses_Despacho_Formato(placa, ruta_base, codigo_despacho, usuario);
        //        buse_model.archivo = "Download/" + direccion;
        //        ViewBag.Message = "Abrir Archivo";


        //    }
        //    catch (Exception e)
        //    {
        //        string mensaje = e.Message;
        //        ViewBag.Message = "Error Archivo";
        //        ViewBag.error = mensaje;

        //        var datosCorredores_ = _CorredoresLN.obtenerListaCorredores();
        //        ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores_);

        //        return View("Reportes_Despacho", buse_model);

        //    }


        //    var datosCorredores = _CorredoresLN.obtenerListaCorredores();
        //    ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);

        //    return View("Reportes_Despacho", buse_model);
        //}

        [HttpPost]
        public ActionResult FIltrar_Docuemntos(DateTime? fecha, int id_corredor)
        {
            BUSESLN _BusesLN = new BUSESLN();
            CorredoresLN _CorredoresLN = new CorredoresLN();

            DateTimeOffset fecha2 = Convert.ToDateTime(fecha);
            String mensaje = "";
            Int32 tipo = 0;


            string fecha_actual = fecha2.ToString("dd.MM.yyyy");

            List<CC_CONDUCTORES> lista = _BusesLN.Listar_Placa(ref mensaje, ref tipo);
            ViewBag.lista = new SelectList(lista, "PLACA", "PLACA");

            ReportesBuses reportes = new ReportesBuses();
            BusesModel buse_model = new BusesModel();

            var reportes_despacho = _BusesLN.FIltrar_Archivo_Despacho(fecha_actual, id_corredor, ref mensaje, ref tipo);


            if (reportes_despacho.Count <= 0)
            {
                ViewBag.Message = "No Existen Registros";

            }

            buse_model.Lista_Buses_despacho = reportes_despacho;

            ViewBag.reportes = "Lista Reportes";

            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);


            return View("Reportes_Despacho", buse_model);
        }


        [HttpPost]
        public ActionResult Ubicacion_Despachador(string longitud, string latitud)
        {
            ViewBag.latitud = latitud;
            ViewBag.longitud = longitud;

            return View();
        }


    }
}