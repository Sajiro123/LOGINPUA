using LN.EntidadesLN;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class ConsultarPlacaController : Controller
    {
        //private readonly BUSESLN _BusesLN;


        //public ConsultarPlacaController()
        //{
        //    _BusesLN = new BUSESLN();
        //}


        public ActionResult Consultar_Placa()
        {
            return View();
        }


        public string Consultar_x_placa(string placa)
        {

            BUSESLN _BusesLN = new BUSESLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _BusesLN.Consultar_x_placa(placa, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }


    }
}