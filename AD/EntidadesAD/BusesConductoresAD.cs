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
using System.Data.SqlClient;

namespace AD.EntidadesAD
{
    public class BusesConductoresAD
    {
        OracleConnection conn;

        public BusesConductoresAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public DataSet Listar_Informes(int mes, int año)
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_BUSESCONDUCTORES.SP_LISTAR_INFORMES";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter cursor_A = new OracleParameter();
                    cursor_A.ParameterName = "P_CURSOR";
                    cursor_A.OracleDbType = OracleDbType.RefCursor;
                    cursor_A.Direction = ParameterDirection.Output;

                    OracleParameter comparativoB = new OracleParameter();
                    comparativoB.ParameterName = "P_NUM_MES";
                    comparativoB.OracleDbType = OracleDbType.Varchar2;
                    comparativoB.Direction = ParameterDirection.Input;
                    comparativoB.Value = mes;

                    OracleParameter comparativoC = new OracleParameter();
                    comparativoC.ParameterName = "P_NUM_ANIO";
                    comparativoC.OracleDbType = OracleDbType.Varchar2;
                    comparativoC.Direction = ParameterDirection.Input;
                    comparativoC.Value = año;



                    command.Parameters.Add(cursor_A);
                    command.Parameters.Add(comparativoB);
                    command.Parameters.Add(comparativoC);



                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
                return ds;
            }
        }

        public RPTA_GENERAL nombre_excel(int id)
        {
            var item = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID", OracleDbType.Varchar2) { Value = id };



            using (var bdCmd = new OracleCommand("PKG_BUSESCONDUCTORES.NOM_EXCEL", conn))
            {

                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Connection.Open();
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            if (!DBNull.Value.Equals(bdRd["AUX"])) { item.DES_ESTADO = bdRd["AUX"].ToString(); }
                        }
                    }
                }
            }
            return item;

        }


        public RPTA_GENERAL eliminar_Archivo(int id)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_NOMBRE", OracleDbType.Int32) { Value = id };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_BUSESCONDUCTORES.ELIMINAR_ARCHIVO", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.Connection.Open();
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se actualizo correctamente";
                }
            }
            catch (Exception ex)
            {
                r.AUX = 0;
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
            }
            return r;

        }

        public RPTA_GENERAL ValidarFecha_Programados(string fecha)
        {
            var item = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha };



            using (var bdCmd = new OracleCommand("PKG_BUSESCONDUCTORES.VALIDARFECHA", conn))
            {

                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            if (!DBNull.Value.Equals(bdRd["AUX"])) { item.DES_ESTADO = bdRd["AUX"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID"])) { item.AUX = Convert.ToInt32(bdRd["ID"]); }
                        }
                    }
                }
            }
            return item;

        }
        public RPTA_GENERAL RegistroInforme(string nombre, string fecha, string usuario_session)

        {
            DateTime dateTime = DateTime.UtcNow.Date;
            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[1] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha };
            bdParameters[2] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario_session };
            bdParameters[3] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_BUSESCONDUCTORES.SP_INSERTAR_INFORMES", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.Connection.Open();
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                }
            }
            catch (Exception ex)
            {
                r.AUX = 0;
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
            }
            return r;

        }
    }
}








