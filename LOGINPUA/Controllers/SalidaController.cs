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
using AD;
using LOGINPUA.Util;
namespace LOGINPUA.Controllers
{
    public class SalidaController : Controller
    {
        //private readonly RutaLN _rutaLN;
        //private readonly SalidaLN _salidaLN;
        //private readonly ParaderoLN _paraderoLN;
        private Object bdConn;
        //private readonly CorredoresLN _CorredoresLN;
        //private readonly RegistroPicoPlacaLN _registroPicoPlacaLN;
        //Util.Util utilidades = new Util.Util();

        //public SalidaController()
        //{
        //    _salidaLN = new SalidaLN();
        //    _rutaLN = new RutaLN();
        //    _paraderoLN = new ParaderoLN();
        //    _CorredoresLN = new CorredoresLN();
        //    _registroPicoPlacaLN = new RegistroPicoPlacaLN();
        //}

        //GET: Salida
        public ActionResult Inicio()
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

        public ActionResult ReporteComparativo()
        {
            Util.Util utilidades = new Util.Util();
            CorredoresLN _CorredoresLN = new CorredoresLN();
            RutaLN _rutaLN = new RutaLN();

            String mensaje = "";
            Int32 tipo = 0;
            var listaVistas = Session["menu_modulo"] as DataTable;
            //Session["menu_modulo"] = dt4;
            var nombreActionCurrent = this.ControllerContext.RouteData.Values["action"].ToString();
            bool auxValida = utilidades.validaVistaxPerfil(nombreActionCurrent, listaVistas);
            ViewBag.currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.currentRoute = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (auxValida)
            {
                var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
                ViewBag.rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);
                 ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
                return View();
            }
            else
            {
                return RedirectToAction("Sistemas_", "Home");
            }
        }

        public ActionResult ImportarRegistros()
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

