using AD.EntidadesAD;
using ENTIDADES;
using LN.EntidadesLN;
using Newtonsoft.Json;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;

namespace LOGINPUA.Models
{
    public class ReadExcel
    {
        public string Matriz_Placas(string path, string usuario)
        {
            String mensaje = "";
            Int32 tipo = 0;
            //var rpta = "";
            RPTA_GENERAL e = new RPTA_GENERAL();
            var mensajeError = "";

            var cantidadRegistrosGuardados = 0;
            var cantidadRegistrosFallidos = 0;


            BUSESLN _busesLN = new BUSESLN();
            List<CC_BUSES> lista = new List<CC_BUSES>();

            int row = 0;



            using (FileStream fs = new FileStream(path, FileMode.Open))
            {

                SLDocument xlDoc = new SLDocument(fs);
                SLWorksheetStatistics stats1 = xlDoc.GetWorksheetStatistics();

                for (row = 2; row <= stats1.EndRowIndex; row++)
                {
                    //aqui lee la placa 
                    var placa = xlDoc.GetCellValueAsString(row, 4);

                    if (placa != "")
                    {
                        CC_BUSES buses = new CC_BUSES();

                        DateTime FECINIC_PT = xlDoc.GetCellValueAsDateTime(row, 2);
                        buses.BS_FECINI_PT = FECINIC_PT.ToString("dd/MM/yyyy");

                        buses.BS_NOM_EMPRE = xlDoc.GetCellValueAsString(row, 3);
                        buses.BS_PADRON = xlDoc.GetCellValueAsString(row, 4);
                        buses.BS_PLACA = xlDoc.GetCellValueAsString(row, 5);
                        buses.BS_PROPIETARIO = xlDoc.GetCellValueAsString(row, 6);
                        buses.BS_PAQUETE_CONCESION = xlDoc.GetCellValueAsString(row, 7);
                        buses.BS_PAQUETE_SERVICIO = xlDoc.GetCellValueAsString(row, 8);
                        buses.BS_TIPO_SERVICIO = xlDoc.GetCellValueAsString(row, 9);
                        buses.BS_ESTADO = xlDoc.GetCellValueAsString(row, 10);
                        buses.BS_MARCA = xlDoc.GetCellValueAsString(row, 11);
                        buses.BS_MODELO = xlDoc.GetCellValueAsString(row, 12);
                        buses.BS_AÑO_FABRICACION = xlDoc.GetCellValueAsString(row, 13);
                        buses.BS_COMBUSTIBLE = xlDoc.GetCellValueAsString(row, 14);
                        buses.BS_TECNOLOGIA_EURO = xlDoc.GetCellValueAsString(row, 15);
                        buses.BS_POTENCIA_MOTOR = xlDoc.GetCellValueAsString(row, 16);
                        buses.BS_SERIE_MOTOR = xlDoc.GetCellValueAsString(row, 17);
                        buses.BS_SERIE_CHASIS = xlDoc.GetCellValueAsString(row, 18);
                        buses.BS_COLOR_VEHICULO = xlDoc.GetCellValueAsString(row, 19);
                        buses.BS_LONGITUD = xlDoc.GetCellValueAsString(row, 20);
                        buses.BS_ASIENTOS = xlDoc.GetCellValueAsString(row, 21);
                        buses.BS_AREA_PASILLO = xlDoc.GetCellValueAsString(row, 22);
                        buses.BS_PESO_NETO = xlDoc.GetCellValueAsString(row, 23);
                        buses.BS_PESO_BRUTO = xlDoc.GetCellValueAsString(row, 24);
                        buses.BS_ALTURA = xlDoc.GetCellValueAsString(row, 25);
                        buses.BS_ANCHO = xlDoc.GetCellValueAsString(row, 26);
                        buses.BS_CARGA_UTIL = xlDoc.GetCellValueAsString(row, 27);
                        buses.BS_PUERTA_IZQUIERDA = xlDoc.GetCellValueAsString(row, 28);
                        buses.BS_PARTIDA_REGISTRAL = xlDoc.GetCellValueAsString(row, 29);
                        buses.BS_CODIGO_CVS = xlDoc.GetCellValueAsString(row, 30);

                        DateTime comp_sin_fec = new DateTime(1900, 1, 1);


                        DateTime CVS_FEC_INIC = xlDoc.GetCellValueAsDateTime(row, 31);
                        if (CVS_FEC_INIC.Equals(comp_sin_fec)) { buses.BS_CVS_FEC_INIC = ""; }
                        else { buses.BS_CVS_FEC_INIC = CVS_FEC_INIC.ToString("dd/MM/yyyy"); }


                        DateTime CVS_FEC_FIN = xlDoc.GetCellValueAsDateTime(row, 32);
                        if (CVS_FEC_FIN.Equals(comp_sin_fec)) { buses.BS_CVS_FEC_FIN = ""; }
                        else { buses.BS_CVS_FEC_FIN = CVS_FEC_FIN.ToString("dd/MM/yyyy"); }

                        DateTime SOAT_FEC_INIC = xlDoc.GetCellValueAsDateTime(row, 33);
                        if (SOAT_FEC_INIC.Equals(comp_sin_fec)) { buses.BS_SOAT_FEC_INIC = ""; }
                        else { buses.BS_SOAT_FEC_INIC = SOAT_FEC_INIC.ToString("dd/MM/yyyy"); }

                        DateTime SOAT_FEC_FIN = xlDoc.GetCellValueAsDateTime(row, 34);
                        if (SOAT_FEC_FIN.Equals(comp_sin_fec)) { buses.BS_SOAT_FEC_FIN = ""; }
                        else { buses.BS_SOAT_FEC_FIN = SOAT_FEC_FIN.ToString("dd/MM/yyyy"); }

                        buses.BS_POLIZA_VEHICULOS = xlDoc.GetCellValueAsString(row, 35);



                        DateTime VEHICULOS_INIC = xlDoc.GetCellValueAsDateTime(row, 36);
                        if (VEHICULOS_INIC.Equals(comp_sin_fec)) { buses.BS_VEHICULOS_INIC = ""; }
                        else { buses.BS_VEHICULOS_INIC = VEHICULOS_INIC.ToString("dd/MM/yyyy"); }

                        DateTime VEHICULOS_FIN = xlDoc.GetCellValueAsDateTime(row, 37);
                        if (VEHICULOS_FIN.Equals(comp_sin_fec)) { buses.BS_VEHICULOS_FIN = ""; }
                        else { buses.BS_VEHICULOS_FIN = VEHICULOS_FIN.ToString("dd/MM/yyyy"); }


                        buses.BS_POLIZA_CIVIL = xlDoc.GetCellValueAsString(row, 38);


                        DateTime RC_INICIO = xlDoc.GetCellValueAsDateTime(row, 39);
                        if (RC_INICIO.Equals(comp_sin_fec)) { buses.BS_RC_INICIO = ""; }
                        else { buses.BS_RC_INICIO = RC_INICIO.ToString("dd/MM/yyyy"); }

                        DateTime RC_FIN = xlDoc.GetCellValueAsDateTime(row, 40);
                        if (RC_FIN.Equals(comp_sin_fec)) { buses.BS_RC_FIN = ""; }
                        else { buses.BS_RC_FIN = RC_FIN.ToString("dd/MM/yyyy"); }

                        buses.BS_POLIZA_ACCI_COLECTIVOS = xlDoc.GetCellValueAsString(row, 41);



                        DateTime APCP_INICIO = xlDoc.GetCellValueAsDateTime(row, 42);
                        if (APCP_INICIO.Equals(comp_sin_fec)) { buses.BS_APCP_INIC = ""; }
                        else { buses.BS_APCP_INIC = APCP_INICIO.ToString("dd/MM/yyyy"); }

                        DateTime APCP_VIGENCIA = xlDoc.GetCellValueAsDateTime(row, 43);
                        if (APCP_VIGENCIA.Equals(comp_sin_fec)) { buses.BS_APCP_FIN = ""; }
                        else { buses.BS_APCP_FIN = APCP_VIGENCIA.ToString("dd/MM/yyyy"); }


                        buses.BS_POLIZA_MULTIRIESGOS = xlDoc.GetCellValueAsString(row, 44);

                        DateTime MULTIRESGOS_INICIO = xlDoc.GetCellValueAsDateTime(row, 45);
                        if (MULTIRESGOS_INICIO.Equals(comp_sin_fec)) { buses.BS_MULTIRESGOS_INIC = ""; }
                        else { buses.BS_MULTIRESGOS_INIC = MULTIRESGOS_INICIO.ToString("dd/MM/yyyy"); }

                        DateTime MULTIRESGOS_VIGENCIA = xlDoc.GetCellValueAsDateTime(row, 46);
                        if (MULTIRESGOS_VIGENCIA.Equals(comp_sin_fec)) { buses.BS_MULTIRESGOS_FIN = ""; }
                        else { buses.BS_MULTIRESGOS_FIN = MULTIRESGOS_VIGENCIA.ToString("dd/MM/yyyy"); }

                        DateTime RTV_INIC = xlDoc.GetCellValueAsDateTime(row, 47);
                        if (RTV_INIC.Equals(comp_sin_fec)) { buses.BS_RTV_INIC = ""; }
                        else { buses.BS_RTV_INIC = RTV_INIC.ToString("dd/MM/yyyy"); }

                        DateTime RTV_FIN = xlDoc.GetCellValueAsDateTime(row, 48);
                        if (RTV_FIN.Equals(comp_sin_fec)) { buses.BS_RTV_FIN = ""; }
                        else { buses.BS_RTV_FIN = RTV_FIN.ToString("dd/MM/yyyy"); }

                        DateTime REVISION_ANUAL_INIC = xlDoc.GetCellValueAsDateTime(row, 49);
                        if (REVISION_ANUAL_INIC.Equals(comp_sin_fec)) { buses.BS_REVI_ANUAL_GNV_INIC = ""; }
                        else { buses.BS_REVI_ANUAL_GNV_INIC = REVISION_ANUAL_INIC.ToString("dd/MM/yyyy"); }


                        DateTime REVISION_ANUAL_FIN = xlDoc.GetCellValueAsDateTime(row, 50);
                        if (REVISION_ANUAL_FIN.Equals(comp_sin_fec)) { buses.BS_REVI_ANUAL_GNV_FIN = ""; }
                        else { buses.BS_REVI_ANUAL_GNV_FIN = REVISION_ANUAL_FIN.ToString("dd/MM/yyyy"); }


                        DateTime REVISION_CILINDRO_INIC = xlDoc.GetCellValueAsDateTime(row, 51);
                        if (REVISION_CILINDRO_INIC.Equals(comp_sin_fec)) { buses.BS_REVISION_CILINDROS_GNV_INIC = ""; }
                        else { buses.BS_REVISION_CILINDROS_GNV_INIC = REVISION_CILINDRO_INIC.ToString("dd/MM/yyyy"); }



                        DateTime REVISION_CILINDRO_FIN = xlDoc.GetCellValueAsDateTime(row, 52);
                        if (REVISION_CILINDRO_FIN.Equals(comp_sin_fec)) { buses.BS_REVISION_CILINDROS_GNV_FIN = ""; }
                        else { buses.BS_REVISION_CILINDROS_GNV_FIN = REVISION_CILINDRO_FIN.ToString("dd/MM/yyyy"); }

                        var abrevCorredor = xlDoc.GetCellValueAsString(row, 53);

                        switch (abrevCorredor)
                        {
                            case "TGA":
                                buses.ID_CORREDOR = 1;
                                break;
                            case "JP":
                                buses.ID_CORREDOR = 2;
                                break;
                            case "CC":
                                buses.ID_CORREDOR = 3;
                                break;
                            case "SJL":
                                buses.ID_CORREDOR = 4;
                                break;
                            case "PN":
                                buses.ID_CORREDOR = 5;
                                break;
                            default:
                                buses.ID_CORREDOR = 0;
                                break;
                        }

                        //buses.ID_CORREDOR = xlDoc.GetCellValueAsInt32(row, 52);


                        buses.USU_REG = usuario;
                        buses.ESTADO_VEHICULO = "NORMAL";
                        buses.ID_ESTADO = 1;
                        lista.Add(buses);
                    }
                }
            }




            foreach (var item in lista)
            {
                //
                if (item.BS_PLACA != "")
                {

                    //var rpt_placa = _busesLN.Verifica_Placa_Existente(item.BS_PLACA, ref mensaje, ref tipo);

                    //if (rpt_placa.AUX == 0)
                    //{
                        CC_BUSES modelo = new CC_BUSES();
                        //modelo.BS_ID = item.BS_ID;
                        modelo.ID_CORREDOR = item.ID_CORREDOR;
                        modelo.BS_FECINI_PT = item.BS_FECINI_PT;
                        modelo.BS_NOM_EMPRE = item.BS_NOM_EMPRE;
                        modelo.BS_PADRON = item.BS_PADRON;
                        modelo.BS_PLACA = item.BS_PLACA;
                        modelo.BS_PROPIETARIO = item.BS_PROPIETARIO;
                        modelo.BS_PAQUETE_CONCESION = item.BS_PAQUETE_CONCESION;
                        modelo.BS_PAQUETE_SERVICIO = item.BS_PAQUETE_SERVICIO;
                        modelo.BS_TIPO_SERVICIO = item.BS_TIPO_SERVICIO;
                        modelo.BS_ESTADO = item.BS_ESTADO;
                        modelo.BS_MARCA = item.BS_MARCA;
                        modelo.BS_MODELO = item.BS_MODELO;
                        modelo.BS_AÑO_FABRICACION = item.BS_AÑO_FABRICACION;
                        modelo.BS_COMBUSTIBLE = item.BS_COMBUSTIBLE;
                        modelo.BS_TECNOLOGIA_EURO = item.BS_TECNOLOGIA_EURO;
                        modelo.BS_POTENCIA_MOTOR = item.BS_POTENCIA_MOTOR;
                        modelo.BS_SERIE_MOTOR = item.BS_SERIE_MOTOR;
                        modelo.BS_SERIE_CHASIS = item.BS_SERIE_CHASIS;
                        modelo.BS_COLOR_VEHICULO = item.BS_COLOR_VEHICULO;
                        modelo.BS_LONGITUD = item.BS_LONGITUD;
                        modelo.BS_ASIENTOS = item.BS_ASIENTOS;
                        modelo.BS_AREA_PASILLO = item.BS_AREA_PASILLO;
                        modelo.BS_PESO_NETO = item.BS_PESO_NETO;
                        modelo.BS_PESO_BRUTO = item.BS_PESO_BRUTO;
                        modelo.BS_ALTURA = item.BS_ALTURA;
                        modelo.BS_ANCHO = item.BS_ANCHO;
                        modelo.BS_CARGA_UTIL = item.BS_CARGA_UTIL;
                        modelo.BS_PUERTA_IZQUIERDA = item.BS_PUERTA_IZQUIERDA;
                        modelo.BS_PARTIDA_REGISTRAL = item.BS_PARTIDA_REGISTRAL;
                        modelo.BS_CODIGO_CVS = item.BS_CODIGO_CVS;
                        modelo.BS_CVS_FEC_INIC = item.BS_CVS_FEC_INIC;
                        modelo.BS_CVS_FEC_FIN = item.BS_CVS_FEC_FIN;
                        modelo.BS_SOAT_FEC_INIC = item.BS_SOAT_FEC_INIC;
                        modelo.BS_SOAT_FEC_FIN = item.BS_SOAT_FEC_FIN;
                        modelo.BS_POLIZA_VEHICULOS = item.BS_POLIZA_VEHICULOS;
                        modelo.BS_VEHICULOS_INIC = item.BS_VEHICULOS_INIC;
                        modelo.BS_VEHICULOS_FIN = item.BS_VEHICULOS_FIN;
                        modelo.BS_POLIZA_CIVIL = item.BS_POLIZA_CIVIL;
                        modelo.BS_RC_INICIO = item.BS_RC_INICIO;
                        modelo.BS_RC_FIN = item.BS_RC_FIN;
                        modelo.BS_POLIZA_ACCI_COLECTIVOS = item.BS_POLIZA_ACCI_COLECTIVOS;
                        modelo.BS_APCP_INIC = item.BS_APCP_INIC;
                        modelo.BS_APCP_FIN = item.BS_APCP_FIN;
                        modelo.BS_POLIZA_MULTIRIESGOS = item.BS_POLIZA_MULTIRIESGOS;
                        modelo.BS_MULTIRESGOS_INIC = item.BS_MULTIRESGOS_INIC;
                        modelo.BS_MULTIRESGOS_FIN = item.BS_MULTIRESGOS_FIN;
                        modelo.BS_RTV_INIC = item.BS_RTV_INIC;
                        modelo.BS_RTV_FIN = item.BS_RTV_FIN;
                        modelo.BS_REVI_ANUAL_GNV_INIC = item.BS_REVI_ANUAL_GNV_INIC;
                        modelo.BS_REVI_ANUAL_GNV_FIN = item.BS_REVI_ANUAL_GNV_FIN;
                        modelo.BS_REVISION_CILINDROS_GNV_INIC = item.BS_REVISION_CILINDROS_GNV_INIC;
                        modelo.BS_REVISION_CILINDROS_GNV_FIN = item.BS_REVISION_CILINDROS_GNV_FIN;
                        modelo.ESTADO_VEHICULO = item.ESTADO_VEHICULO;
                        modelo.PLACA_REEMPLAZADA = item.PLACA_REEMPLAZADA;
                        modelo.ID_ESTADO = item.ID_ESTADO;
                        modelo.USU_REG = usuario;

                        e = _busesLN.Insertar_Buses_Nuevos(modelo, ref mensaje, ref tipo);

                        if (e.COD_ESTADO == 1)
                        {
                            cantidadRegistrosGuardados++;
                        }
                        else
                        {
                            cantidadRegistrosFallidos++;
                        }
                    //}
                }
            }

            if (cantidadRegistrosGuardados >= cantidadRegistrosFallidos)
            {
                var result = cantidadRegistrosGuardados + " registros guardados exitosamente.";
                e.DES_ESTADO = result;
                e.COD_ESTADO = 1;
            }
            else if (cantidadRegistrosGuardados == 0)
            {
                var result = cantidadRegistrosFallidos + " No se han registrado correctamente.";
                e.DES_ESTADO = result;
                e.COD_ESTADO = 0;
            }


            return JsonConvert.SerializeObject(e);

        }





