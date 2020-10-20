using Dapper.Contrib.Extensions;

namespace ENTIDADES
{
    public class CC_RUTA
    {
        [ExplicitKey]
        public int ID_RUTA { get; set; }
        public string NOMBRE { get; set; }
        public string NOMBRE_ETIQUETA { get; set; }
        public string NRO_RUTA { get; set; }
        public string CORREDOR_NOMBRE { get; set; }
        public string DISTRITOS { get; set; }
        public string SENTIDOS { get; set; }
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string ABREV_CORREDOR { get; set; }

        
    }
}
