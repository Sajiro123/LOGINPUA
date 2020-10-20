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
    public class RutaAD
    {
        OracleConnection conn;

        public RutaAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);

        }
        public List<CC_RUTA> getRutaxCorredor(int idCorredor) //cabecera de la programación
        {

            List<CC_RUTA> resultado = new List<CC_RUTA>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = idCorredor }; ;

            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.GET_RUTA_X_CORREDOR", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_RUTA();

                            if (!DBNull.Value.Equals(bdRd["ID_RUTA"])) { item.ID_RUTA = Convert.ToInt32(bdRd["ID_RUTA"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NRO_RUTA"])) { item.NRO_RUTA = bdRd["NRO_RUTA"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_RUTA_CORREDOR> getRutaCorredor() //cabecera de la programación
        {

            List<CC_RUTA_CORREDOR> resultado = new List<CC_RUTA_CORREDOR>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.GET_RUTAS_CORREDOR", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_RUTA_CORREDOR();

                            if (!DBNull.Value.Equals(bdRd["ID_RUTA"])) { item.ID_RUTA = Convert.ToInt32(bdRd["ID_RUTA"]); }
                            if (!DBNull.Value.Equals(bdRd["NRO_RUTA"])) { item.NRO_RUTA = bdRd["NRO_RUTA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ABREV_CORREDOR"])) { item.ABREV_CORREDOR = bdRd["ABREV_CORREDOR"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }




        public List<CC_RUTA_CORREDOR> getRuta_X_modalidad(int modalida_transpo)
        {
            List<CC_RUTA_CORREDOR> resultado = new List<CC_RUTA_CORREDOR>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_MODALIDAD", OracleDbType.Int32) { Value = modalida_transpo }; ;

            using (var bdCmd = new OracleCommand("PKG_RUTA.getRuta_X_modalidad", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_RUTA_CORREDOR();

                            if (!DBNull.Value.Equals(bdRd["ID_RUTA"])) { item.ID_RUTA = Convert.ToInt32(bdRd["ID_RUTA"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NRO_RUTA"])) { item.NRO_RUTA = bdRd["NRO_RUTA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ABREV_CORREDOR"])) { item.ABREV_CORREDOR = bdRd["ABREV_CORREDOR"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public List<CC_RUTA_TIPO_SERVICIO> getRutaSerOpe_X_modalidad(int id_ruta)
        {
            List<CC_RUTA_TIPO_SERVICIO> resultado = new List<CC_RUTA_TIPO_SERVICIO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = id_ruta }; ;

            using (var bdCmd = new OracleCommand("PKG_RUTA.getRuta_ServOper", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_RUTA_TIPO_SERVICIO();

                            if (!DBNull.Value.Equals(bdRd["ID_RUTA_TIPO_SERVICIO"])) { item.ID_RUTA_TIPO_SERVICIO = Convert.ToInt32(bdRd["ID_RUTA_TIPO_SERVICIO"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_RUTA"])) { item.ID_RUTA = Convert.ToInt32(bdRd["ID_RUTA"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_TIPO_SERVICIO"])) { item.ID_TIPO_SERVICIO = Convert.ToInt32(bdRd["ID_TIPO_SERVICIO"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE_ETIQUETA"])) { item.NOMBRE_ETIQUETA = bdRd["NOMBRE_ETIQUETA"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_RECORRIDO> getLado_X_Ruta(int id_ruta)
        {
            List<CC_RECORRIDO> resultado = new List<CC_RECORRIDO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = id_ruta }; ;

            using (var bdCmd = new OracleCommand("PKG_RUTA.getLado_X_Ruta", conn))
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

                            if (!DBNull.Value.Equals(bdRd["ID_RUTA"])) { item.ID_RUTA = Convert.ToInt32(bdRd["ID_RUTA"]); }
                            if (!DBNull.Value.Equals(bdRd["SENTIDO"])) { item.SENTIDO = bdRd["SENTIDO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["LADO"])) { item.LADO = bdRd["LADO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NRO_RUTA"])) { item.NRO_RUTA = Convert.ToInt32(bdRd["NRO_RUTA"]); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

    }
}
