using Dapper.Contrib.Extensions;

namespace ENTIDADES
{
    public class CC_DATOS_CORREDOR
    {
        [ExplicitKey]
        public string CORREDOR_NOMBRE { get; set; }
        public string HORARIO_RUTA_DIAS { get; set; }
        public string HORARIO_RUTA_INICIO { get; set; }
        public string HORARIO_RUTA_FIN { get; set; }
        public string TIPOLOGIA_NOMBRE { get; set; }

        public string DIAS { get; set; }
    }
}
