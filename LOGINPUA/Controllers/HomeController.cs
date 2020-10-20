using LOGINPUA.Util.Seguridad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LOGINPUA.Models;
using System.Data;
using ENTIDADES;
using LN.EntidadesLN;

namespace LOGINPUA.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
       

        public ActionResult mensaje_error()
        {
            Session.Abandon();
            Session.Clear();
            //return RedirectToAction("Login", "Usuario", new { script = "localStorage.setItem('storcs', 'crsn');" });
            return RedirectToAction("Login", "Usuarios");
        }

        public ActionResult Sistemas_(int? id_modalidad)
        {
            var id_usuario = (int)Session["id_usuario"];
            //mostrar cuentas de matrix
            //var List_Cuenta_Matrix = _Usuario_PersonaLN.Listar_Cuentas_x_Usuario(id_usuario);
            if (id_modalidad != null)
            {
                ViewBag.modalidad = id_modalidad;
            }
            var modalidadUsuario = Session["modalidadUsuario"];
            string json_modalidad = JsonConvert.SerializeObject(modalidadUsuario, Formatting.Indented);

            ViewBag.modalidadUsuario = json_modalidad;
            //ViewBag.CuentasMatrix = List_Cuenta_Matrix;

            return View();
        }




        public ActionResult Inicio(int id)
        {
            var menuUsuario = (DataTable)Session["menu_modulo"];

            // LOS MODULOS DE LA TATBLA 4
            Dictionary<int, MenuModels> dicMenu_Modulo = new Dictionary<int, MenuModels>();
            int contador = 0;
            foreach (DataRow row in menuUsuario.Rows)
            {
                if (Convert.ToInt32(row[6].ToString()) == id)
                {
                    var intmodalidad = int.Parse(row[10].ToString());

                    var idmenu = int.Parse(row[1].ToString());
                    var nomb_menu = row[2].ToString();
                    var url_menu = row[3].ToString();
                    var is_padre = int.Parse(row[4].ToString());
                    var id_menu_padre = int.Parse(row[5].ToString());

                    var id_modulo = int.Parse(row[6].ToString());
                    var nom_modulo = row[7].ToString();
                    var url_modulo = row[8].ToString();
                    var img_modulo = row[9].ToString();
                    var id_modalidad_ = int.Parse(row[10].ToString());

                    var menu = new MenuModels();

                    menu.idmenu = idmenu;
                    menu.nombmenu = nomb_menu;
                    menu.url_menu = url_menu;
                    menu.is_padre = is_padre;
                    menu.id_menu_padre = id_menu_padre;
                    menu.idmodulo = id_modulo;
                    menu.nomModulo = nom_modulo;
                    menu.urlmodulo = url_modulo;
                    menu.imgmodulo = img_modulo;
                    menu.id_modalidad = id_modalidad_;
                    dicMenu_Modulo.Add(contador, menu);

                    contador++;
                }
            }
            Session["modulo_session"] = "";
            Session["modulo_session"] = dicMenu_Modulo;
            Session["id_modulo"] = id;



            return View();


        }

        public string TipoModalidad(int id_modalidad)
        {
            var menuUsuario = (DataTable)Session["menu_modulo"];
            // LOS MODULOS DE LA TATBLA 4
            Dictionary<int, MenuModels> dicMenu_Modulo = new Dictionary<int, MenuModels>();
            int contador = 0;
            foreach (DataRow row in menuUsuario.Rows)
            {
                var intmodalidad = int.Parse(row[10].ToString());
                if (intmodalidad == id_modalidad)
                {
                    var idmenu = int.Parse(row[1].ToString());
                    var nomb_menu = row[2].ToString();
                    var url_menu = row[3].ToString();
                    var is_padre = int.Parse(row[4].ToString());
                    var id_menu_padre = int.Parse(row[5].ToString());

                    var id_modulo = int.Parse(row[6].ToString());
                    var nom_modulo = row[7].ToString();
                    var url_modulo = row[8].ToString();
                    var img_modulo = row[9].ToString();
                    var id_modalidad_ = int.Parse(row[10].ToString());

                    var menu = new MenuModels();

                    menu.idmenu = idmenu;
                    menu.nombmenu = nomb_menu;
                    menu.url_menu = url_menu;
                    menu.is_padre = is_padre;
                    menu.id_menu_padre = id_menu_padre;
                    menu.idmodulo = id_modulo;
                    menu.nomModulo = nom_modulo;
                    menu.urlmodulo = url_modulo;
                    menu.imgmodulo = img_modulo;
                    menu.id_modalidad = id_modalidad_;
                    dicMenu_Modulo.Add(contador, menu);

                    contador++;

                }

            }
            string json_menu = JsonConvert.SerializeObject(dicMenu_Modulo, Formatting.Indented);
            return json_menu;
        }
         
       


    }
}