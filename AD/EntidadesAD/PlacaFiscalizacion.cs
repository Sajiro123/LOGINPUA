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
    public class PlacaFiscalizacion
    {
        OracleConnection conn;
        public PlacaFiscalizacion(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public List<CONADISPLACA> getPlacasConadis()
        {
            List<CONADISPLACA> resultado = new List<CONADISPLACA>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_GESTION_SALIDA.getPlacasConadis", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CONADISPLACA();

                            if (!DBNull.Value.Equals(bdRd["ID_PLACAS"])) { item.ID_PLACA = Convert.ToInt32(bdRd["ID_PLACAS"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["PLACA"])) { item.PLACA = bdRd["PLACA"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<VECINOSPLACA> getplacasVecinosEmp()
        {
            List<VECINOSPLACA> resultado = new List<VECINOSPLACA>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_GESTION_SALIDA.getPlacasVecinos", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new VECINOSPLACA();

                            if (!DBNull.Value.Equals(bdRd["ID_VECINOSEMPA"])) { item.ID_VECINOSEMPA = Convert.ToInt32(bdRd["ID_VECINOSEMPA"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["NUMERO_PLACA"])) { item.NUMERO_PLACA = bdRd["NUMERO_PLACA"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

    }
}
