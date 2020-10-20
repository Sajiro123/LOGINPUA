using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOGINPUA.Models
{
    public class AccesoModel
    {
        public int Nivel { get; set; }
        //TM_ACCESO
        public int ACCCOD { get; set; }
        public string ACCNOM { get; set; }
        public string ACCICO { get; set; }
        public HttpPostedFileBase ACCICOFILE { get; set; }
        public string ACCDIR { get; set; }
        public string ACCGRU { get; set; }
        //TV_ACC_EMP
        public int ACCEMPCOD { get; set; }
        public int EMPCOD { get; set; }
        public string USUREG { get; set; }
        public DateTime FECREG { get; set; }
        public string USUMOD { get; set; }
        public DateTime FECMOD { get; set; }
        public int ESTREG { get; set; }
    }
}