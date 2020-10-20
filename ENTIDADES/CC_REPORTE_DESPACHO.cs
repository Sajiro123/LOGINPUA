using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CC_REPORTE_DESPACHO
    {
        public int NRO_RUTA { get; set; }
        public string FECHA { get; set; }
         public int CANTIDAD_VIAJES { get; set; }
        public string USU_REG { get; set; }
        public DateTime FECHA_REG { get; set; }
        public int ID_MAESTROPASAJERO { get; set; }
    }
}
