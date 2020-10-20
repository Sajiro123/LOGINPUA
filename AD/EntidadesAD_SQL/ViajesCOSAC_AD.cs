using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
//using AD.EntidadesAD_SQL;

namespace AD.EntidadesAD_SQL
{
    public class ViajesCOSAC_AD
    {
        
        public DataSet consultaViajesCOSAC(string etiqueta_ruta,string fechaConsulta, string CadenaConexion)
        {

            //string Sql = "ProcBoleto";
            SqlParameter[] Parametros = new SqlParameter[2];
            DataSet Ds = new DataSet();
            AD.ConexionSQL.Conexion ObjSQL = new AD.ConexionSQL.Conexion();
            try
            {
                string Sql = "dbo.csp_ticketSysMileageExport6";
                Parametros[0] = new SqlParameter("@nombre_ruta", System.Data.SqlDbType.VarChar);
                Parametros[0].Value = etiqueta_ruta;
                Parametros[1] = new SqlParameter("@fechaConsulta", System.Data.SqlDbType.VarChar);
                Parametros[1].Value = fechaConsulta;

                ObjSQL.CadenaConexion = CadenaConexion;
                Ds = ObjSQL.CargarDs(Sql, Parametros);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return Ds;
        }
    }
}
