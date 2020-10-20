using Dapper.Contrib.Extensions;
using System;

namespace ENTIDADES
{
    public class TV_LOG_ACC
    {
        [ExplicitKey]
        public int LOGACCCOD { get; set; }
        public int LOGCOD { get; set; }
        public int ACCCOD { get; set; }
        public string USUREG { get; set; }
        public DateTime FECREG { get; set; }
        public string USUMOD { get; set; }
        public DateTime FECMOD { get; set; }
        public int ESTREG { get; set; }
    }
}
