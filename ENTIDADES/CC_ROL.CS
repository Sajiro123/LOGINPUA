using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace ENTIDADES
{
    public class CC_ROL
    {

        public int ID_ROLPERSONA { get; set; }
        public string NOMBRE { get; set; }
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }

        public string USU_MODIF { get; set; }
        public string FECHA_MODIF { get; set; }


        
    }
}
