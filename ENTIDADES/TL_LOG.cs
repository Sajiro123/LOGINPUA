using Dapper.Contrib.Extensions;
using System;

namespace ENTIDADES
{
    public class TL_LOG
    {
        [ExplicitKey]
        public int LOGCOD { get; set; }
        public string LOGIP { get; set; }
        public DateTime LOGFEC { get; set; }
        public string LOGUSE { get; set; }
        public string LOGDES { get; set; }
        public string LOGMAC { get; set; }
        public string USUREG { get; set; }
        public DateTime FECREG { get; set; }
        public string USUMOD { get; set; }
        public DateTime FECMOD { get; set; }
        public int ESTREG { get; set; }
    }
}
