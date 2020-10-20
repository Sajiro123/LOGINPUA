using System;
using Dapper.Contrib.Extensions;

namespace ENTIDADES
{
    public class CC_RUTA_CORREDOR
    {
        [ExplicitKey]
        public int ID_RUTA { get; set; }
        public string NOMBRE { get; set; }
        public string NRO_RUTA { get; set; }
        public string ABREV_CORREDOR { get; set; }


    }
}
