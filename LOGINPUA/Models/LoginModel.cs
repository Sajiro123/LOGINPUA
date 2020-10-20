using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOGINPUA.Models
{
    public class LoginModel
    {
        public int LOGIDE { get; set; }
        public string LOGUSU { get; set; }
        public string LOGPAS { get; set; }
        public string LOGROL { get; set; }
        public string LOGPER { get; set; }
        public string USUREG { get; set; }
        public DateTime FECREG { get; set; }
        public string USUMOD { get; set; }
        public DateTime FECMOD { get; set; }
        public int ESTREG { get; set; }
        public int LOGINI { get; set; }
        public int LOGCORROJ { get; set; }
        public int LOGCORAMA { get; set; }
        public int LOGCORVER { get; set; }
        public int LOGCORAZU { get; set; }
        public int LOGCORMOR { get; set; }

        public String USULOG { get; set; }
        public String USUCON { get; set; }
    }
}


