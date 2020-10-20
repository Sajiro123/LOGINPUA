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


    public class BusesAD
    {
        OracleConnection conn;

        public BusesAD(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref conn, _bdConn);
        }


        public List<CC_BUSES> Listar_Buses()
        {
            List<CC_BUSES> resultado = new List<CC_BUSES>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_BUSES.SP_LISTA_BUSES", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_BUSES();
                            if (!DBNull.Value.Equals(bdRd["BS_ID"])) { item.BS_ID = Convert.ToInt32(bdRd["BS_ID"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_CORREDOR"])) { item.ID_CORREDOR = Convert.ToInt32(bdRd["ID_CORREDOR"]); }
                            if (!DBNull.Value.Equals(bdRd["ABREVIATURA"])) { item.ABREVIATURA = bdRd["ABREVIATURA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_FECINI_PT"])) { item.BS_FECINI_PT = bdRd["BS_FECINI_PT"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_NOM_EMPRE"])) { item.BS_NOM_EMPRE = bdRd["BS_NOM_EMPRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PLACA"])) { item.BS_PLACA = bdRd["BS_PLACA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PROPIETARIO"])) { item.BS_PROPIETARIO = bdRd["BS_PROPIETARIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PAQUETE_CONCESION"])) { item.BS_PAQUETE_CONCESION = bdRd["BS_PAQUETE_CONCESION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PAQUETE_SERVICIO"])) { item.BS_PAQUETE_SERVICIO = bdRd["BS_PAQUETE_SERVICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_TIPO_SERVICIO"])) { item.BS_TIPO_SERVICIO = bdRd["BS_TIPO_SERVICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_ESTADO"])) { item.BS_ESTADO = bdRd["BS_ESTADO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_MARCA"])) { item.BS_MARCA = bdRd["BS_MARCA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_MODELO"])) { item.BS_MODELO = bdRd["BS_MODELO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_AÑO_FABRICACION"])) { item.BS_AÑO_FABRICACION = bdRd["BS_AÑO_FABRICACION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_COMBUSTIBLE"])) { item.BS_COMBUSTIBLE = bdRd["BS_COMBUSTIBLE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_TECNOLOGIA_EURO"])) { item.BS_TECNOLOGIA_EURO = bdRd["BS_TECNOLOGIA_EURO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_POTENCIA_MOTOR"])) { item.BS_POTENCIA_MOTOR = bdRd["BS_POTENCIA_MOTOR"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_SERIE_MOTOR"])) { item.BS_SERIE_MOTOR = bdRd["BS_SERIE_MOTOR"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_SERIE_CHASIS"])) { item.BS_SERIE_CHASIS = bdRd["BS_SERIE_CHASIS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_COLOR_VEHICULO"])) { item.BS_COLOR_VEHICULO = bdRd["BS_COLOR_VEHICULO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_LONGITUD"])) { item.BS_LONGITUD = bdRd["BS_LONGITUD"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_ASIENTOS"])) { item.BS_ASIENTOS = bdRd["BS_ASIENTOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_AREA_PASILLO"])) { item.BS_AREA_PASILLO = bdRd["BS_AREA_PASILLO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PESO_NETO"])) { item.BS_PESO_NETO = bdRd["BS_PESO_NETO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PESO_BRUTO"])) { item.BS_PESO_BRUTO = bdRd["BS_PESO_BRUTO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_ALTURA"])) { item.BS_ALTURA = bdRd["BS_ALTURA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_ANCHO"])) { item.BS_ANCHO = bdRd["BS_ANCHO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_CARGA_UTIL"])) { item.BS_CARGA_UTIL = bdRd["BS_CARGA_UTIL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PUERTA_IZQUIERDA"])) { item.BS_PUERTA_IZQUIERDA = bdRd["BS_PUERTA_IZQUIERDA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PARTIDA_REGISTRAL"])) { item.BS_PARTIDA_REGISTRAL = bdRd["BS_PARTIDA_REGISTRAL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_CODIGO_CVS"])) { item.BS_CODIGO_CVS = bdRd["BS_CODIGO_CVS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_CVS_FEC_INIC"])) { item.BS_CVS_FEC_INIC = bdRd["BS_CVS_FEC_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_CVS_FEC_FIN"])) { item.BS_CVS_FEC_FIN = bdRd["BS_CVS_FEC_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_SOAT_FEC_INIC"])) { item.BS_SOAT_FEC_INIC = bdRd["BS_SOAT_FEC_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_SOAT_FEC_FIN"])) { item.BS_SOAT_FEC_FIN = bdRd["BS_SOAT_FEC_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_POLIZA_VEHICULOS"])) { item.BS_POLIZA_VEHICULOS = bdRd["BS_POLIZA_VEHICULOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_VEHICULOS_INIC"])) { item.BS_VEHICULOS_INIC = bdRd["BS_VEHICULOS_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_VEHICULOS_FIN"])) { item.BS_VEHICULOS_FIN = bdRd["BS_VEHICULOS_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_POLIZA_CIVIL"])) { item.BS_POLIZA_CIVIL = bdRd["BS_POLIZA_CIVIL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_RC_INICIO"])) { item.BS_RC_INICIO = bdRd["BS_RC_INICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_RC_FIN"])) { item.BS_RC_FIN = bdRd["BS_RC_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_POLIZA_ACCI_COLECTIVOS"])) { item.BS_POLIZA_ACCI_COLECTIVOS = bdRd["BS_POLIZA_ACCI_COLECTIVOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_APCP_INIC"])) { item.BS_APCP_INIC = bdRd["BS_APCP_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_APCP_FIN"])) { item.BS_APCP_FIN = bdRd["BS_APCP_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_POLIZA_MULTIRIESGOS"])) { item.BS_POLIZA_MULTIRIESGOS = bdRd["BS_POLIZA_MULTIRIESGOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_MULTIRESGOS_INIC"])) { item.BS_MULTIRESGOS_INIC = bdRd["BS_MULTIRESGOS_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_MULTIRESGOS_FIN"])) { item.BS_MULTIRESGOS_FIN = bdRd["BS_MULTIRESGOS_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_RTV_INIC"])) { item.BS_RTV_INIC = bdRd["BS_RTV_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_RTV_FIN"])) { item.BS_RTV_FIN = bdRd["BS_RTV_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_REVI_ANUAL_GNV_INIC"])) { item.BS_REVI_ANUAL_GNV_INIC = bdRd["BS_REVI_ANUAL_GNV_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_REVI_ANUAL_GNV_FIN"])) { item.BS_REVI_ANUAL_GNV_FIN = bdRd["BS_REVI_ANUAL_GNV_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_REVISION_CILINDROS_GNV_INIC"])) { item.BS_REVISION_CILINDROS_GNV_INIC = bdRd["BS_REVISION_CILINDROS_GNV_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_REVISION_CILINDROS_GNV_FIN"])) { item.BS_REVISION_CILINDROS_GNV_FIN = bdRd["BS_REVISION_CILINDROS_GNV_FIN"].ToString(); }
                            //if (!DBNull.Value.Equals(bdRd["COD_CAC"])) { item.COD_CAC = bdRd["COD_CAC"].ToString(); }
                            //if (!DBNull.Value.Equals(bdRd["ESTADO_VEHICULO"])) { item.ESTADO_VEHICULO = bdRd["ESTADO_VEHICULO"].ToString(); }
                            //if (!DBNull.Value.Equals(bdRd["PLACA_REEMPLAZADA"])) { item.PLACA_REEMPLAZADA = bdRd["PLACA_REEMPLAZADA"].ToString(); }
                            //if (!DBNull.Value.Equals(bdRd["USUREG"])) { item.USUREG = bdRd["USUREG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_ESTADO"])) { item.ID_ESTADO = Convert.ToInt32(bdRd["ID_ESTADO"]); }
                            if (!DBNull.Value.Equals(bdRd["BS_PADRON"])) { item.BS_PADRON = bdRd["BS_PADRON"].ToString(); }


                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public CC_BUSES Consultar_x_placa(string placa)
        {
            CC_BUSES resultado = new CC_BUSES();

            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("BS_PLACA_", OracleDbType.Varchar2) { Value = placa };
            bdParameters[2] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy") };

            using (var bdCmd = new OracleCommand("PKG_BUSES.SP_BUSCAR_PLACA", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                             resultado = new CC_BUSES();

                            if (!DBNull.Value.Equals(bdRd["BS_PLACA"])) { resultado.BS_PLACA = bdRd["BS_PLACA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_NOM_EMPRE"])) { resultado.BS_NOM_EMPRE = bdRd["BS_NOM_EMPRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_MARCA"])) { resultado.BS_MARCA = bdRd["BS_MARCA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_MODELO"])) { resultado.BS_MODELO = bdRd["BS_MODELO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_CODIGO_CVS"])) { resultado.BS_CODIGO_CVS = bdRd["BS_CODIGO_CVS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["CORREDOR_NOMBRE"])) { resultado.CORREDOR_NOMBRE = bdRd["CORREDOR_NOMBRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PAQUETE_SERVICIO"])) { resultado.BS_PAQUETE_SERVICIO = bdRd["BS_PAQUETE_SERVICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_ESTADO"])) { resultado.BS_ESTADO = bdRd["BS_ESTADO"].ToString(); }


                        }
                    }
                }
            }
            return resultado;
        }

               
        public List<CC_BUSES> Listar_Buses_by_Id(int idCorredor)
        {
            List<CC_BUSES> resultado = new List<CC_BUSES>();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = idCorredor };

            using (var bdCmd = new OracleCommand("PKG_BUSES.SP_LISTA_BUSES_BY_ID", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_BUSES();
                            if (!DBNull.Value.Equals(bdRd["BS_ID"])) { item.BS_ID = Convert.ToInt32(bdRd["BS_ID"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_CORREDOR"])) { item.ID_CORREDOR = Convert.ToInt32(bdRd["ID_CORREDOR"]); }
                            if (!DBNull.Value.Equals(bdRd["ABREVIATURA"])) { item.ABREVIATURA = bdRd["ABREVIATURA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_FECINI_PT"])) { item.BS_FECINI_PT = bdRd["BS_FECINI_PT"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_NOM_EMPRE"])) { item.BS_NOM_EMPRE = bdRd["BS_NOM_EMPRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PLACA"])) { item.BS_PLACA = bdRd["BS_PLACA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PROPIETARIO"])) { item.BS_PROPIETARIO = bdRd["BS_PROPIETARIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PAQUETE_CONCESION"])) { item.BS_PAQUETE_CONCESION = bdRd["BS_PAQUETE_CONCESION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PAQUETE_SERVICIO"])) { item.BS_PAQUETE_SERVICIO = bdRd["BS_PAQUETE_SERVICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_TIPO_SERVICIO"])) { item.BS_TIPO_SERVICIO = bdRd["BS_TIPO_SERVICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_ESTADO"])) { item.BS_ESTADO = bdRd["BS_ESTADO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_MARCA"])) { item.BS_MARCA = bdRd["BS_MARCA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_MODELO"])) { item.BS_MODELO = bdRd["BS_MODELO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_AÑO_FABRICACION"])) { item.BS_AÑO_FABRICACION = bdRd["BS_AÑO_FABRICACION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_COMBUSTIBLE"])) { item.BS_COMBUSTIBLE = bdRd["BS_COMBUSTIBLE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_TECNOLOGIA_EURO"])) { item.BS_TECNOLOGIA_EURO = bdRd["BS_TECNOLOGIA_EURO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_POTENCIA_MOTOR"])) { item.BS_POTENCIA_MOTOR = bdRd["BS_POTENCIA_MOTOR"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_SERIE_MOTOR"])) { item.BS_SERIE_MOTOR = bdRd["BS_SERIE_MOTOR"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_SERIE_CHASIS"])) { item.BS_SERIE_CHASIS = bdRd["BS_SERIE_CHASIS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_COLOR_VEHICULO"])) { item.BS_COLOR_VEHICULO = bdRd["BS_COLOR_VEHICULO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_LONGITUD"])) { item.BS_LONGITUD = bdRd["BS_LONGITUD"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_ASIENTOS"])) { item.BS_ASIENTOS = bdRd["BS_ASIENTOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_AREA_PASILLO"])) { item.BS_AREA_PASILLO = bdRd["BS_AREA_PASILLO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PESO_NETO"])) { item.BS_PESO_NETO = bdRd["BS_PESO_NETO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PESO_BRUTO"])) { item.BS_PESO_BRUTO = bdRd["BS_PESO_BRUTO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_ALTURA"])) { item.BS_ALTURA = bdRd["BS_ALTURA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_ANCHO"])) { item.BS_ANCHO = bdRd["BS_ANCHO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_CARGA_UTIL"])) { item.BS_CARGA_UTIL = bdRd["BS_CARGA_UTIL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PUERTA_IZQUIERDA"])) { item.BS_PUERTA_IZQUIERDA = bdRd["BS_PUERTA_IZQUIERDA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PARTIDA_REGISTRAL"])) { item.BS_PARTIDA_REGISTRAL = bdRd["BS_PARTIDA_REGISTRAL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_CODIGO_CVS"])) { item.BS_CODIGO_CVS = bdRd["BS_CODIGO_CVS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_CVS_FEC_INIC"])) { item.BS_CVS_FEC_INIC = bdRd["BS_CVS_FEC_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_CVS_FEC_FIN"])) { item.BS_CVS_FEC_FIN = bdRd["BS_CVS_FEC_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_SOAT_FEC_INIC"])) { item.BS_SOAT_FEC_INIC = bdRd["BS_SOAT_FEC_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_SOAT_FEC_FIN"])) { item.BS_SOAT_FEC_FIN = bdRd["BS_SOAT_FEC_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_POLIZA_VEHICULOS"])) { item.BS_POLIZA_VEHICULOS = bdRd["BS_POLIZA_VEHICULOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_VEHICULOS_INIC"])) { item.BS_VEHICULOS_INIC = bdRd["BS_VEHICULOS_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_VEHICULOS_FIN"])) { item.BS_VEHICULOS_FIN = bdRd["BS_VEHICULOS_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_POLIZA_CIVIL"])) { item.BS_POLIZA_CIVIL = bdRd["BS_POLIZA_CIVIL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_RC_INICIO"])) { item.BS_RC_INICIO = bdRd["BS_RC_INICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_RC_FIN"])) { item.BS_RC_FIN = bdRd["BS_RC_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_POLIZA_ACCI_COLECTIVOS"])) { item.BS_POLIZA_ACCI_COLECTIVOS = bdRd["BS_POLIZA_ACCI_COLECTIVOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_APCP_INIC"])) { item.BS_APCP_INIC = bdRd["BS_APCP_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_APCP_FIN"])) { item.BS_APCP_FIN = bdRd["BS_APCP_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_POLIZA_MULTIRIESGOS"])) { item.BS_POLIZA_MULTIRIESGOS = bdRd["BS_POLIZA_MULTIRIESGOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_MULTIRESGOS_INIC"])) { item.BS_MULTIRESGOS_INIC = bdRd["BS_MULTIRESGOS_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_MULTIRESGOS_FIN"])) { item.BS_MULTIRESGOS_FIN = bdRd["BS_MULTIRESGOS_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_RTV_INIC"])) { item.BS_RTV_INIC = bdRd["BS_RTV_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_RTV_FIN"])) { item.BS_RTV_FIN = bdRd["BS_RTV_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_REVI_ANUAL_GNV_INIC"])) { item.BS_REVI_ANUAL_GNV_INIC = bdRd["BS_REVI_ANUAL_GNV_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_REVI_ANUAL_GNV_FIN"])) { item.BS_REVI_ANUAL_GNV_FIN = bdRd["BS_REVI_ANUAL_GNV_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_REVISION_CILINDROS_GNV_INIC"])) { item.BS_REVISION_CILINDROS_GNV_INIC = bdRd["BS_REVISION_CILINDROS_GNV_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_REVISION_CILINDROS_GNV_FIN"])) { item.BS_REVISION_CILINDROS_GNV_FIN = bdRd["BS_REVISION_CILINDROS_GNV_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_ESTADO"])) { item.ID_ESTADO = Convert.ToInt32(bdRd["ID_ESTADO"]); }
                            if (!DBNull.Value.Equals(bdRd["BS_PADRON"])) { item.BS_PADRON = bdRd["BS_PADRON"].ToString(); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public RPTA_GENERAL Insertar_Buses_Nuevos(CC_BUSES modelo)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[57];

            bdParameters[0] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = modelo.ID_CORREDOR };
            bdParameters[1] = new OracleParameter("P_BS_FECINI_PT", OracleDbType.Varchar2) { Value = modelo.BS_FECINI_PT };
            bdParameters[2] = new OracleParameter("P_BS_NOM_EMPRE", OracleDbType.Varchar2) { Value = modelo.BS_NOM_EMPRE };
            bdParameters[3] = new OracleParameter("P_BS_PLACA", OracleDbType.Varchar2) { Value = modelo.BS_PLACA };
            bdParameters[4] = new OracleParameter("P_BS_PROPIETARIO", OracleDbType.Varchar2) { Value = modelo.BS_PROPIETARIO };
            bdParameters[5] = new OracleParameter("P_BS_PAQUETE_CONCESION", OracleDbType.Varchar2) { Value = modelo.BS_PAQUETE_CONCESION };
            bdParameters[6] = new OracleParameter("P_BS_PAQUETE_SERVICIO", OracleDbType.Varchar2) { Value = modelo.BS_PAQUETE_SERVICIO };
            bdParameters[7] = new OracleParameter("P_BS_TIPO_SERVICIO", OracleDbType.Varchar2) { Value = modelo.BS_TIPO_SERVICIO };
            bdParameters[8] = new OracleParameter("P_BS_ESTADO", OracleDbType.Varchar2) { Value = modelo.BS_ESTADO };
            bdParameters[9] = new OracleParameter("P_BS_MARCA", OracleDbType.Varchar2) { Value = modelo.BS_MARCA };
            bdParameters[10] = new OracleParameter("P_BS_MODELO", OracleDbType.Varchar2) { Value = modelo.BS_MODELO };
            bdParameters[11] = new OracleParameter("P_BS_AÑO_FABRICACION", OracleDbType.Varchar2) { Value = modelo.BS_AÑO_FABRICACION };
            bdParameters[12] = new OracleParameter("P_BS_COMBUSTIBLE", OracleDbType.Varchar2) { Value = modelo.BS_COMBUSTIBLE };
            bdParameters[13] = new OracleParameter("P_BS_TECNOLOGIA_EURO", OracleDbType.Varchar2) { Value = modelo.BS_COMBUSTIBLE };
            bdParameters[14] = new OracleParameter("P_BS_POTENCIA_MOTOR", OracleDbType.Varchar2) { Value = modelo.BS_POTENCIA_MOTOR };
            bdParameters[15] = new OracleParameter("P_BS_SERIE_MOTOR", OracleDbType.Varchar2) { Value = modelo.BS_SERIE_MOTOR };
            bdParameters[16] = new OracleParameter("P_BS_SERIE_CHASIS", OracleDbType.Varchar2) { Value = modelo.BS_SERIE_CHASIS };
            bdParameters[17] = new OracleParameter("P_BS_COLOR_VEHICULO", OracleDbType.Varchar2) { Value = modelo.BS_COLOR_VEHICULO };
            bdParameters[18] = new OracleParameter("P_BS_LONGITUD", OracleDbType.Varchar2) { Value = modelo.BS_LONGITUD };
            bdParameters[19] = new OracleParameter("P_BS_ASIENTOS", OracleDbType.Varchar2) { Value = modelo.BS_ASIENTOS };
            bdParameters[20] = new OracleParameter("P_BS_AREA_PASILLO", OracleDbType.Varchar2) { Value = modelo.BS_AREA_PASILLO };
            bdParameters[21] = new OracleParameter("P_BS_PESO_NETO", OracleDbType.Varchar2) { Value = modelo.BS_PESO_NETO };
            bdParameters[22] = new OracleParameter("P_BS_PESO_BRUTO", OracleDbType.Varchar2) { Value = modelo.BS_PESO_BRUTO };
            bdParameters[23] = new OracleParameter("P_BS_ALTURA", OracleDbType.Varchar2) { Value = modelo.BS_ALTURA };
            bdParameters[24] = new OracleParameter("P_BS_ANCHO", OracleDbType.Varchar2) { Value = modelo.BS_ANCHO };
            bdParameters[25] = new OracleParameter("P_BS_CARGA_UTIL", OracleDbType.Varchar2) { Value = modelo.BS_CARGA_UTIL };
            bdParameters[26] = new OracleParameter("P_BS_PUERTA_IZQUIERDA", OracleDbType.Varchar2) { Value = modelo.BS_PUERTA_IZQUIERDA };
            bdParameters[27] = new OracleParameter("P_BS_PARTIDA_REGISTRAL", OracleDbType.Varchar2) { Value = modelo.BS_PARTIDA_REGISTRAL };
            bdParameters[28] = new OracleParameter("P_BS_CODIGO_CVS", OracleDbType.Varchar2) { Value = modelo.BS_CODIGO_CVS };
            bdParameters[29] = new OracleParameter("P_BS_CVS_FEC_INIC", OracleDbType.Varchar2) { Value = modelo.BS_CVS_FEC_INIC };
            bdParameters[30] = new OracleParameter("P_BS_CVS_FEC_FIN", OracleDbType.Varchar2) { Value = modelo.BS_CVS_FEC_FIN };
            bdParameters[31] = new OracleParameter("P_BS_SOAT_FEC_INIC", OracleDbType.Varchar2) { Value = modelo.BS_SOAT_FEC_INIC };
            bdParameters[32] = new OracleParameter("P_BS_SOAT_FEC_FIN", OracleDbType.Varchar2) { Value = modelo.BS_SOAT_FEC_FIN };
            bdParameters[33] = new OracleParameter("P_BS_POLIZA_VEHICULOS", OracleDbType.Varchar2) { Value = modelo.BS_POLIZA_VEHICULOS };
            bdParameters[34] = new OracleParameter("P_BS_VEHICULOS_INIC", OracleDbType.Varchar2) { Value = modelo.BS_VEHICULOS_INIC };
            bdParameters[35] = new OracleParameter("P_BS_VEHICULOS_FIN", OracleDbType.Varchar2) { Value = modelo.BS_VEHICULOS_FIN };
            bdParameters[36] = new OracleParameter("P_BS_POLIZA_CIVIL", OracleDbType.Varchar2) { Value = modelo.BS_POLIZA_CIVIL };
            bdParameters[37] = new OracleParameter("P_BS_RC_INICIO", OracleDbType.Varchar2) { Value = modelo.BS_RC_INICIO };
            bdParameters[38] = new OracleParameter("P_BS_RC_FIN", OracleDbType.Varchar2) { Value = modelo.BS_RC_FIN };
            bdParameters[39] = new OracleParameter("P_BS_POLIZA_ACCI_COLECTIVOS", OracleDbType.Varchar2) { Value = modelo.BS_POLIZA_ACCI_COLECTIVOS };
            bdParameters[40] = new OracleParameter("P_BS_APCP_INIC", OracleDbType.Varchar2) { Value = modelo.BS_APCP_INIC };
            bdParameters[41] = new OracleParameter("P_BS_APCP_FIN", OracleDbType.Varchar2) { Value = modelo.BS_APCP_FIN };
            bdParameters[42] = new OracleParameter("P_BS_POLIZA_MULTIRIESGOS", OracleDbType.Varchar2) { Value = modelo.BS_POLIZA_MULTIRIESGOS };
            bdParameters[43] = new OracleParameter("P_BS_MULTIRESGOS_INIC", OracleDbType.Varchar2) { Value = modelo.BS_MULTIRESGOS_INIC };
            bdParameters[44] = new OracleParameter("P_BS_MULTIRESGOS_FIN", OracleDbType.Varchar2) { Value = modelo.BS_MULTIRESGOS_FIN };
            bdParameters[45] = new OracleParameter("P_BS_RTV_INIC", OracleDbType.Varchar2) { Value = modelo.BS_RTV_INIC };
            bdParameters[46] = new OracleParameter("P_BS_RTV_FIN", OracleDbType.Varchar2) { Value = modelo.BS_RTV_FIN };
            bdParameters[47] = new OracleParameter("P_BS_REVI_ANUAL_GNV_INIC", OracleDbType.Varchar2) { Value = modelo.BS_REVI_ANUAL_GNV_INIC };
            bdParameters[48] = new OracleParameter("P_BS_REVI_ANUAL_GNV_FIN", OracleDbType.Varchar2) { Value = modelo.BS_REVI_ANUAL_GNV_FIN };
            bdParameters[49] = new OracleParameter("P_BS_REVISION_CILINDROS_GNV_IN", OracleDbType.Varchar2) { Value = modelo.BS_REVISION_CILINDROS_GNV_INIC };
            bdParameters[50] = new OracleParameter("P_BS_REVISION_CILINDROS_GNV_FIN", OracleDbType.Varchar2) { Value = modelo.BS_REVISION_CILINDROS_GNV_FIN };
            bdParameters[51] = new OracleParameter("P_ESTADO_VEHICULO", OracleDbType.Varchar2) { Value = modelo.ESTADO_VEHICULO };
            bdParameters[52] = new OracleParameter("P_PLACA_REEMPLAZADA", OracleDbType.Varchar2) { Value = modelo.PLACA_REEMPLAZADA };
            bdParameters[53] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = modelo.USU_REG };
            bdParameters[54] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[55] = new OracleParameter("P_ID_ESTADO", OracleDbType.Int32) { Value = modelo.ID_ESTADO };
            bdParameters[56] = new OracleParameter("P_BS_PADRON", OracleDbType.Varchar2) { Value = modelo.BS_PADRON };


            try
            {
                using (var bdCmd = new OracleCommand("PKG_BUSES.SP_INSERTAR_BUSES_NUEVOS", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Connection.Open(); 
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
        public void LimpiarTabla()
        {
            try
            {
                using (var bdCmd = new OracleCommand("PKG_BUSES.SP_LIMPIAR_TB_BUS", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
             
            }
        }
        public RPTA_GENERAL anularBus(int idBus, string usuarioAnula)
        {
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_BS_ID", OracleDbType.Int32) { Value = idBus };
            bdParameters[1] = new OracleParameter("P_USU_ANULA", OracleDbType.Varchar2) { Value = usuarioAnula };
            bdParameters[2] = new OracleParameter("P_FECHA_ANULA", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_BUSES.anularBus", conn))
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

        public RPTA_GENERAL desafectarBus(int id_placa_reemplazada, string placa_nueva)
        {
            RPTA_GENERAL r = new RPTA_GENERAL();
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_BS_ID", OracleDbType.Int32) { Value = id_placa_reemplazada };
            bdParameters[1] = new OracleParameter("P_PLACA_REEMPLAZADA", OracleDbType.Varchar2) { Value = placa_nueva };


            try
            {
                using (var bdCmd = new OracleCommand("PKG_BUSES.SP_DESAFECTAR_BUS", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = "Se Actualizo correctamente";
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

        public List<CC_BUSES> Listar_Buses_Desafectados()
        {
            List<CC_BUSES> resultado = new List<CC_BUSES>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_BUSES.listarBusesDesafectados", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_BUSES();
                            if (!DBNull.Value.Equals(bdRd["BS_ID"])) { item.BS_ID = Convert.ToInt32(bdRd["BS_ID"]); }
                            if (!DBNull.Value.Equals(bdRd["ID_CORREDOR"])) { item.ID_CORREDOR = Convert.ToInt32(bdRd["ID_CORREDOR"]); }
                            if (!DBNull.Value.Equals(bdRd["ABREVIATURA"])) { item.ABREVIATURA = bdRd["ABREVIATURA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_FECINI_PT"])) { item.BS_FECINI_PT = bdRd["BS_FECINI_PT"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_NOM_EMPRE"])) { item.BS_NOM_EMPRE = bdRd["BS_NOM_EMPRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PLACA"])) { item.BS_PLACA = bdRd["BS_PLACA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PROPIETARIO"])) { item.BS_PROPIETARIO = bdRd["BS_PROPIETARIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PAQUETE_CONCESION"])) { item.BS_PAQUETE_CONCESION = bdRd["BS_PAQUETE_CONCESION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PAQUETE_SERVICIO"])) { item.BS_PAQUETE_SERVICIO = bdRd["BS_PAQUETE_SERVICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_TIPO_SERVICIO"])) { item.BS_TIPO_SERVICIO = bdRd["BS_TIPO_SERVICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_ESTADO"])) { item.BS_ESTADO = bdRd["BS_ESTADO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_MARCA"])) { item.BS_MARCA = bdRd["BS_MARCA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_MODELO"])) { item.BS_MODELO = bdRd["BS_MODELO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_AÑO_FABRICACION"])) { item.BS_AÑO_FABRICACION = bdRd["BS_AÑO_FABRICACION"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_COMBUSTIBLE"])) { item.BS_COMBUSTIBLE = bdRd["BS_COMBUSTIBLE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_TECNOLOGIA_EURO"])) { item.BS_TECNOLOGIA_EURO = bdRd["BS_TECNOLOGIA_EURO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_POTENCIA_MOTOR"])) { item.BS_POTENCIA_MOTOR = bdRd["BS_POTENCIA_MOTOR"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_SERIE_MOTOR"])) { item.BS_SERIE_MOTOR = bdRd["BS_SERIE_MOTOR"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_SERIE_CHASIS"])) { item.BS_SERIE_CHASIS = bdRd["BS_SERIE_CHASIS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_COLOR_VEHICULO"])) { item.BS_COLOR_VEHICULO = bdRd["BS_COLOR_VEHICULO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_LONGITUD"])) { item.BS_LONGITUD = bdRd["BS_LONGITUD"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_ASIENTOS"])) { item.BS_ASIENTOS = bdRd["BS_ASIENTOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_AREA_PASILLO"])) { item.BS_AREA_PASILLO = bdRd["BS_AREA_PASILLO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PESO_NETO"])) { item.BS_PESO_NETO = bdRd["BS_PESO_NETO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PESO_BRUTO"])) { item.BS_PESO_BRUTO = bdRd["BS_PESO_BRUTO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_ALTURA"])) { item.BS_ALTURA = bdRd["BS_ALTURA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_ANCHO"])) { item.BS_ANCHO = bdRd["BS_ANCHO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_CARGA_UTIL"])) { item.BS_CARGA_UTIL = bdRd["BS_CARGA_UTIL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PUERTA_IZQUIERDA"])) { item.BS_PUERTA_IZQUIERDA = bdRd["BS_PUERTA_IZQUIERDA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_PARTIDA_REGISTRAL"])) { item.BS_PARTIDA_REGISTRAL = bdRd["BS_PARTIDA_REGISTRAL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_CODIGO_CVS"])) { item.BS_CODIGO_CVS = bdRd["BS_CODIGO_CVS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_CVS_FEC_INIC"])) { item.BS_CVS_FEC_INIC = bdRd["BS_CVS_FEC_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_CVS_FEC_FIN"])) { item.BS_CVS_FEC_FIN = bdRd["BS_CVS_FEC_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_SOAT_FEC_INIC"])) { item.BS_SOAT_FEC_INIC = bdRd["BS_SOAT_FEC_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_SOAT_FEC_FIN"])) { item.BS_SOAT_FEC_FIN = bdRd["BS_SOAT_FEC_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_POLIZA_VEHICULOS"])) { item.BS_POLIZA_VEHICULOS = bdRd["BS_POLIZA_VEHICULOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_VEHICULOS_INIC"])) { item.BS_VEHICULOS_INIC = bdRd["BS_VEHICULOS_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_VEHICULOS_FIN"])) { item.BS_VEHICULOS_FIN = bdRd["BS_VEHICULOS_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_POLIZA_CIVIL"])) { item.BS_POLIZA_CIVIL = bdRd["BS_POLIZA_CIVIL"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_RC_INICIO"])) { item.BS_RC_INICIO = bdRd["BS_RC_INICIO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_RC_FIN"])) { item.BS_RC_FIN = bdRd["BS_RC_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_POLIZA_ACCI_COLECTIVOS"])) { item.BS_POLIZA_ACCI_COLECTIVOS = bdRd["BS_POLIZA_ACCI_COLECTIVOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_APCP_INIC"])) { item.BS_APCP_INIC = bdRd["BS_APCP_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_APCP_FIN"])) { item.BS_APCP_FIN = bdRd["BS_APCP_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_POLIZA_MULTIRIESGOS"])) { item.BS_POLIZA_MULTIRIESGOS = bdRd["BS_POLIZA_MULTIRIESGOS"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_MULTIRESGOS_INIC"])) { item.BS_MULTIRESGOS_INIC = bdRd["BS_MULTIRESGOS_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_MULTIRESGOS_FIN"])) { item.BS_MULTIRESGOS_FIN = bdRd["BS_MULTIRESGOS_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_RTV_INIC"])) { item.BS_RTV_INIC = bdRd["BS_RTV_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_RTV_FIN"])) { item.BS_RTV_FIN = bdRd["BS_RTV_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_REVI_ANUAL_GNV_INIC"])) { item.BS_REVI_ANUAL_GNV_INIC = bdRd["BS_REVI_ANUAL_GNV_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_REVI_ANUAL_GNV_FIN"])) { item.BS_REVI_ANUAL_GNV_FIN = bdRd["BS_REVI_ANUAL_GNV_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_REVISION_CILINDROS_GNV_INIC"])) { item.BS_REVISION_CILINDROS_GNV_INIC = bdRd["BS_REVISION_CILINDROS_GNV_INIC"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_REVISION_CILINDROS_GNV_FIN"])) { item.BS_REVISION_CILINDROS_GNV_FIN = bdRd["BS_REVISION_CILINDROS_GNV_FIN"].ToString(); }
                            //if (!DBNull.Value.Equals(bdRd["COD_CAC"])) { item.COD_CAC = bdRd["COD_CAC"].ToString(); }
                            //if (!DBNull.Value.Equals(bdRd["ESTADO_VEHICULO"])) { item.ESTADO_VEHICULO = bdRd["ESTADO_VEHICULO"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["PLACA_REEMPLAZADA"])) { item.PLACA_REEMPLAZADA = bdRd["PLACA_REEMPLAZADA"].ToString(); }
                            //if (!DBNull.Value.Equals(bdRd["USUREG"])) { item.USUREG = bdRd["USUREG"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_ESTADO"])) { item.ID_ESTADO = Convert.ToInt32(bdRd["ID_ESTADO"]); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public RPTA_GENERAL Insertar_Buses_Afectados(CC_BUSES modelo)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[57];
            //bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            //bdParameters[0] = new OracleParameter("BS_ID", OracleDbType.Int32) { Value = modelo.BS_ID };
            bdParameters[0] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = modelo.ID_CORREDOR };
            bdParameters[1] = new OracleParameter("P_BS_FECINI_PT", OracleDbType.Varchar2) { Value = modelo.BS_FECINI_PT };
            bdParameters[2] = new OracleParameter("P_BS_NOM_EMPRE", OracleDbType.Varchar2) { Value = modelo.BS_NOM_EMPRE };
            bdParameters[3] = new OracleParameter("P_BS_PLACA", OracleDbType.Varchar2) { Value = modelo.BS_PLACA };
            bdParameters[4] = new OracleParameter("P_BS_PROPIETARIO", OracleDbType.Varchar2) { Value = modelo.BS_PROPIETARIO };
            bdParameters[5] = new OracleParameter("P_BS_PAQUETE_CONCESION", OracleDbType.Varchar2) { Value = modelo.BS_PAQUETE_CONCESION };
            bdParameters[6] = new OracleParameter("P_BS_PAQUETE_SERVICIO", OracleDbType.Varchar2) { Value = modelo.BS_PAQUETE_SERVICIO };
            bdParameters[7] = new OracleParameter("P_BS_TIPO_SERVICIO", OracleDbType.Varchar2) { Value = modelo.BS_TIPO_SERVICIO };
            bdParameters[8] = new OracleParameter("P_BS_ESTADO", OracleDbType.Varchar2) { Value = modelo.BS_ESTADO };
            bdParameters[9] = new OracleParameter("P_BS_MARCA", OracleDbType.Varchar2) { Value = modelo.BS_MARCA };
            bdParameters[10] = new OracleParameter("P_BS_MODELO", OracleDbType.Varchar2) { Value = modelo.BS_MODELO };
            bdParameters[11] = new OracleParameter("P_BS_AÑO_FABRICACION", OracleDbType.Varchar2) { Value = modelo.BS_AÑO_FABRICACION };
            bdParameters[12] = new OracleParameter("P_BS_COMBUSTIBLE", OracleDbType.Varchar2) { Value = modelo.BS_COMBUSTIBLE };
            bdParameters[13] = new OracleParameter("P_BS_TECNOLOGIA_EURO", OracleDbType.Varchar2) { Value = modelo.BS_COMBUSTIBLE };
            bdParameters[14] = new OracleParameter("P_BS_POTENCIA_MOTOR", OracleDbType.Varchar2) { Value = modelo.BS_POTENCIA_MOTOR };
            bdParameters[15] = new OracleParameter("P_BS_SERIE_MOTOR", OracleDbType.Varchar2) { Value = modelo.BS_SERIE_MOTOR };
            bdParameters[16] = new OracleParameter("P_BS_SERIE_CHASIS", OracleDbType.Varchar2) { Value = modelo.BS_SERIE_CHASIS };
            bdParameters[17] = new OracleParameter("P_BS_COLOR_VEHICULO", OracleDbType.Varchar2) { Value = modelo.BS_COLOR_VEHICULO };
            bdParameters[18] = new OracleParameter("P_BS_LONGITUD", OracleDbType.Varchar2) { Value = modelo.BS_LONGITUD };
            bdParameters[19] = new OracleParameter("P_BS_ASIENTOS", OracleDbType.Varchar2) { Value = modelo.BS_ASIENTOS };
            bdParameters[20] = new OracleParameter("P_BS_AREA_PASILLO", OracleDbType.Varchar2) { Value = modelo.BS_AREA_PASILLO };
            bdParameters[21] = new OracleParameter("P_BS_PESO_NETO", OracleDbType.Varchar2) { Value = modelo.BS_PESO_NETO };
            bdParameters[22] = new OracleParameter("P_BS_PESO_BRUTO", OracleDbType.Varchar2) { Value = modelo.BS_PESO_BRUTO };
            bdParameters[23] = new OracleParameter("P_BS_ALTURA", OracleDbType.Varchar2) { Value = modelo.BS_ALTURA };
            bdParameters[24] = new OracleParameter("P_BS_ANCHO", OracleDbType.Varchar2) { Value = modelo.BS_ANCHO };
            bdParameters[25] = new OracleParameter("P_BS_CARGA_UTIL", OracleDbType.Varchar2) { Value = modelo.BS_CARGA_UTIL };
            bdParameters[26] = new OracleParameter("P_BS_PUERTA_IZQUIERDA", OracleDbType.Varchar2) { Value = modelo.BS_PUERTA_IZQUIERDA };
            bdParameters[27] = new OracleParameter("P_BS_PARTIDA_REGISTRAL", OracleDbType.Varchar2) { Value = modelo.BS_PARTIDA_REGISTRAL };
            bdParameters[28] = new OracleParameter("P_BS_CODIGO_CVS", OracleDbType.Varchar2) { Value = modelo.BS_CODIGO_CVS };
            bdParameters[29] = new OracleParameter("P_BS_CVS_FEC_INIC", OracleDbType.Varchar2) { Value = modelo.BS_CVS_FEC_INIC };
            bdParameters[30] = new OracleParameter("P_BS_CVS_FEC_FIN", OracleDbType.Varchar2) { Value = modelo.BS_CVS_FEC_FIN };
            bdParameters[31] = new OracleParameter("P_BS_SOAT_FEC_INIC", OracleDbType.Varchar2) { Value = modelo.BS_SOAT_FEC_INIC };
            bdParameters[32] = new OracleParameter("P_BS_SOAT_FEC_FIN", OracleDbType.Varchar2) { Value = modelo.BS_SOAT_FEC_FIN };
            bdParameters[33] = new OracleParameter("P_BS_POLIZA_VEHICULOS", OracleDbType.Varchar2) { Value = modelo.BS_POLIZA_VEHICULOS };
            bdParameters[34] = new OracleParameter("P_BS_VEHICULOS_INIC", OracleDbType.Varchar2) { Value = modelo.BS_VEHICULOS_INIC };
            bdParameters[35] = new OracleParameter("P_BS_VEHICULOS_FIN", OracleDbType.Varchar2) { Value = modelo.BS_VEHICULOS_FIN };
            bdParameters[36] = new OracleParameter("P_BS_POLIZA_CIVIL", OracleDbType.Varchar2) { Value = modelo.BS_POLIZA_CIVIL };
            bdParameters[37] = new OracleParameter("P_BS_RC_INICIO", OracleDbType.Varchar2) { Value = modelo.BS_RC_INICIO };
            bdParameters[38] = new OracleParameter("P_BS_RC_FIN", OracleDbType.Varchar2) { Value = modelo.BS_RC_FIN };
            bdParameters[39] = new OracleParameter("P_BS_POLIZA_ACCI_COLECTIVOS", OracleDbType.Varchar2) { Value = modelo.BS_POLIZA_ACCI_COLECTIVOS };
            bdParameters[40] = new OracleParameter("P_BS_APCP_INIC", OracleDbType.Varchar2) { Value = modelo.BS_APCP_INIC };
            bdParameters[41] = new OracleParameter("P_BS_APCP_FIN", OracleDbType.Varchar2) { Value = modelo.BS_APCP_FIN };
            bdParameters[42] = new OracleParameter("P_BS_POLIZA_MULTIRIESGOS", OracleDbType.Varchar2) { Value = modelo.BS_POLIZA_MULTIRIESGOS };
            bdParameters[43] = new OracleParameter("P_BS_MULTIRESGOS_INIC", OracleDbType.Varchar2) { Value = modelo.BS_MULTIRESGOS_INIC };
            bdParameters[44] = new OracleParameter("P_BS_MULTIRESGOS_FIN", OracleDbType.Varchar2) { Value = modelo.BS_MULTIRESGOS_FIN };
            bdParameters[45] = new OracleParameter("P_BS_RTV_INIC", OracleDbType.Varchar2) { Value = modelo.BS_RTV_INIC };
            bdParameters[46] = new OracleParameter("P_BS_RTV_FIN", OracleDbType.Varchar2) { Value = modelo.BS_RTV_FIN };
            bdParameters[47] = new OracleParameter("P_BS_REVI_ANUAL_GNV_INIC", OracleDbType.Varchar2) { Value = modelo.BS_REVI_ANUAL_GNV_INIC };
            bdParameters[48] = new OracleParameter("P_BS_REVI_ANUAL_GNV_FIN", OracleDbType.Varchar2) { Value = modelo.BS_REVI_ANUAL_GNV_FIN };
            bdParameters[49] = new OracleParameter("P_BS_REVISION_CILINDROS_GNV_IN", OracleDbType.Varchar2) { Value = modelo.BS_REVISION_CILINDROS_GNV_INIC };
            bdParameters[50] = new OracleParameter("P_BS_REVISION_CILINDROS_GNV_FIN", OracleDbType.Varchar2) { Value = modelo.BS_REVISION_CILINDROS_GNV_FIN };
            bdParameters[51] = new OracleParameter("P_ESTADO_VEHICULO", OracleDbType.Varchar2) { Value = modelo.ESTADO_VEHICULO };
            bdParameters[52] = new OracleParameter("P_PLACA_REEMPLAZADA", OracleDbType.Varchar2) { Value = modelo.PLACA_REEMPLAZADA };
            bdParameters[53] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = modelo.USU_REG };
            bdParameters[54] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };
            bdParameters[55] = new OracleParameter("P_ID_ESTADO", OracleDbType.Int32) { Value = modelo.ID_ESTADO };
            bdParameters[56] = new OracleParameter("P_PLACA_", OracleDbType.Varchar2, 50);
            bdParameters[56].Direction = ParameterDirection.Output;


            try
            {
                using (var bdCmd = new OracleCommand("PKG_BUSES.SP_INSERT_BUS_RETURN_PLACA", conn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(bdParameters);
                    bdCmd.ExecuteNonQuery();
                    var placa = bdCmd.Parameters["P_PLACA_"].Value.ToString();

                    r.AUX = 1;
                    r.COD_ESTADO = 1;
                    r.DES_ESTADO = placa;
                }
            }
            catch (Exception ex)
            {
                r.COD_ESTADO = 0;
                r.DES_ESTADO = ex.Message;
            }
            return r;
        }




        public RPTA_GENERAL Modificar_Bus(CC_BUSES modelo)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[54];
            //bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");


            bdParameters[0] = new OracleParameter("P_BS_ID", OracleDbType.Int32) { Value = modelo.BS_ID };
            bdParameters[1] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = modelo.ID_CORREDOR };
            bdParameters[2] = new OracleParameter("P_BS_FECINI_PT", OracleDbType.Varchar2) { Value = modelo.BS_FECINI_PT };
            bdParameters[3] = new OracleParameter("P_BS_NOM_EMPRE", OracleDbType.Varchar2) { Value = modelo.BS_NOM_EMPRE };
            bdParameters[4] = new OracleParameter("P_BS_PLACA", OracleDbType.Varchar2) { Value = modelo.BS_PLACA };
            bdParameters[5] = new OracleParameter("P_BS_PROPIETARIO", OracleDbType.Varchar2) { Value = modelo.BS_PROPIETARIO };
            bdParameters[6] = new OracleParameter("P_BS_PAQUETE_CONCESION", OracleDbType.Varchar2) { Value = modelo.BS_PAQUETE_CONCESION };
            bdParameters[7] = new OracleParameter("P_BS_PAQUETE_SERVICIO", OracleDbType.Varchar2) { Value = modelo.BS_PAQUETE_SERVICIO };
            bdParameters[8] = new OracleParameter("P_BS_TIPO_SERVICIO", OracleDbType.Varchar2) { Value = modelo.BS_TIPO_SERVICIO };
            bdParameters[9] = new OracleParameter("P_BS_ESTADO", OracleDbType.Varchar2) { Value = modelo.BS_ESTADO };
            bdParameters[10] = new OracleParameter("P_BS_MARCA", OracleDbType.Varchar2) { Value = modelo.BS_MARCA };
            bdParameters[11] = new OracleParameter("P_BS_MODELO", OracleDbType.Varchar2) { Value = modelo.BS_MODELO };
            bdParameters[12] = new OracleParameter("P_BS_AÑO_FABRICACION", OracleDbType.Varchar2) { Value = modelo.BS_AÑO_FABRICACION };
            bdParameters[13] = new OracleParameter("P_BS_COMBUSTIBLE", OracleDbType.Varchar2) { Value = modelo.BS_COMBUSTIBLE };
            bdParameters[14] = new OracleParameter("P_BS_TECNOLOGIA_EURO", OracleDbType.Varchar2) { Value = modelo.BS_COMBUSTIBLE };
            bdParameters[15] = new OracleParameter("P_BS_POTENCIA_MOTOR", OracleDbType.Varchar2) { Value = modelo.BS_POTENCIA_MOTOR };
            bdParameters[16] = new OracleParameter("P_BS_SERIE_MOTOR", OracleDbType.Varchar2) { Value = modelo.BS_SERIE_MOTOR };
            bdParameters[17] = new OracleParameter("P_BS_SERIE_CHASIS", OracleDbType.Varchar2) { Value = modelo.BS_SERIE_CHASIS };
            bdParameters[18] = new OracleParameter("P_BS_COLOR_VEHICULO", OracleDbType.Varchar2) { Value = modelo.BS_COLOR_VEHICULO };
            bdParameters[19] = new OracleParameter("P_BS_LONGITUD", OracleDbType.Varchar2) { Value = modelo.BS_LONGITUD };
            bdParameters[20] = new OracleParameter("P_BS_ASIENTOS", OracleDbType.Varchar2) { Value = modelo.BS_ASIENTOS };
            bdParameters[21] = new OracleParameter("P_BS_AREA_PASILLO", OracleDbType.Varchar2) { Value = modelo.BS_AREA_PASILLO };
            bdParameters[22] = new OracleParameter("P_BS_PESO_NETO", OracleDbType.Varchar2) { Value = modelo.BS_PESO_NETO };
            bdParameters[23] = new OracleParameter("P_BS_PESO_BRUTO", OracleDbType.Varchar2) { Value = modelo.BS_PESO_BRUTO };
            bdParameters[24] = new OracleParameter("P_BS_ALTURA", OracleDbType.Varchar2) { Value = modelo.BS_ALTURA };
            bdParameters[25] = new OracleParameter("P_BS_ANCHO", OracleDbType.Varchar2) { Value = modelo.BS_ANCHO };
            bdParameters[26] = new OracleParameter("P_BS_CARGA_UTIL", OracleDbType.Varchar2) { Value = modelo.BS_CARGA_UTIL };
            bdParameters[27] = new OracleParameter("P_BS_PUERTA_IZQUIERDA", OracleDbType.Varchar2) { Value = modelo.BS_PUERTA_IZQUIERDA };
            bdParameters[28] = new OracleParameter("P_BS_PARTIDA_REGISTRAL", OracleDbType.Varchar2) { Value = modelo.BS_PARTIDA_REGISTRAL };
            bdParameters[29] = new OracleParameter("P_BS_CODIGO_CVS", OracleDbType.Varchar2) { Value = modelo.BS_CODIGO_CVS };
            bdParameters[30] = new OracleParameter("P_BS_CVS_FEC_INIC", OracleDbType.Varchar2) { Value = modelo.BS_CVS_FEC_INIC };
            bdParameters[31] = new OracleParameter("P_BS_CVS_FEC_FIN", OracleDbType.Varchar2) { Value = modelo.BS_CVS_FEC_FIN };
            bdParameters[32] = new OracleParameter("P_BS_SOAT_FEC_INIC", OracleDbType.Varchar2) { Value = modelo.BS_SOAT_FEC_INIC };
            bdParameters[33] = new OracleParameter("P_BS_SOAT_FEC_FIN", OracleDbType.Varchar2) { Value = modelo.BS_SOAT_FEC_FIN };
            bdParameters[34] = new OracleParameter("P_BS_POLIZA_VEHICULOS", OracleDbType.Varchar2) { Value = modelo.BS_POLIZA_VEHICULOS };
            bdParameters[35] = new OracleParameter("P_BS_VEHICULOS_INIC", OracleDbType.Varchar2) { Value = modelo.BS_VEHICULOS_INIC };
            bdParameters[36] = new OracleParameter("P_BS_VEHICULOS_FIN", OracleDbType.Varchar2) { Value = modelo.BS_VEHICULOS_FIN };
            bdParameters[37] = new OracleParameter("P_BS_POLIZA_CIVIL", OracleDbType.Varchar2) { Value = modelo.BS_POLIZA_CIVIL };
            bdParameters[38] = new OracleParameter("P_BS_RC_INICIO", OracleDbType.Varchar2) { Value = modelo.BS_RC_INICIO };
            bdParameters[39] = new OracleParameter("P_BS_RC_FIN", OracleDbType.Varchar2) { Value = modelo.BS_RC_FIN };
            bdParameters[40] = new OracleParameter("P_BS_POLIZA_ACCI_COLECTIVOS", OracleDbType.Varchar2) { Value = modelo.BS_POLIZA_ACCI_COLECTIVOS };
            bdParameters[41] = new OracleParameter("P_BS_APCP_INIC", OracleDbType.Varchar2) { Value = modelo.BS_APCP_INIC };
            bdParameters[42] = new OracleParameter("P_BS_APCP_FIN", OracleDbType.Varchar2) { Value = modelo.BS_APCP_FIN };
            bdParameters[43] = new OracleParameter("P_BS_POLIZA_MULTIRIESGOS", OracleDbType.Varchar2) { Value = modelo.BS_POLIZA_MULTIRIESGOS };
            bdParameters[44] = new OracleParameter("P_BS_MULTIRESGOS_INIC", OracleDbType.Varchar2) { Value = modelo.BS_MULTIRESGOS_INIC };
            bdParameters[45] = new OracleParameter("P_BS_MULTIRESGOS_FIN", OracleDbType.Varchar2) { Value = modelo.BS_MULTIRESGOS_FIN };
            bdParameters[46] = new OracleParameter("P_BS_RTV_INIC", OracleDbType.Varchar2) { Value = modelo.BS_RTV_INIC };
            bdParameters[47] = new OracleParameter("P_BS_RTV_FIN", OracleDbType.Varchar2) { Value = modelo.BS_RTV_FIN };
            bdParameters[48] = new OracleParameter("P_BS_REVI_ANUAL_GNV_INIC", OracleDbType.Varchar2) { Value = modelo.BS_REVI_ANUAL_GNV_INIC };
            bdParameters[49] = new OracleParameter("P_BS_REVI_ANUAL_GNV_FIN", OracleDbType.Varchar2) { Value = modelo.BS_REVI_ANUAL_GNV_FIN };
            bdParameters[50] = new OracleParameter("P_BS_REVISION_CILINDROS_GNV_IN", OracleDbType.Varchar2) { Value = modelo.BS_REVISION_CILINDROS_GNV_INIC };
            bdParameters[51] = new OracleParameter("P_BS_REVISION_CILINDROS_GNV_FIN", OracleDbType.Varchar2) { Value = modelo.BS_REVISION_CILINDROS_GNV_FIN };
            //bdParameters[52] = new OracleParameter("P_ESTADO_VEHICULO", OracleDbType.Varchar2) { Value = modelo.ESTADO_VEHICULO };
            bdParameters[52] = new OracleParameter("P_USU_MODIF", OracleDbType.Varchar2) { Value = modelo.USU_MODIF };
            bdParameters[53] = new OracleParameter("P_FECHA_MODIF", OracleDbType.Varchar2) { Value = fecha_actual };



            try
            {
                using (var bdCmd = new OracleCommand("PKG_BUSES.SP_EDITAR_BUSES", conn))
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




        public RPTA_GENERAL Verifica_Placa_Existente(string placa)
        {

            var item = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[2];

            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_BS_PLACA", OracleDbType.Varchar2) { Value = placa };

            using (var bdCmd = new OracleCommand("PKG_BUSES.RPTA_PLACA_EXISTENTE", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);

                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {

                            if (!DBNull.Value.Equals(bdRd["AUX"])) { item.AUX = Convert.ToInt32(bdRd["AUX"]); }

                        }
                    }
                }
            }
            return item;
        }

        //public CC_BUSES Estado_Vigencias(CC_BUSES model)
        //{
        //    var bdParameters = new OracleDynamicParameters();
        //    var query = "SELECT BS_PLACA,BS_NOM_EMPRE ,BS_CVS_FEC_FIN ,BS_VEHICULOS_FIN ,BS_RTV_FIN ,BS_SOAT_FEC_FIN,BS_RC_FIN  FROM CC_BUSES WHERE BS_PLACA='" + model.BS_PLACA + "'";
        //    var result = SqlMapper.QueryFirst<CC_BUSES>(conn, query);
        //    return result;
        //}

        public CC_BUSES Estado_Vigencias(string placa)
        {
            var item = new CC_BUSES();

            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[1] = new OracleParameter("P_BS_PLACA", OracleDbType.Varchar2) { Value = placa };

            using (var bdCmd = new OracleCommand("PKG_BUSES.SP_ESTADO_VIGENCIAS", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            if (!DBNull.Value.Equals(bdRd["BS_PLACA"])) { item.BS_PLACA = bdRd["BS_PLACA"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_NOM_EMPRE"])) { item.BS_NOM_EMPRE = bdRd["BS_NOM_EMPRE"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_CVS_FEC_FIN"])) { item.BS_CVS_FEC_FIN = bdRd["BS_CVS_FEC_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_VEHICULOS_FIN"])) { item.BS_VEHICULOS_FIN = bdRd["BS_VEHICULOS_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_RTV_FIN"])) { item.BS_RTV_FIN = bdRd["BS_RTV_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_SOAT_FEC_FIN"])) { item.BS_SOAT_FEC_FIN = bdRd["BS_SOAT_FEC_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["BS_RC_FIN"])) { item.BS_RC_FIN = bdRd["BS_RC_FIN"].ToString(); }
                            if (!DBNull.Value.Equals(bdRd["ID_CORREDOR"])) { item.ID_CORREDOR =Convert.ToInt32(bdRd["ID_CORREDOR"]); }

                            
                        }
                    }
                }
            }
            return item;
        }

        //public BUSES_DESPACHO Ultimo_Codigo_Archivo(string placa)
        //{
        //    var bdParameters = new OracleDynamicParameters();
        //    var query = "select  * from buses_despacho  where bs_placa='" + placa + "' ORDER BY BD_COD_DESPACHO DESC";
        //    var result = SqlMapper.QueryFirst<BUSES_DESPACHO>(conn, query);
        //    return result;
        //}

      


        //public void Insertar_Buses_Despacho(int BD_ID, int CD_ID, string BS_PLACA, string BD_ESTADO, string BD_CALIDAD, string BD_OBSERVACION, string BD_DIRECCION, int? BD_KM, string BD_CONCESIONARIO, string BD_FECHA, string USUARIO, int ESTADO_DATO, string longuitud, string latitud, int cod_despacho, string url_foto, string url_foto2, string url_foto3, string url_foto4, string estado_bus, string comentario)
        //{
        //    var bdParameters = new OracleDynamicParameters();
        //    if (BD_CALIDAD == null || BD_CALIDAD == "NO TIENE") BD_CALIDAD = " ";
        //    if (BD_ESTADO == "NO TIENE") BD_ESTADO = " ";

        //    var query = "PKG_BUSES.SP_INSERTAR_FORMATO_DESPACHO";
        //    bdParameters.Add("BD_ID", OracleDbType.Int32, ParameterDirection.Input, BD_ID);
        //    bdParameters.Add("CD_ID", OracleDbType.Int32, ParameterDirection.Input, CD_ID);
        //    bdParameters.Add("BS_PLACA", OracleDbType.Varchar2, ParameterDirection.Input, BS_PLACA);
        //    bdParameters.Add("BD_ESTADO", OracleDbType.Varchar2, ParameterDirection.Input, BD_ESTADO);
        //    bdParameters.Add("BD_CALIDAD", OracleDbType.Varchar2, ParameterDirection.Input, BD_CALIDAD);
        //    bdParameters.Add("BD_OBSERVACION", OracleDbType.Varchar2, ParameterDirection.Input, BD_OBSERVACION);
        //    bdParameters.Add("BD_DIRECCION", OracleDbType.Varchar2, ParameterDirection.Input, BD_DIRECCION);
        //    bdParameters.Add("BD_KM", OracleDbType.Varchar2, ParameterDirection.Input, BD_KM);
        //    bdParameters.Add("BD_CONCESIONARIO", OracleDbType.Varchar2, ParameterDirection.Input, BD_CONCESIONARIO);
        //    bdParameters.Add("USUREG", OracleDbType.Varchar2, ParameterDirection.Input, USUARIO);
        //    bdParameters.Add("ESTREG", OracleDbType.Int32, ParameterDirection.Input, ESTADO_DATO);
        //    bdParameters.Add("BD_LATITUD", OracleDbType.Varchar2, ParameterDirection.Input, latitud);
        //    bdParameters.Add("BD_LONGUITUD", OracleDbType.Varchar2, ParameterDirection.Input, longuitud);
        //    bdParameters.Add("BD_COD_DESPACHO", OracleDbType.Int32, ParameterDirection.Input, cod_despacho);
        //    bdParameters.Add("BD_URL_FOTO_", OracleDbType.Varchar2, ParameterDirection.Input, url_foto);
        //    bdParameters.Add("BD_URL_FOTO2_", OracleDbType.Varchar2, ParameterDirection.Input, url_foto2);
        //    bdParameters.Add("BD_URL_FOTO3_", OracleDbType.Varchar2, ParameterDirection.Input, url_foto3);
        //    bdParameters.Add("BD_URL_FOTO4_", OracleDbType.Varchar2, ParameterDirection.Input, url_foto4);
        //    bdParameters.Add("ESTADO_BUS_", OracleDbType.Varchar2, ParameterDirection.Input, estado_bus);
        //    bdParameters.Add("COMENTARIO_", OracleDbType.Varchar2, ParameterDirection.Input, comentario);

        //    SqlMapper.QueryFirstOrDefault(conn, query, param: bdParameters, commandType: CommandType.StoredProcedure);
        //}

        public RPTA_GENERAL Insertar_Buses_Despacho(int BD_ID, int CD_ID, string BS_PLACA, string BD_ESTADO, string BD_CALIDAD, string BD_OBSERVACION, string BD_DIRECCION, int? BD_KM, string BD_CONCESIONARIO, string BD_FECHA, string USUARIO, int ESTADO_DATO, string longuitud, string latitud, int cod_despacho, string url_foto, string url_foto2, string url_foto3, string url_foto4, string estado_bus, string comentario,int id_corredor)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[21];

            bdParameters[0] = new OracleParameter("BD_ID", OracleDbType.Int32) { Value = BD_ID };
            bdParameters[1] = new OracleParameter("CD_ID", OracleDbType.Int32) { Value = CD_ID };
            bdParameters[2] = new OracleParameter("BS_PLACA", OracleDbType.Varchar2) { Value = BS_PLACA };
            bdParameters[3] = new OracleParameter("BD_ESTADO", OracleDbType.Varchar2) { Value = BD_ESTADO };
            bdParameters[4] = new OracleParameter("BD_CALIDAD", OracleDbType.Varchar2) { Value = BD_CALIDAD };
            bdParameters[5] = new OracleParameter("BD_OBSERVACION", OracleDbType.Varchar2) { Value = BD_OBSERVACION };
            bdParameters[6] = new OracleParameter("BD_DIRECCION", OracleDbType.Varchar2) { Value = BD_DIRECCION };
            bdParameters[7] = new OracleParameter("BD_KM", OracleDbType.Varchar2) { Value = BD_KM };
            bdParameters[8] = new OracleParameter("BD_CONCESIONARIO", OracleDbType.Varchar2) { Value = BD_CONCESIONARIO };
            bdParameters[9] = new OracleParameter("USUREG", OracleDbType.Varchar2) { Value = USUARIO };
            bdParameters[10] = new OracleParameter("ESTREG", OracleDbType.Varchar2) { Value = ESTADO_DATO };
            bdParameters[11] = new OracleParameter("BD_LATITUD", OracleDbType.Varchar2) { Value = latitud };
            bdParameters[12] = new OracleParameter("BD_LONGUITUD", OracleDbType.Varchar2) { Value = longuitud };
            bdParameters[13] = new OracleParameter("BD_COD_DESPACHO", OracleDbType.Varchar2) { Value = cod_despacho };
            bdParameters[14] = new OracleParameter("BD_URL_FOTO_", OracleDbType.Varchar2) { Value = url_foto };
            bdParameters[15] = new OracleParameter("BD_URL_FOTO2_", OracleDbType.Varchar2) { Value = url_foto2 };
            bdParameters[16] = new OracleParameter("BD_URL_FOTO3_", OracleDbType.Varchar2) { Value = url_foto3 };
            bdParameters[17] = new OracleParameter("BD_URL_FOTO4_", OracleDbType.Varchar2) { Value = url_foto4 };
            bdParameters[18] = new OracleParameter("ESTADO_BUS_", OracleDbType.Varchar2) { Value = estado_bus };
            bdParameters[19] = new OracleParameter("COMENTARIO_", OracleDbType.Varchar2) { Value = comentario };
            bdParameters[20] = new OracleParameter("P_ID_CORREDOR", OracleDbType.Int32) { Value = id_corredor };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_BUSES.SP_INSERTAR_FORMATO_DESPACHO", conn))
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

        //public List<BUSES_DESPACHO> Lista_Conceptos_Dentro()
        //{
        //    var bdParameters = new OracleDynamicParameters();
        //    var query = "SELECT * FROM CONCEPTOS_DESPACHO WHERE CD_ID <7";
        //    var result = SqlMapper.Query<BUSES_DESPACHO>(conn, query).ToList();
        //    return result;
        //}

        public List<BUSES_DESPACHO> Lista_Conceptos_Dentro()
        {
            List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_BUSES.SP_LISTA_CONCEPTOS_DENTRO", conn))
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

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        //public List<BUSES_DESPACHO> Lista_Conceptos_Exterior()
        //{
        //    var bdParameters = new OracleDynamicParameters();
        //    var query = "SELECT * FROM CONCEPTOS_DESPACHO WHERE CD_ID >6 AND CD_ID<39";
        //    var result = SqlMapper.Query<BUSES_DESPACHO>(conn, query).ToList();
        //    return result;
        //}

        //public List<BUSES_DESPACHO> Lista_Conceptos_Exterior()
        //{
        //    List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();

        //    OracleParameter[] bdParameters = new OracleParameter[1];
        //    bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

        //    using (var bdCmd = new OracleCommand("PKG_BUSES.SP_LISTA_CONCEPTOS_EXTERIOR", conn))
        //    {
        //        bdCmd.CommandType = CommandType.StoredProcedure;
        //        bdCmd.Parameters.AddRange(bdParameters);
        //        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
        //        {
        //            if (bdRd.HasRows)
        //            {
        //                while (bdRd.Read())
        //                {
        //                    var item = new BUSES_DESPACHO();
        //                    if (!DBNull.Value.Equals(bdRd["CD_ID"])) { item.CD_ID = Convert.ToInt32(bdRd["CD_ID"]); }
        //                    if (!DBNull.Value.Equals(bdRd["CD_CONCEPTOS"])) { item.CD_CONCEPTOS = bdRd["CD_CONCEPTOS"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["CD_TIPO_DOCUMENTO"])) { item.CD_TIPO_DOCUMENTO = bdRd["CD_TIPO_DOCUMENTO"].ToString(); }

        //                    resultado.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    return resultado;
        //}



        public List<BUSES_DESPACHO> Lista_Conceptos_Exterior()
        {
            List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);


            using (var bdCmd = new OracleCommand("PKG_BUSES.SP_LISTA_CONCEPTOS_EXTERIOR", conn))
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

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }




        //public List<BUSES_DESPACHO> Lista_Cabina_Vehiculo()
        //{
        //    var bdParameters = new OracleDynamicParameters();
        //    var query = "SELECT * FROM CONCEPTOS_DESPACHO WHERE CD_ID>38";
        //    var result = SqlMapper.Query<BUSES_DESPACHO>(conn, query).ToList();
        //    return result;
        //}

        public List<BUSES_DESPACHO> Lista_Cabina_Vehiculo()
        {
            List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_BUSES.SP_LISTA_CABINA_VEHICULO", conn))
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

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        public List<BUSES_DESPACHO> Lista_Revisiones_Mecanicas()
        {
            List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_BUSES.SP_LISTA_REVISION_TECNICA", conn))
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

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }

        

        //public List<BUSES_DESPACHO> Buscar_Documentacion_Bus(string placa, int codigo_despacho)
        //{
        //    var bdParameters = new OracleDynamicParameters();
        //    var query = "PKG_BUSES.SP_BUSCAR_DOCUMENTOS_BUS";

        //    bdParameters.Add("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
        //    bdParameters.Add("BS_PLACA_", OracleDbType.Varchar2, ParameterDirection.Input, placa);
        //    bdParameters.Add("ESTADO_BUS_", OracleDbType.Int32, ParameterDirection.Input, codigo_despacho);


        //    var result = SqlMapper.Query<BUSES_DESPACHO>(conn, query, param: bdParameters, commandType: CommandType.StoredProcedure).ToList();
        //    return result;
        //}

        //public List<BUSES_DESPACHO> Buscar_Documentacion_Bus(string placa, int codigo_despacho)
        //{
        //    List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();

        //    OracleParameter[] bdParameters = new OracleParameter[3];
        //    bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
        //    bdParameters[1] = new OracleParameter("BS_PLACA_", OracleDbType.Varchar2) { Value = placa };
        //    bdParameters[2] = new OracleParameter("ESTADO_BUS_", OracleDbType.Int32) { Value = codigo_despacho };

        //    using (var bdCmd = new OracleCommand("PKG_BUSES.SP_BUSCAR_DOCUMENTOS_BUS", conn))
        //    {
        //        bdCmd.CommandType = CommandType.StoredProcedure;
        //        bdCmd.Parameters.AddRange(bdParameters);
        //        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
        //        {
        //            if (bdRd.HasRows)
        //            {
        //                while (bdRd.Read())
        //                {
        //                    var item = new BUSES_DESPACHO();
        //                    if (!DBNull.Value.Equals(bdRd["BS_PLACA"])) { item.BS_PLACA = bdRd["BS_PLACA"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["CD_ID"])) { item.CD_ID = Convert.ToInt32(bdRd["CD_ID"]); }
        //                    if (!DBNull.Value.Equals(bdRd["CD_CONCEPTOS"])) { item.CD_CONCEPTOS = bdRd["CD_CONCEPTOS"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["BD_ESTADO"])) { item.BD_ESTADO = bdRd["BD_ESTADO"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["BD_OBSERVACION"])) { item.BD_OBSERVACION = bdRd["BD_OBSERVACION"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["CVS_FEC_FIN"])) { item.CVS_FEC_FIN = bdRd["CVS_FEC_FIN"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["VEHICULOS_FIN"])) { item.VEHICULOS_FIN = bdRd["VEHICULOS_FIN"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["RTV_FIN"])) { item.RTV_FIN = bdRd["RTV_FIN"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["SOAT_FEC_FIN"])) { item.SOAT_FEC_FIN = bdRd["SOAT_FEC_FIN"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["RC_FIN"])) { item.RC_FIN = bdRd["RC_FIN"].ToString(); }


        //                    resultado.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    return resultado;
        //}

        //public List<BUSES_DESPACHO> Buscar_Exterior_Bus(string placa, int codigo_despacho)
        //{
        //    var bdParameters = new OracleDynamicParameters();
        //    var query = "PKG_BUSES.SP_BUSCAR_EXTERIOR_BUS";

        //    bdParameters.Add("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
        //    bdParameters.Add("BS_PLACA_", OracleDbType.Varchar2, ParameterDirection.Input, placa);
        //    bdParameters.Add("ESTADO_BUS_", OracleDbType.Int32, ParameterDirection.Input, codigo_despacho);



        //    var result = SqlMapper.Query<BUSES_DESPACHO>(conn, query, param: bdParameters, commandType: CommandType.StoredProcedure).ToList();
        //    return result;
        //}

        //public List<BUSES_DESPACHO> Buscar_Exterior_Bus(string placa, int codigo_despacho)
        //{
        //    List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();

        //    OracleParameter[] bdParameters = new OracleParameter[3];
        //    bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
        //    bdParameters[1] = new OracleParameter("BS_PLACA_", OracleDbType.Varchar2) { Value = placa };
        //    bdParameters[2] = new OracleParameter("ESTADO_BUS_", OracleDbType.Int32) { Value = codigo_despacho };

        //    using (var bdCmd = new OracleCommand("PKG_BUSES.SP_BUSCAR_EXTERIOR_BUS", conn))
        //    {
        //        bdCmd.CommandType = CommandType.StoredProcedure;
        //        bdCmd.Parameters.AddRange(bdParameters);
        //        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
        //        {
        //            if (bdRd.HasRows)
        //            {
        //                while (bdRd.Read())
        //                {
        //                    var item = new BUSES_DESPACHO();
        //                    if (!DBNull.Value.Equals(bdRd["CD_ID"])) { item.CD_ID = Convert.ToInt32(bdRd["CD_ID"]); }
        //                    if (!DBNull.Value.Equals(bdRd["CD_CONCEPTOS"])) { item.CD_CONCEPTOS = bdRd["CD_CONCEPTOS"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["BD_ESTADO"])) { item.BD_ESTADO = bdRd["BD_ESTADO"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["BD_CALIDAD"])) { item.BD_CALIDAD = bdRd["BD_CALIDAD"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["BD_OBSERVACION"])) { item.BD_OBSERVACION = bdRd["BD_OBSERVACION"].ToString(); }
        //                    resultado.Add(item);
        //                }
        //            }
        //        }
        //    }

        //    return resultado;
        //}


        //public List<BUSES_DESPACHO> Buscar_Cabina_Bus(string placa, int codigo_despacho)
        //{
        //    var bdParameters = new OracleDynamicParameters();
        //    var query = "PKG_BUSES.SP_BUSCAR_CABINA_BUS";

        //    bdParameters.Add("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
        //    bdParameters.Add("BS_PLACA_", OracleDbType.Varchar2, ParameterDirection.Input, placa);
        //    bdParameters.Add("ESTADO_BUS_", OracleDbType.Int32, ParameterDirection.Input, codigo_despacho);


        //    var result = SqlMapper.Query<BUSES_DESPACHO>(conn, query, param: bdParameters, commandType: CommandType.StoredProcedure).ToList();
        //    return result;
        //}

        //public List<BUSES_DESPACHO> Buscar_Cabina_Bus(string placa, int codigo_despacho)
        //{
        //    List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();

        //    OracleParameter[] bdParameters = new OracleParameter[3];
        //    bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
        //    bdParameters[1] = new OracleParameter("BS_PLACA_", OracleDbType.Varchar2) { Value = placa };
        //    bdParameters[2] = new OracleParameter("ESTADO_BUS_", OracleDbType.Int32) { Value = codigo_despacho };

        //    using (var bdCmd = new OracleCommand("PKG_BUSES.SP_BUSCAR_CABINA_BUS", conn))
        //    {
        //        bdCmd.CommandType = CommandType.StoredProcedure;
        //        bdCmd.Parameters.AddRange(bdParameters);
        //        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
        //        {
        //            if (bdRd.HasRows)
        //            {
        //                while (bdRd.Read())
        //                {
        //                    var item = new BUSES_DESPACHO();
        //                    if (!DBNull.Value.Equals(bdRd["CD_ID"])) { item.CD_ID = Convert.ToInt32(bdRd["CD_ID"]); }
        //                    if (!DBNull.Value.Equals(bdRd["CD_CONCEPTOS"])) { item.CD_CONCEPTOS = bdRd["CD_CONCEPTOS"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["BD_ESTADO"])) { item.BD_ESTADO = bdRd["BD_ESTADO"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["BD_CALIDAD"])) { item.BD_CALIDAD = bdRd["BD_CALIDAD"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["BD_OBSERVACION"])) { item.BD_OBSERVACION = bdRd["BD_OBSERVACION"].ToString(); }
        //                    resultado.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    return resultado;
        //}

        //public List<BUSES_DESPACHO> Buscar_Formato_Placa(string placa, int codigo_despacho)
        //{
        //    var bdParameters = new OracleDynamicParameters();
        //    var query = "SELECT DISTINCT BS_PLACA,BD_CONCESIONARIO,BD_DIRECCION,to_char(BD_FECHA, 'DD/MM/YYYY') AS BD_FECHA, to_char(BD_FECHA, 'hh:mm') AS HORA, BD_KM, USUREG,URL_FOTO URL_FOTO1,URL_FOTO2,URL_FOTO3,URL_FOTO4 FROM BUSES_DESPACHO where bs_placa ='" + placa + "' and BD_COD_DESPACHO=" + codigo_despacho + " AND ROWNUM<2";
        //    var result = SqlMapper.Query<BUSES_DESPACHO>(conn, query).ToList();
        //    return result;
        //}

        //public List<BUSES_DESPACHO> Buscar_Formato_Placa(string placa, int codigo_despacho)
        //{
        //    List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();

        //    OracleParameter[] bdParameters = new OracleParameter[3];
        //    bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
        //    bdParameters[1] = new OracleParameter("BS_PLACA_", OracleDbType.Varchar2) { Value = placa };
        //    bdParameters[2] = new OracleParameter("CODIGO_", OracleDbType.Int32) { Value = codigo_despacho };

        //    using (var bdCmd = new OracleCommand("PKG_BUSES.SP_BUSCAR_FORMATO_PLACA_", conn))
        //    {
        //        bdCmd.CommandType = CommandType.StoredProcedure;
        //        bdCmd.Parameters.AddRange(bdParameters);
        //        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
        //        {
        //            if (bdRd.HasRows)
        //            {
        //                while (bdRd.Read())
        //                {
        //                    var item = new BUSES_DESPACHO();
        //                    if (!DBNull.Value.Equals(bdRd["BS_PLACA"])) { item.BS_PLACA = bdRd["BS_PLACA"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["BD_CONCESIONARIO"])) { item.BD_CONCESIONARIO = bdRd["BD_CONCESIONARIO"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["BD_DIRECCION"])) { item.BD_DIRECCION = bdRd["BD_DIRECCION"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["BD_FECHA"])) { item.BD_FECHA = bdRd["BD_FECHA"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["HORA"])) { item.HORA = Convert.ToDateTime(bdRd["HORA"]); }
        //                    if (!DBNull.Value.Equals(bdRd["BD_KM"])) { item.BD_KM = Convert.ToInt32(bdRd["BD_KM"]); }
        //                    if (!DBNull.Value.Equals(bdRd["USUREG"])) { item.USUREG = bdRd["USUREG"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["URL_FOTO"])) { item.URL_FOTO = bdRd["URL_FOTO"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["URL_FOTO2"])) { item.URL_FOTO2 = bdRd["URL_FOTO2"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["URL_FOTO3"])) { item.URL_FOTO3 = bdRd["URL_FOTO3"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["URL_FOTO4"])) { item.URL_FOTO4 = bdRd["URL_FOTO4"].ToString(); }

        //                    resultado.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    return resultado;
        //}
        //public List<BUSES_DESPACHO> Listar_Informes_Despacho()
        //{
        //    var bdParameters = new OracleDynamicParameters();
        //    var query = "select  DISTINCT bs_placa,BD_COD_DESPACHO,to_char(BD_FECHA)BD_FECHA FROM BUSES_DESPACHO";
        //    var result = SqlMapper.Query<BUSES_DESPACHO>(conn, query).ToList();
        //    return result;
        //}

        //public List<BUSES_DESPACHO> Listar_Informes_Despacho()
        //{
        //    List<BUSES_DESPACHO> resultado = new List<BUSES_DESPACHO>();

        //    OracleParameter[] bdParameters = new OracleParameter[1];
        //    bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

        //    using (var bdCmd = new OracleCommand("PKG_BUSES.SP_LISTA_INFORMES_DESPACHO", conn))
        //    {
        //        bdCmd.CommandType = CommandType.StoredProcedure;
        //        bdCmd.Parameters.AddRange(bdParameters);
        //        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
        //        {
        //            if (bdRd.HasRows)
        //            {
        //                while (bdRd.Read())
        //                {
        //                    var item = new BUSES_DESPACHO();
        //                    if (!DBNull.Value.Equals(bdRd["BS_PLACA"])) { item.BS_PLACA = bdRd["BS_PLACA"].ToString(); }
        //                    if (!DBNull.Value.Equals(bdRd["BD_COD_DESPACHO"])) { item.BD_COD_DESPACHO = Convert.ToInt32(bdRd["BD_COD_DESPACHO"]); }
        //                    if (!DBNull.Value.Equals(bdRd["BD_FECHA"])) { item.BD_FECHA = bdRd["BD_FECHA"].ToString(); }

        //                    resultado.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    return resultado;
        //}

        //public void Cambiar_Estado_Bus(string placa, int codigo_despacho)
        //{
        //    var bdParameters = new OracleDynamicParameters();
        //    var query = "update buses_despacho set ESTADO_BUS='NO PROGRAMABLE' WHERE BS_PLACA='" + placa + "' and BD_COD_DESPACHO=" + codigo_despacho + "";
        //    var result = SqlMapper.Query(conn, query);
        //}

        public RPTA_GENERAL Cambiar_Estado_Bus(string placa, int codigo_despacho)
        {

            RPTA_GENERAL r = new RPTA_GENERAL();

            OracleParameter[] bdParameters = new OracleParameter[2];
            //bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            bdParameters[0] = new OracleParameter("P_BS_PLACA", OracleDbType.Varchar2) { Value = placa };
            bdParameters[1] = new OracleParameter("P_BD_COD_DESPACHO", OracleDbType.Int32) { Value = codigo_despacho };

            try
            {
                using (var bdCmd = new OracleCommand("PKG_BUSES.SP_CAMBIAR_ESTADO_BUS", conn))
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
        

        public List<CC_CONDUCTORES> Listar_Placa()
        {
            List<CC_CONDUCTORES> resultado = new List<CC_CONDUCTORES>();

            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            using (var bdCmd = new OracleCommand("PKG_BUSES.SP_LISTA_BUSES", conn))
            {
                bdCmd.CommandType = CommandType.StoredProcedure;
                bdCmd.Parameters.AddRange(bdParameters);
                using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                {
                    if (bdRd.HasRows)
                    {
                        while (bdRd.Read())
                        {
                            var item = new CC_CONDUCTORES();
                            if (!DBNull.Value.Equals(bdRd["BS_PLACA"])) { item.PLACA = bdRd["BS_PLACA"].ToString(); }

                            resultado.Add(item);
                        }
                    }
                }
            }
            return resultado;
        }


        public List<BUSES_DESPACHO> FIltrar_Archivo_Despacho(string fecha,int id_corredor)
        {

            var bdParameters = new OracleDynamicParameters();

            var query = "";

           
            query = "select  DISTINCT B.bs_placa,B.BD_COD_DESPACHO,to_char(B.BD_FECHA)BD_FECHA,B.BD_LONGUITUD,B.BD_LATITUD,B.ESTADO_BUS,USUREG FROM BUSES_DESPACHO  B WHERE ID_CORREDOR ="+ id_corredor+" AND to_char(BD_FECHA, 'DD.MM.YYYY')= '" + fecha + "'";
            var result = SqlMapper.Query<BUSES_DESPACHO>(conn, query).ToList();
            return result;
        }


    }
}
