using Dapper.Contrib.Extensions;
using System;

namespace Entidades
{
    public class CC_DISTANCIA_PARADEROS
    {
        [ExplicitKey]
        public int DIS_ID { get; set; }
        public string DIS_PARADERO_IDA { get; set; }
        public string DIS_PARADERO_VUELTA { get; set; }
        public float DIS_RECORRIDO_KM { get; set; }
        public string ID_CORREDOR { get; set; }
        public string SALIDA_BUS { get; set; }
        public float DIS_DISTANCIA_PROGRAMADA { get; set; }
        public string DIS_DIA { get; set; }
        public string DIS_RUTA { get; set; }
        public string DIS_OPERADOR { get; set; }
        public string CC_SENTIDO { get; set; }


    }
}
