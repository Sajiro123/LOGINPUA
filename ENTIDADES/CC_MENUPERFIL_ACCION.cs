using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CC_MENUPERFIL_ACCION
    {
        public int IDMENU_ACCION { get; set; }
        public int ID_MENU { get; set; }
        public int ID_ACCION { get; set; }
        public string NOMBRE { get; set; }
        public string ICON_ACCION { get; set; }
        public string MENU { get; set; }
        public string URL { get; set; }


        public int ID_MENUSUARIOPERFIL { get; set; }
        public int ID_ESTADO { get; set; }


    }
}
