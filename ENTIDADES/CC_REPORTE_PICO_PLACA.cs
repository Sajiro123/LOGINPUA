using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CC_REPORTE_PICO_PLACA
    {
        public string FECHA_REGISTRO { get; set; }
        public string ABREV_CORREDOR { get; set; }
        public string NRO_RUTA { get; set; }
        public string HINICIO { get; set; }
        public string HFIN { get; set; }
        public double VEL_PROMEDIO_AB { get; set; }
        public double VEL_PROMEDIO_BA { get; set; }


        public string TIEMPO_IDA { get; set; }
        public string TIEMPO_VUELTA { get; set; }

        public string M_IDA { get; set; }
        public string T_IDA { get; set; }
        public string M_VUELTA { get; set; }
        public string T_VUELTA { get; set; }




        public string TURNO { get; set; }

        public string VEL_PROMEDIO_A_MAÑANA_IDA { get; set; }
        public string VEL_PROMEDIO_A_MAÑANA_VUELTA { get; set; }

        public string VEL_PROMEDIO_B_MAÑANA_IDA { get; set; }
        public string VEL_PROMEDIO_B_MAÑANA_VUELTA { get; set; }



        public string VEL_PROMEDIO_A_TARDE_IDA { get; set; }
        public string VEL_PROMEDIO_A_TARDE_VUELTA { get; set; }

        public string VEL_PROMEDIO_B_TARDE_IDA { get; set; }
        public string VEL_PROMEDIO_B_TARDE_VUELTA { get; set; } 
        public string VEL_PROMEDIO_AB_TARDE { get; set; }
        public string VEL_PROMEDIO_BA_TARDE { get; set; }
         
        public string TIEMPO_PROM_A_1 { get; set; }
        public string TIEMPO_PROM_B_1 { get; set; } 
        public string TIEMPO_PROM_A_2 { get; set; }
        public string TIEMPO_PROM_B_2 { get; set; }
         

        public string TIEMPO_PROM_A_3 { get; set; }
        public string TIEMPO_PROM_B_3 { get; set; } 
        public string TIEMPO_PROM_A_4 { get; set; }
        public string TIEMPO_PROM_B_4 { get; set; } 

        public string TIEMPO_PROM_A { get; set; }
        public string TIEMPO_PROM_B { get; set; }
         
        public double DISTANCIA_A { get; set; }
        public double DISTANCIA_B { get; set; }
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string ID_ESTADO { get; set; }
        public string FECHA_INICIO { get; set; }
        public string FECHA_FIN { get; set; }
        public string archivo { get; set; }
        public string CORREDOR { get; set; }
        public int ID_REG_PPLAC { get; set; }
        public int ID_RUTA { get; set; }
        public string OBS { get; set; }
        public string USU_MODIF { get; set; }

        public string FECHA_MODIF { get; set; }
        public string USU_ANULA { get; set; }
        public string FECHA_ANULA { get; set; }


        
        



    }
}
