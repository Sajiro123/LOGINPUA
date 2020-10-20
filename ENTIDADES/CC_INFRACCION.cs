using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class CC_INFRACCION
    {
        public int ID_INFRACCION { get; set; }
        public int ID_PERSONA_INCIDENCIA { get; set; }
        public string PERSONA_INCIDENCIA { get; set; }
        public string COD_INFRACCION { get; set; }
        public string DESCRIPCION { get; set; }
        public string TIPO_INCIDENCIA { get; set; }
        public string CALIFICACION { get; set; }
        public string MULTA_UIT { get; set; }
        public string REINCIDENCIA_UIT { get; set; }
        public string TIPO_INFRACCION { get; set; }
        public string SANCION { get; set; }
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USU_MODIF { get; set; }
        public string FECHA_MODIF { get; set; }
        public int ID_ESTADO { get; set; }


    }
}
