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
    public class SupervisorAD
    {
        OracleConnection conn;
        public SupervisorAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public RPTA_GENERAL Registrar_Formato_Campo(string usuario, int id_corredor, string placa, string direccion, int? km, string concesionario, string longuitud, string latitud, string url_foto, string url_foto2, string url_foto3, string url_foto4, string comentario)
        {

            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[16];
            bdParameters[0] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = id_corredor };
            bdParameters[1] = new OracleParameter("P_PLACA", OracleDbType.Varchar2) { Value = placa };
            bdParameters[2] = new OracleParameter("P_CONCESIONARIO", OracleDbType.Varchar2) { Value = concesionario };
            bdParameters[3] = new OracleParameter("P_DIRECCION_INSPECCION", OracleDbType.Varchar2) { Value = direccion };
            bdParameters[4] = new OracleParameter("P_KM", OracleDbType.Varchar2) { Value = km.ToString() };
            bdParameters[5] = new OracleParameter("P_LATITUD", OracleDbType.Varchar2) { Value = latitud };
            bdParameters[6] = new OracleParameter("P_LONGITUD", OracleDbType.Varchar2) { Value = longuitud };
            bdParameters[7] = new OracleParameter("P_FOTO_1", OracleDbType.Varchar2) { Value = url_foto };
            bdParameters[8] = new OracleParameter("P_FOTO_2", OracleDbType.Varchar2) { Value = url_foto2 };
            bdParameters[9] = new OracleParameter("P_FOTO_3", OracleDbType.Varchar2) { Value = url_foto3 };
            bdParameters[10] = new OracleParameter("P_FOTO_4", OracleDbType.Varchar2) { Value = url_foto4 };
            bdParameters[11] = new OracleParameter("P_COMENTARIO", OracleDbType.Varchar2) { Value = comentario };
            bdParameters[12] = new OracleParameter("P_ESTADO_BUS", OracleDbType.Varchar2) { Value = "PROGRAMABLE" };
            bdParameters[13] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[14] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[15] = new OracleParameter("P_ID_MAESTRO_BUSES", OracleDbType.Int32, direction: ParameterDirection.Output);


            try
            {
                using (var bdCmd = new OracleCommand("PKG_SUPERVISOR.SP_INSERTAR_MAESTRO_BUSES", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;

                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se registró correctamente";
                    r.AUX = int.Parse(bdCmd.Parameters["P_ID_MAESTRO_BUSES"].Value.ToString());
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

        public RPTA_GENERAL Insertar_Buses_Despacho(int id_maestro_buses, int id_conceptos, int bd_estado, int bd_calidad, string bd_observacion, string usuario)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");

            OracleParameter[] bdParameters = new OracleParameter[7];

            bdParameters[0] = new OracleParameter("P_ID_MAESTRO_BUSESD", OracleDbType.Int32) { Value = id_maestro_buses };
            bdParameters[1] = new OracleParameter("P_ID_CONCEPTOS", OracleDbType.Int32) { Value = id_conceptos };
            bdParameters[2] = new OracleParameter("P_BD_ESTADO", OracleDbType.Int32) { Value = bd_estado };
            bdParameters[3] = new OracleParameter("P_BD_CALIDAD", OracleDbType.Int32) { Value = bd_calidad };
            bdParameters[4] = new OracleParameter("P_BD_OBSERVACION", OracleDbType.Varchar2) { Value = bd_observacion };
            bdParameters[5] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[6] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = fecha_actual };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_SUPERVISOR.SP_INSERTAR_FORMATO_BUSES", conn))
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

        public RPTA_GENERAL Actualizar_No_programable(int id_buses_despacho)
        {
            //actualizar maestro no programabale


            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[1];

            bdParameters[0] = new OracleParameter("P_ID_BUSES_DESPACHO", OracleDbType.Int32) { Value = id_buses_despacho };
            try
            {
                using (var bdCmd = new OracleCommand("PKG_SUPERVISOR.SP_ACTUALIZAR_ID_FORMATO_N_P", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se actualizo correctamente";
                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
            }
            return r;
        }

        public RPTA_GENERAL Actualizar_programable(int id_maestro_buses)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[1];

            bdParameters[0] = new OracleParameter("P_ID_MAESTRO_BUSESD", OracleDbType.Int32) { Value = id_maestro_buses };
            try
            {
                using (var bdCmd = new OracleCommand("PKG_SUPERVISOR.SP_ACTUALIZAR_ID_FORMATO_S_P", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se actualizo correctamente";
                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
            }
            return r;
        }



        public DataTable Listarvalida_ruta_franjas(int id_maestro_formato_buses)
        {
            DataTable ds = new DataTable();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_SUPERVISOR.SP_LISTAR_DESPACHO_X_ID";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter cursor_A = new OracleParameter();
                    cursor_A.ParameterName = "P_CURSOR";
                    cursor_A.OracleDbType = OracleDbType.RefCursor;
                    cursor_A.Direction = ParameterDirection.Output;

                    OracleParameter comparativoA = new OracleParameter();
                    comparativoA.ParameterName = "P_ID_BUSES_DESPACHO";
                    comparativoA.OracleDbType = OracleDbType.Int32;
                    comparativoA.Direction = ParameterDirection.Input;
                    comparativoA.Value = id_maestro_formato_buses;


                    command.Parameters.Add(cursor_A);
                    command.Parameters.Add(comparativoA);

                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
                return ds;
            }
        }

        public List<BUSES_DESPACHO> Lista_Conceptos_Exterior(string id_maestro, string accion)
        {
            List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();

            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ACCION", OracleDbType.Varchar2) { Value = accion };
            bdParameters[2] = new OracleParameter("P_ID_MAESTRO", OracleDbType.Varchar2) { Value = id_maestro };

            using (var bdCmd = new OracleCommand("PKG_SUPERVISOR.SP_LISTA_CONCEPTOS_EXTERIOR", conn))
            {
                bdCmd.Connection.Open();
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new BUSES_DESPACHO();
                            if (!DBNull.Value.Equals(bdRd["CD_ID"])) { item.CD_ID = Convert.ToInt32(bdRd["CD_ID"]); }
                            if (!DBNull.Value.Equals(bdRd["CD_CONCEPTOS"])) { item.CD_CONCEPTOS = bdRd["CD_CONCEPTOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CD_TIPO_DOCUMENTO"])) { item.CD_TIPO_DOCUMENTO = bdRd["CD_TIPO_DOCUMENTO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BD_ESTADO"])) { item.BD_ESTADO = Convert.ToInt32(bdRd["BD_ESTADO"]); }
                            if (!DBNull.Value.Equals(bdRd["BD_CALIDAD"])) { item.BD_CALIDAD = Convert.ToInt32(bdRd["BD_CALIDAD"]); }
                            if (!DBNull.Value.Equals(bdRd["BD_OBSERVACION"])) { item.BD_OBSERVACION = bdRd["BD_OBSERVACION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["STATUS"])) { item.STATUS = bdRd["STATUS"].ToString(); }


                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public List<BUSES_DESPACHO> Lista_Revisiones_Mecanicas(string id_maestro, string accion)
        {
            List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();

            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ACCION", OracleDbType.Varchar2) { Value = accion };
            bdParameters[2] = new OracleParameter("P_ID_MAESTRO", OracleDbType.Varchar2) { Value = id_maestro };

            using (var bdCmd = new OracleCommand("PKG_SUPERVISOR.SP_LISTA_REVISION_TECNICA", conn))
            {
                bdCmd.Connection.Open();
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new BUSES_DESPACHO();
                            if (!DBNull.Value.Equals(bdRd["CD_ID"])) { item.CD_ID = Convert.ToInt32(bdRd["CD_ID"]); }
                            if (!DBNull.Value.Equals(bdRd["CD_CONCEPTOS"])) { item.CD_CONCEPTOS = bdRd["CD_CONCEPTOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CD_TIPO_DOCUMENTO"])) { item.CD_TIPO_DOCUMENTO = bdRd["CD_TIPO_DOCUMENTO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BD_ESTADO"])) { item.BD_ESTADO = Convert.ToInt32(bdRd["BD_ESTADO"]); }
                            if (!DBNull.Value.Equals(bdRd["BD_CALIDAD"])) { item.BD_CALIDAD = Convert.ToInt32(bdRd["BD_CALIDAD"]); }
                            if (!DBNull.Value.Equals(bdRd["BD_OBSERVACION"])) { item.BD_OBSERVACION = bdRd["BD_OBSERVACION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["STATUS"])) { item.STATUS = bdRd["STATUS"].ToString(); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<BUSES_DESPACHO> Lista_Cabina_Vehiculo(string id_maestro, string accion)
        {
            List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();

            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ACCION", OracleDbType.Varchar2) { Value = accion };
            bdParameters[2] = new OracleParameter("P_ID_MAESTRO", OracleDbType.Varchar2) { Value = id_maestro };

            using (var bdCmd = new OracleCommand("PKG_SUPERVISOR.SP_LISTA_CABINA_VEHICULO", conn))
            {
                bdCmd.Connection.Open();
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new BUSES_DESPACHO();
                            if (!DBNull.Value.Equals(bdRd["CD_ID"])) { item.CD_ID = Convert.ToInt32(bdRd["CD_ID"]); }
                            if (!DBNull.Value.Equals(bdRd["CD_CONCEPTOS"])) { item.CD_CONCEPTOS = bdRd["CD_CONCEPTOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CD_TIPO_DOCUMENTO"])) { item.CD_TIPO_DOCUMENTO = bdRd["CD_TIPO_DOCUMENTO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BD_ESTADO"])) { item.BD_ESTADO = Convert.ToInt32(bdRd["BD_ESTADO"]); }
                            if (!DBNull.Value.Equals(bdRd["BD_CALIDAD"])) { item.BD_CALIDAD = Convert.ToInt32(bdRd["BD_CALIDAD"]); }
                            if (!DBNull.Value.Equals(bdRd["BD_OBSERVACION"])) { item.BD_OBSERVACION = bdRd["BD_OBSERVACION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["STATUS"])) { item.STATUS = bdRd["STATUS"].ToString(); }
 
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<BUSES_DESPACHO> Lista_Conceptos_Dentro(string id_maestro, string accion)
        {
            List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();

            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ACCION", OracleDbType.Varchar2) { Value = accion };
            bdParameters[2] = new OracleParameter("P_ID_MAESTRO", OracleDbType.Varchar2) { Value = id_maestro };

            using (var bdCmd = new OracleCommand("PKG_SUPERVISOR.SP_LISTA_CONCEPTOS_DENTRO", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new BUSES_DESPACHO();
                            if (!DBNull.Value.Equals(bdRd["CD_ID"])) { item.CD_ID = Convert.ToInt32(bdRd["CD_ID"]); }
                            if (!DBNull.Value.Equals(bdRd["CD_CONCEPTOS"])) { item.CD_CONCEPTOS = bdRd["CD_CONCEPTOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CD_TIPO_DOCUMENTO"])) { item.CD_TIPO_DOCUMENTO = bdRd["CD_TIPO_DOCUMENTO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BD_ESTADO"])) { item.BD_ESTADO = Convert.ToInt32(bdRd["BD_ESTADO"]); }
                            if (!DBNull.Value.Equals(bdRd["BD_CALIDAD"])) { item.BD_CALIDAD = Convert.ToInt32(bdRd["BD_CALIDAD"]); }
                            if (!DBNull.Value.Equals(bdRd["BD_OBSERVACION"])) { item.BD_OBSERVACION = bdRd["BD_OBSERVACION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["STATUS"])) { item.STATUS = bdRd["STATUS"].ToString(); }


                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public DataTable Listar_Maestro_Filtros(int id_corredor, string fecha)
        {
            DataTable ds = new DataTable();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_SUPERVISOR.SP_LISTAR_MAESTRO_FILTRO";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter cursor_A = new OracleParameter();
                    cursor_A.ParameterName = "P_CURSOR";
                    cursor_A.OracleDbType = OracleDbType.RefCursor;
                    cursor_A.Direction = ParameterDirection.Output;

                    OracleParameter comparativoA = new OracleParameter();
                    comparativoA.ParameterName = "P_ID_CORREDOR";
                    comparativoA.OracleDbType = OracleDbType.Int32;
                    comparativoA.Direction = ParameterDirection.Input;
                    comparativoA.Value = id_corredor;

                    OracleParameter comparativoB = new OracleParameter();
                    comparativoB.ParameterName = "P_FECHA_REG";
                    comparativoB.OracleDbType = OracleDbType.Varchar2;
                    comparativoB.Direction = ParameterDirection.Input;
                    comparativoB.Value = fecha;


                    command.Parameters.Add(cursor_A);
                    command.Parameters.Add(comparativoA);
                    command.Parameters.Add(comparativoB);


                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
                return ds;
            }
        }










        public RPTA_GENERAL Eliminar_Maestro(int id_maestro_buses)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[1];

            bdParameters[0] = new OracleParameter("P_ID_MAESTRO_BUSESD", OracleDbType.Int32) { Value = id_maestro_buses };
            try
            {
                using (var bdCmd = new OracleCommand("PKG_SUPERVISOR.SP_ELIMAR_MAESTRO", conn))
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
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
            }
            return r;
        }

        public RPTA_GENERAL Actualizar_maestro_np(int id_maestro_buses)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[1];

            bdParameters[0] = new OracleParameter("P_ID_MAESTRO_BUSESD", OracleDbType.Int32) { Value = id_maestro_buses };
            try
            {
                using (var bdCmd = new OracleCommand("PKG_SUPERVISOR.SP_ACTUALIZAR_MAESTRO_NP", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se actualizo correctamente";
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








