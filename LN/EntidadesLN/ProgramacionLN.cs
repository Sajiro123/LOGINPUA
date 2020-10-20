using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using AD;
using System.Web.Mvc;
using System.Web;
using System.Collections;
using System.IO;
using SpreadsheetLight;
using System.Linq;
using System.Transactions;
using System.Web.Script.Serialization;
using LOGINPUA.Util;
using System.Data;

namespace LN.EntidadesLN
{
    public class ProgramacionLN
    {
        private ProgramacionAD programacionAD;
        private Object bdConn;

        public ProgramacionLN()
        {
            programacionAD = new ProgramacionAD(ref bdConn);
        }
        public List<CC_DATOS_CORREDOR> obtenerDataCorredor(int idCorredor, ref string mensaje, ref int tipo)
        {
            List<CC_DATOS_CORREDOR> resultado = new List<CC_DATOS_CORREDOR>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = programacionAD.getDataCorredor(idCorredor);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;

        }
        public RPTA_GENERAL ValidarFecha_Programados(string fecha, int idServicio, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    Conexion.finalizar(ref bdConn);
                    resultado = programacionAD.ValidarFecha_Programados(fecha, idServicio);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }

            return resultado;

        }
        public List<CC_PROGRAMA_RUTA> getViajes_ProgrUnidades(string fecha, int id_ruta, string sentido, ref string mensaje, ref int tipo)
        {
            List<CC_PROGRAMA_RUTA> resultado = new List<CC_PROGRAMA_RUTA>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = programacionAD.getViajes_ProgrUnidades(fecha, id_ruta, sentido);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;

        }
        public List<CC_PROGRAMA_RUTA> getviajesProgrServ(string fecha, int id_ruta, string sentido, int idservicio, ref string mensaje, ref int tipo)
        {
            List<CC_PROGRAMA_RUTA> resultado = new List<CC_PROGRAMA_RUTA>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = programacionAD.getviajesProgrServ(fecha, id_ruta, sentido, idservicio);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }

        public RPTA_GENERAL Actulizar_ViajeTiempoReal(int idsalida, string unidad, int cac_temporal, string hora_salida, string observacion, string usureg_real, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = programacionAD.Actulizar_ViajeTiempoReal(idsalida, unidad, cac_temporal, hora_salida, observacion, usureg_real);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }

        public RPTA_GENERAL Actualizar_Incidencias(int id_maestro, string observacion, string horaejecutada, string usureg_real, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = programacionAD.Actualizar_Incidencias(id_maestro, observacion, horaejecutada, usureg_real);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }
        public RPTA_GENERAL Limpiar_ViajeDet(int id_maestro, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = programacionAD.Limpiar_ViajeDet(id_maestro);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }






        public RPTA_GENERAL AgregarCamposPOG(int idruta, string FnodeA, string TnodeA, string FnodeB, string TnodeB, string DistanciaA, string DistanciaB, string usuario, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = programacionAD.AgregarCamposPOG(idruta, FnodeA, TnodeA, FnodeB, TnodeB, DistanciaA, DistanciaB, usuario);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }
        public CC_PROGRAMA_RUTA getIdMaestro_x_fecha(int id_ruta, string fecha, ref string mensaje, ref int tipo)
        {
            CC_PROGRAMA_RUTA resultado = new CC_PROGRAMA_RUTA();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = programacionAD.getIdMaestro_x_fecha(id_ruta, fecha);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;

        }

