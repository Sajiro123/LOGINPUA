using Dapper.Contrib.Extensions;
using System;

namespace ENTIDADES
{
    public class TB_LOGIN
    {
        [ExplicitKey]
        public int LOGCOD { get; set; }
        public int USUCOD { get; set; }
        public string LOGUSU { get; set; }
        public string LOGCON { get; set; }
        public string USUREG { get; set; }
        public DateTime FECREG { get; set; }
        public string USUMOD { get; set; }
        public DateTime FECMOD { get; set; }
        public int ESTREG { get; set; }
    }
}
