using Dapper.Contrib.Extensions;
using System;

namespace ENTIDADES
{
    public class TM_USUARIOS_EMPRESA
    {
        [ExplicitKey]
        public int USUEMPCOD { get; set; }
        public int EMPCOD { get; set; }
        public int LOGCOD { get; set; }
        public string USUEMPUSU { get; set; }
        public string USUEMPCON { get; set; }

        public string USUREG { get; set; }
        public DateTime FECREG { get; set; }
        public string USUMOD { get; set; }
        public DateTime FECMOD { get; set; }
        public int ESTREG { get; set; }
    }
}
