using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CC_DATA_ANALISIS_PROG_CORREDORES
    {
        //public int ID_RUTA_TIPO_SERVICIO { get; set; }
        public string ID_RUTA { get; set; }
        public string FECHA { get; set; }
        public string HORA_PASO { get; set; }
        public string SENTIDO { get; set; }
        public string ID_SALIDAEJECUTADA { get; set; }
        public string ID_PARADERO { get; set; }
    }
}
