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
    public class Proveedor_ServAD
    {
        OracleConnection conn;
        public Proveedor_ServAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
         }

        public RPTA_GENERAL Update_UsuarioProveedor(int idusuario_provserv, int idusuario, string usuario_session)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_ID_PROV_USUARIO", OracleDbType.Int32) { Value = idusuario_provserv };
            bdParameters[1] = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32) { Value = idusuario };
            bdParameters[2] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario_session };
            bdParameters[3] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROVEEDOR_SERV.registrarUsuario_Proveedor", conn))
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

        public RPTA_GENERAL Actualizar_ContraseñaAnti(int idprov_usu)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_ID_PROV_USUARIO", OracleDbType.Int32) { Value = idprov_usu };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROVEEDOR_SERV.Actualizar_Contraseña", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se Actualizo Correctamente";
                }
            }
            catch (Exception ex)
            {
                if (ex.HelpLink == "-2147467259")
                {
                    r.AUX = 0;
                    r.DES_ESTADO = "Asignar un Usuario";
                    r.COD_ESTADO = 0;
                }
                else
                {
                    r.AUX = 0;
                    r.COD_ESTADO = 0;
                    r.DES_ESTADO = ex.Message;
                }
            }
            return r;
        }

        public RPTA_GENERAL Desactivar_Cuenta_Usuario(int idusuario, string usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_ID_PROV_USUARIO", OracleDbType.Int32) { Value = idusuario };
            bdParameters[1] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[2] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROVEEDOR_SERV.anular_Usuario_Corredores", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se Anulo el Usuario y Corredores correctamente";
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

        public RPTA_GENERAL Desactivar_Usuarios(int idusuario, string usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32) { Value = idusuario };
            bdParameters[1] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[2] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROVEEDOR_SERV.anularProvServ_Usuario", conn))
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

        public List<CC_PROVUSU_CORREDOR> ListCorredor_X_Usuprov(int id_prov_corredor_usu)
        {
            List<CC_PROVUSU_CORREDOR> resultado = new List<CC_PROVUSU_CORREDOR>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_PROV_USUARIO", OracleDbType.Int32) { Value = id_prov_corredor_usu };


            using (var bdCmd = new OracleCommand("PKG_PROVEEDOR_SERV.ListCorredores_ProvUsu", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                 using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_PROVUSU_CORREDOR();

                            if (!DBNull.Value.Equals(bdRd["IDPROVUSU_CORREDOR"])) { item.IDPROVUSU_CORREDOR = Convert.ToInt32(bdRd["IDPROVUSU_CORREDOR"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_PROV_USUARIO"])) { item.ID_PROV_USUARIO = Convert.ToInt32(bdRd["ID_PROV_USUARIO"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_CORREDOR"])) { item.ID_CORREDOR = Convert.ToInt32(bdRd["ID_CORREDOR"]); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_CORREDOR> ListCorredores(int id_prov_serv)
        {
            List<CC_CORREDOR> resultado = new List<CC_CORREDOR>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32) { Value = id_prov_serv };

            using (var bdCmd = new OracleCommand("PKG_PROVEEDOR_SERV.ListCorredores", conn))
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
                            if (!DBNull.Value.Equals(bdRd["ID_PROV_SERV"])) { item.ID_PROV_SERV = Convert.ToInt32(bdRd["ID_PROV_SERV"]); }
                            if (!DBNull.Value.Equals(bdRd["CORREDOR_NOMBRE"])) { item.CORREDOR_NOMBRE = bdRd["CORREDOR_NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ABREVIATURA"])) { item.ABREVIATURA = bdRd["ABREVIATURA"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }
        public List<CC_PROVEEDORSERV> ListarProveedorServ()
        {
            List<CC_PROVEEDORSERV> resultado = new List<CC_PROVEEDORSERV>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_PROVEEDOR_SERV.listarProveedorServ", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_PROVEEDORSERV();

                            if (!DBNull.Value.Equals(bdRd["ID_PROV_SERV"])) { item.ID_PROV_SERV = Convert.ToInt32(bdRd["ID_PROV_SERV"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["URL_SERV"])) { item.URL_SERV = bdRd["URL_SERV"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }
        public List<CC_USUARIO_PERSONA> ListarUsuarios()
        {
            List<CC_USUARIO_PERSONA> resultado = new List<CC_USUARIO_PERSONA>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_TBUSUARIO.listarUsuario", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_USUARIO_PERSONA();

                            if (!DBNull.Value.Equals(bdRd["ID_USUARIO"])) { item.ID_USUARIO = Convert.ToInt32(bdRd["ID_USUARIO"]); }
                            if (!DBNull.Value.Equals(bdRd["USUARIO"])) { item.USUARIO = bdRd["USUARIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CLAVE"])) { item.CLAVE = bdRd["CLAVE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRES"])) { item.NOMBRE = bdRd["NOMBRES"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["APEPAT"])) { item.APEPAT = bdRd["APEPAT"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["APEMAT"])) { item.APEMAT = bdRd["APEMAT"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_USUARIO"])) { item.ID_USUARIO = Convert.ToInt32(bdRd["ID_USUARIO"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_ESTADO"])) { item.ID_ESTADO = (bdRd["ID_ESTADO"]).ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;

        }
        public List<CC_PROVEEDORSERV_USUARIO> ListarProvServ_Usuario()
        {
            List<CC_PROVEEDORSERV_USUARIO> resultado = new List<CC_PROVEEDORSERV_USUARIO>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_PROVEEDOR_SERV.listarProvServ_Usuario", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_PROVEEDORSERV_USUARIO();

                            if (!DBNull.Value.Equals(bdRd["ID_PROV_USUARIO"])) { item.ID_PROV_USUARIO = Convert.ToInt32(bdRd["ID_PROV_USUARIO"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_PROV_SERV"])) { item.ID_PROV_SERV = Convert.ToInt32(bdRd["ID_PROV_SERV"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE_PROVE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USUARIO_PROV"])) { item.USUARIO_PROV = bdRd["USUARIO_PROV"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CONTRASENA"])) { item.CONTRASENA = bdRd["CONTRASENA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_USUARIO"])) { item.ID_USUARIO = Convert.ToInt32(bdRd["ID_USUARIO"]); }
                            if (!DBNull.Value.Equals(bdRd["USUARIO"])) { item.USUARIO = bdRd["USUARIO"].ToString(); }

                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public CC_USUARIO_PERSONA Buscar_x_idusuario(int idusuario)
        {
            var item = new CC_USUARIO_PERSONA();


            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32) { Value = idusuario };

            using (var bdCmd = new OracleCommand("PKG_PROVEEDOR_SERV.listarUsuario_x_ID", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            if (!DBNull.Value.Equals(bdRd["CLAVE"])) { item.CLAVE = (bdRd["CLAVE"]).ToString(); }
                        }
                    }
                }
            }
            return item;
        }

        public RPTA_GENERAL AgregarUsuario_Corredor(int idcorredores, int idusuario, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_ID_PROV_USUARIO", OracleDbType.Int32) { Value = idusuario };
            bdParameters[1] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = idcorredores };
            bdParameters[2] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[3] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROVEEDOR_SERV.registrarUsuario_Corredor", conn))
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


        public RPTA_GENERAL Rpta_Cuenta_Existente(int id_prov, string cuenta)
        {
            var item = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_PROV_SERV", OracleDbType.Int32) { Value = id_prov };
            bdParameters[2] = new OracleParameter("P_USUARIO", OracleDbType.Varchar2) { Value = cuenta };


            using (var bdCmd = new OracleCommand("PKG_PROVEEDOR_SERV.Rpta_Cuenta_Existente", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            item = new RPTA_GENERAL();
                            if (!DBNull.Value.Equals(bdRd["AUX"])) { item.AUX = Convert.ToInt32(bdRd["AUX"]); }
                            return item;
                        }
                    }
                }
            }
            return item;
        }

        public RPTA_GENERAL Rpta_Usuario_Existente(int id_provusu, int idusuario, int id_prov_serv)
        {
            var item = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = id_provusu };
            bdParameters[2] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = idusuario };
            bdParameters[3] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = id_prov_serv };


            using (var bdCmd = new OracleCommand("PKG_PROVEEDOR_SERV.Rpta_Usuario_Existente", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            item = new RPTA_GENERAL();

                            if (!DBNull.Value.Equals(bdRd["AUX"])) { item.AUX = Convert.ToInt32(bdRd["AUX"]); }
                            return item;
                        }
                    }
                }
            }
            return item;
        }


        public RPTA_GENERAL Actualizar_Usuario_Prov(int id_provusu, int idusuario, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_ID_PROV_USUARIO", OracleDbType.Int32) { Value = id_provusu };
            bdParameters[1] = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32) { Value = idusuario };
            bdParameters[2] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[3] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROVEEDOR_SERV.Actualizar_Prov_Usuario", conn))
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


        public RPTA_GENERAL AgregarUsuario_Proveedor(int idprov, string usuario, string contraseña, string usuario_session)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[5];
            bdParameters[0] = new OracleParameter("P_ID_PROV_SERV", OracleDbType.Int32) { Value = idprov };
            bdParameters[1] = new OracleParameter("P_USUARIO", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[2] = new OracleParameter("P_CONTRASENA", OracleDbType.Varchar2) { Value = contraseña };
            bdParameters[3] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario_session };
            bdParameters[4] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };



            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROVEEDOR_SERV.registrarUsuario_Proveedor", conn))
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

        public RPTA_GENERAL AgregarUsuario_Proveedor_Usuario(int idprov, int idusuario, string usuario, string contraseña, string usuario_session)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            var p_id_prov_serv = 0;

            OracleParameter[] bdParameters = new OracleParameter[7];
            bdParameters[0] = new OracleParameter("P_ID_PROV_SERV", OracleDbType.Int32) { Value = idprov };
            bdParameters[1] = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32) { Value = idusuario };
            bdParameters[2] = new OracleParameter("P_USUARIO", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[3] = new OracleParameter("P_CONTRASENA", OracleDbType.Varchar2) { Value = contraseña };
            bdParameters[4] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario_session };
            bdParameters[5] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[6] = new OracleParameter("P_ID_PROV_SERV_", OracleDbType.Int32, direction: ParameterDirection.Output);



            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROVEEDOR_SERV.registrarUsuario_Prov_Cuenta", conn))
                {

                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();

                    p_id_prov_serv = int.Parse(bdCmd.Parameters["P_ID_PROV_SERV_"].Value.ToString());
                    r.AUX = p_id_prov_serv;

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