        public RPTA_GENERAL verificarFormatoExcel(string rutaArchivo, HttpRequestBase rq, string fecha, int tipodia, int reemplazar, int idruta, int ruta_text,int semana)
        {
            RutaLN _rutaLN = new RutaLN();
            SalidaProgramadaLN _salidaProgramadaLN = new SalidaProgramadaLN();
            ProgramacionLN _ProgramacionLN = new ProgramacionLN();
            String mensaje = "";
            Int32 tipo = 0;
            int ID_MAESTRO_SALIDA_PROG = 0;
            var count = 0;
            int igual = 0;
            var count_fallidos = 0;
            string[] array_fecha = new string[10];
            var fecha_actual = fecha.Replace(" ", "");
            array_fecha = fecha_actual.Split('-');
            ArrayList array_id_maestro = new ArrayList(10);

            //
            RPTA_GENERAL e = new RPTA_GENERAL();
            using (FileStream fs = new FileStream(rutaArchivo, FileMode.Open))
            {
                SLDocument xlDoc = new SLDocument(fs);
                var hojasExcel = xlDoc.GetSheetNames();
                var encontroCabecera = false;
                var nombreHoja = "";
                var columnaInicioDetalle = 0;
                foreach (var hoja in hojasExcel)
                {
                    SLDocument sheet = new SLDocument(fs, hoja);
                    SLWorksheetStatistics stadisticasHoja = xlDoc.GetWorksheetStatistics();

                    for (int row = 0; row <= stadisticasHoja.EndRowIndex; row++) //verificando cabecera
                    {
                        var textoColumnaOperador = sheet.GetCellValueAsString(row, 4); //fila, columna
                        var textoColumnaTipodia = sheet.GetCellValueAsString(row, 2); //fila, columna

                        if (textoColumnaTipodia == "TIPO DIA" || textoColumnaOperador == "OPERADOR")
                        {
                            encontroCabecera = true;
                            nombreHoja = hoja.ToString();
                            columnaInicioDetalle = row + 1;
                        }

                        if (nombreHoja != "")
                        {
                            int ruta = sheet.GetCellValueAsInt32(row, 3);
                            //VALIDAR LA RUTA
                            if (ruta != ruta_text)
                            {
                                if (ruta != 0)
                                {
                                    e.COD_ESTADO = 0;
                                    e.DES_ESTADO = "Ingresar Correctamente la ruta";
                                    return e;
                                }
                            }

                        }
                    }
                }


                try
                {
                    //VERIFICA_DATA_MAESTRO_PROG
                    if (reemplazar == 0)
                    { //aceptó reemplazar la data
                        List<RPTA_GENERAL> Listafecha = new List<RPTA_GENERAL>();

                        string fechaexistenteBD = "";
                        HttpContext.Current.Session["id_maestrosalida"] = 0;
                        foreach (var item in array_fecha)
                        {
                            var rpta = _ProgramacionLN.ValidarFecha_Programados(item, idruta, ref mensaje, ref tipo);
                            //IDMAESTRO
                            HttpContext.Current.Session["id_maestrosalida"] = rpta.AUX;
                            if (rpta.DES_ESTADO != "NO EXISTE")
                            {
                                fechaexistenteBD = rpta.DES_ESTADO;
                                if (fechaexistenteBD == fecha)
                                {
                                    igual = 1;
                                }
                                break;
                            }
                        }

                        if (fechaexistenteBD != "" && igual == 0)
                        {
                            string[] array_fechaBD = new string[10];
                            var fecha_BD = fechaexistenteBD.Replace(" ", "");
                            array_fechaBD = fecha_BD.Split('-');


                            foreach (var item_array_fecha in array_fecha)
                            {
                                foreach (var item_array_fechaBD in array_fechaBD)
                                {
                                    RPTA_GENERAL RPT = new RPTA_GENERAL();

                                    if (item_array_fechaBD != item_array_fecha)
                                    {
                                        if (item_array_fechaBD != null)
                                        {
                                            RPT.DES_ESTADO = item_array_fechaBD;
                                        }
                                    }
                                    Listafecha.Add(RPT);
                                }
                            }
                        }

                        string fechas = "";
                        int cont = 0;

                        var List_eliminado_duplicados = (from item in Listafecha
                                                         group item.DES_ESTADO by item.DES_ESTADO into g
                                                         where g.Count() == 1
                                                         select g.Key).ToArray();

                        List<string> lista = List_eliminado_duplicados.ToList();

                        foreach (var item_Fechas in List_eliminado_duplicados)
                        {
                            if (item_Fechas != null)
                            {
                                fechas += item_Fechas + " - ";
                            }
                        }

                        if ((int)HttpContext.Current.Session["id_maestrosalida"] != 0)
                        {

                            fechas = fechas == "" ? fechaexistenteBD : fechas;

                            e.DES_ESTADO = fechas;
                            e.COD_ESTADO = 3;
                            return e;
                        }
                    }

                    //VALIDAR LA RUTA


                    if (reemplazar == 1)
                    {
                        //e = _salidaProgramadaLN.AnularMaestroSalida(e.AUX);

                        int id_maestro = (int)HttpContext.Current.Session["id_maestrosalida"];
                        if (id_maestro != 0)
                        {
                            e.COD_ESTADO = 1;
                        }

                        _salidaProgramadaLN.anular_maestro_prog(id_maestro, ref mensaje, ref tipo);
                        if (e.COD_ESTADO == 0)
                        { //valida si ocurrio un error al momento de anular los registros
                            return e;
                        }
                    }
                    List<CC_RUTA_TIPO_SERVICIO> arrayTipoSer = new List<CC_RUTA_TIPO_SERVICIO>();

                    var TipoServ = _rutaLN.getRutaSerOpe_X_modalidad(idruta, ref mensaje, ref tipo);

                    foreach (var item in TipoServ)
                    {
                        var item_ind = new CC_RUTA_TIPO_SERVICIO();
                        item_ind.ID_RUTA_TIPO_SERVICIO = item.ID_RUTA_TIPO_SERVICIO;
                        item_ind.NOMBRE = item.NOMBRE;
                        item_ind.ID_TIPO_SERVICIO = item.ID_TIPO_SERVICIO;
                        arrayTipoSer.Add(item_ind);
                    }



                    var rptaInseraCabecera = _salidaProgramadaLN.registrarMaestroSalidaProgramada(tipodia, idruta, fecha, semana, HttpContext.Current.Session["user_login"].ToString(), ref mensaje, ref tipo);
                    var id_maestro_salidaProgramada = 0;
                    if (rptaInseraCabecera.COD_ESTADO == 1)
                    {
                        id_maestro_salidaProgramada = rptaInseraCabecera.AUX;
                    }



                    //
                    if (nombreHoja != "")
                    { //encontro el excel con el formato
                        SLDocument hojaProgramacion = new SLDocument(fs, nombreHoja);
                        SLWorksheetStatistics hojaProgramacionStadistics = xlDoc.GetWorksheetStatistics();

                        var fechaTemporal = DateTime.Now.ToString("dd/MM/yyyy");
                        var fechaHoraTemporal = DateTime.Now.ToString("dd/MM/yyyy 12:00:00 tt");


                        //BUSCAR POR FECHA 


                        for (int row = columnaInicioDetalle; row <= hojaProgramacionStadistics.EndRowIndex; row++)
                        {
                            var hsalida = hojaProgramacion.GetCellValueAsString(row, 10).ToString() == "" ? "" : hojaProgramacion.GetCellValueAsDateTime(row, 10).ToString();

                            if (hsalida != "")//validar celdas vacias
                            {


                                var tipoDia = hojaProgramacion.GetCellValueAsString(row, 2).ToString();


                                var nroServicio = hojaProgramacion.GetCellValueAsString(row, 6).ToString();
                                var pog = hojaProgramacion.GetCellValueAsString(row, 7).ToString();
                                var pot = hojaProgramacion.GetCellValueAsString(row, 8).ToString() == "" ? "" : hojaProgramacion.GetCellValueAsDateTime(row, 8).ToString();
                                String[] arr_hora_pot;
                                if (pot != "") //para la hora de salida programada
                                {
                                    arr_hora_pot = pot.Split(' ');
                                    pot = arr_hora_pot[1];
                                }

                                var fnode = hojaProgramacion.GetCellValueAsString(row, 9).ToString();
                                var hllegada = hojaProgramacion.GetCellValueAsDateTime(row, 11).ToString();
                                String[] arr_hora_llegada_prog;

                                if (hsalida != "") //para la hora de salida programada
                                {
                                    hsalida = DateTime.Parse(hsalida).ToString("HH:mm:ss");
                                }

                                if (hllegada != "")//para la hora de llegada programada
                                {
                                    hllegada = DateTime.Parse(hllegada).ToString("HH:mm:ss");
                                }

                                var tnode = hojaProgramacion.GetCellValueAsString(row, 12).ToString();
                                var PIG = hojaProgramacion.GetCellValueAsString(row, 13).ToString();

                                var trip_time = hojaProgramacion.GetCellValueAsString(row, 16).ToString() == "" ? "" : hojaProgramacion.GetCellValueAsDateTime(row, 16).ToString();
                                if (trip_time != "")//para la hora de llegada programada
                                {
                                    trip_time = DateTime.Parse(trip_time).ToString("HH:mm:ss");
                                }
                                var distancia = hojaProgramacion.GetCellValueAsString(row, 17).ToString() == "" ? "" : hojaProgramacion.GetCellValueAsString(row, 17).ToString();


                                /**************************************************************************************************/
                                var layover = hojaProgramacion.GetCellValueAsString(row, 15).ToString() == "" ? "" : hojaProgramacion.GetCellValueAsDateTime(row, 15).ToString();
                                var layoverFormatoFecha = "";
                                String[] arr_layover;
                                double minutosDiferenciaLayover = 0.0;
                                if (layover != "")//para la hora de llegada programada
                                {
                                    arr_layover = layover.Split(' ');
                                    layover = arr_layover[1];//obteniendo la hora y AM/PM
                                    layoverFormatoFecha = fechaTemporal + " " + layover;//concatenando la fecha temporal + layover hora
                                    minutosDiferenciaLayover = DateTime.Parse(fechaTemporal).Subtract(DateTime.Parse(layoverFormatoFecha)).TotalMinutes;//obtiene diferencia en minutos
                                    minutosDiferenciaLayover = Math.Abs(minutosDiferenciaLayover);
                                }

                                /*************************************************************************************************/
                                var acumulado = "0";
                                var sentido = hojaProgramacion.GetCellValueAsString(row, 19).ToString();
                                var turno = hojaProgramacion.GetCellValueAsString(row, 21).ToString();


                                var tipoServicio = hojaProgramacion.GetCellValueAsString(row, 22).ToString();
                                int idtiposerv = 0;

                                foreach (var item in arrayTipoSer)
                                {
                                    if (item.NOMBRE == tipoServicio)
                                    {
                                        idtiposerv = item.ID_RUTA_TIPO_SERVICIO;
                                    }
                                }

                                var placa = hojaProgramacion.GetCellValueAsString(row, 23).ToString();
                                var cacConductor = hojaProgramacion.GetCellValueAsString(row, 24).ToString();

                                var registraDetalle = _salidaProgramadaLN.guardarMSalidaProgramadaDet(id_maestro_salidaProgramada, tipoDia, nroServicio, pog, pot, fnode, hsalida, hllegada, tnode, PIG, minutosDiferenciaLayover, acumulado, sentido, turno, idtiposerv, trip_time, distancia, placa, cacConductor, HttpContext.Current.Session["user_login"].ToString(), ref mensaje, ref tipo);


                                if (registraDetalle.COD_ESTADO == 0)
                                {
                                    e.COD_ESTADO = 0;
                                    e.DES_ESTADO = "Ingresar Correctamente los Tipos de Servicios";
                                    return e;
                                }

                                //insertar VIAJES DE DESPACHO 

                                array_id_maestro.Add(registraDetalle.AUX);

                                if (registraDetalle.COD_ESTADO == 1)
                                {
                                    count++;
                                }
                                else
                                {
                                    count_fallidos++;
                                }
                            }
                           
                        }
                        //INSERTAR VIAJES DESPACHO DE LA PROGRAMACION
                        foreach (var fechaitem in array_fecha)
                        {
                            //insertar los maestros
                            var ViajeMaestroDespacho = _salidaProgramadaLN.registrarProgrViajDespacho(id_maestro_salidaProgramada, fechaitem, HttpContext.Current.Session["user_login"].ToString(), ref mensaje, ref tipo);
                            int idViajeMaestro = ViajeMaestroDespacho.AUX;
                            foreach (int id_maestro_item in array_id_maestro)
                            {
                                //insetar los viajes con cada viaje 
                                _salidaProgramadaLN.guardarMViajesProgramadaDet(idViajeMaestro, id_maestro_item, HttpContext.Current.Session["user_login"].ToString(), ref mensaje, ref tipo);

                            }
                        }
                    }

                    else
                    { //no encontró el excel con el formato correcto

                    }
                }
                catch (TransactionAbortedException ex)
                {
                    e.COD_ESTADO = 0;
                    e.DES_ESTADO = "No se registraron los datos, ERROR->" + ex.Message;
                    e.AUX = count;
                    return e;
                }

                e.COD_ESTADO = 1;
                e.DES_ESTADO = "La programación se cargó correctamente y la Cantidad de Viajes es : " + count;
                e.AUX = count;
                return e;
            }
        }

        public RPTA_GENERAL ValidarFecha_Programados(string fecha, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();

            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = programacionAD.ValidarFecha_Programados(fecha);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }
  
        public RPTA_GENERAL nombre_zip(int id, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();

            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = programacionAD.nombre_zip(id);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }

        public RPTA_GENERAL eliminar_Archivo(int id, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();

            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = programacionAD.eliminar_Archivo(id);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }
        public RPTA_GENERAL RegistroInforme(string nombre, string fecha, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();

            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = programacionAD.RegistroInforme(nombre, fecha, SessionHelper.user_login);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;
        }

        public DataSet Listar_Informes(int mes, int año, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = programacionAD.Listar_Informes(mes, año);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                resultado = null;
            }
            Conexion.finalizar(ref bdConn);
            return resultado;

        }

    }
}
