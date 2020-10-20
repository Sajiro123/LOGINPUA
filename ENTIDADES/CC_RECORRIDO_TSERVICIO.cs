using Dapper.Contrib.Extensions;

namespace ENTIDADES
{
    public class CC_RECORRIDO_TSERVICIO
    {
        public int IDRECORRIDOTIPOSERVICIO { get; set; }
        public int ID_RECORRIDO { get; set; }
        public string LADO { get; set; }

        public string SENTIDO { get; set; }
    }
}
