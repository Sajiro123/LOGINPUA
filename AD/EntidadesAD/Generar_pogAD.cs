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
    public class Generar_pogAD
    {
        OracleConnection conn;
        public Generar_pogAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public RPTA_GENERAL Limpiar_Temporal_POG()
        {

            RPTA_GENERAL r = new RPTA_GENERAL();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_GENERAR_POG.ELIMINAR_TEMPORAL_INTEVALO", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.ExecuteNonQuery();
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



        public DataSet Listar_Franja_POG()
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_GENERAR_POG.LISTAR_TEMPORAL";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter cursor_A = new OracleParameter();
                    cursor_A.ParameterName = "P_CURSOR";
                    cursor_A.OracleDbType = OracleDbType.RefCursor;
                    cursor_A.Direction = ParameterDirection.Output;

                    command.Parameters.Add(cursor_A);


                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
                return ds;
            }
        }


        public RPTA_GENERAL registrar_Detalle_Maestro_POG(int id_maestro_pog, string p_franja_hi, string p_franja_hf, string t_viaje_prom_a, string t_viaje_prom_b, string intervalo_a, string intervalo_b, string v_promedio_a, string v_promedio_b, string usuario)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[10];
            bdParameters[0] = new OracleParameter("P_ID_MAESTRO_POG", OracleDbType.Int32) { Value = id_maestro_pog };
            bdParameters[1] = new OracleParameter("P_FRANJA_HI", OracleDbType.Varchar2) { Value = p_franja_hi };
            bdParameters[2] = new OracleParameter("P_FRANJA_HF", OracleDbType.Varchar2) { Value = p_franja_hf };
            bdParameters[3] = new OracleParameter("P_T_VIAJE_PROM_A", OracleDbType.Varchar2) { Value = t_viaje_prom_a };
            bdParameters[4] = new OracleParameter("P_T_VIAJE_PROM_B", OracleDbType.Varchar2) { Value = t_viaje_prom_b };
            bdParameters[5] = new OracleParameter("P_INTERVALO_A", OracleDbType.Varchar2) { Value = intervalo_a };
            bdParameters[6] = new OracleParameter("P_INTERVALO_B", OracleDbType.Varchar2) { Value = intervalo_b };
            bdParameters[7] = new OracleParameter("P_V_PROMEDIO_A", OracleDbType.Varchar2) { Value = v_promedio_a };
            bdParameters[8] = new OracleParameter("P_V_PROMEDIO_B", OracleDbType.Varchar2) { Value = v_promedio_b };
            bdParameters[9] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario };


            try
            {
                using (var bdCmd = new OracleCommand("PKG_GENERAR_POG.REGISTRAR_DETALLE_POG", conn))
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


        public RPTA_GENERAL registrar_Maestro_POG(string fecha_programada, int TipoDia, int id_ruta, string usuario)
        {
            DateTime fecha = DateTime.Now;
            string fecha_actual = fecha.ToString("dd/MM/yyyy");

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[6];
            bdParameters[0] = new OracleParameter("P_ID_DIA", OracleDbType.Int32) { Value = TipoDia };
            bdParameters[1] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = id_ruta };
            bdParameters[2] = new OracleParameter("P_FECHA_PROGRAMADA", OracleDbType.Varchar2) { Value = fecha_programada };
            bdParameters[3] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[4] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha_actual };
            bdParameters[5] = new OracleParameter("P_ID_MAESTRO_POG", OracleDbType.Int32, direction: ParameterDirection.Output);



            try
            {
                using (var bdCmd = new OracleCommand("PKG_GENERAR_POG.REGISTRA_MAESTROPOG", conn))
                {
                    bdCmd.Connection.Open();
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.AUX = int.Parse(bdCmd.Parameters["P_ID_MAESTRO_POG"].Value.ToString());
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


        public RPTA_GENERAL ValidarFecha_Programados(string fecha, int idServicio)
        {
            var item = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha };
            bdParameters[2] = new OracleParameter("P_IDSERVICIO", OracleDbType.Int32) { Value = idServicio };



            using (var bdCmd = new OracleCommand("PKG_GENERAR_POG.VALIDARFECHA_PROGRAMACION", conn))
            {
                bdCmd.Connection.Open();
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            if (!DBNull.Value.Equals(bdRd["AUX"])) { item.DES_ESTADO = bdRd["AUX"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID"])) { item.AUX = Convert.ToInt32(bdRd["ID"]); }

                        }
                    }
                }
            }
            return item;

        }
        public RPTA_GENERAL Eliminar_Maestro_POG(int id_maestro)
        {
            var r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_ID_MAESTRO", OracleDbType.Int32) { Value = id_maestro };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_GENERAR_POG.ELIMINAR_MAESTRO_POG", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
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

        public DataSet Listar_Data_Pog(int id_maestro_det)
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_GENERAR_POG.GET_LIST_DATA_POG";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter cursor = new OracleParameter();
                    cursor.ParameterName = "P_CURSOR";
                    cursor.OracleDbType = OracleDbType.RefCursor;
                    cursor.Direction = ParameterDirection.Output;

                    OracleParameter P_ID_MAESTRO_DET = new OracleParameter();
                    P_ID_MAESTRO_DET.ParameterName = "P_ID_MAESTRO_DET";
                    P_ID_MAESTRO_DET.OracleDbType = OracleDbType.Int32;
                    P_ID_MAESTRO_DET.Direction = ParameterDirection.Input;
                    P_ID_MAESTRO_DET.Value = id_maestro_det;


                    command.Parameters.Add(cursor);
                    command.Parameters.Add(P_ID_MAESTRO_DET);

                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
                return ds;
            }
        }



        public DataSet Listar_Recorrido()
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_GENERAR_POG.LISTAR_RECORRIDO";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter cursor = new OracleParameter();
                    cursor.ParameterName = "P_CURSOR";
                    cursor.OracleDbType = OracleDbType.RefCursor;
                    cursor.Direction = ParameterDirection.Output;

                    command.Parameters.Add(cursor);
                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
                return ds;
            }
        }
        //public DataSet Get_IdMaestro_Pog(int id_ruta ,string fecha)
        //{
        //    DataSet ds = new DataSet();

        //    using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
        //    {
        //        using (OracleCommand command = new OracleCommand())
        //        {

        //            command.CommandText = "PKG_GENERAR_POG.GET_IDMAESTRO_POG";
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Connection = conn;

        //            OracleParameter cursor = new OracleParameter();
        //            cursor.ParameterName = "P_CURSOR";
        //            cursor.OracleDbType = OracleDbType.RefCursor;
        //            cursor.Direction = ParameterDirection.Output;

        //            OracleParameter ruta = new OracleParameter();
        //            ruta.ParameterName = "P_ID_RUTA";
        //            ruta.OracleDbType = OracleDbType.Int32;
        //            ruta.Direction = ParameterDirection.Input;
        //            ruta.Value = id_ruta;

        //            OracleParameter p_fecha = new OracleParameter();
        //            p_fecha.ParameterName = "P_FECHA";
        //            p_fecha.OracleDbType = OracleDbType.Varchar2;
        //            p_fecha.Direction = ParameterDirection.Input;
        //            p_fecha.Value = fecha;

        //            command.Parameters.Add(ruta);
        //            command.Parameters.Add(p_fecha);
        //            command.Parameters.Add(cursor);


        //            OracleDataAdapter da = new OracleDataAdapter();
        //            da.SelectCommand = command;
        //            da.Fill(ds);
        //        }
        //        return ds;
        //    }
        //}

    }
}








