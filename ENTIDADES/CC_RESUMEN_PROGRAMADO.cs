using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ENTIDADES
{
    public class CC_RESUMEN_PROGRAMADO
    {
        public int ID_RUTA { get; set; }
        public string ID_MAESTRO_SALIDA_PROG { get; set; }
        public string NRO_RUTA { get; set; }
        public string TIPO_SERVICIO { get; set; }
        public string TIPO_OPERACIONAL { get; set; }
        public string TIPO_DIA { get; set; }
        public int SEMANA { get; set; }
        public string ABREVIATURA { get; set; }
        public string CANTIDAD_VIAJE { get; set; }
        public string FECHA_PROGRAMACION { get; set; }
        public string FECHA_REG { get; set; }
        public string USU_REG { get; set; }
    }
}
