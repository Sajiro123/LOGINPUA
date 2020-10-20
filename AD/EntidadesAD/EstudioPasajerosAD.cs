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
    public class EstudioPasajerosAD
    {
        OracleConnection conn;

        public EstudioPasajerosAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }

        public RPTA_GENERAL registroPasajeros_Campo(CC_ESTUDIO_PASAJERO ModelPasajerosCampo)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[17];

            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = ModelPasajerosCampo.ID_RUTA };
            bdParameters[1] = new OracleParameter("P_ID_PERSONA", OracleDbType.Int32) { Value = ModelPasajerosCampo.ID_PERSONA };
            bdParameters[2] = new OracleParameter("P_ID_PARADERO", OracleDbType.Int32) { Value = ModelPasajerosCampo.ID_PARADERO };
            bdParameters[3] = new OracleParameter("P_PLACA_BUS", OracleDbType.Varchar2) { Value = ModelPasajerosCampo.PLACA_BUS };
            bdParameters[4] = new OracleParameter("P_HORA_INICIO", OracleDbType.Varchar2) { Value = ModelPasajerosCampo.HORA_INICIO };
            bdParameters[5] = new OracleParameter("P_HORA_FIN", OracleDbType.Varchar2) { Value = ModelPasajerosCampo.HORA_FIN };
            bdParameters[6] = new OracleParameter("P_PASAJ_SUBEN", OracleDbType.Int32) { Value = ModelPasajerosCampo.PASAJ_SUBEN };
            bdParameters[7] = new OracleParameter("P_PASAJ_BAJAN", OracleDbType.Int32) { Value = ModelPasajerosCampo.PASAJ_BAJAN };
            bdParameters[8] = new OracleParameter("P_OBSERVACIONES", OracleDbType.Varchar2) { Value = ModelPasajerosCampo.OBSERVACIONES };
            bdParameters[9] = new OracleParameter("P_PASAJ_COLA", OracleDbType.Int32) { Value = ModelPasajerosCampo.PASAJ_COLA };
            bdParameters[10] = new OracleParameter("P_TIPO_OBSERVACION", OracleDbType.Varchar2) { Value = ModelPasajerosCampo.TIPO_OBSERVACION };
            bdParameters[11] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = ModelPasajerosCampo.USU_REG };
            bdParameters[12] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") };
            bdParameters[13] = new OracleParameter("P_CAPACIDAD_LLEGADA", OracleDbType.Int32) { Value = ModelPasajerosCampo.CAPACIDAD_LLEGADA };
            bdParameters[14] = new OracleParameter("P_CAPACIDAD_SALIDA", OracleDbType.Int32) { Value = ModelPasajerosCampo.CAPACIDAD_SALIDA };
            bdParameters[15] = new OracleParameter("P_NUMERO_SERVICIO", OracleDbType.Int32) { Value = ModelPasajerosCampo.NUMERO_SERVICIO };
            bdParameters[16] = new OracleParameter("P_ASIENTOS_LIBRES", OracleDbType.Int32) { Value = ModelPasajerosCampo.ASIENTOS_LIBRES };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_ESTUD_PASAJ.SP_INSERTAR_PASAJ_CAMPO", conn))
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

        public RPTA_GENERAL registroPasajeros_OrigenDestino(int id_corredor, int id_ruta, int id_persona, int id_paradero_orig, int id_paradero_dest, int id_tarjeta, string usuario)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[8];

            bdParameters[0] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = id_corredor };
            bdParameters[1] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = id_ruta };
            bdParameters[2] = new OracleParameter("P_ID_PARADERO_ORIG", OracleDbType.Int32) { Value = id_paradero_orig };
            bdParameters[3] = new OracleParameter("P_ID_PARADERO_DEST", OracleDbType.Int32) { Value = id_paradero_dest };
            bdParameters[4] = new OracleParameter("P_ID_PERSONA", OracleDbType.Int32) { Value = id_persona };
            bdParameters[5] = new OracleParameter("P_ID_TARJETA", OracleDbType.Int32) { Value = id_tarjeta };
            bdParameters[6] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[7] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_ESTUD_PASAJ.SP_INSERTAR_PASAJ_ORIG_DEST", conn))
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

        public RPTA_GENERAL registroPasajeros_Colectivo(int id_corredor, int id_ruta, int id_paradero, string tipo_vehiculo, int suben, int bajan, string placa, string usuario)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[9];

            bdParameters[0] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = id_corredor };
            bdParameters[1] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = id_ruta };
            bdParameters[2] = new OracleParameter("P_ID_PARADERO", OracleDbType.Int32) { Value = id_paradero };
            bdParameters[3] = new OracleParameter("P_TIPO_VEHICULO", OracleDbType.Varchar2) { Value = tipo_vehiculo };
            bdParameters[4] = new OracleParameter("P_PLACA", OracleDbType.Varchar2) { Value = placa };
            bdParameters[5] = new OracleParameter("P_SUBEN", OracleDbType.Int32) { Value = suben };
            bdParameters[6] = new OracleParameter("P_BAJAN", OracleDbType.Int32) { Value = bajan };
            bdParameters[7] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = usuario };
            bdParameters[8] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_ESTUD_PASAJ.SP_INSERTAR_PASAJ_COLECTIVO", conn))
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

        public List<CC_ESTUDIO_PASAJERO> Listar_Oferta_Demada(int id_ruta, string fechaConsultaInicio, string fechaConsultaFin)
        {
            List<CC_ESTUDIO_PASAJERO> resultado = new List<CC_ESTUDIO_PASAJERO>();

            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = id_ruta };
            bdParameters[2] = new OracleParameter("P_FINI", OracleDbType.Varchar2) { Value = fechaConsultaInicio };
            bdParameters[3] = new OracleParameter("P_FFIN", OracleDbType.Varchar2) { Value = fechaConsultaFin };


            using (var bdCmd = new OracleCommand("PKG_ESTUD_PASAJ.SP_LISTAR_PASAJ_CAMPO", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_ESTUDIO_PASAJERO();
                            //if (!DBNull.Value.Equals(bdRd["FECHA_INCIDENCIA"])) { item.FECHA_INCIDENCIA = Convert.ToDateTime(bdRd["FECHA_INCIDENCIA"]); }
                            //if (!DBNull.Value.Equals(bdRd["ID_ESTUD_PASAJ"])) { item.ID_ESTUD_PASAJ = Convert.ToInt32(bdRd["ID_ESTUD_PASAJ"]); }
                            //if (!DBNull.Value.Equals(bdRd["ID_RUTA"])) { item.ID_RUTA = Convert.ToInt32(bdRd["ID_RUTA"]); }
                            if (!DBNull.Value.Equals(bdRd["NUM_RUTA"])) { item.NUM_RUTA = bdRd["NUM_RUTA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE_CORREDOR"])) { item.NOMBRE_CORREDOR = bdRd["NOMBRE_CORREDOR"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["LADO"])) { item.LADO = bdRd["LADO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PARADERO"])) { item.PARADERO = bdRd["PARADERO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PLACA_BUS"])) { item.PLACA_BUS = bdRd["PLACA_BUS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["HORA_INICIO"])) { item.HORA_INICIO = bdRd["HORA_INICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["HORA_FIN"])) { item.HORA_FIN = bdRd["HORA_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PASAJ_SUBEN"])) { item.PASAJ_SUBEN = Convert.ToInt32(bdRd["PASAJ_SUBEN"]); }
                            if (!DBNull.Value.Equals(bdRd["PASAJ_BAJAN"])) { item.PASAJ_BAJAN = Convert.ToInt32(bdRd["PASAJ_BAJAN"]); }
                            if (!DBNull.Value.Equals(bdRd["PASAJ_COLA"])) { item.PASAJ_COLA = Convert.ToInt32(bdRd["PASAJ_COLA"]); }
                            if (!DBNull.Value.Equals(bdRd["CAPACIDAD_LLEGADA"])) { item.CAPACIDAD_LLEGADA = Convert.ToInt32(bdRd["CAPACIDAD_LLEGADA"]); }
                            if (!DBNull.Value.Equals(bdRd["CAPACIDAD_SALIDA"])) { item.CAPACIDAD_SALIDA = Convert.ToInt32(bdRd["CAPACIDAD_SALIDA"]); }
                            if (!DBNull.Value.Equals(bdRd["OBSERVACIONES"])) { item.OBSERVACIONES = bdRd["OBSERVACIONES"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NUMERO_SERVICIO"])) { item.NUMERO_SERVICIO = Convert.ToInt32(bdRd["NUMERO_SERVICIO"]); }
                            if (!DBNull.Value.Equals(bdRd["ASIENTOS_LIBRES"])) { item.ASIENTOS_LIBRES = Convert.ToInt32(bdRd["ASIENTOS_LIBRES"]); }


                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_ESTUDIO_PASAJERO> Listar_Origen_Destino(int id_ruta, string fechaConsultaInicio, string fechaConsultaFin)
        {
            List<CC_ESTUDIO_PASAJERO> resultado = new List<CC_ESTUDIO_PASAJERO>();

            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_FINI", OracleDbType.Varchar2) { Value = fechaConsultaInicio };
            bdParameters[2] = new OracleParameter("P_FFIN", OracleDbType.Varchar2) { Value = fechaConsultaFin };
            bdParameters[3] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = id_ruta };


            using (var bdCmd = new OracleCommand("PKG_ESTUD_PASAJ.SP_LISTAR_PASAJ_ORIG_DEST", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_ESTUDIO_PASAJERO();
                            //if (!DBNull.Value.Equals(bdRd["FECHA_INCIDENCIA"])) { item.FECHA_INCIDENCIA = Convert.ToDateTime(bdRd["FECHA_INCIDENCIA"]); }
                            //if (!DBNull.Value.Equals(bdRd["ID_ESTUD_PASAJ"])) { item.ID_ESTUD_PASAJ = Convert.ToInt32(bdRd["ID_ESTUD_PASAJ"]); }
                            //if (!DBNull.Value.Equals(bdRd["ID_RUTA"])) { item.ID_RUTA = Convert.ToInt32(bdRd["ID_RUTA"]); }
                            if (!DBNull.Value.Equals(bdRd["NUM_RUTA"])) { item.NUM_RUTA = bdRd["NUM_RUTA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE_CORREDOR"])) { item.NOMBRE_CORREDOR = bdRd["NOMBRE_CORREDOR"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["LADO"])) { item.LADO = bdRd["LADO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PARADERO_ORIG"])) { item.PARADERO_ORIG = bdRd["PARADERO_ORIG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PARADERO_DEST"])) { item.PARADERO_DEST = bdRd["PARADERO_DEST"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TIPO_TARJETA"])) { item.TIPO_TARJETA = bdRd["TIPO_TARJETA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }


                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<CC_ESTUDIO_PASAJERO> Listar_Colectivo(int id_ruta, string fechaConsultaInicio, string fechaConsultaFin)
        {
            List<CC_ESTUDIO_PASAJERO> resultado = new List<CC_ESTUDIO_PASAJERO>();

            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_FINI", OracleDbType.Varchar2) { Value = fechaConsultaInicio };
            bdParameters[2] = new OracleParameter("P_FFIN", OracleDbType.Varchar2) { Value = fechaConsultaFin };
            bdParameters[3] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = id_ruta };


            using (var bdCmd = new OracleCommand("PKG_ESTUD_PASAJ.SP_LISTAR_PASAJ_COLECTIVO", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_ESTUDIO_PASAJERO();
                            //if (!DBNull.Value.Equals(bdRd["FECHA_INCIDENCIA"])) { item.FECHA_INCIDENCIA = Convert.ToDateTime(bdRd["FECHA_INCIDENCIA"]); }
                            //if (!DBNull.Value.Equals(bdRd["ID_ESTUD_PASAJ"])) { item.ID_ESTUD_PASAJ = Convert.ToInt32(bdRd["ID_ESTUD_PASAJ"]); }
                            //if (!DBNull.Value.Equals(bdRd["ID_RUTA"])) { item.ID_RUTA = Convert.ToInt32(bdRd["ID_RUTA"]); }
                            if (!DBNull.Value.Equals(bdRd["NUM_RUTA"])) { item.NUM_RUTA = bdRd["NUM_RUTA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["NOMBRE_CORREDOR"])) { item.NOMBRE_CORREDOR = bdRd["NOMBRE_CORREDOR"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["LADO"])) { item.LADO = bdRd["LADO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PARADERO"])) { item.PARADERO = bdRd["PARADERO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TIPO_VEHICULO"])) { item.TIPO_VEHICULO = bdRd["TIPO_VEHICULO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PASAJ_SUBEN"])) { item.PASAJ_SUBEN = Convert.ToInt32(bdRd["PASAJ_SUBEN"]); }
                            if (!DBNull.Value.Equals(bdRd["PASAJ_BAJAN"])) { item.PASAJ_BAJAN = Convert.ToInt32(bdRd["PASAJ_BAJAN"]); }
                            if (!DBNull.Value.Equals(bdRd["PLACA_COLECTIVO"])) { item.PLACA_COLECTIVO = bdRd["PLACA_COLECTIVO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }



        public DataSet Listar_Reporte_Paraderos(int id_ruta, string fecha, string tipo_estado, string lado, ref string mensaje, ref int tipo)
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_ESTUD_PASAJ.LISTAR_PARADERO_PASAJ";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter cursor_A = new OracleParameter();
                    cursor_A.ParameterName = "P_CURSOR";
                    cursor_A.OracleDbType = OracleDbType.RefCursor;
                    cursor_A.Direction = ParameterDirection.Output;


                    OracleParameter cursor_b = new OracleParameter();
                    cursor_b.ParameterName = "P_ID_RUTA";
                    cursor_b.OracleDbType = OracleDbType.Int32;
                    cursor_b.Direction = ParameterDirection.Input;
                    cursor_b.Value = id_ruta;

                    OracleParameter cursor_C = new OracleParameter();
                    cursor_C.ParameterName = "P_FECHA";
                    cursor_C.OracleDbType = OracleDbType.Varchar2;
                    cursor_C.Direction = ParameterDirection.Input;
                    cursor_C.Value = fecha;



                    OracleParameter cursor_E = new OracleParameter();
                    cursor_E.ParameterName = "P_TIPO";
                    cursor_E.OracleDbType = OracleDbType.Varchar2;
                    cursor_E.Direction = ParameterDirection.Input;
                    cursor_E.Value = tipo_estado;

                    OracleParameter cursor_h = new OracleParameter();
                    cursor_h.ParameterName = "P_LADO";
                    cursor_h.OracleDbType = OracleDbType.Varchar2;
                    cursor_h.Direction = ParameterDirection.Input;
                    cursor_h.Value = lado;





                    command.Parameters.Add(cursor_A);
                    command.Parameters.Add(cursor_b);
                    command.Parameters.Add(cursor_C);
                    command.Parameters.Add(cursor_E);
                    command.Parameters.Add(cursor_h);



                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
                return ds;
            }
        }


        public DataSet Listar_Reporte_Dias(int id_ruta, string inicio, string fin, string tipo_estado, ref string mensaje, ref int tipo)
        {
            DataSet ds = new DataSet();

            using (var bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISCC")))
            {
                using (OracleCommand command = new OracleCommand())
                {

                    command.CommandText = "PKG_ESTUD_PASAJ.LISTAR_FECHAS_PASAJ";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = conn;

                    OracleParameter cursor_A = new OracleParameter();
                    cursor_A.ParameterName = "P_CURSOR";
                    cursor_A.OracleDbType = OracleDbType.RefCursor;
                    cursor_A.Direction = ParameterDirection.Output;


                    OracleParameter cursor_b = new OracleParameter();
                    cursor_b.ParameterName = "P_ID_RUTA";
                    cursor_b.OracleDbType = OracleDbType.Int32;
                    cursor_b.Direction = ParameterDirection.Input;
                    cursor_b.Value = id_ruta;

                    OracleParameter cursor_C = new OracleParameter();
                    cursor_C.ParameterName = "P_INIC";
                    cursor_C.OracleDbType = OracleDbType.Varchar2;
                    cursor_C.Direction = ParameterDirection.Input;
                    cursor_C.Value = inicio;

                    OracleParameter cursor_D = new OracleParameter();
                    cursor_D.ParameterName = "P_FINI";
                    cursor_D.OracleDbType = OracleDbType.Varchar2;
                    cursor_D.Direction = ParameterDirection.Input;
                    cursor_D.Value = fin;

                    OracleParameter cursor_E = new OracleParameter();
                    cursor_E.ParameterName = "P_TIPO";
                    cursor_E.OracleDbType = OracleDbType.Varchar2;
                    cursor_E.Direction = ParameterDirection.Input;
                    cursor_E.Value = tipo_estado;


                    command.Parameters.Add(cursor_A);
                    command.Parameters.Add(cursor_b);
                    command.Parameters.Add(cursor_C);
                    command.Parameters.Add(cursor_D);
                    command.Parameters.Add(cursor_E);

                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds);
                }
                return ds;
            }
        }







    }
}
