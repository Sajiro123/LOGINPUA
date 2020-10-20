using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LN.EntidadesLN;
using System.Data;
using Newtonsoft.Json;
using ENTIDADES;
using LN;
using System.Transactions;
using System.Web.Script.Serialization;

namespace LOGINPUA.Controllers
{
    public class AnalisisTiempoViajeController : Controller
    {

        // Util.Util utilidades = new Util.Util();

        //private readonly AnalisisProgramacionLN _AnalisisProgramacion;
        //private readonly CorredoresLN _CorredoresLN;
        //private readonly RutaLN _rutaLN;
        //private readonly ModalidadTransporteLN _modalidadTransporteLN;
        //private readonly TipoServicioLN _tipoServicioLN;

        private Object bdConn;
        //public AnalisisTiempoViajeController()
        //{
        //    _AnalisisProgramacion = new AnalisisProgramacionLN();
        //    _CorredoresLN = new CorredoresLN();
        //    _rutaLN = new RutaLN();
        //    _modalidadTransporteLN = new ModalidadTransporteLN();
        //    _tipoServicioLN = new TipoServicioLN();
        //}

        // GET: AnalisisTiempoViaje
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
            //
            ViewBag.currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.currentRoute = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (auxValida)
            {
                var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
                ViewBag.rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);
                ViewBag.modalidadTransporte = _modalidadTransporteLN.getModalidadTransporte(ref mensaje, ref tipo);
                ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
                ViewBag.currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
                ViewBag.currentRoute = this.ControllerContext.RouteData.Values["controller"].ToString();
                ViewBag.modalidadUsuario = 2; //[1 : CORREDOR ] -- [2: COSAC] esto viene del usuario
                ViewBag.Accionesview = Lista_acciones;
                return View();
            }
            else
            {
                return RedirectToAction("Sistemas_", "Home");
            }

