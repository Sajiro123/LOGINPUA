using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LOGINPUA.Models
{
    public class BusesModel
    {
        public List<ENTIDADES.BUSES_DESPACHO> Lista_Dentro_Vehiculo { get; set; }
        public List<ENTIDADES.BUSES_DESPACHO> Lista_Exterior_Vehiculo { get; set; }
        public List<ENTIDADES.BUSES_DESPACHO> Lista_Cabina_Vehiculo { get; set; }
        public List<ENTIDADES.CC_BUSES> Lista_Buses { get; set; }
        public List<ENTIDADES.BUSES_DESPACHO> Lista_Buses_despacho { get; set; }
        public List<ENTIDADES.BUSES_DESPACHO> Lista_Revisiones_Mecanicas { get; set; } 
        public CC_BUSES CC_BUSES { get; set; }
        public  string  archivo{ get; set; }
    }
}