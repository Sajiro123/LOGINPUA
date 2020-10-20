using System.Collections.Generic;
using ENTIDADES;
using AD.EntidadesAD;
using System;
using System.Data;
using AD;
using System.IO;
using SpreadsheetLight;
using LOGINPUA.Util;


namespace LN.EntidadesLN
{
    public class PasajeroValidacionLN
    {
        private Object bdConn;
        private PasajeroValidacionAD _PasajeroValidadacion;

        public PasajeroValidacionLN()
        {
            _PasajeroValidadacion = new PasajeroValidacionAD(ref bdConn);
        }


        public List<PasajeroVal> ListarcvsPasajeros(int idRuta, string fecha, int reemplazar, string abrevProveedorSistemasCorredor, string pathFinal,List<CC_PARADERO> lparadero, string usuario_session, ref string mensaje, ref int tipo)
        {
            List<PasajeroVal> Lista_Pasajero = new List<PasajeroVal>();

            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    //PARADEROS
                         try
                    {
                        using (FileStream fs = new FileStream(pathFinal, FileMode.Open))
                        {
                            SLDocument xlDoc = new SLDocument(fs);
                            var hojasExcel = xlDoc.GetSheetNames();
                            SLDocument hojaDespacho = new SLDocument(fs, hojasExcel[0]);//leyendo la hoja 
                            SLWorksheetStatistics hojaDespachoStadistics = hojaDespacho.GetWorksheetStatistics();
                            //MAESTRO PASAJERO VALIDACION
                            _PasajeroValidadacion = new PasajeroValidacionAD(ref bdConn);
                            var rpt_maestro = _PasajeroValidadacion.RegistrarMaestro(idRuta, fecha, usuario_session);
                            Conexion.finalizar(ref bdConn);
                            int id_maestro = rpt_maestro.AUX;

                            var count = 0;

                            for (int row = 5; row <= hojaDespachoStadistics.EndRowIndex; row++) //solo para obtener posicion de las cabeceras
                            {
                                if (hojaDespacho.GetCellValueAsString(row, 10).ToString() != "")
                                {
                                    var Id_ruta = hojaDespacho.GetCellValueAsString(row, 10).ToString();
                                    Id_ruta = Id_ruta.Substring(0, 3);

                                    if (abrevProveedorSistemasCorredor == Id_ruta)
                                    {
                                        var Hora_excel = hojaDespacho.GetCellValueAsDateTime(row, 3).ToString("HH:mm:00");
                                        var Tarjeta = hojaDespacho.GetCellValueAsString(row, 4).ToString();
                                        var Nombre_chofer = hojaDespacho.GetCellValueAsString(row, 5).ToString();
                                        var Perfil = hojaDespacho.GetCellValueAsString(row, 6).ToString();
                                        var Bus = hojaDespacho.GetCellValueAsString(row, 7).ToString();
                                        var Placa = hojaDespacho.GetCellValueAsString(row, 8).ToString();
                                        var Patron = hojaDespacho.GetCellValueAsString(row, 9).ToString();
                                        var operador = hojaDespacho.GetCellValueAsString(row, 11).ToString();
                                        var n_servicio = Convert.ToInt32(hojaDespacho.GetCellValueAsString(row, 12).ToString());
                                        var id_carrera = Convert.ToInt32(hojaDespacho.GetCellValueAsString(row, 13).ToString());

                                        var nombreEtiqueta = hojaDespacho.GetCellValueAsString(row, 14).ToString();
                                        int id_paradero = getIdParaderoByEtiqueta(nombreEtiqueta, lparadero, ref mensaje, ref tipo);

                                        var paradero = hojaDespacho.GetCellValueAsString(row, 15).ToString();
                                        var monto = Convert.ToDouble(hojaDespacho.GetCellValueAsString(row, 16).ToString());

                                        PasajeroVal Pasajero = new PasajeroVal();
                                        Pasajero.id_maestro = id_maestro;
                                        Pasajero.id_paradero = id_paradero;
                                        Pasajero.n_servicio = n_servicio;
                                        Pasajero.Placa = Placa;
                                        Pasajero.Hora_excel = Hora_excel;
                                        Pasajero.Tarjeta = Tarjeta;
                                        Pasajero.Nombre_chofer = Nombre_chofer;
                                        Pasajero.Perfil = Perfil;
                                        Pasajero.Patron = Patron;
                                        Pasajero.operador = operador;
                                        Pasajero.id_carrera = id_carrera;
                                        Pasajero.monto = monto;
                                        Pasajero.Bus = Bus;
                                        Pasajero.id_estado = 1;
                                        Pasajero.usuario_session = usuario_session;
                                        Pasajero.fecha_reg = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");

                                        count++;

                                        Lista_Pasajero.Add(Pasajero);
                                    }
                                }
                            }
                            return Lista_Pasajero;
                        }

                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Lista_Pasajero;
        }


        public RPTA_GENERAL ValidarExcel(string TipoCarga, int idRuta, string fecha, int reemplazar, string abrevProveedorSistemasCorredor, string pathFinal, List<CC_PARADERO> lparadero, string usuario_session, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL rpta = new RPTA_GENERAL();
            List<string> lista_num = new List<string>();
            int row = 0;
            bool fecha_estado = true;

            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    if (lparadero.Count == 0)
                    {
                        rpta.COD_ESTADO = 0;
                        rpta.DES_ESTADO = "No existe información de paraderos de esta ruta";
                        return rpta;
                    }

                    _PasajeroValidadacion = new PasajeroValidacionAD(ref bdConn);
                    rpta = verificarRegistroPasajeroValExistente(fecha, idRuta, ref mensaje, ref tipo);
                    Conexion.finalizar(ref bdConn);


                    if (TipoCarga == "I")
                    {

                        if (rpta.COD_ESTADO == 1 && reemplazar == 0)
                        { //aceptó reemplazar la data
                            rpta.COD_ESTADO = 3;
                            rpta.DES_ESTADO = fecha;
                            return rpta;
                        }
                        if (reemplazar == 1)
                        {
                            _PasajeroValidadacion = new PasajeroValidacionAD(ref bdConn);
                            rpta = _PasajeroValidadacion.AnularMaestroSalida(rpta.AUX);
                            Conexion.finalizar(ref bdConn);

                            if (rpta.COD_ESTADO == 0)
                            { //valida si ocurrio un error al momento de anular los registros
                                return rpta;
                            }
                        }
                    }
                    else
                    {
                        if (rpta.COD_ESTADO == 1)
                        {
                            _PasajeroValidadacion = new PasajeroValidacionAD(ref bdConn);
                            rpta = _PasajeroValidadacion.AnularMaestroSalida(rpta.AUX);
                            Conexion.finalizar(ref bdConn);
                        }
                    }

                    try
                    {
                        using (FileStream fs = new FileStream(pathFinal, FileMode.Open))
                        {
                            SLDocument xlDoc = new SLDocument(fs);
                            var hojasExcel = xlDoc.GetSheetNames();
                            SLDocument hojaDespacho = new SLDocument(fs, hojasExcel[0]);//leyendo la hoja 
                            SLWorksheetStatistics hojaDespachoStadistics = xlDoc.GetWorksheetStatistics();

                            for (row = 5; row <= hojaDespachoStadistics.EndRowIndex; row++) //solo para obtener posicion de las cabeceras
                            {
                                if (hojaDespacho.GetCellValueAsString(row, 10).ToString() != "")
                                {
                                    //VALIDAR FECHA
                                    if (fecha_estado == true)
                                    {
                                        var FECHA_SERVICIO = hojaDespacho.GetCellValueAsDateTime(row, 2).ToString("dd/MM/yyyy");
                                        fecha_estado = false;
                                        if (fecha != FECHA_SERVICIO)
                                        {
                                            rpta.COD_ESTADO = 0; rpta.DES_ESTADO = "Ingresar Correctamente la fecha";
                                            return rpta;
                                        }
                                    }

                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw;
                    }

                    rpta.COD_ESTADO = 1;
                    rpta.DES_ESTADO = "Se registro Correctamente";
                    return rpta;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return rpta;
        }

        public RPTA_GENERAL importarExcelPasajeros(int idRuta, string fecha, int reemplazar, string abrevProveedorSistemasCorredor, string pathFinal, List<CC_PARADERO> lparadero, string usuario_session, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL rpta = new RPTA_GENERAL();
            List<string> lista_num = new List<string>();
            int row = 0;
            bool fecha_estado = true;

            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    if (lparadero.Count == 0)
                    {
                        rpta.COD_ESTADO = 0;
                        rpta.DES_ESTADO = "No existe información de paraderos de esta ruta";
                        return rpta;
                    }

                    var hor04 = 0;
                    var hor05 = 0;
                    var hor06 = 0;
                    var hor07 = 0;
                    var hor08 = 0;
                    var hor09 = 0;
                    var hor10 = 0;
                    var hor11 = 0;
                    var hor12 = 0;
                    var hor13 = 0;
                    var hor14 = 0;
                    var hor15 = 0;
                    var hor16 = 0;
                    var hor17 = 0;
                    var hor18 = 0;
                    var hor19 = 0;
                    var hor20 = 0;
                    var hor21 = 0;
                    var hor22 = 0;
                    var hor23 = 0;

                    int[,] min_sum30 = new int[,] { };
                    int[,] min_sum10 = new int[,] { };

                    _PasajeroValidadacion = new PasajeroValidacionAD(ref bdConn);
                    rpta = verificarRegistroPasajeroValExistente(fecha, idRuta, ref mensaje, ref tipo);
                    Conexion.finalizar(ref bdConn);

                    if (rpta.COD_ESTADO == 1 && reemplazar == 0)
                    { //aceptó reemplazar la data
                        rpta.COD_ESTADO = 3;
                        rpta.DES_ESTADO = fecha;
                        return rpta;
                    }
                    if (reemplazar == 1)
                    {
                        _PasajeroValidadacion = new PasajeroValidacionAD(ref bdConn);
                        rpta = _PasajeroValidadacion.AnularMaestroSalida(rpta.AUX);
                        Conexion.finalizar(ref bdConn);

                        if (rpta.COD_ESTADO == 0)
                        { //valida si ocurrio un error al momento de anular los registros
                            return rpta;
                        }
                    }

                    try
                    {
                        using (FileStream fs = new FileStream(pathFinal, FileMode.Open))
                        {
                            SLDocument xlDoc = new SLDocument(fs);
                            var hojasExcel = xlDoc.GetSheetNames();
                            SLDocument hojaDespacho = new SLDocument(fs, hojasExcel[0]);//leyendo la hoja 
                            SLWorksheetStatistics hojaDespachoStadistics = xlDoc.GetWorksheetStatistics();


                            for (row = 5; row <= hojaDespachoStadistics.EndRowIndex; row++) //solo para obtener posicion de las cabeceras
                            {
                                if (hojaDespacho.GetCellValueAsString(row, 10).ToString() != "")
                                {
                                    //VALIDAR FECHA
                                    if (fecha_estado == true)
                                    {
                                        var FECHA_SERVICIO = hojaDespacho.GetCellValueAsDateTime(row, 2).ToString("dd/MM/yyyy");
                                        fecha_estado = false;
                                        if (fecha != FECHA_SERVICIO)
                                        {
                                            rpta.COD_ESTADO = 0; rpta.DES_ESTADO = "Ingresar Correctamente la fecha";
                                            return rpta;
                                        }
                                    }

                                }
                            }

                            bool estado_id_paradero = true;

                            //MAESTRO PASAJERO VALIDACION
                            _PasajeroValidadacion = new PasajeroValidacionAD(ref bdConn);
                            var rpt_maestro = _PasajeroValidadacion.RegistrarMaestro(idRuta, fecha, usuario_session);
                            Conexion.finalizar(ref bdConn);
                            int id_maestro = rpt_maestro.AUX;
                            //VALIDADCION DE PARADEROS
                            var id_paradero = 0;

                            var count_datos = 0;

                            foreach (var item in lparadero)
                            {





                                hor04 = 0;
                                hor05 = 0;
                                hor06 = 0;
                                hor07 = 0;
                                hor08 = 0;
                                hor09 = 0;
                                hor10 = 0;
                                hor11 = 0;
                                hor12 = 0;
                                hor13 = 0;
                                hor14 = 0;
                                hor15 = 0;
                                hor16 = 0;
                                hor17 = 0;
                                hor18 = 0;
                                hor19 = 0;
                                hor20 = 0;
                                hor21 = 0;
                                hor22 = 0;
                                hor23 = 0;

                                min_sum30 = new int[,]{
                            {0, 0},//4 position [0]
                            {0, 0},//5 position [1]
                            {0, 0},//6 position [2]
                            {0, 0},//7 position [3]
                            {0, 0},//8 position [4]
                            {0, 0},//9 position [5]
                            {0, 0},//10 position [6]
                            {0, 0},//11 position [7]
                            {0, 0},//12 position [8]
                            {0, 0},//13 position [9]
                            {0, 0},//14 position [10]
                            {0, 0},//15 position [11]
                            {0, 0},//16 position [12]
                            {0, 0},//17 position [13]
                            {0, 0},//18 position [14]
                            {0, 0},//19 position [15]
                            {0, 0},//20 position [16]
                            {0, 0},//21 position [17]
                            {0, 0},//22 position [18]
                            {0, 0},//23 position [19]
                        };

                                min_sum10 = new int[,]{
                            {0, 0, 0, 0, 0, 0},//4 position [0]
                            {0, 0, 0, 0, 0, 0},//5 position [1]
                            {0, 0, 0, 0, 0, 0},//6 position [2]
                            {0, 0, 0, 0, 0, 0},//7 position [3]
                            {0, 0, 0, 0, 0, 0},//8 position [4]
                            {0, 0, 0, 0, 0, 0},//9 position [5]
                            {0, 0, 0, 0, 0, 0},//10 position [6]
                            {0, 0, 0, 0, 0, 0},//11 position [7]
                            {0, 0, 0, 0, 0, 0},//12 position [8]
                            {0, 0, 0, 0, 0, 0},//13 position [9]
                            {0, 0, 0, 0, 0, 0},//14 position [10]
                            {0, 0, 0, 0, 0, 0},//15 position [11]
                            {0, 0, 0, 0, 0, 0},//16 position [12]
                            {0, 0, 0, 0, 0, 0},//17 position [13]
                            {0, 0, 0, 0, 0, 0},//18 position [14]
                            {0, 0, 0, 0, 0, 0},//19 position [15]
                            {0, 0, 0, 0, 0, 0},//20 position [16]
                            {0, 0, 0, 0, 0, 0},//21 position [17]
                            {0, 0, 0, 0, 0, 0},//22 position [18]
                            {0, 0, 0, 0, 0, 0},//23 position [19]
                        };

                                for (row = 5; row <= hojaDespachoStadistics.EndRowIndex; row++)
                                {
                                    if (hojaDespacho.GetCellValueAsString(row, 10).ToString() != "")
                                    {
                                        var nombreEtiqueta = hojaDespacho.GetCellValueAsString(row, 13).ToString();
                                        var RutaExcel = (hojaDespacho.GetCellValueAsString(row, 10).ToString());
                                        RutaExcel = RutaExcel.Substring(0, 3);

                                        if (abrevProveedorSistemasCorredor == RutaExcel)
                                        {
                                            if (nombreEtiqueta == item.ETIQUETA_NOMBRE)
                                            {
                                                if (estado_id_paradero)
                                                {
                                                    id_paradero = getIdParaderoByEtiqueta(nombreEtiqueta, lparadero, ref mensaje, ref tipo);
                                                    estado_id_paradero = false;
                                                }

                                                //SUMA DE PASAJEROS POR PARADERO                              
                                                var Hora_excel = hojaDespacho.GetCellValueAsDateTime(row, 3).ToString("HH:mm:00");
                                                int hh = Convert.ToInt32(Hora_excel.Substring(0, 2));
                                                int mm = Convert.ToInt32(Hora_excel.Substring(3, 2));

                                                if (hh == 4)
                                                {
                                                    hor04++;
                                                    if (mm <= 30)
                                                    {
                                                        min_sum30[0, 0]++;
                                                        if (mm <= 10)
                                                        {
                                                            min_sum10[0, 0]++;
                                                        }
                                                        else if (mm <= 20)
                                                        {
                                                            min_sum10[0, 1]++;
                                                        }
                                                        else if (mm <= 30)
                                                        {
                                                            min_sum10[0, 2]++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        min_sum30[0, 1]++;
                                                        if (mm <= 40)
                                                        {
                                                            min_sum10[0, 3]++;
                                                        }
                                                        else if (mm <= 50)
                                                        {
                                                            min_sum10[0, 4]++;
                                                        }
                                                        else if (mm <= 60)
                                                        {
                                                            min_sum10[0, 5]++;
                                                        }
                                                    }
                                                }

                                                else if (hh == 5)
                                                {
                                                    hor05++; if (mm <= 30)
                                                    {
                                                        min_sum30[1, 0]++; if (mm <= 10) { min_sum10[1, 0]++; } else if (mm <= 20) { min_sum10[1, 1]++; } else if (mm <= 30) { min_sum10[1, 2]++; }
                                                    }
                                                    else { min_sum30[1, 1]++; if (mm <= 40) { min_sum10[1, 3]++; } else if (mm <= 50) { min_sum10[1, 4]++; } else if (mm <= 60) { min_sum10[1, 5]++; } }
                                                }

                                                else if (hh == 6)
                                                {
                                                    hor06++; if (mm <= 30)
                                                    {
                                                        min_sum30[2, 0]++; if (mm <= 10) { min_sum10[2, 0]++; } else if (mm <= 20) { min_sum10[2, 1]++; } else if (mm <= 30) { min_sum10[2, 2]++; }
                                                    }
                                                    else { min_sum30[2, 1]++; if (mm <= 40) { min_sum10[2, 3]++; } else if (mm <= 50) { min_sum10[2, 4]++; } else if (mm <= 60) { min_sum10[2, 5]++; } }
                                                }

                                                else if (hh == 7)
                                                {
                                                    hor07++; if (mm <= 30)
                                                    {
                                                        min_sum30[3, 0]++; if (mm <= 10) { min_sum10[3, 0]++; } else if (mm <= 20) { min_sum10[3, 1]++; } else if (mm <= 30) { min_sum10[3, 2]++; }
                                                    }
                                                    else { min_sum30[3, 1]++; if (mm <= 40) { min_sum10[3, 3]++; } else if (mm <= 50) { min_sum10[3, 4]++; } else if (mm <= 60) { min_sum10[3, 5]++; } }
                                                }

                                                else if (hh == 8)
                                                {
                                                    hor08++; if (mm <= 30)
                                                    {
                                                        min_sum30[4, 0]++; if (mm <= 10) { min_sum10[4, 0]++; } else if (mm <= 20) { min_sum10[4, 1]++; } else if (mm <= 30) { min_sum10[4, 2]++; }
                                                    }
                                                    else { min_sum30[4, 1]++; if (mm <= 40) { min_sum10[4, 3]++; } else if (mm <= 50) { min_sum10[4, 4]++; } else if (mm <= 60) { min_sum10[4, 5]++; } }
                                                }

                                                else if (hh == 9)
                                                {
                                                    hor09++; if (mm <= 30)
                                                    {
                                                        min_sum30[5, 0]++; if (mm <= 10) { min_sum10[5, 0]++; } else if (mm <= 20) { min_sum10[5, 1]++; } else if (mm <= 30) { min_sum10[5, 2]++; }
                                                    }
                                                    else { min_sum30[5, 1]++; if (mm <= 40) { min_sum10[5, 3]++; } else if (mm <= 50) { min_sum10[5, 4]++; } else if (mm <= 60) { min_sum10[5, 5]++; } }
                                                }

                                                else if (hh == 10)
                                                {
                                                    hor10++; if (mm <= 30)
                                                    {
                                                        min_sum30[6, 0]++; if (mm <= 10) { min_sum10[6, 0]++; } else if (mm <= 20) { min_sum10[6, 1]++; } else if (mm <= 30) { min_sum10[6, 2]++; }
                                                    }
                                                    else { min_sum30[6, 1]++; if (mm <= 40) { min_sum10[6, 3]++; } else if (mm <= 50) { min_sum10[6, 4]++; } else if (mm <= 60) { min_sum10[6, 5]++; } }
                                                }

                                                else if (hh == 11)
                                                {
                                                    hor11++; if (mm <= 30)
                                                    {
                                                        min_sum30[7, 0]++; if (mm <= 10) { min_sum10[7, 0]++; } else if (mm <= 20) { min_sum10[7, 1]++; } else if (mm <= 30) { min_sum10[7, 2]++; }
                                                    }
                                                    else { min_sum30[7, 1]++; if (mm <= 40) { min_sum10[7, 3]++; } else if (mm <= 50) { min_sum10[7, 4]++; } else if (mm <= 60) { min_sum10[7, 5]++; } }
                                                }

                                                else if (hh == 12)
                                                {
                                                    hor12++; if (mm <= 30)
                                                    {
                                                        min_sum30[8, 0]++; if (mm <= 10) { min_sum10[8, 0]++; } else if (mm <= 20) { min_sum10[8, 1]++; } else if (mm <= 30) { min_sum10[8, 2]++; }
                                                    }
                                                    else { min_sum30[8, 1]++; if (mm <= 40) { min_sum10[8, 3]++; } else if (mm <= 50) { min_sum10[8, 4]++; } else if (mm <= 60) { min_sum10[8, 5]++; } }
                                                }

                                                else if (hh == 13)
                                                {
                                                    hor13++; if (mm <= 30)
                                                    {
                                                        min_sum30[9, 0]++; if (mm <= 10) { min_sum10[9, 0]++; } else if (mm <= 20) { min_sum10[9, 1]++; } else if (mm <= 30) { min_sum10[9, 2]++; }
                                                    }
                                                    else { min_sum30[9, 1]++; if (mm <= 40) { min_sum10[9, 3]++; } else if (mm <= 50) { min_sum10[9, 4]++; } else if (mm <= 60) { min_sum10[9, 5]++; } }
                                                }

                                                else if (hh == 14)
                                                {
                                                    hor14++; if (mm <= 30)
                                                    {
                                                        min_sum30[10, 0]++; if (mm <= 10) { min_sum10[10, 0]++; } else if (mm <= 20) { min_sum10[10, 1]++; } else if (mm <= 30) { min_sum10[10, 2]++; }
                                                    }
                                                    else { min_sum30[10, 1]++; if (mm <= 40) { min_sum10[10, 3]++; } else if (mm <= 50) { min_sum10[10, 4]++; } else if (mm <= 60) { min_sum10[10, 5]++; } }
                                                }

                                                else if (hh == 15)
                                                {
                                                    hor15++; if (mm <= 30)
                                                    {
                                                        min_sum30[11, 0]++; if (mm <= 10) { min_sum10[11, 0]++; } else if (mm <= 20) { min_sum10[11, 1]++; } else if (mm <= 30) { min_sum10[11, 2]++; }
                                                    }
                                                    else { min_sum30[11, 1]++; if (mm <= 40) { min_sum10[11, 3]++; } else if (mm <= 50) { min_sum10[11, 4]++; } else if (mm <= 60) { min_sum10[11, 5]++; } }
                                                }

                                                else if (hh == 16)
                                                {
                                                    hor16++; if (mm <= 30)
                                                    {
                                                        min_sum30[12, 0]++; if (mm <= 10) { min_sum10[12, 0]++; } else if (mm <= 20) { min_sum10[12, 1]++; } else if (mm <= 30) { min_sum10[12, 2]++; }
                                                    }
                                                    else { min_sum30[12, 1]++; if (mm <= 40) { min_sum10[12, 3]++; } else if (mm <= 50) { min_sum10[12, 4]++; } else if (mm <= 60) { min_sum10[12, 5]++; } }
                                                }

                                                else if (hh == 17)
                                                {
                                                    hor17++; if (mm <= 30)
                                                    {
                                                        min_sum30[13, 0]++; if (mm <= 10) { min_sum10[13, 0]++; } else if (mm <= 20) { min_sum10[13, 1]++; } else if (mm <= 30) { min_sum10[13, 2]++; }
                                                    }
                                                    else { min_sum30[13, 1]++; if (mm <= 40) { min_sum10[13, 3]++; } else if (mm <= 50) { min_sum10[13, 4]++; } else if (mm <= 60) { min_sum10[13, 5]++; } }
                                                }

                                                else if (hh == 18)
                                                {
                                                    hor18++; if (mm <= 30)
                                                    {
                                                        min_sum30[14, 0]++; if (mm <= 10) { min_sum10[14, 0]++; } else if (mm <= 20) { min_sum10[14, 1]++; } else if (mm <= 30) { min_sum10[14, 2]++; }
                                                    }
                                                    else { min_sum30[14, 1]++; if (mm <= 40) { min_sum10[14, 3]++; } else if (mm <= 50) { min_sum10[14, 4]++; } else if (mm <= 60) { min_sum10[14, 5]++; } }
                                                }

                                                else if (hh == 19)
                                                {
                                                    hor19++; if (mm <= 30)
                                                    {
                                                        min_sum30[15, 0]++; if (mm <= 10) { min_sum10[15, 0]++; } else if (mm <= 20) { min_sum10[15, 1]++; } else if (mm <= 30) { min_sum10[15, 2]++; }
                                                    }
                                                    else { min_sum30[15, 1]++; if (mm <= 40) { min_sum10[15, 3]++; } else if (mm <= 50) { min_sum10[15, 4]++; } else if (mm <= 60) { min_sum10[15, 5]++; } }
                                                }

                                                else if (hh == 20)
                                                {
                                                    hor20++; if (mm <= 30)
                                                    {
                                                        min_sum30[16, 0]++; if (mm <= 10) { min_sum10[16, 0]++; } else if (mm <= 20) { min_sum10[16, 1]++; } else if (mm <= 30) { min_sum10[16, 2]++; }
                                                    }
                                                    else { min_sum30[16, 1]++; if (mm <= 40) { min_sum10[16, 3]++; } else if (mm <= 50) { min_sum10[16, 4]++; } else if (mm <= 60) { min_sum10[16, 5]++; } }
                                                }

                                                else if (hh == 21)
                                                {
                                                    hor21++; if (mm <= 30)
                                                    {
                                                        min_sum30[17, 0]++; if (mm <= 10) { min_sum10[17, 0]++; } else if (mm <= 20) { min_sum10[17, 1]++; } else if (mm <= 30) { min_sum10[17, 2]++; }
                                                    }
                                                    else { min_sum30[17, 1]++; if (mm <= 40) { min_sum10[17, 3]++; } else if (mm <= 50) { min_sum10[17, 4]++; } else if (mm <= 60) { min_sum10[17, 5]++; } }
                                                }

                                                else if (hh == 22)
                                                {
                                                    hor22++; if (mm <= 30)
                                                    {
                                                        min_sum30[18, 0]++; if (mm <= 10) { min_sum10[18, 0]++; } else if (mm <= 20) { min_sum10[18, 1]++; } else if (mm <= 30) { min_sum10[18, 2]++; }
                                                    }
                                                    else { min_sum30[18, 1]++; if (mm <= 40) { min_sum10[18, 3]++; } else if (mm <= 50) { min_sum10[18, 4]++; } else if (mm <= 60) { min_sum10[18, 5]++; } }
                                                }

                                                else if (hh == 23)
                                                {
                                                    hor23++; if (mm <= 30)
                                                    {
                                                        min_sum30[19, 0]++; if (mm <= 10) { min_sum10[19, 0]++; } else if (mm <= 20) { min_sum10[19, 1]++; } else if (mm <= 30) { min_sum10[19, 2]++; }
                                                    }
                                                    else { min_sum30[19, 1]++; if (mm <= 40) { min_sum10[19, 3]++; } else if (mm <= 50) { min_sum10[19, 4]++; } else if (mm <= 60) { min_sum10[19, 5]++; } }
                                                }

                                            }
                                        }

                                    }
                                }
                                estado_id_paradero = true;
                                if (id_paradero > 0)
                                {

                                    count_datos++;

                                    //REGISTRAR  PASAJERO X HORA 
                                    _PasajeroValidadacion = new PasajeroValidacionAD(ref bdConn);
                                    //X HORA
                                    rpta = _PasajeroValidadacion.RegistrarDetall_Hora(id_maestro, id_paradero, hor04, hor05, hor06, hor07, hor08, hor09, hor10, hor11, hor12, hor13, hor14, hor15, hor16, hor17, hor18, hor19, hor20, hor21, hor22, hor23, usuario_session);
                                    //X CADA 30 MINUTOS
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[0, 0], min_sum30[0, 1], 4, usuario_session);//04
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[1, 0], min_sum30[1, 1], 5, usuario_session);//05
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[2, 0], min_sum30[2, 1], 6, usuario_session);//06
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[3, 0], min_sum30[3, 1], 7, usuario_session);//07
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[4, 0], min_sum30[4, 1], 8, usuario_session);//08
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[5, 0], min_sum30[5, 1], 9, usuario_session);//09
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[6, 0], min_sum30[6, 1], 10, usuario_session);//10
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[7, 0], min_sum30[7, 1], 11, usuario_session);//11
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[8, 0], min_sum30[8, 1], 12, usuario_session);//12
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[9, 0], min_sum30[9, 1], 13, usuario_session);//13
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[10, 0], min_sum30[10, 1], 14, usuario_session);//14
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[11, 0], min_sum30[11, 1], 15, usuario_session);//15
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[12, 0], min_sum30[12, 1], 16, usuario_session);//16
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[13, 0], min_sum30[13, 1], 17, usuario_session);//17
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[14, 0], min_sum30[14, 1], 18, usuario_session);//18
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[15, 0], min_sum30[15, 1], 19, usuario_session);//19
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[16, 0], min_sum30[16, 1], 20, usuario_session);//20
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[17, 0], min_sum30[17, 1], 21, usuario_session);//21
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[18, 0], min_sum30[18, 1], 22, usuario_session);//22
                                    _PasajeroValidadacion.RegistrarDetall_30min(id_maestro, id_paradero, min_sum30[19, 0], min_sum30[19, 1], 23, usuario_session);//23


                                    //X CADA 10 MINUTOS
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[0, 0], min_sum10[0, 1], min_sum10[0, 2], min_sum10[0, 3], min_sum10[0, 4], min_sum10[0, 5], 4, usuario_session);//04
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[1, 0], min_sum10[1, 1], min_sum10[1, 2], min_sum10[1, 3], min_sum10[1, 4], min_sum10[1, 5], 5, usuario_session);//05
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[2, 0], min_sum10[2, 1], min_sum10[2, 2], min_sum10[2, 3], min_sum10[2, 4], min_sum10[2, 5], 6, usuario_session);//06
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[3, 0], min_sum10[3, 1], min_sum10[3, 2], min_sum10[3, 3], min_sum10[3, 4], min_sum10[3, 5], 7, usuario_session);//07
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[4, 0], min_sum10[4, 1], min_sum10[4, 2], min_sum10[4, 3], min_sum10[4, 4], min_sum10[4, 5], 8, usuario_session);//08
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[5, 0], min_sum10[5, 1], min_sum10[5, 2], min_sum10[5, 3], min_sum10[5, 4], min_sum10[5, 5], 9, usuario_session);//09
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[6, 0], min_sum10[6, 1], min_sum10[6, 2], min_sum10[6, 3], min_sum10[6, 4], min_sum10[6, 5], 10, usuario_session);//10
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[7, 0], min_sum10[7, 1], min_sum10[7, 2], min_sum10[7, 3], min_sum10[7, 4], min_sum10[7, 5], 11, usuario_session);//11
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[8, 0], min_sum10[8, 1], min_sum10[8, 2], min_sum10[8, 3], min_sum10[8, 4], min_sum10[8, 5], 12, usuario_session);//12
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[9, 0], min_sum10[9, 1], min_sum10[9, 2], min_sum10[9, 3], min_sum10[9, 4], min_sum10[9, 5], 13, usuario_session);//13
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[10, 0], min_sum10[10, 1], min_sum10[10, 2], min_sum10[10, 3], min_sum10[10, 4], min_sum10[10, 5], 14, usuario_session);//14
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[11, 0], min_sum10[11, 1], min_sum10[11, 2], min_sum10[11, 3], min_sum10[11, 4], min_sum10[11, 5], 15, usuario_session);//15
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[12, 0], min_sum10[12, 1], min_sum10[12, 2], min_sum10[12, 3], min_sum10[12, 4], min_sum10[12, 5], 16, usuario_session);//16
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[13, 0], min_sum10[13, 1], min_sum10[13, 2], min_sum10[13, 3], min_sum10[13, 4], min_sum10[13, 5], 17, usuario_session);//17
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[14, 0], min_sum10[14, 1], min_sum10[14, 2], min_sum10[14, 3], min_sum10[14, 4], min_sum10[14, 5], 18, usuario_session);//18
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[15, 0], min_sum10[15, 1], min_sum10[15, 2], min_sum10[15, 3], min_sum10[15, 4], min_sum10[15, 5], 19, usuario_session);//19
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[16, 0], min_sum10[16, 1], min_sum10[16, 2], min_sum10[16, 3], min_sum10[16, 4], min_sum10[16, 5], 20, usuario_session);//20
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[17, 0], min_sum10[17, 1], min_sum10[17, 2], min_sum10[17, 3], min_sum10[17, 4], min_sum10[17, 5], 21, usuario_session);//21
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[18, 0], min_sum10[18, 1], min_sum10[18, 2], min_sum10[18, 3], min_sum10[18, 4], min_sum10[18, 5], 22, usuario_session);//22
                                    _PasajeroValidadacion.RegistrarDetall_10min(id_maestro, id_paradero, min_sum10[19, 0], min_sum10[19, 1], min_sum10[19, 2], min_sum10[19, 3], min_sum10[19, 4], min_sum10[19, 5], 23, usuario_session);//23

                                    Conexion.finalizar(ref bdConn);
                                    id_paradero = 0;
                                }


                            }

                            if (count_datos > 10)
                            {
                                return rpta;
                            }
                            else
                            {
                                _PasajeroValidadacion = new PasajeroValidacionAD(ref bdConn);
                                rpta = _PasajeroValidadacion.AnularMaestroSalida(rpt_maestro.AUX);
                                Conexion.finalizar(ref bdConn);

                                RPTA_GENERAL rpta_total = new RPTA_GENERAL();
                                rpta_total.COD_ESTADO = 0;
                                rpta_total.DES_ESTADO = "Ingresar el formato Correcto";
                                return rpta_total;
                            }

                        }

                    }
                    catch (Exception e)
                    {
                        string mensaje2 = e.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
                rpta = null;
            }
            Conexion.finalizar(ref bdConn);
            return rpta;

        }


        public DataSet Listarfechas_ruta_franjas(int franja, int idruta, string inicio, string fin, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _PasajeroValidadacion.Listarfechas_ruta_franjas(franja, idruta, inicio, fin);
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

        public DataSet listar_paraderos_ruta_val(int franja, int id_ruta, string fecha, string lado, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _PasajeroValidadacion.listar_paraderos_ruta_val(franja, id_ruta, fecha, lado);
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

        public DataSet Calcular_franja(int id_ruta, string fecha, string lado, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _PasajeroValidadacion.Calcular_franja(id_ruta, fecha, lado);
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

        public DataSet Listarvalida_ruta_franjas(int franja, string fecha, ref string mensaje, ref int tipo)
        {
            DataSet resultado = new DataSet();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _PasajeroValidadacion.Listarvalida_ruta_franjas(franja, fecha);
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

        public int getIdParaderoByEtiqueta(string nombreEtiqueta, List<CC_PARADERO> lparadero, ref string mensaje, ref int tipo)
        {
            var id_paradero = 0;
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    foreach (var item in lparadero)
                    {
                        if (nombreEtiqueta == item.ETIQUETA_NOMBRE)
                        {
                            id_paradero = item.ID_PARADERO;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
            }
            Conexion.finalizar(ref bdConn);
            return id_paradero;
        }

        public RPTA_GENERAL verificarRegistroPasajeroValExistente(string fechaConsulta, int idRuta, ref string mensaje, ref int tipo)
        {
            RPTA_GENERAL resultado = new RPTA_GENERAL();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    var data = _PasajeroValidadacion.verificarRegistroPasajeroValExistente(idRuta, fechaConsulta);
                    if (data.COD_ESTADO > 0)
                    {
                        resultado.COD_ESTADO = 1;
                        resultado.DES_ESTADO = "Si Existe Información";
                        resultado.AUX = data.AUX;
                    }
                    else
                    {
                        resultado.COD_ESTADO = 0;
                        resultado.DES_ESTADO = "No hay info en esta fecha";
                    }
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
        public List<CC_REPORTE_DESPACHO> Consultar_DespachoFecha(int mes_año, int año, string idruta, ref string mensaje, ref int tipo)
        {
            List<CC_REPORTE_DESPACHO> resultado = new List<CC_REPORTE_DESPACHO>();
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    resultado = _PasajeroValidadacion.Consultar_DespachoFecha(mes_año, año, idruta);
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

        public void Agregar_Temporal_POG(int id, string lado, string intervalo, ref string mensaje, ref int tipo)
        {
            try
            {
                if (SessionHelper.sesion_valida(ref mensaje, ref tipo))
                {
                    _PasajeroValidadacion.Agregar_Temporal_POG(id, lado, intervalo);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                tipo = 0;
            }
            Conexion.finalizar(ref bdConn);

        }
    }
}