        //    while (row < stats1.EndRowIndex + 1)
        //    {
        //        contador++;
        //        id = id + 1;
        //        CC_BUSES buses = new CC_BUSES();
        //        buses.BS_ID = id;

        //        DateTime FECINIC_PT = xlDoc.GetCellValueAsDateTime(row, 2);
        //        buses.BS_FECINI_PT = FECINIC_PT.ToString("dd/MM/yyyy");

        //        buses.BS_NOM_EMPRE = xlDoc.GetCellValueAsString(row, 3);
        //        buses.BS_PLACA = xlDoc.GetCellValueAsString(row, 4);
        //        buses.BS_PROPIETARIO = xlDoc.GetCellValueAsString(row, 5);
        //        buses.BS_PAQUETE_CONCESION = xlDoc.GetCellValueAsString(row, 6);
        //        buses.BS_PAQUETE_SERVICIO = xlDoc.GetCellValueAsString(row, 7);
        //        buses.BS_TIPO_SERVICIO = xlDoc.GetCellValueAsString(row, 8);
        //        buses.BS_ESTADO = xlDoc.GetCellValueAsString(row, 9);
        //        buses.BS_MARCA = xlDoc.GetCellValueAsString(row, 10);
        //        buses.BS_MODELO = xlDoc.GetCellValueAsString(row, 11);
        //        buses.BS_AÑO_FABRICACION = xlDoc.GetCellValueAsString(row, 12);
        //        buses.BS_COMBUSTIBLE = xlDoc.GetCellValueAsString(row, 13);
        //        buses.BS_TECNOLOGIA_EURO = xlDoc.GetCellValueAsString(row, 14);
        //        buses.BS_POTENCIA_MOTOR = xlDoc.GetCellValueAsString(row, 15);
        //        buses.BS_SERIE_MOTOR = xlDoc.GetCellValueAsString(row, 16);
        //        buses.BS_SERIE_CHASIS = xlDoc.GetCellValueAsString(row, 17);
        //        buses.BS_COLOR_VEHICULO = xlDoc.GetCellValueAsString(row, 18);
        //        buses.BS_LONGITUD = xlDoc.GetCellValueAsString(row, 19);
        //        buses.BS_ASIENTOS = xlDoc.GetCellValueAsString(row, 20);
        //        buses.BS_AREA_PASILLO = xlDoc.GetCellValueAsString(row, 21);
        //        buses.BS_PESO_NETO = xlDoc.GetCellValueAsString(row, 22);
        //        buses.BS_PESO_BRUTO = xlDoc.GetCellValueAsString(row, 23);
        //        buses.BS_ALTURA = xlDoc.GetCellValueAsString(row, 24);
        //        buses.BS_ANCHO = xlDoc.GetCellValueAsString(row, 25);
        //        buses.BS_CARGA_UTIL = xlDoc.GetCellValueAsString(row, 26);
        //        buses.BS_PUERTA_IZQUIERDA = xlDoc.GetCellValueAsString(row, 27);
        //        buses.BS_PARTIDA_REGISTRAL = xlDoc.GetCellValueAsString(row, 28);
        //        buses.BS_CODIGO_CVS = xlDoc.GetCellValueAsString(row, 29);

