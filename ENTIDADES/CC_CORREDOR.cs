using Dapper.Contrib.Extensions;

namespace ENTIDADES
{
    public class CC_CORREDOR
    {
        [ExplicitKey]
        public int ID_CORREDOR { get; set; }
        public int ID_PROV_SERV { get; set; } 
        
        public string CORREDOR_NOMBRE { get; set; }
        public string ABREVIATURA { get; set; }
        public int ID_MODALIDAD_TRANS { get; set; }
        
    }
}
