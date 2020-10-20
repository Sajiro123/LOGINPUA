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
    public class CorrelativoAD
    {
        OracleConnection conn;

        public CorrelativoAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public List<CC_CORRELATIVO_INFORME> Listar_InfCorr()
        {
            List<CC_CORRELATIVO_INFORME> resultado = new List<CC_CORRELATIVO_INFORME>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);


            using (var bdCmd = new OracleCommand("PKG_INFORME_CORRELATIVO.SP_LISTAR_INF_CORR", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_CORRELATIVO_INFORME();
                            if (!DBNull.Value.Equals(bdRd["ID_INF_CORR"])) { item.ID_INF_CORR = Convert.ToInt32(bdRd["ID_INF_CORR"]); }
                            if (!DBNull.Value.Equals(bdRd["NUM_CORRELATIVO"])) { item.NUM_CORRELATIVO = Convert.ToInt32(bdRd["NUM_CORRELATIVO"]); }
                            if (!DBNull.Value.Equals(bdRd["PERS_DIRIG"])) { item.PERS_DIRIG = bdRd["PERS_DIRIG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PERS_RESP"])) { item.PERS_RESP = bdRd["PERS_RESP"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ASUNTO"])) { item.ASUNTO = bdRd["ASUNTO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["REFERENCIA"])) { item.REFERENCIA = bdRd["REFERENCIA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_INFORME = bdRd["FECHA_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_RECEPCION"])) { item.FECHA_RECEPCION = bdRd["FECHA_RECEPCION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ARCHIVADOR"])) { item.ARCHIVADOR = bdRd["ARCHIVADOR"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ESTADO_INFORME"])) { item.ESTADO_INFORME = bdRd["ESTADO_INFORME"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRES_USU"])) { item.NOMBRES_USU = bdRd["NOMBRES_USU"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["APEPAT_USU"])) { item.APEPAT_USU = bdRd["APEPAT_USU"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["APEMAT_USU"])) { item.APEMAT_USU = bdRd["APEMAT_USU"].ToString(); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public RPTA_GENERAL Registrar_InfCorr(CC_CORRELATIVO_INFORME Model_Inf_Corr)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[10];

            bdParameters[0] = new OracleParameter("P_ID_ESTADO", OracleDbType.Int32) { Value = Model_Inf_Corr.ID_ESTADO };
            bdParameters[1] = new OracleParameter("P_NUM_CORRELATIVO", OracleDbType.Varchar2) { Value = Model_Inf_Corr.NUM_CORRELATIVO };
            bdParameters[2] = new OracleParameter("P_PERS_DIRIG", OracleDbType.Varchar2) { Value = Model_Inf_Corr.PERS_DIRIG };
            bdParameters[3] = new OracleParameter("P_PERS_RESP", OracleDbType.Varchar2) { Value = Model_Inf_Corr.PERS_RESP };
            bdParameters[4] = new OracleParameter("P_ASUNTO", OracleDbType.Varchar2) { Value = Model_Inf_Corr.ASUNTO };
            bdParameters[5] = new OracleParameter("P_REFERENCIA", OracleDbType.Varchar2) { Value = Model_Inf_Corr.REFERENCIA };
            bdParameters[6] = new OracleParameter("P_FECHA_RECEPCION", OracleDbType.Varchar2) { Value = Model_Inf_Corr.FECHA_RECEPCION };
            bdParameters[7] = new OracleParameter("P_ARCHIVADOR", OracleDbType.Varchar2) { Value = Model_Inf_Corr.ARCHIVADOR };
            bdParameters[8] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = Model_Inf_Corr.USU_REG };
            bdParameters[9] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            //bdParameters[10] = new OracleParameter("P_SIGUE_NUM_CORR", OracleDbType.Int32, direction: ParameterDirection.Output);


            try
            {
                using (var bdCmd = new OracleCommand("PKG_INFORME_CORRELATIVO.SP_INSERTAR_INF_CORR", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();

                    //var ult_correlativo = int.Parse(bdCmd.Parameters["P_SIGUE_NUM_CORR"].Value.ToString());

                    //r.AUX = ult_correlativo;
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
        public RPTA_GENERAL Editar_InfCorr(CC_CORRELATIVO_INFORME Model_Inf_Corr)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[8];
            //bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");


            bdParameters[0] = new OracleParameter("P_ID_INF_CORR", OracleDbType.Int32) { Value = Model_Inf_Corr.ID_INF_CORR };
            bdParameters[1] = new OracleParameter("P_PERS_DIRIG", OracleDbType.Varchar2) { Value = Model_Inf_Corr.PERS_DIRIG };
            bdParameters[2] = new OracleParameter("P_PERS_RESP", OracleDbType.Varchar2) { Value = Model_Inf_Corr.PERS_RESP };
            bdParameters[3] = new OracleParameter("P_ASUNTO", OracleDbType.Varchar2) { Value = Model_Inf_Corr.ASUNTO };
            bdParameters[4] = new OracleParameter("P_REFERENCIA", OracleDbType.Varchar2) { Value = Model_Inf_Corr.REFERENCIA };
            bdParameters[5] = new OracleParameter("P_ARCHIVADOR", OracleDbType.Varchar2) { Value = Model_Inf_Corr.ARCHIVADOR };
            bdParameters[6] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = Model_Inf_Corr.USU_MODIF };
            bdParameters[7] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };
            try
            {
                using (var bdCmd = new OracleCommand("PKG_INFORME_CORRELATIVO.SP_EDITAR_INF_CORR", conn))
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

        public RPTA_GENERAL Actualizar_InfCorr(CC_CORRELATIVO_INFORME Model_Inf_Corr)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[4];
            //bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");


            bdParameters[0] = new OracleParameter("P_ID_INF_CORR", OracleDbType.Int32) { Value = Model_Inf_Corr.ID_INF_CORR };
            bdParameters[1] = new OracleParameter("P_FECHA_RECEPCION", OracleDbType.Varchar2) { Value = Model_Inf_Corr.FECHA_RECEPCION };
            bdParameters[2] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = Model_Inf_Corr.USU_MODIF };
            bdParameters[3] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };
            try
            {
                using (var bdCmd = new OracleCommand("PKG_INFORME_CORRELATIVO.SP_ACTUALIZAR_INF_CORR", conn))
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


        public RPTA_GENERAL Anular_InfCorr(int idInfCorr, string usuarioAnula)
        {
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_ID_INF_CORR", OracleDbType.Int32) { Value = idInfCorr };
            bdParameters[1] = new OracleParameter("P_USU_ANULA", OracleDbType.Varchar2) { Value = usuarioAnula };
            bdParameters[2] = new OracleParameter("P_FECHA_ANULA", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_INFORME_CORRELATIVO.SP_ANULAR_INF_CORR", conn))
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

        public List<CC_CORRELATIVO_INFORME> Ultimo_InfCorr()
        {
            List<CC_CORRELATIVO_INFORME> resultado = new List<CC_CORRELATIVO_INFORME>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);


            using (var bdCmd = new OracleCommand("PKG_INFORME_CORRELATIVO.SP_LISTAR_ULT_NUM_CORR", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_CORRELATIVO_INFORME();
                            if (!DBNull.Value.Equals(bdRd["ULT_NUM_CORRELATIVO"])) { item.ULT_NUM_CORRELATIVO = Convert.ToInt32(bdRd["ULT_NUM_CORRELATIVO"]); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        //public RPTA_GENERAL Actualizar_Incidencia(CC_INCIDENCIA Model_Incidencia)
        //{

        //    RPTA_GENERAL r = new RPTA_GENERAL();

        //    OracleParameter[] bdParameters = new OracleParameter[4];
        //    //bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

        //    var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");

        //    bdParameters[0] = new OracleParameter("P_ID_INCIDENCIA", OracleDbType.Int32) { Value = Model_Incidencia.ID_INCIDENCIA };
        //    bdParameters[1] = new OracleParameter("P_TIEMPO_SOLUCION", OracleDbType.Varchar2) { Value = Model_Incidencia.HORA_FIN };
        //    bdParameters[2] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = Model_Incidencia.USU_MODIF };
        //    bdParameters[3] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };

        //    try
        //    {
        //        using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_ACTUALIZAR_INCIDENCIA", conn))
        //        {
        //            bdCmd.CommandType = CommandType.StoredProcedure;
        //            bdCmd.Parameters.AddRange(bdParameters);
        //            bdCmd.ExecuteNonQuery();
        //            //BS_ID = int.Parse(bdCmd.Parameters["BS_ID"].Value.ToString());
        //            //r.AUX = BS_ID;
        //            r.COD_ESTADO = 1;
        //            r.DES_ESTADO = "Se actualizó correctamente";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        r.COD_ESTADO = 0;
        //        r.DES_ESTADO = ex.Message;
        //    }
        //    return r;
        //}


        //public RPTA_GENERAL Estado_Incidencia(int id_incidencias, string estado)
        //{
        //    RPTA_GENERAL r = new RPTA_GENERAL();

        //    OracleParameter[] bdParameters = new OracleParameter[2];
        //    bdParameters[0] = new OracleParameter("P_ID_INCIDENCIA", OracleDbType.Int32) { Value = id_incidencias };
        //    bdParameters[1] = new OracleParameter("P_ESTADO", OracleDbType.Varchar2) { Value = estado };
        //    try
        //    {
        //        using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_EDITAR_ESTADO_INCIDENCIA", conn))
        //        {
        //            bdCmd.CommandType = CommandType.StoredProcedure;
        //            bdCmd.Parameters.AddRange(bdParameters);
        //            bdCmd.ExecuteNonQuery();
        //            r.COD_ESTADO = 1;
        //            r.DES_ESTADO = "Se modificó correctamente";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        r.COD_ESTADO = 0;
        //        r.DES_ESTADO = ex.Message;
        //    }
        //    return r;
        //}




        //public List<CC_CONCESIONARIOS> ListarConcesionarios(int id_corredor)
        //{
        //    List<CC_CONCESIONARIOS> resultado = new List<CC_CONCESIONARIOS>();

        //    OracleParameter[] bdParameters = new OracleParameter[2];
        //    bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
        //    bdParameters[1] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = id_corredor };


        //    using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_LISTAR_CONCESIONARIO", conn))
        //    {
        //        bdCmd.CommandType = CommandType.StoredProcedure;
        //        bdCmd.Parameters.AddRange(bdParameters);
        //        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
        //        {
        //            if (bdRd.HasRows)
        //            {
        //                while (bdRd.Read())
        //                {
        //                    var item = new CC_CONCESIONARIOS();
        //                    if (!DBNull.Value.Equals(bdRd["ID_CONCESIONARIO"])) { item.ID_CONCESIONARIO = Convert.ToInt32(bdRd["ID_CONCESIONARIO"]); }
        //                    if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ToString(); }

        //                    resultado.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    return resultado;
        //}


        //public List<CC_INFRACCION> Listar_Infracciones()
        //{
        //    List<CC_INFRACCION> resultado = new List<CC_INFRACCION>();

        //    OracleParameter[] bdParameters = new OracleParameter[1];
        //    bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

        //    using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_LISTAR_INFRACCION", conn))
        //    {
        //        bdCmd.CommandType = CommandType.StoredProcedure;
        //        bdCmd.Parameters.AddRange(bdParameters);
        //        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
        //        {
        //            if (bdRd.HasRows)
        //            {
        //                while (bdRd.Read())
        //                {
        //                    var item = new CC_INFRACCION();
        //                    if (!DBNull.Value.Equals(bdRd["ID_INFRACCION"])) { item.ID_INFRACCION = Convert.ToInt32(bdRd["ID_INFRACCION"]); }
        //                    if (!DBNull.Value.Equals(bdRd["ID_PERSONA_INCIDENCIA"])) { item.ID_PERSONA_INCIDENCIA = Convert.ToInt32(bdRd["ID_PERSONA_INCIDENCIA"]); }
        //                    if (!DBNull.Value.Equals(bdRd["PERSONA_INCIDENCIA"])) { item.PERSONA_INCIDENCIA = bdRd["PERSONA_INCIDENCIA"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["COD_INFRACCION"])) { item.COD_INFRACCION = bdRd["COD_INFRACCION"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["DESCRIPCION"])) { item.DESCRIPCION = bdRd["DESCRIPCION"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["CALIFICACION"])) { item.CALIFICACION = bdRd["CALIFICACION"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["MULTA_UIT"])) { item.MULTA_UIT = bdRd["MULTA_UIT"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["MONTO_MULTA"])) { item.MONTO_MULTA = bdRd["MONTO_MULTA"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["TIPO_INFRACCION"])) { item.TIPO_INFRACCION = bdRd["TIPO_INFRACCION"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["SANCION"])) { item.SANCION = bdRd["SANCION"].ToString(); }
        //                    resultado.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    return resultado;
        //}

        //public List<CC_PERSONA_INCIDENCIA> Listar_Persona_Incidencia()
        //{
        //    List<CC_PERSONA_INCIDENCIA> resultado = new List<CC_PERSONA_INCIDENCIA>();

        //    OracleParameter[] bdParameters = new OracleParameter[1];
        //    bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

        //    using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_LISTAR_PERSONA_INCIDENCIA", conn))
        //    {
        //        bdCmd.CommandType = CommandType.StoredProcedure;
        //        bdCmd.Parameters.AddRange(bdParameters);
        //        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
        //        {
        //            if (bdRd.HasRows)
        //            {
        //                while (bdRd.Read())
        //                {
        //                    var item = new CC_PERSONA_INCIDENCIA();
        //                    if (!DBNull.Value.Equals(bdRd["ID_PERSONA_INCIDENCIA"])) { item.ID_PERSONA_INCIDENCIA = Convert.ToInt32(bdRd["ID_PERSONA_INCIDENCIA"]); }
        //                    if (!DBNull.Value.Equals(bdRd["DESCRIPCION"])) { item.DESCRIPCION = bdRd["DESCRIPCION"].ToString(); }


        //                    resultado.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    return resultado;
        //}


        //public RPTA_GENERAL Registrar_Infraccion(CC_INFRACCION Model_Infraccion)
        //{

        //    RPTA_GENERAL r = new RPTA_GENERAL();

        //    OracleParameter[] bdParameters = new OracleParameter[11];

        //    bdParameters[0] = new OracleParameter("P_ID_PERSONA_INCIDENCIA", OracleDbType.Int32) { Value = Model_Infraccion.ID_PERSONA_INCIDENCIA };
        //    bdParameters[1] = new OracleParameter("P_COD_INFRACCION", OracleDbType.Varchar2) { Value = Model_Infraccion.COD_INFRACCION };
        //    bdParameters[2] = new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2) { Value = Model_Infraccion.DESCRIPCION };
        //    bdParameters[3] = new OracleParameter("P_CALIFICACION", OracleDbType.Varchar2) { Value = Model_Infraccion.CALIFICACION };
        //    bdParameters[4] = new OracleParameter("P_MULTA_UIT", OracleDbType.Varchar2) { Value = Model_Infraccion.MULTA_UIT };
        //    bdParameters[5] = new OracleParameter("P_MONTO_MULTA", OracleDbType.Varchar2) { Value = Model_Infraccion.MONTO_MULTA };
        //    bdParameters[6] = new OracleParameter("P_DESCUENTO", OracleDbType.Varchar2) { Value = Model_Infraccion.TIPO_INFRACCION };
        //    bdParameters[7] = new OracleParameter("P_MOTIVO", OracleDbType.Varchar2) { Value = Model_Infraccion.SANCION };
        //    bdParameters[8] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = Model_Infraccion.USU_REG };
        //    bdParameters[9] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
        //    bdParameters[10] = new OracleParameter("P_ID_ESTADO", OracleDbType.Int32) { Value = Model_Infraccion.ID_ESTADO };


        //    try
        //    {
        //        using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_INSERTAR_INFRACCION", conn))
        //        {
        //            bdCmd.CommandType = CommandType.StoredProcedure;
        //            bdCmd.Parameters.AddRange(bdParameters);
        //            bdCmd.ExecuteNonQuery();
        //            r.COD_ESTADO = 1;
        //            r.DES_ESTADO = "Se registró correctamente";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        r.COD_ESTADO = 0;
        //        r.DES_ESTADO = ex.Message;
        //    }
        //    return r;
        //}

        //public RPTA_GENERAL Editar_Infraccion(CC_INFRACCION Model_Infraccion)
        //{

        //    RPTA_GENERAL r = new RPTA_GENERAL();

        //    OracleParameter[] bdParameters = new OracleParameter[11];
        //    //bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

        //    var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");


        //    bdParameters[0] = new OracleParameter("P_ID_INFRACCION", OracleDbType.Int32) { Value = Model_Infraccion.ID_INFRACCION };
        //    bdParameters[1] = new OracleParameter("P_ID_PERSONA_INCIDENCIA", OracleDbType.Int32) { Value = Model_Infraccion.ID_PERSONA_INCIDENCIA };
        //    bdParameters[2] = new OracleParameter("P_COD_INFRACCION", OracleDbType.Varchar2) { Value = Model_Infraccion.COD_INFRACCION };
        //    bdParameters[3] = new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2) { Value = Model_Infraccion.DESCRIPCION };
        //    bdParameters[4] = new OracleParameter("P_CALIFICACION", OracleDbType.Varchar2) { Value = Model_Infraccion.CALIFICACION };
        //    bdParameters[5] = new OracleParameter("P_MULTA_UIT", OracleDbType.Varchar2) { Value = Model_Infraccion.MULTA_UIT };
        //    bdParameters[6] = new OracleParameter("P_MONTO_MULTA", OracleDbType.Varchar2) { Value = Model_Infraccion.MONTO_MULTA };
        //    bdParameters[7] = new OracleParameter("P_DESCUENTO", OracleDbType.Varchar2) { Value = Model_Infraccion.TIPO_INFRACCION };
        //    bdParameters[8] = new OracleParameter("P_MOTIVO", OracleDbType.Varchar2) { Value = Model_Infraccion.SANCION };
        //    bdParameters[9] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = Model_Infraccion.USU_MODIF };
        //    bdParameters[10] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };



        //    try
        //    {
        //        using (var bdCmd = new OracleCommand("PKG_INCIDENCIAS.SP_EDITAR_INFRACCION", conn))
        //        {
        //            bdCmd.CommandType = CommandType.StoredProcedure;
        //            bdCmd.Parameters.AddRange(bdParameters);
        //            bdCmd.ExecuteNonQuery();
        //            //BS_ID = int.Parse(bdCmd.Parameters["BS_ID"].Value.ToString());
        //            //r.AUX = BS_ID;
        //            r.COD_ESTADO = 1;
        //            r.DES_ESTADO = "Se modificó correctamente";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        r.COD_ESTADO = 0;
        //        r.DES_ESTADO = ex.Message;
        //    }
        //    return r;
        //}


    }
}
