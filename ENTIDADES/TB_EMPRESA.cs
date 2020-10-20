using Dapper.Contrib.Extensions;
using System;

namespace ENTIDADES
{
    public class TB_EMPRESA
    {
        [ExplicitKey]
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
