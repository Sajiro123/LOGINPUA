using ENTIDADES;
using LN.EntidadesLN;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class ModuloController : Controller
    {
        // GET: Modulo
        //private readonly ModuloLN _ModuloLN;
        //Util.Util utilidades = new Util.Util();

        //public ModuloController()
        //{
        //    _ModuloLN =new ModuloLN();
        //}
        public ActionResult Mantenimiento_Modulo()
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

        public string AgregarModulo(int idmodalidad, string nombre, string url)
        {
            ModuloLN _ModuloLN = new ModuloLN();

            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = "AESPINOZA";
            var result = "";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var extension = Path.GetExtension(file.FileName);

                var rpta = _ModuloLN.AgregarModulo(idmodalidad, nombre, url, "", session_usuario, ref mensaje, ref tipo);
                var id_modulo = rpta.AUX;
                string id_modulo_final = id_modulo.ToString();
                var img_url = id_modulo_final + '-' + nombre + extension;


                var path = Path.Combine(Server.MapPath("~/Util/Modulo/"), img_url);

                var rpta_actualizar = _ModuloLN.modificarModulo_IMG(id_modulo, img_url, session_usuario, ref mensaje, ref tipo);


                file.SaveAs(path);

                result = JsonConvert.SerializeObject(rpta); //para la lista principal


            }
            return result;

        }
        public string ListarModalidad()
        {
            ModuloLN _ModuloLN = new ModuloLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _ModuloLN.ListarModalidad(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string ListarModulo(int id)
        {
            ModuloLN _ModuloLN = new ModuloLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _ModuloLN.ListarModulo(id, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }


        public string AnularModulo(int id)
        {
            ModuloLN _ModuloLN = new ModuloLN();
            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = Session["user_login"].ToString();

            var listar = _ModuloLN.AnularModulo(id, session_usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }
        public string modificarModulo(int idmodulo, int id_modalidad, string nombre, string url)
        {
            ModuloLN _ModuloLN = new ModuloLN();
            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = Session["user_login"].ToString();

            var listar = _ModuloLN.modificarModulo(idmodulo, id_modalidad, nombre, url, session_usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string Reemplazar_Img(string name_img)
        {
            RPTA_GENERAL rpta = new RPTA_GENERAL();
            var result = "";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var extension = Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Util/Modulo/"), name_img);
                file.SaveAs(path);
                rpta.DES_ESTADO = "Modificado Correctamente";
                rpta.COD_ESTADO = 1;

                result = JsonConvert.SerializeObject(rpta); //para la lista principal


            }


            //var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

    }
}