        //        DateTime comp_sin_fec = new DateTime(1900, 1, 1);


        //        DateTime CVS_FEC_INIC = xlDoc.GetCellValueAsDateTime(row, 30);
        //        if (CVS_FEC_INIC.Equals(comp_sin_fec)) { buses.BS_CVS_FEC_INIC = ""; }
        //        else { buses.BS_CVS_FEC_INIC = CVS_FEC_INIC.ToString("dd/MM/yyyy"); }


        //        DateTime CVS_FEC_FIN = xlDoc.GetCellValueAsDateTime(row, 31);
        //        if (CVS_FEC_FIN.Equals(comp_sin_fec)) { buses.BS_CVS_FEC_FIN = ""; }
        //        else { buses.BS_CVS_FEC_FIN = CVS_FEC_FIN.ToString("dd/MM/yyyy"); }

        //        DateTime SOAT_FEC_INIC = xlDoc.GetCellValueAsDateTime(row, 32);
        //        if (SOAT_FEC_INIC.Equals(comp_sin_fec)) { buses.BS_SOAT_FEC_INIC = ""; }
        //        else { buses.BS_SOAT_FEC_INIC = SOAT_FEC_INIC.ToString("dd/MM/yyyy"); }

        //        DateTime SOAT_FEC_FIN = xlDoc.GetCellValueAsDateTime(row, 33);
        //        if (SOAT_FEC_FIN.Equals(comp_sin_fec)) { buses.BS_SOAT_FEC_FIN = ""; }
        //        else { buses.BS_SOAT_FEC_FIN = SOAT_FEC_FIN.ToString("dd/MM/yyyy"); }

