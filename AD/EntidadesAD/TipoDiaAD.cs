using DA;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using ENTIDADES;
using Oracle.DataAccess.Client;
using System.Data;
using System;

namespace AD.EntidadesAD
{
    public class TipoDiaAD
    {
        OracleConnection conn;

        public TipoDiaAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public List<CC_TIPO_DIA> getTipoDias() //cabecera de la programación
        {
            List<CC_TIPO_DIA> resultado = new List<CC_TIPO_DIA>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.GET_TIPO_DIAS", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_TIPO_DIA();

                            if (!DBNull.Value.Equals(bdRd["ID_TIPO_DIA"])) { item.ID_TIPO_DIA = Convert.ToInt32(bdRd["ID_TIPO_DIA"]); }
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
