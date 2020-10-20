using System.Web;
using System.Web.Mvc;
using LOGINPUA.Util;

namespace LOGINPUA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new VerificarAcceso(), 1);
            filters.Add(new HandleErrorAttribute());
        }
    }
}
