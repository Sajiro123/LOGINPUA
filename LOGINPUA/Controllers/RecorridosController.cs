using LN.EntidadesLN;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class RecorridosController : Controller
    {
        // GET: Recorridos
        public ActionResult Mantenimiento()
        {
            return View();
        }


        public string Listar_Recorridos()
        {
            CorredoresLN _CorredoresLN = new CorredoresLN();
            Generar_pogLN _Generar_pogAD = new Generar_pogLN();

            String mensaje = "";
            Int32 tipo = 0;

            var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
            ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);

            var lista = _Generar_pogAD.Listar_Recorrido(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }
    }
}