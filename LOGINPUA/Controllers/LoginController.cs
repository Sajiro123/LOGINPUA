using Entidades;
using ENTIDADES;
using ENTIDADES.InnerJoin;
using LN.EntidadesLN;
using LOGINPUA.Models;
using LOGINPUA.Util;
using LOGINPUA.Util.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.NetworkInformation;
using System.Web.Mvc;

namespace LOGINPUA.Controllers
{
    public class LoginController : Controller
    {
        //private readonly LoginLN _LoginLN;
        //private string message;
        //private int errortype;

        //private readonly PerfilLN _PerfilLN;
        //Util.Util utilidades = new Util.Util();


        //public LoginController()
        //{
        //    //String Clave = Encriptador.Desencriptar("oW+LRwYtAKFwq8VhIgA29w==");
        //    _LoginLN = new LoginLN();
        //}


        public ActionResult CerrarSesion()
        {
            Session.Abandon();
            Session.Clear();
            //return RedirectToAction("Login", "Usuario", new { script = "localStorage.setItem('storcs', 'crsn');" });
            return RedirectToAction("Login", "Usuarios");

        }

        //OBTENER MAC
        public static PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }
        string MAC = GetMacAddress().ToString();


        public ActionResult Ingresar(string usuario, string clave)
        {


            LoginLN _LoginLN = new LoginLN();

            String mensaje = "";
            Int32 tipo = 0;
            Dictionary<int, string> dicModalidad = new Dictionary<int, string>();
            List<CC_ACCESO_CORREDORES> Lista_acceso = new List<CC_ACCESO_CORREDORES>();
            List<CC_PERSONA> Lista_Persona = new List<CC_PERSONA>();
            List<CC_MENUPERFIL_ACCION> Lista_Acciones = new List<CC_MENUPERFIL_ACCION>();


            Dictionary<string, DataTable> respuesta = new Dictionary<string, DataTable>();
            var codRespuesta = 0;
            var desRespuesta = "";
            DataSet Ds = new DataSet();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();

            //
            string[] modalidad = new string[10];
            string clave_encriptada = Encriptador.Encriptar(clave);

            //obtengo todos los datos del usuario
            Ds = _LoginLN.IngresarIn(usuario, clave_encriptada, ref mensaje, ref tipo);

            //  TABLA 
            dt1 = Ds.Tables[0];
             
            //VALIDAR SI USUARIO EXISTE 
            if (Ds.Tables.Count <= 1)
            {
                codRespuesta = int.Parse(dt1.Rows[0][1].ToString()); // 1 ó 0 
                desRespuesta = dt1.Rows[0][0].ToString(); //"Mensaje"
            }
            else
            {
                dt2 = Ds.Tables[1];
                dt3 = Ds.Tables[2];
                dt4 = Ds.Tables[3];
                dt5 = Ds.Tables[4];
                dt6 = Ds.Tables[5];


                //RESPUESTA DEL USUARIRO
                codRespuesta = int.Parse(dt1.Rows[0][1].ToString()); // 1 ó 0 
                desRespuesta = dt1.Rows[0][0].ToString(); //"Mensaje"

                //Accesos Corredores
                foreach (DataRow item in dt5.Rows)
                {
                    CC_ACCESO_CORREDORES acceso_corredor = new CC_ACCESO_CORREDORES();
                    acceso_corredor.IDPROVUSU_CORREDOR = int.Parse(item[0].ToString());
                    acceso_corredor.ID_PROV_USUARIO = int.Parse(item[1].ToString());
                    acceso_corredor.URL_SERV = item[2].ToString();
                    acceso_corredor.NOMBRE_ACCESO = item[3].ToString();
                    acceso_corredor.USUARIO = item[4].ToString();
                    acceso_corredor.CONTRASENA = item[5].ToString();
                    acceso_corredor.ID_CORREDOR = int.Parse(item[6].ToString());
                    acceso_corredor.ID_USUARIO = int.Parse(item[7].ToString());
                    acceso_corredor.CORREDOR_NOMBRE = item[8].ToString();
                    acceso_corredor.PROVABREV_CORR = item[9].ToString();
                    acceso_corredor.COLOR_REPRESENTATIVO = item[10].ToString();

                    Lista_acceso.Add(acceso_corredor);
                }
                // LAS MODALIDADES DE LA TABLA 3
                foreach (DataRow item in dt2.Rows)
                {
                    CC_PERSONA persona_list = new CC_PERSONA();
                    persona_list.NOMBRES = item[5].ToString();
                    persona_list.APEPAT = item[6].ToString();
                    persona_list.APEMAT = item[7].ToString();
                    persona_list.NRODOCUMENTO = Convert.ToInt32(item[8].ToString());
                    persona_list.CORREO = item[9].ToString();
                    persona_list.ID_PERSONA = Convert.ToInt32(item[10].ToString());
                    SessionHelper.id_persona = Convert.ToInt32(item[10].ToString());


                    Lista_Persona.Add(persona_list);
                }
                SessionHelper.Lista_Persona = Lista_Persona;

                // LAS MODALIDADES DE LA TABLA 3
                foreach (DataRow item in dt3.Rows)
                {
                    var id_modalidad = int.Parse(item[3].ToString());
                    var nom_modalidad = item[4].ToString();
                    dicModalidad.Add(id_modalidad, nom_modalidad);


                    //perfil
                    if (id_modalidad == 1)
                    {
                        Session["perfil_corredor"] = int.Parse(item[1].ToString());
                    }
                }

                //LAS ACCIONES DE LA TABLA 6
                foreach (DataRow item in dt6.Rows)
                {
                    CC_MENUPERFIL_ACCION accion_list = new CC_MENUPERFIL_ACCION();
                    accion_list.IDMENU_ACCION = Convert.ToInt32(item[2].ToString());
                    accion_list.ID_MENUSUARIOPERFIL = Convert.ToInt32(item[6].ToString());
                    accion_list.ID_ACCION = Convert.ToInt32(item[3].ToString());
                    accion_list.NOMBRE = item[4].ToString();
                    accion_list.MENU = item[1].ToString();
                    accion_list.ID_MENU = Convert.ToInt32(item[0].ToString());
                    accion_list.URL = item[9].ToString();


                    Lista_Acciones.Add(accion_list);
                }

                Session["acceso_corredor"] = Lista_acceso;
                Session["menu_modulo"] = dt4;
                Session["id_usuario"] = Convert.ToInt32(dt2.Rows[0][0].ToString());
                SessionHelper.user_login = dt2.Rows[0][1].ToString();
                SessionHelper.user_rol = dt2.Rows[0][4].ToString();
                SessionHelper.user_password = dt2.Rows[0][2].ToString();                
                Session["modalidadUsuario"] = dicModalidad;
                Session["menu_acciones"] = Lista_Acciones;
            }

            if (codRespuesta == 1) //ya estad logeado 
            { //usando la segunda tabla de los datos del usuario
                Session["userId"] = 1;
                return RedirectToAction("Sistemas_", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Usuarios", new { m = desRespuesta, u = usuario });
            }
        }

    }
}