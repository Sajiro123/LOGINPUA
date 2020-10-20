using Dapper.Contrib.Extensions;
using System;

namespace Entidades
{
    public class TM_EMPRESA
    {
        [ExplicitKey]
        public int EMPIDE { get; set; }
        [ExplicitKey]
        public int EMPEMP { get; set; }
        public string EMPUSE { get; set; }
        public string EMPPAS { get; set; }
    }
}
