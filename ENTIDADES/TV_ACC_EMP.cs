using Dapper.Contrib.Extensions;
using System;

namespace ENTIDADES
{
    public class TV_ACC_EMP
    {
        [ExplicitKey]
        public int ACCEMPCOD { get; set; }
        public int ACCCOD { get; set; }
        public int EMPCOD { get; set; }
        public string USUREG { get; set; }
        public DateTime FECREG { get; set; }
        public string USUMOD { get; set; }
        public DateTime FECMOD { get; set; }
        public int ESTREG { get; set; }
    }
}
