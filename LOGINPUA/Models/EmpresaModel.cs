using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOGINPUA.Models
{
    public class EmpresaModel
    {
        public int Nivel { get; set; }
        public int EMPCOD { get; set; }
        public string EMPNOM { get; set; }
        public string EMPPAG { get; set; }

        public string USUREG { get; set; }
        public DateTime FECREG { get; set; }
        public string USUMOD { get; set; }
        public DateTime FECMOD { get; set; }
        public int ESTREG { get; set; }
    }
}