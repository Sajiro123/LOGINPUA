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
    public class RegistroPicoPlacaAD
    {
        private readonly OracleConnection conn;
        public RegistroPicoPlacaAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }



        //public CC_RECORRIDO getKmPorLadoYRecorrido(int idRuta, string lado) {
        //    var bdParameters = new OracleDynamicParameters();
        //    bdParameters.Add("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
        //    bdParameters.Add("P_ID_RUTA", OracleDbType.Int32, ParameterDirection.Input, idRuta);
        //    bdParameters.Add("P_LADO", OracleDbType.Varchar2, ParameterDirection.Input, lado);

        //    var query = "PKG_PROGRAMACION_BUSES.GET_KM_POR_LADO";
        //    var result = SqlMapper.QueryFirst<CC_RECORRIDO>(conn, query, param: bdParameters, commandType: CommandType.StoredProcedure);

        //    return result;
        //}


        public CC_RECORRIDO getKmPorLadoYRecorrido(int idRuta, string lado)
        {
            CC_RECORRIDO resultado = new CC_RECORRIDO();

            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idRuta };
            bdParameters[2] = new OracleParameter("P_LADO", OracleDbType.Varchar2) { Value = lado }; ;

            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.GET_KM_POR_LADO", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_RECORRIDO();

                            if (!DBNull.Value.Equals(bdRd["MEDIDA_KM"])) { item.MEDIDA_KM = Convert.ToInt32(bdRd["MEDIDA_KM"]); }
                        }
                    }
                }
            }
            return resultado;
        }


        //public List<CC_REPORTE_PICO_PLACA> getReportePicoPlacaByFechas(string finicio, string ffin, int idRuta) {
        //    var bdParameters = new OracleDynamicParameters();
        //    bdParameters.Add("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
        //    bdParameters.Add("P_FECHA_INI", OracleDbType.Varchar2, ParameterDirection.Input, finicio);
        //    bdParameters.Add("P_FECHA_FIN", OracleDbType.Varchar2, ParameterDirection.Input, ffin);
        //    bdParameters.Add("P_ID_RUTA", OracleDbType.Int32, ParameterDirection.Input, idRuta);

        //    var query = "PKG_PROGRAMACION_BUSES.GET_DATA_REGISTROS_PICO_PLACA";
        //    var result = SqlMapper.Query<CC_REPORTE_PICO_PLACA>(conn, query, param: bdParameters, commandType: CommandType.StoredProcedure).ToList();

        //    return result;
        //}


        public List<CC_REPORTE_PICO_PLACA> getReportePicoPlacaByFechas(string finicio, string ffin, int idRuta)
        {
            List<CC_REPORTE_PICO_PLACA> resultado = new List<CC_REPORTE_PICO_PLACA>();

            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_FECHA_INI", OracleDbType.Varchar2) { Value = finicio };
            bdParameters[2] = new OracleParameter("P_FECHA_FIN", OracleDbType.Varchar2) { Value = ffin }; ;
            bdParameters[3] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idRuta }; ;


            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.GET_DATA_REGISTROS_PICO_PLACA", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_REPORTE_PICO_PLACA();

                            if (!DBNull.Value.Equals(bdRd["FECHA_REGISTRO"])) { item.FECHA_REGISTRO = bdRd["FECHA_REGISTRO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ABREV_CORREDOR"])) { item.ABREV_CORREDOR = bdRd["ABREV_CORREDOR"].ToString(); }

                            if (!DBNull.Value.Equals(bdRd["NRO_RUTA"])) { item.NRO_RUTA = bdRd["NRO_RUTA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["HINICIO"])) { item.HINICIO = bdRd["HINICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["HFIN"])) { item.HFIN = bdRd["HFIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["VEL_PROMEDIO_AB"])) { item.VEL_PROMEDIO_AB = Convert.ToDouble(bdRd["VEL_PROMEDIO_AB"]); }
                            if (!DBNull.Value.Equals(bdRd["VEL_PROMEDIO_BA"])) { item.VEL_PROMEDIO_BA = Convert.ToDouble(bdRd["VEL_PROMEDIO_BA"]); }
                            if (!DBNull.Value.Equals(bdRd["DISTANCIA_A"])) { item.DISTANCIA_A = Convert.ToDouble(bdRd["DISTANCIA_A"]); }
                            if (!DBNull.Value.Equals(bdRd["DISTANCIA_B"])) { item.DISTANCIA_B = Convert.ToDouble(bdRd["DISTANCIA_B"]); }
                            if (!DBNull.Value.Equals(bdRd["TIEMPO_PROM_A"])) { item.TIEMPO_PROM_A = bdRd["TIEMPO_PROM_A"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TIEMPO_PROM_B"])) { item.TIEMPO_PROM_B = bdRd["TIEMPO_PROM_B"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_ESTADO"])) { item.ID_ESTADO = bdRd["ID_ESTADO"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        //public List<CC_REPORTE_PICO_PLACA> getReporte_Comparativo(string fecha, int id_ruta, string turno, string ida, string vuelta)
        //{
        //    var bdParameters = new OracleDynamicParameters();
        //    bdParameters.Add("P_FECHA", OracleDbType.Varchar2, ParameterDirection.Input, fecha);
        //    bdParameters.Add("P_TURNO", OracleDbType.Varchar2, ParameterDirection.Input, turno);
        //    bdParameters.Add("IDRUTA", OracleDbType.Int32, ParameterDirection.Input, id_ruta);

        //    bdParameters.Add("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
        //    bdParameters.Add("IDA", OracleDbType.Varchar2, ParameterDirection.Input, ida);
        //    bdParameters.Add("VUELTA", OracleDbType.Varchar2, ParameterDirection.Input, vuelta);

        //    var query = "PKG_PROGRAMACION_BUSES.GET_REPORTE_COMPARATIVO";
        //    var result = SqlMapper.Query<CC_REPORTE_PICO_PLACA>(conn, query, param: bdParameters, commandType: CommandType.StoredProcedure).ToList();

        //    return result;
        //}


        public List<CC_REPORTE_PICO_PLACA> getReporte_Comparativo(string fecha, int id_ruta, string turno, string ida, string vuelta)
        {
            List<CC_REPORTE_PICO_PLACA> resultado = new List<CC_REPORTE_PICO_PLACA>();

            OracleParameter[] bdParameters = new OracleParameter[6];
            bdParameters[0] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha };
            bdParameters[1] = new OracleParameter("P_TURNO", OracleDbType.Varchar2) { Value = turno };
            bdParameters[2] = new OracleParameter("IDRUTA", OracleDbType.Int32) { Value = id_ruta }; ;
            bdParameters[3] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[4] = new OracleParameter("IDA", OracleDbType.Varchar2) { Value = ida }; ;
            bdParameters[5] = new OracleParameter("VUELTA", OracleDbType.Varchar2) { Value = vuelta }; ;

            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.GET_REPORTE_COMPARATIVO", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_REPORTE_PICO_PLACA();

                            if (!DBNull.Value.Equals(bdRd["ID_REG_PPLAC"])) { item.ID_REG_PPLAC = Convert.ToInt32(bdRd["ID_REG_PPLAC"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_RUTA"])) { item.ID_RUTA = Convert.ToInt32(bdRd["ID_RUTA"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_ESTADO"])) { item.ID_ESTADO = bdRd["ID_ESTADO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TURNO"])) { item.TURNO = bdRd["TURNO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["HINICIO"])) { item.HINICIO = bdRd["HINICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["HFIN"])) { item.HFIN = bdRd["HFIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["VEL_PROMEDIO_AB"])) { item.VEL_PROMEDIO_AB = Convert.ToDouble(bdRd["VEL_PROMEDIO_AB"]); }
                            if (!DBNull.Value.Equals(bdRd["OBS"])) { item.OBS = bdRd["OBS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_REG"])) { item.USU_REG = bdRd["USU_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = bdRd["FECHA_REG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_MODIF"])) { item.USU_MODIF = bdRd["USU_MODIF"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_MODIF"])) { item.FECHA_MODIF = bdRd["FECHA_MODIF"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["USU_ANULA"])) { item.USU_ANULA = bdRd["USU_ANULA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_ANULA"])) { item.FECHA_ANULA = bdRd["FECHA_ANULA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["FECHA_REGISTRO"])) { item.FECHA_REGISTRO = bdRd["FECHA_REGISTRO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["VEL_PROMEDIO_BA"])) { item.VEL_PROMEDIO_BA = Convert.ToDouble(bdRd["VEL_PROMEDIO_BA"]); }
                            if (!DBNull.Value.Equals(bdRd["DISTANCIA_A"])) { item.DISTANCIA_A = Convert.ToDouble(bdRd["DISTANCIA_A"]); }
                            if (!DBNull.Value.Equals(bdRd["DISTANCIA_B"])) { item.DISTANCIA_B = Convert.ToDouble(bdRd["DISTANCIA_B"]); }
                            if (!DBNull.Value.Equals(bdRd["TIEMPO_PROM_A"])) { item.TIEMPO_PROM_A = bdRd["TIEMPO_PROM_A"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TIEMPO_PROM_B"])) { item.TIEMPO_PROM_B = bdRd["TIEMPO_PROM_B"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }



        //public RPTA_GENERAL registrarPicoPlaca(int idRuta, int idEstado, string turno,
        //                                        string fechaRegistro, string horaInicio, string horaFin,
        //                                        double velocidadPromedio_AB, double velocidadPromedio_BA,
        //                                        double distancia_A, double distancia_B,
        //                                        double tiempo_A, double tiempo_B,
        //                                        string observacion, string nomUsuario)
        //{
        //    RPTA_GENERAL r = new RPTA_GENERAL();
        //    var bdParameters = new OracleDynamicParameters();
        //    var query = "PKG_PROGRAMACION_BUSES.REGISTRO_PICO_PLACA";
        //    try
        //    {
        //        bdParameters.Add("P_ID_RUTA", OracleDbType.Int32, ParameterDirection.Input, idRuta);
        //        bdParameters.Add("P_ID_ESTADO", OracleDbType.Int32, ParameterDirection.Input, idEstado);
        //        bdParameters.Add("P_TURNO", OracleDbType.Varchar2, ParameterDirection.Input, turno);
        //        bdParameters.Add("P_FECHA_REGISTRO", OracleDbType.Varchar2, ParameterDirection.Input, fechaRegistro);
        //        bdParameters.Add("P_HINICIO", OracleDbType.Varchar2, ParameterDirection.Input, horaInicio);
        //        bdParameters.Add("P_HFIN", OracleDbType.Varchar2, ParameterDirection.Input, horaFin);
        //        bdParameters.Add("P_VEL_PROMEDIO_AB", OracleDbType.Decimal, ParameterDirection.Input, velocidadPromedio_AB);
        //        bdParameters.Add("P_VEL_PROMEDIO_BA", OracleDbType.Decimal, ParameterDirection.Input, velocidadPromedio_BA);
        //        bdParameters.Add("P_DISTANCIA_A", OracleDbType.Decimal, ParameterDirection.Input, distancia_A);
        //        bdParameters.Add("P_DISTANCIA_B", OracleDbType.Decimal, ParameterDirection.Input, distancia_B);
        //        bdParameters.Add("P_TIEMPO_PROM_A", OracleDbType.Decimal, ParameterDirection.Input, tiempo_A);
        //        bdParameters.Add("P_TIEMPO_PROM_B", OracleDbType.Decimal, ParameterDirection.Input, tiempo_B);
        //        bdParameters.Add("P_OBS", OracleDbType.Varchar2, ParameterDirection.Input, observacion);
        //        bdParameters.Add("P_FECHA_REG", OracleDbType.Varchar2, ParameterDirection.Input, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));
        //        bdParameters.Add("P_USU_REG", OracleDbType.Varchar2, ParameterDirection.Input, nomUsuario);
        //        SqlMapper.QueryFirstOrDefault(conn, query, param: bdParameters, commandType: CommandType.StoredProcedure);

        //        r.COD_ESTADO = 1;
        //        r.DES_ESTADO = "Se registró correctamente";
        //    }
        //    catch (Exception ex)
        //    {
        //        r.COD_ESTADO = 0;
        //        r.DES_ESTADO = ex.Message;
        //        throw;
        //    }
        //    return r;
        //}


        public RPTA_GENERAL registrarPicoPlaca(int idRuta, int idEstado, string turno,
                                                string fechaRegistro, string horaInicio, string horaFin,
                                                double velocidadPromedio_AB, double velocidadPromedio_BA,
                                                double distancia_A, double distancia_B,
                                                double tiempo_A, double tiempo_B,
                                                string observacion, string nomUsuario)
        {
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[15];

            bdParameters[0] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idRuta };
            bdParameters[1] = new OracleParameter("P_ID_ESTADO", OracleDbType.Int32) { Value = idEstado };
            bdParameters[2] = new OracleParameter("P_TURNO", OracleDbType.Varchar2) { Value = turno };
            bdParameters[3] = new OracleParameter("P_FECHA_REGISTRO", OracleDbType.Varchar2) { Value = fechaRegistro };
            bdParameters[4] = new OracleParameter("P_HINICIO", OracleDbType.Varchar2) { Value = horaInicio };
            bdParameters[5] = new OracleParameter("P_HFIN", OracleDbType.Int32) { Value = horaFin };
            bdParameters[6] = new OracleParameter("P_VEL_PROMEDIO_AB", OracleDbType.Decimal) { Value = velocidadPromedio_AB };
            bdParameters[7] = new OracleParameter("P_VEL_PROMEDIO_BA", OracleDbType.Decimal) { Value = velocidadPromedio_BA };
            bdParameters[8] = new OracleParameter("P_DISTANCIA_A", OracleDbType.Decimal) { Value = distancia_A };
            bdParameters[9] = new OracleParameter("P_DISTANCIA_B", OracleDbType.Decimal) { Value = distancia_B };
            bdParameters[10] = new OracleParameter("P_TIEMPO_PROM_A", OracleDbType.Decimal) { Value = tiempo_A };
            bdParameters[11] = new OracleParameter("P_TIEMPO_PROM_B", OracleDbType.Decimal) { Value = tiempo_B };
            bdParameters[12] = new OracleParameter("P_OBS", OracleDbType.Decimal) { Value = observacion };
            bdParameters[13] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[14] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = nomUsuario };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.REGISTRO_PICO_PLACA", conn))
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
                r.DES_ESTADO = ex.Message;
            }
            return r;
        }

        //public RPTA_GENERAL anularRegistrosPorFecha(string fechaRegistro, int idRuta)
        //{

        //    RPTA_GENERAL r = new RPTA_GENERAL();
        //    var bdParameters = new OracleDynamicParameters();
        //    var query = "PKG_PROGRAMACION_BUSES.ELIMINAR_REGISTROS";
        //    try
        //    {
        //        bdParameters.Add("P_FECHA_REGISTRO", OracleDbType.Varchar2, ParameterDirection.Input, fechaRegistro);
        //        bdParameters.Add("P_ID_RUTA", OracleDbType.Int32, ParameterDirection.Input, idRuta);
        //        SqlMapper.QueryFirstOrDefault(conn, query, param: bdParameters, commandType: CommandType.StoredProcedure);
        //        r.COD_ESTADO = 1;
        //        r.DES_ESTADO = "Se anuló los registros correctamente";
        //    }
        //    catch (Exception ex)
        //    {
        //        r.COD_ESTADO = 0;
        //        r.DES_ESTADO = ex.Message;
        //    }
        //    return r;
        //}



        public RPTA_GENERAL anularRegistrosPorFecha(string fechaRegistro, int idRuta)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            string fecha_actual = dateTime.ToString("dd/MM/yyyy");
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_FECHA_REGISTRO", OracleDbType.Varchar2) { Value = fechaRegistro };
            bdParameters[1] = new OracleParameter("P_ID_RUTA", OracleDbType.Int32) { Value = idRuta };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_PERFIL.anularPerfil", conn))
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




        //public List<CC_REPORTE_PICO_PLACA> getHora_Comparativo(string fecha, int id_ruta, string turno)
        //{
        //    var bdParameters = new OracleDynamicParameters();
        //    bdParameters.Add("P_FECHA", OracleDbType.Varchar2, ParameterDirection.Input, fecha);
        //    bdParameters.Add("P_TURNO", OracleDbType.Varchar2, ParameterDirection.Input, turno);
        //    bdParameters.Add("IDRUTA", OracleDbType.Int32, ParameterDirection.Input, id_ruta);
        //    bdParameters.Add("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

        //    var query = "PKG_PROGRAMACION_BUSES.GET_HORA_RECORRIDO";
        //    var result = SqlMapper.Query<CC_REPORTE_PICO_PLACA>(conn, query, param: bdParameters, commandType: CommandType.StoredProcedure).ToList();

        //    return result;
        //}

        public List<CC_REPORTE_PICO_PLACA> getHora_Comparativo(string fecha, int id_ruta, string turno)
        {
            List<CC_REPORTE_PICO_PLACA> resultado = new List<CC_REPORTE_PICO_PLACA>();

            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fecha };
            bdParameters[1] = new OracleParameter("P_TURNO", OracleDbType.Varchar2) { Value = turno };
            bdParameters[2] = new OracleParameter("IDRUTA", OracleDbType.Int32) { Value = id_ruta }; ;
            bdParameters[3] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_PROGRAMACION_BUSES.GET_HORA_RECORRIDO", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_REPORTE_PICO_PLACA();

                            if (!DBNull.Value.Equals(bdRd["TIEMPO_IDA"])) { item.TIEMPO_IDA = bdRd["TIEMPO_IDA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["TIEMPO_VUELTA"])) { item.TIEMPO_VUELTA = bdRd["TIEMPO_VUELTA"].ToString(); }
                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }








    }
}
