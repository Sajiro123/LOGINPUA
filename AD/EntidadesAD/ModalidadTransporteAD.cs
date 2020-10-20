using DA;
using Dapper;
using Entidades;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio;
using ENTIDADES;

namespace AD.EntidadesAD
{
    public class ModalidadTransporteAD
    {
        OracleConnection conn;
        public ModalidadTransporteAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
            //conn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC"));
        }
        public List<CC_MODALIDAD_TRANSPORTE> getModalidadTransporte()
        {
            List<CC_MODALIDAD_TRANSPORTE> resultado = new List<CC_MODALIDAD_TRANSPORTE>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_RUTA_TIPO_SRV.mostrarModalidadTransporte", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_MODALIDAD_TRANSPORTE();
                            if (!DBNull.Value.Equals(bdRd["ID_MODALIDAD_TRANS"])) { item.ID_MODALIDAD_TRANS = Convert.ToInt32(bdRd["ID_MODALIDAD_TRANS"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }
    }
}
