using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace ENTIDADES
{
    public class CC_PERSONA
    {

        public int ID_TIPO_DOCUMENTO { get; set; }
        public int ID_PERSONA { get; set; }        
        public int ID_ROLPERSONA { get; set; }
        public string NOMBRES { get; set; }
        public string APEPAT { get; set; }
        public string APEMAT { get; set; }
        public string CORREO { get; set; }
        public int NRODOCUMENTO { get; set; }

        public string TIPODOC { get; set; }
        public string ROL { get; set; }


        
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }

 


    }
}
