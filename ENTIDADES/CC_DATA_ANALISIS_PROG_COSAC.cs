using System;
using Dapper.Contrib.Extensions;

namespace ENTIDADES
{
    public class CC_DATA_ANALISIS_PROG_COSAC
    {
        //public int ID_RUTA_TIPO_SERVICIO { get; set; }
        public string ABREV_ESTACION { get; set; }
        public int SEQ_VIAJE { get; set; }
        public string SENTIDO { get; set; }
        public string HEJECUTADA { get; set; }
        public string HPROGRAMADA { get; set; }
        public string FECHA { get; set; }
        public int NUMBLOCK { get; set; }
        public int ID_BUS { get; set; }
        public int ID_VIAJE { get; set; }
        public string FEC_HORAPASO_REG { get; set; }
        
    }
}
