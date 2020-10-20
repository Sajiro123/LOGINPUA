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
    public class IncidenciasAD
    {
        OracleConnection conn;

        public IncidenciasAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        //public List<CC_INCIDENCIA> Listar_Incidencias(int id_ruta)
        //{
        //    List<CC_INCIDENCIA> resultado = new List<CC_INCIDENCIA>();

        //    OracleParameter[] bdParameters = new OracleParameter[2];
        //    bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
        //    bdParameters[1] = new OracleParameter("P_ID_RUTA", OracleDbType.Varchar2) { Value = id_ruta };


        //    using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_LISTAR_INCIDENCIAS", conn))
        //    {
        //        bdCmd.CommandType = CommandType.StoredProcedure;
        //        bdCmd.Parameters.AddRange(bdParameters);
        //        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
        //        {
        //            if (bdRd.HasRows)
        //            {
        //                while (bdRd.Read())
        //                {
        //                    var item = new CC_INCIDENCIA();
        //                    if (!DBNull.Value.Equals(bdRd["ID_INCIDENCIA"])) { item.ID_INCIDENCIA = Convert.ToInt32(bdRd["ID_INCIDENCIA"]); }
        //                    if (!DBNull.Value.Equals(bdRd["ID_RUTA"])) { item.ID_RUTA = Convert.ToInt32(bdRd["ID_RUTA"]); }
        //                    if (!DBNull.Value.Equals(bdRd["ID_INFRACCION"])) { item.ID_INFRACCION = Convert.ToInt32(bdRd["ID_INFRACCION"]); }
        //                    if (!DBNull.Value.Equals(bdRd["EMPRESA"])) { item.EMPRESA = bdRd["EMPRESA"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["NUM_DOCUMENTO"])) { item.NUM_DOCUMENTO = bdRd["NUM_DOCUMENTO"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["FECHA_INCIDENCIA"])) { item.FECHA_INCIDENCIA = Convert.ToDateTime(bdRd["FECHA_INCIDENCIA"]); }
        //                    if (!DBNull.Value.Equals(bdRd["PLACA_PADRON"])) { item.PLACA_PADRON = bdRd["PLACA_PADRON"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["PARADERO"])) { item.PARADERO = bdRd["PARADERO"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["LADO"])) { item.LADO = bdRd["LADO"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["CONTRATO"])) { item.CONTRATO = bdRd["CONTRATO"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["EMPRESA_NOMBRE"])) { item.EMPRESA_NOMBRE = bdRd["EMPRESA_NOMBRE"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["PAQUETE_CONCESION"])) { item.PAQUETE_CONCESION = bdRd["PAQUETE_CONCESION"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["ABREVIATURA_CORREDOR"])) { item.ABREVIATURA_CORREDOR = bdRd["ABREVIATURA_CORREDOR"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["INFORMANTE"])) { item.INFORMANTE = bdRd["INFORMANTE"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["CODIGO_CAC"])) { item.CODIGO_CAC = Convert.ToInt32(bdRd["CODIGO_CAC"]); }
        //                    if (!DBNull.Value.Equals(bdRd["APELLIDOS"])) { item.APELLIDOS = bdRd["APELLIDOS"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["NOMBRES"])) { item.NOMBRES = bdRd["NOMBRES"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["COD_INFRACCION"])) { item.COD_INFRACCION = bdRd["COD_INFRACCION"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["DESCRIPCION_INFRACCION"])) { item.DESCRIPCION_INFRACCION = bdRd["DESCRIPCION_INFRACCION"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["SANCION"])) { item.SANCION = bdRd["SANCION"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["DESCRIPCION_INCIDENCIA"])) { item.DESCRIPCION_INCIDENCIA = bdRd["DESCRIPCION_INCIDENCIA"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["DESCARGO"])) { item.DESCARGO = bdRd["DESCARGO"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["NOMBRES_USU"])) { item.NOMBRES_USU = bdRd["NOMBRES_USU"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["APEPAT_USU"])) { item.APEPAT_USU = bdRd["APEPAT_USU"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["APEMAT_USU"])) { item.APEMAT_USU = bdRd["APEMAT_USU"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["ESTADO_INCIDENCIA"])) { item.ESTADO_INCIDENCIA = bdRd["ESTADO_INCIDENCIA"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["HORA_FIN"])) { item.HORA_FIN = Convert.ToDateTime(bdRd["HORA_FIN"]); }
        //                    if (!DBNull.Value.Equals(bdRd["LUGAR_HECHO"])) { item.LUGAR_HECHO = bdRd["LUGAR_HECHO"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["INTERVENCION_POLICIAL"])) { item.INTERVENCION_POLICIAL = bdRd["INTERVENCION_POLICIAL"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["INFORME"])) { item.INFORME = bdRd["INFORME"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["PROYECTO_CARTA"])) { item.PROYECTO_CARTA = bdRd["PROYECTO_CARTA"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["NUM_SERVICIO"])) { item.NUM_SERVICIO = bdRd["NUM_SERVICIO"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["NUM_INFORME"])) { item.NUM_INFORME = bdRd["NUM_INFORME"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["CUMPLIMIENTO_PLAZO"])) { item.CUMPLIMIENTO_PLAZO = bdRd["CUMPLIMIENTO_PLAZO"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["OBSERVACION_CUMPLIMIENTO"])) { item.OBSERVACION_CUMPLIMIENTO = bdRd["OBSERVACION_CUMPLIMIENTO"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["SUPERVISOR"])) { item.SUPERVISOR = bdRd["SUPERVISOR"].ToString(); }



        //                    resultado.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    return resultado;
        //}


        public DataSet Listar_Incidencias(int id_ruta)
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_INCIDENCIAS.SP_LISTAR_INCIDENCIAS";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter cursor_A = new OracleParameter();
                    cursor_A.ParameterName = "P_CURSOR";
                    cursor_A.OracleDbType = OracleDbType.RefCursor;
                    cursor_A.Direction = ParameterDirection.Output;

                    OracleParameter comparativoA = new OracleParameter();
                    comparativoA.ParameterName = "P_ID_RUTA";
                    comparativoA.OracleDbType = OracleDbType.Int32;
                    comparativoA.Direction = ParameterDirection.Input;
                    comparativoA.Value = id_ruta;


                    command.Parameters.Add(cursor_A);
                    command.Parameters.Add(comparativoA);


                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
                return ds;
            }
        }



        public RPTA_GENERAL Registrar_Incidencia(CC_INCIDENCIA Model_Incidencia, string fotoNombre1, string fotoNombre2, string fotoNombre3)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[29];

            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = Model_Incidencia.ID_RUTA };
            bdParameters[1] = new OracleParameter("P_ID_INFRACCION", OracleDbType.Int32) { Value = Model_Incidencia.ID_INFRACCION };
            bdParameters[2] = new OracleParameter("P_NUM_DOCUMENTO", OracleDbType.Varchar2) { Value = Model_Incidencia.NUM_DOCUMENTO };
            bdParameters[3] = new OracleParameter("P_EMPRESA", OracleDbType.Varchar2) { Value = Model_Incidencia.EMPRESA };
            bdParameters[4] = new OracleParameter("P_PLACA_PADRON", OracleDbType.Varchar2) { Value = Model_Incidencia.PLACA_PADRON };
            bdParameters[5] = new OracleParameter("P_NUM_SERVICIO", OracleDbType.Varchar2) { Value = Model_Incidencia.NUM_SERVICIO };
            bdParameters[6] = new OracleParameter("P_LADO", OracleDbType.Varchar2) { Value = Model_Incidencia.LADO };
            bdParameters[7] = new OracleParameter("P_LUGAR_HECHO", OracleDbType.Varchar2) { Value = Model_Incidencia.LUGAR_HECHO };
            bdParameters[8] = new OracleParameter("P_INTERVENCION_POLICIAL", OracleDbType.Varchar2) { Value = Model_Incidencia.INTERVENCION_POLICIAL };
            bdParameters[9] = new OracleParameter("P_INFORMANTE", OracleDbType.Varchar2) { Value = Model_Incidencia.INFORMANTE };
            bdParameters[10] = new OracleParameter("P_PARADERO", OracleDbType.Varchar2) { Value = Model_Incidencia.PARADERO };
            bdParameters[11] = new OracleParameter("P_FECHA_INCIDENCIA", OracleDbType.Varchar2) { Value = Model_Incidencia.FECHA_INCIDENCIA };
            bdParameters[12] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = Model_Incidencia.USU_REG };
            bdParameters[13] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[14] = new OracleParameter("P_ID_ESTADO", OracleDbType.Int32) { Value = Model_Incidencia.ID_ESTADO };
            bdParameters[15] = new OracleParameter("P_PROYECTO_CARTA", OracleDbType.Varchar2) { Value = Model_Incidencia.PROYECTO_CARTA };
            bdParameters[16] = new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2) { Value = Model_Incidencia.DESCRIPCION_INCIDENCIA };
            bdParameters[17] = new OracleParameter("P_DESCARGO", OracleDbType.Varchar2) { Value = Model_Incidencia.DESCARGO };
            bdParameters[18] = new OracleParameter("P_PAQUETE_CONCESION", OracleDbType.Varchar2) { Value = Model_Incidencia.PAQUETE_CONCESION };
            bdParameters[19] = new OracleParameter("P_CONTRATO", OracleDbType.Varchar2) { Value = Model_Incidencia.CONTRATO };
            bdParameters[20] = new OracleParameter("P_HORA_FIN", OracleDbType.Varchar2) { Value = Model_Incidencia.HORA_FIN };
            bdParameters[21] = new OracleParameter("P_INFORME", OracleDbType.Varchar2) { Value = Model_Incidencia.INFORME };

            bdParameters[22] = new OracleParameter("P_NUM_INFORME", OracleDbType.Varchar2) { Value = Model_Incidencia.NUM_INFORME };
            bdParameters[23] = new OracleParameter("P_CUMPLIMIENTO_PLAZO", OracleDbType.Varchar2) { Value = Model_Incidencia.CUMPLIMIENTO_PLAZO };
            bdParameters[24] = new OracleParameter("P_OBSERVACION_CUMPLIMIENTO", OracleDbType.Varchar2) { Value = Model_Incidencia.OBSERVACION_CUMPLIMIENTO };
            bdParameters[25] = new OracleParameter("P_SUPERVISOR", OracleDbType.Varchar2) { Value = Model_Incidencia.SUPERVISOR };
            bdParameters[26] = new OracleParameter("P_FOTO_1", OracleDbType.Varchar2) { Value = fotoNombre1 };
            bdParameters[27] = new OracleParameter("P_FOTO_2", OracleDbType.Varchar2) { Value = fotoNombre2 };
            bdParameters[28] = new OracleParameter("P_FOTO_3", OracleDbType.Varchar2) { Value = fotoNombre3 };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_INSERTAR_INCIDENCIA", conn))
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
            }
            return r;
        }

        public RPTA_GENERAL Actualizar_Incidencia(CC_INCIDENCIA Model_Incidencia)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[4];
            //bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");

            bdParameters[0] = new OracleParameter("P_ID_INCIDENCIA", OracleDbType.Int32) { Value = Model_Incidencia.ID_INCIDENCIA };
            bdParameters[1] = new OracleParameter("P_TIEMPO_SOLUCION", OracleDbType.Varchar2) { Value = Model_Incidencia.HORA_FIN };
            bdParameters[2] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = Model_Incidencia.USU_MODIF };
            bdParameters[3] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_ACTUALIZAR_INCIDENCIA", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    //BS_ID = int.Parse(bdCmd.Parameters["BS_ID"].Value.ToString());
                    //r.AUX = BS_ID;
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se actualizó correctamente";
                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
            }
            return r;
        }

        public RPTA_GENERAL Editar_Incidencia(CC_INCIDENCIA Model_Incidencia)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[8];
            //bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");


            bdParameters[0] = new OracleParameter("P_ID_INCIDENCIA", OracleDbType.Int32) { Value = Model_Incidencia.ID_INCIDENCIA };
            bdParameters[1] = new OracleParameter("P_LADO", OracleDbType.Varchar2) { Value = Model_Incidencia.LADO };
            bdParameters[2] = new OracleParameter("P_PARADERO", OracleDbType.Varchar2) { Value = Model_Incidencia.PARADERO };
            bdParameters[3] = new OracleParameter("P_FECHA_INCIDENCIA", OracleDbType.Varchar2) { Value = Model_Incidencia.FECHA_INCIDENCIA };
            bdParameters[4] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = Model_Incidencia.USU_MODIF };
            bdParameters[5] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = Model_Incidencia.FECHA_MODIF };
            bdParameters[6] = new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2) { Value = Model_Incidencia.DESCRIPCION_INCIDENCIA };
            bdParameters[7] = new OracleParameter("P_HORA_FIN", OracleDbType.Varchar2) { Value = Model_Incidencia.HORA_FIN };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_EDITAR_INCIDENCIA", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    //BS_ID = int.Parse(bdCmd.Parameters["BS_ID"].Value.ToString());
                    //r.AUX = BS_ID;
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
        public RPTA_GENERAL Estado_Incidencia(int id_incidencias, string estado)
        {
            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_ID_INCIDENCIA", OracleDbType.Int32) { Value = id_incidencias };
            bdParameters[1] = new OracleParameter("P_ESTADO", OracleDbType.Varchar2) { Value = estado };
            try
            {
                using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_EDITAR_ESTADO_INCIDENCIA", conn))
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



        public RPTA_GENERAL AnularIncidencia(int idIncidencia, string usuarioAnula)
        {
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_ID_INCIDENCIA", OracleDbType.Int32) { Value = idIncidencia };
            bdParameters[1] = new OracleParameter("P_USU_ANULA", OracleDbType.Varchar2) { Value = usuarioAnula };
            bdParameters[2] = new OracleParameter("P_FECHA_ANULA", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_ANULAR_INCIDENCIA", conn))
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

        public List<CC_CONCESIONARIOS> ListarConcesionarios(int id_corredor)
        {
            List<CC_CONCESIONARIOS> resultado = new List<CC_CONCESIONARIOS>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = id_corredor };


            using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_LISTAR_CONCESIONARIO", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_CONCESIONARIOS();
                            if (!DBNull.Value.Equals(bdRd["ID_CONCESIONARIO"])) { item.ID_CONCESIONARIO = Convert.ToInt32(bdRd["ID_CONCESIONARIO"]); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public List<CC_INFRACCION> Listar_Infracciones()
        {
            List<CC_INFRACCION> resultado = new List<CC_INFRACCION>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_LISTAR_INFRACCION", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_INFRACCION();
                            if (!DBNull.Value.Equals(bdRd["ID_INFRACCION"])) { item.ID_INFRACCION = Convert.ToInt32(bdRd["ID_INFRACCION"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_PERSONA_INCIDENCIA"])) { item.ID_PERSONA_INCIDENCIA = Convert.ToInt32(bdRd["ID_PERSONA_INCIDENCIA"]); }
                            if (!DBNull.Value.Equals(bdRd["PERSONA_INCIDENCIA"])) { item.PERSONA_INCIDENCIA = bdRd["PERSONA_INCIDENCIA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["COD_INFRACCION"])) { item.COD_INFRACCION = bdRd["COD_INFRACCION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["DESCRIPCION"])) { item.DESCRIPCION = bdRd["DESCRIPCION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TIPO_INCIDENCIA"])) { item.TIPO_INCIDENCIA = bdRd["TIPO_INCIDENCIA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CALIFICACION"])) { item.CALIFICACION = bdRd["CALIFICACION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["MULTA_UIT"])) { item.MULTA_UIT = bdRd["MULTA_UIT"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["REINCIDENCIA_UIT"])) { item.REINCIDENCIA_UIT = bdRd["REINCIDENCIA_UIT"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TIPO_INFRACCION"])) { item.TIPO_INFRACCION = bdRd["TIPO_INFRACCION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["SANCION"])) { item.SANCION = bdRd["SANCION"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_PERSONA_INCIDENCIA> Listar_Persona_Incidencia()
        {
            List<CC_PERSONA_INCIDENCIA> resultado = new List<CC_PERSONA_INCIDENCIA>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_LISTAR_PERSONA_INCIDENCIA", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_PERSONA_INCIDENCIA();
                            if (!DBNull.Value.Equals(bdRd["ID_PERSONA_INCIDENCIA"])) { item.ID_PERSONA_INCIDENCIA = Convert.ToInt32(bdRd["ID_PERSONA_INCIDENCIA"]); }
                            if (!DBNull.Value.Equals(bdRd["DESCRIPCION"])) { item.DESCRIPCION = bdRd["DESCRIPCION"].ToString(); }


                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public RPTA_GENERAL Registrar_Infraccion(CC_INFRACCION Model_Infraccion)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[12];

            bdParameters[0] = new OracleParameter("P_ID_PERSONA_INCIDENCIA", OracleDbType.Int32) { Value = Model_Infraccion.ID_PERSONA_INCIDENCIA };
            bdParameters[1] = new OracleParameter("P_COD_INFRACCION", OracleDbType.Varchar2) { Value = Model_Infraccion.COD_INFRACCION };
            bdParameters[2] = new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2) { Value = Model_Infraccion.DESCRIPCION };
            bdParameters[3] = new OracleParameter("P_TIPO_INCIDENCIA", OracleDbType.Varchar2) { Value = Model_Infraccion.TIPO_INCIDENCIA };
            bdParameters[4] = new OracleParameter("P_CALIFICACION", OracleDbType.Varchar2) { Value = Model_Infraccion.CALIFICACION };
            bdParameters[5] = new OracleParameter("P_MULTA_UIT", OracleDbType.Varchar2) { Value = Model_Infraccion.MULTA_UIT };
            bdParameters[6] = new OracleParameter("P_REINCIDENCIA_UIT", OracleDbType.Varchar2) { Value = Model_Infraccion.REINCIDENCIA_UIT };
            bdParameters[7] = new OracleParameter("P_DESCUENTO", OracleDbType.Varchar2) { Value = Model_Infraccion.TIPO_INFRACCION };
            bdParameters[8] = new OracleParameter("P_MOTIVO", OracleDbType.Varchar2) { Value = Model_Infraccion.SANCION };
            bdParameters[9] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = Model_Infraccion.USU_REG };
            bdParameters[10] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[11] = new OracleParameter("P_ID_ESTADO", OracleDbType.Int32) { Value = Model_Infraccion.ID_ESTADO };


            try
            {
                using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_INSERTAR_INFRACCION", conn))
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
            }
            return r;
        }

        public RPTA_GENERAL Editar_Infraccion(CC_INFRACCION Model_Infraccion)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[12];
            //bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");


            bdParameters[0] = new OracleParameter("P_ID_INFRACCION", OracleDbType.Int32) { Value = Model_Infraccion.ID_INFRACCION };
            bdParameters[1] = new OracleParameter("P_ID_PERSONA_INCIDENCIA", OracleDbType.Int32) { Value = Model_Infraccion.ID_PERSONA_INCIDENCIA };
            bdParameters[2] = new OracleParameter("P_COD_INFRACCION", OracleDbType.Varchar2) { Value = Model_Infraccion.COD_INFRACCION };
            bdParameters[3] = new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2) { Value = Model_Infraccion.DESCRIPCION };
            bdParameters[4] = new OracleParameter("P_TIPO_INCIDENCIA", OracleDbType.Varchar2) { Value = Model_Infraccion.TIPO_INCIDENCIA };
            bdParameters[5] = new OracleParameter("P_CALIFICACION", OracleDbType.Varchar2) { Value = Model_Infraccion.CALIFICACION };
            bdParameters[6] = new OracleParameter("P_MULTA_UIT", OracleDbType.Varchar2) { Value = Model_Infraccion.MULTA_UIT };
            bdParameters[7] = new OracleParameter("P_REINCIDENCIA_UIT", OracleDbType.Varchar2) { Value = Model_Infraccion.REINCIDENCIA_UIT };
            bdParameters[8] = new OracleParameter("P_DESCUENTO", OracleDbType.Varchar2) { Value = Model_Infraccion.TIPO_INFRACCION };
            bdParameters[9] = new OracleParameter("P_MOTIVO", OracleDbType.Varchar2) { Value = Model_Infraccion.SANCION };
            bdParameters[10] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = Model_Infraccion.USU_MODIF };
            bdParameters[11] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };



            try
            {
                using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_EDITAR_INFRACCION", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    //BS_ID = int.Parse(bdCmd.Parameters["BS_ID"].Value.ToString());
                    //r.AUX = BS_ID;
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
