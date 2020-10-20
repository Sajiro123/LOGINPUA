
var util = new Util()
$(document).ready(function () {
    getCorredoresByModalidad();


    $('#rangoFechaConsulta').daterangepicker({
        //startDate: FECHA_HOY,

        //maxDate: FECHA_HOY,
        //startDate: '14/02/2020',
        //endDate: '08/05/2020',
        timePicker: false,
        //startDate: moment().startOf('hour'),
        //endDate: moment().startOf('hour').add(7, 'days'),
        //"disabledDays": new Array('02/11/2019', '03/11/2019'),
        "opens": "center",
        locale: { format: 'DD/MM/YYYY', "daysOfWeek": ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"], "monthNames": ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"] }
    });


    $('#txtFecha').datepicker({
        endDate: new Date(FECHA_HOY.split('/')[1] + "/" + FECHA_HOY.split('/')[0] + "/" + FECHA_HOY.split('/')[2]),
        format: "dd/mm/yyyy"
    });

    $('#txtFecha').parent().parent().css('display', 'none')
    $('#selectLado').parent().parent().css('display', 'none')
    $('#selectParaderoIni').parent().parent().css('display', 'none')
    $('#selectParaderoFin').parent().parent().css('display', 'none')




});

function getRutaPorCorredor() {
    $.ajax({
        url: URL_GET_RUTA_X_CORREDOR,
        dataType: 'json',
        data: { idCorredor: $('#selectCorredores').val() },
        success: function (result) {
            $('#selectRutaConsulta').empty();
            if (result.length == 0) { // si la lista esta vacia
                $('#selectRutaConsulta').append('<option value="0">' + '--No hay información--' + '</option>');

            } else {
                $.each(result, function () {
                    $('#selectRutaConsulta').append('<option value="' + this.ID_RUTA + '">' + this.NRO_RUTA + '</option>');
                });
            }
            cambioDeRuta();
        }

    }, JSON);
}

function getRecorridoXRuta() {
    $.ajax({
        url: URL_GET_RECORRIDO_X_RUTA,
        dataType: 'json',
        data: { idRuta: $('#selectRuta').val() },
        success: function (result) {
            DATA_RECORRIDOS = result;
            $('#selectLado').empty();
            var strCodRecorrido = '';
            if (result.length == 0) { // si la lista esta vacia
                $('#selectLado').append('<option value="0">' + '--No hay información--' + '</option>');
            } else {
                $.each(result, function () {
                    $('#selectLado').append('<option value="' + this.ID_RECORRIDO + '">' + this.LADO + '</option>');
                    strCodRecorrido += this.ID_RECORRIDO + '|';
                });

            }

        }
    }, JSON);
}


function getCorredoresByModalidad() {
    var modalidadTransporteSelecionado = $('#mod').val();
    $('#selectCorredores').empty();
    var cantidadRegistros = 0;
    $.each(JSON_DATA_CORREDORES, function () {

        if (this.ID_MODALIDAD_TRANS == Number(modalidadTransporteSelecionado)) {
            cantidadRegistros++;
            $('#selectCorredores').append('<option value="' + this.ID_CORREDOR + '">' + this.ABREVIATURA + '</option>');
        }
    });
    if (cantidadRegistros == 0) {
        $('#selectCorredores').append('<option value="0">' + '-- No hay información --' + '</option>');
        return false
    }
    getRutaPorCorredor();

}

function verificarLadoxIdRecorrido(idRecorrido) {
    var rptaLado = '';
    $.each(DATA_RECORRIDOS, function () {
        if (this.ID_RECORRIDO == idRecorrido) {
            rptaLado = this.LADO;
        }
    });
    return rptaLado;
}

var FECHA_TEMP = '27/02/2020';

