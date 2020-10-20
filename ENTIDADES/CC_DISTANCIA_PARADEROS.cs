using Dapper.Contrib.Extensions;
using System;

namespace Entidades
{
    public class TM_PROGRAMACION_IMG
    {
        [ExplicitKey]
        public int ID_PROGRAMACION_FOTOS { get; set; }
        public string FOTOS_URL { get; set; }
          public DateTime FOTOS_FECHA { get; set; }
        public string FOTOS_DESCRIPCION { get; set; }
        public string ID_CORREDOR { get; set; }

     }
}