        //        buses.BS_POLIZA_VEHICULOS = xlDoc.GetCellValueAsString(row, 34);



        //        DateTime VEHICULOS_INIC = xlDoc.GetCellValueAsDateTime(row, 35);
        //        if (VEHICULOS_INIC.Equals(comp_sin_fec)) { buses.BS_VEHICULOS_INIC = ""; }
        //        else { buses.BS_VEHICULOS_INIC = VEHICULOS_INIC.ToString("dd/MM/yyyy"); }

        //        DateTime VEHICULOS_FIN = xlDoc.GetCellValueAsDateTime(row, 36);
        //        if (VEHICULOS_FIN.Equals(comp_sin_fec)) { buses.BS_VEHICULOS_FIN = ""; }
        //        else { buses.BS_VEHICULOS_FIN = VEHICULOS_FIN.ToString("dd/MM/yyyy"); }


        //        buses.BS_POLIZA_CIVIL = xlDoc.GetCellValueAsString(row, 37);


        //        DateTime RC_INICIO = xlDoc.GetCellValueAsDateTime(row, 38);
        //        if (RC_INICIO.Equals(comp_sin_fec)) { buses.BS_RC_INICIO = ""; }
        //        else { buses.BS_RC_INICIO = RC_INICIO.ToString("dd/MM/yyyy"); }

