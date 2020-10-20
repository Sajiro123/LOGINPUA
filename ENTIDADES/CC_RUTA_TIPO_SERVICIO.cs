using System;
using Dapper.Contrib.Extensions;


namespace ENTIDADES
{
    public class CC_RUTA_TIPO_SERVICIO
    {
        public int ID_RUTA_TIPO_SERVICIO { get; set; }
        public int ID_TIPO_SERVICIO { get; set; }
        public int ID_TIPOSERVICIO_OPER { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO_SERVICIO { get; set; }
        public string TIPO_OPERACIONAL { get; set; }
        public string NOMBRE_ETIQUETA { get; set; }
        public string NRO_RUTA { get; set; }
        public string LADOS { get; set; }
        public string LADOS_TSERV { get; set; }
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }
        public int ID_RUTA { get; set; }
        public string COLOR { get; set; }
    }
}
