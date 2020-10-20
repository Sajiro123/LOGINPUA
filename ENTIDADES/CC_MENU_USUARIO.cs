using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CC_MENU_USUARIO
    {
        
        public int ID_MENU { get; set; }

        public int ID_MODULO_SISTEMA { get; set; }
        public string NOMBRE { get; set; }
        public string URL { get; set; }
        public string ICONO { get; set; }

        public string IMAGEN { get; set; }
        public string IS_PADRE { get; set; }
        public int ID_MENU_PADRE { get; set; }
        public int ID_MODULO { get; set; }
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string MODULO { get; set; }

        


    }
}
