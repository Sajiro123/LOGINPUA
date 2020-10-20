using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class BUSES_DESPACHO
    {
        public int ID_MAESTRO_DESPACHO { get; set; }
        public int CD_ID { get; set; }

        public int ID_BUSES_DESPACHO { get; set; }

        public string BS_PLACA { get; set; }
        public int BD_DESINFECCION { get; set; }

        public string BD_DIRECCION { get; set; }
        public string BD_CONCESIONARIO { get; set; }
        public int ID_CORREDOR { get; set; }
        public int? BD_KM { get; set; }
        public string COMENTARIO { get; set; }
        public string BD_LATITUD { get; set; }
        public string BD_LONGUITUD { get; set; }
        public string URL_FOTO { get; set; }
        public string URL_FOTO2 { get; set; }
        public string URL_FOTO3 { get; set; }
        public string URL_FOTO4 { get; set; }


        public int ID_CONCEPTOS { get; set; }
        public string CD_CONCEPTOS { get; set; }

        public string CD_TIPO_DOCUMENTO { get; set; }
        public int BD_ESTADO { get; set; }
        public int BD_CALIDAD { get; set; }
        public string BD_OBSERVACION { get; set; }


        public int ID_ESTADO { get; set; }
        public string USUREG { get; set; }
        public string STATUS { get; set; }




        public string CVS_FEC_FIN { get; set; }
        public string VEHICULOS_FIN { get; set; }
        public string RTV_FIN { get; set; }
        public string SOAT_FEC_FIN { get; set; }
        public string RC_FIN { get; set; }




    }
}
