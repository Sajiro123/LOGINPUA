using ENTIDADES;
using LN.EntidadesLN;
using LOGINPUA.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class PerfilController : Controller
    {
        //private readonly PerfilLN _PerfilLN;
        //Util.Util utilidades = new Util.Util();


        //public PerfilController()
        //{
        //    _PerfilLN =new PerfilLN();
        //}
        // GET: Perfil
        public ActionResult Perfil_Inicio()
        {
            Util.Util utilidades = new Util.Util();

            var listaVistas = Session["menu_modulo"] as DataTable;
            var listacciones = Session["menu_acciones"] as List<CC_MENUPERFIL_ACCION>;
            //Session["menu_modulo"] = dt4;
            var nombreActionCurrent = this.ControllerContext.RouteData.Values["action"].ToString();
            bool auxValida = utilidades.validaVistaxPerfil(nombreActionCurrent, listaVistas);

            ViewBag.currentAction = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.currentRoute = this.ControllerContext.RouteData.Values["controller"].ToString();

 
            var Lista_acciones = utilidades.validadAccionMenu(listacciones, nombreActionCurrent, this.ControllerContext.RouteData.Values["controller"].ToString());

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

        public string listarPerfil(int idmodalidad)
        {
            PerfilLN _PerfilLN = new PerfilLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _PerfilLN.ListarPerfil(idmodalidad, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }
        public string listarModalidad()
        {
            PerfilLN _PerfilLN = new PerfilLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _PerfilLN.ListarModalidad(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string ModificarPerfil(int idmodalidad, int id_modalidad_perfil, string nombre_perfil)
        {
            PerfilLN _PerfilLN = new PerfilLN();
            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = Session["user_login"].ToString();
            var listar = _PerfilLN.ModificarPerfil(idmodalidad, id_modalidad_perfil, nombre_perfil, session_usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }
        public string AnularPerfil(int idperfil,string nombre_perfil)
        {
            PerfilLN _PerfilLN = new PerfilLN();
            String mensaje = "";
            Int32 tipo = 0;
            var rpta = new RPTA_GENERAL();
            string session_usuario = Session["user_login"].ToString();

            var datos = _PerfilLN.Validar_Perfil(idperfil, ref mensaje, ref tipo);

            if (datos.ID_PERFIL ==0)
            {
                rpta = _PerfilLN.AnularPerfil(idperfil, session_usuario, ref mensaje, ref tipo);
            }
            else
            {
                rpta.COD_ESTADO = 0;
                rpta.DES_ESTADO = "Existen usuario con el perfil";
            }


            var result = JsonConvert.SerializeObject(rpta); //paraa anular
            return result;
        }
        public string AgregarPerfil(string nombre, int idmodalidad)
        {
            PerfilLN _PerfilLN = new PerfilLN();
            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = SessionHelper.user_login;

            //Se registra el Perfil
            var rpt = _PerfilLN.AgregarPerfil(nombre, session_usuario, ref mensaje, ref tipo);
            var id_perfil = rpt.AUX;

            if (id_perfil != 0)
            {
                var rpt_final = _PerfilLN.AgregarPerfil_modalidad(id_perfil, idmodalidad, session_usuario, ref mensaje, ref tipo);

            }

            var result = JsonConvert.SerializeObject(rpt); //para agregar
            return result;
        }

    }
}