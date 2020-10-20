using Dapper.Contrib.Extensions;
using System;

namespace ENTIDADES
{
    public class TM_ACCESO
    {
        [ExplicitKey]
        public int ACCCOD { get; set; }
        public string ACCNOM { get; set; }
        public string ACCICO { get; set; }
        public string ACCDIR { get; set; }
        public string ACCGRU { get; set; }

        public string USUREG { get; set; }
        public DateTime FECREG { get; set; }
        public string USUMOD { get; set; }
        public DateTime FECMOD { get; set; }
        public int ESTREG { get; set; }
    }
}
