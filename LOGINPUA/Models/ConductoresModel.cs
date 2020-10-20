using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOGINPUA.Models
{
    public class ConductoresModel
    {
        public List<ENTIDADES.CC_CONDUCTORES>Lista_conductores { get; set; }
        public CC_CONDUCTORES CC_CONDUCTORES { get; set; }
        public CC_PERSONA CC_PERSONA { get; set; }

    }
}