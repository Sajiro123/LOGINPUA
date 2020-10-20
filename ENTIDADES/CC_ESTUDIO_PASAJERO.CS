using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace ENTIDADES
{
    public class CC_ESTUDIO_PASAJERO
    {
        [ExplicitKey]
        public int ID_ESTUD_PASAJ { get; set; }
        public int ID_CORREDOR { get; set; }
        public string NOMBRE_CORREDOR { get; set; }
        public int ID_RUTA { get; set; }
        public string NUM_RUTA { get; set; }
        public int ID_BUS { get; set; }
        public string PLACA_BUS { get; set; }
        public int ID_PERSONA { get; set; }
        public string LADO { get; set; }
        public int ID_PARADERO { get; set; }
        public string PARADERO { get; set; }
        public string PARADERO_ORIG { get; set; }
        public string PARADERO_DEST { get; set; }
        public string HORA_INICIO { get; set; }
        public string HORA_FIN { get; set; }
        public int PASAJ_SUBEN { get; set; }
        public int PASAJ_BAJAN { get; set; }
        public string OBSERVACIONES { get; set; }
        public int PASAJ_COLA { get; set; }
        public string TIPO_TARJETA { get; set; }
        public string TIPO_OBSERVACION { get; set; }
        public string TIPO_VEHICULO { get; set; }
        public string PLACA_COLECTIVO { get; set; }
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USU_MODIF { get; set; }
        public string USU_ANULA { get; set; }
        public int CAPACIDAD_LLEGADA { get; set; }
        public int NRO_ORDEN { get; set; }
        public int CAPACIDAD_SALIDA { get; set; }
        public int NUMERO_SERVICIO { get; set; }
        public int ASIENTOS_LIBRES { get; set; }
    }
    
}
