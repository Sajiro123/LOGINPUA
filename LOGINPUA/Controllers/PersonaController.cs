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
    public class PersonaController : Controller
    {
        // GET: Persona

        //private readonly PersonaLN _PersonaLN;
        //Util.Util utilidades = new Util.Util();


        //public PersonaController()
        //{

        //    _PersonaLN =new PersonaLN();
        //}
        public ActionResult MantenimientoPerson()
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

        public ActionResult Actualizar_Datos()
        {

            return View();
        }

        public string ListarTipoDocumento()
        {
            PersonaLN _PersonaLN = new PersonaLN();

            String mensaje = "";
            Int32 tipo = 0;
            var listar = _PersonaLN.ListarTipoDocumento(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string listarTipoRol()
        {
            PersonaLN _PersonaLN = new PersonaLN();

            String mensaje = "";
            Int32 tipo = 0;
            var listar = _PersonaLN.listarTipoRol(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string AgregarPersona(string nombre, string apepaterno, string apematerno, string numdocu, int tipodocu, string correo, int tiporol)
        {
            PersonaLN _PersonaLN = new PersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = Session["user_login"].ToString();


            var listar = _PersonaLN.registrarPersona(nombre, apepaterno, apematerno, numdocu, tipodocu, correo, tiporol, session_usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string getlistaPersonas()
        {
            PersonaLN _PersonaLN = new PersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _PersonaLN.getlistaPersonas(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string ModificarPersonas(int idpersona, string nombre, string apepaterno, string apematerno, int numdocu, int tipodocu, string correo, int tiporol)
        {
            PersonaLN _PersonaLN = new PersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            var modificar = _PersonaLN.ModificarPersonas(idpersona, nombre, apepaterno, apematerno, numdocu, tipodocu, correo, tiporol, Session["user_login"].ToString(), ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(modificar); //para la lista principal
            return result;
        }

        public string EliminarPersonas(int idpersona)
        {
            PersonaLN _PersonaLN = new PersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = Session["user_login"].ToString();
            var eliminar = _PersonaLN.EliminarPersonas(idpersona, session_usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(eliminar); //para la lista principal
            return result;
        }

        public string EditarDatosPersona(int idpersona,string nombre,string apepaterno,string apematerno, string numdocu, string correo)
        {
            PersonaLN _PersonaLN = new PersonaLN();
            String mensaje = "";
            Int32 tipo = 0;
            string session_usuario = Session["user_login"].ToString();
            var eliminar = _PersonaLN.EditarDatosPersonas(idpersona,nombre,apepaterno,apematerno,numdocu,correo, session_usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(eliminar); //para la lista principal
            return result;
        }

    }
}