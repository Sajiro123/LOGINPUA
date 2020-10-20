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
    public class PlacasConadisController : Controller
    {
        //private readonly PlacaFiscalizacionLN _PlacasFiscalizacion;
        //// GET: PlacasConadis
        //public PlacasConadisController()
        //{
        //    _PlacasFiscalizacion = new PlacaFiscalizacionLN();
        //}

        public ActionResult Index()
        {
            return View();
        }

        public string getplacasConadis()
        {
            PlacaFiscalizacionLN _PlacasFiscalizacion = new PlacaFiscalizacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            var rpta = "";
            //var placasConadis = "";
            try
            {
                rpta = JsonConvert.SerializeObject(_PlacasFiscalizacion.getPlacasConadis(ref mensaje, ref tipo));
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }

            return rpta;
        }

        public string getplacasVecinosEmp()
        {
            PlacaFiscalizacionLN _PlacasFiscalizacion = new PlacaFiscalizacionLN();

            String mensaje = "";
            Int32 tipo = 0;
            var rpta = "";
            //var placasConadis = "";
            try
            {
                rpta = JsonConvert.SerializeObject(_PlacasFiscalizacion.getplacasVecinosEmp(ref mensaje, ref tipo));
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }

            return rpta;
        }
    }
}