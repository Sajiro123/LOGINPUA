using Dapper.Contrib.Extensions;
namespace ENTIDADES
{
    public class CC_RECORRIDO
    {


        public int ID_RUTA { get; set; }
        public int NRO_RUTA { get; set; }
        public int ID_RECORRIDO { get; set; }
        public string SENTIDO { get; set; }
        public string LADO { get; set; }
        public double MEDIDA_KM { get; set; }

    }
}