        //        DateTime RC_FIN = xlDoc.GetCellValueAsDateTime(row, 39);
        //        if (RC_FIN.Equals(comp_sin_fec)) { buses.BS_RC_FIN = ""; }
        //        else { buses.BS_RC_FIN = RC_FIN.ToString("dd/MM/yyyy"); }

        //        buses.BS_POLIZA_ACCI_COLECTIVOS = xlDoc.GetCellValueAsString(row, 40);



        //        DateTime APCP_INICIO = xlDoc.GetCellValueAsDateTime(row, 41);
        //        if (APCP_INICIO.Equals(comp_sin_fec)) { buses.BS_APCP_INIC = ""; }
        //        else { buses.BS_APCP_INIC = APCP_INICIO.ToString("dd/MM/yyyy"); }

        //        DateTime APCP_VIGENCIA = xlDoc.GetCellValueAsDateTime(row, 42);
        //        if (APCP_VIGENCIA.Equals(comp_sin_fec)) { buses.BS_APCP_FIN = ""; }
        //        else { buses.BS_APCP_FIN = APCP_VIGENCIA.ToString("dd/MM/yyyy"); }


        //        buses.BS_POLIZA_MULTIRIESGOS = xlDoc.GetCellValueAsString(row, 43);

        //        DateTime MULTIRESGOS_INICIO = xlDoc.GetCellValueAsDateTime(row, 44);
        //        if (MULTIRESGOS_INICIO.Equals(comp_sin_fec)) { buses.BS_MULTIRESGOS_INIC = ""; }
        //        else { buses.BS_MULTIRESGOS_INIC = MULTIRESGOS_INICIO.ToString("dd/MM/yyyy"); }

