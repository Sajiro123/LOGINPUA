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
    public class TipoServicioAD
    {
        OracleConnection conn;
        public TipoServicioAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public List<CC_RUTA_TIPO_SERVICIO> getRutaTipoDeServicioOper(int idRuta)
        {
            List<CC_RUTA_TIPO_SERVICIO> resultado = new List<CC_RUTA_TIPO_SERVICIO>();
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idRuta };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_RUTA_TIPO_SRV.getRutaTipoDeServicioOper", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_RUTA_TIPO_SERVICIO();
                            if (!DBNull.Value.Equals(bdRd["ID_RUTA_TIPO_SERVICIO"])) { item.ID_RUTA_TIPO_SERVICIO = Convert.ToInt32(bdRd["ID_RUTA_TIPO_SERVICIO"]); }
                            if (!DBNull.Value.Equals(bdRd["TIPO_SERVICIO"])) { item.TIPO_SERVICIO = bdRd["TIPO_SERVICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_TIPO_SERVICIO"])) { item.ID_TIPO_SERVICIO = Convert.ToInt32(bdRd["ID_TIPO_SERVICIO"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_TIPOSERVICIO_OPER"])) { item.ID_TIPOSERVICIO_OPER = Convert.ToInt32(bdRd["ID_TIPOSERVICIO_OPER"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_RUTA"])) { item.ID_RUTA = Convert.ToInt32(bdRd["ID_RUTA"]); }
                            if (!DBNull.Value.Equals(bdRd["TIPO_OPERACIONAL"])) { item.TIPO_OPERACIONAL = bdRd["TIPO_OPERACIONAL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["COLOR"])) { item.COLOR = bdRd["COLOR"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["LADOS"])) { item.LADOS = bdRd["LADOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["LADOS_TSERV"])) { item.LADOS_TSERV = bdRd["LADOS_TSERV"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_PARADERO> getParaderoByIdRecorridTServ(int idRecorridoTipoServicio)
        {
            List<CC_PARADERO> resultado = new List<CC_PARADERO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_IDRECORRIDOTIPOSERVICIO", OracleDbType.Int32) { Value = idRecorridoTipoServicio };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_RUTA_TIPO_SRV.mostrarParaderosPorRuta", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_PARADERO();
                            if (!DBNull.Value.Equals(bdRd["ID_PARADERO_TIPOSERVICIO"])) { item.ID_PARADERO_TIPOSERVICIO = Convert.ToInt32(bdRd["ID_PARADERO_TIPOSERVICIO"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_PARADERO"])) { item.ID_PARADERO = Convert.ToInt32(bdRd["ID_PARADERO"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_ESTADO"])) { item.ID_ESTADO = Convert.ToInt32(bdRd["ID_ESTADO"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["LATITUD"])) { item.LATITUD = Double.Parse(bdRd["LATITUD"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["LONGITUD"])) { item.LONGITUD = Double.Parse(bdRd["LONGITUD"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["DISTANCIA_PARCIAL"])) { item.DISTANCIA_PARCIAL = Convert.ToDouble(bdRd["DISTANCIA_PARCIAL"]); }
                            if (!DBNull.Value.Equals(bdRd["ETIQUETA_NOMBRE"])) { item.ETIQUETA_NOMBRE = bdRd["ETIQUETA_NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["SENTIDO"])) { item.SENTIDO = bdRd["SENTIDO"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_RUTA_TIPO_SERVICIO> getTiposServicio()
        {
            List<CC_RUTA_TIPO_SERVICIO> resultado = new List<CC_RUTA_TIPO_SERVICIO>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_RUTA_TIPO_SRV.getTipoDeServicioRuta", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_RUTA_TIPO_SERVICIO();

                            if (!DBNull.Value.Equals(bdRd["ID_TIPO_SERVICIO"])) { item.ID_TIPO_SERVICIO = Convert.ToInt32(bdRd["ID_TIPO_SERVICIO"]); }
                            if (!DBNull.Value.Equals(bdRd["TIPO_SERVICIO"])) { item.TIPO_SERVICIO = bdRd["TIPO_SERVICIO"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_RUTA_TIPO_SERVICIO> getTiposServicioOperacional()
        {
            List<CC_RUTA_TIPO_SERVICIO> resultado = new List<CC_RUTA_TIPO_SERVICIO>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_RUTA_TIPO_SRV.getTipoDeServicioOper", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_RUTA_TIPO_SERVICIO();

                            if (!DBNull.Value.Equals(bdRd["ID_TIPOSERVICIO_OPER"])) { item.ID_TIPOSERVICIO_OPER = Convert.ToInt32(bdRd["ID_TIPOSERVICIO_OPER"]); }
                            if (!DBNull.Value.Equals(bdRd["TIPO_OPERACIONAL"])) { item.TIPO_SERVICIO = bdRd["TIPO_OPERACIONAL"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public RPTA_GENERAL registrarRutaTipoServicio(int idRuta, int idTipoServicio, int idTipoServicioOper, string nombre, string color, string usuarioRegistra)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            var id_ruta_tipoServicio = 0;
            OracleParameter[] bdParameters = new OracleParameter[8];
            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idRuta };
            bdParameters[1] = new OracleParameter("P_ID_TIPO_SERVICIO", OracleDbType.Int32) { Value = idTipoServicio };
            bdParameters[2] = new OracleParameter("P_ID_TSERVICIO_OPER", OracleDbType.Int32) { Value = idTipoServicioOper };
            bdParameters[3] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[4] = new OracleParameter("P_COLOR", OracleDbType.Varchar2) { Value = color };
            bdParameters[5] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuarioRegistra };
            bdParameters[6] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[7] = new OracleParameter("P_ID_RUTA_TSERVICIO", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_RUTA_TIPO_SRV.guardarRutaTipoDeServicio", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    id_ruta_tipoServicio = int.Parse(bdCmd.Parameters["P_ID_RUTA_TSERVICIO"].Value.ToString());
                    r.AUX = id_ruta_tipoServicio;
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }

        public RPTA_GENERAL editarRutaTservic(int idRutaTipoServ, int idTipoServicio, int idTipoServicioOper, string nombre,string color,string usuarioModifica)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[7];

            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");

            bdParameters[0] = new OracleParameter("P_ID_RUTA_TIPO_SERVICIO", OracleDbType.Int32) { Value = idRutaTipoServ };
            bdParameters[1] = new OracleParameter("P_ID_TIPO_SERVICIO", OracleDbType.Int32) { Value = idTipoServicio };
            bdParameters[2] = new OracleParameter("P_ID_TIPOSERVICIO_OPER", OracleDbType.Int32) { Value = idTipoServicioOper };
            bdParameters[3] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[4] = new OracleParameter("P_COLOR", OracleDbType.Varchar2) { Value = color };
            bdParameters[5] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = usuarioModifica };
            bdParameters[6] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };



            try
            {
                using (var bdCmd = new OracleCommand("PKG_RUTA_TIPO_SRV.SP_EDITAR_RUTA_T_SERVIC", conn))
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

        public RPTA_GENERAL anularRutaTipoServicio(int idRutaServicioOperacional, string usu_anula)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            var cantidadRegistros = 0;
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_ID_RUTA_TSERVICIO", OracleDbType.Int32) { Value = idRutaServicioOperacional };
            bdParameters[1] = new OracleParameter("P_USU_ANULA", OracleDbType.Varchar2) { Value = usu_anula };
            bdParameters[2] = new OracleParameter("P_FECHA_ANULA", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[3] = new OracleParameter("P_CANTIDAD_REGISTROS", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_RUTA_TIPO_SRV.anularRutaTipoDeServicio", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    cantidadRegistros = int.Parse(bdCmd.Parameters["P_CANTIDAD_REGISTROS"].Value.ToString());
                    r.AUX = cantidadRegistros;
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }

        public RPTA_GENERAL registrarRutaServicioOperacional(int idRutaServicioOperacional, string usu_anula)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            var cantidadRegistros = 0;
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_ID_RUTA_TSERVICIO", OracleDbType.Int32) { Value = idRutaServicioOperacional };
            bdParameters[1] = new OracleParameter("P_USU_ANULA", OracleDbType.Varchar2) { Value = usu_anula };
            bdParameters[2] = new OracleParameter("P_FECHA_ANULA", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[3] = new OracleParameter("P_CANTIDAD_REGISTROS", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_RUTA_TIPO_SRV.anularRutaTipoDeServicio", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    cantidadRegistros = int.Parse(bdCmd.Parameters["P_CANTIDAD_REGISTROS"].Value.ToString());
                    r.AUX = cantidadRegistros;
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }

        public RPTA_GENERAL actualizarParaderoTservic(int idParaderoTipoServicio, int idEstado, string usu_modif)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_ID_PARADERO_TIPOSERVICIO", OracleDbType.Int32) { Value = idParaderoTipoServicio };
            bdParameters[1] = new OracleParameter("P_ID_ESTADO", OracleDbType.Int32) { Value = idEstado };
            bdParameters[2] = new OracleParameter("P_USU_MOD", OracleDbType.Varchar2) { Value = usu_modif };
            bdParameters[3] = new OracleParameter("P_FECHA_MOD", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            //
            try
            {
                using (var bdCmd = new OracleCommand("PKG_RUTA_TIPO_SRV.actualizarParaderoTservic", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se actualizó correctamente";
                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }

        public RPTA_GENERAL registraRecorridoServicioOperacional(int idRutaTipoServicio, string lado, string sentido, string usuarioReg)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            var idRecorridoTipoServicio = 0;
            OracleParameter[] bdParameters = new OracleParameter[7];

            bdParameters[0] = new OracleParameter("P_ID_RUTA_TIPO_SERVICIO", OracleDbType.Int32) { Value = idRutaTipoServicio };
            bdParameters[1] = new OracleParameter("P_LADO", OracleDbType.Varchar2) { Value = lado };
            bdParameters[2] = new OracleParameter("P_SENTIDO", OracleDbType.Varchar2) { Value = sentido };
            bdParameters[3] = new OracleParameter("P_ID_RECORRIDO", OracleDbType.Int32, direction: ParameterDirection.Output);
            bdParameters[4] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuarioReg };
            bdParameters[5] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[6] = new OracleParameter("P_IDRECORRIDOTIPOSERVICIO", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_RUTA_TIPO_SRV.regRecorridoDeROperacional", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    idRecorridoTipoServicio = int.Parse(bdCmd.Parameters["P_IDRECORRIDOTIPOSERVICIO"].Value.ToString());
                    r.AUX = idRecorridoTipoServicio;
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }
    }
}
