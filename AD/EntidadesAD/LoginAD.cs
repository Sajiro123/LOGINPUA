using DA;
using Dapper;
using Entidades;
using ENTIDADES;
using ENTIDADES.InnerJoin;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace AD.EntidadesAD
{

    public class LoginAD
    {
        OracleConnection conn;
        public LoginAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public CC_USUARIO_PERSONA Verificar_Usuario(string usuario, string contraseña)
        {
            var item = new CC_USUARIO_PERSONA();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_LOGUSU", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
 
            using (var bdCmd = new OracleCommand("PKG_LOGIN.SP_AUTENTICAR_USUARIO", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            if (!DBNull.Value.Equals(bdRd["ID_USUARIO"])) { item.ID_USUARIO = Convert.ToInt32(bdRd["ID_USUARIO"]); }
                        }
                    }
                }
            }
            return item;
        }
        public DataSet IngresarIn(string usuario, string clave)
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {
 
                    command.CommandText = "PKG_LOGIN.SP_INGRESAR_LOGIN_REAL";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter comparativoA = new OracleParameter();
                    comparativoA.ParameterName = "P_LOGUSU";
                    comparativoA.OracleDbType = OracleDbType.Varchar2;
                    comparativoA.Direction = ParameterDirection.Input;
                    comparativoA.Value = usuario;

                    OracleParameter comparativoB = new OracleParameter();
                    comparativoB.ParameterName = "P_LOGCON";
                    comparativoB.OracleDbType = OracleDbType.Varchar2;
                    comparativoB.Direction = ParameterDirection.Input;
                    comparativoB.Value = clave;

                    OracleParameter cursor_A = new OracleParameter();
                    cursor_A.ParameterName = "P_CURSOR_A";
                    cursor_A.OracleDbType = OracleDbType.RefCursor;
                    cursor_A.Direction = ParameterDirection.Output;

                    OracleParameter cursor_B = new OracleParameter();
                    cursor_B.ParameterName = "P_CURSOR_B";
                    cursor_B.OracleDbType = OracleDbType.RefCursor;
                    cursor_B.Direction = ParameterDirection.Output;

                    OracleParameter cursor_C = new OracleParameter();
                    cursor_C.ParameterName = "P_CURSOR_C";
                    cursor_C.OracleDbType = OracleDbType.RefCursor;
                    cursor_C.Direction = ParameterDirection.Output;

                    OracleParameter cursor_D = new OracleParameter();
                    cursor_D.ParameterName = "P_CURSOR_D";
                    cursor_D.OracleDbType = OracleDbType.RefCursor;
                    cursor_D.Direction = ParameterDirection.Output;

                    OracleParameter cursor_E = new OracleParameter();
                    cursor_E.ParameterName = "P_CURSOR_E";
                    cursor_E.OracleDbType = OracleDbType.RefCursor;
                    cursor_E.Direction = ParameterDirection.Output;

                    OracleParameter cursor_F = new OracleParameter();
                    cursor_F.ParameterName = "P_CURSOR_F";
                    cursor_F.OracleDbType = OracleDbType.RefCursor;
                    cursor_F.Direction = ParameterDirection.Output;

                    command.Parameters.Add(comparativoA);
                    command.Parameters.Add(comparativoB);
                    command.Parameters.Add(cursor_A);
                    command.Parameters.Add(cursor_B);
                    command.Parameters.Add(cursor_C);
                    command.Parameters.Add(cursor_D);
                    command.Parameters.Add(cursor_E);
                    command.Parameters.Add(cursor_F);


                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);

                }
                return ds;

            }
        }

    }
}
