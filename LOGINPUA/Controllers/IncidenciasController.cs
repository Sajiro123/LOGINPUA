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
    public class IncidenciasController : Controller
    {

        //private readonly PerfilLN _PerfilLN;
        //Util.Util utilidades = new Util.Util();

        //private readonly LoginLN _LoginLN;
        //private readonly RutaLN _rutaLN;
        //private readonly CorredoresLN _CorredoresLN;
        //private readonly IncidenciasLN _IncidenciasLN;
        //public IncidenciasController()
        //{
        //    _LoginLN = new LoginLN();
        //    _rutaLN = new RutaLN();
        //    _IncidenciasLN = new IncidenciasLN();
        //    _CorredoresLN = new CorredoresLN();
        //}
        //ReadExcel readExcel = new ReadExcel();

        public ActionResult Incidencias()
        {
            Util.Util utilidades = new Util.Util();
            CorredoresLN _CorredoresLN = new CorredoresLN();
            RutaLN _rutaLN = new RutaLN();

            //limpiando los archivos subidos del dia anterior
            limpiar_upload();
            //limpiando la sesion
            Session["carpetaUpload"] = null;

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

        public string Listar_Incidencias(int id_ruta)
        {
            IncidenciasLN _IncidenciasLN = new IncidenciasLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _IncidenciasLN.Listar_Incidencias(id_ruta, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }

        //[HttpPost]
        //public ActionResult Registrar_Incidencia(CC_INCIDENCIA Model_Incidencia, HttpPostedFileBase Fotocargar1, HttpPostedFileBase Fotocargar2, HttpPostedFileBase Fotocargar3)
        //{
        //    IncidenciasLN _IncidenciasLN = new IncidenciasLN();
        //    Util.Util utilidades = new Util.Util();
        //    CorredoresLN _CorredoresLN = new CorredoresLN();
        //    RutaLN _rutaLN = new RutaLN();

        //    string urlArchivos = "~/Util/Imagenes_incidencias";
        //    String mensaje = "";
        //    Int32 tipo = 0;
        //    var result = "";

        //    string usuario = Session["user_login"].ToString();
        //    Model_Incidencia.ID_ESTADO = 1;
        //    Model_Incidencia.USU_REG = usuario;

        //    //ACTUALIZAR DE BAJA ID_MAESTRO ANTIGUO
        //    /* IMAGEN */
        //    string ruta_base = Server.MapPath(urlArchivos);

        //    var data = _IncidenciasLN.AgregarIncidencia(Model_Incidencia, ruta_base, ref mensaje, ref tipo, Fotocargar1, Fotocargar2, Fotocargar3);

        //    result = JsonConvert.SerializeObject(data);
        //    ViewBag.resultado = result;

        //    var listaVistas = Session["menu_modulo"] as DataTable;
        //    var listacciones = Session["menu_acciones"] as List<CC_MENUPERFIL_ACCION>;
        //    var nombreActionCurrent = this.ControllerContext.RouteData.Values["action"].ToString();
        //    bool auxValida = utilidades.validaVistaxPerfil(nombreActionCurrent, listaVistas);
        //    var Lista_acciones = utilidades.validadAccionMenu(listacciones, nombreActionCurrent, this.ControllerContext.RouteData.Values["controller"].ToString());

        //    ViewBag.currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
        //    ViewBag.currentRoute = this.ControllerContext.RouteData.Values["controller"].ToString();


        //    var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
        //    ViewBag.rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);
        //    ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);

        //    return View("Incidencias");

        //}

        public string Registrar_Incidencia(CC_INCIDENCIA Model_Incidencia)
        {
            IncidenciasLN _IncidenciasLN = new IncidenciasLN();
            Util.Util utilidades = new Util.Util();
            CorredoresLN _CorredoresLN = new CorredoresLN();
            RutaLN _rutaLN = new RutaLN();

            string urlArchivos = "~/Util/Imagenes_incidencias";
            String mensaje = "";
            Int32 tipo = 0;
            //var result = "";

            string usuario = Session["user_login"].ToString();
            Model_Incidencia.ID_ESTADO = 1;
            Model_Incidencia.USU_REG = usuario;

            /* IMAGEN */
            string ruta_base = Server.MapPath(urlArchivos);
            string carpeta = Path.Combine(Server.MapPath("~/Upload"), Session["carpetaUpload"].ToString());
            var data = _IncidenciasLN.AgregarIncidencia(Model_Incidencia, ruta_base, carpeta, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(data); //para la lista principal
            Session["carpetaUpload"] = null;
            return result;
            //return Json(data);
        }

        public JsonResult Agregar_Archivo(HttpPostedFileBase fileadj)
        {
            List<String> lista = new List<String>();
            string mensaje = "";

            if (Session["carpetaUpload"] == null)
            {
                Session["carpetaUpload"] = DateTime.Now.ToString("yyyyMMddHHmmss") + Session["user_login"];
            }
            string carpeta = Path.Combine(Server.MapPath("~/Upload"), Session["carpetaUpload"].ToString());
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            if (Directory.GetFiles(carpeta).Length < 3)
            {
                string archivo = Path.Combine(carpeta, fileadj.FileName);
                if (!System.IO.File.Exists(archivo))
                {
                    fileadj.SaveAs(archivo);
                }
                else
                {
                    mensaje = "El nombre del archivo ya existe";
                }
            }
            else
            {
                mensaje = "Ya ingresaste el máximo de 3 archivos";
            }

            foreach (string archivo in Directory.GetFiles(carpeta))
            {
                string nombre = Path.GetFileName(archivo);
                lista.Add(nombre);
            }

            
            return Json(new { carpeta = Session["carpetaUpload"], lista = lista, mensaje = mensaje });
        }

        public JsonResult Eliminar_Archivo(String nombre)
        {
            List<String> lista = new List<String>();
            string mensaje = "";
            string carpeta = "";
            if (Session["carpetaUpload"] != null)
            {
                carpeta = Path.Combine(Server.MapPath("~/Upload"), Session["carpetaUpload"].ToString());
                System.IO.File.Delete(Path.Combine(carpeta, nombre));

                //foreach (string archivo in Directory.GetFiles(carpeta))
                //{
                //    string nombre_2 = Path.GetFileName(archivo);
                //    lista.Add(nombre_2);
                //}
            }
            else
            {
                mensaje = "Aun no subes archivos";
            }
            return Json(new { carpeta = Session["carpetaUpload"], lista = lista, mensaje = mensaje });
        }

        void limpiar_upload()
        {
            try
            {
                string fecact = System.IO.File.ReadAllText(Server.MapPath("~/Upload/fecact.txt"));
                string fecha = DateTime.Now.ToString("yyyyMMdd");
                if (fecact != fecha)
                {
                    foreach (var carpeta in Directory.GetDirectories(Server.MapPath("~/Upload")))
                    {
                        foreach (var archivo in Directory.GetFiles(carpeta))
                        {
                            System.IO.File.Delete(archivo);
                        }
                        Directory.Delete(carpeta);
                    }
                    System.IO.File.WriteAllText(Server.MapPath("~/Upload/fecact.txt"), fecha);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public string Actualizar_Incidencia(CC_INCIDENCIA Model_Incidencia)
        {
            IncidenciasLN _IncidenciasLN = new IncidenciasLN();
            String mensaje = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();

            Model_Incidencia.USU_MODIF = usuario;

            var data = _IncidenciasLN.Actualizar_Incidencia(Model_Incidencia, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(data);
            return result;
        }

        public string Editar_Incidencia(CC_INCIDENCIA Model_Incidencia)
        {
            IncidenciasLN _IncidenciasLN = new IncidenciasLN();
            String mensaje = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();

            Model_Incidencia.USU_MODIF = usuario;

            var data = _IncidenciasLN.Editar_Incidencia(Model_Incidencia, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(data);
            return result;
        }

        public string AnularIncidencia(int idIncidencia)
        {
            IncidenciasLN _IncidenciasLN = new IncidenciasLN();
            String mensaje = "";
            Int32 tipo = 0;
            var anularBus = JsonConvert.SerializeObject(_IncidenciasLN.AnularIncidencia(idIncidencia, Session["user_login"].ToString(), ref mensaje, ref tipo));
            return anularBus;
        }

        public string ListarConcesionarios(int id_corredor)
        {
            IncidenciasLN _IncidenciasLN = new IncidenciasLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _IncidenciasLN.ListarConcesionarios(id_corredor, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }
        public string Estado_Incidencia(int id_incidencias, string estado)
        {
            IncidenciasLN _IncidenciasLN = new IncidenciasLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _IncidenciasLN.Estado_Incidencia(id_incidencias, estado, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }



        public ActionResult Infracciones()
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

        public string Listar_Infracciones()
        {
            IncidenciasLN _IncidenciasLN = new IncidenciasLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _IncidenciasLN.Listar_Infracciones(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }

        public string Listar_Persona_Incidencia()
        {
            IncidenciasLN _IncidenciasLN = new IncidenciasLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _IncidenciasLN.Listar_Persona_Incidencia(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }

        public string Registrar_Infraccion(CC_INFRACCION Model_Infraccion)
        {
            IncidenciasLN _IncidenciasLN = new IncidenciasLN();
            String mensaje = "";
            Int32 tipo = 0;
            RPTA_GENERAL r = new RPTA_GENERAL();
            var result = "";

            string usuario = Session["user_login"].ToString();
            Model_Infraccion.ID_ESTADO = 1;
            Model_Infraccion.USU_REG = usuario;

            var data = _IncidenciasLN.Registrar_Infraccion(Model_Infraccion, ref mensaje, ref tipo);
            result = JsonConvert.SerializeObject(data);


            return result;
        }

        public string Editar_Infraccion(CC_INFRACCION Model_Infraccion)
        {
            IncidenciasLN _IncidenciasLN = new IncidenciasLN();
            String mensaje = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();

            Model_Infraccion.USU_MODIF = usuario;


            var data = _IncidenciasLN.Editar_Infraccion(Model_Infraccion, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(data);
            return result;
        }


    }

}