using System;
using Dapper.Contrib.Extensions;
namespace ENTIDADES
{
    public class CC_PARADERO
    {
        [ExplicitKey]
        public int ID_PARADERO { get; set; }
        public int ID_PARADERO_TIPOSERVICIO { get; set; }
        public int ID_RUTA{ get; set; }

        public int ID_RECORRIDO { get; set; }
        public int ID_TIPO_PARADERO { get; set; }
        public int ID_VIA { get; set; }
        public int ID_ESTADO { get; set; }
        public string LADO { get; set; }
        public string SENTIDO { get; set; }
        public int NRO_ORDEN { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO { get; set; }
        public string ETIQUETA_NOMBRE { get; set; }
        public double DISTANCIA_PARCIAL { get; set; }
        public double LATITUD { get; set; }
        public double LONGITUD { get; set; }
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }
    }
}
