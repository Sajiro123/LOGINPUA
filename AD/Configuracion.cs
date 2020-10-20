using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace AD
{
    public sealed class Configuracion
    {
        public static string GetConectionSting(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static string GetParameter(string name)
        {
            return ConfigurationManager.AppSettings[name].ToString();
        }

    }
}
