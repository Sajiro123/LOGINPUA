using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace ENTIDADES
{
    public class CC_CONCESIONARIOS
    {
        [ExplicitKey]
        public int ID_CONCESIONARIO { get; set; }
        public int ID_CORREDOR { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO_CONTRATO { get; set; }
        public string PAQUETES { get; set; }
        public string REPRESENTANTE { get; set; }
        public string CARGO { get; set; }
        public string DIRECCION { get; set; }
        public string DISTRITO { get; set; }

    }
}
