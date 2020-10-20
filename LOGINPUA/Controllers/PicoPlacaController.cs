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
    public class PicoPlacaController : Controller
    {
        //Reporte_ComparativoV model_rp = new Reporte_ComparativoV();

        //private readonly CorredoresLN _CorredoresLN;
        //private readonly RutaLN _rutaLN;
        //private readonly RegistroPicoPlacaLN _registroPicoPlacaLN;
        //Util.Util utilidades = new Util.Util();

        //public PicoPlacaController()
        //{
        //    _CorredoresLN = new CorredoresLN();
        //    _rutaLN = new RutaLN();
        //    _registroPicoPlacaLN = new RegistroPicoPlacaLN();

        //}
        // GET: PicoPlaca
        public ActionResult RegistroPicoPlaca()
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
                ViewBag.Accionesview = Lista_acciones; 
                ViewBag.rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);
                ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);

                return View(_CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo));
            }
            else
            {
                return RedirectToAction("Sistemas_", "Home");
            }
        }
        public ActionResult Reporte()
        {
            Util.Util utilidades = new Util.Util();
             RutaLN _rutaLN = new RutaLN();

            String mensaje = "";
            Int32 tipo = 0;
            var listaVistas = Session["menu_modulo"] as DataTable;
            var listacciones = Session["menu_acciones"] as List<CC_MENUPERFIL_ACCION>;
            //Session["menu_modulo"] = dt4;
            var nombreActionCurrent = this.ControllerContext.RouteData.Values["action"].ToString();
            bool auxValida = utilidades.validaVistaxPerfil(nombreActionCurrent, listaVistas);
            var Lista_acciones = utilidades.validadAccionMenu(listacciones,nombreActionCurrent, this.ControllerContext.RouteData.Values["controller"].ToString());

            ViewBag.currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.currentRoute = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (auxValida)
            {
                ViewBag.Accionesview = Lista_acciones;
                var rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);
                return View(rutas);
            }
            else
            {
                return RedirectToAction("Sistemas_", "Home");
            }
        }

        public string ConsultarHoras(string fechaInicio, string fechaFin, int id_ruta)
        {
            RegistroPicoPlacaLN _registroPicoPlacaLN = new RegistroPicoPlacaLN();
            String mensaje = "";
            Int32 tipo = 0;

            string fechaInicio_cambiada = fechaInicio.Replace(" ", "");
            fechaInicio_cambiada = fechaInicio_cambiada.Replace("-", "|");
            string fechaFin_cambiada = fechaFin.Replace(" ", "");
            fechaFin_cambiada = fechaFin_cambiada.Replace("-", "|");


            var Tabla1 = _registroPicoPlacaLN.getHora_Comparativo(fechaInicio_cambiada, id_ruta, "MAÑANA", ref mensaje, ref tipo);
            var Tabla2 = _registroPicoPlacaLN.getHora_Comparativo(fechaInicio_cambiada, id_ruta, "TARDE", ref mensaje, ref tipo);

            var Tabla3 = _registroPicoPlacaLN.getHora_Comparativo(fechaFin_cambiada, id_ruta, "MAÑANA", ref mensaje, ref tipo);
            var Tabla4 = _registroPicoPlacaLN.getHora_Comparativo(fechaFin_cambiada, id_ruta, "TARDE", ref mensaje, ref tipo);


            List<CC_HORAS> horas_1 = new List<CC_HORAS>();
            List<CC_HORAS> horas_2 = new List<CC_HORAS>();
            List<CC_HORAS> horas_3 = new List<CC_HORAS>();
            List<CC_HORAS> horas_4 = new List<CC_HORAS>();


            //RECORRER mañanaida
            foreach (var item in Tabla1)
            {
                CC_HORAS horas = new CC_HORAS();

                horas.mañanaida = item.TIEMPO_IDA;
                horas.mañanavuelta = item.TIEMPO_VUELTA;
                horas_1.Add(horas);
            }
            //RECORRER mañanaida

            foreach (var item in Tabla2)
            {
                CC_HORAS horas = new CC_HORAS();

                horas.mañanaida = item.TIEMPO_IDA;
                horas.mañanavuelta = item.TIEMPO_VUELTA;
                horas_2.Add(horas);
            }
            //RECORRER mañanaida

            foreach (var item in Tabla3)
            {
                CC_HORAS horas = new CC_HORAS();

                horas.mañanaida = item.TIEMPO_IDA;
                horas.mañanavuelta = item.TIEMPO_VUELTA;
                horas_3.Add(horas);
            }
            //RECORRER mañanaida

            foreach (var item in Tabla4)
            {
                CC_HORAS horas = new CC_HORAS();

                horas.mañanaida = item.TIEMPO_IDA;
                horas.mañanavuelta = item.TIEMPO_VUELTA;
                horas_4.Add(horas);
            }


            var data_ = new Dictionary<string, List<CC_HORAS>>(); //columna, nombre

            data_.Add("d_manana_ida", horas_1);
            data_.Add("d_manana_vuelta", horas_2);
            data_.Add("d_tarde_ida", horas_3);
            data_.Add("d_tarde_vuelta", horas_4);

            //            var result = JsonConvert.SerializeObject(); //para la lista principal 
            var result = JsonConvert.SerializeObject(data_); //para la lista principal
            return result;
        }
        public string Reporte_Comparativo(string fechaInicio, string fechaFin, int id_ruta, string MAÑANA_IDA, string MAÑANA_VUELTA, string TARDE_IDA, string TARDE_VUELTA)
        {
            RegistroPicoPlacaLN _registroPicoPlacaLN = new RegistroPicoPlacaLN();
            Reporte_ComparativoV model_rp = new Reporte_ComparativoV();
            RutaLN _rutaLN = new RutaLN();

            String mensaje = "";
            Int32 tipo = 0;
            CC_REPORTE_PICO_PLACA model = new CC_REPORTE_PICO_PLACA();


            string fechaInicio_cambiada = fechaInicio.Replace(" ", "");
            fechaInicio_cambiada = fechaInicio_cambiada.Replace("-", "|");
            string fechaFin_cambiada = fechaFin.Replace(" ", "");
            fechaFin_cambiada = fechaFin_cambiada.Replace("-", "|");
            //CONSULTA MAÑANA
            var Tabla1 = _registroPicoPlacaLN.getReporte_Comparativo(fechaInicio_cambiada, id_ruta, "MAÑANA", MAÑANA_IDA, MAÑANA_VUELTA, ref mensaje, ref tipo);
            var Tabla2 = _registroPicoPlacaLN.getReporte_Comparativo(fechaFin_cambiada, id_ruta, "MAÑANA", MAÑANA_IDA, MAÑANA_VUELTA, ref mensaje, ref tipo);

            //CONSULTA TARDE
            var Tabla3 = _registroPicoPlacaLN.getReporte_Comparativo(fechaInicio_cambiada, id_ruta, "TARDE", TARDE_IDA, TARDE_VUELTA, ref mensaje, ref tipo);
            var Tabla4 = _registroPicoPlacaLN.getReporte_Comparativo(fechaFin_cambiada, id_ruta, "TARDE", TARDE_IDA, TARDE_VUELTA, ref mensaje, ref tipo);



            if (Tabla1.Count > 0 && Tabla2.Count > 0 && Tabla3.Count > 0 && Tabla4.Count > 0)
            {
                //TABLA 1
                foreach (var item in Tabla1)
                {
                    //VELOCIDAD TIEMPO A Y B
                    model_rp.suma_tiempo_A = Double.Parse(item.TIEMPO_PROM_A);
                    model_rp.suma_tiempo_B = Double.Parse(item.TIEMPO_PROM_B);
                    if (model_rp.suma_tiempo_A > 0)
                    {
                        model_rp.contador_tiempoa++;
                        model_rp.total_tiempo_a += model_rp.suma_tiempo_A;
                    }
                    if (model_rp.suma_tiempo_B > 0)
                    {
                        model_rp.contador_tiempob++;
                        model_rp.total_tiempo_b += model_rp.suma_tiempo_B;
                    }
                    //VELOCIDAD PROMEDIO
                    model_rp.suma_velocidad_A = item.VEL_PROMEDIO_AB;
                    model_rp.suma_velocidad_B = item.VEL_PROMEDIO_BA;

                    if (model_rp.suma_velocidad_A > 0)
                    {
                        model_rp.TOTAL_a_velocidad += model_rp.suma_velocidad_A;
                        model_rp.contador_velocidad_a++;
                    }

                    if (model_rp.suma_velocidad_B > 0)
                    {
                        model_rp.TOTAL_b_velocidad += model_rp.suma_velocidad_B;
                        model_rp.contador_velocidad_b++;

                    }
                }
                //PROMEDIO


                model_rp.total_tiempo_a = model_rp.total_tiempo_a == 0 ? 0 : (model_rp.total_tiempo_a / model_rp.contador_tiempoa);
                model_rp.total_tiempo_b = model_rp.total_tiempo_b == 0 ? 0 : (model_rp.total_tiempo_b / model_rp.contador_tiempob);

                //VELOCIDAD
                model_rp.TOTAL_a_velocidad = model_rp.TOTAL_a_velocidad == 0 ? 0 : (model_rp.TOTAL_a_velocidad / model_rp.contador_velocidad_a);
                model_rp.TOTAL_b_velocidad = model_rp.TOTAL_b_velocidad == 0 ? 0 : (model_rp.TOTAL_b_velocidad / model_rp.contador_velocidad_b);

                //SETEAR LOS DATOS AL MODEL
                model.VEL_PROMEDIO_A_MAÑANA_IDA = model_rp.TOTAL_a_velocidad.ToString();
                model.VEL_PROMEDIO_A_MAÑANA_VUELTA = model_rp.TOTAL_b_velocidad.ToString();

                model.TIEMPO_PROM_A_1 = model_rp.total_tiempo_a.ToString();
                model.TIEMPO_PROM_B_1 = model_rp.total_tiempo_b.ToString();

                Limpiar(model_rp);

                //TABLA 2
                foreach (var item in Tabla2)
                {
                    //VELOCIDAD TIEMPO A Y B
                    model_rp.suma_tiempo_A = Double.Parse(item.TIEMPO_PROM_A);
                    model_rp.suma_tiempo_B = Double.Parse(item.TIEMPO_PROM_B);

                    if (model_rp.suma_tiempo_A > 0)
                    {
                        model_rp.contador_tiempoa++;
                        model_rp.total_tiempo_a += model_rp.suma_tiempo_A;

                    }
                    if (model_rp.suma_tiempo_B > 0)
                    {
                        model_rp.contador_tiempob++;
                        model_rp.total_tiempo_b += model_rp.suma_tiempo_B;

                    }
                    //VELOCIDAD PROMEDIO AB

                    model_rp.suma_velocidad_A = item.VEL_PROMEDIO_AB;
                    model_rp.suma_velocidad_B = item.VEL_PROMEDIO_BA;

                    if (model_rp.suma_velocidad_A > 0)
                    {
                        model_rp.TOTAL_a_velocidad += model_rp.suma_velocidad_A;
                        model_rp.contador_velocidad_a++;
                    }

                    if (model_rp.suma_velocidad_B > 0)
                    {
                        model_rp.TOTAL_b_velocidad += model_rp.suma_velocidad_B;
                        model_rp.contador_velocidad_b++;

                    }
                }

                //TIEMPO 
                model_rp.total_tiempo_a = model_rp.total_tiempo_a == 0 ? 0 : (model_rp.total_tiempo_a / model_rp.contador_tiempoa);
                model_rp.total_tiempo_b = model_rp.total_tiempo_b == 0 ? 0 : (model_rp.total_tiempo_b / model_rp.contador_tiempob);

                //VELOCIDAD
                model_rp.TOTAL_a_velocidad = model_rp.TOTAL_a_velocidad == 0 ? 0 : (model_rp.TOTAL_a_velocidad / model_rp.contador_velocidad_a);
                model_rp.TOTAL_b_velocidad = model_rp.TOTAL_b_velocidad == 0 ? 0 : (model_rp.TOTAL_b_velocidad / model_rp.contador_velocidad_b);

                //MODEL
                model.VEL_PROMEDIO_B_MAÑANA_IDA = model_rp.TOTAL_a_velocidad.ToString();
                model.VEL_PROMEDIO_B_MAÑANA_VUELTA = model_rp.TOTAL_b_velocidad.ToString();

                model.TIEMPO_PROM_A_2 = model_rp.total_tiempo_a.ToString();
                model.TIEMPO_PROM_B_2 = model_rp.total_tiempo_b.ToString();

                Limpiar(model_rp);

                //TABLA 3
                foreach (var item in Tabla3)
                {
                    model_rp.suma_tiempo_A = Double.Parse(item.TIEMPO_PROM_A);
                    model_rp.suma_tiempo_B = Double.Parse(item.TIEMPO_PROM_B);
                    if (model_rp.suma_tiempo_A > 0)
                    {
                        model_rp.contador_tiempoa++;
                        model_rp.total_tiempo_a += model_rp.suma_tiempo_A;

                    }
                    if (model_rp.suma_tiempo_B > 0)
                    {
                        model_rp.contador_tiempob++;
                        model_rp.total_tiempo_b += model_rp.suma_tiempo_B;
                    }

                    if (model_rp.suma_velocidad_A > 0)
                    {
                        model_rp.TOTAL_a_velocidad += model_rp.suma_velocidad_A;
                        model_rp.contador_velocidad_a++;
                    }

                    if (model_rp.suma_velocidad_B > 0)
                    {
                        model_rp.TOTAL_b_velocidad += model_rp.suma_velocidad_B;
                        model_rp.contador_velocidad_b++;

                    }
                }
                //TIEMPO 
                model_rp.total_tiempo_a = model_rp.total_tiempo_a == 0 ? 0 : (model_rp.total_tiempo_a / model_rp.contador_tiempoa);
                model_rp.total_tiempo_b = model_rp.total_tiempo_b == 0 ? 0 : (model_rp.total_tiempo_b / model_rp.contador_tiempob);


                //VELOCIDAD
                model_rp.TOTAL_a_velocidad = model_rp.TOTAL_a_velocidad == 0 ? 0 : (model_rp.TOTAL_a_velocidad / model_rp.contador_velocidad_a);
                model_rp.TOTAL_b_velocidad = model_rp.TOTAL_b_velocidad == 0 ? 0 : (model_rp.TOTAL_b_velocidad / model_rp.contador_velocidad_b);



                //MODEL
                model.VEL_PROMEDIO_A_TARDE_IDA = model_rp.TOTAL_a_velocidad.ToString();
                model.VEL_PROMEDIO_A_TARDE_VUELTA = model_rp.TOTAL_b_velocidad.ToString();

                model.TIEMPO_PROM_A_3 = model_rp.total_tiempo_a.ToString();
                model.TIEMPO_PROM_B_3 = model_rp.total_tiempo_b.ToString();

                Limpiar(model_rp);


                string corredor = "";

                //TABLA 4
                foreach (var item in Tabla4)
                {
                    model_rp.suma_tiempo_A = Double.Parse(item.TIEMPO_PROM_A);
                    model_rp.suma_tiempo_B = Double.Parse(item.TIEMPO_PROM_B);

                    if (model_rp.suma_tiempo_A > 0)
                    {
                        model_rp.contador_tiempoa++;
                        model_rp.total_tiempo_a += model_rp.suma_tiempo_A;

                    }
                    if (model_rp.suma_tiempo_B > 0)
                    {
                        model_rp.contador_tiempob++;
                        model_rp.total_tiempo_b += model_rp.suma_tiempo_B;

                    }

                    if (model_rp.suma_velocidad_A > 0)
                    {
                        model_rp.TOTAL_a_velocidad += model_rp.suma_velocidad_A;
                        model_rp.contador_velocidad_a++;
                    }

                    if (model_rp.suma_velocidad_B > 0)
                    {
                        model_rp.TOTAL_b_velocidad += model_rp.suma_velocidad_B;
                        model_rp.contador_velocidad_b++;

                    }
                }

                //TIEMPO 
                model_rp.total_tiempo_a = model_rp.total_tiempo_a == 0 ? 0 : (model_rp.total_tiempo_a / model_rp.contador_tiempoa);
                model_rp.total_tiempo_b = model_rp.total_tiempo_b == 0 ? 0 : (model_rp.total_tiempo_b / model_rp.contador_tiempob);


                //VELOCIDAD
                model_rp.TOTAL_a_velocidad = model_rp.TOTAL_a_velocidad == 0 ? 0 : (model_rp.TOTAL_a_velocidad / model_rp.contador_velocidad_a);
                model_rp.TOTAL_b_velocidad = model_rp.TOTAL_b_velocidad == 0 ? 0 : (model_rp.TOTAL_b_velocidad / model_rp.contador_velocidad_b);


                //MODEL
                model.VEL_PROMEDIO_B_TARDE_IDA = model_rp.TOTAL_a_velocidad.ToString();
                model.VEL_PROMEDIO_B_TARDE_VUELTA = model_rp.TOTAL_b_velocidad.ToString();


                model.TIEMPO_PROM_A_4 = model_rp.total_tiempo_a.ToString();
                model.TIEMPO_PROM_B_4 = model_rp.total_tiempo_b.ToString();

                //TARDE

                //FECHAS 
                model.FECHA_INICIO = fechaInicio;
                model.FECHA_FIN = fechaFin;

                model.M_IDA = MAÑANA_IDA;
                model.M_VUELTA = MAÑANA_VUELTA;
                model.T_IDA = TARDE_IDA;
                model.T_VUELTA = TARDE_VUELTA;


                var id_rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);

                foreach (var item in id_rutas)
                {
                    if (id_ruta == item.ID_RUTA)
                    {
                        corredor = item.NOMBRE;
                    }
                }
                model.CORREDOR = corredor;
            }
            else
            {
                model.TIEMPO_PROM_A_2 = "0";
                model.TIEMPO_PROM_B_2 = "0";
            }

            var result = JsonConvert.SerializeObject(model); //para la lista principal
            return result;
        }
        public ActionResult exportarPDF(string VEL_PROMEDIO_A_MANANA_IDA, string VEL_PROMEDIO_A_MANANA_VUELTA, string TIEMPO_PROM_A_1, string TIEMPO_PROM_B_1, string VEL_PROMEDIO_B_MANANA_IDA, string VEL_PROMEDIO_B_MANANA_VUELTA, string TIEMPO_PROM_A_2, string TIEMPO_PROM_B_2, string VEL_PROMEDIO_A_TARDE_IDA, string VEL_PROMEDIO_A_TARDE_VUELTA, string TIEMPO_PROM_A_3, string TIEMPO_PROM_B_3, string VEL_PROMEDIO_B_TARDE_IDA, string VEL_PROMEDIO_B_TARDE_VUELTA, string TIEMPO_PROM_A_4, string TIEMPO_PROM_B_4, string FECHA_INICIO, string FECHA_FIN, string CORREDOR, string M_IDA, string M_VUELTA, string T_IDA, string T_VUELTA)
        {
            RutaLN _rutaLN = new RutaLN();

            String mensaje = "";
            Int32 tipo = 0;

            var direccion = "";
            CC_REPORTE_PICO_PLACA model = new CC_REPORTE_PICO_PLACA();

            model.VEL_PROMEDIO_A_MAÑANA_IDA = VEL_PROMEDIO_A_MANANA_IDA;
            model.VEL_PROMEDIO_A_MAÑANA_VUELTA = VEL_PROMEDIO_A_MANANA_VUELTA;
            model.TIEMPO_PROM_A_1 = TIEMPO_PROM_A_1;
            model.TIEMPO_PROM_B_1 = TIEMPO_PROM_B_1;

            model.VEL_PROMEDIO_B_MAÑANA_IDA = VEL_PROMEDIO_B_MANANA_IDA;
            model.VEL_PROMEDIO_B_MAÑANA_VUELTA = VEL_PROMEDIO_B_MANANA_VUELTA;
            model.TIEMPO_PROM_A_2 = TIEMPO_PROM_A_2;
            model.TIEMPO_PROM_B_2 = TIEMPO_PROM_B_2;

            model.VEL_PROMEDIO_A_TARDE_IDA = VEL_PROMEDIO_A_TARDE_IDA;
            model.VEL_PROMEDIO_A_TARDE_VUELTA = VEL_PROMEDIO_A_TARDE_VUELTA;
            model.TIEMPO_PROM_A_3 = TIEMPO_PROM_A_3;
            model.TIEMPO_PROM_B_3 = TIEMPO_PROM_B_3;

            model.FECHA_INICIO = FECHA_INICIO;
            model.FECHA_FIN = FECHA_FIN;
            model.CORREDOR = CORREDOR;


            model.VEL_PROMEDIO_B_TARDE_IDA = VEL_PROMEDIO_B_TARDE_IDA;
            model.VEL_PROMEDIO_B_TARDE_VUELTA = VEL_PROMEDIO_B_TARDE_VUELTA;
            model.TIEMPO_PROM_A_4 = TIEMPO_PROM_A_4;
            model.TIEMPO_PROM_B_4 = TIEMPO_PROM_B_4;


            model.M_IDA = M_IDA;
            model.M_VUELTA = M_VUELTA;
            model.T_IDA = T_IDA;
            model.T_VUELTA = T_VUELTA;

            string ruta_base = Server.MapPath("~");
            if (ruta_base.Substring(ruta_base.Length - 1) != "\\")
            {
                ruta_base = ruta_base + "\\";
            }

            Reportes_Comparativo model_rp = new Reportes_Comparativo();

            try
            {
                direccion = model_rp.Insertar_rp_comparativo(model, ruta_base);

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
      

        public string subir_archivo(int idRuta, string fecha, int reemplazar, string abrevProveedorSistemasCorredor, string tipoCarga)
        {
            var rpta = "";

            switch (tipoCarga)
            {
                case "I":
                    rpta = importarExcelDespachos(idRuta, fecha, reemplazar, abrevProveedorSistemasCorredor);
                    break;
                case "M":
                    rpta = Carga_Masiva(idRuta, abrevProveedorSistemasCorredor);
                    break;
                default:
                    break;
            }
            return rpta;
        }
        public string Carga_Masiva(int idRuta, string abrevProveedorSistemasCorredor)
        {
            RPTA_GENERAL e = new RPTA_GENERAL();
            e.COD_ESTADO = 0;
            e.DES_ESTADO = "";
            var ob = Request.Files;
            var archivoSubido = Request.Files[0];
            var nombreArchivo = Path.GetFileName(archivoSubido.FileName);
            var extensionArchivo = Path.GetExtension(archivoSubido.FileName);
            var arrFileName = nombreArchivo.Split('.');
            var nuevoNombreArchivoExcel = arrFileName[0] + "_FILE_" + DateTime.Now.ToString("dd_MM_yyyy_h_mm_ss") + extensionArchivo;
            var pathArchivo = Server.MapPath("~/Download/" + nuevoNombreArchivoExcel);
            string pathFinal = Path.Combine(pathArchivo);
            archivoSubido.SaveAs(pathFinal);
            var pathArchivoextract = Server.MapPath("~/Download/excel");
            //crear contador para saber cuantos fueron registrados con exito
            int exitoso = 0;
            int fallido = 0;
            var mensajeError = "";
            //eliminando los archivos que contienen el excel
            string[] files = Directory.GetFiles(pathArchivoextract);
            foreach (string file in files)
            {
                System.IO.File.Delete(file);
            }
            /*##############################*/

            try
            {
                using (ZipArchive zfile = ZipFile.Open(pathFinal, ZipArchiveMode.Read))
                {
                    ZipFile.ExtractToDirectory(pathArchivo, pathArchivoextract);
                    int i = 0;
                    int rowarchivos = 0;

                    using (ZipArchive archive = ZipFile.OpenRead(pathArchivo))
                    {
                        rowarchivos = archive.Entries.Count();

                        string[] nombres = new string[rowarchivos];
                        string[] arrSplit = new string[rowarchivos];
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {

                            nombres[i] = entry.Name;
                            string fechaSinformatoExcel = "";
                            string fecformateada1 = "";
                            string fecformateada2 = "";
                            string fecformateada3 = "";
                            string fecformateadaT = "";

                            arrSplit = nombres[i].Split('_');
                            fechaSinformatoExcel = arrSplit[1];

                            fecformateada1 = fechaSinformatoExcel.Substring(0, 2) + "/";
                            fecformateada2 = fechaSinformatoExcel.Substring(2, 2) + "/";
                            fecformateada3 = fechaSinformatoExcel.Substring(4, 4);
                            fecformateadaT = fecformateada1 + fecformateada2 + fecformateada3;
                            string nom_excel = nombres[i];
                            var path_excel = Server.MapPath("~/Download/excel/" + nom_excel);
                            string pathFinal_excel = Path.Combine(path_excel);

                            e = importarMasivo(idRuta, fecformateadaT, abrevProveedorSistemasCorredor, pathFinal_excel);

                            if (e.COD_ESTADO == 1)
                            {
                                exitoso++;
                                e.COD_ESTADO = 1;
                            }
                            else { fallido++; }

                            i++;
                            string ruta_base = Server.MapPath("~");
                            if (ruta_base.Substring(ruta_base.Length - 1) != "\\")
                            {
                                ruta_base = ruta_base + "\\";
                            }
                            System.IO.File.Delete(pathArchivoextract + "\\" + nom_excel);
                        }
                        zfile.Dispose();
                    }
                    System.IO.File.Delete(pathArchivo);
                }
            }
            catch (Exception ex)
            {
                e.COD_ESTADO = 0;
                mensajeError = ex.Message;
            }
            System.IO.File.Delete(pathFinal);
            e.DES_ESTADO = (exitoso > 0 ? exitoso + " fechas guardadas exitosamente." : "Ocurrió un error no se han guardado los registros." + "->" + mensajeError);

            return JsonConvert.SerializeObject(e);
        }
        public RPTA_GENERAL importarMasivo(int idRuta, string fecha, string abrevProveedorSistemasCorredor, string pathFinal)
        {
            RegistroPicoPlacaLN _registroPicoPlacaLN = new RegistroPicoPlacaLN();

            String mensaje = "";
            Int32 tipo = 0;
            RPTA_GENERAL e = new RPTA_GENERAL();
            e = _registroPicoPlacaLN.anularRegistrosPorFecha(fecha, idRuta, ref mensaje, ref tipo); //anula si encuentra data 
            if (e.COD_ESTADO == 0)
            { //valida si ocurrio un error al momento de anular los registros
                return e;
            }

            var rpta = "";
            switch (abrevProveedorSistemasCorredor)
            {
                case "TGA":
                    rpta = importarExcelDespachosABEXA(idRuta, fecha, pathFinal);
                    break;
                case "SJL":
                    rpta = importarExcelDespachosABEXA(idRuta, fecha, pathFinal);
                    break;
                case "CC":
                    rpta = importarExcelDespachosABEXA(idRuta, fecha, pathFinal);
                    break;
                case "PN":
                    rpta = importarExcelDespachosHACOM(idRuta, fecha, pathFinal);
                    break;
                case "JP":
                    rpta = importarExcelDespachosHACOM(idRuta, fecha, pathFinal);
                    break;
                default:
                    rpta = "ERROR LLAMAR AL ADMINISTRADOR DEL SISTEMA";
                    break;
            }
            return JsonConvert.DeserializeObject<RPTA_GENERAL>(rpta);
        }
        public string Rutas_ciclica(int idRuta, string fecha, SLDocument hojaDespacho)
        {
            RegistroPicoPlacaLN _registroPicoPlacaLN = new RegistroPicoPlacaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var mensajeError = "";
            RPTA_GENERAL e = new RPTA_GENERAL();
            e.COD_ESTADO = 1;
            e.DES_ESTADO = "Correctamente";
            try
            {


                CC_RECORRIDO recorrido = new CC_RECORRIDO();
                SLWorksheetStatistics hojaDespachoStadistics = hojaDespacho.GetWorksheetStatistics();

                var positionFinB = 1;
                var PositionEncabezadoA_final = 0;
                double[] distanciaRecorrido = { 0.0, 0.0 };
                string fechaTemporal = DateTime.Now.ToString("dd/MM/yyyy");
                int PositionEncabezadoB_final = 0;

                string[] lados = { "A", "B" };
                //string[] rangoHorario = { "06:00-07:00", "07:01-08:00", "08:01-09:00", "09:01-10:00", "17:00-18:00", "18:01-19:00", "19:01-20:00", "20:01-21:00" };
                string[] rangoHorario = {
                                                "04:00:00 AM-04:29:00 AM", "04:30:00 AM-04:59:00 AM",
                                                "05:00:00 AM-05:29:00 AM", "05:30:00 AM-05:59:00 AM",
                                                "06:00:00 AM-06:29:00 AM", "06:30:00 AM-06:59:00 AM",
                                                "07:00:00 AM-07:29:00 AM", "07:30:00 AM-07:59:00 AM",
                                                "08:00:00 AM-08:29:00 AM", "08:30:00 AM-08:59:00 AM",
                                                "09:00:00 AM-09:29:00 AM", "09:30:00 AM-09:59:00 AM",
                                                "10:00:00 AM-10:29:00 AM", "10:30:00 AM-10:59:00 AM",
                                                "11:00:00 AM-11:29:00 AM", "11:30:00 AM-11:59:00 AM",
                                                "12:00:00 PM-12:29:00 PM", "12:30:00 PM-12:59:00 PM",
                                                "01:00:00 PM-01:29:00 PM", "01:30:00 PM-01:59:00 PM",
                                                "02:00:00 PM-02:29:00 PM", "02:30:00 PM-02:59:00 PM",
                                                "03:00:00 PM-03:29:00 PM", "03:30:00 PM-03:59:00 PM",
                                                "04:00:00 PM-04:29:00 PM", "04:30:00 PM-04:59:00 PM",
                                                "05:00:00 PM-05:29:00 PM", "05:30:00 PM-05:59:00 PM",
                                                "06:00:00 PM-06:29:00 PM", "06:30:00 PM-06:59:00 PM",
                                                "07:00:00 PM-07:29:00 PM", "07:30:00 PM-07:59:00 PM",
                                                "08:00:00 PM-08:29:00 PM", "08:30:00 PM-08:59:00 PM",
                                                "09:00:00 PM-09:29:00 PM", "09:30:00 PM-09:59:00 PM",
                                                "10:00:00 PM-10:29:00 PM", "10:30:00 PM-10:59:00 PM",
                                                "11:00:00 PM-11:29:00 PM", "11:30:00 PM-11:59:00 PM"
                                            };

                recorrido = _registroPicoPlacaLN.getKmPorLadoYRecorrido(idRuta, lados[0], ref mensaje, ref tipo); //LADO A
                distanciaRecorrido[0] = recorrido.MEDIDA_KM;
                recorrido = _registroPicoPlacaLN.getKmPorLadoYRecorrido(idRuta, lados[1], ref mensaje, ref tipo); //LADO B
                distanciaRecorrido[1] = recorrido.MEDIDA_KM;

                int[] posicionRowLADOS = { 0, 0 };
                for (int col = 1; col < hojaDespachoStadistics.EndColumnIndex + 1; col++) //solo para obtener posicion de las cabeceras
                {
                    for (int row = 1; row < hojaDespachoStadistics.EndRowIndex; row++)
                    {
                        //RECORRER LAS CABECERAS
                        var FilaColumnaFinal = hojaDespacho.GetCellValueAsString(9, col).ToString();

                        var FilaColumna_inicial_A = hojaDespacho.GetCellValueAsString(10, col).ToString();


                        //VALIDACION DEL EXCEL FECHA  
                        if (row == 5)
                        {
                            var textoColumna_Fecha = hojaDespacho.GetCellValueAsString(5, 2);
                            string fecha_excel = Convert.ToDateTime(textoColumna_Fecha).ToString("dd/MM/yyyy");
                            if (fecha != fecha_excel)
                            {
                                e.COD_ESTADO = 0;
                                e.DES_ESTADO = "ERROR -> La fecha no corresponde al de la información.";
                                return JsonConvert.SerializeObject(e);
                            }
                        }


                        switch (FilaColumnaFinal)
                        {
                            case "Total KM":
                                PositionEncabezadoB_final = col - 1; //1                             
                                break;
                        }

                        //Saber el comienzo Del Lado B
                        if (FilaColumna_inicial_A.EndsWith("2") && positionFinB == 1)
                        {
                            PositionEncabezadoA_final = col - 1;   //300
                            positionFinB = 0;
                        }
                    }
                }
                ////para la division del promediado
                ////acumulado para el promediado
                var totalRegistrosPorRango_B = 0; //para la division del promediado
                var acumuladoDistanciasPorRango_B = 0.0; //acumulado para el promediado
                var velocidadPromediadaPorTurno_B = 0.0; //velocidad Promediada
                string turno = "";
                var cantidadRegistrosGuardados = 0;
                var cantidadRegistrosFallidos = 0;
                var auxRangos = 0;


                double[] menorTiempoViajeMinutosAB = obtenerMenorHoraDeViajeEnMinutosCiclicas(hojaDespacho, idRuta);
                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        foreach (var item in rangoHorario) //RECORRE HORARIOS
                        {
                            double velocidadPromediadaPorTurno_A = 0.0; //velocidad Promediada
                            var totalRegistrosPorRango_A = 0;
                            double acumuladoDistanciasPorRango_A = 0;
                            double tiempo_A = 0;
                            double tiempo_B = 0;
                            long timestampFechaHoraInicio = 0;
                            long timestampFechaHoraFin = 0;
                            auxRangos++;
                            var arrdataHoras = item.Split('-');
                            var hInicio = fechaTemporal + " " + arrdataHoras[0];
                            var hFin = fechaTemporal + " " + arrdataHoras[1];

                            //LADO A RECORRIDO DE FILAS

                            for (int row = 11; row < hojaDespachoStadistics.EndRowIndex; row++) //11 es la posicicion donde comienza la data 
                            {
                                //VALIDACION DE VACIOS EN PARADEROS
                                var Hora_ParaderoInicio_A = hojaDespacho.GetCellValueAsDateTime(row, 8).ToString("HH:mm"); //8 es la posicion donde empieza el lado A
                                var Hora_Final_A = hojaDespacho.GetCellValueAsDateTime(row, PositionEncabezadoA_final).ToString("HH:mm"); //PositionEncabezadoA_final es la posicion donde acaba A


                                if (Hora_ParaderoInicio_A != "00:00" && Hora_Final_A != "00:00")
                                {
                                    var RowHora_Final_A = fechaTemporal + " " + Hora_Final_A;
                                    var RowHora_ParaderoInicio_A = fechaTemporal + " " + Hora_ParaderoInicio_A;
                                    var estaDentroDelRango = false;

                                    estaDentroDelRango = BetweenDatetimesTimestamp(ConvertToTimestamp(DateTime.Parse(RowHora_ParaderoInicio_A)).ToString(),
                                                                                    ConvertToTimestamp(DateTime.Parse(hInicio)).ToString(),
                                                                                    ConvertToTimestamp(DateTime.Parse(hFin)).ToString()
                                                                                   );
                                    //if (BetweenDatetimes(DateTime.Parse(RowHora_ParaderoInicio_A), DateTime.Parse(hInicio), DateTime.Parse(hFin)))
                                    if (estaDentroDelRango)
                                    {

                                        timestampFechaHoraInicio = ConvertToTimestamp(DateTime.Parse(Hora_ParaderoInicio_A));
                                        timestampFechaHoraFin = ConvertToTimestamp(DateTime.Parse(Hora_Final_A));
                                        var tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                                        if (tiempoViajeTimestamp < 0)
                                        {
                                            tiempoViajeTimestamp = 0;
                                            timestampFechaHoraFin = timestampFechaHoraFin + 86400; //le agrega un dia 
                                            tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                                        }
                                        double tiempoViajeminutos = (tiempoViajeTimestamp / 60);
                                        double tiempoViajeHoras = tiempoViajeminutos / 60; //tiempo en horas
                                        double velocidadKM = (distanciaRecorrido[0] / tiempoViajeHoras);//velocidad x tiempo  [0] = LADO A , [1] = LADO B

                                        if (tiempoViajeminutos <= menorTiempoViajeMinutosAB[0] && tiempoViajeminutos >= 30) //viaje mayor igual a 30 minutos
                                        { //si no sobrepasa el menor tiempo del viaje A + 2horas entonces entra a la validacion
                                            totalRegistrosPorRango_A++; //aumenta la cantidad de registros para luego hacer el promediado
                                            acumuladoDistanciasPorRango_A += velocidadKM;
                                            tiempo_A += tiempoViajeHoras;
                                        }
                                    }
                                }
                            }
                            velocidadPromediadaPorTurno_A = (totalRegistrosPorRango_A == 0 ? 0 : acumuladoDistanciasPorRango_A / totalRegistrosPorRango_A); //velocidad promediada A
                            tiempo_A = totalRegistrosPorRango_A == 0 ? 0 : tiempo_A / totalRegistrosPorRango_A;

                            //LADO B RECORRIDO DE FILAS


                            for (int row_final = 11; row_final < hojaDespachoStadistics.EndRowIndex; row_final++)
                            {
                                //VALIDACION DE VACIOS EN PARADEROS
                                var Hora_ParaderoInicio_B = hojaDespacho.GetCellValueAsDateTime(row_final, PositionEncabezadoA_final + 1).ToString("HH:mm"); //6 es la posicion donde esta el lado A si la ruta es 107 +2
                                var Hora_Final_B = hojaDespacho.GetCellValueAsDateTime(row_final, PositionEncabezadoB_final).ToString("HH:mm"); //6 es la posicion donde esta el lado A

                                if (Hora_ParaderoInicio_B != "00:00" && Hora_Final_B != "00:00")
                                {
                                    var RowHora_Final_B = fechaTemporal + " " + Hora_Final_B;
                                    var RowHora_ParaderoInicio_B = fechaTemporal + " " + Hora_ParaderoInicio_B;
                                    var estaDentroDelRango = false;
                                    estaDentroDelRango = BetweenDatetimesTimestamp(ConvertToTimestamp(DateTime.Parse(RowHora_ParaderoInicio_B)).ToString(),
                                                                                    ConvertToTimestamp(DateTime.Parse(hInicio)).ToString(),
                                                                                    ConvertToTimestamp(DateTime.Parse(hFin)).ToString());
                                    //if (BetweenDatetimes(DateTime.Parse(RowHora_ParaderoInicio_B), DateTime.Parse(hInicio), DateTime.Parse(hFin)))
                                    if (estaDentroDelRango)
                                    {
                                        var horaInicioB = hojaDespacho.GetCellValueAsDateTime(row_final, PositionEncabezadoA_final + 1).ToString("HH:mm"); //6 es la posicion donde esta el lado A
                                        var horaSalidaB = hojaDespacho.GetCellValueAsDateTime(row_final, PositionEncabezadoB_final).ToString("HH:mm");
                                        timestampFechaHoraInicio = ConvertToTimestamp(DateTime.Parse(horaInicioB));
                                        timestampFechaHoraFin = ConvertToTimestamp(DateTime.Parse(horaSalidaB));

                                        var tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                                        if (tiempoViajeTimestamp < 0)
                                        {
                                            tiempoViajeTimestamp = 0;
                                            timestampFechaHoraFin = timestampFechaHoraFin + 86400; //le agrega un dia 
                                            tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                                        }
                                        double tiempoViajeminutos = (tiempoViajeTimestamp / 60);
                                        double tiempoViajeHoras = tiempoViajeminutos / 60; //tiempo en horas
                                        double velocidadKM = (distanciaRecorrido[1] / tiempoViajeHoras);//velocidad x tiempo  [0] = LADO A , [1] = LADO B

                                        if (tiempoViajeminutos <= menorTiempoViajeMinutosAB[1] && tiempoViajeminutos >= 30) //mayor igual a 30 minutos
                                        { //si no sobrepasa el menor tiempo del viaje A + 2horas entonces entra a la validacion
                                            totalRegistrosPorRango_B++; //aumenta la cantidad de registros para luego hacer el promediado
                                            acumuladoDistanciasPorRango_B += velocidadKM;
                                            tiempo_B += tiempoViajeHoras;
                                        }
                                    }
                                }
                            }
                            velocidadPromediadaPorTurno_B = totalRegistrosPorRango_B == 0 ? 0 : acumuladoDistanciasPorRango_B / totalRegistrosPorRango_B; //velocidad promediada A
                            tiempo_B = totalRegistrosPorRango_B == 0 ? 0 : tiempo_B / totalRegistrosPorRango_B;

                            var encuentraTurnoAM = arrdataHoras[0].IndexOf("AM");
                            var encuentraTurnoPM = arrdataHoras[0].IndexOf("PM");
                            if (encuentraTurnoAM != -1)
                            {
                                turno = "MAÑANA";
                            }

                            if (encuentraTurnoPM != -1)
                            {
                                turno = "TARDE";
                            }
                            //
                            e = _registroPicoPlacaLN.registrarVelocidadPPlaca(idRuta, 1, turno, fecha, arrdataHoras[0], arrdataHoras[1], velocidadPromediadaPorTurno_A, velocidadPromediadaPorTurno_B, distanciaRecorrido[0], distanciaRecorrido[1], tiempo_A, tiempo_B, "", Session["user_login"].ToString(), ref mensaje, ref tipo); //aqui registra
                                                                                                                                                                                                                                                                                                                //
                            if (e.COD_ESTADO == 1)
                            {
                                cantidadRegistrosGuardados++;
                            }
                            else
                            {
                                cantidadRegistrosFallidos++;
                            }
                            //reseteando los valores
                            totalRegistrosPorRango_A = 0; //para la division del promediado
                            acumuladoDistanciasPorRango_A = 0.0; //acumulado para el promediado
                            velocidadPromediadaPorTurno_A = 0.0; //velocidad Promediada
                                                                 //
                            totalRegistrosPorRango_B = 0; //para la division del promediado
                            acumuladoDistanciasPorRango_B = 0.0; //acumulado para el promediado
                            velocidadPromediadaPorTurno_B = 0.0; //velocidad Promediada

                        }
                        scope.Complete();

                    }
                    catch (TransactionAbortedException ex)
                    {
                        e.DES_ESTADO = "No se registraron los datos, ERROR->" + ex.Message;
                    }

                }
                e.DES_ESTADO = (cantidadRegistrosGuardados > 0 ? cantidadRegistrosGuardados + " registros guardados registrados exitosamente." : " Ocurrió un error no se han guardado los registros." + "->" + mensajeError);

            }

            catch (Exception ex)
            {
                e.COD_ESTADO = 0;
                e.DES_ESTADO = "No se pudo insertar los registros, ERROR->" + ex.Message;
            }
            return JsonConvert.SerializeObject(e);

        }
        public string importarExcelDespachos(int idRuta, string fecha, int reemplazar, string abrevProveedorSistemasCorredor)
        {
            RegistroPicoPlacaLN _registroPicoPlacaLN = new RegistroPicoPlacaLN();
            String mensaje = "";
            Int32 tipo = 0;
            RPTA_GENERAL e = new RPTA_GENERAL();
            e = verificarRegistroPicoPlacaExistente(fecha, idRuta);

            if (e.COD_ESTADO == 1 && reemplazar == 0)
            {
                e.COD_ESTADO = 3;
                return JsonConvert.SerializeObject(e);
            }

            if (reemplazar == 1)
            { //aceptó reemplazar la data
                e = _registroPicoPlacaLN.anularRegistrosPorFecha(fecha, idRuta, ref mensaje, ref tipo);
                if (e.COD_ESTADO == 0)
                { //valida si ocurrio un error al momento de anular los registros
                    return JsonConvert.SerializeObject(e);
                }
            }

            var ob = Request.Files;
            var archivoSubido = Request.Files[0];
            var nombreArchivo = Path.GetFileName(archivoSubido.FileName);
            var extensionArchivo = Path.GetExtension(archivoSubido.FileName);
            var arrFileName = nombreArchivo.Split('.');
            var nuevoNombreArchivoExcel = arrFileName[0] + "_FILE_" + DateTime.Now.ToString("dd_MM_yyyy_h_mm_ss") + extensionArchivo;
            var pathArchivo = Server.MapPath("~/Adjuntos/Files_Despacho/" + nuevoNombreArchivoExcel);
            string pathFinal = Path.Combine(pathArchivo);
            archivoSubido.SaveAs(pathFinal);

            var rpta = "";
            switch (abrevProveedorSistemasCorredor)
            {
                case "TGA":
                    rpta = importarExcelDespachosABEXA(idRuta, fecha, pathFinal);
                    break;
                case "SJL":
                    rpta = importarExcelDespachosABEXA(idRuta, fecha, pathFinal);
                    break;
                case "CC":
                    rpta = importarExcelDespachosABEXA(idRuta, fecha, pathFinal);
                    break;
                case "PN":
                    rpta = importarExcelDespachosHACOM(idRuta, fecha, pathFinal);
                    break;
                case "JP":
                    rpta = importarExcelDespachosHACOM(idRuta, fecha, pathFinal);
                    break;
                default:
                    rpta = "ERROR LLAMAR AL ADMINISTRADOR DEL SISTEMA";
                    break;
            }

            return rpta;
        }
        public string importarExcelDespachosABEXA(int idRuta, string fecha, string pathArchivo)
        {
            RegistroPicoPlacaLN _registroPicoPlacaLN = new RegistroPicoPlacaLN();
            String mensaje = "";
            Int32 tipo = 0;
            RPTA_GENERAL e = new RPTA_GENERAL();
            /*PROCESO PARA LEER EXCEL*/
            CC_RECORRIDO recorrido = new CC_RECORRIDO();

            e.COD_ESTADO = 1;
            e.DES_ESTADO = "";
            //Leyendo excel
            var pathArchivoDataDespacho = pathArchivo;
            try
            {
                using (FileStream fs = new FileStream(pathArchivoDataDespacho, FileMode.Open))
                {
                    SLDocument xlDoc = new SLDocument(fs);
                    var hojasExcel = xlDoc.GetSheetNames();
                    SLDocument hojaDespacho = new SLDocument(fs, hojasExcel[0]);//leyendo la hoja 
                    SLWorksheetStatistics hojaDespachoStadistics = xlDoc.GetWorksheetStatistics();
                    string fechaTemporal = DateTime.Now.ToString("dd/MM/yyyy");
                    string[] lados = { "A", "B" };
                    double[] distanciaRecorrido = { 0.0, 0.0 };
                    recorrido = _registroPicoPlacaLN.getKmPorLadoYRecorrido(idRuta, lados[0], ref mensaje, ref tipo); //LADO A
                    distanciaRecorrido[0] = recorrido.MEDIDA_KM;
                    recorrido = _registroPicoPlacaLN.getKmPorLadoYRecorrido(idRuta, lados[1], ref mensaje, ref tipo); //LADO B
                    distanciaRecorrido[1] = recorrido.MEDIDA_KM;
                    //
                    string[] rangoHorario = {
                                                "04:00:00 AM-04:29:00 AM", "04:30:00 AM-04:59:00 AM",
                                                "05:00:00 AM-05:29:00 AM", "05:30:00 AM-05:59:00 AM",
                                                "06:00:00 AM-06:29:00 AM", "06:30:00 AM-06:59:00 AM",
                                                "07:00:00 AM-07:29:00 AM", "07:30:00 AM-07:59:00 AM",
                                                "08:00:00 AM-08:29:00 AM", "08:30:00 AM-08:59:00 AM",
                                                "09:00:00 AM-09:29:00 AM", "09:30:00 AM-09:59:00 AM",
                                                "10:00:00 AM-10:29:00 AM", "10:30:00 AM-10:59:00 AM",
                                                "11:00:00 AM-11:29:00 AM", "11:30:00 AM-11:59:00 AM",
                                                "12:00:00 PM-12:29:00 PM", "12:30:00 PM-12:59:00 PM",
                                                "01:00:00 PM-01:29:00 PM", "01:30:00 PM-01:59:00 PM",
                                                "02:00:00 PM-02:29:00 PM", "02:30:00 PM-02:59:00 PM",
                                                "03:00:00 PM-03:29:00 PM", "03:30:00 PM-03:59:00 PM",
                                                "04:00:00 PM-04:29:00 PM", "04:30:00 PM-04:59:00 PM",
                                                "05:00:00 PM-05:29:00 PM", "05:30:00 PM-05:59:00 PM",
                                                "06:00:00 PM-06:29:00 PM", "06:30:00 PM-06:59:00 PM",
                                                "07:00:00 PM-07:29:00 PM", "07:30:00 PM-07:59:00 PM",
                                                "08:00:00 PM-08:29:00 PM", "08:30:00 PM-08:59:00 PM",
                                                "09:00:00 PM-09:29:00 PM", "09:30:00 PM-09:59:00 PM",
                                                "10:00:00 PM-10:29:00 PM", "10:30:00 PM-10:59:00 PM",
                                                "11:00:00 PM-11:29:00 PM", "11:30:00 PM-11:59:00 PM"
                                            };
                    //obtiendo columna del lado A 
                    var nroColumnaLlegadaSentidoA = 0;
                    var nroColumnaLlegadaSentidoB = 0;
                    //
                    for (int col = 1; col < hojaDespachoStadistics.EndColumnIndex; col++) //solo para obtener posicion de las cabeceras
                    {
                        var textoColumna = hojaDespacho.GetCellValueAsString(7, col).ToString();

                        switch (textoColumna)
                        {
                            case "Llegada":
                                nroColumnaLlegadaSentidoA = idRuta == 3 ? col - 1 : col;
                                break;
                            case "Llegada B":
                                nroColumnaLlegadaSentidoB = idRuta == 3 ? col - 1 : col;
                                break;
                            default:
                                break;
                        }
                        //var horaSalida = hojaDespacho.GetCellValueAsString(row, 6).ToString();
                    }
                    //velocidad Promediada
                    var totalRegistrosPorRango_B = 0; //para la division del promediado
                    var acumuladoDistanciasPorRango_B = 0.0; //acumulado para el promediado
                    //velocidad Promediada
                    var cantidadRegistrosGuardados = 0;
                    var cantidadRegistrosFallidos = 0;
                    //recorriendo horarios
                    var auxRangos = 0;
                    var mensajeError = "";
                    var fechaTiempoRegistroExcel = "";
                    string[] arrFechaTiempoRegistroExcel = new string[2];
                    //
                    double[] menorTiempoViajeMinutosAB = obtenerMenorHoraDeViajeEnMinutosABEXA(hojaDespacho, nroColumnaLlegadaSentidoA, nroColumnaLlegadaSentidoB, ref fechaTiempoRegistroExcel); //obtiene un arreglo con el menor tiempo viaje en A y B + 2 HORAS
                    if (fechaTiempoRegistroExcel == "")
                    {
                        e.COD_ESTADO = 0;
                        e.DES_ESTADO = "ERROR -> Revisar la data del excel.";
                        return JsonConvert.SerializeObject(e);
                    }
                    else
                    {
                        arrFechaTiempoRegistroExcel = fechaTiempoRegistroExcel.Split(' ');
                        fechaTiempoRegistroExcel = arrFechaTiempoRegistroExcel[0];

                        if (fechaTiempoRegistroExcel != fecha)
                        { //si la fecha del excel es igual a la fecha que ingreso el usuario
                            e.COD_ESTADO = 0;
                            e.DES_ESTADO = "ERROR -> La fecha no corresponde al de la información.";
                            return JsonConvert.SerializeObject(e);
                        }
                    }
                    //
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            foreach (var item in rangoHorario) //RECORRE HORARIOS
                            {
                                double velocidadPromediadaPorTurno_A = 0.0; //velocidad Promediada
                                var totalRegistrosPorRango_A = 0;
                                double acumuladoDistanciasPorRango_A = 0;
                                long tmstamp_promediado_A = 0;
                                long tmstamp_promediado_B = 0;
                                auxRangos++;
                                var arrdataHoras = item.Split('-');
                                var hInicio = fechaTemporal + " " + arrdataHoras[0].ToUpper();
                                var hFin = fechaTemporal + " " + arrdataHoras[1].ToUpper();



                                double tiempo_promedio_A = 0;
                                double tiempo_promedio_B = 0;
                                var velocidadPromediadaPorTurno_B = 0.0;
                                var turno = "";

                                for (int row = 9; row < hojaDespachoStadistics.EndRowIndex; row++) //9 es la posicicion donde comienza la data 
                                {
                                    var estadoViaje = hojaDespacho.GetCellValueAsString(row, 11).ToString(); //texto Estado viaje
                                    var viajeLado = hojaDespacho.GetCellValueAsString(row, 5).ToString(); //texto LADO
                                    var textoServicio = hojaDespacho.GetCellValueAsString(row, 7).ToString(); //texto LADO
                                    var rowFechaHoraSalida = hojaDespacho.GetCellValueAsString(row, 6).ToString(); //6 es la posicion columna donde esta el lado A
                                    var posicionColumnaLlegada = (viajeLado == "A" ? nroColumnaLlegadaSentidoA + 2 : nroColumnaLlegadaSentidoB + 2);
                                    var rowFechaHoraLlegada = hojaDespacho.GetCellValueAsString(row, posicionColumnaLlegada).ToString();
                                    //
                                    long timestampFechaHoraSalida = 0;
                                    long timestampFechaHoraLlegada = 0;
                                    //
                                    var arrFechaHoraSalida = hojaDespacho.GetCellValueAsDateTime(row, 6).ToString().Split(' '); //6 es la posicion columna donde esta el lado A
                                    var arrFechaHoraLlegada = hojaDespacho.GetCellValueAsDateTime(row, posicionColumnaLlegada).ToString().Split(' '); //6 es la posicion columna donde esta el lado A

                                    if (rowFechaHoraSalida != "" && rowFechaHoraLlegada != "" && estadoViaje == "EJECUTADO")
                                    { //si la hora de salida y la hora de llegada tienen datos
                                        rowFechaHoraSalida = fechaTemporal + " " + arrFechaHoraSalida[1] + " " + arrFechaHoraSalida[2];
                                        rowFechaHoraLlegada = fechaTemporal + " " + arrFechaHoraLlegada[1] + " " + arrFechaHoraLlegada[2];

                                        if (viajeLado == "A")
                                        {
                                            var estaDentroDelrango = false;
                                            //estaDentroDelrango = BetweenDatetimes(  DateTime.ParseExact(rowFechaHoraSalida, "dd/MM/yyyy HH:mm:ss tt", CultureInfo.InvariantCulture), 
                                            //                                        DateTime.ParseExact(hInicio, "dd/MM/yyyy HH:mm:ss tt", CultureInfo.InvariantCulture),
                                            //                                        DateTime.ParseExact(hFin, "dd/MM/yyyy HH:mm:ss tt", CultureInfo.InvariantCulture));
                                            timestampFechaHoraSalida = ConvertToTimestamp(DateTime.Parse(rowFechaHoraSalida));
                                            timestampFechaHoraLlegada = ConvertToTimestamp(DateTime.Parse(rowFechaHoraLlegada));

                                            estaDentroDelrango = BetweenDatetimesTimestamp(timestampFechaHoraSalida.ToString(),
                                                                                                  ConvertToTimestamp(DateTime.Parse(hInicio)).ToString(),
                                                                                                  ConvertToTimestamp(DateTime.Parse(hFin)).ToString());

                                            if (item == "11:00:00 PM-11:29:00 PM")
                                            {
                                                var x = 0;
                                            }

                                            if (estaDentroDelrango)
                                            { //si esta dentro del rango del turno
                                                var tiempoViajeTimestamp = timestampFechaHoraLlegada - timestampFechaHoraSalida;
                                                if (tiempoViajeTimestamp < 0)
                                                {
                                                    tiempoViajeTimestamp = 0;
                                                    timestampFechaHoraLlegada = timestampFechaHoraLlegada + 86400; //le agrega un dia 
                                                    tiempoViajeTimestamp = timestampFechaHoraLlegada - timestampFechaHoraSalida;
                                                }

                                                double tiempoViajeMinutos = (tiempoViajeTimestamp / 60); // en minutos
                                                double tiempoViajeHoras = tiempoViajeMinutos / 60; //tiempo en horas
                                                double velocidadKM = (distanciaRecorrido[0] / tiempoViajeHoras);//velocidad x tiempo  [0] = LADO A , [1] = LADO B

                                                if (tiempoViajeMinutos <= menorTiempoViajeMinutosAB[0] && tiempoViajeMinutos >= 30) //mayor igual a 30 minutos
                                                { //si no sobrepasa el menor tiempo del viaje A + 2horas entonces entra a la validacion
                                                    totalRegistrosPorRango_A++; //aumenta la cantidad de registros para luego hacer el promediado
                                                    acumuladoDistanciasPorRango_A += velocidadKM;
                                                    tmstamp_promediado_A += tiempoViajeTimestamp;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var estaDentroDelrango = false;
                                            timestampFechaHoraSalida = ConvertToTimestamp(DateTime.Parse(rowFechaHoraSalida));
                                            timestampFechaHoraLlegada = ConvertToTimestamp(DateTime.Parse(rowFechaHoraLlegada));
                                            estaDentroDelrango = BetweenDatetimesTimestamp(timestampFechaHoraSalida.ToString(),
                                                                                            ConvertToTimestamp(DateTime.Parse(hInicio)).ToString(),
                                                                                            ConvertToTimestamp(DateTime.Parse(hFin)).ToString());

                                            if (estaDentroDelrango)
                                            { //si esta dentro del rango del turno

                                                var tiempoViajeTimestamp = timestampFechaHoraLlegada - timestampFechaHoraSalida;
                                                if (tiempoViajeTimestamp < 0)
                                                {
                                                    tiempoViajeTimestamp = 0;
                                                    timestampFechaHoraLlegada = timestampFechaHoraLlegada + 86400; //le agrega un dia 
                                                    tiempoViajeTimestamp = timestampFechaHoraLlegada - timestampFechaHoraSalida;
                                                }

                                                double tiempoViajeMinutos = (tiempoViajeTimestamp / 60); // en minutos
                                                double tiempoViajeHoras = (tiempoViajeMinutos / 60);
                                                double velocidadKM = (distanciaRecorrido[1] / tiempoViajeHoras);//velocidad x tiempo  [0] = LADO A , [1] = LADO B

                                                if (tiempoViajeMinutos <= menorTiempoViajeMinutosAB[1] && tiempoViajeMinutos >= 30) //mayor igual a 30 minutos
                                                { //si no sobrepasa el menor tiempo del viaje A + 2horas entonces entra a la validacion
                                                    totalRegistrosPorRango_B++; //aumenta la cantidad de registros para luego hacer el promediado
                                                    acumuladoDistanciasPorRango_B += velocidadKM;
                                                    tmstamp_promediado_B += tiempoViajeTimestamp;
                                                }

                                            }
                                        }
                                    }
                                }
                                velocidadPromediadaPorTurno_A = totalRegistrosPorRango_A == 0 ? 0 : (acumuladoDistanciasPorRango_A / totalRegistrosPorRango_A); //velocidad promediada A
                                velocidadPromediadaPorTurno_B = totalRegistrosPorRango_B == 0 ? 0 : (acumuladoDistanciasPorRango_B / totalRegistrosPorRango_B); //velocidad promediada B
                                var encuentraTurnoAM = arrdataHoras[0].IndexOf("AM");
                                var encuentraTurnoPM = arrdataHoras[0].IndexOf("PM");

                                if (encuentraTurnoAM != -1)
                                {
                                    turno = "MAÑANA";
                                }

                                if (encuentraTurnoPM != -1)
                                {
                                    turno = "TARDE";
                                }

                                //formateando el tiempo promedio
                                tiempo_promedio_A = Double.Parse(tmstamp_promediado_A.ToString()) / 60; //tiempo en minutos lado A
                                tiempo_promedio_A = tiempo_promedio_A / 60;//tiempo en horas lado A
                                tiempo_promedio_A = totalRegistrosPorRango_A == 0 ? 0 : tiempo_promedio_A / totalRegistrosPorRango_A;
                                //
                                tiempo_promedio_B = Double.Parse(tmstamp_promediado_B.ToString()) / 60; //tiempo en minutos lado B
                                tiempo_promedio_B = tiempo_promedio_B / 60; //tiempo en horas lado B
                                tiempo_promedio_B = totalRegistrosPorRango_B == 0 ? 0 : tiempo_promedio_B / totalRegistrosPorRango_B;
                                //string path = Server.MapPath("~/Download/prueba.txt");
                                //string createText = total + Environment.NewLine;


                                //e = _registroPicoPlacaLN.registrarVelocidadPPlaca(idRuta, 1, turno, fecha, arrdataHoras[0], arrdataHoras[1], velocidadPromediadaPorTurno_A, velocidadPromediadaPorTurno_B, 
                                //                                                        distanciaRecorrido[0], distanciaRecorrido[1], tiempo_promedio_A, tiempo_promedio_B, "", Session["user_login"].ToString()); //aqui registra
                                e = _registroPicoPlacaLN.registrarVelocidadPPlaca(idRuta, 1, turno, fecha, arrdataHoras[0], arrdataHoras[1], velocidadPromediadaPorTurno_A, velocidadPromediadaPorTurno_B,
                                distanciaRecorrido[0], distanciaRecorrido[1], tiempo_promedio_A, tiempo_promedio_B, "", Session["user_login"].ToString(), ref mensaje, ref tipo); //aqui registra

                                //                                                                                                                                                                                                                                                                                 //
                                if (e.COD_ESTADO == 1)
                                {
                                    cantidadRegistrosGuardados++;
                                }
                                else
                                {
                                    mensajeError = e.DES_ESTADO;
                                    cantidadRegistrosFallidos++;
                                }
                                //reseteando los valores
                                totalRegistrosPorRango_A = 0; //para la division del promediado
                                acumuladoDistanciasPorRango_A = 0.0; //acumulado para el promediado
                                velocidadPromediadaPorTurno_A = 0.0; //velocidad Promediada
                                                                     //
                                totalRegistrosPorRango_B = 0; //para la division del promediado
                                acumuladoDistanciasPorRango_B = 0.0; //acumulado para el promediado
                                velocidadPromediadaPorTurno_B = 0.0; //velocidad Promediada
                                                                     //
                                tiempo_promedio_A = 0;
                                tiempo_promedio_B = 0;
                                tmstamp_promediado_A = 0;
                                tmstamp_promediado_B = 0;
                            }
                            scope.Complete();
                            //System.IO.File.WriteAllText(path, createText);
                        }
                        catch (TransactionAbortedException ex)
                        {
                            e.COD_ESTADO = 0;
                            e.DES_ESTADO = "(CODIGO 1) ERROR " + ex.Message;
                        }
                    }
                    e.DES_ESTADO = (cantidadRegistrosGuardados > 0 ? cantidadRegistrosGuardados + " registros guardados registrados exitosamente." : "Ocurrió un error no se han guardado los registros." + "->" + mensajeError);
                }
            }
            catch (Exception ex)
            {
                e.COD_ESTADO = 0;
                e.DES_ESTADO = "Ocurrió un error en la carga de data para la fecha ->" + fecha + " ERROR:{" + ex.Message + "}";
            }
            return JsonConvert.SerializeObject(e);
        }
        public string importarExcelDespachosHACOM(int idRuta, string fecha, string pathArchivoDataDespacho)
        {
            RegistroPicoPlacaLN _registroPicoPlacaLN = new RegistroPicoPlacaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var mensajeError = "";
            RPTA_GENERAL e = new RPTA_GENERAL();
            e.COD_ESTADO = 1;
            e.DES_ESTADO = "Correctamente";

            int ColumTotalKM_B = 0;

            //Leyendo excel
            try
            {
                using (FileStream fs = new FileStream(pathArchivoDataDespacho, FileMode.Open))
                {
                    SLDocument xlDoc = new SLDocument(fs);
                    CC_RECORRIDO recorrido = new CC_RECORRIDO();

                    var hojasExcel = xlDoc.GetSheetNames();
                    SLDocument hojaDespacho = new SLDocument(fs, hojasExcel[0]);//leyendo la hoja 
                    SLWorksheetStatistics hojaDespachoStadistics = xlDoc.GetWorksheetStatistics();

                    var nroColumnaInicioSentidoA = 0;
                    var nroColumnaFinSentidoB = 0;
                    int ColumTotalKM_A = 0;
                    var nroColumnaFinal_B = 0;
                    int position = 1;
                    double[] distanciaRecorrido = { 0.0, 0.0 };
                    string fechaTemporal = DateTime.Now.ToString("dd/MM/yyyy");
                    int posicionRowLADOS1 = 0;

                    var PositionEncabezado = 0;
                    string[] lados = { "A", "B" };
                    //string[] rangoHorario = { "06:00-07:00", "07:01-08:00", "08:01-09:00", "09:01-10:00", "17:00-18:00", "18:01-19:00", "19:01-20:00", "20:01-21:00" };
                    string[] rangoHorario = {
                                                "04:00:00 AM-04:29:00 AM", "04:30:00 AM-04:59:00 AM",
                                                "05:00:00 AM-05:29:00 AM", "05:30:00 AM-05:59:00 AM",
                                                "06:00:00 AM-06:29:00 AM", "06:30:00 AM-06:59:00 AM",
                                                "07:00:00 AM-07:29:00 AM", "07:30:00 AM-07:59:00 AM",
                                                "08:00:00 AM-08:29:00 AM", "08:30:00 AM-08:59:00 AM",
                                                "09:00:00 AM-09:29:00 AM", "09:30:00 AM-09:59:00 AM",
                                                "10:00:00 AM-10:29:00 AM", "10:30:00 AM-10:59:00 AM",
                                                "11:00:00 AM-11:29:00 AM", "11:30:00 AM-11:59:00 AM",
                                                "12:00:00 PM-12:29:00 PM", "12:30:00 PM-12:59:00 PM",
                                                "01:00:00 PM-01:29:00 PM", "01:30:00 PM-01:59:00 PM",
                                                "02:00:00 PM-02:29:00 PM", "02:30:00 PM-02:59:00 PM",
                                                "03:00:00 PM-03:29:00 PM", "03:30:00 PM-03:59:00 PM",
                                                "04:00:00 PM-04:29:00 PM", "04:30:00 PM-04:59:00 PM",
                                                "05:00:00 PM-05:29:00 PM", "05:30:00 PM-05:59:00 PM",
                                                "06:00:00 PM-06:29:00 PM", "06:30:00 PM-06:59:00 PM",
                                                "07:00:00 PM-07:29:00 PM", "07:30:00 PM-07:59:00 PM",
                                                "08:00:00 PM-08:29:00 PM", "08:30:00 PM-08:59:00 PM",
                                                "09:00:00 PM-09:29:00 PM", "09:30:00 PM-09:59:00 PM",
                                                "10:00:00 PM-10:29:00 PM", "10:30:00 PM-10:59:00 PM",
                                                "11:00:00 PM-11:29:00 PM", "11:30:00 PM-11:59:00 PM"
                                            };

                    recorrido = _registroPicoPlacaLN.getKmPorLadoYRecorrido(idRuta, lados[0], ref mensaje, ref tipo); //LADO A
                    distanciaRecorrido[0] = recorrido.MEDIDA_KM;
                    recorrido = _registroPicoPlacaLN.getKmPorLadoYRecorrido(idRuta, lados[1], ref mensaje, ref tipo); //LADO B
                    distanciaRecorrido[1] = recorrido.MEDIDA_KM;

                    int[] posicionRowLADOS = { 0, 0 };
                    int cantidadRegistros = 0;
                    for (int col = 1; col < hojaDespachoStadistics.EndColumnIndex + 1; col++) //solo para obtener posicion de las cabeceras
                    {
                        for (int j = 1; j < hojaDespachoStadistics.EndRowIndex; j++)
                        {

                            var textoColumna_inicial = hojaDespacho.GetCellValueAsString(9, col).ToString();
                            var textoColumna_Final_A = hojaDespacho.GetCellValueAsString(9, col).ToString();
                            var textoColumna_Final_B = hojaDespacho.GetCellValueAsString(j, col).ToString();

                            if (j == 5)
                            {

                                var textoColumna_Fecha = hojaDespacho.GetCellValueAsString(5, 2);
                                //VALIDACION FECHA                            
                                string fecha_excel = Convert.ToDateTime(textoColumna_Fecha).ToString("dd/MM/yyyy");
                                if (fecha != fecha_excel)
                                {

                                    e.COD_ESTADO = 0;
                                    e.DES_ESTADO = "ERROR -> La fecha no corresponde al de la información.";
                                    return JsonConvert.SerializeObject(e);
                                }
                            }

                            //LADO A OBTENER POSICION
                            switch (textoColumna_inicial)
                            {
                                case "Hora Inicio":
                                    nroColumnaInicioSentidoA = col;
                                    break;
                                case "Hora Fin":
                                    nroColumnaFinSentidoB = col;
                                    break;
                            }

                            switch (textoColumna_Final_A)
                            {
                                case "Total KM":
                                    posicionRowLADOS1 = col; //1                             
                                    ColumTotalKM_A = (idRuta == 1 ? posicionRowLADOS1 - 3 : posicionRowLADOS1 - 1); //1 : ruta 107
                                    break;
                            }
                            //LADO B OBTENER POSICION
                            if (textoColumna_Final_B != textoColumna_Final_A)
                            {
                                switch (textoColumna_Final_B)
                                {
                                    case "Total KM":
                                        posicionRowLADOS1 = col; //1      
                                        ColumTotalKM_B = posicionRowLADOS1 - 1;

                                        break;
                                }
                            }

                            if (textoColumna_Final_B == "REPORTE MATRIZ DE EVENTOS - Despacho detallado")
                            {

                                PositionEncabezado = j;   //300

                            }

                        }
                    }


                    if (ColumTotalKM_B == 0)
                    {
                        var rutas = Rutas_ciclica(idRuta, fecha, hojaDespacho);

                        return rutas;
                    }



                    //para la division del promediado
                    //acumulado para el promediado
                    var totalRegistrosPorRango_B = 0; //para la division del promediado
                    var acumuladoDistanciasPorRango_B = 0.0; //acumulado para el promediado
                    var velocidadPromediadaPorTurno_B = 0.0; //velocidad Promediada
                    string turno = "";
                    var cantidadRegistrosGuardados = 0;
                    var cantidadRegistrosFallidos = 0;
                    var auxRangos = 0;

                    double[] menorTiempoViajeMinutosAB = obtenerMenorHoraDeViajeEnMinutosHACOM(hojaDespacho, idRuta);
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            foreach (var item in rangoHorario) //RECORRE HORARIOS
                            {
                                double velocidadPromediadaPorTurno_A = 0.0; //velocidad Promediada
                                var totalRegistrosPorRango_A = 0;
                                double acumuladoDistanciasPorRango_A = 0;
                                double tiempo_A = 0;
                                double tiempo_B = 0;
                                long timestampFechaHoraInicio = 0;
                                long timestampFechaHoraFin = 0;
                                auxRangos++;
                                var arrdataHoras = item.Split('-');
                                var hInicio = fechaTemporal + " " + arrdataHoras[0];
                                var hFin = fechaTemporal + " " + arrdataHoras[1];
                                //LADO A RECORRIDO DE FILAS
                                for (int row = 11; row < PositionEncabezado; row++) //9 es la posicicion donde comienza la data 
                                {
                                    //VALIDACION DE VACIOS EN PARADEROS
                                    var Hora_ParaderoInicio_A = hojaDespacho.GetCellValueAsDateTime(row, 8).ToString("HH:mm"); //6 es la posicion donde esta el lado A
                                    var Hora_Final_A = hojaDespacho.GetCellValueAsDateTime(row, ColumTotalKM_A).ToString("HH:mm"); //6 es la posicion donde esta el lado A


                                    if (Hora_ParaderoInicio_A != "00:00" && Hora_Final_A != "00:00")
                                    {
                                        var RowHora_Final_A = fechaTemporal + " " + Hora_Final_A;
                                        var RowHora_ParaderoInicio_A = fechaTemporal + " " + Hora_ParaderoInicio_A;
                                        var estaDentroDelRango = false;

                                        estaDentroDelRango = BetweenDatetimesTimestamp(ConvertToTimestamp(DateTime.Parse(RowHora_ParaderoInicio_A)).ToString(),
                                                                                        ConvertToTimestamp(DateTime.Parse(hInicio)).ToString(),
                                                                                        ConvertToTimestamp(DateTime.Parse(hFin)).ToString()
                                                                                       );
                                        //if (BetweenDatetimes(DateTime.Parse(RowHora_ParaderoInicio_A), DateTime.Parse(hInicio), DateTime.Parse(hFin)))
                                        if (estaDentroDelRango)
                                        {
                                            var horaInicio_A = hojaDespacho.GetCellValueAsDateTime(row, nroColumnaInicioSentidoA).ToString("HH:mm"); //6 es la posicion donde esta el lado A 
                                            var horaFin_A = hojaDespacho.GetCellValueAsDateTime(row, nroColumnaFinSentidoB).ToString("HH:mm"); //6 es la posicion donde esta el lado A 
                                            timestampFechaHoraInicio = ConvertToTimestamp(DateTime.Parse(horaInicio_A));
                                            timestampFechaHoraFin = ConvertToTimestamp(DateTime.Parse(horaFin_A));
                                            var tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                                            if (tiempoViajeTimestamp < 0)
                                            {
                                                tiempoViajeTimestamp = 0;
                                                timestampFechaHoraFin = timestampFechaHoraFin + 86400; //le agrega un dia 
                                                tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                                            }
                                            double tiempoViajeminutos = (tiempoViajeTimestamp / 60);
                                            double tiempoViajeHoras = tiempoViajeminutos / 60; //tiempo en horas
                                            double velocidadKM = (distanciaRecorrido[0] / tiempoViajeHoras);//velocidad x tiempo  [0] = LADO A , [1] = LADO B

                                            if (tiempoViajeminutos <= menorTiempoViajeMinutosAB[0] && tiempoViajeminutos >= 30) //viaje mayor igual a 30 minutos
                                            { //si no sobrepasa el menor tiempo del viaje A + 2horas entonces entra a la validacion
                                                totalRegistrosPorRango_A++; //aumenta la cantidad de registros para luego hacer el promediado
                                                acumuladoDistanciasPorRango_A += velocidadKM;
                                                tiempo_A += tiempoViajeHoras;
                                            }
                                        }
                                    }
                                }

                                velocidadPromediadaPorTurno_A = (totalRegistrosPorRango_A == 0 ? 0 : acumuladoDistanciasPorRango_A / totalRegistrosPorRango_A); //velocidad promediada A
                                tiempo_A = totalRegistrosPorRango_A == 0 ? 0 : tiempo_A / totalRegistrosPorRango_A;
                                //LADO B RECORRIDO DE FILAS
                                for (int row_final = PositionEncabezado + 3; row_final < hojaDespachoStadistics.EndRowIndex; row_final++)
                                {
                                    //VALIDACION DE VACIOS EN PARADEROS
                                    var Hora_ParaderoInicio_B = hojaDespacho.GetCellValueAsDateTime(row_final, (idRuta == 1 ? 11 : 8)).ToString("HH:mm"); //6 es la posicion donde esta el lado A si la ruta es 107 +2
                                    var Hora_Final_B = hojaDespacho.GetCellValueAsDateTime(row_final, ColumTotalKM_B).ToString("HH:mm"); //6 es la posicion donde esta el lado A

                                    if (Hora_ParaderoInicio_B != "00:00" && Hora_Final_B != "00:00")
                                    {
                                        var RowHora_Final_B = fechaTemporal + " " + Hora_Final_B;
                                        var RowHora_ParaderoInicio_B = fechaTemporal + " " + Hora_ParaderoInicio_B;
                                        var estaDentroDelRango = false;
                                        estaDentroDelRango = BetweenDatetimesTimestamp(ConvertToTimestamp(DateTime.Parse(RowHora_ParaderoInicio_B)).ToString(),
                                                                                        ConvertToTimestamp(DateTime.Parse(hInicio)).ToString(),
                                                                                        ConvertToTimestamp(DateTime.Parse(hFin)).ToString()
                                                                                      );
                                        //if (BetweenDatetimes(DateTime.Parse(RowHora_ParaderoInicio_B), DateTime.Parse(hInicio), DateTime.Parse(hFin)))
                                        if (estaDentroDelRango)
                                        {
                                            var horaInicioB = hojaDespacho.GetCellValueAsDateTime(row_final, 4).ToString("HH:mm"); //6 es la posicion donde esta el lado A
                                            var horaSalidaB = hojaDespacho.GetCellValueAsDateTime(row_final, 5).ToString("HH:mm");
                                            timestampFechaHoraInicio = ConvertToTimestamp(DateTime.Parse(horaInicioB));
                                            timestampFechaHoraFin = ConvertToTimestamp(DateTime.Parse(horaSalidaB));

                                            var tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                                            if (tiempoViajeTimestamp < 0)
                                            {
                                                tiempoViajeTimestamp = 0;
                                                timestampFechaHoraFin = timestampFechaHoraFin + 86400; //le agrega un dia 
                                                tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                                            }
                                            double tiempoViajeminutos = (tiempoViajeTimestamp / 60);
                                            double tiempoViajeHoras = tiempoViajeminutos / 60; //tiempo en horas
                                            double velocidadKM = (distanciaRecorrido[1] / tiempoViajeHoras);//velocidad x tiempo  [0] = LADO A , [1] = LADO B

                                            if (tiempoViajeminutos <= menorTiempoViajeMinutosAB[1] && tiempoViajeminutos >= 30) //mayor igual a 30 minutos
                                            { //si no sobrepasa el menor tiempo del viaje A + 2horas entonces entra a la validacion
                                                totalRegistrosPorRango_B++; //aumenta la cantidad de registros para luego hacer el promediado
                                                acumuladoDistanciasPorRango_B += velocidadKM;
                                                tiempo_B += tiempoViajeHoras;
                                            }
                                        }
                                    }
                                }
                                velocidadPromediadaPorTurno_B = totalRegistrosPorRango_B == 0 ? 0 : acumuladoDistanciasPorRango_B / totalRegistrosPorRango_B; //velocidad promediada A
                                tiempo_B = totalRegistrosPorRango_B == 0 ? 0 : tiempo_B / totalRegistrosPorRango_B;

                                var encuentraTurnoAM = arrdataHoras[0].IndexOf("AM");
                                var encuentraTurnoPM = arrdataHoras[0].IndexOf("PM");
                                if (encuentraTurnoAM != -1)
                                {
                                    turno = "MAÑANA";
                                }

                                if (encuentraTurnoPM != -1)
                                {
                                    turno = "TARDE";
                                }
                                //
                                e = _registroPicoPlacaLN.registrarVelocidadPPlaca(idRuta, 1, turno, fecha, arrdataHoras[0], arrdataHoras[1], velocidadPromediadaPorTurno_A, velocidadPromediadaPorTurno_B, distanciaRecorrido[0], distanciaRecorrido[1], tiempo_A, tiempo_B, "", Session["user_login"].ToString(), ref mensaje, ref tipo); //aqui registra
                                //
                                if (e.COD_ESTADO == 1)
                                {
                                    cantidadRegistrosGuardados++;
                                }
                                else
                                {
                                    cantidadRegistrosFallidos++;
                                }
                                //reseteando los valores
                                totalRegistrosPorRango_A = 0; //para la division del promediado
                                acumuladoDistanciasPorRango_A = 0.0; //acumulado para el promediado
                                velocidadPromediadaPorTurno_A = 0.0; //velocidad Promediada
                                                                     //
                                totalRegistrosPorRango_B = 0; //para la division del promediado
                                acumuladoDistanciasPorRango_B = 0.0; //acumulado para el promediado
                                velocidadPromediadaPorTurno_B = 0.0; //velocidad Promediada


                            }
                            scope.Complete();
                        }
                        catch (TransactionAbortedException ex)
                        {
                            e.DES_ESTADO = "No se registraron los datos, ERROR->" + ex.Message;
                        }
                    }
                    e.DES_ESTADO = (cantidadRegistrosGuardados > 0 ? cantidadRegistrosGuardados + " registros guardados registrados exitosamente." : " Ocurrió un error no se han guardado los registros." + "->" + mensajeError);
                }
            }
            catch (Exception ex)
            {
                e.COD_ESTADO = 0;
                e.DES_ESTADO = "No se pudo insertar los registros, ERROR->" + ex.Message;
            }
            return JsonConvert.SerializeObject(e);
        }

        //public string importarExcelDespachosHACOM_FORMATO_NUEVO(int idRuta, string fecha, string pathArchivoDataDespacho)
        //{
        //    var mensajeError = "";
        //    RPTA_GENERAL e = new RPTA_GENERAL();
        //    e.COD_ESTADO = 1;
        //    e.DES_ESTADO = "Correctamente";

        //    int ColumTotalKM_B = 0;

        //    //Leyendo excel
        //    try
        //    {
        //        using (FileStream fs = new FileStream(pathArchivoDataDespacho, FileMode.Open))
        //        {
        //            SLDocument xlDoc = new SLDocument(fs);
        //            CC_RECORRIDO recorrido = new CC_RECORRIDO();

        //            var hojasExcel = xlDoc.GetSheetNames();
        //            SLDocument hojaDespacho = new SLDocument(fs, hojasExcel[0]);//leyendo la hoja 
        //            SLWorksheetStatistics hojaDespachoStadistics = xlDoc.GetWorksheetStatistics();

        //            var nroColumnaInicioSentidoA = 0;
        //            var nroColumnaFinSentidoB = 0;
        //            int ColumTotalKM_A = 0;
        //            var nroColumnaFinal_B = 0;
        //            int position = 1;
        //            double[] distanciaRecorrido = { 0.0, 0.0 };
        //            string fechaTemporal = DateTime.Now.ToString("dd/MM/yyyy");
        //            int posicionRowLADOS1 = 0;

        //            var PositionEncabezado = 0;
        //            string[] lados = { "A", "B" };
        //            //string[] rangoHorario = { "06:00-07:00", "07:01-08:00", "08:01-09:00", "09:01-10:00", "17:00-18:00", "18:01-19:00", "19:01-20:00", "20:01-21:00" };
        //            string[] rangoHorario = {
        //                                        "04:00:00 AM-04:29:00 AM", "04:30:00 AM-04:59:00 AM",
        //                                        "05:00:00 AM-05:29:00 AM", "05:30:00 AM-05:59:00 AM",
        //                                        "06:00:00 AM-06:29:00 AM", "06:30:00 AM-06:59:00 AM",
        //                                        "07:00:00 AM-07:29:00 AM", "07:30:00 AM-07:59:00 AM",
        //                                        "08:00:00 AM-08:29:00 AM", "08:30:00 AM-08:59:00 AM",
        //                                        "09:00:00 AM-09:29:00 AM", "09:30:00 AM-09:59:00 AM",
        //                                        "10:00:00 AM-10:29:00 AM", "10:30:00 AM-10:59:00 AM",
        //                                        "11:00:00 AM-11:29:00 AM", "11:30:00 AM-11:59:00 AM",
        //                                        "12:00:00 PM-12:29:00 PM", "12:30:00 PM-12:59:00 PM",
        //                                        "01:00:00 PM-01:29:00 PM", "01:30:00 PM-01:59:00 PM",
        //                                        "02:00:00 PM-02:29:00 PM", "02:30:00 PM-02:59:00 PM",
        //                                        "03:00:00 PM-03:29:00 PM", "03:30:00 PM-03:59:00 PM",
        //                                        "04:00:00 PM-04:29:00 PM", "04:30:00 PM-04:59:00 PM",
        //                                        "05:00:00 PM-05:29:00 PM", "05:30:00 PM-05:59:00 PM",
        //                                        "06:00:00 PM-06:29:00 PM", "06:30:00 PM-06:59:00 PM",
        //                                        "07:00:00 PM-07:29:00 PM", "07:30:00 PM-07:59:00 PM",
        //                                        "08:00:00 PM-08:29:00 PM", "08:30:00 PM-08:59:00 PM",
        //                                        "09:00:00 PM-09:29:00 PM", "09:30:00 PM-09:59:00 PM",
        //                                        "10:00:00 PM-10:29:00 PM", "10:30:00 PM-10:59:00 PM",
        //                                        "11:00:00 PM-11:29:00 PM", "11:30:00 PM-11:59:00 PM"
        //                                    };

        //            recorrido = _registroPicoPlacaLN.getKmPorLadoYRecorrido(idRuta, lados[0]); //LADO A
        //            distanciaRecorrido[0] = recorrido.MEDIDA_KM;
        //            recorrido = _registroPicoPlacaLN.getKmPorLadoYRecorrido(idRuta, lados[1]); //LADO B
        //            distanciaRecorrido[1] = recorrido.MEDIDA_KM;

        //            int[] posicionRowLADOS = { 0, 0 };
        //            int cantidadRegistros = 0;
        //            for (int col = 1; col < hojaDespachoStadistics.EndColumnIndex + 1; col++) //solo para obtener posicion de las cabeceras
        //            {
        //                for (int j = 1; j < hojaDespachoStadistics.EndRowIndex; j++)
        //                {

        //                    var textoColumna_inicial = hojaDespacho.GetCellValueAsString(9, col).ToString();
        //                    var textoColumna_Final_A = hojaDespacho.GetCellValueAsString(9, col).ToString();
        //                    var textoColumna_Final_B = hojaDespacho.GetCellValueAsString(j, col).ToString();

        //                    if (j == 5)
        //                    {

        //                        var textoColumna_Fecha = hojaDespacho.GetCellValueAsString(5, 2);
        //                        //VALIDACION FECHA                            
        //                        string fecha_excel = Convert.ToDateTime(textoColumna_Fecha).ToString("dd/MM/yyyy");
        //                        if (fecha != fecha_excel)
        //                        {

        //                            e.COD_ESTADO = 0;
        //                            e.DES_ESTADO = "ERROR -> La fecha no corresponde al de la información.";
        //                            return JsonConvert.SerializeObject(e);
        //                        }
        //                    }

        //                    //LADO A OBTENER POSICION
        //                    switch (textoColumna_inicial)
        //                    {
        //                        case "Salida Terminal A":
        //                            nroColumnaInicioSentidoA = col;
        //                            break;
        //                        case "Llegada Terminal B":
        //                            nroColumnaFinSentidoB = col;
        //                            break;
        //                    }

        //                    switch (textoColumna_Final_A)
        //                    {
        //                        case "KM":
        //                            posicionRowLADOS1 = col; //1                             
        //                            ColumTotalKM_A = (idRuta == 1 ? posicionRowLADOS1 - 6 : posicionRowLADOS1 - 4); //1 : ruta 107
        //                            break;
        //                    }
        //                    //LADO B OBTENER POSICION
        //                    if (textoColumna_Final_B != textoColumna_Final_A)
        //                    {
        //                        switch (textoColumna_Final_B)
        //                        {
        //                            case "BA":
        //                                posicionRowLADOS1 = col; //1      
        //                                ColumTotalKM_B = (idRuta == 1 ? posicionRowLADOS1 - 5 : posicionRowLADOS1 - 4);
        //                                PositionEncabezado = j;
        //                                break;
        //                        }
        //                    }
        //                }
        //            }
        //            if (ColumTotalKM_B == 0)
        //            {
        //                var rutas = Rutas_ciclica(idRuta, fecha, hojaDespacho);
        //                return rutas;
        //            }



        //            //para la division del promediado
        //            //acumulado para el promediado
        //            var totalRegistrosPorRango_B = 0; //para la division del promediado
        //            var acumuladoDistanciasPorRango_B = 0.0; //acumulado para el promediado
        //            var velocidadPromediadaPorTurno_B = 0.0; //velocidad Promediada
        //            string turno = "";
        //            var cantidadRegistrosGuardados = 0;
        //            var cantidadRegistrosFallidos = 0;
        //            var auxRangos = 0;

        //            double[] menorTiempoViajeMinutosAB = obtenerMenorHoraDeViajeEnMinutosHACOM(hojaDespacho, idRuta);
        //            using (TransactionScope scope = new TransactionScope())
        //            {
        //                try
        //                {
        //                    foreach (var item in rangoHorario) //RECORRE HORARIOS
        //                    {
        //                        double velocidadPromediadaPorTurno_A = 0.0; //velocidad Promediada
        //                        var totalRegistrosPorRango_A = 0;
        //                        double acumuladoDistanciasPorRango_A = 0;
        //                        double tiempo_A = 0;
        //                        double tiempo_B = 0;
        //                        long timestampFechaHoraInicio = 0;
        //                        long timestampFechaHoraFin = 0;
        //                        auxRangos++;
        //                        var arrdataHoras = item.Split('-');
        //                        var hInicio = fechaTemporal + " " + arrdataHoras[0];
        //                        var hFin = fechaTemporal + " " + arrdataHoras[1];
        //                        //LADO A RECORRIDO DE FILAS
        //                        for (int row = 11; row < PositionEncabezado; row++) //9 es la posicicion donde comienza la data 
        //                        {
        //                            //VALIDACION DE VACIOS EN PARADEROS
        //                            var Hora_ParaderoInicio_A = hojaDespacho.GetCellValueAsDateTime(row, 13).ToString("HH:mm"); //6 es la posicion donde esta el lado A
        //                            var Hora_Final_A = hojaDespacho.GetCellValueAsDateTime(row, ColumTotalKM_A).ToString("HH:mm"); //6 es la posicion donde esta el lado A


        //                            if (Hora_ParaderoInicio_A != "00:00" && Hora_Final_A != "00:00")
        //                            {
        //                                var RowHora_Final_A = fechaTemporal + " " + Hora_Final_A;
        //                                var RowHora_ParaderoInicio_A = fechaTemporal + " " + Hora_ParaderoInicio_A;
        //                                var estaDentroDelRango = false;

        //                                estaDentroDelRango = BetweenDatetimesTimestamp(ConvertToTimestamp(DateTime.Parse(RowHora_ParaderoInicio_A)).ToString(),
        //                                                                                ConvertToTimestamp(DateTime.Parse(hInicio)).ToString(),
        //                                                                                ConvertToTimestamp(DateTime.Parse(hFin)).ToString()
        //                                                                               );
        //                                //if (BetweenDatetimes(DateTime.Parse(RowHora_ParaderoInicio_A), DateTime.Parse(hInicio), DateTime.Parse(hFin)))
        //                                if (estaDentroDelRango)
        //                                {
        //                                    timestampFechaHoraInicio = ConvertToTimestamp(DateTime.Parse(Hora_ParaderoInicio_A));
        //                                    timestampFechaHoraFin = ConvertToTimestamp(DateTime.Parse(Hora_Final_A));
        //                                    var tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
        //                                    if (tiempoViajeTimestamp < 0)
        //                                    {
        //                                        tiempoViajeTimestamp = 0;
        //                                        timestampFechaHoraFin = timestampFechaHoraFin + 86400; //le agrega un dia 
        //                                        tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
        //                                    }
        //                                    double tiempoViajeminutos = (tiempoViajeTimestamp / 60);
        //                                    double tiempoViajeHoras = tiempoViajeminutos / 60; //tiempo en horas
        //                                    double velocidadKM = (distanciaRecorrido[0] / tiempoViajeHoras);//velocidad x tiempo  [0] = LADO A , [1] = LADO B

        //                                    if (tiempoViajeminutos <= menorTiempoViajeMinutosAB[0] && tiempoViajeminutos >= 30) //viaje mayor igual a 30 minutos
        //                                    { //si no sobrepasa el menor tiempo del viaje A + 2horas entonces entra a la validacion
        //                                        totalRegistrosPorRango_A++; //aumenta la cantidad de registros para luego hacer el promediado
        //                                        acumuladoDistanciasPorRango_A += velocidadKM;
        //                                        tiempo_A += tiempoViajeHoras;
        //                                    }
        //                                }
        //                            }
        //                        }

        //                        velocidadPromediadaPorTurno_A = (totalRegistrosPorRango_A == 0 ? 0 : acumuladoDistanciasPorRango_A / totalRegistrosPorRango_A); //velocidad promediada A
        //                        tiempo_A = totalRegistrosPorRango_A == 0 ? 0 : tiempo_A / totalRegistrosPorRango_A;

        //                        //LADO B RECORRIDO DE FILAS

        //                        for (int row_final = PositionEncabezado + 1; row_final < hojaDespachoStadistics.EndRowIndex; row_final++)
        //                        {
        //                            //VALIDACION DE VACIOS EN PARADEROS
        //                            var Hora_ParaderoInicio_B = hojaDespacho.GetCellValueAsDateTime(row_final, (idRuta == 1 ? 15 : 13)).ToString("HH:mm");//Primer Padero es la posicion donde esta el lado A si la ruta es 107 +2
        //                            var Hora_Final_B = hojaDespacho.GetCellValueAsDateTime(row_final, ColumTotalKM_B).ToString("HH:mm"); //Ultimo paradero lado B

        //                            if (Hora_ParaderoInicio_B != "00:00" && Hora_Final_B != "00:00")
        //                            {
        //                                var RowHora_Final_B = fechaTemporal + " " + Hora_Final_B;
        //                                var RowHora_ParaderoInicio_B = fechaTemporal + " " + Hora_ParaderoInicio_B;
        //                                var estaDentroDelRango = false;
        //                                estaDentroDelRango = BetweenDatetimesTimestamp(ConvertToTimestamp(DateTime.Parse(RowHora_ParaderoInicio_B)).ToString(),
        //                                                                                ConvertToTimestamp(DateTime.Parse(hInicio)).ToString(),
        //                                                                                ConvertToTimestamp(DateTime.Parse(hFin)).ToString()
        //                                                                              );
        //                                //if (BetweenDatetimes(DateTime.Parse(RowHora_ParaderoInicio_B), DateTime.Parse(hInicio), DateTime.Parse(hFin)))
        //                                if (estaDentroDelRango)
        //                                {
        //                                    timestampFechaHoraInicio = ConvertToTimestamp(DateTime.Parse(Hora_ParaderoInicio_B));
        //                                    timestampFechaHoraFin = ConvertToTimestamp(DateTime.Parse(Hora_Final_B));

        //                                    var tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
        //                                    if (tiempoViajeTimestamp < 0)
        //                                    {
        //                                        tiempoViajeTimestamp = 0;
        //                                        timestampFechaHoraFin = timestampFechaHoraFin + 86400; //le agrega un dia 
        //                                        tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
        //                                    }
        //                                    double tiempoViajeminutos = (tiempoViajeTimestamp / 60);
        //                                    double tiempoViajeHoras = tiempoViajeminutos / 60; //tiempo en horas
        //                                    double velocidadKM = (distanciaRecorrido[1] / tiempoViajeHoras);//velocidad x tiempo  [0] = LADO A , [1] = LADO B

        //                                    if (tiempoViajeminutos <= menorTiempoViajeMinutosAB[1] && tiempoViajeminutos >= 30) //mayor igual a 30 minutos
        //                                    { //si no sobrepasa el menor tiempo del viaje A + 2horas entonces entra a la validacion
        //                                        totalRegistrosPorRango_B++; //aumenta la cantidad de registros para luego hacer el promediado
        //                                        acumuladoDistanciasPorRango_B += velocidadKM;
        //                                        tiempo_B += tiempoViajeHoras;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        velocidadPromediadaPorTurno_B = totalRegistrosPorRango_B == 0 ? 0 : acumuladoDistanciasPorRango_B / totalRegistrosPorRango_B; //velocidad promediada A
        //                        tiempo_B = totalRegistrosPorRango_B == 0 ? 0 : tiempo_B / totalRegistrosPorRango_B;

        //                        var encuentraTurnoAM = arrdataHoras[0].IndexOf("AM");
        //                        var encuentraTurnoPM = arrdataHoras[0].IndexOf("PM");
        //                        if (encuentraTurnoAM != -1)
        //                        {
        //                            turno = "MAÑANA";
        //                        }

        //                        if (encuentraTurnoPM != -1)
        //                        {
        //                            turno = "TARDE";
        //                        }
        //                        //
        //                        e = _registroPicoPlacaLN.registrarVelocidadPPlaca(idRuta, 1, turno, fecha, arrdataHoras[0], arrdataHoras[1], velocidadPromediadaPorTurno_A, velocidadPromediadaPorTurno_B, distanciaRecorrido[0], distanciaRecorrido[1], tiempo_A, tiempo_B, "", Session["user_login"].ToString()); //aqui registra
        //                        //
        //                        if (e.COD_ESTADO == 1)
        //                        {
        //                            cantidadRegistrosGuardados++;
        //                        }
        //                        else
        //                        {
        //                            cantidadRegistrosFallidos++;
        //                        }
        //                        //reseteando los valores
        //                        totalRegistrosPorRango_A = 0; //para la division del promediado
        //                        acumuladoDistanciasPorRango_A = 0.0; //acumulado para el promediado
        //                        velocidadPromediadaPorTurno_A = 0.0; //velocidad Promediada
        //                                                             //
        //                        totalRegistrosPorRango_B = 0; //para la division del promediado
        //                        acumuladoDistanciasPorRango_B = 0.0; //acumulado para el promediado
        //                        velocidadPromediadaPorTurno_B = 0.0; //velocidad Promediada


        //                    }
        //                    scope.Complete();
        //                }
        //                catch (TransactionAbortedException ex)
        //                {
        //                    e.DES_ESTADO = "No se registraron los datos, ERROR->" + ex.Message;
        //                }
        //            }
        //            e.DES_ESTADO = (cantidadRegistrosGuardados > 0 ? cantidadRegistrosGuardados + " registros guardados registrados exitosamente." : " Ocurrió un error no se han guardado los registros." + "->" + mensajeError);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        e.COD_ESTADO = 0;
        //        e.DES_ESTADO = "No se pudo insertar los registros, ERROR->" + ex.Message;
        //    }
        //    return JsonConvert.SerializeObject(e);
        //}
        public double[] obtenerMenorHoraDeViajeEnMinutosABEXA(SLDocument hojaXLSX, int finalColumnaLadoA, int finalColumnaLadoB, ref string fechaRegistroExcel)
        {
            var cantidadViajesLadoA = 0;
            var cantidadViajesLadoB = 0;
            var HORAS_MINUTOS = 60;
            SLWorksheetStatistics hojaDespachoStadistics = hojaXLSX.GetWorksheetStatistics();
            string fechaTemporal = DateTime.Now.ToString("dd/MM/yyyy");
            double[] viajesLadoA = new double[hojaDespachoStadistics.EndRowIndex];
            double[] viajesLadoB = new double[hojaDespachoStadistics.EndRowIndex];
            double[] rpta = new double[2];
            for (int row = 9; row < hojaDespachoStadistics.EndRowIndex; row++) //solo para obtener posicion de las cabeceras
            {
                var estadoViaje = hojaXLSX.GetCellValueAsString(row, 11).ToString(); //texto Estado viaje
                var viajeLado = hojaXLSX.GetCellValueAsString(row, 5).ToString(); //texto LADO
                var txtFecharow = ""; //texto FECHA
                if (row == 9)
                {
                    txtFecharow = hojaXLSX.GetCellValueAsDateTime(row, 2).ToString(); //texto fecha
                    fechaRegistroExcel = txtFecharow;
                }
                var textoServicio = hojaXLSX.GetCellValueAsString(row, 7).ToString(); //texto LADO
                var rowFechaHoraSalida = hojaXLSX.GetCellValueAsString(row, 6).ToString(); //6 es la posicion columna donde esta el lado A
                var posicionColumnaLlegada = (viajeLado == "A" ? finalColumnaLadoA + 2 : finalColumnaLadoB + 2);
                var rowFechaHoraLlegada = hojaXLSX.GetCellValueAsString(row, posicionColumnaLlegada).ToString();
                //
                long timestampFechaHoraSalida = 0;
                long timestampFechaHoraLlegada = 0;
                //
                var arrFechaHoraSalida = hojaXLSX.GetCellValueAsDateTime(row, 6).ToString().Split(' '); //6 es la posicion columna donde esta el lado A
                var arrFechaHoraLlegada = hojaXLSX.GetCellValueAsDateTime(row, posicionColumnaLlegada).ToString().Split(' '); //6 es la posicion columna donde esta el lado A
                //
                if (rowFechaHoraSalida != "" && rowFechaHoraLlegada != "" && estadoViaje == "EJECUTADO")
                { //si la hora de salida y la hora de llegada tienen datos
                    rowFechaHoraSalida = fechaTemporal + " " + arrFechaHoraSalida[1] + " " + arrFechaHoraSalida[2];
                    rowFechaHoraLlegada = fechaTemporal + " " + arrFechaHoraLlegada[1] + " " + arrFechaHoraLlegada[2];
                    //
                    timestampFechaHoraSalida = ConvertToTimestamp(DateTime.Parse(rowFechaHoraSalida));
                    timestampFechaHoraLlegada = ConvertToTimestamp(DateTime.Parse(rowFechaHoraLlegada));
                    var tiempoViajeTimestamp = timestampFechaHoraLlegada - timestampFechaHoraSalida;
                    if (tiempoViajeTimestamp < 0)
                    {
                        tiempoViajeTimestamp = 0;
                        timestampFechaHoraLlegada = timestampFechaHoraLlegada + 86400; //le agrega un dia 
                        tiempoViajeTimestamp = timestampFechaHoraLlegada - timestampFechaHoraSalida;
                    }

                    double tiempoViajeMinutos = (tiempoViajeTimestamp / 60); // en minutos
                    //
                    if (viajeLado == "A" && tiempoViajeMinutos >= 30) //mayor igual a 30 minutos
                    {
                        viajesLadoA[cantidadViajesLadoA] = tiempoViajeMinutos;
                        cantidadViajesLadoA++;
                    }

                    if (viajeLado == "B" && tiempoViajeMinutos >= 30) //mayor igual a 30 minutos
                    {
                        viajesLadoB[cantidadViajesLadoB] = tiempoViajeMinutos;
                        cantidadViajesLadoB++;
                    }
                }
            }

            viajesLadoA = viajesLadoA.Where(i => i != 0).ToArray();  //
            viajesLadoB = viajesLadoB.Where(i => i != 0).ToArray();  //
            //buscando el menor en el lado A y B
            rpta[0] = viajesLadoA.Min() + (HORAS_MINUTOS * 2.5); // A
            rpta[1] = viajesLadoB.Min() + (HORAS_MINUTOS * 2.5);//B
            return rpta;
        }
        public double[] obtenerMenorHoraDeViajeEnMinutosHACOM(SLDocument hojaXLSX, int idRuta)
        {
            SLWorksheetStatistics hojaDespachoStadistics = hojaXLSX.GetWorksheetStatistics();
            string fechaTemporal = DateTime.Now.ToString("dd/MM/yyyy");
            var nroColumnaInicioSentidoA = 0;
            var nroColumnaFinSentidoB = 0;
            var nroColumnaEncabezadoFinal = 0;
            var nroColumnaFinal_A = 0;
            var nroColumnaFinal_B = 0;
            int posicionRowLADOS = 1;
            int cantidadRegistros = 0;
            int position = 1;

            var PositionEncabezado = 0;
            double[] viajesLadoA = new double[hojaDespachoStadistics.EndRowIndex];
            double[] viajesLadoB = new double[hojaDespachoStadistics.EndRowIndex];
            var cantidadViajesLadoA = 0;
            int ColumTotalKM_A = 0;
            int ColumTotalKM_B = 0;
            int posicionRowLADOS1 = 0;
            var cantidadViajesLadoB = 0;
            long timestampFechaHoraInicio = 0;
            long timestampFechaHoraFin = 0;
            var HORAS_MINUTOS = 60;
            double[] rpta = new double[2];


            for (int col = 1; col < hojaDespachoStadistics.EndColumnIndex + 1; col++) //solo para obtener posicion de las cabeceras
            {
                for (int j = 1; j < hojaDespachoStadistics.EndRowIndex; j++)
                {
                    nroColumnaFinal_B = 0;

                    var textoColumna_inicial = hojaXLSX.GetCellValueAsString(9, col).ToString();
                    var textoColumna_Final_A = hojaXLSX.GetCellValueAsString(9, col).ToString();
                    var textoColumna_Final_B = hojaXLSX.GetCellValueAsString(j, col).ToString();


                    //LADO A OBTENER POSICION
                    switch (textoColumna_inicial)
                    {
                        case "Salida Terminal A":
                            nroColumnaInicioSentidoA = col;
                            break;
                        case "Llegada Terminal B":
                            nroColumnaFinSentidoB = col;
                            break;
                    }

                    switch (textoColumna_Final_A)
                    {
                        case "KM":
                            posicionRowLADOS1 = col; //1                             
                            ColumTotalKM_A = (idRuta == 1 ? posicionRowLADOS1 - 6 : posicionRowLADOS1 - 4); //1 : ruta 107
                            break;
                    }


                    //LADO B OBTENER POSICION
                    if (textoColumna_Final_B != textoColumna_Final_A)
                    {
                        switch (textoColumna_Final_B)
                        {
                            case "BA":
                                posicionRowLADOS1 = col; //1      
                                ColumTotalKM_B = (idRuta == 1 ? posicionRowLADOS1 - 5 : posicionRowLADOS1 - 4);
                                PositionEncabezado = j;   //300

                                break;
                        }
                    }
                }
            }

            //recorrdiendo lado A 
            for (int row = 11; row < PositionEncabezado; row++) //9 es la posicicion donde comienza la data 
            {
                var Hora_ParaderoInicio_A = hojaXLSX.GetCellValueAsDateTime(row, 13).ToString("HH:mm"); //primer paradero Terminal A es la posicion donde esta el lado A
                var Hora_Final_A = hojaXLSX.GetCellValueAsDateTime(row, ColumTotalKM_A).ToString("HH:mm"); //ultimo paradero es la posicion donde esta el lado A
                if (Hora_ParaderoInicio_A != "00:00" && Hora_Final_A != "00:00")
                {
                    timestampFechaHoraInicio = ConvertToTimestamp(DateTime.Parse(Hora_ParaderoInicio_A));
                    timestampFechaHoraFin = ConvertToTimestamp(DateTime.Parse(Hora_Final_A));
                    var tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                    if (tiempoViajeTimestamp < 0)
                    {
                        tiempoViajeTimestamp = 0;
                        timestampFechaHoraFin = timestampFechaHoraFin + 86400; //le agrega un dia 
                        tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                    }

                    double tiempoViajeMinutos = (tiempoViajeTimestamp / 60);
                    viajesLadoA[cantidadViajesLadoA] = tiempoViajeMinutos;
                    cantidadViajesLadoA++;
                }
            }

            //recorriendo lado B
            for (int row_final = PositionEncabezado + 1; row_final < hojaDespachoStadistics.EndRowIndex; row_final++)
            {
                var Hora_ParaderoInicio_B = hojaXLSX.GetCellValueAsDateTime(row_final, (idRuta == 1 ? 15 : 13)).ToString("HH:mm");  //Primer Padero es la posicion donde esta el lado A si la ruta es 107 +2
                var Hora_Final_B = hojaXLSX.GetCellValueAsDateTime(row_final, ColumTotalKM_B).ToString("HH:mm"); //Ultimo paradero lado B
                if (Hora_ParaderoInicio_B != "00:00" && Hora_Final_B != "00:00")
                {
                    timestampFechaHoraInicio = ConvertToTimestamp(DateTime.Parse(Hora_ParaderoInicio_B));
                    timestampFechaHoraFin = ConvertToTimestamp(DateTime.Parse(Hora_Final_B));
                    var tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                    if (tiempoViajeTimestamp < 0)
                    {
                        tiempoViajeTimestamp = 0;
                        timestampFechaHoraFin = timestampFechaHoraFin + 86400; //le agrega un dia 
                        tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                    }
                    double tiempoViajeMinutos = (tiempoViajeTimestamp / 60);
                    viajesLadoB[cantidadViajesLadoB] = tiempoViajeMinutos;
                    cantidadViajesLadoB++;
                }
            }
            viajesLadoA = viajesLadoA.Where(i => i != 0 && i >= 30).ToArray();  //
            viajesLadoB = viajesLadoB.Where(i => i != 0 && i >= 30).ToArray();  //
            //buscando el menor en el lado A y B
            rpta[0] = viajesLadoA.Min() + (HORAS_MINUTOS * 2.5); // A
            rpta[1] = viajesLadoB.Min() + (HORAS_MINUTOS * 2.5);//B
            return rpta;
        }
        public double[] obtenerMenorHoraDeViajeEnMinutosCiclicas(SLDocument hojaDespacho, int idRuta)
        {
            SLWorksheetStatistics hojaDespachoStadistics = hojaDespacho.GetWorksheetStatistics();
            string fechaTemporal = DateTime.Now.ToString("dd/MM/yyyy");
            var nroColumnaInicioSentidoA = 0;
            var nroColumnaFinSentidoB = 0;
            var nroColumnaEncabezadoFinal = 0;
            var nroColumnaFinal_A = 0;
            var nroColumnaFinal_B = 0;
            int posicionRowLADOS = 1;
            int cantidadRegistros = 0;
            int position = 1;

            var PositionEncabezado = 0;
            double[] viajesLadoA = new double[hojaDespachoStadistics.EndRowIndex];
            double[] viajesLadoB = new double[hojaDespachoStadistics.EndRowIndex];
            var cantidadViajesLadoA = 0;
            int ColumTotalKM_A = 0;
            int ColumTotalKM_B = 0;
            int posicionRowLADOS1 = 0;
            var cantidadViajesLadoB = 0;
            long timestampFechaHoraInicio = 0;
            long timestampFechaHoraFin = 0;
            var HORAS_MINUTOS = 60;
            double[] rpta = new double[2];
            var PositionEncabezadoA_final = 0;
            var positionFinB = 1;
            var PositionEncabezadoB_final = 0;


            for (int col = 1; col < hojaDespachoStadistics.EndColumnIndex + 1; col++) //solo para obtener posicion de las cabeceras
            {
                for (int j = 1; j < hojaDespachoStadistics.EndRowIndex; j++)
                {
                    var FilaColumnaFinal = hojaDespacho.GetCellValueAsString(9, col).ToString();
                    var FilaColumna_inicial_A = hojaDespacho.GetCellValueAsString(10, col).ToString();


                    //Saber el comienzo Del Lado B
                    if (FilaColumna_inicial_A.EndsWith("2") && positionFinB == 1)
                    {
                        PositionEncabezadoA_final = col - 1;   //300
                        positionFinB = 0;
                    }

                    switch (FilaColumnaFinal)
                    {
                        case "Total KM":
                            PositionEncabezadoB_final = col - 1; //1                             
                            break;
                    }

                }
            }

            //recorrdiendo lado A 
            for (int row = 11; row < hojaDespachoStadistics.EndRowIndex; row++) //9 es la posicicion donde comienza la data 
            {
                var Hora_ParaderoInicio_A = hojaDespacho.GetCellValueAsDateTime(row, 8).ToString("HH:mm"); //8 es la posicion donde empieza el lado A
                var Hora_Final_A = hojaDespacho.GetCellValueAsDateTime(row, PositionEncabezadoA_final).ToString("HH:mm"); //PositionEncabezadoA_final es la posicion donde acaba A
                if (Hora_ParaderoInicio_A != "00:00" && Hora_Final_A != "00:00")
                {
                    timestampFechaHoraInicio = ConvertToTimestamp(DateTime.Parse(Hora_ParaderoInicio_A));
                    timestampFechaHoraFin = ConvertToTimestamp(DateTime.Parse(Hora_Final_A));
                    var tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                    if (tiempoViajeTimestamp < 0)
                    {
                        tiempoViajeTimestamp = 0;
                        timestampFechaHoraFin = timestampFechaHoraFin + 86400; //le agrega un dia 
                        tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                    }

                    double tiempoViajeMinutos = (tiempoViajeTimestamp / 60);
                    viajesLadoA[cantidadViajesLadoA] = tiempoViajeMinutos;
                    cantidadViajesLadoA++;
                }
            }

            //recorriendo lado B
            for (int row_final = PositionEncabezado + 3; row_final < hojaDespachoStadistics.EndRowIndex; row_final++)
            {
                var Hora_ParaderoInicio_B = hojaDespacho.GetCellValueAsDateTime(row_final, PositionEncabezadoA_final + 1).ToString("HH:mm"); //6 es la posicion donde esta el lado A si la ruta es 107 +2
                var Hora_Final_B = hojaDespacho.GetCellValueAsDateTime(row_final, PositionEncabezadoB_final).ToString("HH:mm"); //6 es la posicion donde esta el lado A

                if (Hora_ParaderoInicio_B != "00:00" && Hora_Final_B != "00:00")
                {
                    var horaInicioB = hojaDespacho.GetCellValueAsDateTime(row_final, 4).ToString("HH:mm"); //6 es la posicion donde esta el lado A
                    var horaSalidaB = hojaDespacho.GetCellValueAsDateTime(row_final, 5).ToString("HH:mm");
                    timestampFechaHoraInicio = ConvertToTimestamp(DateTime.Parse(horaInicioB));
                    timestampFechaHoraFin = ConvertToTimestamp(DateTime.Parse(horaSalidaB));
                    var tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                    if (tiempoViajeTimestamp < 0)
                    {
                        tiempoViajeTimestamp = 0;
                        timestampFechaHoraFin = timestampFechaHoraFin + 86400; //le agrega un dia 
                        tiempoViajeTimestamp = timestampFechaHoraFin - timestampFechaHoraInicio;
                    }
                    double tiempoViajeMinutos = (tiempoViajeTimestamp / 60);
                    viajesLadoB[cantidadViajesLadoB] = tiempoViajeMinutos;
                    cantidadViajesLadoB++;
                }
            }
            viajesLadoA = viajesLadoA.Where(i => i != 0 && i >= 30).ToArray();  //
            viajesLadoB = viajesLadoB.Where(i => i != 0 && i >= 30).ToArray();  //
            //buscando el menor en el lado A y B
            rpta[0] = viajesLadoA.Min() + (HORAS_MINUTOS * 2.5); // A
            rpta[1] = viajesLadoB.Min() + (HORAS_MINUTOS * 2.5);//B
            return rpta;
        }
        public string getReportePicoPlacaByFechas(string fechaInicio, string fechaFin, int idruta)
        {
            RegistroPicoPlacaLN _registroPicoPlacaLN = new RegistroPicoPlacaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var result = JsonConvert.SerializeObject(_registroPicoPlacaLN.getReportePicoPlacaByFechas(fechaInicio, fechaFin, idruta, ref mensaje, ref tipo)); //para la lista principal
            return result;
        }
        public RPTA_GENERAL verificarRegistroPicoPlacaExistente(string fechaConsulta, int idRuta)
        {
            RegistroPicoPlacaLN _registroPicoPlacaLN = new RegistroPicoPlacaLN();
            String mensaje = "";
            Int32 tipo = 0;
            RPTA_GENERAL r = new RPTA_GENERAL();
            var data = _registroPicoPlacaLN.getReportePicoPlacaByFechas(fechaConsulta, fechaConsulta, idRuta, ref mensaje, ref tipo);
            if (data.Count > 0)
            {
                r.COD_ESTADO = 1;
                r.DES_ESTADO = "Ya se ha registrado información para esta fecha";
            }
            else
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = "No hay info en esta fecha";
            }
            return r;
        }
        public static bool BetweenDatetimes(DateTime input, DateTime date1, DateTime date2)
        {
            return (input > date1 && input < date2);
        }
        public static bool BetweenDatetimesTimestamp(string input, string date1, string date2)
        {
            return (Int64.Parse(input) >= Int64.Parse(date1) && Int64.Parse(input) <= Int64.Parse(date2));
        }
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static long ConvertToTimestamp(DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;
            return (long)elapsedTime.TotalSeconds;
        }
        private double getKmPorRecorrido(int idRuta, string LadoRecorrido)
        {
            var rpta = 0.0;
            return rpta;
        }
        public Reporte_ComparativoV Limpiar(Reporte_ComparativoV model_rp)
        {
            model_rp.suma_tiempo_A = 0.0;
            model_rp.suma_tiempo_B = 0.0;
            model_rp.suma_velocidad_A = 0.0;
            model_rp.suma_velocidad_B = 0.0;
            model_rp.total_tiempo_a = 0.0;
            model_rp.total_tiempo_b = 0.0;
            model_rp.contador_tiempoa = 0;
            model_rp.contador_tiempob = 0;
            model_rp.contador_velocidad_a = 0;
            model_rp.contador_velocidad_b = 0;
            model_rp.TOTAL_a_velocidad = 0;
            model_rp.TOTAL_b_velocidad = 0;
            return model_rp;
        }

    }
}