using Dapper.Contrib.Extensions;
using System;

namespace ENTIDADES
{
    public class TB_USUARIO
    {
        [ExplicitKey]
        public int USUCOD { get; set; }
        public string NDOCUMENTO { get; set; }
        public string PERFIL { get; set; }

        public string LOGCON { get; set; }
     
        public string USUNOM { get; set; }
        public string USUAPEPAT { get; set; }
        public string USUAPEMAT { get; set; }

        public string USUMCONT { get; set; }
        public string CODEMP { get; set; }

        public string LOGUSU { get; set; }

        public string PASSWORD { get; set; }
        public string USUREG { get; set; }
        public DateTime FECREG { get; set; }

         
        public string FECHA_REGISTRO { get; set; }
        public string USUMOD { get; set; }
        public DateTime FECMOD { get; set; }
        public int ESTREG { get; set; }
    }
}
