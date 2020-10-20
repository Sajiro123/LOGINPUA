using ENTIDADES;
using LN.EntidadesLN;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class Produccion_DemandaController : Controller
    {
        // GET: Produccion_Demanda
        public ActionResult Cargar_Archivo()
        {
            return View();
        }

        public ActionResult Listar()
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


        public string Listar_Informes(string mes_año)
        {
            ProduccionDemandaLN ProduccionDemandaLN = new ProduccionDemandaLN();
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


            var Lista_informes = ProduccionDemandaLN.Listar_Informes(mes, año, ref mensaje, ref tipo);
            var rpta = JsonConvert.SerializeObject(Lista_informes);
            return rpta;
        }

        public string CargarArchivo(string fecha, int reemplazar)
        {

            ProduccionDemandaLN ProduccionDemandaLN = new ProduccionDemandaLN();
            var Rpta = new RPTA_GENERAL();

            String mensaje = "";
            Int32 tipo = 0;
            var nuevoNombreArchivoExcel = "";
            var rpt_result = ProduccionDemandaLN.ValidarFecha_Programados(fecha, ref mensaje, ref tipo);





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
                    var nombre = ProduccionDemandaLN.nombre_excel(id, ref mensaje, ref tipo);
                    System.IO.File.Delete(Server.MapPath("~/Adjuntos/Produccion/" + nombre.DES_ESTADO));
                    ProduccionDemandaLN.eliminar_Archivo(id, ref mensaje, ref tipo);

                    var ob = Request.Files;
                    var archivoSubido = Request.Files[0];
                    var nombreArchivo = Path.GetFileName(archivoSubido.FileName);
                    var extensionArchivo = Path.GetExtension(archivoSubido.FileName);
                    var arrFileName = nombreArchivo.Split('.');
                    nuevoNombreArchivoExcel = arrFileName[0] + extensionArchivo;
                    var pathArchivo = Server.MapPath("~/Adjuntos/Produccion/" + nuevoNombreArchivoExcel);
                    string pathFinal = Path.Combine(pathArchivo);
                    archivoSubido.SaveAs(pathFinal);

                    Rpta = ProduccionDemandaLN.RegistroInforme(nuevoNombreArchivoExcel, fecha, ref mensaje, ref tipo);
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
                var pathArchivo = Server.MapPath("~/Adjuntos/Produccion/" + nuevoNombreArchivoExcel);
                string pathFinal = Path.Combine(pathArchivo);
                archivoSubido.SaveAs(pathFinal);
                Rpta = ProduccionDemandaLN.RegistroInforme(nuevoNombreArchivoExcel, fecha, ref mensaje, ref tipo);

            }


            return JsonConvert.SerializeObject(Rpta);
        }

        public string ver_excelFinal(string resultado)
        {
            var pathArchivo = Server.MapPath("~/Adjuntos/Produccion/");
            string name_page = "verExcel.html";


            try
            {
                System.IO.File.Delete(Server.MapPath(pathArchivo + name_page));
            }
            catch (Exception)
            {
            }

            Workbook workbook = new Workbook();
            workbook.LoadFromFile(Server.MapPath("~/Adjuntos/Produccion/" + resultado));
            Worksheet sheet = workbook.Worksheets[0];
            var ruta = pathArchivo + name_page;
            sheet.SaveToHtml(pathArchivo + name_page);

            string text = System.IO.File.ReadAllText(pathArchivo + name_page);
            text = text.Replace("Evaluation&nbsp;Warning&nbsp;:&nbsp;The&nbsp;document&nbsp;was&nbsp;created&nbsp;with&nbsp;&nbsp;Spire.XLS&nbsp;for&nbsp;.NET", "<br/>");
            System.IO.File.WriteAllText(pathArchivo + name_page, text);

            //leer html 
            return JsonConvert.SerializeObject(name_page);
        }
    }
}