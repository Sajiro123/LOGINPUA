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
    public class RutasAD
    {
        private readonly OracleConnection conn;
        public RutasAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public List<CC_RUTA> listarRutasByIdCorredor(int idCorredor)
        {
            List<CC_RUTA> resultado = new List<CC_RUTA>();
            OracleParameter[] bdParameters = new OracleParameter[2];

            bdParameters[0] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = idCorredor };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_RUTA.listarRuta", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_RUTA();
                            if (!DBNull.Value.Equals(bdRd["ID_RUTA"])) { item.ID_RUTA = Int32.Parse(bdRd["ID_RUTA"].ToString()); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CORREDOR_NOMBRE"])) { item.CORREDOR_NOMBRE = bdRd["CORREDOR_NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NRO_RUTA"])) { item.NRO_RUTA = bdRd["NRO_RUTA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["DISTRITOS"])) { item.DISTRITOS = bdRd["DISTRITOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["SENTIDOS"])) { item.SENTIDOS = bdRd["SENTIDOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public RPTA_GENERAL registrarRuta(int idCorredor, string nombre, string nroRuta, string distritos, string usuarioRegistra)
        {
            RPTA_GENERAL r = new RPTA_GENERAL();
            var id_ruta = 0;
            OracleParameter[] bdParameters = new OracleParameter[7];
            bdParameters[0] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = idCorredor };
            bdParameters[1] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[2] = new OracleParameter("P_NRO_RUTA", OracleDbType.Varchar2) { Value = nroRuta };
            bdParameters[3] = new OracleParameter("P_DISTRITOS", OracleDbType.Varchar2) { Value = distritos };
            bdParameters[4] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuarioRegistra };
            bdParameters[5] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[6] = new OracleParameter("ID_RUTA", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_RUTA.registrarRuta", conn))
                {
                   bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    id_ruta = int.Parse(bdCmd.Parameters["ID_RUTA"].Value.ToString());
                    r.AUX = id_ruta;
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

        public RPTA_GENERAL registrarRecorrido(int idRuta, string sentido, string lado, string usuarioRegistra)
        {
            RPTA_GENERAL r = new RPTA_GENERAL();
            var id_recorrido = 0;
            OracleParameter[] bdParameters = new OracleParameter[6];
            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idRuta };
            bdParameters[1] = new OracleParameter("P_SENTIDO", OracleDbType.Varchar2) { Value = sentido };
            bdParameters[2] = new OracleParameter("P_LADO", OracleDbType.Varchar2) { Value = lado };
            bdParameters[3] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuarioRegistra };
            bdParameters[4] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[5] = new OracleParameter("P_ID_RECORRIDO", OracleDbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var bdCmd = new OracleCommand("PKG_RUTA.registrarRecorrido", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    id_recorrido = int.Parse(bdCmd.Parameters["P_ID_RECORRIDO"].Value.ToString());
                    r.AUX = id_recorrido;
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


        public RPTA_GENERAL editar_Ruta(int idRuta, string nroRuta, string nombre, string distritos, string usuarioModifica)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[6];
            //bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");

            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idRuta };
            bdParameters[1] = new OracleParameter("P_NRO_RUTA", OracleDbType.Varchar2) { Value = nroRuta };
            bdParameters[2] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
            bdParameters[3] = new OracleParameter("P_DISTRITOS", OracleDbType.Varchar2) { Value = distritos };
            bdParameters[4] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = usuarioModifica };
            bdParameters[5] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };



            try
            {
                using (var bdCmd = new OracleCommand("PKG_RUTA.SP_EDITAR_RUTA", conn))
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


        public RPTA_GENERAL ActualizarRecorrido(int idRecorrido, string nombre)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[2]; 
            bdParameters[0] = new OracleParameter("P_ID_RECORRIDO", OracleDbType.Int32) { Value = idRecorrido };
            bdParameters[1] = new OracleParameter("P_NOMBRE", OracleDbType.Varchar2) { Value = nombre };
 
             
            try
            {
                using (var bdCmd = new OracleCommand("PKG_RUTA.SP_ACTUALIZAR_RECORRIDO", conn))
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
        







        public RPTA_GENERAL anularRuta(int idRuta, string usuarioRegistra)
        {
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idRuta };
            bdParameters[1] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuarioRegistra };
            bdParameters[2] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_RUTA.anularRuta", conn))
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
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
                throw;
            }
            return r;
        }
    }
}
