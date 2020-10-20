using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOGINPUA.Models
{
    public class AsignarModel
    {
        public int Nivel { get; set; }
        //TM_USUARIOS_EMPRESA
        public int USUEMPCOD { get; set; }
        public int EMPCOD { get; set; }
        public int LOGCOD { get; set; }
        public string USUEMPUSU { get; set; }
        public string USUEMPCON { get; set; }
        //ASIGNACION
        public string EMPNOM { get; set; }
        public string USUREG { get; set; }
        public DateTime FECREG { get; set; }
        public string USUMOD { get; set; }
        public DateTime FECMOD { get; set; }
        public int ESTREG { get; set; }
    }
}