function Listar_pasajeros_filtro() {


    $('#tablaGeneral').children('caption').remove()


    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');

    var fechaConsultaInicio = $('#rangoFechaConsulta').val().split('-')[0].trim();
    var fechaConsultaFin = $('#rangoFechaConsulta').val().split('-')[1].trim();
    var fecha = $('#txtFecha').val()
    var id_ruta = $('#selectRutaConsulta').val()

    $('#tablaGeneral thead').empty();
    $('#tablaGeneral tbody').empty();

    var idfiltro = $('#idfiltro_primero').val()
    var strHTML = "";


    var salto = $('#selectHoraSalto').val();

    if (idfiltro == "DIAS") {

        $.ajax({
            url: URL_GET_LISTAR_DIAS,
            dataType: 'json',
            data: {
                id_ruta: id_ruta,
                fechaConsultaInicio: fechaConsultaInicio,
                fechaConsultaFin: fechaConsultaFin,
                tipo_estado: $('#idfiltro option:selected').val()
            },
            success: function (result) {
                $('#consultar').prop('disabled', false).html('Consultar');

                if (result.Table.length == 0) {
                    Swal.fire({
                        type: 'info',
                        title: "No existe información en este fecha",
                        showConfirmButton: false,
                    });
                    return false
                }

                var agrupadoPorfechas = _.groupBy(result.Table, function (d) {
                    var FECHA_REG = d.FECHA_REG
                    FECHA_REG = FECHA_REG.substr(0, 10)
                    return FECHA_REG
                })  //agrupado por fechas la data del comparativo A


                if (salto == 60) {

                    $('#tablaGeneral thead').append('<tr><th>Fecha</th><th class="text-center" colspan="2" >04:00 </th><th class="text-center" colspan="2">05:00 </th><th class="text-center" colspan="2">06:00 </th><th class="text-center" colspan="2">07:00 </th><th class="text-center" colspan="2">08:00 </th><th class="text-center" colspan="2">09:00 </th><th class="text-center" colspan="2">10:00 </th><th class="text-center" colspan="2">11:00 </th><th class="text-center" colspan="2">12:00 </th><th class="text-center" colspan="2">13:00 </th><th class="text-center" colspan="2">14:00 </th><th class="text-center" colspan="2">15:00 </th><th class="text-center" colspan="2">16:00 </th><th class="text-center" colspan="2">17:00 </th><th class="text-center" colspan="2">18:00 </th><th class="text-center" colspan="2">19:00 </th><th class="text-center" colspan="2">20:00 </th><th class="text-center" colspan="2">21:00 </th><th class="text-center" colspan="2">22:00 </th><th class="text-center" colspan="2">23:00 </th></tr>')
                    $('#tablaGeneral thead').append('<tr><th class="text-center"></th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th></tr>')

                    $.each(agrupadoPorfechas, function (fecha_sola, data_abuelo) { //por fechas 
                        var dataFranjaHoras = util.obtenerHorasEnTimestampPOG(FECHA_TEMP + ' ' + '04:00:00', FECHA_TEMP + ' ' + '23:00:00', salto);

                        $.each(data_abuelo, function (j, data_padre) { //por fecha   AGRUPAR POR FRANJAS
                            verificaViajePorFranjaHoraria(this, dataFranjaHoras);
                        });

                        var strHTML = "<td><b>" + fecha_sola + "</b></td>";
                        strHTMLBody = ""

                        $.each(dataFranjaHoras, function (j, data_hijo) { //POR FECHA AGRUPADO
                            var suben = 0;
                            var bajan = 0;
                            $.each(data_hijo.arrViajes, function (k, data_nieto) { //por HORA FECHA
                                suben += data_nieto.PASAJ_SUBEN
                                bajan += data_nieto.PASAJ_BAJAN
                            });
                            strHTMLBody += "<td>" + suben + "</td>" + "<td>" + bajan + "</td>"
                        });
                        $('#tablaGeneral tbody').append('<tr>' + strHTML + strHTMLBody + '</tr>')
                    });


                } else {// SALTO 30 MIN 
                    var dataFranjaHoras = util.obtenerHorasEnTimestampPOG(FECHA_TEMP + ' ' + '04:00:00', FECHA_TEMP + ' ' + '23:00:00', salto);

                    var Nrofecha= [];
                    var colum_ruta = "";
                    var row_ruta = "";
                    var fecha_S_B = "";

                    $.each(result.Table, function (da, d) {
                        var FECHA_REG = d.FECHA_REG
                        FECHA_REG = FECHA_REG.substr(0, 10)
                        Nrofecha.push(FECHA_REG)
                    });

                    Nrofecha = Nrofecha.unique();
                    var num_ruta = Nrofecha.length

                    //RUTAS UNIQUE
                    $.each(Nrofecha, function (i, j) {
                        colum_ruta += '<th colspan="2">' + this + '</th>';
                        row_ruta += '<td></td><td></td>'
                        fecha_S_B += '<th>S</th><th>B</th>'
                    });

                    $('#tablaGeneral thead').append('<tr><th>Franja</th>' + colum_ruta + '</tr>')
                    $('#tablaGeneral thead').append('<tr><th></th>' + fecha_S_B + '</tr>')

                    //tbody FRANJAS
                    strHTML = "";
                    $.each(dataFranjaHoras, function (a, b) {
                        if (a < 40) {
                            strHTML += '<tr>' +
                                '<td class="text-center"><b>' + (this.hInicio).substr(0, (this.hInicio).length - 3) + '-' + (this.hFin).substr(0, (this.hFin).length - 3) + '</b> </td>' +
                                row_ruta +
                            '</tr>';
                        }
                    });
                    $('#tablaGeneral tbody').append(strHTML);

                    //empezamos  
                    var ladoa = 1
                    var ladob = 2

                    $.each(agrupadoPorfechas, function (fecha_sola, data_abuelo) { //por fechas 
                        var totalSubenBajan = []

                        var dataFranjaHoras = util.obtenerHorasEnTimestampPOG(FECHA_TEMP + ' ' + '04:00:00', FECHA_TEMP + ' ' + '23:00:00', salto);

                        $.each(data_abuelo, function (j, data_padre) { //por fecha   AGRUPAR POR FRANJAS
                            verificaViajePorFranjaHoraria(this, dataFranjaHoras);
                        });

                        var Suben_Bajan = {
                            S: 0,
                            B: 0
                        }
                        $.each(dataFranjaHoras, function (j, data_hijo) { //POR FECHA AGRUPADO
                            var suben = 0;
                            var bajan = 0;
                            $.each(data_hijo.arrViajes, function (k, data_nieto) { //por HORA FECHA
                                suben += data_nieto.PASAJ_SUBEN
                                bajan += data_nieto.PASAJ_BAJAN
                            });
                            Suben_Bajan = {
                                S: suben,
                                B: bajan
                            }
                            totalSubenBajan.push(Suben_Bajan)
                        });

                        var position_final = 0



                        $.each($('#tablaGeneral tbody').children(), function (k, data) {//tr 
                            var estado = true
                            $.each($(this).children(), function (trc, datatr) {//td

                                if (trc > 0) {
                                    if (ladoa == trc) {
                                        var SUBEN = totalSubenBajan[position_final].S
                                        $(this).text(SUBEN)

                                    } else if (ladob == trc) {
                                        var BAJAN = totalSubenBajan[position_final].B
                                        $(this).text(BAJAN)
                                        position_final++;
                                    }
                                }
                            });

                        });
                        ladoa = ladoa + 2
                        ladob = ladob + 2
                    });

                }


                $("#tablaGeneral").tableExport({
                    formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
                    position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
                    bootstrap: false,//Usar lo estilos de css de bootstrap para los botones (true, false)
                    fileName: "PasajeeroValidaciones",    //Nombre del archivo 
                });
            }
        }, JSON);


    }
    else if (idfiltro == "PARADERO") {
        $.ajax({
            url: URL_GET_LISTAR_PARADEROS,
            dataType: 'json',
            data: {
                id_ruta: id_ruta,
                fecha: fecha,
                tipo_estado: $('#idfiltro option:selected').val(),
                lado: $('#selectLado option:selected').val()
            },
            success: function (result) {
                $('#consultar').prop('disabled', false).html('Consultar');

                console.log(result)

                if (result.Table.length == 0) {

                    Swal.fire({
                        type: 'info',
                        title: "No existe información en este fecha",
                        showConfirmButton: false,
                    });
                    return false
                }


                var agrupadoPorParaderos = _.groupBy(result.Table, function (d) {
                    var NRO_ORDEN = d.NRO_ORDEN
                    return NRO_ORDEN
                })  //agrupado por fechas la data del comparativo A

                if (salto == 60) {// salto 60 PARADERO



                    var nroOrdenParaderoIniSelec = Number($("#selectParaderoIni option:selected").text().split('.-')[0]);
                    var nroOrdenParaderoFinSelec = Number($("#selectParaderoFin option:selected").text().split('.-')[0]);
 


                    $.each(result.Table, function () { //recorriendo el detalle del viaje
                        //aqui se valida el tramo solicitado
                        var nroOrdenParaderoPaso = getNroOrdenByIdParadero(this.ID_PARADERO); //obtiene el nro de orden del paradero paso para validar los tramos de busqueda
                        nroOrdenParaderoPaso = (nroOrdenParaderoPaso ? nroOrdenParaderoPaso.NRO_ORDEN : null);

                    });

                    $('#tablaGeneral thead').append('<tr><th>Fecha</th><th class="text-center" colspan="2" >04:00 </th><th class="text-center" colspan="2">05:00 </th><th class="text-center" colspan="2">06:00 </th><th class="text-center" colspan="2">07:00 </th><th class="text-center" colspan="2">08:00 </th><th class="text-center" colspan="2">09:00 </th><th class="text-center" colspan="2">10:00 </th><th class="text-center" colspan="2">11:00 </th><th class="text-center" colspan="2">12:00 </th><th class="text-center" colspan="2">13:00 </th><th class="text-center" colspan="2">14:00 </th><th class="text-center" colspan="2">15:00 </th><th class="text-center" colspan="2">16:00 </th><th class="text-center" colspan="2">17:00 </th><th class="text-center" colspan="2">18:00 </th><th class="text-center" colspan="2">19:00 </th><th class="text-center" colspan="2">20:00 </th><th class="text-center" colspan="2">21:00 </th><th class="text-center" colspan="2">22:00 </th><th class="text-center" colspan="2">23:00 </th></tr>')
                    $('#tablaGeneral thead').append('<tr><th class="text-center"></th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th><th class="text-center">S</th><th class="text-center">B</th></tr>')

                      
                    $.each(agrupadoPorParaderos, function (i, data_abuelo) { //por fechas 
                        var dataFranjaHoras = util.obtenerHorasEnTimestampPOG(FECHA_TEMP + ' ' + '04:00:00', FECHA_TEMP + ' ' + '23:00:00', 60);
                        var Nombre_Paradero = data_abuelo[0].PARADERO

                        strHTMLBody = ""


                        $.each(data_abuelo, function (j, data_padre) { //por fecha   AGRUPAR POR FRANJAS
                            verificaViajePorFranjaHoraria(this, dataFranjaHoras);
                        });


                        $.each(dataFranjaHoras, function (j, data_nieto) { //por fecha   AGRUPAR POR FRANJAS

                            var suben = 0;
                            var bajan = 0;
                            $.each(data_nieto.arrViajes, function (k, data_hijo) { //por HORA FECHA
                                suben += data_hijo.PASAJ_SUBEN
                                bajan += data_hijo.PASAJ_BAJAN
                            });
                            strHTMLBody += "<td>" + suben + "</td>" + "<td>" + bajan + "</td>"
                        });



                        $('#tablaGeneral tbody').append('<tr><td><b>' + Nombre_Paradero + "<b/></td>" + strHTMLBody + '</tr>')
                    });
                } else {//30 PARADEROS

                     
                    var dataFranjaHoras = util.obtenerHorasEnTimestampPOG(FECHA_TEMP + ' ' + '04:00:00', FECHA_TEMP + ' ' + '23:00:00', salto);

                    var NroRuta = [];
                    var colum_ruta = "";
                    var row_ruta = "";
                    var fecha_S_B = "";


                    var agrupadoPorParaderos = _.groupBy(result.Table, function (d) {
                        var NRO_ORDEN = d.NRO_ORDEN
                        return NRO_ORDEN
                    })

                    $.each(agrupadoPorParaderos, function (da, data) {
                        $.each(data, function (da, d) {
                            var PARADERO = d.PARADERO
                            NroRuta.push(PARADERO)
                        });
                    });

                    NroRuta = NroRuta.unique();

                    //RUTAS UNIQUE
                    $.each(NroRuta, function (i, j) {
                        colum_ruta += '<th colspan="2">' + this + '</th>';
                        row_ruta += '<td></td><td></td>'
                        fecha_S_B += '<th>S</th><th>B</th>'
                    });

                    $('#tablaGeneral thead').append('<tr><th>Franja</th>' + colum_ruta + '</tr>')
                    $('#tablaGeneral thead').append('<tr><th></th>' + fecha_S_B + '</tr>')

                    //tbody FRANJAS
                    strHTML = "";
                    $.each(dataFranjaHoras, function (a, b) {
                        if (a < 40) {
                            strHTML += '<tr>' +
                                '<td class="text-center"><b>' + (this.hInicio).substr(0, (this.hInicio).length - 3) + '-' + (this.hFin).substr(0, (this.hFin).length - 3) + '</b> </td>' +
                                row_ruta +
                            '</tr>';
                        }
                    });
                    $('#tablaGeneral tbody').append(strHTML);


                    //empezamos  
                    var ladoa = 1
                    var ladob = 2

                    $.each(agrupadoPorParaderos, function (fecha_sola, data_abuelo) { //por fechas 
                        var totalSubenBajan = []

                        var dataFranjaHoras = util.obtenerHorasEnTimestampPOG(FECHA_TEMP + ' ' + '04:00:00', FECHA_TEMP + ' ' + '23:00:00', salto);

                        $.each(data_abuelo, function (j, data_padre) { //por fecha   AGRUPAR POR FRANJAS
                            verificaViajePorFranjaHoraria(this, dataFranjaHoras);
                        });

                        var Suben_Bajan = {
                            S: 0,
                            B: 0
                        }
                        $.each(dataFranjaHoras, function (j, data_hijo) { //POR FECHA AGRUPADO
                            var suben = 0;
                            var bajan = 0;
                            $.each(data_hijo.arrViajes, function (k, data_nieto) { //por HORA FECHA
                                suben += data_nieto.PASAJ_SUBEN
                                bajan += data_nieto.PASAJ_BAJAN
                            });
                            Suben_Bajan = {
                                S: suben,
                                B: bajan
                            }
                            totalSubenBajan.push(Suben_Bajan)
                        });

                        var position_final = 0

                        $.each($('#tablaGeneral tbody').children(), function (k, data) {//tr 
                            var estado = true
                            $.each($(this).children(), function (trc, datatr) {//td

                                if (trc > 0) {
                                    if (ladoa == trc) {
                                        var SUBEN = totalSubenBajan[position_final].S
                                        $(this).text(SUBEN)

                                    } else if (ladob == trc) {
                                        var BAJAN = totalSubenBajan[position_final].B
                                        $(this).text(BAJAN)
                                        position_final++;
                                    }
                                }
                            });

                        });
                        ladoa = ladoa + 2
                        ladob = ladob + 2
                    });
                }
                  
                $("#tablaGeneral").tableExport({
                    formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
                    position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
                    bootstrap: false,//Usar lo estilos de css de bootstrap para los botones (true, false)
                    fileName: "PasajeeroValidaciones",    //Nombre del archivo 
                });
            }
        }, JSON);
    }
}

