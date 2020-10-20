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
    public class Menu_UsuarioAD
    {
        OracleConnection conn;
        public Menu_UsuarioAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
            //conn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC"));
        }


        public List<CC_MENU_USUARIO> ListarModulos(int id_modalidad)
        {
            List<CC_MENU_USUARIO> resultado = new List<CC_MENU_USUARIO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_MODALIDAD_TRANS", OracleDbType.Int32) { Value = id_modalidad };

            using (var bdCmd = new OracleCommand("PKG_MENU_USUARIO.listarModulos", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_MENU_USUARIO();

                            if (!DBNull.Value.Equals(bdRd["ID_MODULO_SISTEMA"])) { item.ID_MODULO_SISTEMA = Convert.ToInt32(bdRd["ID_MODULO_SISTEMA"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["URL"])) { item.URL = bdRd["URL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["IMAGEN"])) { item.IMAGEN = bdRd["IMAGEN"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;

        }

        public List<CC_MENU_USUARIO> listarMenuPadre(int id_modalidad)
        {
            List<CC_MENU_USUARIO> resultado = new List<CC_MENU_USUARIO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_MODALIDAD_TRANS", OracleDbType.Int32) { Value = id_modalidad };


            using (var bdCmd = new OracleCommand("PKG_MENU_USUARIO.listarMenuPadre", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_MENU_USUARIO();

                            if (!DBNull.Value.Equals(bdRd["ID_MENU"])) { item.ID_MENU = Convert.ToInt32(bdRd["ID_MENU"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["URL"])) { item.URL = bdRd["URL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ICONO"])) { item.ICONO = bdRd["ICONO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["IS_PADRE"])) { item.IS_PADRE = bdRd["IS_PADRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_MODULO"])) { item.ID_MODULO = Convert.ToInt32(bdRd["ID_MODULO"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_MENU_PADRE"])) { item.ID_MENU_PADRE = Convert.ToInt32(bdRd["ID_MENU_PADRE"]); }
                            if (!DBNull.Value.Equals(bdRd["MODULO"])) { item.MODULO = bdRd["MODULO"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;

        }

        public RPTA_GENERAL EditarMenu(int id, string nombre, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_IDMENU", OracleDbType.Int32) { Value = id };
            bdParameters[1] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[2] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[3] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_MENU_USUARIO.Editar_Menu", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se Modifico correctamente";
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

        public RPTA_GENERAL AnularMenu(int id, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_IDMENU", OracleDbType.Int32) { Value = id };
            bdParameters[1] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[2] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_MENU_USUARIO.Eliminar_Menu", conn))
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

        public List<CC_MENU_USUARIO> listarMODULOS_X_ID(int id, int idmodalidad)
        {
            List<CC_MENU_USUARIO> resultado = new List<CC_MENU_USUARIO>();

            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID", OracleDbType.Int32) { Value = id };
            bdParameters[2] = new OracleParameter("P_ID_MODALIDAD", OracleDbType.Int32) { Value = idmodalidad };

            using (var bdCmd = new OracleCommand("PKG_MENU_USUARIO.listarmodulos_id", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_MENU_USUARIO();

                            if (!DBNull.Value.Equals(bdRd["ID_MENU"])) { item.ID_MENU = Convert.ToInt32(bdRd["ID_MENU"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;

        }

        public RPTA_GENERAL AgregarMenu(string nombre, string url, string icono, int tipomenu, int menupadre, int modulo, int idmodalidad, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[9];

            bdParameters[0] = new OracleParameter("P_ID_MODALIDAD", OracleDbType.Varchar2) { Value = idmodalidad };
            bdParameters[1] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[2] = new OracleParameter("P_URL", OracleDbType.Varchar2) { Value = url };
            bdParameters[3] = new OracleParameter("P_ICONO", OracleDbType.Varchar2) { Value = icono };
            bdParameters[4] = new OracleParameter("P_IS_PADRE", OracleDbType.Int32) { Value = tipomenu };
            bdParameters[5] = new OracleParameter("P_ID_MENU_PADRE", OracleDbType.Int32) { Value = menupadre };
            bdParameters[6] = new OracleParameter("P_ID_MODULO", OracleDbType.Int32) { Value = modulo };
            bdParameters[7] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[8] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_MENU_USUARIO.registrarMenu", conn))
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
        //public RPTA_GENERAL ModificarUsuarios(int idusuario,string clave, int modalidad, string estado, int perfil, string session_usuario)
        //{
        //    DateTime dateTime = DateTime.UtcNow.Date;
        //    string fecha_actual = dateTime.ToString("dd/MM/yyyy");
        //    RPTA_GENERAL r = new RPTA_GENERAL();
        //    OracleParameter[] bdParameters = new OracleParameter[7];
        //    bdParameters[0] = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32) { Value = idusuario };
        //    bdParameters[1] = new OracleParameter("P_ID_MODALIDAD_TRANS", OracleDbType.Int32) { Value = modalidad };
        //    bdParameters[2] = new OracleParameter("P_ID_PERFIL", OracleDbType.Int32) { Value = perfil };
        //    bdParameters[3] = new OracleParameter("P_CLAVE", OracleDbType.Varchar2) { Value = clave };
        //    bdParameters[4] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };
        //    bdParameters[5] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = session_usuario };
        //    bdParameters[6] = new OracleParameter("P_ESTADO", OracleDbType.Varchar2) { Value = estado };


        //    try
        //    {
        //        using (var bdCmd = new OracleCommand("PKG_TBUSUARIO.modificarUsuario", conn))
        //        {
        //            bdCmd.CommandType = CommandType.StoredProcedure;
        //            bdCmd.Parameters.AddRange(bdParameters);
        //            bdCmd.ExecuteNonQuery();
        //            r.COD_ESTADO = 1;
        //            r.DES_ESTADO = "Se modifico correctamente";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        r.AUX = 0;
        //        r.COD_ESTADO = 0;
        //        r.DES_ESTADO = ex.Message;
        //    }
        //    return r;
        //}


        //public RPTA_GENERAL Desactivar_Usuarios(int idusuario, string usuario)
        //{
        //    DateTime dateTime = DateTime.UtcNow.Date;
        //    string fecha_actual = dateTime.ToString("dd/MM/yyyy");
        //    RPTA_GENERAL r = new RPTA_GENERAL();
        //    OracleParameter[] bdParameters = new OracleParameter[3];
        //    bdParameters[0] = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32) { Value = idusuario };
        //    bdParameters[1] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario };
        //    bdParameters[2] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };

        //    try
        //    {
        //        using (var bdCmd = new OracleCommand("PKG_TBUSUARIO.anularUsuario", conn))
        //        {
        //            bdCmd.CommandType = CommandType.StoredProcedure;
        //            bdCmd.Parameters.AddRange(bdParameters);
        //            bdCmd.ExecuteNonQuery();
        //            r.COD_ESTADO = 1;
        //            r.DES_ESTADO = "Se elimino correctamente";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        r.AUX = 0;
        //        r.COD_ESTADO = 0;
        //        r.DES_ESTADO = ex.Message;
        //    }
        //    return r;
        //}
    }
}