        //        DateTime MULTIRESGOS_VIGENCIA = xlDoc.GetCellValueAsDateTime(row, 45);
        //        if (MULTIRESGOS_VIGENCIA.Equals(comp_sin_fec)) { buses.BS_MULTIRESGOS_FIN = ""; }
        //        else { buses.BS_MULTIRESGOS_FIN = MULTIRESGOS_VIGENCIA.ToString("dd/MM/yyyy"); }

        //        DateTime RTV_INIC = xlDoc.GetCellValueAsDateTime(row, 46);
        //        if (RTV_INIC.Equals(comp_sin_fec)) { buses.BS_RTV_INIC = ""; }
        //        else { buses.BS_RTV_INIC = RTV_INIC.ToString("dd/MM/yyyy"); }

        //        DateTime RTV_FIN = xlDoc.GetCellValueAsDateTime(row, 47);
        //        if (RTV_FIN.Equals(comp_sin_fec)) { buses.BS_RTV_FIN = ""; }
        //        else { buses.BS_RTV_FIN = RTV_FIN.ToString("dd/MM/yyyy"); }

        //        DateTime REVISION_ANUAL_INIC = xlDoc.GetCellValueAsDateTime(row, 48);
        //        if (REVISION_ANUAL_INIC.Equals(comp_sin_fec)) { buses.BS_REVI_ANUAL_GNV_INIC = ""; }
        //        else { buses.BS_REVI_ANUAL_GNV_INIC = REVISION_ANUAL_INIC.ToString("dd/MM/yyyy"); }


