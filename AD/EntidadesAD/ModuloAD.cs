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
    public class ModuloAD
    {
        OracleConnection conn;
        public ModuloAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
            //conn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC"));
        }

        public List<CC_MODALIDAD> ListarModalidad()
        {
            List<CC_MODALIDAD> resultado = new List<CC_MODALIDAD>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_TBUSUARIO.listarModalidad", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_MODALIDAD();

                            if (!DBNull.Value.Equals(bdRd["ID_MODALIDAD_TRANS"])) { item.ID_MODALIDAD_TRANS = Convert.ToInt32(bdRd["ID_MODALIDAD_TRANS"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public RPTA_GENERAL AnularModulo(int idperfil, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_ID_MODULO", OracleDbType.Int32) { Value = idperfil };
            bdParameters[1] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[2] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_MODULOSISTEMA.anularModulo", conn))
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
        public List<CC_MODULO> ListarModulo(int idmodalidad)
        {
            List<CC_MODULO> resultado = new List<CC_MODULO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_MODALIDAD", OracleDbType.Varchar2) { Value = idmodalidad };

            using (var bdCmd = new OracleCommand("PKG_MODULOSISTEMA.listarModulo", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_MODULO();

                            if (!DBNull.Value.Equals(bdRd["ID_MODULO_SISTEMA"])) { item.ID_MODULO_SISTEMA = Convert.ToInt32(bdRd["ID_MODULO_SISTEMA"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_MODALIDAD_TRANS"])) { item.ID_MODALIDAD_TRANS = Convert.ToInt32(bdRd["ID_MODALIDAD_TRANS"]); }
                            if (!DBNull.Value.Equals(bdRd["MODALIDAD"])) { item.MODALIDAD = bdRd["MODALIDAD"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["URL"])) { item.URL = bdRd["URL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["IMAGEN"])) { item.IMAGEN = bdRd["IMAGEN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }



                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public RPTA_GENERAL AgregarModulo(int idmodalidad, string nombre, string url, string img, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            var id_modulo = 0;

            OracleParameter[] bdParameters = new OracleParameter[7];
            bdParameters[0] = new OracleParameter("P_IDMODALIDAD", OracleDbType.Int32) { Value = idmodalidad };
            bdParameters[1] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[2] = new OracleParameter("P_URL", OracleDbType.Varchar2) { Value = url };
            bdParameters[3] = new OracleParameter("P_IMAGEN", OracleDbType.Varchar2) { Value = img };
            bdParameters[4] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[5] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[6] = new OracleParameter("P_ID_MODULO", OracleDbType.Int32, direction: ParameterDirection.Output);


            try
            {
                using (var bdCmd = new OracleCommand("PKG_MODULOSISTEMA.registrarModulo", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    id_modulo = int.Parse(bdCmd.Parameters["P_ID_MODULO"].Value.ToString());
                    r.AUX = id_modulo;
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

        public RPTA_GENERAL modificarModulo(int idmodulo, int id_modalidad, string nombre, string url, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[6];
            bdParameters[0] = new OracleParameter("P_ID_MODULO_SISTEMA", OracleDbType.Int32) { Value = idmodulo };
            bdParameters[1] = new OracleParameter("P_ID_MODALIDAD_TRANS", OracleDbType.Int32) { Value = id_modalidad };
            bdParameters[2] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[3] = new OracleParameter("P_URL", OracleDbType.Varchar2) { Value = url };
            bdParameters[4] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[5] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = session_usuario };


            try
            {
                using (var bdCmd = new OracleCommand("PKG_MODULOSISTEMA.modificarModulo", conn))
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


        public RPTA_GENERAL modificarModulo_IMG(int idmodulo, string nombre, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_ID_MODULO_SISTEMA", OracleDbType.Int32) { Value = idmodulo };
            bdParameters[1] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[2] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[3] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = session_usuario };


            try
            {
                using (var bdCmd = new OracleCommand("PKG_MODULOSISTEMA.modificarModulo_IMG", conn))
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








