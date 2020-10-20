using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace ENTIDADES
{
    public class CC_PROGRAMA_RUTA
    {
        [ExplicitKey]
        public int ID { get; set; }
        public int ID_MSALIDA_PROG_DET { get; set; }
        public int ID_MAESTRO_SALIDA_PROG { get; set; }
        
        public string TIPO_DIA { get; set; }
        public string RUTA { get; set; }
        public string OPERADOR { get; set; }
        public string RECORDID { get; set; }
        public string ROUTE { get; set; }
        public string BLK { get; set; }
        public string POG { get; set; }
        public string POT { get; set; }
        public string FNODE { get; set; }
        public string FTIME { get; set; }
        public string TTIME { get; set; }
        public string TNODE { get; set; }
        public string PIG { get; set; }
        public string PIT { get; set; }
        public string LAY { get; set; }
        public string TRIP_TIME { get; set; }
        public string DISTANCIA { get; set; }
        public string ACUMULADO { get; set; }
        public string PATS { get; set; }
        public string SENTIDO { get; set; }
        public string OBSERVACIONES { get; set; }
        public string PLACA { get; set; }
        public string PAQUETE { get; set; }
        public string HSALIDA { get; set; }
        public string CAC_CONDUCTOR { get; set; }  
        public int SERVICIO { get; set; } 
        public string HSALIDA_REAL { get; set; }
        public string PLACA_TIEMPOREAL { get; set; }
        public int CAC_TIEMPOREAL { get; set; }
 




        public List<CC_CONDUCTORES> LISTA_CONDUCTORES { get; set; }

    }
}
