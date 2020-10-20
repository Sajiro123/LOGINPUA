using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CC_PROVSERV_USUARIO
    {
        public int ID_PROV_USUARIO { get; set; }
        public int ID_PROV_SERV { get; set; }
        public int ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string CONTRASENA { get; set; }
        public string ID_ESTADO { get; set; }
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }


    }
}
