using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LOGINPUA.Util
{
    public static class SessionHelper
    {

        public static String user_login
        {
            get { return HttpContext.Current.Session["user_login"] == null ? null : HttpContext.Current.Session["user_login"].ToString(); }
            set { HttpContext.Current.Session["user_login"] = value; }
        }

        public static List<CC_PERSONA> Lista_Persona
        {
            
            get { return (List<CC_PERSONA>)HttpContext.Current.Session["user_datos"] == null   ? null : (List<CC_PERSONA>)HttpContext.Current.Session["user_datos"]; }
            set { HttpContext.Current.Session["user_datos"] = value; }
        }

        public static String user_rol
        {
            get { return HttpContext.Current.Session["user_rol"] == null ? null : HttpContext.Current.Session["user_rol"].ToString(); }
            set { HttpContext.Current.Session["user_rol"] = value; }
        }

        public static int id_persona
        {
            get{ return Convert.ToInt32(HttpContext.Current.Session["ID_PERSONA"]) == 0 ? 0 : Convert.ToInt32(HttpContext.Current.Session["ID_PERSONA"]);}
            set { HttpContext.Current.Session["ID_PERSONA"] = value; }
        }
        public static String user_password
        {
            get { return HttpContext.Current.Session["user_password"] == null ? null : HttpContext.Current.Session["user_password"].ToString(); }
            set { HttpContext.Current.Session["user_password"] = value; }
        }

        public static bool sesion_valida(ref string mensaje, ref int tipo)
        {

            if (HttpContext.Current.Session["user_login"] != null)
            {
                return true;
            }
            else
            {
                mensaje = "La sesión ha caducado";
                tipo = -1;
                return false;
            }
        }
    }

}
