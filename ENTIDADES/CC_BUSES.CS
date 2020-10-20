using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace ENTIDADES
{
    public class CC_BUSES
    {
        [ExplicitKey]
        public int BS_ID { get; set; }
        public int ID_CORREDOR { get; set; }
        public string ABREVIATURA { get; set; }
        public string BS_FECINI_PT { get; set; }
        public string BS_NOM_EMPRE { get; set; }
        public string BS_PADRON { get; set; }
        public string BS_PLACA { get; set; }
        public string BS_PROPIETARIO { get; set; }
        public string BS_PAQUETE_CONCESION { get; set; }
        public string BS_PAQUETE_SERVICIO { get; set; }
        public string BS_TIPO_SERVICIO { get; set; }
        public string BS_ESTADO { get; set; }
        public string BS_MARCA { get; set; }
        public string BS_MODELO { get; set; }
        public string BS_AÑO_FABRICACION { get; set; }
        public string BS_COMBUSTIBLE { get; set; }
        public string BS_TECNOLOGIA_EURO { get; set; }
        public string BS_POTENCIA_MOTOR { get; set; }
        public string BS_SERIE_MOTOR { get; set; }
        public string BS_SERIE_CHASIS { get; set; }
        public string BS_COLOR_VEHICULO { get; set; }
        public string BS_LONGITUD { get; set; }
        public string BS_ASIENTOS { get; set; }
        public string BS_AREA_PASILLO { get; set; }
        public string BS_PESO_NETO { get; set; }
        public string BS_PESO_BRUTO { get; set; }
        public string BS_ALTURA { get; set; }
        public string BS_ANCHO { get; set; }
        public string BS_CARGA_UTIL { get; set; }
        public string BS_PUERTA_IZQUIERDA { get; set; }
        public string BS_PARTIDA_REGISTRAL { get; set; }
        public string BS_CODIGO_CVS { get; set; }
        public string BS_CVS_FEC_INIC { get; set; }
        public string BS_CVS_FEC_FIN { get; set; }
        public string BS_SOAT_FEC_INIC { get; set; }
        public string BS_SOAT_FEC_FIN { get; set; }
        public string BS_POLIZA_VEHICULOS { get; set; }
        public string BS_VEHICULOS_INIC { get; set; }
        public string BS_VEHICULOS_FIN { get; set; }
        public string BS_POLIZA_CIVIL { get; set; }
        public string BS_RC_INICIO { get; set; }
        public string BS_RC_FIN { get; set; }
        public string BS_POLIZA_ACCI_COLECTIVOS { get; set; }
        public string BS_APCP_INIC { get; set; }
        public string BS_APCP_FIN { get; set; }
        public string BS_POLIZA_MULTIRIESGOS { get; set; }
        public string BS_MULTIRESGOS_INIC { get; set; }
        public string BS_MULTIRESGOS_FIN { get; set; }
        public string BS_RTV_INIC { get; set; }
        public string BS_RTV_FIN { get; set; }
        public string BS_REVI_ANUAL_GNV_INIC { get; set; }
        public string BS_REVI_ANUAL_GNV_FIN { get; set; }
        public string BS_REVISION_CILINDROS_GNV_INIC { get; set; }
        public string BS_REVISION_CILINDROS_GNV_FIN { get; set; }
        public string COD_CAC { get; set; }
        public string ESTADO_VEHICULO { get; set; }
        public string PLACA_REEMPLAZADA { get; set; }
        public string USU_REG { get; set; }
        public string USU_MODIF { get; set; }
        public string USU_ANULA { get; set; }
        public string CORREDOR_NOMBRE { get; set; }

        
        public int ID_ESTADO { get; set; }
    }
}
