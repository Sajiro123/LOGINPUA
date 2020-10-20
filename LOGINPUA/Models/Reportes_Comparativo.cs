using AD.EntidadesAD;
using CrystalDecisions.CrystalReports.Engine;
using ENTIDADES;
using LN.EntidadesLN;
using LN.Reportes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Diagnostics;

namespace LOGINPUA.Models
{
    public class Reportes_Comparativo
    {
        private Object bdConn;

        private readonly BusesAD _loginRepositorio;

        public Reportes_Comparativo()
        {
            BusesAD loginRepositorio = new BusesAD(ref bdConn);
             _loginRepositorio = loginRepositorio;
        }
        public string Insertar_rp_comparativo(CC_REPORTE_PICO_PLACA model, string ruta_base)
        {


            ReportDocument rd = new ReportDocument();
            Reporte_Comparativo dsReporte = new Reporte_Comparativo();

            //IMPORTAR TABLEADAPTER.
            rd.Dispose();
            var dtFormato = new Reporte_Comparativo.TBREGISTROPICOPLACADataTable();
            //RECORRER LISTAS.

            string fecha_in = model.FECHA_INICIO.ToString();

            var dr = dtFormato.NewRow();

            string corredor = model.CORREDOR;





            double vel_ida_t = 0.0;
            double vel_vuelta_t = 0.0;
            double vel_vuelta_m = 0.0;
            double vel_ida_m = 0.0;



            DateTime tiempo1 = Convert.ToDateTime(model.TIEMPO_PROM_A_1);
            DateTime tiempo2 = Convert.ToDateTime(model.TIEMPO_PROM_A_2);
            double minutos_ida_m = tiempo2.Subtract(tiempo1).TotalMinutes;

            DateTime tiempo3 = Convert.ToDateTime(model.TIEMPO_PROM_B_1);
            DateTime tiempo4 = Convert.ToDateTime(model.TIEMPO_PROM_B_2);
            double minutos_vuelta_m = tiempo4.Subtract(tiempo3).TotalMinutes;


            DateTime tiempo5 = Convert.ToDateTime(model.TIEMPO_PROM_A_3);
            DateTime tiempo6 = Convert.ToDateTime(model.TIEMPO_PROM_A_4);
            double minutos_ida_t = tiempo6.Subtract(tiempo5).TotalMinutes;

            DateTime tiempo7 = Convert.ToDateTime(model.TIEMPO_PROM_B_3);
            DateTime tiempo8 = Convert.ToDateTime(model.TIEMPO_PROM_B_4);
            double minutos_vuelta_t = tiempo8.Subtract(tiempo7).TotalMinutes;



            double vel_1 = Convert.ToDouble(model.VEL_PROMEDIO_A_MAÑANA_IDA);
            double vel_2 = Convert.ToDouble(model.VEL_PROMEDIO_B_MAÑANA_IDA);

            vel_ida_m = vel_1 == 0 ? 0 : ((vel_2 - vel_1) / vel_1) * 100;
            vel_ida_m = Math.Round(vel_ida_m, 1);

            double vel_3 = Convert.ToDouble(model.VEL_PROMEDIO_A_MAÑANA_VUELTA);
            double vel_4 = Convert.ToDouble(model.VEL_PROMEDIO_B_MAÑANA_VUELTA);

            vel_vuelta_m = vel_3 == 0 ? 0 : ((vel_4 - vel_3) / vel_3) * 100;
            vel_vuelta_m = Math.Round(vel_vuelta_m, 1);

            double vel_5 = Convert.ToDouble(model.VEL_PROMEDIO_A_TARDE_IDA);
            double vel_6 = Convert.ToDouble(model.VEL_PROMEDIO_B_TARDE_IDA);
            vel_ida_t = vel_5 == 0 ? 0 : ((vel_6 - vel_5) / vel_5) * 100;
            vel_ida_t = Math.Round(vel_ida_t, 1);
            double vel_7 = Convert.ToDouble(model.VEL_PROMEDIO_A_TARDE_VUELTA);
            double vel_8 = Convert.ToDouble(model.VEL_PROMEDIO_B_TARDE_VUELTA);
            vel_vuelta_t = vel_7 == 0 ? 0 : ((vel_8 - vel_7) / vel_7) * 100;
            vel_vuelta_t = Math.Round(vel_vuelta_t, 1);
            dr["TIEMPO_IDA"] = model.M_IDA + "-" + model.M_VUELTA;
            dr["TIEMPO_VUELTA"] = model.T_IDA + "-" + model.T_VUELTA;
            dr["MINUTOS_IDA_M"] = minutos_ida_m;
            dr["MINUTOS_VUELTA_M"] = minutos_vuelta_m;
            dr["MINUTOS_IDA_T"] = minutos_ida_t;
            dr["MINUTOS_VUELTA_T"] = minutos_vuelta_t;
            dr["CORREDOR"] = corredor;
            dr["VELOCIDAD_IDA_M"] = vel_ida_m;
            dr["VELOCIDAD_VUELTA_M"] = vel_vuelta_m;
            dr["VELOCIDAD_IDA_T"] = vel_ida_t;
            dr["VELOCIDAD_VUELTA_T"] = vel_vuelta_t;
            dr["VEL_PROMEDIO_A_MAÑANA_IDA"] = model.VEL_PROMEDIO_A_MAÑANA_IDA;
            dr["VEL_PROMEDIO_A_MAÑANA_VUELTA"] = model.VEL_PROMEDIO_A_MAÑANA_VUELTA;
            dr["TIEMPO_PROM_A_1"] = model.TIEMPO_PROM_A_1;
            dr["TIEMPO_PROM_B_1"] = model.TIEMPO_PROM_B_1;
            dr["VEL_PROMEDIO_B_MAÑANA_IDA"] = model.VEL_PROMEDIO_B_MAÑANA_IDA;
            dr["VEL_PROMEDIO_B_MAÑANA_VUELTA"] = model.VEL_PROMEDIO_B_MAÑANA_VUELTA;
            dr["TIEMPO_PROM_A_2"] = model.TIEMPO_PROM_A_2;
            dr["TIEMPO_PROM_B_2"] = model.TIEMPO_PROM_B_2;
            dr["VEL_PROMEDIO_A_TARDE_IDA"] = model.VEL_PROMEDIO_A_TARDE_IDA;
            dr["VEL_PROMEDIO_A_TARDE_VUELTA"] = model.VEL_PROMEDIO_A_TARDE_VUELTA;
            dr["TIEMPO_PROM_A_3"] = model.TIEMPO_PROM_A_3;
            dr["TIEMPO_PROM_B_3"] = model.TIEMPO_PROM_B_3;

            dr["VEL_PROMEDIO_B_TARDE_IDA"] = model.VEL_PROMEDIO_B_TARDE_IDA;
            dr["VEL_PROMEDIO_B_TARDE_VUELTA"] = model.VEL_PROMEDIO_B_TARDE_VUELTA;
            dr["TIEMPO_PROM_A_4"] = model.TIEMPO_PROM_A_4;
            dr["TIEMPO_PROM_B_4"] = model.TIEMPO_PROM_B_4;
            dr["FECHA_1"] = model.FECHA_INICIO;
            dr["FECHA_2"] = model.FECHA_FIN;
            dtFormato.Rows.Add(dr);

            //CARGAR TABLAS.
            dsReporte.Tables["TBREGISTROPICOPLACA"].Merge(dtFormato);
            /* GUARDAR LOS DATOS EN EL DATASET*/
            rd = new REPORTE_COMPARATIVO();
            rd.SetDataSource(dsReporte);
            /*CREAR PDF*/
            String resultado = "FORMATO_REPORTE_PICO_PLACA.pdf";
            System.IO.File.Delete(ruta_base + @"Download\" + resultado);
            rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, ruta_base + @"Download\" + resultado);
            rd.Close();
            return resultado;
        }

        public string Insertar_rp_comparativo_Salida(CC_REPORTE_PICO_PLACA model, string ruta_base)
        {


            ReportDocument rd = new ReportDocument();
            Reporte_Comparativo dsReporte = new Reporte_Comparativo();

            //IMPORTAR TABLEADAPTER.
            rd.Dispose();
            var dtFormato = new Reporte_SalidaComparativo.DataTable1DataTable();
            //RECORRER LISTAS.

            string fecha_in = model.FECHA_INICIO.ToString();

            var dr = dtFormato.NewRow();

            string corredor = model.CORREDOR;

            double vel_vuelta_m = 0.0;
            double vel_ida_m = 0.0;



            DateTime tiempo1 = Convert.ToDateTime(model.TIEMPO_PROM_A_1);
            DateTime tiempo2 = Convert.ToDateTime(model.TIEMPO_PROM_B_2);
            double minutos_ida_m = tiempo2.Subtract(tiempo1).TotalMinutes;

            DateTime tiempo3 = Convert.ToDateTime(model.TIEMPO_PROM_A_3);
            DateTime tiempo4 = Convert.ToDateTime(model.TIEMPO_PROM_B_4);
            double minutos_vuelta_m = tiempo4.Subtract(tiempo3).TotalMinutes;

            
            double vel_1 = Convert.ToDouble(model.VEL_PROMEDIO_A_MAÑANA_IDA);
            double vel_2 = Convert.ToDouble(model.VEL_PROMEDIO_B_MAÑANA_IDA);

            vel_ida_m = vel_1 == 0 ? 0 : ((vel_2 - vel_1) / vel_1) * 100;
            vel_ida_m = Math.Round(vel_ida_m, 1);

            double vel_3 = Convert.ToDouble(model.VEL_PROMEDIO_A_TARDE_IDA);
            double vel_4 = Convert.ToDouble(model.VEL_PROMEDIO_B_TARDE_IDA);

            vel_vuelta_m = vel_3 == 0 ? 0 : ((vel_4 - vel_3) / vel_3) * 100;
            vel_vuelta_m = Math.Round(vel_vuelta_m, 1);


            //TIEMPO
            dr["TIEMPO_PROM_A_1"] = model.TIEMPO_PROM_A_1;
            dr["TIEMPO_PROM_B_2"] = model.TIEMPO_PROM_B_2;
            dr["TIEMPO_PROM_A_3"] = model.TIEMPO_PROM_A_3;
            dr["TIEMPO_PROM_B_4"] = model.TIEMPO_PROM_B_4;

            //VELOCIDAD
            dr["VEL_PROMEDIO_A_MAÑANA_IDA"] = Math.Round(Convert.ToDouble(model.VEL_PROMEDIO_A_MAÑANA_IDA),2);
            dr["VEL_PROMEDIO_B_MAÑANA_IDA"] = Math.Round(Convert.ToDouble(model.VEL_PROMEDIO_B_MAÑANA_IDA),2);
            dr["VEL_PROMEDIO_A_TARDE_IDA"] = Math.Round(Convert.ToDouble(model.VEL_PROMEDIO_A_TARDE_IDA),2);
            dr["VEL_PROMEDIO_B_TARDE_IDA"] = Math.Round(Convert.ToDouble(model.VEL_PROMEDIO_B_TARDE_IDA),2);
            
            //TIEMPO PROMEDIO
            dr["TIEMPO_IDA"] = minutos_ida_m;
            dr["TIEMPO_VUELTA"] = minutos_vuelta_m;

            //VELOCIDAD PROMEDIO
            dr["VELOCIDAD_IDA_M"] = vel_ida_m;
            dr["VELOCIDAD_VUELTA_M"] = vel_vuelta_m;


            dr["FECHA_1"] = model.FECHA_INICIO;
            dr["FECHA_2"] = model.FECHA_FIN;

            dr["CORREDOR"] = model.CORREDOR;

            dtFormato.Rows.Add(dr);

            //CARGAR TABLAS.
            dsReporte.Tables["DataTable1"].Merge(dtFormato);
            /* GUARDAR LOS DATOS EN EL DATASET*/
            rd = new REPORTE_DESPACHO();
            rd.SetDataSource(dsReporte);
            /*CREAR PDF*/
            String resultado = "FORMATO_REPORTE_PICO_PLACA.pdf";
            System.IO.File.Delete(ruta_base + @"Download\" + resultado);
            rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, ruta_base + @"Download\" + resultado);
            rd.Close();
            return resultado;
        }

    }


}