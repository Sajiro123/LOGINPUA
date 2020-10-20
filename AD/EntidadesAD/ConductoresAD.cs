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
    public class ConductoresAD : Repositorio<CC_CONDUCTORES>
    {
        OracleConnection conn;
        public ConductoresAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }



        public List<CC_CONDUCTORES> Listar_Conductores()
        {
            List<CC_CONDUCTORES> resultado = new List<CC_CONDUCTORES>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_CONDUCTORES.SP_LISTAR_CONDUCTORES", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_CONDUCTORES();
                            if (!DBNull.Value.Equals(bdRd["CODIGO"])) { item.CODIGO = Convert.ToInt32(bdRd["CODIGO"]); }
                            if (!DBNull.Value.Equals(bdRd["EMPRESA"])) { item.EMPRESA = bdRd["EMPRESA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["APELLIDOS"])) { item.APELLIDOS = bdRd["APELLIDOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRES"])) { item.NOMBRES = bdRd["NOMBRES"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["SITUACION"])) { item.SITUACION = bdRd["SITUACION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CONTIPLIC"])) { item.CONTIPLIC = bdRd["CONTIPLIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CONNUMLIC"])) { item.CONNUMLIC = bdRd["CONNUMLIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CONFECNAC"])) { item.CONFECNAC = bdRd["CONFECNAC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NUMDOC"])) { item.NUMDOC = bdRd["NUMDOC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["INICIO"])) { item.INICIO = bdRd["INICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["VIGENCIA"])) { item.VIGENCIA = bdRd["VIGENCIA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECREG"])) { item.FECREG = bdRd["FECREG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CONCOD"])) { item.CONCOD = Convert.ToInt32(bdRd["CONCOD"]); }


                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }
        public CC_CONDUCTORES Conductor_x_DNI(string nrodocumento)
        {
            CC_CONDUCTORES item = new CC_CONDUCTORES();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_DNI", OracleDbType.Varchar2) { Value = nrodocumento };


            using (var bdCmd = new OracleCommand("PKG_CONDUCTORES.SP_BUSCAR_CONDUCTOR", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            item = new CC_CONDUCTORES();
                            if (!DBNull.Value.Equals(bdRd["CODIGO"])) { item.CODIGO = Convert.ToInt32(bdRd["CODIGO"]); }
                            if (!DBNull.Value.Equals(bdRd["EMPRESA"])) { item.EMPRESA = bdRd["EMPRESA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["APELLIDOS"])) { item.APELLIDOS = bdRd["APELLIDOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRES"])) { item.NOMBRES = bdRd["NOMBRES"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CONTIPLIC"])) { item.CONTIPLIC = bdRd["CONTIPLIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CONNUMLIC"])) { item.CONNUMLIC = bdRd["CONNUMLIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CONFECNAC"])) { item.CONFECNAC = bdRd["CONFECNAC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NUMDOC"])) { item.NUMDOC = bdRd["NUMDOC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["INICIO"])) { item.INICIO = bdRd["INICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["VIGENCIA"])) { item.VIGENCIA = bdRd["VIGENCIA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_ESTADO"])) { item.ID_ESTADO = Convert.ToInt32(bdRd["ID_ESTADO"]); }

                        }
                    }
                }
            }
            return item;
        }
        public CC_PERSONAL Personal_x_DNI(string nrodocumento)
        {
            CC_PERSONAL item = new CC_PERSONAL();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_DNI", OracleDbType.Varchar2) { Value = nrodocumento };


            using (var bdCmd = new OracleCommand("PKG_CONDUCTORES.SP_BUSCAR_DNI_PERSONAL", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            item = new CC_PERSONAL();
                            if (!DBNull.Value.Equals(bdRd["NOMBRES_COMPLETOS"])) { item.NOMBRES_COMPLETOS = bdRd["NOMBRES_COMPLETOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["DNI"])) { item.DNI = bdRd["DNI"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CARGO"])) { item.CARGO = bdRd["CARGO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["EMPRESA"])) { item.EMPRESA = bdRd["EMPRESA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_ESTADO"])) { item.ID_ESTADO = Convert.ToInt32(bdRd["ID_ESTADO"]); }                            
                        }
                    }
                }
            }

            return item;
        }


    }
}
