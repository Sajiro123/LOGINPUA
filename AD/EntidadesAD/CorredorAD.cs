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
    public class CorredorAD
    {
        OracleConnection conn;

        public CorredorAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        //public List<CC_CORREDOR> getListaCorredor() //cabecera de la programación
        //{
        //    var bdParameters = new OracleDynamicParameters();
        //    bdParameters.Add("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
        //    //bdParameters.Add("ID_CORREDOR", OracleDbType.Int32, ParameterDirection.Input, 1);
        //    var query = "PKG_PROGRAMACION_BUSES.GET_LISTA_CORREDORES";
        //    var result = SqlMapper.Query<CC_CORREDOR>(conn, query, param: bdParameters, commandType: CommandType.StoredProcedure).ToList();
        //    return result;
        //}

        public List<CC_CORREDOR> getListaCorredor()
        {
            List<CC_CORREDOR> resultado = new List<CC_CORREDOR>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.GET_LISTA_CORREDORES", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                bdCmd.Connection.Open();
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_CORREDOR();
                            if (!DBNull.Value.Equals(bdRd["ID_CORREDOR"])) { item.ID_CORREDOR = Convert.ToInt32(bdRd["ID_CORREDOR"]); }
                            if (!DBNull.Value.Equals(bdRd["CORREDOR_NOMBRE"])) { item.CORREDOR_NOMBRE = bdRd["CORREDOR_NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ABREVIATURA"])) { item.ABREVIATURA = bdRd["ABREVIATURA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_MODALIDAD_TRANS"])) { item.ID_MODALIDAD_TRANS = Convert.ToInt32(bdRd["ID_MODALIDAD_TRANS"]); }

                            resultado.Add(item);
                        }
                    }
                }
             }
            return resultado;
        }


        public List<CC_CORREDOR> getRutaCorredor()
        {
            List<CC_CORREDOR> resultado = new List<CC_CORREDOR>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_BUSES.GET_LISTA_CORREDORES", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                 using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_CORREDOR();
                            if (!DBNull.Value.Equals(bdRd["ID_CORREDOR"])) { item.ID_CORREDOR = Convert.ToInt32(bdRd["ID_CORREDOR"]); }
                            if (!DBNull.Value.Equals(bdRd["CORREDOR_NOMBRE"])) { item.CORREDOR_NOMBRE = bdRd["CORREDOR_NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ABREVIATURA"])) { item.ABREVIATURA = bdRd["ABREVIATURA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_MODALIDAD_TRANS"])) { item.ID_MODALIDAD_TRANS = Convert.ToInt32(bdRd["ID_MODALIDAD_TRANS"]); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

    }
}
