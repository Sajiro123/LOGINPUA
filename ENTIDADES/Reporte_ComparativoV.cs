using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Reporte_ComparativoV
    {
        public double suma_tiempo_A { get; set; }
        public double suma_tiempo_B { get; set; }
        public double suma_velocidad_A { get; set; }
        public double suma_velocidad_B { get; set; }
        public double total_tiempo_a { get; set; }
        public double total_tiempo_b { get; set; }

        public double contador_tiempoa { get; set; }
        public double contador_tiempob { get; set; }
        public double contador_velocidad_a { get; set; }
        public double contador_velocidad_b { get; set; }

        public double TOTAL_a_velocidad { get; set; }
        public double TOTAL_b_velocidad { get; set; }
 
    }
}
