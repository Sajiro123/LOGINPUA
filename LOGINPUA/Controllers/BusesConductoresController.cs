using ENTIDADES;
using LN.EntidadesLN;
using LOGINPUA.Util.Seguridad;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class BusesConductoresController : Controller
    {
        // GET: BusesConductores
      
        public ActionResult Inicio()
        {
            var session_Perfil = int.Parse(Session["perfil_corredor"].ToString());
            ViewBag.id_perfilcc = session_Perfil;

            CorredoresLN _CorredoresLN = new CorredoresLN();
            String mensaje = "";
            Int32 tipo = 0;

            ViewBag.ACTION_REEMPLAZAR_DATOS = false;

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
                var datosCorredores = _CorredoresLN.obtenerListaCorredores(ref mensaje, ref tipo);
                ViewBag.corredores = JsonConvert.SerializeObject(datosCorredores);
                return View();
            }
            else
            {
                return RedirectToAction("Sistemas_", "Home");
            }
        }

        public JsonResult EnviarCorreo(string para, string copia, string asunto, string nombreadjunto,string cuerpo)
        {

            var usuario = Session["user_datos"] as List<ENTIDADES.CC_PERSONA>;
            var correo = "";
            var nombre = "";
            foreach (var item in usuario)
            {
                correo = item.CORREO;
                nombre = item.NOMBRES +" "+ item.APEPAT + " " + item.APEMAT;           
            }

            var adjunto = Server.MapPath("~/Adjuntos/BusesConductores/" + nombreadjunto);
            Int32 tipo = 0;
            String mensaje = "";

            //Se divide mediante la coma los diferentes correos, se limpia espacios en blanco y se valida que no haya vacio entre comas
            var correos_para = para.Split(',').Select(x => x.Trim()).Where(x => x!="").ToArray();
            var copia_para = copia.Split(',').Select(x => x.Trim()).Where(x => x != "").ToArray();

            //se agrega los correos en una lista
            var Listacorreospara = new List<string>();
            for (int i = 0; i < correos_para.Length ; i ++)
            {
                Listacorreospara.Add(correos_para[i]);
            }
            //se agrega las copias ocultas en una lista
            var Listacopiapara = new List<string>();
            for (int i = 0; i < copia_para.Length; i++)
            {
                Listacopiapara.Add(copia_para[i]);
            }

            //se obtiene la clave del usuario
            String user_pass = Encriptador.Desencriptar(Session["user_password"].ToString());

            //se hace uso del servicio para enviar correo
            Util.Servicio.EnvioCorreo(Listacorreospara, Listacopiapara, correo, user_pass, asunto, new List<string> { }, cuerpo, adjunto, ref tipo, ref mensaje);


            return Json(new {tipo = tipo, mensaje = mensaje });
        }

        public string Listar_Informes(string mes_año)
        {
           BusesConductoresLN ProduccionDemandaLN = new BusesConductoresLN();
            CorredoresLN _CorredoresLN = new CorredoresLN();
            RPTA_GENERAL e = new RPTA_GENERAL();

            String mensaje = "";
            Int32 tipo = 0;
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


            var Lista_informes = ProduccionDemandaLN.Listar_Informes(mes, año, ref mensaje, ref tipo);
            var rpta = JsonConvert.SerializeObject(Lista_informes);
            return rpta;
        }

        public string CargarArchivo(string fecha, int reemplazar)
        {

            BusesConductoresLN BusesDespachoLN = new BusesConductoresLN();
            var Rpta = new RPTA_GENERAL();
            ViewBag.ACTION_REEMPLAZAR_DATOS = true;
            String mensaje = "";
            Int32 tipo = 0;
            var nuevoNombreArchivoExcel = "";
            var rpt_result = BusesDespachoLN.ValidarFecha_Programados(fecha, ref mensaje, ref tipo);
             
            if (rpt_result.AUX > 0)
            {
                var id = rpt_result.AUX;
                if (reemplazar == 0)
                {
                    Rpta.AUX = rpt_result.AUX;
                    Rpta.COD_ESTADO = 3;
                    Rpta.DES_ESTADO = "¿Se reemplazara el archivo de la fecha " + rpt_result.DES_ESTADO + " ?";
                    return JsonConvert.SerializeObject(Rpta);
                }
                else
                {
                    var nombre = BusesDespachoLN.nombre_excel(id, ref mensaje, ref tipo);
                    System.IO.File.Delete(Server.MapPath("~/Adjuntos/BusesConductores/" + nombre.DES_ESTADO));
                    BusesDespachoLN.eliminar_Archivo(id, ref mensaje, ref tipo);

                    var ob = Request.Files;
                    var archivoSubido = Request.Files[0];
                    var nombreArchivo = Path.GetFileName(archivoSubido.FileName);
                    var extensionArchivo = Path.GetExtension(archivoSubido.FileName);
                    var arrFileName = nombreArchivo.Split('.');
                    nuevoNombreArchivoExcel = arrFileName[0] + extensionArchivo;
                    var pathArchivo = Server.MapPath("~/Adjuntos/BusesConductores/" + nuevoNombreArchivoExcel);
                    string pathFinal = Path.Combine(pathArchivo);
                    archivoSubido.SaveAs(pathFinal);

                    Rpta = BusesDespachoLN.RegistroInforme(nuevoNombreArchivoExcel, fecha, ref mensaje, ref tipo);
                    return JsonConvert.SerializeObject(Rpta);
                }
            }
            else
            {
                var ob = Request.Files;
                var archivoSubido = Request.Files[0];
                var nombreArchivo = Path.GetFileName(archivoSubido.FileName);
                var extensionArchivo = Path.GetExtension(archivoSubido.FileName);
                var arrFileName = nombreArchivo.Split('.');
                nuevoNombreArchivoExcel = arrFileName[0] + extensionArchivo;
                var pathArchivo = Server.MapPath("~/Adjuntos/BusesConductores/" + nuevoNombreArchivoExcel);
                string pathFinal = Path.Combine(pathArchivo);
                archivoSubido.SaveAs(pathFinal);
                Rpta = BusesDespachoLN.RegistroInforme(nuevoNombreArchivoExcel, fecha, ref mensaje, ref tipo);

            }
            var adjunto = Server.MapPath("~/Adjuntos/BusesConductores/" + nuevoNombreArchivoExcel);

            //Lista de correos para ya definida por el usuario
            var Listacorreospara = new List<string>();
                //correo prueba
                //Listacorreospara.Add("willy.espinoza@atu.gob.pe");

                Listacorreospara.Add("mvizcarra@atu.gob.pe");

            //Lista de correos con copia ya definida por el usuario
            var Listacopiapara = new List<string>();
            //correo prueba
            //Listacopiapara.Add("pierre.rodriguez@atu.gob.pe");
            
                Listacopiapara.Add("jose.hermitano@atu.gob.pe");
                Listacopiapara.Add("do10@atu.gob.pe");
                Listacopiapara.Add("do30@atu.gob.pe");
                Listacopiapara.Add("do25@atu.gob.pe");
                Listacopiapara.Add("sstr57@atu.gob.pe");
                Listacopiapara.Add("sstr44@atu.gob.pe");
                Listacopiapara.Add("sstr81@atu.gob.pe");
                Listacopiapara.Add("sstr83@atu.gob.pe");
                Listacopiapara.Add("raul.vasquez@atu.gob.pe");
                Listacopiapara.Add("supervmttosit@atu.gob.pe");

             //Asunto predefinido
             var asunto = "Placas Programadas Corredores Complementarios " + nuevoNombreArchivoExcel.Substring(0,2)+"/"+nuevoNombreArchivoExcel.Substring(2,2)+"/" + nuevoNombreArchivoExcel.Substring(4,4);

            //Cuerpo del mensaje
            var cuerpo = System.IO.File.ReadAllText(Server.MapPath("~/Util/CuerpoCorreo.txt"));

            //Parametros del cuerpo
            var parametro_cuerpo = new List<String>();

            //Saludo dependiendo de la hora, parametro 0
            var hora_del_dia_saludo = DateTime.Now;
            if (hora_del_dia_saludo.Hour>=5 && hora_del_dia_saludo.Hour < 12)
            {
                parametro_cuerpo.Add("Buenos días");
            }
            else if (hora_del_dia_saludo.Hour >= 12 && hora_del_dia_saludo.Hour < 18)
            {
                parametro_cuerpo.Add("Buenas tardes");
            }
            else
            {
                parametro_cuerpo.Add("Buenas noches");
            }

            //fecha del dia siguiente para el parametro 1
            parametro_cuerpo.Add(DateTime.Now.AddDays(1).ToString("dd/mm/yyyy"));
            //correo y nombres del usuario, parametro 3 y 4
            var usuario = Session["user_datos"] as List<ENTIDADES.CC_PERSONA>;
            var correo = "";
            var nombre_usu = "";
            foreach (var item in usuario)
            {
                correo = item.CORREO;
                nombre_usu = item.NOMBRES + " " + item.APEPAT + " " + item.APEMAT;
            }
            parametro_cuerpo.Add(nombre_usu);
            parametro_cuerpo.Add(correo);

            String user_pass = Encriptador.Desencriptar(Session["user_password"].ToString());

            Util.Servicio.EnvioCorreo(Listacorreospara, Listacopiapara, correo, user_pass, asunto, parametro_cuerpo, cuerpo, adjunto, ref tipo, ref mensaje);
            return JsonConvert.SerializeObject(Rpta);
        }

        public string ver_excelFinal(string resultado)
        {
            var pathArchivo = Server.MapPath("~/Adjuntos/BusesConductores/");
            string name_page = "verExcel.html";


            try
            {
                System.IO.File.Delete(Server.MapPath(pathArchivo + name_page));
            }
            catch (Exception)
            {
            }

            Workbook workbook = new Workbook();
            workbook.LoadFromFile(Server.MapPath("~/Adjuntos/BusesConductores/" + resultado));
            Worksheet sheet = workbook.Worksheets[6];
            var ruta = pathArchivo + name_page;
            sheet.SaveToHtml(pathArchivo + name_page);

            string text = System.IO.File.ReadAllText(pathArchivo + name_page);
            text = text.Replace("Evaluation&nbsp;Warning&nbsp;:&nbsp;The&nbsp;document&nbsp;was&nbsp;created&nbsp;with&nbsp;&nbsp;Spire.XLS&nbsp;for&nbsp;.NET", "<br/>");
            System.IO.File.WriteAllText(pathArchivo + name_page, text);

            //leer html 
            return JsonConvert.SerializeObject(name_page);
        }
    }
}