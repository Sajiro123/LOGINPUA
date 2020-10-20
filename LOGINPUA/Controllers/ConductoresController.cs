using ENTIDADES;
using LN.EntidadesLN;
using LOGINPUA.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class ConductoresController : Controller
    {
        //private readonly LoginLN _LoginLN;

        //private readonly ConductoresLN _ConductoresLN;
        ////CC_CONDUCTORES model = new CC_CONDUCTORES();

        //public ConductoresController()
        //{
        //    _LoginLN = new LoginLN();
        //    _ConductoresLN = new ConductoresLN();
        //}


        public ActionResult Conductores()
        {
            return View();
        }

        public string ListarConductores()
        {
            ConductoresLN _ConductoresLN = new ConductoresLN();
            String mensaje = "";
            Int32 tipo = 0;
            var lista = _ConductoresLN.Listar_Conductores(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(lista); //para la lista principal
            return result;
        }

    }
}
