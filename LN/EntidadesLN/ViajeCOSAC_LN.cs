using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using AD.EntidadesAD_SQL;
using AD;
 using LOGINPUA.Util;

namespace LN.EntidadesLN
{
    public class ViajeCOSAC_LN
    {   
        string cadenaConexionCOSAC = "ConexionORBCAD";
        private Object bdConn;
        public DataSet consultaViajesCOSAC(string etiqueta_ruta, string fechaConsulta, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = new ViajesCOSAC_AD().consultaViajesCOSAC(etiqueta_ruta, fechaConsulta, cadenaConexionCOSAC);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }
    }
}