function getNroOrdenByIdParadero(idParadero) {
    var rpta = null;
    $.each(JSONparaderos, function () {
        if (idParadero == this.ID_PARADERO) {
            rpta = this;
            return false;
        }
    });
    return rpta;
}

function verificaViajePorFranjaHoraria(objSalida, dataFranjaHoraria) {
    //objSalida.arrViajesFinal = [];
    $.each(dataFranjaHoraria, function () {
        this.arrViajesFinal = [];

        var hora_salida = objSalida.FECHA_REG
        hora_salida = hora_salida.substr(10, 9)
        hora_salida = hora_salida.split(':')
        var hora = hora_salida[0]
        hora = hora.trim();
        var minutos = hora_salida[1]
        minutos = minutos.trim()
        var segundos = "00"
        var hora_salida = hora + ":" + minutos + ":" + segundos

        var tmstmIniRango = util.convertDatetoTimeStamp(FECHA_TEMP + ' ' + this.hInicio);
        var tmstmFinRango = util.convertDatetoTimeStamp(FECHA_TEMP + ' ' + this.hFin);
        var tmstmFecSalida = util.convertDatetoTimeStamp(FECHA_TEMP + ' ' + hora_salida);

        if (tmstmFecSalida >= tmstmIniRango && tmstmFecSalida <= tmstmFinRango) {
            var item = objSalida;
            this.arrViajes.push(item);
        }
    });
}


function FiltroCambio() {

    var filtro = $('#idfiltro_primero').val();
    if (filtro == "DIAS") {
        $('#rangoFechaConsulta').parent().parent().css('display', 'block')
        $('#txtFecha').parent().parent().css('display', 'none')

        $('#selectLado').parent().parent().css('display', 'none')
        $('#selectParaderoIni').parent().parent().css('display', 'none')
        $('#selectParaderoFin').parent().parent().css('display', 'none')



    } else if (filtro == "PARADERO") {
        $('#rangoFechaConsulta').parent().parent().css('display', 'none')
        $('#txtFecha').parent().parent().css('display', 'block')

        $('#selectLado').parent().parent().css('display', 'block')
        $('#selectParaderoIni').parent().parent().css('display', 'block')
        $('#selectParaderoFin').parent().parent().css('display', 'block')



    }
}

function cambioDeRuta() {
    getParaderosxRuta($('#selectRutaConsulta').val())
}


Array.prototype.unique = function (a) {
    return function () { return this.filter(a) }
}(function (a, b, c) {
    return c.indexOf(a, b + 1) < 0
});