            //var datosCorredores = _CorredoresLN.obtenerListaCorredores();
            //ViewBag.rutas = _rutaLN.getRutaCorredor();
            //ViewBag.modalidadTransporte = _modalidadTransporteLN.getModalidadTransporte();
            //ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
            //ViewBag.currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            //ViewBag.currentRoute = this.ControllerContext.RouteData.Values["controller"].ToString();
            //ViewBag.modalidadUsuario = 2; //[1 : CORREDOR ] -- [2: COSAC] esto viene del usuario
            //return View();
        }
        public ActionResult ReporteViajes()
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
            //
            ViewBag.currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.currentRoute = this.ControllerContext.RouteData.Values["controller"].ToString();
            //if (auxValida)
            //{
            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);
            ViewBag.modalidadTransporte = _modalidadTransporteLN.getModalidadTransporte(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
            ViewBag.currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.currentRoute = this.ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.modalidadUsuario = 2; //[1 : CORREDOR ] -- [2: COSAC] esto viene del usuario
            ViewBag.Accionesview = Lista_acciones;
            return View();
            //}
            //else
            //{
            //    return RedirectToAction("Sistemas_", "Home");
            //}

            //var datosCorredores = _CorredoresLN.obtenerListaCorredores();
            //ViewBag.rutas = _rutaLN.getRutaCorredor();
            //ViewBag.modalidadTransporte = _modalidadTransporteLN.getModalidadTransporte();
            //ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
            //ViewBag.currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            //ViewBag.currentRoute = this.ControllerContext.RouteData.Values["controller"].ToString();
            //ViewBag.modalidadUsuario = 2; //[1 : CORREDOR ] -- [2: COSAC] esto viene del usuario
            //return View();
        }
        public string registrarViajesCosac(string fechaRegistra, int codModalidadTransporte) //OBTIENE DATA DESDE BD SQL SERVER 
        {
            AnalisisProgramacionLN _AnalisisProgramacion = new AnalisisProgramacionLN();

            String mensaje = "";
            Int32 tipo = 0;
            RPTA_GENERAL r = new RPTA_GENERAL();
            //################### TRABAJANDO CON LA DATA DE [ 14/10/2019 - 20/10/2019] ############################
            Dictionary<string, DataTable> respuesta = new Dictionary<string, DataTable>();
            DataSet Ds = new DataSet();
            //*******************obtiendo las rutas por modalidad (COSAC)
            var dataRutas = _AnalisisProgramacion.getRutasByModalidadTransporte(codModalidadTransporte, ref mensaje, ref tipo); //2 COSAC
            string FECHA_REGISTRA = fechaRegistra;
            var CANTIDAD_EXITO = 0;
            foreach (var item in dataRutas) //aqui recorre las rutas que tiene la modalidad transporte (COSAC) 
            {
                if (item.NOMBRE_ETIQUETA != null)
                {
                    try
                    {
                        var dataViajes = _AnalisisProgramacion.verificarDataParaAnalisis(fechaRegistra, item.ID_RUTA_TIPO_SERVICIO, ref mensaje, ref tipo);

                        if (dataViajes.Count > 0)
                        {
                            r.DES_ESTADO += "[" + item.NOMBRE_ETIQUETA + "] Ya hay información para esta ruta en el dia " + FECHA_REGISTRA + ".";
                        }
                        else
                        {

                            Ds = new ViajeCOSAC_LN().consultaViajesCOSAC(item.NOMBRE_ETIQUETA.Trim(), FECHA_REGISTRA, ref mensaje, ref tipo);

                            using (TransactionScope scope = new TransactionScope())
                            {
                                try
                                {
                                    if (Ds.Tables[0].Rows.Count > 0)
                                    {
                                        var rptaRegistra = _AnalisisProgramacion.registrarMaestroViajeCOSAC(item.ID_RUTA_TIPO_SERVICIO, FECHA_REGISTRA, Session["user_login"].ToString(), ref mensaje, ref tipo);//Registra MaestroViaje 
                                        var idMaestroViaje = 0;
                                        var cantidadViajesRegistradosExitosamente = 0;
                                        //

                                        //
                                        if (rptaRegistra.AUX != 0 && rptaRegistra.COD_ESTADO == 1)
                                        {
                                            idMaestroViaje = rptaRegistra.AUX;
                                            foreach (DataRow row in Ds.Tables[0].Rows)
                                            {
                                                var fecha = row[0];
                                                var hProgramada = row[1].ToString();
                                                var hEjecutada = row[2].ToString();
                                                var idEstacion = row[3];
                                                var estacion = row[4].ToString();
                                                var abrevEstacion = row[5].ToString();
                                                var abrevRuta = row[6].ToString();
                                                var block = Int32.Parse(row[7].ToString());
                                                var idUnidad = Int32.Parse(row[8].ToString());
                                                var sentido = row[9].ToString();
                                                var seq_viaje = Int32.Parse(row[10].ToString());
                                                var id_viaje = Int32.Parse(row[11].ToString());
                                                var fecha_hora_paso_registro = row[12].ToString();

                                                var rptaRegistraDetalle = _AnalisisProgramacion.registrarMDetalleViajeCOSAC(idMaestroViaje, hProgramada, hEjecutada, estacion, abrevEstacion,
                                                                                                                            abrevRuta, block, sentido, seq_viaje, idUnidad, id_viaje, fecha_hora_paso_registro, Session["user_login"].ToString(), ref mensaje, ref tipo);//Registra MaestroViajeDetalle
                                                if (rptaRegistraDetalle.COD_ESTADO == 1)
                                                {
                                                    cantidadViajesRegistradosExitosamente++;
                                                }
                                            }
                                        }
                                        CANTIDAD_EXITO = cantidadViajesRegistradosExitosamente;
                                        r.COD_ESTADO = 1;
                                        r.DES_ESTADO += "[" + item.NOMBRE_ETIQUETA.Trim() + "]" + " se registraron " + CANTIDAD_EXITO.ToString();
                                    }
                                    else
                                    {
                                        r.COD_ESTADO = 0;
                                        r.DES_ESTADO += "[" + item.NOMBRE_ETIQUETA.Trim() + "] Detalle de viajes en el orbcat es cero.\n";
                                    }
                                }
                                catch (TransactionAbortedException ex)
                                {
                                    r.COD_ESTADO = 0;
                                    r.DES_ESTADO += "se registraron " + ex.Message;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        r.COD_ESTADO = 0;
                        r.DES_ESTADO = "ocurrio un error " + ex.Message;
                    }
                }
            }
            AD.Conexion.finalizar(ref bdConn); //cierra conexion
            return JsonConvert.SerializeObject(r, Formatting.Indented);
        }

        public RPTA_GENERAL registrarMaestroViajeCOSAC(int idRutaTipoServicio, string fecha)
        {
            AnalisisProgramacionLN _AnalisisProgramacion = new AnalisisProgramacionLN();

            String mensaje = "";
            Int32 tipo = 0;
            var datos = _AnalisisProgramacion.registrarMaestroViajeCOSAC(idRutaTipoServicio, fecha, Session["user_login"].ToString(), ref mensaje, ref tipo);
            return datos;
        }

        public string getDataMuestraViajesCOSAC(int idRutatipoServicio, string fechaConsultaIni, string fechaConsultaFin)
        {
            AnalisisProgramacionLN _AnalisisProgramacion = new AnalisisProgramacionLN();

            String mensaje = "";
            Int32 tipo = 0;
            var datos = _AnalisisProgramacion.getDataMuestraViajesCOSAC(idRutatipoServicio, fechaConsultaIni, fechaConsultaFin, ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(datos, Formatting.Indented);
        }


        public string getDataMuestraViajesCORREDORES(int idRutatipoServicio, string fechaConsultaIni, string fechaConsultaFin)
        {
            AnalisisProgramacionLN _AnalisisProgramacion = new AnalisisProgramacionLN();

            String mensaje = "";
            Int32 tipo = 0;
            var datos = _AnalisisProgramacion.getDataMuestraViajesCORREDORES(idRutatipoServicio, fechaConsultaIni, fechaConsultaFin, ref mensaje, ref tipo);
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = 500000000;
            string jsonData = js.Serialize(datos);
            return jsonData;// jsonData;
            //return JsonConvert.SerializeObject(datos, Formatting.Indented);
        }

        //public List<CC_DATA_ANALISIS_PROG_CORREDORES> getDataMuestraViajesCORREDORES(int idRutatipoServicio, string fechaConsultaIni, string fechaConsultaFin)
        //{
        //    var datos = _AnalisisProgramacion.getDataMuestraViajesCORREDORES(idRutatipoServicio, fechaConsultaIni, fechaConsultaFin);
        //    //return JsonConvert.SerializeObject(datos, Formatting.Indented);
        //    return datos;
        //}

        //public CC_DATA_ANALISIS_PROG_CORREDORES getDataMuestraViajesCORREDORES(int idRutatipoServicio, string fechaConsultaIni, string fechaConsultaFin)
        //{
        //    CC_DATA_ANALISIS_PROG_CORREDORES e = new CC_DATA_ANALISIS_PROG_CORREDORES();
        //    e.FECHA = "10/01/2020";

        //    //var datos = _AnalisisProgramacion.getDataMuestraViajesCORREDORES(idRutatipoServicio, fechaConsultaIni, fechaConsultaFin);
        //    //return JsonConvert.SerializeObject(datos, Formatting.Indented);
        //    return e;
        //}

        public string getRecorridosTServByRutaServ(int idRutatipoServicio)
        {
            AnalisisProgramacionLN _AnalisisProgramacion = new AnalisisProgramacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            var datos = _AnalisisProgramacion.getRecorridosTServByRutaServ(idRutatipoServicio, ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(datos, Formatting.Indented);
        }

        public List<CC_RUTA_TIPO_SERVICIO> obtenerRutasPorModalidadTransporte(int idModalidadTransporte)
        {
            AnalisisProgramacionLN _AnalisisProgramacion = new AnalisisProgramacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            var datos = _AnalisisProgramacion.getRutasByModalidadTransporte(idModalidadTransporte, ref mensaje, ref tipo);
            return datos;
        }

        public string getParaderoTServByRecTserv(int idRecorridoTipoServicio)
        {
            AnalisisProgramacionLN _AnalisisProgramacion = new AnalisisProgramacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            var datos = _AnalisisProgramacion.getParaderoTServByRecTserv(idRecorridoTipoServicio, ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(datos, Formatting.Indented);
        }

        public string getViajesPorRuta(int id_ruta, string fechaConsulta, string lado)
        {
            AnalisisProgramacionLN _AnalisisProgramacion = new AnalisisProgramacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            string fechaConsulta_cambiada = fechaConsulta.Replace(" ", "");
            fechaConsulta_cambiada = fechaConsulta_cambiada.Replace("-", "|");

            Dictionary<string, DataTable> respuesta = new Dictionary<string, DataTable>();
            DataSet Ds = new DataSet();

            Ds = _AnalisisProgramacion.getViajesPorRuta(id_ruta, fechaConsulta_cambiada, lado, ref mensaje, ref tipo);
            var i = 0;
            foreach (DataTable dt in Ds.Tables)
            {
                respuesta.Add("dt" + i, dt);
                i++;
            }
            return JsonConvert.SerializeObject(respuesta, Formatting.Indented);
        }
        public string actualizarPromTemp(int id_temporal, double vel_prom, string tiempo_prom, string lado)
        {
            AnalisisProgramacionLN _AnalisisProgramacion = new AnalisisProgramacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            var datos = _AnalisisProgramacion.actualizarPromTemp(id_temporal, vel_prom, tiempo_prom, lado, ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(datos, Formatting.Indented);
        }
    }
}