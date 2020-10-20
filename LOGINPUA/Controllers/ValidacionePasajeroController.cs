using AD;
using AD.EntidadesAD;
using ENTIDADES;
using LN.EntidadesLN;
using LOGINPUA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class ValidacionePasajeroController : Controller
    {
        // GET: ValidacionePasajero
        private Object bdConn;
        public ActionResult SubirValidacionPasaj()
        {
            CorredoresLN _CorredoresLN = new CorredoresLN();

            String mensaje = "";
            Int32 tipo = 0;
            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
            return View();
        }
        //public ActionResult ReporteValidacionPasajeros()
        //{
        //    CorredoresLN _CorredoresLN = new CorredoresLN();


        //    String mensaje = "";
        //    Int32 tipo = 0;
        //    var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
        //    ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);

        //    return View();
        //}

        //public ActionResult Calcular_Intervalos()
        //{
        //    CorredoresLN _CorredoresLN = new CorredoresLN();


        //    String mensaje = "";
        //    Int32 tipo = 0;
        //    var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
        //    ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);

        //    return View();
        //}

        //public string Calcular_franja(int id_ruta, string fechas, string lado)
        //{
        //    PasajeroValidacionLN _PasajeroValidacionLN = new PasajeroValidacionLN();

        //    String mensaje = "";
        //    Int32 tipo = 0;
        //    string fechaFin_cambiada = fechas.Replace(" ", "");
        //    fechaFin_cambiada = fechaFin_cambiada.Replace("-", "|");


        //    var rpta = _PasajeroValidacionLN.Calcular_franja(id_ruta, fechaFin_cambiada, lado, ref mensaje, ref tipo);
        //    return JsonConvert.SerializeObject(rpta);
        //}


       

        public string PorcentajeTiempo()
        {
            var time = Convert.ToInt32(Session["time_carga"]);
            time++;
            return JsonConvert.SerializeObject(time);
        }



        public string subir_archivo(string tipoCarga, int idRuta, string fecha, int reemplazar, string abrevProveedorSistemasCorredor)
        {
            int porcentaje = 0;
            Session["time_carga"] = 0;

            var rpta = "";

            var ob = Request.Files;
            var archivoSubido = Request.Files[0];
            var nombreArchivo = Path.GetFileName(archivoSubido.FileName);
            var extensionArchivo = Path.GetExtension(archivoSubido.FileName);
            var arrFileName = nombreArchivo.Split('.');
            var nuevoNombreArchivoExcel = arrFileName[0] + "_FILE_" + DateTime.Now.ToString("dd_MM_yyyy_h_mm_ss") + extensionArchivo;
            var pathArchivo = Server.MapPath("~/Adjuntos/" + nuevoNombreArchivoExcel);
            string pathFinal = Path.Combine(pathArchivo);
            archivoSubido.SaveAs(pathFinal);
            var pathArchivoextract = Server.MapPath("~/Adjuntos/Files_Despacho");

            switch (tipoCarga)
            {
                case "I":
                    var rpta_ = cargarExcelArchivo(idRuta, fecha, reemplazar, abrevProveedorSistemasCorredor, tipoCarga, pathArchivo, ref porcentaje);
                    return JsonConvert.SerializeObject(rpta_);

                case "M":
                    rpta = Carga_Masiva(idRuta, abrevProveedorSistemasCorredor, pathArchivo, pathFinal, pathArchivoextract, ref porcentaje);
                    break;
                default:
                    break;
            }
            return rpta;
        }

        public string Carga_Masiva(int idRuta, string abrevProveedorSistemasCorredor, string pathArchivo, string pathFinal, string pathArchivoextract, ref int porcentaje)
        {
            RPTA_GENERAL e = new RPTA_GENERAL(); ;
            e.COD_ESTADO = 0;
            e.DES_ESTADO = "";

            int exitoso = 0;
            int fallido = 0;
            var mensajeError = "";


            //eliminando los archivos que contie    nen el excel
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

                            string nom_excel = nombres[i];
                            var path_excel = Server.MapPath("~/Adjuntos/Files_Despacho/" + nom_excel);
                            string pathFinal_excel = Path.Combine(path_excel);
                            string tipo = "M";

                            string[] nom_excel_;
                            nom_excel_ = nom_excel.Split('.');

                            string[] day_array;
                            string day = nom_excel_[0];
                            day_array = day.Split(' ');

                            if (nom_excel_[2] == "20")
                            {
                                nom_excel_[2] = "2020";
                            }

                            var fecha_final = day_array[2] + "/" + nom_excel_[1] + "/" + nom_excel_[2];

                            e = cargarExcelArchivo(idRuta, fecha_final, 0, abrevProveedorSistemasCorredor, tipo, pathFinal_excel, ref porcentaje);

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
                    System.IO.File.Delete(pathFinal);
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

        public RPTA_GENERAL cargarExcelArchivo(int idRuta, string fecha, int reemplazar, string abrevProveedorSistemasCorredor, string TipoCarga, string pathFinal_excel_, ref int porcentaje)
        {





            PasajeroValidacionLN _PasajeroValidacionLN = new PasajeroValidacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = Session["user_login"].ToString();

            var _PasajeroValidadacion = new PasajeroValidacionAD(ref bdConn);
            var lparadero = _PasajeroValidadacion.paraderos_x_ID(idRuta);
            Conexion.finalizar(ref bdConn);

            var validacion = _PasajeroValidacionLN.ValidarExcel(TipoCarga, idRuta, fecha, reemplazar, abrevProveedorSistemasCorredor, pathFinal_excel_, lparadero, session_usuario, ref mensaje, ref tipo);


            //creal cvs

            if (validacion.COD_ESTADO == 1)
            {
                var rpta = _PasajeroValidacionLN.ListarcvsPasajeros(idRuta, fecha, reemplazar, abrevProveedorSistemasCorredor, pathFinal_excel_, lparadero, session_usuario, ref mensaje, ref tipo);




                var basePath = AppDomain.CurrentDomain.BaseDirectory + @"Download\";
                var finalPath = Path.Combine(basePath, "validacion.csv");
                try
                {
                    System.IO.File.Delete(finalPath);//elimino el cvs 
                }
                catch (Exception)
                {
                }

                ExportData Clase = new ExportData();
                var Path_finalcvs = Clase.ExportCsv(rpta, "validacion");

                var str_Path = AppDomain.CurrentDomain.BaseDirectory + @"Download\" + "\\migra.bat";






                ProcessStartInfo processInfo = new ProcessStartInfo(str_Path);
                processInfo.UseShellExecute = false;
                Process batchProcess = new Process();
                batchProcess.StartInfo = processInfo;
                batchProcess.Start();

                string ruta_base = Server.MapPath("~");
                if (ruta_base.Substring(ruta_base.Length - 1) != "\\")
                {
                    ruta_base = ruta_base + "\\";
                }

                //System.IO.File.Delete(Path_finalcvs);//elimino el cvs 
                System.IO.File.Delete(pathFinal_excel_);//elimino excel

                return validacion;
            }
            else
            {
                return validacion;
            }
        }


        public string Listarvalida_ruta_franjas(int franja, string fecha)
        {
            PasajeroValidacionLN _PasajeroValidacionLN = new PasajeroValidacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            var data = _PasajeroValidacionLN.Listarvalida_ruta_franjas(franja, fecha, ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(data); ;
        }


        public string listar_paraderos_ruta_val(int franja, int id_ruta, string fecha, string lado)
        {
            PasajeroValidacionLN _PasajeroValidacionLN = new PasajeroValidacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            var rpta = _PasajeroValidacionLN.listar_paraderos_ruta_val(franja, id_ruta, fecha, lado, ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(rpta);
        }

        public string Listarfechas_ruta_franjas(int franja, int idruta, string inicio, string fin)
        {
            PasajeroValidacionLN _PasajeroValidacionLN = new PasajeroValidacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            var data = _PasajeroValidacionLN.Listarfechas_ruta_franjas(franja, idruta, inicio, fin, ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(data); ;
        }





        public string listarMaestroPasajeros(string mes_año, string idruta)
        {
            PasajeroValidacionLN _PasajeroValidacionLN = new PasajeroValidacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            RPTA_GENERAL e = new RPTA_GENERAL();

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
            var data = _PasajeroValidacionLN.Consultar_DespachoFecha(mes, año, idruta, ref mensaje, ref tipo);

            return JsonConvert.SerializeObject(data);
        }

        //public string Guardar_Temporal_POG(string[] intervalos, string lado)
        //{
        //    PasajeroValidacionLN _PasajeroValidacionLN = new PasajeroValidacionLN();
        //    String mensaje = "";
        //    Int32 tipo = 0;
        //    var rpt = new RPTA_GENERAL();

        //    int id = 0;
        //    foreach (var intervalo in intervalos)
        //    {
        //        id++;
        //        _PasajeroValidacionLN.Agregar_Temporal_POG(id, lado, intervalo, ref mensaje, ref tipo);
        //    }
        //    return JsonConvert.SerializeObject(rpt);
        //}
    }
}