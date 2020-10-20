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
    public class Usuario_PersonaAD
    {
         OracleConnection conn;
        public Usuario_PersonaAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }


        public List<CC_PERFIL_USUARIO> ListarPerfil_modalidad()
        {
            List<CC_PERFIL_USUARIO> resultado = new List<CC_PERFIL_USUARIO>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_TBUSUARIO.listarPerfil", conn))
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

                            if (!DBNull.Value.Equals(bdRd["ID_PERFIL_MODALIDAD"])) { item.ID_PERFIL_MODALIDAD = Convert.ToInt32(bdRd["ID_PERFIL_MODALIDAD"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_PERFIL"])) { item.ID_PERFIL = Convert.ToInt32(bdRd["ID_PERFIL"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_MODALIDAD_TRANS"])) { item.ID_MODALIDAD_TRANS = Convert.ToInt32(bdRd["ID_MODALIDAD_TRANS"]); }
                            if (!DBNull.Value.Equals(bdRd["MODALIDAD"])) { item.MODALIDAD = bdRd["MODALIDAD"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PERFIL"])) { item.PERFIL = bdRd["PERFIL"].ToString(); }


                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }
        public List<CC_PERFIL_USUARIO> ListarUsuario_x_ID(int id)
        {
            List<CC_PERFIL_USUARIO> resultado = new List<CC_PERFIL_USUARIO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32) { Value = id }; ;

            using (var bdCmd = new OracleCommand("PKG_TBUSUARIO.listarUsuario_x_ID", conn))
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

                            if (!DBNull.Value.Equals(bdRd["ID_USUARIO_PERFIL"])) { item.ID_USUARIO_PERFIL = bdRd["ID_USUARIO_PERFIL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_USUARIO"])) { item.ID_USUARIO = Convert.ToInt32(bdRd["ID_USUARIO"]); }
                            if (!DBNull.Value.Equals(bdRd["USUARIO"])) { item.USUARIO = bdRd["USUARIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CLAVE"])) { item.CLAVE = bdRd["CLAVE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["IDMODALIDAD"])) { item.IDMODALIDAD = bdRd["IDMODALIDAD"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_ESTADO"])) { item.ID_ESTADO = bdRd["ID_ESTADO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PERFILES"])) { item.PERFILES = bdRd["PERFILES"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = (bdRd["USU_REG"]).ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = (bdRd["FECHA_REG"]).ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_PROVEEDORSERV_USUARIO> ListCuentas_x_id(int id_prov)
        {
            List<CC_PROVEEDORSERV_USUARIO> resultado = new List<CC_PROVEEDORSERV_USUARIO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_PROV", OracleDbType.Int32) { Value = id_prov }; ;

            using (var bdCmd = new OracleCommand("PKG_TBUSUARIO.listarCuentas_Provserv_Xid", conn))
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
                            if (!DBNull.Value.Equals(bdRd["USUARIO"])) { item.USUARIO = bdRd["USUARIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CONTRASENA"])) { item.CONTRASENA = bdRd["CONTRASENA"].ToString(); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_PROVEEDORSERV_USUARIO> Consultar_CuentaProvUsu(int id_usuario)
        {
            List<CC_PROVEEDORSERV_USUARIO> resultado = new List<CC_PROVEEDORSERV_USUARIO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32) { Value = id_usuario }; ;

            using (var bdCmd = new OracleCommand("PKG_TBUSUARIO.SP_Consultar_CuentaProvUsu", conn))
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
                            if (!DBNull.Value.Equals(bdRd["IDPROVUSU_CORREDOR"])) { item.IDPROVUSU_CORREDOR = Convert.ToInt32(bdRd["IDPROVUSU_CORREDOR"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_USUARIO"])) { item.ID_USUARIO = Convert.ToInt32(bdRd["ID_USUARIO"]); }
                            if (!DBNull.Value.Equals(bdRd["USUARIO"])) { item.USUARIO = bdRd["USUARIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CONTRASENA"])) { item.CONTRASENA = bdRd["CONTRASENA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_CORREDOR"])) { item.ID_CORREDOR = Convert.ToInt32(bdRd["ID_CORREDOR"]); }
                            if (!DBNull.Value.Equals(bdRd["CORREDOR_NOMBRE"])) { item.CORREDOR_NOMBRE = bdRd["CORREDOR_NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ABREVIATURA"])) { item.ABREVIATURA = bdRd["ABREVIATURA"].ToString(); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }



        public List<CC_PERSONA> ListarPersona()
        {
            List<CC_PERSONA> resultado = new List<CC_PERSONA>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_TBUSUARIO.listarPersona", conn))
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

                            if (!DBNull.Value.Equals(bdRd["ID_PERSONA"])) { item.ID_PERSONA = Convert.ToInt32(bdRd["ID_PERSONA"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRES"])) { item.NOMBRES = bdRd["NOMBRES"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["APEPAT"])) { item.APEPAT = bdRd["APEPAT"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["APEMAT"])) { item.APEMAT = bdRd["APEMAT"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;

        }

        public List<CC_PROVEEDORSERV_USUARIO> Listar_Cuentas_x_Usuario(int id_usuario)
        {
            List<CC_PROVEEDORSERV_USUARIO> resultado = new List<CC_PROVEEDORSERV_USUARIO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32) { Value = id_usuario };

            using (var bdCmd = new OracleCommand("PKG_TBUSUARIO.Listar_Cuentas_x_Usuario", conn))
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
                            if (!DBNull.Value.Equals(bdRd["USUARIO"])) { item.USUARIO = bdRd["USUARIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CONTRASENA"])) { item.CONTRASENA = bdRd["CONTRASENA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_CORREDOR"])) { item.ID_CORREDOR = Convert.ToInt32(bdRd["ID_CORREDOR"]); }
                            if (!DBNull.Value.Equals(bdRd["PROVABREV_CORR"])) { item.PROVABREV_CORR = bdRd["PROVABREV_CORR"].ToString(); }
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



        public RPTA_GENERAL registrarUsuario_Perfil(int id_usuario, int idperfil, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_USUARIO", OracleDbType.Int32) { Value = id_usuario };
            bdParameters[1] = new OracleParameter("P_ID_PERFIL", OracleDbType.Int32) { Value = idperfil }; ;
            bdParameters[2] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[3] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_TBUSUARIO.registrarUsuario_Perfil", conn))
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
        public RPTA_GENERAL AgregarUsuario(int idpersona, string usuario, string contraseña, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            var id_usuario = 0;

            OracleParameter[] bdParameters = new OracleParameter[6];
            bdParameters[0] = new OracleParameter("P_ID_PERSONA", OracleDbType.Int32) { Value = idpersona };
            bdParameters[1] = new OracleParameter("P_USUARIO", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[2] = new OracleParameter("P_CLAVE", OracleDbType.Varchar2) { Value = contraseña };
            bdParameters[3] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[4] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[5] = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_TBUSUARIO.registrarUsuario", conn))
                {
                    bdCmd.Connection.Open();
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();

                    id_usuario = int.Parse(bdCmd.Parameters["P_ID_USUARIO"].Value.ToString());
                    r.AUX = id_usuario;
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

        public RPTA_GENERAL ModificarUsuarios_Perfil(int idusuario, string idperfil, string clave, int estado_usuario, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[6];
            bdParameters[0] = new OracleParameter("P_ID_USUARIO", OracleDbType.Int32) { Value = idusuario };
            bdParameters[1] = new OracleParameter("P_IDPERFIL", OracleDbType.Varchar2) { Value = idperfil };
            bdParameters[2] = new OracleParameter("P_CLAVE", OracleDbType.Varchar2) { Value = clave };
            bdParameters[3] = new OracleParameter("ID_ESTADO_USU", OracleDbType.Int32) { Value = estado_usuario };
            bdParameters[4] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[5] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = session_usuario };


            try
            {
                using (var bdCmd = new OracleCommand("PKG_TBUSUARIO.modificarUsuario_perfil", conn))
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
                using (var bdCmd = new OracleCommand("PKG_TBUSUARIO.anularUsuario", conn))
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
    }
}