        public ActionResult ViajesPromediados()
        {
            Util.Util utilidades = new Util.Util();
            CorredoresLN _CorredoresLN = new CorredoresLN();
            RutaLN _rutaLN = new RutaLN();

            String mensaje = "";
            Int32 tipo = 0;
            var listaVistas = Session["menu_modulo"] as DataTable;
            //Session["menu_modulo"] = dt4;
            var nombreActionCurrent = this.ControllerContext.RouteData.Values["action"].ToString();
            bool auxValida = utilidades.validaVistaxPerfil(nombreActionCurrent, listaVistas);
            ViewBag.currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.currentRoute = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (auxValida)
            {
                var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
                ViewBag.rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);
                ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
                return View();
            }
            else
            {
                return RedirectToAction("Sistemas_", "Home");
            }
        }

        public string Consultar_ReporteFechas(string mes_año, string idruta)
        {
            SalidaLN  _salidaLN = new SalidaLN();

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
            var data = _salidaLN.Consultar_DespachoFecha(mes, año, idruta, ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(data);

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
                    rpta = importarDataDespachoABEXA(idRuta, fecha, pathFinal);
                    break;
                case "SJL":
                    rpta = importarDataDespachoABEXA(idRuta, fecha, pathFinal);
                    break;
                case "CC":
                    rpta = importarDataDespachoABEXA(idRuta, fecha, pathFinal);
                    break;
                case "PN":
                    rpta = importarDataDespachoHACOM(idRuta, fecha, pathFinal);
                    break;
                case "JP":
                    rpta = importarDataDespachoHACOM(idRuta, fecha, pathFinal);
                    break;
                default:
                    rpta = "ERROR LLAMAR AL ADMINISTRADOR DEL SISTEMA";
                    break;
            }
            return JsonConvert.DeserializeObject<RPTA_GENERAL>(rpta);
        }

        public string importarExcelDespachos(int idRuta, string fecha, int reemplazar, string abrevProveedorSistemasCorredor)
        {
            String mensaje = "";
            Int32 tipo = 0;
            //RPTA_GENERAL e = new RPTA_GENERAL();
            RPTA_GENERAL rpta = new RPTA_GENERAL();
            SalidaLN _SalidaLN = new SalidaLN();
            RegistroPicoPlacaLN _registroPicoPlacaLN = new RegistroPicoPlacaLN();

            rpta = verificarRegistroPicoPlacaExistente(fecha, idRuta);
            if (rpta.COD_ESTADO == 1 && reemplazar == 0)
            { //aceptó reemplazar la data
                rpta.COD_ESTADO = 3;
                return JsonConvert.SerializeObject(rpta);
            }
            if (reemplazar == 1)
            {
                rpta = _SalidaLN.AnularMaestroSalida(rpta.AUX, ref mensaje, ref tipo);
                if (rpta.COD_ESTADO == 0)
                { //valida si ocurrio un error al momento de anular los registros
                    return JsonConvert.SerializeObject(rpta);
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

            var rptaImportacion = "";
            switch (abrevProveedorSistemasCorredor)
            {
                case "TGA":
                    rptaImportacion = importarDataDespachoABEXA(idRuta, fecha, pathFinal);
                    break;
                case "SJL":
                    rptaImportacion = importarDataDespachoABEXA(idRuta, fecha, pathFinal);
                    break;
                case "CC":
                    rptaImportacion = importarDataDespachoABEXA(idRuta, fecha, pathFinal);
                    break;
                case "PN":
                    rptaImportacion = importarDataDespachoHACOM(idRuta, fecha, pathFinal);
                    break;
                case "JP":
                    rptaImportacion = importarDataDespachoHACOM(idRuta, fecha, pathFinal);
                    break;
                default:
                    rptaImportacion = "ERROR LLAMAR AL ADMINISTRADOR DEL SISTEMA";
                    break;
            }
            return rptaImportacion;
        }

        public RPTA_GENERAL verificarRegistroPicoPlacaExistente(string fechaConsulta, int idRuta)
        {
            SalidaLN _salidaLN = new SalidaLN();

            String mensaje = "";
            Int32 tipo = 0;
            RPTA_GENERAL r = new RPTA_GENERAL();
            var data = _salidaLN.VerificarMaestroSalida(idRuta, fechaConsulta, ref mensaje, ref tipo);
            if (data.COD_ESTADO > 0)
            {
                r.COD_ESTADO = 1;
                r.DES_ESTADO = "Si Existe Información";
                r.AUX = data.AUX;
            }
            else
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = "No hay info en esta fecha";
            }
            return r;
        }

        public string getDataComparativa(string fechaComparativaA, string fechaComparativaB, int idRuta)
        {
            SalidaLN _salidaLN = new SalidaLN();

            String mensaje = "";
            Int32 tipo = 0;
            Dictionary<string, DataTable> respuesta = new Dictionary<string, DataTable>();
            DataSet Ds = new DataSet();
            //
            Ds = _salidaLN.getDataComparativos(fechaComparativaA, fechaComparativaB, idRuta, ref mensaje, ref tipo);
            var i = 0;
            foreach (DataTable dt in Ds.Tables)
            {
                respuesta.Add("dt" + i, dt);
                i++;
            }
            return JsonConvert.SerializeObject(respuesta, Formatting.Indented);
        }

        public string getParaderosByIdRuta(int id_ruta)
        {
            ParaderoLN _paraderoLN = new ParaderoLN();

            String mensaje = "";
            Int32 tipo = 0;
            var paraderos = JsonConvert.SerializeObject(_paraderoLN.getParaderosByIdRuta(id_ruta, ref mensaje, ref tipo));
            return paraderos;
        }

        public string getViajesPorRuta(int id_ruta, string fechaInicio, string fechaFin)
        {
            SalidaLN _salidaLN = new SalidaLN();

            String mensaje = "";
            Int32 tipo = 0;
            //var dataViajes = JsonConvert.SerializeObject(_salidaLN.getViajesPorRuta(id_ruta, fechaInicio, fechaFin));
            //return dataViajes;

            Dictionary<string, DataTable> respuesta = new Dictionary<string, DataTable>();
            DataSet Ds = new DataSet();
            //
            Ds = _salidaLN.getViajesPorRuta(id_ruta, fechaInicio, fechaFin, ref mensaje, ref tipo);
            var i = 0;
            foreach (DataTable dt in Ds.Tables)
            {
                respuesta.Add("dt" + i, dt);
                i++;
            }
            return JsonConvert.SerializeObject(respuesta, Formatting.Indented);
        }

        //public string importarDataDespachoHACOM_FORMATO_NUEVO(int idRuta, string fecha, string pathFinal)
        //{
        //    RPTA_GENERAL e = new RPTA_GENERAL();
        //     var path_excel = pathFinal;
        //    //var path_excel = Server.MapPath("~/Download/" + "301_27082019.xlsx");
        //    string pathFinal_excel = Path.Combine(path_excel);
        //    var mensajeError = "";

        //    /*obteniendo data de los controles recorrido A  y B segun la ruta*/
        //    var listaParaderos = _salidaLN.getParaderosByIdRuta(idRuta); //lista de paraderos tanto lado A y B 
        //    var Total_CantidadRegistroFallidos = 0;
        //    var Total_CantidadRegistroGuardados = 0;
        //    var Total_Horas = 0;
        //    //registra maestro salida
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        try
        //        {

        //            using (FileStream fs = new FileStream(path_excel, FileMode.Open))
        //            {
        //                SLDocument xlDoc = new SLDocument(fs);
        //                var hojasExcel = xlDoc.GetSheetNames();
        //                SLDocument hojaDespacho = new SLDocument(fs, hojasExcel[0]);//leyendo la hoja 
        //                SLWorksheetStatistics hojaDespachoStadistics = xlDoc.GetWorksheetStatistics();
        //                //obteniendo limites para le registro

        //                var data_inicio_fila = 11; //estático
        //                var data_ini_colum_detalle_Ida = 13; //estática
        //                var cantidadRegistrosGuardados_A = 0; //estática
        //                var cantidadRegistrosFallidos_A = 0; //estática
        //                var cantidadRegistrosFallidos_B = 0; //estática
        //                var cantidadRegistrosGuardados_B = 0; //estática
        //                var FilaIdentificador_vuelta = 0;
        //                var Columna_Final_Totalkm = 0;
        //                var Columna_Final_A = 0;
        //                var Columna_Final_B = 0;
        //                var estado_viaje = "";
        //                string fecha_excel = "";
        //                var cantidadRegistrosGuardados_Horas_A = 0;
        //                var cantidadRegistrosGuardados_Horas_B = 0;



        //                var ct = 0;

        //                for (int col = 1; col <= hojaDespachoStadistics.EndColumnIndex; col++) //solo para obtener posicion de las cabeceras
        //                {
        //                    for (int j = 1; j < hojaDespachoStadistics.EndRowIndex; j++)
        //                    {
        //                        var textoColumna = hojaDespacho.GetCellValueAsString(j, col).ToString();
        //                        switch (textoColumna)
        //                        {
        //                            case "Servicio":
        //                                FilaIdentificador_vuelta = j; //La fila del seZrvicio B
        //                                break;
        //                            case "KM":
        //                                if (ct == 0)
        //                                {
        //                                    Columna_Final_Totalkm = col - 3; // columna de Total KM
        //                                    Columna_Final_A = Columna_Final_Totalkm;

        //                                }
        //                                if (ct == 1)
        //                                {
        //                                    Columna_Final_Totalkm = col - 3; // columna de Total KM
        //                                    Columna_Final_B = col - 4;
        //                                }
        //                                ct++;
        //                                break;
        //                            default:
        //                                break;
        //                        }

        //                        if (col == 5)
        //                        {
        //                            var textoColumna_Fecha = hojaDespacho.GetCellValueAsString(5, 2);
        //                            //VALIDACION FECHA                            
        //                            fecha_excel = Convert.ToDateTime(textoColumna_Fecha).ToString("dd/MM/yyyy");
        //                            if (fecha != fecha_excel)
        //                            {
        //                                e.COD_ESTADO = 0;
        //                                e.DES_ESTADO = "ERROR -> La fecha no corresponde al de la información.";
        //                                return JsonConvert.SerializeObject(e);
        //                            }
        //                        }
        //                    }
        //                }

        //                var id_maestro_salida = _salidaLN.registrarMaestroSalida(idRuta, fecha_excel, Session["user_login"].ToString()).AUX; //registra el maestro del documento

        //                //obteniendo posicion columna y nombre de paradero
        //                var paraderosLadoA = new Dictionary<int, string>(); //columna, nombre
        //                var paraderosLadoB = new Dictionary<int, string>(); //columna, nombre
        //                var pos_ini_columna_control_A = 13;
        //                var pos_row_nom_paraderos_A = 10;


        //                /*************** llenando los paraderos del Lado A ***************/

        //                for (int columnaExcel = pos_ini_columna_control_A; columnaExcel <= hojaDespachoStadistics.EndColumnIndex; columnaExcel++) //solo para obtener posicion de las cabeceras
        //                {
        //                    var textoColumna = hojaDespacho.GetCellValueAsString(pos_row_nom_paraderos_A, columnaExcel).ToString();
        //                    if (textoColumna != "")
        //                    {
        //                        paraderosLadoA.Add(columnaExcel, textoColumna);
        //                    }
        //                }

        //                var pos_ini_columna_control_B = 13;
        //                var pos_row_nom_paraderos_B = FilaIdentificador_vuelta + 1;

        //                /*************** llenando los paraderos del Lado B ***************/

        //                for (int columnaExcel = pos_ini_columna_control_B; columnaExcel <= hojaDespachoStadistics.EndColumnIndex; columnaExcel++) //solo para obtener posicion de las cabeceras
        //                {
        //                    var textoColumna = hojaDespacho.GetCellValueAsString(pos_row_nom_paraderos_B, columnaExcel).ToString();

        //                    if (textoColumna != "")
        //                    {
        //                        paraderosLadoB.Add(columnaExcel, textoColumna);
        //                    }
        //                }

        //                string padron_a = "";
        //                string placa_a = "";
        //                var sentido = "";
        //                var hora_salida_excel_A = "";
        //                var hora_llegada_excel = "";
        //                var nro_servicio = 0;
        //                var conductor = "";

        //                //LADO A 
        //                var rowLastladoA = FilaIdentificador_vuelta - 3;
        //                for (int row = data_inicio_fila; row < rowLastladoA; row++) //13 es la posicicion donde comienza la data 
        //                {
        //                    //OBTENGO EL PATRON Y PLACA DEL LADO A
        //                    string[] array_a;
        //                    var padron_position_a = hojaDespacho.GetCellValueAsString(row, 6).ToString();//patron y placa
        //                    array_a = padron_position_a.Split(' ');
        //                    padron_a = array_a[0].Trim('(').Trim(')');
        //                    placa_a = array_a[1];
        //                    sentido = "A";
        //                    hora_salida_excel_A = hojaDespacho.GetCellValueAsString(row, 13).ToString() == "" ? "" : hojaDespacho.GetCellValueAsDateTime(row,13).ToString();

        //                    if (hora_salida_excel_A != "")
        //                    {
        //                        hora_salida_excel_A = DateTime.Parse(hora_salida_excel_A).ToString("HH:mm:ss");
        //                    }
        //                    if (hojaDespacho.GetCellValueAsString(row, Columna_Final_A).ToString() != "")
        //                    {
        //                        hora_llegada_excel = hojaDespacho.GetCellValueAsDateTime(row, Columna_Final_A).ToString();
        //                        hora_llegada_excel = DateTime.Parse(hora_llegada_excel).ToString("HH:mm:ss");
        //                    }
        //                    try
        //                    {
        //                        nro_servicio = Int32.Parse(hojaDespacho.GetCellValueAsString(row, 4).ToString());
        //                    }
        //                    catch (Exception)
        //                    {
        //                        nro_servicio = 0;
        //                    }

        //                    conductor = hojaDespacho.GetCellValueAsString(row, 10).ToString();
        //                    var estado_viaje_llegada = hora_llegada_excel != "" ? "EJECUTADO" : "TRUNCO";
        //                    var estado_viaje_salida = hora_salida_excel_A != "" ? "EJECUTADO" : "TRUNCO";
        //                    if (estado_viaje_llegada != estado_viaje_salida) { estado_viaje = "TRUNCO"; } else { estado_viaje = "EJECUTADO"; }

        //                    //registra LA SALIDAEJECUTADA //registra la salida jecutada

        //                    var id_salida_ejecutada = _salidaLN.registrarSalidaEjecutada(id_maestro_salida, padron_a, placa_a, sentido, hora_salida_excel_A, hora_llegada_excel, nro_servicio, conductor, "", "", estado_viaje, Session["user_login"].ToString());

        //                    //}

        //                    for (int colum = pos_ini_columna_control_A; colum <= Columna_Final_A; colum++)
        //                    {
        //                        //recorriendo el detalle por nombre de las hora paso de los controles

        //                        var nombreParadero = getNombreParaderoxPosicionCol(colum, sentido, paraderosLadoA);
        //                        var horaPaso = hojaDespacho.GetCellValueAsString(row, colum).ToString() == "" ? "" : hojaDespacho.GetCellValueAsDateTime(row, colum).ToString();

        //                        if (horaPaso != "")
        //                        {
        //                            horaPaso = DateTime.Parse(horaPaso).ToString("HH:mm:ss");
        //                        }
        //                        var id_paradero = getIdParaderoByEtiqueta(nombreParadero, listaParaderos, sentido);
        //                        if (id_paradero != 0)
        //                        {
        //                            var rptaRegistroDetalle = _salidaLN.registrarSalidaHoraPasoParadero(id_salida_ejecutada.AUX, id_paradero, horaPaso, sentido, Session["user_login"].ToString());
        //                         }
        //                        cantidadRegistrosGuardados_Horas_A++;
        //                    }

        //                    if (e.COD_ESTADO == 0)
        //                    {
        //                        cantidadRegistrosGuardados_A++;


        //                    }
        //                    else
        //                    {
        //                        cantidadRegistrosFallidos_A++;
        //                    }
        //                }
        //                //scope.Complete();

        //                var padron_b = "";
        //                var placa_b = "";
        //                var hora_salida_excel_B = "";
        //                var hora_llegada_excel_B = "";
        //                conductor = "";

        //                //LADO B

        //                var RowInicioLadoB = FilaIdentificador_vuelta + 2;
        //                for (int row = RowInicioLadoB; row <= hojaDespachoStadistics.EndRowIndex; row++) //13    es la posicicion donde comienza la data 
        //                {
        //                    //OBTENGO EL PATRON Y PLACA DEL LADO b
        //                    string[] array_b;

        //                    var padron_position_b = hojaDespacho.GetCellValueAsString(row, 6).ToString(); //patron y placa
        //                    array_b = padron_position_b.Split(' ');
        //                    padron_b = array_b[0].Trim('(').Trim(')');
        //                    placa_b = array_b[1];
        //                    sentido = "B";
        //                    hora_salida_excel_B = hojaDespacho.GetCellValueAsString(row, 8).ToString() == "" ? "" : hojaDespacho.GetCellValueAsDateTime(row, 8).ToString();

        //                    if (hora_salida_excel_B != "")
        //                    {
        //                        hora_salida_excel_B = DateTime.Parse(hora_salida_excel_B).ToString("HH:mm:ss");
        //                    }
        //                    if (hojaDespacho.GetCellValueAsString(row, Columna_Final_B).ToString() != "")
        //                    {
        //                        hora_llegada_excel_B = hojaDespacho.GetCellValueAsDateTime(row, Columna_Final_B).ToString();
        //                        hora_llegada_excel_B = DateTime.Parse(hora_llegada_excel_B).ToString("HH:mm:ss");
        //                    }
        //                    try
        //                    {
        //                        nro_servicio = Int32.Parse(hojaDespacho.GetCellValueAsString(row, 4).ToString());
        //                    }
        //                    catch (Exception)
        //                    {
        //                        nro_servicio = 0;
        //                    }

        //                    conductor = hojaDespacho.GetCellValueAsString(row, 10).ToString();



        //                    var estado_viaje_llegada = hora_llegada_excel_B != "" ? "EJECUTADO" : "TRUNCO";
        //                    var estado_viaje_salida = hora_salida_excel_B != "" ? "EJECUTADO" : "TRUNCO";

        //                    if (estado_viaje_llegada != estado_viaje_salida) { estado_viaje = "TRUNCO"; } else { estado_viaje = "EJECUTADO"; }

        //                    //registra LA SALIDAEJECUTADA //registra la salida jecutada

        //                    var id_salida_ejecutada = _salidaLN.registrarSalidaEjecutada(id_maestro_salida, padron_b, placa_b, sentido, hora_salida_excel_B, hora_llegada_excel_B, nro_servicio, conductor, "", "", estado_viaje, Session["user_login"].ToString());

        //                    //}

        //                    for (int colum = pos_ini_columna_control_B; colum <= Columna_Final_B; colum++)
        //                    {
        //                        //recorriendo el detalle por nombre de las hora paso de los controles

        //                        var nombreParadero = getNombreParaderoxPosicionCol(colum, sentido, paraderosLadoB);
        //                        var horaPaso = hojaDespacho.GetCellValueAsString(row, colum).ToString() == "" ? "" : hojaDespacho.GetCellValueAsDateTime(row, colum).ToString();

        //                        if (horaPaso != "")
        //                        {
        //                            horaPaso = DateTime.Parse(horaPaso).ToString("HH:mm:ss");
        //                        }
        //                        var id_paradero = getIdParaderoByEtiqueta(nombreParadero, listaParaderos, sentido);
        //                        if (id_paradero != 0)
        //                        {
        //                            var rptaRegistroDetalle = _salidaLN.registrarSalidaHoraPasoParadero(id_salida_ejecutada.AUX, id_paradero, horaPaso, sentido, Session["user_login"].ToString());
        //                        }
        //                        cantidadRegistrosGuardados_Horas_B++;
        //                    }
        //                    if (e.COD_ESTADO == 0)
        //                    {
        //                        cantidadRegistrosGuardados_B++;
        //                    }
        //                    else
        //                    {
        //                        cantidadRegistrosFallidos_B++;
        //                    }

        //                }

        //                Total_CantidadRegistroGuardados = cantidadRegistrosGuardados_A + cantidadRegistrosGuardados_B;
        //                Total_CantidadRegistroFallidos = cantidadRegistrosFallidos_A + cantidadRegistrosFallidos_B;

        //                Total_Horas = cantidadRegistrosGuardados_Horas_A + cantidadRegistrosGuardados_Horas_B;
        //                if (Total_CantidadRegistroGuardados > 0)
        //                {
        //                    e.COD_ESTADO = 1;
        //                }


        //                //scope.Complete();
        //            }
        //        }
        //        catch (TransactionAbortedException ex)
        //        {
        //            e.COD_ESTADO = 0;
        //            e.DES_ESTADO = "(CODIGO 1) ERROR " + ex.Message;
        //        }
        //    }
        //    e.DES_ESTADO = (Total_CantidadRegistroGuardados > 0 ? Total_CantidadRegistroGuardados + " Viajes guardados y " + Total_Horas + " Horas  registrados exitosamente." : " Ocurrió un error no se han guardado los registros." + "->" + mensajeError);
        //    Conexion.finalizar(ref bdConn); //cierra conexion
        //    return JsonConvert.SerializeObject(e);
        //}

        public string importarDataDespachoHACOM(int idRuta, string fecha, string pathFinal)
        {
            SalidaLN _salidaLN = new SalidaLN();

            String mensaje = "";
            Int32 tipo = 0;
            RPTA_GENERAL e = new RPTA_GENERAL();
            var path_excel = pathFinal;
            //var path_excel = Server.MapPath("~/Download/" + "301_27082019.xlsx");
            string pathFinal_excel = Path.Combine(path_excel);
            var mensajeError = "";

            /*obteniendo data de los controles recorrido A  y B segun la ruta*/
            var listaParaderos = _salidaLN.getParaderosByIdRuta(idRuta, ref mensaje, ref tipo); //lista de paraderos tanto lado A y B 
            var Total_CantidadRegistroFallidos = 0;
            var Total_CantidadRegistroGuardados = 0;
            var Total_Horas = 0;
            //registra maestro salida
             
                try
                {

                    using (FileStream fs = new FileStream(path_excel, FileMode.Open))
                    {
                        SLDocument xlDoc = new SLDocument(fs);
                        var hojasExcel = xlDoc.GetSheetNames();
                        SLDocument hojaDespacho = new SLDocument(fs, hojasExcel[0]);//leyendo la hoja 
                        SLWorksheetStatistics hojaDespachoStadistics = xlDoc.GetWorksheetStatistics();
                        //obteniendo limites para le registro

                        var data_inicio_fila = 11; //estático
                        var data_ini_colum_detalle_Ida = 13; //estática
                        var cantidadRegistrosGuardados_A = 0; //estática
                        var cantidadRegistrosFallidos_A = 0; //estática
                        var cantidadRegistrosFallidos_B = 0; //estática
                        var cantidadRegistrosGuardados_B = 0; //estática
                        var FilaIdentificador_vuelta = 0;
                        var Columna_Final_Totalkm = 0;
                        var Columna_Final_A = 0;
                        var Columna_Final_B = 0;
                        var estado_viaje = "";
                        string fecha_excel = "";
                        var cantidadRegistrosGuardados_Horas_A = 0;
                        var cantidadRegistrosGuardados_Horas_B = 0;




                        var ct = 0;

                        for (int col = 1; col <= hojaDespachoStadistics.EndColumnIndex; col++) //solo para obtener posicion de las cabeceras
                        {
                            for (int j = 1; j < hojaDespachoStadistics.EndRowIndex; j++)
                            {
                                var textoColumna = hojaDespacho.GetCellValueAsString(j, col).ToString();
                                switch (textoColumna)
                                {
                                    case "Servicio":
                                        FilaIdentificador_vuelta = j; //La fila del seZrvicio B
                                        break;
                                    case "Total KM":
                                        if (ct == 0)
                                        {
                                            Columna_Final_Totalkm = col - 1; // columna de Total KM
                                            Columna_Final_A = Columna_Final_Totalkm;

                                        }
                                        if (ct == 1)
                                        {
                                            Columna_Final_Totalkm = col - 1; // columna de Total KM
                                            Columna_Final_B = col - 1;
                                        }
                                        ct++;
                                        break;
                                    default:
                                        break;
                                }

                                if (col == 5)
                                {
                                    var textoColumna_Fecha = hojaDespacho.GetCellValueAsString(5, 2);
                                    //VALIDACION FECHA                            
                                    fecha_excel = Convert.ToDateTime(textoColumna_Fecha).ToString("dd/MM/yyyy");
                                    if (fecha != fecha_excel)
                                    {
                                        e.COD_ESTADO = 0;
                                        e.DES_ESTADO = "ERROR -> La fecha no corresponde al de la información.";
                                        return JsonConvert.SerializeObject(e);
                                    }
                                }
                            }
                        }
                        //RUTA CICLICAS 

                        if (Columna_Final_B == 0)
                        {
                            var rutas = Rutas_ciclica(idRuta, fecha, hojaDespacho);
                            return rutas;

                        }


                        var id_maestro_salida = _salidaLN.registrarMaestroSalida(idRuta, fecha_excel, Session["user_login"].ToString(), ref mensaje, ref tipo).AUX; //registra el maestro del documento

                        //obteniendo posicion columna y nombre de paradero
                        var paraderosLadoA = new Dictionary<int, string>(); //columna, nombre
                        var paraderosLadoB = new Dictionary<int, string>(); //columna, nombre
                        var pos_ini_columna_control_A = 8;
                        var pos_row_nom_paraderos_A = 10;


                        /*************** llenando los paraderos del Lado A ***************/

                        for (int columnaExcel = pos_ini_columna_control_A; columnaExcel <= hojaDespachoStadistics.EndColumnIndex; columnaExcel++) //solo para obtener posicion de las cabeceras
                        {
                            var textoColumna = hojaDespacho.GetCellValueAsString(pos_row_nom_paraderos_A, columnaExcel).ToString();
                            if (textoColumna != "")
                            {
                                paraderosLadoA.Add(columnaExcel, textoColumna);
                            }
                        }

                        var pos_ini_columna_control_B = 8;
                        var pos_row_nom_paraderos_B = FilaIdentificador_vuelta + 1;

                        /*************** llenando los paraderos del Lado B ***************/

                        for (int columnaExcel = pos_ini_columna_control_B; columnaExcel <= hojaDespachoStadistics.EndColumnIndex; columnaExcel++) //solo para obtener posicion de las cabeceras
                        {
                            var textoColumna = hojaDespacho.GetCellValueAsString(pos_row_nom_paraderos_B, columnaExcel).ToString();

                            if (textoColumna != "")
                            {
                                paraderosLadoB.Add(columnaExcel, textoColumna);
                            }
                        }

                        string padron_a = "";
                        string placa_a = "";
                        var sentido = "";
                        var hora_salida_excel_A = "";
                        var hora_llegada_excel = "";
                        var nro_servicio = 0;
                        var conductor = "";

                        //LADO A 
                        var rowLastladoA = FilaIdentificador_vuelta - 3;
                        for (int row = data_inicio_fila; row < rowLastladoA; row++) //11 es la posicicion donde comienza la data 
                        {
                            //OBTENGO EL PATRON Y PLACA DEL LADO A
                            string[] array_a;
                            var padron_position_a = hojaDespacho.GetCellValueAsString(row, 2).ToString();
                            array_a = padron_position_a.Split(' ');
                            padron_a = array_a[0].Trim('(').Trim(')');
                            placa_a = array_a[1];
                            sentido = "A";
                            hora_salida_excel_A = hojaDespacho.GetCellValueAsString(row, 8).ToString() == "" ? "" : hojaDespacho.GetCellValueAsDateTime(row, 8).ToString();

                            if (hora_salida_excel_A != "")
                            {
                                hora_salida_excel_A = DateTime.Parse(hora_salida_excel_A).ToString("HH:mm:ss");
                            }
                            if (hojaDespacho.GetCellValueAsString(row, Columna_Final_A).ToString() != "")
                            {
                                hora_llegada_excel = hojaDespacho.GetCellValueAsDateTime(row, Columna_Final_A).ToString();
                                hora_llegada_excel = DateTime.Parse(hora_llegada_excel).ToString("HH:mm:ss");
                            }
                            try
                            {
                                nro_servicio = Int32.Parse(hojaDespacho.GetCellValueAsString(row, 6).ToString());
                            }
                            catch (Exception)
                            {
                                nro_servicio = 0;
                            }

                            conductor = hojaDespacho.GetCellValueAsString(row, 7).ToString();
                            var estado_viaje_llegada = hora_llegada_excel != "" ? "EJECUTADO" : "TRUNCO";
                            var estado_viaje_salida = hora_salida_excel_A != "" ? "EJECUTADO" : "TRUNCO";
                            if (estado_viaje_llegada != estado_viaje_salida) { estado_viaje = "TRUNCO"; } else { estado_viaje = "EJECUTADO"; }

                            //registra LA SALIDAEJECUTADA //registra la salida jecutada

                            var id_salida_ejecutada = _salidaLN.registrarSalidaEjecutada(id_maestro_salida, padron_a, placa_a, sentido, hora_salida_excel_A, hora_llegada_excel, nro_servicio, conductor, "", "", estado_viaje, Session["user_login"].ToString(), ref mensaje, ref tipo);

                            //}

                            for (int colum = pos_ini_columna_control_A; colum <= Columna_Final_A; colum++)
                            {
                                //recorriendo el detalle por nombre de las hora paso de los controles

                                var nombreParadero = getNombreParaderoxPosicionCol(colum, sentido, paraderosLadoA);
                                var horaPaso = hojaDespacho.GetCellValueAsString(row, colum).ToString() == "" ? "" : hojaDespacho.GetCellValueAsDateTime(row, colum).ToString();

                                if (horaPaso != "")
                                {
                                    horaPaso = DateTime.Parse(horaPaso).ToString("HH:mm:ss");
                                }
                                var id_paradero = getIdParaderoByEtiqueta(nombreParadero, listaParaderos, sentido);
                                if (id_paradero != 0)
                                {
                                    var rptaRegistroDetalle = _salidaLN.registrarSalidaHoraPasoParadero(id_salida_ejecutada.AUX, id_paradero, horaPaso, sentido, Session["user_login"].ToString(), ref mensaje, ref tipo);
                                }
                                cantidadRegistrosGuardados_Horas_A++;
                            }

                            if (e.COD_ESTADO == 0)
                            {
                                cantidadRegistrosGuardados_A++;


                            }
                            else
                            {
                                cantidadRegistrosFallidos_A++;
                            }
                        }
                        //scope.Complete();

                        var padron_b = "";
                        var placa_b = "";
                        var hora_salida_excel_B = "";
                        var hora_llegada_excel_B = "";
                        conductor = "";

                        //LADO B

                        var RowInicioLadoB = FilaIdentificador_vuelta + 2;
                        for (int row = RowInicioLadoB; row <= hojaDespachoStadistics.EndRowIndex; row++) //11 es la posicicion donde comienza la data 
                        {
                            //OBTENGO EL PATRON Y PLACA DEL LADO b
                            string[] array_b;
                            var padron_position_b = hojaDespacho.GetCellValueAsString(row, 2).ToString();
                            array_b = padron_position_b.Split(' ');
                            padron_b = array_b[0].Trim('(').Trim(')');
                            placa_b = array_b[1];
                            sentido = "B";
                            hora_salida_excel_B = hojaDespacho.GetCellValueAsString(row, 8).ToString() == "" ? "" : hojaDespacho.GetCellValueAsDateTime(row, 8).ToString();

                            if (hora_salida_excel_B != "")
                            {
                                hora_salida_excel_B = DateTime.Parse(hora_salida_excel_B).ToString("HH:mm:ss");
                            }
                            if (hojaDespacho.GetCellValueAsString(row, Columna_Final_B).ToString() != "")
                            {
                                hora_llegada_excel_B = hojaDespacho.GetCellValueAsDateTime(row, Columna_Final_B).ToString();
                                hora_llegada_excel_B = DateTime.Parse(hora_llegada_excel_B).ToString("HH:mm:ss");
                            }
                            try
                            {
                                nro_servicio = Int32.Parse(hojaDespacho.GetCellValueAsString(row, 6).ToString());
                            }
                            catch (Exception)
                            {
                                nro_servicio = 0;
                            }

                            conductor = hojaDespacho.GetCellValueAsString(row, 7).ToString();
                            var estado_viaje_llegada = hora_llegada_excel_B != "" ? "EJECUTADO" : "TRUNCO";
                            var estado_viaje_salida = hora_salida_excel_B != "" ? "EJECUTADO" : "TRUNCO";
                            if (estado_viaje_llegada != estado_viaje_salida) { estado_viaje = "TRUNCO"; } else { estado_viaje = "EJECUTADO"; }

                            //registra LA SALIDAEJECUTADA //registra la salida jecutada

                            var id_salida_ejecutada = _salidaLN.registrarSalidaEjecutada(id_maestro_salida, padron_b, placa_b, sentido, hora_salida_excel_B, hora_llegada_excel_B, nro_servicio, conductor, "", "", estado_viaje, Session["user_login"].ToString(), ref mensaje, ref tipo);

                            //}

                            for (int colum = pos_ini_columna_control_B; colum <= Columna_Final_B; colum++)
                            {
                                //recorriendo el detalle por nombre de las hora paso de los controles

                                var nombreParadero = getNombreParaderoxPosicionCol(colum, sentido, paraderosLadoB);
                                var horaPaso = hojaDespacho.GetCellValueAsString(row, colum).ToString() == "" ? "" : hojaDespacho.GetCellValueAsDateTime(row, colum).ToString();

                                if (horaPaso != "")
                                {
                                    horaPaso = DateTime.Parse(horaPaso).ToString("HH:mm:ss");
                                }
                                var id_paradero = getIdParaderoByEtiqueta(nombreParadero, listaParaderos, sentido);
                                if (id_paradero != 0)
                                {
                                    var rptaRegistroDetalle = _salidaLN.registrarSalidaHoraPasoParadero(id_salida_ejecutada.AUX, id_paradero, horaPaso, sentido, Session["user_login"].ToString(), ref mensaje, ref tipo);
                                }
                                cantidadRegistrosGuardados_Horas_B++;
                            }
                            if (e.COD_ESTADO == 0)
                            {
                                cantidadRegistrosGuardados_B++;
                            }
                            else
                            {
                                cantidadRegistrosFallidos_B++;
                            }

                        }

                        Total_CantidadRegistroGuardados = cantidadRegistrosGuardados_A + cantidadRegistrosGuardados_B;
                        Total_CantidadRegistroFallidos = cantidadRegistrosFallidos_A + cantidadRegistrosFallidos_B;

                        Total_Horas = cantidadRegistrosGuardados_Horas_A + cantidadRegistrosGuardados_Horas_B;
                        if (Total_CantidadRegistroGuardados > 0)
                        {
                            e.COD_ESTADO = 1;
                        }


                        //scope.Complete();
                    }
                }
                catch (TransactionAbortedException ex)
                {
                    e.COD_ESTADO = 0;
                    e.DES_ESTADO = "(CODIGO 1) ERROR " + ex.Message;
                }
             
            e.DES_ESTADO = (Total_CantidadRegistroGuardados > 0 ? Total_CantidadRegistroGuardados + " Viajes guardados y" + Total_Horas + " Horas  registrados exitosamente." : " Ocurrió un error no se han guardado los registros." + "->" + mensajeError);
            Conexion.finalizar(ref bdConn); //cierra conexion
            return JsonConvert.SerializeObject(e);
        }

        public string importarDataDespachoABEXA(int idRuta, string fecha, string pathFinal)
        {
            SalidaLN _salidaLN = new SalidaLN();

            String mensaje = "";
            Int32 tipo = 0;
            RPTA_GENERAL e = new RPTA_GENERAL();

            var path_excel = pathFinal;
            string pathFinal_excel = Path.Combine(path_excel);
            var fechaTiempoRegistroExcel = "";
            var txtFecharow = ""; //texto FECHA
            var cantidadRegistrosGuardados = 0;
            var cantidadRegistrosFallidos = 0;
            var mensajeError = "";
            var totalHoras = 0;


            /*obteniendo data de los controles recorrido A  y B segun la ruta*/
            var listaParaderos = _salidaLN.getParaderosByIdRuta(idRuta, ref mensaje, ref tipo); //lista de paraderos tanto lado A y B 
                                                                         //var fechaExcelDespacho = "24/07/2019";
                                                                         //registra maestro salida
          
                try
                {
                    using (FileStream fs = new FileStream(path_excel, FileMode.Open))
                    {
                        SLDocument xlDoc = new SLDocument(fs);
                        var hojasExcel = xlDoc.GetSheetNames();
                        SLDocument hojaDespacho = new SLDocument(fs, hojasExcel[0]);//leyendo la hoja 
                        SLWorksheetStatistics hojaDespachoStadistics = xlDoc.GetWorksheetStatistics();

                        //obteniendo limites para le registro
                        var data_inicio_fila = 9; //estático
                        var data_ini_colum_detalle_Ida = 13; //estática
                        var data_fin_colum_detalle_Ida = 0; //dinamico
                        var data_ini_colum_detalle_vuelta = 0; //dinamico
                        var data_fin_colum_detalle_vuelta = 0; //dinamico

                        for (int col = 1; col <= hojaDespachoStadistics.EndColumnIndex; col++) //solo para obtener posicion de las cabeceras
                        {
                            var textoColumna = hojaDespacho.GetCellValueAsString(7, col).ToString();
                            switch (textoColumna)
                            {
                                case "Llegada":
                                    data_fin_colum_detalle_Ida = idRuta == 3 ? col - 1 : col;
                                    data_fin_colum_detalle_Ida = col;

                                    data_fin_colum_detalle_Ida += 2; // 2 columnas +
                                    data_ini_colum_detalle_vuelta = col + 3; //+ 3 esta en el inicio de la vuelta
                                    break;
                                case "Llegada B":
                                    data_fin_colum_detalle_vuelta = idRuta == 3 ? col - 1 : col;
                                    data_fin_colum_detalle_vuelta = col;
                                    data_fin_colum_detalle_vuelta += 2;
                                    break;
                                default:
                                    break;
                            }



                            if (col == 9)
                            {
                                txtFecharow = xlDoc.GetCellValueAsDateTime(col, 2).ToString(); //texto fecha
                                fechaTiempoRegistroExcel = txtFecharow;

                                string[] arrFechaTiempoRegistroExcel = new string[2];
                                //
                                //obtiene un arreglo con el menor tiempo viaje en A y B + 2 HORAS
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
                                    string fecha_excel = Convert.ToDateTime(fechaTiempoRegistroExcel).ToString("dd/MM/yyyy");


                                    if (fecha_excel != fecha)
                                    { //si la fecha del excel es igual a la fecha que ingreso el usuario
                                        e.COD_ESTADO = 0;
                                        e.DES_ESTADO = "ERROR -> La fecha no corresponde al de la información.";
                                        return JsonConvert.SerializeObject(e);
                                    }
                                }
                            }


                        }
                        var id_maestro_salida = _salidaLN.registrarMaestroSalida(idRuta, fechaTiempoRegistroExcel, Session["user_login"].ToString(), ref mensaje, ref tipo).AUX; //registra el maestro del documento


                        //obteniendo posicion columna y nombre de paradero
                        var paraderosLadoA = new Dictionary<int, string>(); //columna, nombre
                        var paraderosLadoB = new Dictionary<int, string>(); //columna, nombre

                        var pos_ini_columna_control = 13;
                        var pos_row_nom_paraderos = 8;
                        /*************** llenando los paraderos de ambos lados ***************/
                        for (int columnaExcel = pos_ini_columna_control; columnaExcel <= hojaDespachoStadistics.EndColumnIndex; columnaExcel++) //solo para obtener posicion de las cabeceras
                        {
                            var textoColumna = hojaDespacho.GetCellValueAsString(pos_row_nom_paraderos, columnaExcel).ToString();
                            if (columnaExcel <= data_fin_colum_detalle_Ida)
                            { //LADO A
                                paraderosLadoA.Add(columnaExcel, textoColumna);
                            }
                            else
                            { //LADO B
                                paraderosLadoB.Add(columnaExcel, textoColumna);
                            }
                        }

                        //ahora recorriendo el excel segun los limites obtenidos
                        for (int row = data_inicio_fila; row <= hojaDespachoStadistics.EndRowIndex; row++) //9 es la posicicion donde comienza la data 
                        {
                            var padron = hojaDespacho.GetCellValueAsString(row, 3).ToString();
                            var placa = hojaDespacho.GetCellValueAsString(row, 4).ToString();
                            var sentido = hojaDespacho.GetCellValueAsString(row, 5).ToString();
                            var hora_salida_excel = hojaDespacho.GetCellValueAsString(row, 6).ToString() == "" ? "" : hojaDespacho.GetCellValueAsDateTime(row, 6).ToString();
                            var hora_llegada_excel = "";

                            if (hora_salida_excel != "")
                            {
                                hora_salida_excel = DateTime.Parse(hora_salida_excel).ToString("HH:mm:ss");
                            }

                            if (hojaDespacho.GetCellValueAsString(row, sentido == "A" ? data_fin_colum_detalle_Ida : data_fin_colum_detalle_vuelta).ToString() != "")
                            {
                                hora_llegada_excel = hojaDespacho.GetCellValueAsDateTime(row, sentido == "A" ? data_fin_colum_detalle_Ida : data_fin_colum_detalle_vuelta).ToString();
                                hora_llegada_excel = DateTime.Parse(hora_llegada_excel).ToString("HH:mm:ss");

                            }
                            var nro_servicio = Int32.Parse(hojaDespacho.GetCellValueAsString(row, 7).ToString());
                            var conductor = hojaDespacho.GetCellValueAsString(row, 9).ToString();
                            var dni_conductor = hojaDespacho.GetCellValueAsString(row, 10).ToString();
                            var estado_viaje = hojaDespacho.GetCellValueAsString(row, 11).ToString();
                            var comentario = hojaDespacho.GetCellValueAsString(row, 12).ToString();
                            var id_salida_ejecutada = _salidaLN.registrarSalidaEjecutada(id_maestro_salida, padron, placa, sentido, hora_salida_excel, hora_llegada_excel, nro_servicio, conductor, dni_conductor, comentario, "ejecutado", Session["user_login"].ToString(), ref mensaje, ref tipo); //registra el maestro del documento //registra la salida ejecutada

                            //recorriendo el detalle por nombre de las hora paso de los controles
                            for (int PosControl = (sentido == "A" ? data_ini_colum_detalle_Ida : data_ini_colum_detalle_vuelta);
                                    PosControl <= (sentido == "A" ? data_fin_colum_detalle_Ida : data_fin_colum_detalle_vuelta);
                                    PosControl++)
                            {
                                var nombreParadero = getNombreParaderoxPosicionCol(PosControl, sentido, (sentido == "A" ? paraderosLadoA : paraderosLadoB));
                                var horaPaso = hojaDespacho.GetCellValueAsString(row, PosControl).ToString() == "" ? "" : hojaDespacho.GetCellValueAsDateTime(row, PosControl).ToString();

                                if (horaPaso != "")
                                {
                                    horaPaso = DateTime.Parse(horaPaso).ToString("HH:mm:ss");
                                }
                                var id_paradero = getIdParaderoByEtiqueta(nombreParadero, listaParaderos, sentido);

                                if (id_paradero != 0)
                                {
                                    var rptaRegistroDetalle = _salidaLN.registrarSalidaHoraPasoParadero(id_salida_ejecutada.AUX, id_paradero, horaPaso, sentido, Session["user_login"].ToString(), ref mensaje, ref tipo);
                                    if (rptaRegistroDetalle.COD_ESTADO == 1)
                                    {
                                        totalHoras++;
                                    }
                                }
                            }
                            if (e.COD_ESTADO == 0)
                            {
                                cantidadRegistrosGuardados++;
                            }
                            else
                            {
                                mensajeError = e.DES_ESTADO;
                                cantidadRegistrosFallidos++;
                            }
                        }

                        if (cantidadRegistrosGuardados > 0)
                        {
                            e.COD_ESTADO = 1;
                        }

                        //scope.Complete();
                    }
                }
                catch (TransactionAbortedException ex)
                {
                    e.COD_ESTADO = 0;
                    e.DES_ESTADO = "(CODIGO 1) ERROR " + ex.Message;
                }
           
            e.DES_ESTADO = (cantidadRegistrosGuardados > 0 ? cantidadRegistrosGuardados + " Viajes guardados y " + totalHoras + " Horas registrados exitosamente." : "Ocurrió un error no se han guardado los registros." + "->" + mensajeError);
            Conexion.finalizar(ref bdConn); //cierra conexion
            return JsonConvert.SerializeObject(e);
        }

        public int getIdParaderoByEtiqueta(string nombreEtiqueta, List<CC_PARADERO> lparadero, string ladoBusqueda)
        {
            var id_paradero = 0;
            foreach (var item in lparadero)
            {
                if (nombreEtiqueta == item.ETIQUETA_NOMBRE && ladoBusqueda == item.LADO)
                {
                    id_paradero = item.ID_PARADERO;
                }
            }
            return id_paradero;
        }


        public int getIdParaderoByEtiqueta_CICLICA_A(string nombreEtiqueta, List<CC_PARADERO> lparadero, string ladoBusqueda)
        {
            var id_paradero = 0;

            foreach (var item in lparadero)
            {
                if (item.LADO == "A")
                {
                    if (nombreEtiqueta == item.ETIQUETA_NOMBRE && ladoBusqueda == item.LADO)
                    {
                        id_paradero = item.ID_PARADERO;
                    }
                }

            }
            return id_paradero;
        }
        public int getIdParaderoByEtiqueta_CICLICA_B(string nombreEtiqueta, List<CC_PARADERO> lparadero, string ladoBusqueda)
        {
            var id_paradero = 0;
            foreach (var item in lparadero)
            {
                if (item.LADO == "B")
                {
                    if (nombreEtiqueta == item.ETIQUETA_NOMBRE && ladoBusqueda == item.LADO)
                    {
                        id_paradero = item.ID_PARADERO;
                    }
                }
            }
            return id_paradero;
        }


        public string getNombreParaderoxPosicionCol(int posicion, string lado, Dictionary<int, string> dataControles)
        {
            var rptaNombre = "";

            foreach (var item in dataControles)
            {
                if (posicion == item.Key)
                {
                    rptaNombre = item.Value;
                }
            }
            return rptaNombre;
        }








        public string Rutas_ciclica(int idRuta, string fecha, SLDocument hojaDespacho)
        {
            SalidaLN _salidaLN = new SalidaLN();

            RPTA_GENERAL e = new RPTA_GENERAL();
            String mensaje = "";
            Int32 tipo = 0;

            var cantidadRegistrosGuardados = 0;
            var cantidadRegistrosFallidos = 0;
            var mensajeError = "";

            var cantidadRegistrosGuardados_Horas_A = 0;
            var cantidadRegistrosGuardados_Horas_B = 0;
            /*obteniendo data de los controles recorrido A  y B segun la ruta*/
            var listaParaderos = _salidaLN.getParaderosByIdRuta(idRuta, ref mensaje, ref tipo); //lista de paraderos tanto lado A y B 
            var Total_Horas = 0;                                                        //registra maestro salida
            var Total_CantidadRegistroFallidos = 0;
            var Total_CantidadRegistroGuardados = 0;
            

                try
                {
                    CC_RECORRIDO recorrido = new CC_RECORRIDO();
                    SLWorksheetStatistics hojaDespachoStadistics = hojaDespacho.GetWorksheetStatistics();

                    var FilaIdentificador_vuelta = 0;
                    var Columna_Final_Totalkm = 0;
                    string fecha_excel = "";
                    var Columna_Final_A = 0;

                    var Columna_Final_B = 0;
                    for (int col = 1; col <= hojaDespachoStadistics.EndColumnIndex; col++) //solo para obtener posicion de las cabeceras
                    {
                        for (int j = 1; j < hojaDespachoStadistics.EndRowIndex; j++)
                        {
                            var Validacion_Vacios = hojaDespacho.GetCellValueAsString(j, 2).ToString();

                            if (Validacion_Vacios == " ")
                            {
                                e.COD_ESTADO = 0;
                                e.DES_ESTADO = "Existen Vacios en el excel";
                                return JsonConvert.SerializeObject(e);
                            }

                            var textoColumna = hojaDespacho.GetCellValueAsString(j, col).ToString();
                            switch (textoColumna)
                            {
                                case "Servicio":
                                    FilaIdentificador_vuelta = j; //La fila del seZrvicio B
                                    break;
                                case "Total KM":
                                    Columna_Final_Totalkm = col - 1; // columna de Total KM
                                    Columna_Final_A = Columna_Final_Totalkm;
                                    Columna_Final_B = Columna_Final_Totalkm;

                                    break;
                                default:
                                    break;
                            }

                            if (col == 5)
                            {

                                var textoColumna_Fecha = hojaDespacho.GetCellValueAsString(5, 2);
                                //VALIDACION FECHA                            
                                fecha_excel = Convert.ToDateTime(textoColumna_Fecha).ToString("dd/MM/yyyy");
                                if (fecha != fecha_excel)
                                {
                                    e.COD_ESTADO = 0;
                                    e.DES_ESTADO = "ERROR -> La fecha no corresponde al de la información.";

                                    return JsonConvert.SerializeObject(e);
                                }
                            }
                        }
                    }



                    var id_maestro_salida = _salidaLN.registrarMaestroSalida(idRuta, fecha_excel, Session["user_login"].ToString(), ref mensaje, ref tipo).AUX; //registra el maestro del documento


                    //obteniendo posicion columna y nombre de paradero
                    var paraderosLadoA = new Dictionary<int, string>(); //columna, nombre
                    var paraderosLadoB = new Dictionary<int, string>(); //columna, nombre
                    var pos_ini_columna_control_A = 8;
                    var pos_row_nom_paraderos_A = 10;

                    var pos_ini_columna_control_B = 8;
                    var pos_row_nom_paraderos_B = 10;

                    /*************** llenando los paraderos del Lado A ***************/

                    for (int columnaExcel = pos_ini_columna_control_A; columnaExcel <= hojaDespachoStadistics.EndColumnIndex; columnaExcel++) //solo para obtener posicion de las cabeceras
                    {
                        var textoColumna_paradero = hojaDespacho.GetCellValueAsString(pos_row_nom_paraderos_A, columnaExcel).ToString();
                        if (textoColumna_paradero != "")
                        {
                            paraderosLadoA.Add(columnaExcel, textoColumna_paradero);
                        }
                    }
                    /*************** llenando los paraderos del Lado B ***************/


                    for (int columnaExcel = pos_ini_columna_control_B; columnaExcel <= hojaDespachoStadistics.EndColumnIndex; columnaExcel++) //solo para obtener posicion de las cabeceras
                    {
                        var textoColumna = hojaDespacho.GetCellValueAsString(pos_row_nom_paraderos_B, columnaExcel).ToString();

                        if (textoColumna != "")
                        {
                            paraderosLadoB.Add(columnaExcel, textoColumna);
                        }
                    }



                    //LADO A 
                    var data_inicio_fila = 11; //estático
                    string padron_a = "";
                    string placa_a = "";
                    var sentido = "";
                    var hora_salida_excel_A = "";
                    var hora_llegada_excel = "";
                    var nro_servicio = 0;
                    var conductor = "";
                    var estado_viaje = "";


                    for (int row = data_inicio_fila; row < hojaDespachoStadistics.EndRowIndex + 1; row++) //11 es la posicicion donde comienza la data 
                    {
                        //OBTENGO EL PATRON Y PLACA DEL LADO A
                        string[] array_a;
                        var padron_position_a = hojaDespacho.GetCellValueAsString(row, 2).ToString();
                        array_a = padron_position_a.Split(' ');
                        padron_a = array_a[0].Trim('(').Trim(')');
                        placa_a = array_a[1];
                        sentido = "A";
                        hora_salida_excel_A = hojaDespacho.GetCellValueAsString(row, 8).ToString() == "" ? "" : hojaDespacho.GetCellValueAsDateTime(row, 8).ToString();

                        if (hora_salida_excel_A != "")
                        {
                            hora_salida_excel_A = DateTime.Parse(hora_salida_excel_A).ToString("HH:mm:ss");
                        }
                        if (hojaDespacho.GetCellValueAsString(row, Columna_Final_A).ToString() != "")
                        {
                            hora_llegada_excel = hojaDespacho.GetCellValueAsDateTime(row, Columna_Final_A).ToString();
                            hora_llegada_excel = DateTime.Parse(hora_llegada_excel).ToString("HH:mm:ss");
                        }
                        try
                        {
                            nro_servicio = Int32.Parse(hojaDespacho.GetCellValueAsString(row, 6).ToString());
                        }
                        catch (Exception)
                        {
                            nro_servicio = 0;
                        }

                        conductor = hojaDespacho.GetCellValueAsString(row, 7).ToString();
                        var estado_viaje_llegada = hora_llegada_excel != "" ? "EJECUTADO" : "TRUNCO";
                        var estado_viaje_salida = hora_salida_excel_A != "" ? "EJECUTADO" : "TRUNCO";
                        if (estado_viaje_llegada != estado_viaje_salida) { estado_viaje = "TRUNCO"; } else { estado_viaje = "EJECUTADO"; }

                        //registra LA SALIDAEJECUTADA //registra la salida jecutada

                        var id_salida_ejecutada = _salidaLN.registrarSalidaEjecutada(id_maestro_salida, padron_a, placa_a, sentido, hora_salida_excel_A, hora_llegada_excel, nro_servicio, conductor, "", "", estado_viaje, "WCUBAS", ref mensaje, ref tipo);

                        //}

                        for (int colum = pos_ini_columna_control_A; colum <= Columna_Final_A; colum++)
                        {
                            //recorriendo el detalle por nombre de las hora paso de los controles

                            var nombreParadero = getNombreParaderoxPosicionCol(colum, sentido, paraderosLadoA);
                            var horaPaso = hojaDespacho.GetCellValueAsString(row, colum).ToString() == "" ? "" : hojaDespacho.GetCellValueAsDateTime(row, colum).ToString();

                            if (horaPaso != "")
                            {
                                horaPaso = DateTime.Parse(horaPaso).ToString("HH:mm:ss");
                            }
                            var id_paradero = getIdParaderoByEtiqueta_CICLICA_A(nombreParadero, listaParaderos, sentido);
                            if (id_paradero != 0)
                            {
                                var rptaRegistroDetalle = _salidaLN.registrarSalidaHoraPasoParadero(id_salida_ejecutada.AUX, id_paradero, horaPaso, sentido, "WCUBAS", ref mensaje, ref tipo);
                            }
                            cantidadRegistrosGuardados_Horas_A++;
                        }


                        if (id_salida_ejecutada.COD_ESTADO == 0)
                        {
                            cantidadRegistrosFallidos++;

                        }
                        else
                        {
                            cantidadRegistrosGuardados++;

                        }
                    }




                    //LADO B 
                    data_inicio_fila = 11; //estático
                    var padron_b = "";
                    var placa_b = "";
                    sentido = "";
                    var hora_salida_excel_B = "";
                    hora_llegada_excel = "";
                    nro_servicio = 0;
                    conductor = "";
                    estado_viaje = "";


                    for (int row = data_inicio_fila; row < hojaDespachoStadistics.EndRowIndex + 1; row++) //11 es la posicicion donde comienza la data 
                    {
                        //OBTENGO EL PATRON Y PLACA DEL LADO A
                        string[] array_b;
                        var padron_position_b = hojaDespacho.GetCellValueAsString(row, 2).ToString();
                        array_b = padron_position_b.Split(' ');
                        padron_b = array_b[0].Trim('(').Trim(')');
                        placa_b = array_b[1];
                        sentido = "B";
                        hora_salida_excel_B = hojaDespacho.GetCellValueAsString(row, 8).ToString() == "" ? "" : hojaDespacho.GetCellValueAsDateTime(row, 8).ToString();

                        if (hora_salida_excel_B != "")
                        {
                            hora_salida_excel_B = DateTime.Parse(hora_salida_excel_B).ToString("HH:mm:ss");
                        }
                        if (hojaDespacho.GetCellValueAsString(row, Columna_Final_B).ToString() != "")
                        {
                            hora_llegada_excel = hojaDespacho.GetCellValueAsDateTime(row, Columna_Final_B).ToString();
                            hora_llegada_excel = DateTime.Parse(hora_llegada_excel).ToString("HH:mm:ss");
                        }
                        try
                        {
                            nro_servicio = Int32.Parse(hojaDespacho.GetCellValueAsString(row, 6).ToString());
                        }
                        catch (Exception)
                        {
                            nro_servicio = 0;
                        }

                        conductor = hojaDespacho.GetCellValueAsString(row, 7).ToString();
                        var estado_viaje_llegada = hora_llegada_excel != "" ? "EJECUTADO" : "TRUNCO";
                        var estado_viaje_salida = hora_salida_excel_B != "" ? "EJECUTADO" : "TRUNCO";
                        if (estado_viaje_llegada != estado_viaje_salida) { estado_viaje = "TRUNCO"; } else { estado_viaje = "EJECUTADO"; }

                        //registra LA SALIDAEJECUTADA //registra la salida jecutada

                        var id_salida_ejecutada = _salidaLN.registrarSalidaEjecutada(id_maestro_salida, padron_a, placa_a, sentido, hora_salida_excel_B, hora_llegada_excel, nro_servicio, conductor, "", "", estado_viaje, "WCUBAS", ref mensaje, ref tipo);

                        //}

                        for (int colum = pos_ini_columna_control_A; colum <= Columna_Final_B; colum++)
                        {
                            //recorriendo el detalle por nombre de las hora paso de los controles

                            var nombreParadero = getNombreParaderoxPosicionCol(colum, sentido, paraderosLadoA);
                            var horaPaso = hojaDespacho.GetCellValueAsString(row, colum).ToString() == "" ? "" : hojaDespacho.GetCellValueAsDateTime(row, colum).ToString();

                            if (horaPaso != "")
                            {
                                horaPaso = DateTime.Parse(horaPaso).ToString("HH:mm:ss");
                            }
                            var id_paradero = getIdParaderoByEtiqueta_CICLICA_B(nombreParadero, listaParaderos, sentido);
                            if (id_paradero != 0)
                            {
                                var rptaRegistroDetalle = _salidaLN.registrarSalidaHoraPasoParadero(id_salida_ejecutada.AUX, id_paradero, horaPaso, sentido, "WCUBAS", ref mensaje, ref tipo);
                            }
                            cantidadRegistrosGuardados_Horas_B++;
                        }

                        if (id_salida_ejecutada.COD_ESTADO == 0)
                        {
                            cantidadRegistrosFallidos++;

                        }
                        else
                        {
                            cantidadRegistrosGuardados++;

                        }
                    }

                    Total_CantidadRegistroGuardados = cantidadRegistrosGuardados;
                    Total_CantidadRegistroFallidos = cantidadRegistrosFallidos;
                    Total_Horas = cantidadRegistrosGuardados_Horas_A + cantidadRegistrosGuardados_Horas_B;


                }
                catch (TransactionAbortedException ex)
                {
                    e.DES_ESTADO = "No se registraron los datos, ERROR->" + ex.Message;
                }

            
            e.DES_ESTADO = (Total_CantidadRegistroGuardados > 0 ? Total_CantidadRegistroGuardados + " Viajes guardados y " + Total_Horas + " Horas  registrados exitosamente." : " Ocurrió un error no se han guardado los registros." + "->" + mensajeError);
            e.COD_ESTADO = 1;
            Conexion.finalizar(ref bdConn); //cierra conexion
            return JsonConvert.SerializeObject(e);
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
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static long ConvertToTimestamp(DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;
            return (long)elapsedTime.TotalSeconds;
        }
        public static bool BetweenDatetimesTimestamp(string input, string date1, string date2)
        {
            return (Int64.Parse(input) >= Int64.Parse(date1) && Int64.Parse(input) <= Int64.Parse(date2));
        }



    }
}