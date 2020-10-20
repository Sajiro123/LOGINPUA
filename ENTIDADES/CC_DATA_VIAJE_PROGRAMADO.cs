using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CC_DATA_VIAJE_PROGRAMADO
    {
        public int ID_MSALIDA_PROG { get; set; }
        public int ID_TIPO_SERVICIO { get; set; } 
        public int ID_MAESTRO_SALIDA_PROG { get; set; }
        public int ID_MSALIDA_PROG_DET { get; set; } 
        public string TIPO_DIA { get; set; }
        public string SERVICIO { get; set; }
        public string POG { get; set; }
        public string POT { get; set; }
        public string FNODE { get; set; }
        public string HSALIDA { get; set; }
        public string HLLEGADA { get; set; }
        public string TNODE { get; set; }
        public string PIG { get; set; }
        public string LAYOVER { get; set; }
        public string ACUMULADO { get; set; }
        public string SENTIDO { get; set; }
        public string TURNO { get; set; }
        public string TIPO_SERVICIO { get; set; }
        public string PLACA { get; set; }
        public string CAC_CONDUCTOR { get; set; }
        public string TRIP_TIME { get; set; }
        public string DISTANCIA { get; set; }

        public string FNODE_A { get; set; }
        public string TNODE_A { get; set; }
        public string FNODE_B { get; set; }
        public string TNODE_B { get; set; }
        public string DISTANCIA_A { get; set; }
        public string DISTANCIA_B { get; set; }

        

    }
}
