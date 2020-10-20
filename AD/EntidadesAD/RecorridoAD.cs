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
    public class RecorridoAD
    {
        OracleConnection conn;
        public RecorridoAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public List<CC_RECORRIDO> getRecorridosByRuta(int idRuta)
        {
            List<CC_RECORRIDO> resultado = new List<CC_RECORRIDO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Double) { Value = idRuta };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_GESTION_SALIDA.getRecorridosByRuta", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_RECORRIDO();

                            if (!DBNull.Value.Equals(bdRd["ID_RECORRIDO"])) { item.ID_RECORRIDO = Convert.ToInt32(bdRd["ID_RECORRIDO"]); }
                            if (!DBNull.Value.Equals(bdRd["SENTIDO"])) { item.SENTIDO = bdRd["SENTIDO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["LADO"])) { item.LADO = bdRd["LADO"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }
    }
}
