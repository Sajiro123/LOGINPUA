using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CC_MODULO
    {
        public int ID_MODULO_SISTEMA { get; set; }
        public int ID_MODALIDAD_TRANS { get; set; }
        public string NOMBRE { get; set; }
        public string URL { get; set; }
        public string MODALIDAD { get; set; }

        public string IMAGEN { get; set; }
        public string USU_REG { get; set; }

        
        public string FECHA_REG { get; set; }
    }
}
