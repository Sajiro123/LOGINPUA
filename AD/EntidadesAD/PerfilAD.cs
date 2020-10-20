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
    public class PerfilAD
    {
        OracleConnection conn;
        public PerfilAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }
        public List<CC_PERFIL_USUARIO> ListarPerfil(int idmodalidad)
        {
            List<CC_PERFIL_USUARIO> resultado = new List<CC_PERFIL_USUARIO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_MODALIDAD", OracleDbType.Varchar2) { Value = idmodalidad };

            using (var bdCmd = new OracleCommand("PKG_PERFIL.listarPerfil", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_PERFIL_USUARIO();

                            if (!DBNull.Value.Equals(bdRd["ID_PERFIL"])) { item.ID_PERFIL = Convert.ToInt32(bdRd["ID_PERFIL"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["MODALIDAD"])) { item.MODALIDAD = bdRd["MODALIDAD"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_MODALIDAD_TRANS"])) { item.ID_MODALIDAD_TRANS = Convert.ToInt32(bdRd["ID_MODALIDAD_TRANS"]); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
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

        public RPTA_GENERAL AgregarPerfil_modalidad(int id_perfil, int idmodalidad, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_ID_PERFIL", OracleDbType.Int32) { Value = id_perfil };
            bdParameters[1] = new OracleParameter("P_MODALIDAD", OracleDbType.Int32) { Value = idmodalidad };
            bdParameters[2] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[3] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = session_usuario };


            try
            {
                using (var bdCmd = new OracleCommand("PKG_PERFIL.registrarPerfil_Modalidad", conn))
                {
                    bdCmd.Connection.Open();
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

        public RPTA_GENERAL AgregarPerfil(string nombre, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            int id_perfil = 0;
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[1] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[2] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[3] = new OracleParameter("P_ID_PERFIL", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PERFIL.registrarPerfil", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    id_perfil = int.Parse(bdCmd.Parameters["P_ID_PERFIL"].Value.ToString());
                    r.AUX = id_perfil;
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                }
            }
            catch (Exception ex)
            {
                r.AUX = 0;
                r.COD_ESTADO = 0;
                r.DES_ESTADO = "Ya existe un perfil con ese nombre";
            }
            return r;
        }

        public RPTA_GENERAL ModificarPerfil(int idmodalidad, int id_modalidad_perfil, string nombre_perfil, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[5];
            bdParameters[0] = new OracleParameter("P_ID_PERFIL_MODALIDAD", OracleDbType.Int32) { Value = id_modalidad_perfil };
            bdParameters[1] = new OracleParameter("P_ID_MODALIDAD_TRANS", OracleDbType.Int32) { Value = idmodalidad };
            bdParameters[2] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre_perfil };
            bdParameters[3] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[4] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = session_usuario };


            try
            {
                using (var bdCmd = new OracleCommand("PKG_PERFIL.modificarPerfil", conn))
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


        public CC_PERFIL_USUARIO Validar_Perfil(int idperfil)
        {
            var item = new CC_PERFIL_USUARIO();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_PERFIL", OracleDbType.Int32) { Value = idperfil };

            using (var bdCmd = new OracleCommand("PKG_PERFIL.consultarPerfil", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {

                            if (!DBNull.Value.Equals(bdRd["ID_PERFIL"])) { item.ID_PERFIL = Convert.ToInt32(bdRd["ID_PERFIL"]); }
                         }
                    }
                }
            }
            return item;
        }


        public RPTA_GENERAL AnularPerfil(int idperfil, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_ID_PERFIL", OracleDbType.Int32) { Value = idperfil };
            bdParameters[1] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[2] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PERFIL.anularPerfil", conn))
                {
                    bdCmd.Connection.Open();
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
    }
}








