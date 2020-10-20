using System;
using Dapper.Contrib.Extensions;

namespace ENTIDADES
{
    public class CC_MODALIDAD_TRANSPORTE
    {
        [ExplicitKey]
        public int ID_MODALIDAD_TRANS { get; set; }
        public string NOMBRE { get; set; }

    }
}
