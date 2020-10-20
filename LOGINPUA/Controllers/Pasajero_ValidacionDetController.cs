using AD;
using AD.EntidadesAD;
using ENTIDADES;
using LN.EntidadesLN;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class Pasajero_ValidacionDetController : Controller
    {
        private Object bdConn;
        public ActionResult SubirValidacionDetallado()
        {
            CorredoresLN _CorredoresLN = new CorredoresLN();

            String mensaje = "";
            Int32 tipo = 0;
            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
            return View();
        }
        public ActionResult ReporteValidacionPasajeros()
        {
            CorredoresLN _CorredoresLN = new CorredoresLN();


            String mensaje = "";
            Int32 tipo = 0;
            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);

            return View();
        }

        public ActionResult Calcular_Intervalos()
        {
            CorredoresLN _CorredoresLN = new CorredoresLN();


            String mensaje = "";
            Int32 tipo = 0;
            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);

            return View();
        }

        public string Calcular_franja(int id_ruta, string fechas, string lado)
        {
            PasajeroValidacionLN _PasajeroValidacionLN = new PasajeroValidacionLN();

            String mensaje = "";
            Int32 tipo = 0;
            string fechaFin_cambiada = fechas.Replace(" ", "");
            fechaFin_cambiada = fechaFin_cambiada.Replace("-", "|");


            var rpta = _PasajeroValidacionLN.Calcular_franja(id_ruta, fechaFin_cambiada, lado, ref mensaje, ref tipo);
            return JsonConvert.SerializeObject(rpta);
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

        public string cargarArchivo(int idRuta, string fecha, int reemplazar, string abrevProveedorSistemasCorredor)
        {

            var _PasajeroValidadacion = new PasajeroValidacionAD(ref bdConn);
            var lparadero = _PasajeroValidadacion.paraderos_x_ID(idRuta);
            Conexion.finalizar(ref bdConn);


            PasajeroValidacionLN _PasajeroValidacionLN = new PasajeroValidacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = Session["user_login"].ToString();

            var ob = Request.Files;
            var archivoSubido = Request.Files[0];
            var nombreArchivo = Path.GetFileName(archivoSubido.FileName);
            var extensionArchivo = Path.GetExtension(archivoSubido.FileName);
            var arrFileName = nombreArchivo.Split('.');
            var nuevoNombreArchivoExcel = arrFileName[0] + "_FILE_" + DateTime.Now.ToString("dd_MM_yyyy_h_mm_ss") + extensionArchivo;
            var pathArchivo = Server.MapPath("~/Adjuntos/Files_Despacho/" + nuevoNombreArchivoExcel);
            string pathFinal = Path.Combine(pathArchivo);
            archivoSubido.SaveAs(pathFinal);

            var rpta = _PasajeroValidacionLN.importarExcelPasajeros(idRuta, fecha, reemplazar, abrevProveedorSistemasCorredor, pathFinal, lparadero, session_usuario, ref mensaje, ref tipo);

            return JsonConvert.SerializeObject(rpta);
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

        public string Guardar_Temporal_POG(string[] intervalos, string lado)
        {
            PasajeroValidacionLN _PasajeroValidacionLN = new PasajeroValidacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            var rpt = new RPTA_GENERAL();

            int id = 0;
            foreach (var intervalo in intervalos)
            {
                id++;
                _PasajeroValidacionLN.Agregar_Temporal_POG(id, lado, intervalo, ref mensaje, ref tipo);
            }
            return JsonConvert.SerializeObject(rpt);
        }
    }
}