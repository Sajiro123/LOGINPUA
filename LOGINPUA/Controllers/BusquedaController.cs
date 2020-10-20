using LN.EntidadesLN;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class BusquedaController : Controller
    {
        //private readonly ConductoresLN _ConductoresLN;

        //public BusquedaController()
        //{
        //    _ConductoresLN = new ConductoresLN();

        //}
        // GET: Busquerda
        public ActionResult Consultar_Conductor_Personal()
        {
            return View();
        }

        public string Busqueda_Conductores_Personal(string nrodocuento)
        {
            ConductoresLN _ConductoresLN = new ConductoresLN();

            var result = "";
            String mensaje = "";
            Int32 tipo = 0;
            var conductor = _ConductoresLN.Conductor_x_DNI(nrodocuento, ref mensaje, ref tipo);
            result = JsonConvert.SerializeObject(conductor);

            if (conductor.ID_ESTADO==0)
            {
                result = "";
                var personal = _ConductoresLN.Personal_x_DNI(nrodocuento, ref mensaje, ref tipo);
                result = JsonConvert.SerializeObject(personal); 
            }
            return result;
        }
    }
}