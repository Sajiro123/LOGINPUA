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
    public class MenuPerfilAD
    {
        OracleConnection conn;
        public MenuPerfilAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
            //conn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC"));
        }

        public List<CC_MENU_PERFIL> ListarPermisos(int idperfil)
        {
            List<CC_MENU_PERFIL> resultado = new List<CC_MENU_PERFIL>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_PERFIL", OracleDbType.Varchar2) { Value = idperfil };

            using (var bdCmd = new OracleCommand("PKG_MENU_PERFIL.listarPermisos", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_MENU_PERFIL();

                            if (!DBNull.Value.Equals(bdRd["ID_MENUSUARIOPERFIL"])) { item.ID_MENUSUARIOPERFIL = Convert.ToInt32(bdRd["ID_MENUSUARIOPERFIL"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_PERFIL_MODALIDAD"])) { item.ID_PERFIL_MODALIDAD = Convert.ToInt32(bdRd["ID_PERFIL_MODALIDAD"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_MENU"])) { item.ID_MENU = Convert.ToInt32(bdRd["ID_MENU"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_ESTADO"])) { item.ID_ESTADO = Convert.ToInt32(bdRd["ID_ESTADO"]); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }



        public List<CC_MENU_PERFIL> listarModalidad_Perfiles()
        {
            List<CC_MENU_PERFIL> resultado = new List<CC_MENU_PERFIL>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_MENU_PERFIL.listarPerfil_Modalidad", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_MENU_PERFIL();

                            if (!DBNull.Value.Equals(bdRd["ID_PERFIL_MODALIDAD"])) { item.ID_PERFIL_MODALIDAD = Convert.ToInt32(bdRd["ID_PERFIL_MODALIDAD"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_PERFIL"])) { item.ID_PERFIL = Convert.ToInt32(bdRd["ID_PERFIL"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_MODALIDAD_TRANS"])) { item.ID_MODALIDAD_TRANS = Convert.ToInt32(bdRd["ID_MODALIDAD_TRANS"]); }


                            if (!DBNull.Value.Equals(bdRd["PERFIL"])) { item.PERFIL = bdRd["PERFIL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["MODALIDAD"])) { item.MODALIDAD = bdRd["MODALIDAD"].ToString(); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public List<CC_MENUPERFIL_ACCION> listarAcceso()
        {
            List<CC_MENUPERFIL_ACCION> resultado = new List<CC_MENUPERFIL_ACCION>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_MENU_PERFIL.listaraccesos", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_MENUPERFIL_ACCION();


                            if (!DBNull.Value.Equals(bdRd["ID_ACCION"])) { item.ID_ACCION = Convert.ToInt32(bdRd["ID_ACCION"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_ESTADO"])) { item.ID_ESTADO = Convert.ToInt32(bdRd["ID_ESTADO"]); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public List<CC_MENUPERFIL_ACCION> accesoslist_X_idmenuperfil(int idmenuperfil)
        {
            List<CC_MENUPERFIL_ACCION> resultado = new List<CC_MENUPERFIL_ACCION>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("p_idmenuperfil", OracleDbType.Int32) { Value = idmenuperfil };

            using (var bdCmd = new OracleCommand("PKG_MENU_PERFIL.accesoslist_X_idmenuperfil", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_MENUPERFIL_ACCION();


                            if (!DBNull.Value.Equals(bdRd["ID_MENU"])) { item.ID_MENU = Convert.ToInt32(bdRd["ID_MENU"]); }
                            if (!DBNull.Value.Equals(bdRd["IDMENU_ACCION"])) { item.IDMENU_ACCION = Convert.ToInt32(bdRd["IDMENU_ACCION"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_ACCION"])) { item.ID_ACCION = Convert.ToInt32(bdRd["ID_ACCION"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ICON_ACCION"])) { item.ICON_ACCION = bdRd["ICON_ACCION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_MENUSUARIOPERFIL"])) { item.ID_MENUSUARIOPERFIL = Convert.ToInt32(bdRd["ID_MENUSUARIOPERFIL"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_ESTADO"])) { item.ID_ESTADO = Convert.ToInt32(bdRd["ID_ESTADO"]); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public List<CC_MENUPERFIL_ACCION> accesoslist_menu_idperfil(int idperfil)
        {
            List<CC_MENUPERFIL_ACCION> resultado = new List<CC_MENUPERFIL_ACCION>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_IDPERFILMODALIDAD", OracleDbType.Int32) { Value = idperfil };

            using (var bdCmd = new OracleCommand("PKG_MENU_PERFIL.accesoslist_menu", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_MENUPERFIL_ACCION();


                            if (!DBNull.Value.Equals(bdRd["ID_MENU"])) { item.ID_MENU = Convert.ToInt32(bdRd["ID_MENU"]); }
                            if (!DBNull.Value.Equals(bdRd["IDMENU_ACCION"])) { item.IDMENU_ACCION = Convert.ToInt32(bdRd["IDMENU_ACCION"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_ACCION"])) { item.ID_ACCION = Convert.ToInt32(bdRd["ID_ACCION"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ICON_ACCION"])) { item.ICON_ACCION = bdRd["ICON_ACCION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_MENUSUARIOPERFIL"])) { item.ID_MENUSUARIOPERFIL = Convert.ToInt32(bdRd["ID_MENUSUARIOPERFIL"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_ESTADO"])) { item.ID_ESTADO = Convert.ToInt32(bdRd["ID_ESTADO"]); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public List<CC_MENU_PERFIL> ListarMenu_x_ID(int idmenu, int idperfil)
        {
            List<CC_MENU_PERFIL> resultado = new List<CC_MENU_PERFIL>();

            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_MENU", OracleDbType.Int32) { Value = idmenu };
            bdParameters[2] = new OracleParameter("P_ID_PERFIL", OracleDbType.Int32) { Value = idperfil };

            using (var bdCmd = new OracleCommand("PKG_MENU_PERFIL.listarMenu_x_id", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_MENU_PERFIL();

                            if (!DBNull.Value.Equals(bdRd["ID_MENU"])) { item.ID_MENU = Convert.ToInt32(bdRd["ID_MENU"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_MENUSUARIOPERFIL"])) { item.ID_MENUSUARIOPERFIL = Convert.ToInt32(bdRd["ID_MENUSUARIOPERFIL"]); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public RPTA_GENERAL Agregar_AccionMenu(int id_menuperfil, int id_accion, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_ID_MENUPERFIL", OracleDbType.Int32) { Value = id_menuperfil };
            bdParameters[1] = new OracleParameter("P_ID_ACCION", OracleDbType.Int32) { Value = id_accion };
            bdParameters[2] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[3] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_MENU_PERFIL.agregar_accionmenu", conn))
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

        public RPTA_GENERAL AgregarPermiso_Perfil(int id_perfil, int id_menu, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_ID_PERFIL", OracleDbType.Int32) { Value = id_perfil };
            bdParameters[1] = new OracleParameter("P_ID_MENU", OracleDbType.Int32) { Value = id_menu };
            bdParameters[2] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[3] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_MENU_PERFIL.registrarPermiso", conn))
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



        public RPTA_GENERAL Desactivar_Permisos(int idmenuperfil, int idestado, int id_perfil, string usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[5];
            bdParameters[0] = new OracleParameter("P_ID_MENUSUARIOPERFIL", OracleDbType.Int32) { Value = idmenuperfil };
            bdParameters[1] = new OracleParameter("P_ID_PERFIL", OracleDbType.Int32) { Value = id_perfil };
            bdParameters[2] = new OracleParameter("P_ESTADO", OracleDbType.Varchar2) { Value = idestado };
            bdParameters[3] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[4] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_MENU_PERFIL.Activar_Desactivar_Menu", conn))
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

        public RPTA_GENERAL Desactivar_Acciones(int idmenuperfil)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_ID_MENUSUARIOPERFIL", OracleDbType.Int32) { Value = idmenuperfil };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_MENU_PERFIL.desactivar_acciones", conn))
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

    }
}








