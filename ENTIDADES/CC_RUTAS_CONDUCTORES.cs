using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace ENTIDADES
{
    public class CC_RUTAS_CONDUCTORES
    {
        [ExplicitKey]
        public int ID_PLACAS_CONDUCTORES { get; set; }
        public string ITEM { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string PAQUETE { get; set; }
        public string PLACA { get; set; }
        public string TURNO { get; set; }
        public string PUNTO_INICIO { get; set; }
        public string HORA_PRESENTACION { get; set; }
        public string HORA_SALE_PATIO { get; set; }
        public string HORA_CABECERA { get; set; }
        public string HORA_TRABAJO { get; set; }
        public string VIAJES { get; set; }
        public string CONDUCTOR { get; set; }
        public int DNI { get; set; }
        public int CODIGO_CAC { get; set; }
        public string FECHA { get; set; }
        public string RUTA { get; set; }
        public string SERVICIO { get; set; }




    }
}
