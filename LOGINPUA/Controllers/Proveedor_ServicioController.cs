using ENTIDADES;
using LN.EntidadesLN;
using LOGINPUA.Util.Seguridad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class Proveedor_ServicioController : Controller
    {
        // GET: Proovedor_Servicio
        //private readonly Proveedor_ServLN _Proveedor_ServLN;
        //private readonly Usuario_PersonaLN _Usuario_PersonaLN;
        //Util.Util utilidades = new Util.Util();

        //public Proveedor_ServicioController()
        //{
        //    _Proveedor_ServLN = new Proveedor_ServLN(); 
        //    _Usuario_PersonaLN = new Usuario_PersonaLN(); ;
        //}

        public ActionResult Inicio()
        {
            Proveedor_ServLN _Proveedor_ServLN = new Proveedor_ServLN();
            Util.Util utilidades = new Util.Util();

            String mensaje = "";
            Int32 tipo = 0;
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
                var listaUsuario = _Proveedor_ServLN.ListarUsuarios(ref mensaje, ref tipo);
                ViewBag.listaUsuario = listaUsuario;
                return View();
            }
            else
            {
                return RedirectToAction("Sistemas_", "Home");
            }
        }


        public string Desactivar_Cuenta_Usuario(int id_prov_usuario)
        {
            Proveedor_ServLN _Proveedor_ServLN = new Proveedor_ServLN();
            String mensaje = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();
            var rpta = _Proveedor_ServLN.Desactivar_Cuenta_Usuario(id_prov_usuario, usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(rpta);
            return result;
        }
        public string Actualizar_ContraseñaAnti(int id_prov_usuario)
        {
            Proveedor_ServLN _Proveedor_ServLN = new Proveedor_ServLN();
            String mensaje = "";
            Int32 tipo = 0;
            var rpta = _Proveedor_ServLN.Actualizar_ContraseñaAnti(id_prov_usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(rpta);
            return result;
        }


        public string Desactivar_ProvUsuarios(int idusuario)
        {
            Proveedor_ServLN _Proveedor_ServLN = new Proveedor_ServLN();
            String mensaje = "";
            Int32 tipo = 0;
            string usuario = Session["user_login"].ToString();
            var rpta = _Proveedor_ServLN.Desactivar_ProvUsuarios(idusuario, usuario, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(rpta);
            return result;
        }


        public string ListarProvee_Serv()
        {
            Proveedor_ServLN _Proveedor_ServLN = new Proveedor_ServLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Proveedor_ServLN.ListarProveedorServ(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }
        public string ListCorredores(int id_prov_serv)
        {
            Proveedor_ServLN _Proveedor_ServLN = new Proveedor_ServLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Proveedor_ServLN.ListCorredores(id_prov_serv, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string ListCorredor_X_Usuprov(int id_prov_usu)
        {
            Proveedor_ServLN _Proveedor_ServLN = new Proveedor_ServLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Proveedor_ServLN.ListCorredor_X_Usuprov(id_prov_usu, ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }


        public string ListarProvServ_Usuario()
        {
            Proveedor_ServLN _Proveedor_ServLN = new Proveedor_ServLN();
            String mensaje = "";
            Int32 tipo = 0;
            var listar = _Proveedor_ServLN.ListarProvServ_Usuario(ref mensaje, ref tipo);
            var result = JsonConvert.SerializeObject(listar); //para la lista principal
            return result;
        }

        public string AgregarUsuario_Proveedor(int idprov, string usuario, string contraseña)
        {
            Proveedor_ServLN _Proveedor_ServLN = new Proveedor_ServLN();
            String mensaje = "";
            Int32 tipo = 0;
            var result = "";
            string usuario_session = Session["user_login"].ToString();

            var rpta_FINAL = _Proveedor_ServLN.Rpta_Cuenta_Existente(idprov, usuario, ref mensaje, ref tipo);

            if (rpta_FINAL.AUX == 1)
            {
                rpta_FINAL.DES_ESTADO = "Usuario Existente";
                rpta_FINAL.COD_ESTADO = 0;
                result = JsonConvert.SerializeObject(rpta_FINAL); //para la lista principal
            }
            else
            {
                var rpta = _Proveedor_ServLN.AgregarUsuario_Proveedor(idprov, usuario, contraseña, usuario_session, ref mensaje, ref tipo);
                result = JsonConvert.SerializeObject(rpta); //para la lista principal
            }
            return result;
        }

        public string AgregarUsuarios_Corredor(string idcorredores, int idusuario, int id_prov_usuario, int id_prov_serv, int tipo_accion)
        {
            Proveedor_ServLN _Proveedor_ServLN = new Proveedor_ServLN();

            String mensaje = "";
            Int32 tipo = 0;
            var result = "";
            string[] id_corredor_nuevo = idcorredores.Split('|');
            string usuario_session = Session["user_login"].ToString();

            if (tipo_accion == 0)
            {
                var rpta_FINAL = _Proveedor_ServLN.Rpta_Usuario_Existente(id_prov_usuario, idusuario, id_prov_serv, ref mensaje, ref tipo);
                if (rpta_FINAL.AUX == 1)
                {
                    rpta_FINAL.DES_ESTADO = "Usuario Existente";
                    rpta_FINAL.COD_ESTADO = 0;
                    result = JsonConvert.SerializeObject(rpta_FINAL); //para la lista principal

                }
                else
                {
                    var rpta_ = _Proveedor_ServLN.Desactivar_Cuenta_Usuario(id_prov_usuario, usuario_session, ref mensaje, ref tipo);

                    //ENLAZA EL USUARIO CON LA CUENTA 
                    var rpta = _Proveedor_ServLN.Actualizar_Usuario_Prov(id_prov_usuario, idusuario, usuario_session, ref mensaje, ref tipo);

                    //AGREGA NUEVOS CORREDORES CON EL USUARIO 
                    foreach (var idcorredor in id_corredor_nuevo)
                    {
                        int id_corredores_final = Convert.ToInt32(idcorredor);
                        var rpta__=_Proveedor_ServLN.AgregarUsuario_Corredor(id_corredores_final, id_prov_usuario, usuario_session, ref mensaje, ref tipo);

                    }
                    //string usuario_session = Session["user_login"].ToString();

                    //var rpta = _Proveedor_ServLN.AgregarUsuario_Proveedor(proveedor, usuario, contraseña, usuario_session);
                    result = JsonConvert.SerializeObject(rpta); //para la lista principal
                }

            }
            else if (tipo_accion == 1)
            {
                _Proveedor_ServLN.Desactivar_Cuenta_Usuario(id_prov_usuario, usuario_session, ref mensaje, ref tipo);

                //ENLAZA EL USUARIO CON LA CUENTA 
                var rpta = _Proveedor_ServLN.Actualizar_Usuario_Prov(id_prov_usuario, idusuario, usuario_session, ref mensaje, ref tipo);

                //AGREGA NUEVOS CORREDORES CON EL USUARIO 
                foreach (var idcorredor in id_corredor_nuevo)
                {
                    int id_corredores_final = Convert.ToInt32(idcorredor);
                    _Proveedor_ServLN.AgregarUsuario_Corredor(id_corredores_final, id_prov_usuario, usuario_session, ref mensaje, ref tipo);

                }
                result = JsonConvert.SerializeObject(rpta); //para la lista principal                
            }
            return result;
        }




        public ActionResult ABEXA(string abrev_corr, string usuario, string contraseña, string url)
        {
            var contraseña_ = contraseña.Replace(' ', '+');

            //Corredor Correspondiente
            string corredorAbexa = "";
            corredorAbexa = usuario + abrev_corr;
            //Concatena datos y encripta
            string concatenado = corredorAbexa + "|" + Encriptador.Desencriptar(contraseña_);
            string codigoConcatenado = Encriptador.Encriptar(concatenado);
            string UrlRedirect = url + codigoConcatenado;
            return Redirect(UrlRedirect);
        }
        public ActionResult MATRIX(string abrev_corr, string usuario, string contraseña, string url)
        {
            //PARA EL ENCRIPTADOR
            string output = "";
            string str = "";

            //var ubijava = @"C:\Program Files\Java\jdk1.8.0_201\bin\";
            var ubiJar = "-jar \"" + Server.MapPath("~/Util/encriptador.jar") + "\" " + abrev_corr + " " + usuario + " " + contraseña;
            try
            {
                var proceso = new Process();
                proceso.EnableRaisingEvents = false;
                proceso.StartInfo.FileName = @"C:\Program Files\Java\jdk1.8.0_201\bin\java.exe";
                proceso.StartInfo.Arguments = ubiJar;
                proceso.StartInfo.UseShellExecute = false;
                proceso.StartInfo.RedirectStandardOutput = true;
                proceso.Start();
                StreamReader reader = proceso.StandardOutput;
                output = reader.ReadToEnd();
                str = output;
                str = str.Remove(str.Length - 2, 2);
                string UrlRedirect = url + str;

                //REDIRECCIONA A MATRIX
                return Redirect(UrlRedirect);
            }
            catch (Win32Exception w)
            {
                Console.WriteLine(w.Message);
                Console.WriteLine(w.ErrorCode.ToString());
                Console.WriteLine(w.NativeErrorCode.ToString());
                Console.WriteLine(w.StackTrace);
                Console.WriteLine(w.Source);
                Exception e = w.GetBaseException();
                Console.WriteLine(e.Message);
                return View(e.Message);
            }
        }


    }
}