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
using AD;

namespace LOGINPUA.Models
{
    public class ReportesBuses
    {

        private  BusesAD _loginRepositorio;
        private Object bdConn;

        public ReportesBuses()
        {
            BusesAD _loginRepositorio = new BusesAD(ref bdConn);
        }
        //public string Insertar_Buses_Despacho_Formato(string placa, string ruta_base, int codigo_despacho,string usuario)
        //{
        //    DataSet1 dsReporte = new DataSet1();

        //    _loginRepositorio = new BusesAD(ref bdConn);
        //    var Formato_Estado_Buses = _loginRepositorio.Buscar_Formato_Placa(placa, codigo_despacho);
        //    Conexion.finalizar(ref bdConn);
        //    _loginRepositorio = new BusesAD(ref bdConn);

        //    var Formato_Documentacion_Bus = _loginRepositorio.Buscar_Documentacion_Bus(placa, codigo_despacho);
        //    Conexion.finalizar(ref bdConn);
        //    _loginRepositorio = new BusesAD(ref bdConn);

        //    var Formato_Exterior_Bus = _loginRepositorio.Buscar_Exterior_Bus(placa, codigo_despacho);
        //    Conexion.finalizar(ref bdConn);
        //    _loginRepositorio = new BusesAD(ref bdConn);

        //    var Formato_Cabina_Bus = _loginRepositorio.Buscar_Cabina_Bus(placa, codigo_despacho);
        //    Conexion.finalizar(ref bdConn);


        //    //IMPORTAR TABLEADAPTER.

        //    var dtFormato = new DataSet1.ESTADO_BUSDataTable();
        //    var dtFormato2 = new DataSet1.TABLE_DOCUMENTACIÓN_VIGENTE_DENTRO_DEL_VEHÍCULODataTable();
        //    var dtFormato3 = new DataSet1.TABLE_EXTERIORDataTable();
        //    var dtFormato4 = new DataSet1.CABINADataTable();


        //    //RECORRER LISTAS.

        //    foreach (var item in Formato_Estado_Buses)
        //    {
        //        if (item.BD_KM==null) { item.BD_KM = 0; }
                 

        //        DateTime hora_now = DateTime.Now;
        //        string hora = hora_now.ToString("hh:mm");

        //        var dr = dtFormato.NewRow();
        //        dr["BS_PLACA"] = item.BS_PLACA;
        //        dr["BD_CONCESIONARIO"] = item.BD_CONCESIONARIO;
        //        dr["BD_DIRECCION"] = item.BD_DIRECCION;
        //        dr["FECHA"] = item.BD_FECHA;
        //        dr["HORA"] = hora;
        //        dr["BD_KM"] = item.BD_KM;
        //        dr["USUREG"] = item.USUREG;
 
        //        //CARGAR FOTOS



        //        string ruta_imagen1 = ruta_base + item.URL_FOTO.Replace("~/", "").Replace("/", "\\");
        //        if (File.Exists(ruta_imagen1))
        //        {
        //            byte[] bytes1 = System.IO.File.ReadAllBytes(ruta_imagen1);
        //            dr["FOTO1"] = bytes1;
        //        }


        //        string ruta_imagen2 = ruta_base + item.URL_FOTO2.Replace("~/", "").Replace("/", "\\");
        //        if (File.Exists(ruta_imagen2))
        //        {
        //            byte[] bytes2 = System.IO.File.ReadAllBytes(ruta_imagen2);
        //            dr["FOTO2"] = bytes2;
        //        }

        //        string ruta_imagen3 = ruta_base + item.URL_FOTO3.Replace("~/", "").Replace("/", "\\");
        //        if (File.Exists(ruta_imagen3))
        //        {
        //            byte[] bytes3 = System.IO.File.ReadAllBytes(ruta_imagen3);
        //            dr["FOTO3"] = bytes3;
        //        }


        //        string ruta_imagen4 = ruta_base + item.URL_FOTO4.Replace("~/", "").Replace("/", "\\");
        //        if (File.Exists(ruta_imagen4))
        //        {
        //            byte[] bytes4 = System.IO.File.ReadAllBytes(ruta_imagen4);
        //            dr["FOTO4"] = bytes4;
        //        }


        //        dtFormato.Rows.Add(dr);
        //    }
        //    foreach (var item in Formato_Documentacion_Bus)
        //    {
        //        var dr = dtFormato2.NewRow();
        //        dr["CD_CONCEPTOS"] = item.CD_CONCEPTOS;
        //        dr["BD_ESTADO"] = item.BD_ESTADO;
        //        dr["BD_OBSERVACION"] = item.BD_OBSERVACION;
        //        dr["CVS_FEC_FIN"] = item.CVS_FEC_FIN;
        //        dr["VEHICULOS_FIN"] = item.VEHICULOS_FIN;
        //        dr["RTV_FIN"] = item.RTV_FIN;
        //        dr["SOAT_FEC_FIN"] = item.SOAT_FEC_FIN;
        //        dr["RC_FIN"] = item.RC_FIN;
        //        dtFormato2.Rows.Add(dr);
        //    }
        //    foreach (var item in Formato_Exterior_Bus)
        //    {
        //        var dr = dtFormato3.NewRow();
        //        dr["CD_CONCEPTOS"] = item.CD_CONCEPTOS;
        //        dr["BD_ESTADO"] = item.BD_ESTADO;
        //        dr["BD_CALIDAD"] = item.BD_CALIDAD;
        //        dr["BD_OBSERVACION"] = item.BD_OBSERVACION;
        //        dtFormato3.Rows.Add(dr);
        //    }

        //    foreach (var item in Formato_Cabina_Bus)
        //    {
        //        var dr = dtFormato4.NewRow();
        //        dr["CD_CONCEPTOS"] = item.CD_CONCEPTOS;
        //        dr["BD_ESTADO"] = item.BD_ESTADO;
        //        dr["BD_CALIDAD"] = item.BD_CALIDAD;
        //        dr["BD_OBSERVACION"] = item.BD_OBSERVACION;
        //        dtFormato4.Rows.Add(dr);
        //    }

        //    //CARGAR TABLAS.

        //    dsReporte.Tables["ESTADO_BUS"].Merge(dtFormato);
        //    dsReporte.Tables["TABLE_DOCUMENTACIÓN VIGENTE DENTRO DEL VEHÍCULO"].Merge(dtFormato2);
        //    dsReporte.Tables["TABLE_EXTERIOR"].Merge(dtFormato3);
        //    dsReporte.Tables["CABINA"].Merge(dtFormato4);

        //    ReportDocument rd = new ReportDocument();


        //    /* GUARDAR LOS DATOS EN EL DATASET*/
        //    rd = new ESTADO_DESPACHO();
        //    rd.SetDataSource(dsReporte);



        //    /*CREAR PDF*/
             
        //    String resultado = "FORMATO"+usuario+".pdf";

        //    System.IO.File.Delete(ruta_base+ @"Download\" + resultado);


        //    rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, ruta_base + @"Download\" + resultado);

             
        //    rd.Close();

        //    return resultado;



        //}
    }
}