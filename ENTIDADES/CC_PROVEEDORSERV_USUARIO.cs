using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CC_PROVEEDORSERV_USUARIO
    {

        public int IDPROVUSU_CORREDOR { get; set; }

        public int ID_PROV_USUARIO { get; set; }
        public int ID_PROV_SERV { get; set; }
        public string NOMBRE_PROVE { get; set; }
        public int ID_USUARIO { get; set; }
        public int ID_CORREDOR { get; set; }
        public string USUARIO { get; set; }
        public string USUARIO_PROV { get; set; }
        public string CORREDOR_NOMBRE { get; set; }
        public string ABREVIATURA { get; set; }
        public string CONTRASENA { get; set; }
        public string ID_ESTADO { get; set; }
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string PROVABREV_CORR { get; set; }

    }
}