        //        DateTime REVISION_ANUAL_FIN = xlDoc.GetCellValueAsDateTime(row, 49);
        //        if (REVISION_ANUAL_FIN.Equals(comp_sin_fec)) { buses.BS_REVI_ANUAL_GNV_FIN = ""; }
        //        else { buses.BS_REVI_ANUAL_GNV_FIN = REVISION_ANUAL_FIN.ToString("dd/MM/yyyy"); }


        //        DateTime REVISION_CILINDRO_INIC = xlDoc.GetCellValueAsDateTime(row, 50);
        //        if (REVISION_CILINDRO_INIC.Equals(comp_sin_fec)) { buses.BS_REVISION_CILINDROS_GNV_INIC = ""; }
        //        else { buses.BS_REVISION_CILINDROS_GNV_INIC = REVISION_CILINDRO_INIC.ToString("dd/MM/yyyy"); }



        //        DateTime REVISION_CILINDRO_FIN = xlDoc.GetCellValueAsDateTime(row, 51);
        //        if (REVISION_CILINDRO_FIN.Equals(comp_sin_fec)) { buses.BS_REVISION_CILINDROS_GNV_FIN = ""; }
        //        else { buses.BS_REVISION_CILINDROS_GNV_FIN = REVISION_CILINDRO_FIN.ToString("dd/MM/yyyy"); }

        //        buses.ID_CORREDOR = xlDoc.GetCellValueAsInt32(row, 52);


        //        buses.USUREG = usuario;
        //        buses.ESTADO_VEHICULO = "NORMAL";


