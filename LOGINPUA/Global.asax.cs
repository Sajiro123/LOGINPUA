using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LOGINPUA
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //UnityConfig.RegisterComponents();
        }


        protected void Application_Error(object sender,EventArgs e )
        {
            Exception exc = Server.GetLastError();
            Server.ClearError();
            Response.Redirect("~/Home/mensaje_error");
        }


        //void Session_End(Object sender, EventArgs E) //se ejecuta cuando la sesion se vence
        //{
        //    // Call your method  
        //    Response.RedirectToRoute("Default");
        //}
    }
}
