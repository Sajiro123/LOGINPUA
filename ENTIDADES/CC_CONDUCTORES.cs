using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace ENTIDADES
{
    public class CC_CONDUCTORES
    {
        [ExplicitKey]
        public int CODIGO { get; set; }
        public string CODIGO_EMPRESA { get; set; }
        public string EMPRESA { get; set; }
        public string APELLIDOS { get; set; }
        public string NOMBRES { get; set; }
        public string SITUACION { get; set; }
        public string CONTIPLIC { get; set; }
        public string CONNUMLIC { get; set; }
        public string CONFECNAC { get; set; }
        public string NUMDOC { get; set; }
        public string INICIO { get; set; }
        public string VIGENCIA { get; set; }
        public string PLACA { get; set; }
        public string FECREG { get; set; }

        public int ID_ESTADO { get; set; }

        public int CONCOD { get; set; }

    }
}
