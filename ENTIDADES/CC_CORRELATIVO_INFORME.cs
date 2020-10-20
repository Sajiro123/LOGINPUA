using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CC_CORRELATIVO_INFORME
    {
        public int ID_INF_CORR { get; set; }
        public int ID_ESTADO { get; set; }        
        public int NUM_CORRELATIVO { get; set; }
        public string PERS_DIRIG { get; set; }
        public string PERS_RESP { get; set; }
        public string ASUNTO { get; set; }
        public string REFERENCIA { get; set; }
        public string FECHA_INFORME { get; set; }
        public string FECHA_RECEPCION { get; set; } 
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USU_MODIF { get; set; }
        public string FECHA_MODIF { get; set; }
        public string NOMBRES_USU { get; set; }
        public string APEPAT_USU { get; set; }
        public string APEMAT_USU { get; set; }
        public string ARCHIVADOR { get; set; }
        public string ESTADO_INFORME { get; set; }
        public int ULT_NUM_CORRELATIVO { get; set; }

    }
}
