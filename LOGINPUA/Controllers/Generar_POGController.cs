using ENTIDADES;
using LN.EntidadesLN;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class Generar_POGController : Controller
    {

        //private readonly Generar_pogLN Generar_pogLN;


        //Util.Util utilidades = new Util.Util();

        //private readonly Generar_pogLN _Generar_pogLN;
        //private readonly CorredoresLN _CorredoresLN;
        //private readonly RutaLN _rutaLN;


        //public Generar_POGController()
        //{
        //    Generar_pogLN = new Generar_pogLN();

        //    _Generar_pogLN = new Generar_pogLN();
        //    _CorredoresLN = new CorredoresLN();
        //    _rutaLN = new RutaLN();
        //}

        // GET: Generar_POG
        public ActionResult Generar_Pog()
        {
            Generar_pogLN _Generar_pogLN = new Generar_pogLN();
            CorredoresLN _CorredoresLN = new CorredoresLN();
            RutaLN _rutaLN = new RutaLN();

            String mensaje = "";
            Int32 tipo = 0;
            _Generar_pogLN.Limpiar_Temporal_POG(ref mensaje, ref tipo);//LIMPIAR TEMPORAL
            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
            return View();
        }
        public ActionResult Reporte_Pog()
        {
            CorredoresLN _CorredoresLN = new CorredoresLN();
            RutaLN _rutaLN = new RutaLN();

            String mensaje = "";
            Int32 tipo = 0;
            //_Generar_pogLN.Limpiar_Temporal_POG(ref mensaje, ref tipo);//LIMPIAR TEMPORAL
            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.rutas = _rutaLN.getRutaCorredor(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
            return View();
        }
        public string Listar_Data_Pog(int id_ruta, string fecha)
        {
            String mensaje = "";
            Int32 tipo = 0;
            Generar_pogLN _Generar_pogLN = new Generar_pogLN();
            var result = "null";

            var rpt_fecha = _Generar_pogLN.ValidarFecha_Programados(fecha, id_ruta, ref mensaje, ref tipo);

            if (rpt_fecha.AUX > 0)
            {
                DataTable dt = new DataTable("resultado");
                dt.Columns.Add(new DataColumn("COD_ESTADO", typeof(int)));
                dt.Columns.Add(new DataColumn("fecha", typeof(string)));

                DataRow dr = dt.NewRow();
                dr["COD_ESTADO"] = 1;
                dr["fecha"] = rpt_fecha.DES_ESTADO;
                dt.Rows.Add(dr);

                var lista = _Generar_pogLN.Listar_Data_Pog(rpt_fecha.AUX, ref mensaje, ref tipo);
                lista.Tables.Add(dt);

                result = JsonConvert.SerializeObject(lista); //para la lista principal
                return result;
            }
            else
            {
                RPTA_GENERAL rpt = new RPTA_GENERAL();
                rpt.COD_ESTADO = 3;
                rpt.DES_ESTADO = "No existe información para esta fecha.";
                result = JsonConvert.SerializeObject(rpt); //para la lista principal
                return result;
            }

        }


        public string Listar_Franja_POG()
        {
            Generar_pogLN _Generar_pogLN = new Generar_pogLN();

            String mensaje = "";
            Int32 tipo = 0;
            var lista = _Generar_pogLN.Listar_Franja_POG(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }

        public JsonResult registrar_Maestro_POG(string fecha, int TipoDia, int reemplazar, int id_maestro, int id_ruta)
        {
            Generar_pogLN _Generar_pogLN = new Generar_pogLN();
            String mensaje = "";
            Int32 tipo = 0;
            fecha = fecha.Replace(" ", "");

            var rpt = new RPTA_GENERAL();
            string session_usuario = Session["user_login"].ToString();

            //VALIDAR FECHAS
            var rpt_fecha = _Generar_pogLN.ValidarFecha_Programados(fecha, id_ruta, ref mensaje, ref tipo);

            if (rpt_fecha.AUX > 0)
            {
                if (reemplazar == 0)
                {
                    rpt.AUX = rpt_fecha.AUX;
                    rpt.COD_ESTADO = 3;
                    rpt.DES_ESTADO = "¿Se reemplazara la programacion de la fecha " + rpt_fecha.DES_ESTADO + " ?";
                    return Json(rpt);
                }
                else
                {
                    _Generar_pogLN.Eliminar_Maestro_POG(id_maestro, ref mensaje, ref tipo);
                    rpt = _Generar_pogLN.registrar_Maestro_POG(fecha, TipoDia, id_ruta, session_usuario, ref mensaje, ref tipo);
                    return Json(rpt);

                }
            }
            else
            {
                rpt = _Generar_pogLN.registrar_Maestro_POG(fecha, TipoDia, id_ruta, session_usuario, ref mensaje, ref tipo);

            }

            return Json(rpt);
        }

        public string Registrar_Detalle_POG(int id_maestro_pog, string p_franja_hi, string p_franja_hf, string t_viaje_prom_a, string t_viaje_prom_b, string intervalo_a, string intervalo_b, string v_promedio_a, string v_promedio_b)
        {
            Generar_pogLN _Generar_pogLN = new Generar_pogLN();
            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = Session["user_login"].ToString();

            var lista = _Generar_pogLN.registrar_Detalle_Maestro_POG(id_maestro_pog, p_franja_hi, p_franja_hf, t_viaje_prom_a, t_viaje_prom_b, intervalo_a, intervalo_b, v_promedio_a, v_promedio_b, session_usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }
    }
}