using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CC_PERFIL_USUARIO
    {

        public string ID_USUARIO_PERFIL { get; set; }

        public int ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string CLAVE { get; set; }
        public string IDMODALIDAD { get; set; }


        public int ID_PERFIL { get; set; }
        public string NOMBRE { get; set; }
        public string PERFILES { get; set; }

        public string MODALIDAD { get; set; }
        public int ID_MODALIDAD_TRANS { get; set; }
        public int ID_PERFIL_MODALIDAD { get; set; }

        public string PERFIL { get; set; }


        public string ID_ESTADO { get; set; }
        public string FECHA_REG { get; set; }
        public string USU_REG { get; set; }

    }
}
