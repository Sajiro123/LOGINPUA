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
    public class RolPersonaAD
    {
         OracleConnection conn;
        public RolPersonaAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public RPTA_GENERAL AgregarRol(string nombre, string usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[1] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[2] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };


            try
            {
                using (var bdCmd = new OracleCommand("PKG_ROL.registrarRol", conn))
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
                r.DES_ESTADO = "Ya existe un perfil con ese nombre";
            }
            return r;
        }


        public List<CC_ROL> ListarRol()
        {
            List<CC_ROL> resultado = new List<CC_ROL>();
            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_ROL.listarRol", conn))
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
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public RPTA_GENERAL AnularRol(int idrol, string session_usuario)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_ID_ROLPERSONA", OracleDbType.Int32) { Value = idrol };
            bdParameters[1] = new OracleParameter("P_USU_ANULA", OracleDbType.Varchar2) { Value = session_usuario };
            bdParameters[2] = new OracleParameter("P_FECHA_ANULA", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_ROL.anularRol", conn))
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

        public RPTA_GENERAL ModificarRol(int idrol, string nombre, string session_usuario)
        {
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[4];

            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");

            bdParameters[0] = new OracleParameter("P_ID_ROLPERSONA", OracleDbType.Int32) { Value = idrol };
            bdParameters[1] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[2] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[3] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = session_usuario };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_ROL.modificarRol", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se modificó correctamente";
                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
            }
            return r;
        }

    }
}








