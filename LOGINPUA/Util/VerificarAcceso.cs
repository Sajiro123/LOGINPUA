using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace LOGINPUA.Util
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class VerificarAcceso : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
            String action = filterContext.ActionDescriptor.ActionName.ToUpper();
            String controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToUpper();
            // Validar si se quiere acceder al Login
            if (action.Equals("LOGIN") && controller.Equals("USUARIOS"))
            {
                // Validar si hay algún usuario con sesion activa
                if (filterContext.HttpContext.Session["userId"] != null)
                {
                    RouteValueDictionary redirectTargetDictionary = ObtenerRutaRedireccionamiento("Sistemas_", "Home");
                    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                }
            }
            //enviarformulario
            else if (action.Equals("GETPLACASCONADIS") && controller.Equals("PLACASCONADIS"))
            {
                return;
            }
            else if (action.Equals("GETPLACASVECINOSEMP") && controller.Equals("PLACASCONADIS"))
            {
                return;
            }
            else if (action.Equals("CONSULTAR_PLACA") && controller.Equals("CONSULTARPLACA") || action.Equals("CONSULTAR_CONDUCTOR_PERSONAL") && controller.Equals("BUSQUEDA"))
            {
                return;
            }
            else if (action.Equals("CONSULTAR_X_PLACA") && controller.Equals("CONSULTARPLACA")|| action.Equals("BUSQUEDA_CONDUCTORES_PERSONAL") && controller.Equals("BUSQUEDA") )
            {
                return;
            }
            else
            {
                if (filterContext.HttpContext.Session["userId"] == null)
                {
                    if (!filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        if (action.Equals("INGRESAR") && controller.Equals("LOGIN"))
                        {
                            // Validar si hay algún usuario con sesion activa
                            if (filterContext.HttpContext.Session["userId"] != null)
                            {
                                //RouteValueDictionary redirectTargetDictionary = ObtenerRutaRedireccionamiento("Index", "Home"); //login antiguo
                                RouteValueDictionary redirectTargetDictionary = ObtenerRutaRedireccionamiento("Login", "Usuarios");
                                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                            }
                        }
                        else
                        {
                            RouteValueDictionary redirectTargetDictionary = ObtenerRutaRedireccionamiento("Login", "Usuarios");
                            filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                        }
                    }
                    else
                    {
                        //if (Boolean.Parse(ConfigurationManager.AppSettings["pruebaConfigLocal"]))
                        //{
                        //    filterContext.Result = new RedirectResult("~/../" + ConfigurationManager.AppSettings["NombreAplicacionLocal"]);
                        //}
                        //else
                        //{
                        //    filterContext.Result = new RedirectResult("~/../" + ConfigurationManager.AppSettings["NombreAplicacionPublica"]);
                        //}
                        filterContext.Result = new RedirectResult("~/../");
                        return;
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }

        private RouteValueDictionary ObtenerRutaRedireccionamiento(string action, string controller)
        {
            RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
            redirectTargetDictionary.Add("controller", controller);
            redirectTargetDictionary.Add("action", action);
            return redirectTargetDictionary;
        }
    }
}