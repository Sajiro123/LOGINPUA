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
    public class ParaderoAD
    {
        OracleConnection conn;
        public ParaderoAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
            //conn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC"));
        }

        public List<CC_PARADERO> getParaderosByIdRuta(int idRuta)
        {
            List<CC_PARADERO> resultado = new List<CC_PARADERO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Double) { Value = idRuta };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_GESTION_SALIDA.getParaderosByIdRuta", conn))
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

                            if (!DBNull.Value.Equals(bdRd["ID_PARADERO"])) { item.ID_PARADERO = Convert.ToInt32(bdRd["ID_PARADERO"]); }
                            if (!DBNull.Value.Equals(bdRd["LADO"])) { item.LADO = bdRd["LADO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NRO_ORDEN"])) { item.NRO_ORDEN = Convert.ToInt32(bdRd["NRO_ORDEN"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = Convert.ToString(bdRd["NOMBRE"]); }
                            if (!DBNull.Value.Equals(bdRd["ETIQUETA_NOMBRE"])) { item.ETIQUETA_NOMBRE = bdRd["ETIQUETA_NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["DISTANCIA_PARCIAL"])) { item.DISTANCIA_PARCIAL = double.Parse(bdRd["DISTANCIA_PARCIAL"].ToString()); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_PARADERO> getParaderosByStrRecorrido(string strCodRecorrido)
        {
            List<CC_PARADERO> resultado = new List<CC_PARADERO>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_STR_RECORRIDO", OracleDbType.Varchar2) { Value = strCodRecorrido };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_PARADERO.listarParaderosByIdRecorrido", conn))
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

                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_PARADERO"])) { item.ID_PARADERO = int.Parse(bdRd["ID_PARADERO"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["ID_RECORRIDO"])) { item.ID_RECORRIDO = int.Parse(bdRd["ID_RECORRIDO"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["ID_TIPO_PARADERO"])) { item.ID_TIPO_PARADERO = int.Parse(bdRd["ID_TIPO_PARADERO"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["ID_VIA"])) { item.ID_VIA = int.Parse(bdRd["ID_VIA"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["NRO_ORDEN"])) { item.NRO_ORDEN = Convert.ToInt32(bdRd["NRO_ORDEN"]); }
                            if (!DBNull.Value.Equals(bdRd["ETIQUETA_NOMBRE"])) { item.ETIQUETA_NOMBRE = Convert.ToString(bdRd["ETIQUETA_NOMBRE"]); }
                            if (!DBNull.Value.Equals(bdRd["TIPO"])) { item.TIPO = bdRd["TIPO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["DISTANCIA_PARCIAL"])) { item.DISTANCIA_PARCIAL = double.Parse(bdRd["DISTANCIA_PARCIAL"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["LATITUD"])) { item.LATITUD = double.Parse(bdRd["LATITUD"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["LONGITUD"])) { item.LONGITUD = double.Parse(bdRd["LONGITUD"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public RPTA_GENERAL registrarParadero(int idRecorrido, int idTipoParadero, int idVia, string nombre, string nombreEtiqueta, double distanciaParcial, double latitud, double longitud, int nroOrden, string usuarioReg) {
            RPTA_GENERAL r = new RPTA_GENERAL();
            var id_paradero = 0;
            OracleParameter[] bdParameters = new OracleParameter[12];
            bdParameters[0] = new OracleParameter("P_ID_RECORRIDO", OracleDbType.Int32) { Value = idRecorrido };
            bdParameters[1] = new OracleParameter("P_ID_TIPO_PARADERO", OracleDbType.Int32) { Value = idTipoParadero };
            bdParameters[2] = new OracleParameter("P_ID_VIA", OracleDbType.Varchar2) { Value = idVia };
            bdParameters[3] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[4] = new OracleParameter("P_ETIQUETA", OracleDbType.Varchar2) { Value = nombreEtiqueta };
            bdParameters[5] = new OracleParameter("P_DIST_PARCIAL", OracleDbType.Double) { Value = distanciaParcial };
            bdParameters[6] = new OracleParameter("P_LATITUD", OracleDbType.Varchar2) { Value = latitud };
            bdParameters[7] = new OracleParameter("P_LONGITUD", OracleDbType.Varchar2) { Value = longitud };
            bdParameters[8] = new OracleParameter("P_NRO_ORDEN", OracleDbType.Int32) { Value = nroOrden };
            bdParameters[9] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuarioReg };
            bdParameters[10] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[11] = new OracleParameter("ID_PARADERO", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PARADERO.registrarParadero", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    id_paradero = int.Parse(bdCmd.Parameters["ID_PARADERO"].Value.ToString());
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                    r.AUX = id_paradero;
                }
            }
            catch (Exception ex)
            {
                r.AUX = 0;
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }

        public RPTA_GENERAL anularParadero(int idParadero, string usuarioReg)
        {
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_ID_PARADERO", OracleDbType.Int32) { Value = idParadero };
            bdParameters[1] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuarioReg };
            bdParameters[2] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PARADERO.anularParadero", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se anuló correctamente";
                }
            }
            catch (Exception ex)
            {
                r.AUX = 0;
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }

        public RPTA_GENERAL modificarParadero(  int idParadero, string nombre, string etiquetaNombre,
                                                int idTipoParadero, double distanciaParcial, 
                                                double latitud, double longitud, int idVia,int numero_orden,
                                                string usuarioModif)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[11];
            bdParameters[0] = new OracleParameter("P_ID_PARADERO", OracleDbType.Int32) { Value = idParadero };
            bdParameters[1] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[2] = new OracleParameter("P_ETIQUETA_NOMBRE", OracleDbType.Varchar2) { Value = etiquetaNombre };
            bdParameters[3] = new OracleParameter("P_ID_TIPO_PARADERO", OracleDbType.Int32) { Value = idTipoParadero };
            bdParameters[4] = new OracleParameter("P_DISTANCIA_PARCIAL", OracleDbType.Decimal) { Value = distanciaParcial };
            bdParameters[5] = new OracleParameter("P_LATITUD", OracleDbType.Decimal) { Value = latitud };
            bdParameters[6] = new OracleParameter("P_LONGITUD", OracleDbType.Decimal) { Value = longitud };
            bdParameters[7] = new OracleParameter("P_ID_VIA", OracleDbType.Int32) { Value = idVia };
            bdParameters[8] = new OracleParameter("P_NRO_ORDEN", OracleDbType.Int32) { Value = numero_orden };            
            bdParameters[9] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = usuarioModif };
            bdParameters[10] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PARADERO.modificarParadero", conn))
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
                r.AUX = 0;
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }
    }
}