        //        lista.Add(buses);

        //        row++;
        //    }


        //}

        //public void Rutas_programadas(string path)
        //{

        //    ConductoresLN _ConductoresLN = new ConductoresLN();

        //    int id = 0;
        //    Excel.Application aplication = new Excel.Application();
        //    Excel.Workbook workbook = aplication.Workbooks.Open(path);
        //    Excel.Worksheet worksheet = workbook.ActiveSheet;
        //    Excel.Range range = worksheet.UsedRange;
        //    List<CC_PROGRAMA_RUTA> lista = new List<CC_PROGRAMA_RUTA>();
        //    try
        //    {
        //        var id1 = _ConductoresLN.Ultimo_id_rutas();
        //        id = id1.ID;
        //    }
        //    catch (Exception exe)
        //    {
        //        id = 0;
        //        string mensaje = exe.Message;
        //    }

        //    for (int row = 2; row < range.Rows.Count + 1; row++)
        //    {
        //        CC_PROGRAMA_RUTA ruta = new CC_PROGRAMA_RUTA();
        //        id = id + 1;
        //        ruta.ID = id;
        //        ruta.TIPO_DIA = xlDoc.GetCellValueAsString(row, 2);
        //        ruta.RUTA = xlDoc.GetCellValueAsString(row, 3);
        //        ruta.OPERADOR = xlDoc.GetCellValueAsString(row, 4);
        //        ruta.RECORDID = xlDoc.GetCellValueAsString(row, 5);
        //        ruta.ROUTE = xlDoc.GetCellValueAsString(row, 6);
        //        ruta.BLK = xlDoc.GetCellValueAsString(row, 7);
        //        ruta.POG = xlDoc.GetCellValueAsString(row, 8);
        //        ruta.POT = xlDoc.GetCellValueAsString(row, 9);
        //        ruta.FNODE = xlDoc.GetCellValueAsString(row, 10);
        //        ruta.FTIME = xlDoc.GetCellValueAsString(row, 11);
        //        ruta.TTIME = xlDoc.GetCellValueAsString(row, 12);
        //        ruta.TNODE = xlDoc.GetCellValueAsString(row, 13);
        //        ruta.PIG = xlDoc.GetCellValueAsString(row, 14);
        //        ruta.PIT = xlDoc.GetCellValueAsString(row, 15);
        //        ruta.LAY = xlDoc.GetCellValueAsString(row, 16);
        //        ruta.TRIP_TIME = xlDoc.GetCellValueAsString(row, 17);
        //        ruta.DISTANCIA = xlDoc.GetCellValueAsString(row, 18);
        //        ruta.ACUMULADO = xlDoc.GetCellValueAsString(row, 19);
        //        ruta.PATS = xlDoc.GetCellValueAsString(row, 20);
        //        ruta.SENTIDO = xlDoc.GetCellValueAsString(row, 21);
        //        ruta.OBSERVACIONES = xlDoc.GetCellValueAsString(row, 22);
        //        ruta.PLACA = xlDoc.GetCellValueAsString(row, 23);
        //        ruta.PAQUETE = xlDoc.GetCellValueAsString(row, 24);

        //        lista.Add(ruta);
        //    }
        //    foreach (var item in lista)
        //    {
        //        _ConductoresLN.Insertar_ruta
        //            (item.ID, item.TIPO_DIA, item.RUTA, item.OPERADOR, item.RECORDID, item.ROUTE,
        //            item.BLK, item.POG, item.POT, item.FNODE, item.FTIME, item.TTIME, item.TNODE
        //            , item.PIG, item.PIT, item.LAY, item.TRIP_TIME, item.DISTANCIA, item.ACUMULADO
        //            , item.PATS, item.SENTIDO, item.OBSERVACIONES, item.PLACA, item.PAQUETE);

        //    }
        //    workbook.Close();
        //}


        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {

                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;

        }

        public void Export_Excel(DataTable table, string nombre_reporte)
        {
            DateTime fecha_actual = DateTime.Now;
            string fecha = fecha_actual.ToString("dd/MM/yyyy");
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + nombre_reporte + "" + fecha + ".xls");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
            //am getting my grid's column headers
            int columnscount = table.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(table.Columns[j]);
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }




    }
}