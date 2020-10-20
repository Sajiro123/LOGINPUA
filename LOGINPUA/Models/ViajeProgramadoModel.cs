using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOGINPUA.Models
{
    public class ViajeProgramadoModel
    {
        public int id_maestro { get; set; }
        public string tipoDia { get; set; }
        public string ruta { get; set; }
        public string route { get; set; }
        public string blk { get; set; }
        public string pog { get; set; }
        public string pot { get; set; }
        public string fNode { get; set; }
        public string fTime { get; set; }
        public string tTime { get; set; }
        public string tNode { get; set; }
        public string pit { get; set; }
        public string lay { get; set; }
        public string tripTime { get; set; }
        public double distancia { get; set; }
        public double acumulado { get; set; }
        public string sentido { get; set; }
        public string concesionario { get; set; }
        public string turno { get; set; }
        public string tipoDeServicio { get; set; }
        public string placa { get; set; }
        public string codigoConductorCAC { get; set; }
    }
}