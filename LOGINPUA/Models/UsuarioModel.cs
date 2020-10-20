using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LOGINPUA.Models
{
    public class UsuarioModel
    {
        public int Nivel { get; set; }
        //Usuario
        public int USUCOD { get; set; }
        public string NDOCUMENTO { get; set; }
        public string USUNOM { get; set; }
        public string USUAPEPAT { get; set; }
        public string USUAPEMAT { get; set; }
        public string PERFIL { get; set; }

        //Login
        public int LOGCOD { get; set; }
        public string LOGUSU { get; set; }
        public string LOGCON { get; set; }
        //Login - Accesos
        public int LOGACCCOD { get; set; }
        public int ACCCOD { get; set; }
        //Acc - Empresa
        public int EMPCOD { get; set; }
        //LOG
        public string USUREG { get; set; }
        public DateTime FECREG { get; set; }
        public string USUMOD { get; set; }
        public DateTime FECMOD { get; set; }
        public int ESTREG { get; set; }
    }
}