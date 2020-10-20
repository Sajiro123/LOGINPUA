using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CC_INGRESOUSUARIO
    {
        public int ID_USUARIO { get; set; }
        public int ID_PERSONA { get; set; }
        public int ID_PERFIL { get; set; }
        public string USUARIO { get; set; }
        public string CLAVE { get; set; }
        public int ID_ESTADO { get; set; }
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }
        public int ESTREG { get; set; }
        public int ID_MODALIDAD_TRANS { get; set; }
         
        
    }
}
