using Dapper.Contrib.Extensions;


namespace ENTIDADES
{
    public class CC_TIPO_DIA
    {
        [ExplicitKey]
        public int ID_TIPO_DIA { get; set; }
        public string NOMBRE { get; set; }        
    }
}
