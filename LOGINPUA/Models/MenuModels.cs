using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOGINPUA.Models
{
    public class MenuModels
    {

        public int idmodulo { get; set; }
        public string nomModulo { get; set; }
        public string urlmodulo { get; set; }
        public string imgmodulo { get; set; }
        public int idmenu { get; set; }
        public string nombmenu { get; set; }
        public string url_menu { get; set; }
        public int is_padre { get; set; }
        public int id_menu_padre { get; set; }
        public int id_modalidad { get; set; }

    }
}