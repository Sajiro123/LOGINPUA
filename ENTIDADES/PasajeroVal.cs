using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class PasajeroVal
    {
        public int id_maestro { get; set; }
        public int id_paradero { get; set; }
        public int n_servicio { get; set; }
        public string Bus { get; set; }
        public string Placa { get; set; }
        public string Hora_excel { get; set; }
        public string Tarjeta { get; set; }
        public string Nombre_chofer { get; set; }
        public string Perfil { get; set; }
        public string Patron { get; set; }
        public string operador { get; set; }
        public int id_carrera { get; set; }
        public double monto { get; set; }
        public int id_estado { get; set; }
        public string usuario_session { get; set; }
        public string fecha_reg { get; set; }

    }
}
