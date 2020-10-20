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
    public class PersonaAD
    {
        OracleConnection conn;
        public PersonaAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
            //conn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC"));
        }

        public List<CC_TIPODOCUMENTO> ListarTipoDocumento()
        {
            List<CC_TIPODOCUMENTO> resultado = new List<CC_TIPODOCUMENTO>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_PERSONA.listarTipoDocumento", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_TIPODOCUMENTO();

                            if (!DBNull.Value.Equals(bdRd["ID_TIPO_DOCUMENTO"])) { item.ID_TIPO_DOCUMENTO = Convert.ToInt32(bdRd["ID_TIPO_DOCUMENTO"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_ROL> listarTipoRol()
        {
            List<CC_ROL> resultado = new List<CC_ROL>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_PERSONA.listarTipoRol", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_ROL();

                            if (!DBNull.Value.Equals(bdRd["ID_ROLPERSONA"])) { item.ID_ROLPERSONA = Convert.ToInt32(bdRd["ID_ROLPERSONA"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            resultado.Add(item);
                        }

                    }
                }
            }
            return resultado;
        }

        public List<CC_PERSONA> getlistaPersonas()
        {
            List<CC_PERSONA> resultado = new List<CC_PERSONA>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_PERSONA.listarPersona", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_PERSONA();

                            if (!DBNull.Value.Equals(bdRd["NOMBRES"])) { item.NOMBRES = bdRd["NOMBRES"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["APEPAT"])) { item.APEPAT = bdRd["APEPAT"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["APEMAT"])) { item.APEMAT = bdRd["APEMAT"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TIPODOC"])) { item.TIPODOC = bdRd["TIPODOC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NRODOCUMENTO"])) { item.NRODOCUMENTO = Convert.ToInt32(bdRd["NRODOCUMENTO"]); }
                            if (!DBNull.Value.Equals(bdRd["ROL"])) { item.ROL = bdRd["ROL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CORREO"])) { item.CORREO = bdRd["CORREO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_PERSONA"])) { item.ID_PERSONA = Convert.ToInt32(bdRd["ID_PERSONA"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_TIPO_DOCUMENTO"])) { item.ID_TIPO_DOCUMENTO = Convert.ToInt32(bdRd["ID_TIPO_DOCUMENTO"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_ROLPERSONA"])) { item.ID_ROLPERSONA = Convert.ToInt32(bdRd["ID_ROLPERSONA"]); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }

                            

                            resultado.Add(item);
                        }

                    }
                }
            }
            return resultado;
        }


        public RPTA_GENERAL registrarPersona(string nombre, string apepaterno, string apematerno, string numdocu, int tipodocu, string correo, int tiporol, string usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[9];
            bdParameters[0] = new OracleParameter("P_ID_TIPO_DOCUMENTO", OracleDbType.Int32) { Value = tipodocu };
            bdParameters[1] = new OracleParameter("P_ID_ROLPERSONA", OracleDbType.Int32) { Value = tiporol };
            bdParameters[2] = new OracleParameter("P_NOMBRES", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[3] = new OracleParameter("P_APEPAT", OracleDbType.Varchar2) { Value = apepaterno };
            bdParameters[4] = new OracleParameter("P_APEMAT", OracleDbType.Varchar2) { Value = apematerno };
            bdParameters[5] = new OracleParameter("P_NRODUCUMENTO", OracleDbType.Int32) { Value = numdocu };
            bdParameters[6] = new OracleParameter("P_CORREO", OracleDbType.Varchar2) { Value = correo };
            bdParameters[7] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[8] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PERSONA.registrarPersona", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
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

        public RPTA_GENERAL ModificarPersonas(int idpersona, string nombre, string apepaterno, string apematerno, int numdocu, int tipodocu, string correo, int tiporol, string usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[10];
            bdParameters[0] = new OracleParameter("P_ID_PERSONA", OracleDbType.Int32) { Value = tiporol };
            bdParameters[1] = new OracleParameter("P_ID_PERSONA", OracleDbType.Int32) { Value = idpersona };
            bdParameters[2] = new OracleParameter("P_NOMBRES", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[3] = new OracleParameter("P_APEPAT", OracleDbType.Varchar2) { Value = apepaterno };
            bdParameters[4] = new OracleParameter("P_APEMAT", OracleDbType.Varchar2) { Value = apematerno };
            bdParameters[5] = new OracleParameter("P_NRODUCUMENTO", OracleDbType.Int32) { Value = numdocu };
            bdParameters[6] = new OracleParameter("P_CORREO", OracleDbType.Varchar2) { Value = correo };
            bdParameters[7] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[8] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[9] = new OracleParameter("P_ID_TIPODOC", OracleDbType.Int32) { Value = tipodocu };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PERSONA.modificarPersona", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se modifico correctamente";
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


        public RPTA_GENERAL EliminarPersonas(int idpersona,string usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_ID_PARADERO", OracleDbType.Int32) { Value = idpersona };
            bdParameters[1] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[2] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
                         
            try
            {
                using (var bdCmd = new OracleCommand("PKG_PERSONA.anularPersona", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se elimino correctamente";
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
        public RPTA_GENERAL EditarDatosPersonas(int idpersona, string nombre, string apepaterno, string apematerno, string numdocu, string correo,string usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[8];
            bdParameters[0] = new OracleParameter("P_ID_PERSONA", OracleDbType.Int32) { Value = idpersona };
            bdParameters[1] = new OracleParameter("P_NOMBRES", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[2] = new OracleParameter("P_APEPAT", OracleDbType.Varchar2) { Value = apepaterno };
            bdParameters[3] = new OracleParameter("P_APEMAT", OracleDbType.Varchar2) { Value = apematerno };
            bdParameters[4] = new OracleParameter("P_NRODUCUMENTO", OracleDbType.Int32) { Value = numdocu };
            bdParameters[5] = new OracleParameter("P_CORREO", OracleDbType.Varchar2) { Value = correo };
            bdParameters[6] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[7] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = usuario };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PERSONA.modificarDatosPersona", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se modifico correctamente";
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


