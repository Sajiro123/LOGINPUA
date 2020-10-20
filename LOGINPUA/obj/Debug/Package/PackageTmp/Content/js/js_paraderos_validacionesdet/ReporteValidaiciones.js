
$(document).ready(function () {




    $('#txtFecha').parent().parent().css('display', 'none')
    $('#idlado').parent().parent().css('display', 'none')
    getCorredoresByModalidad();

    $('#txtFecha').datepicker({
        endDate: new Date(FECHA_HOY.split('/')[1] + "/" + FECHA_HOY.split('/')[0] + "/" + FECHA_HOY.split('/')[2]),
        format: "dd/mm/yyyy"
    });

    $('#rangoFechaConsulta').daterangepicker({
        startDate: FECHA_HOY,
        
        maxDate: FECHA_HOY,
        //startDate: '06/11/2019',
        //endDate: '06/11/2019',
        timePicker: false,
        //startDate: moment().startOf('hour'),
        //endDate: moment().startOf('hour').add(7, 'days'),
        //"disabledDays": new Array('02/11/2019', '03/11/2019'),
        "opens": "center",
        locale: { format: 'DD/MM/YYYY', "daysOfWeek": ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"], "monthNames": ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"] }
    });

});

function Listarvalida_filtro_franjas() {
    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');


    var HORA_OPERACIONES = {
        inicio: '04:00:00',
        fin: '23:59:00'
    };
    var fechaConsulta = $('#txtFecha').val()
    var franja = $('#selectHoraSalto').val()
    var dataHoras = util.obtenerHorasEnTimestampValPasajero(fechaConsulta + ' ' + HORA_OPERACIONES.inicio, fechaConsulta + ' ' + HORA_OPERACIONES.fin, franja);




    $('#tablaGeneral thead').empty();
    $('#tablaGeneral tbody').empty();

    var franja = franja;
    var idfiltro = $('#idfiltro').val();
    var strHTML = "";

    if (idfiltro == "ruta") {
        $.ajax({
            url: URL_GETLISTAR_RUTA_FRANJA,
            dataType: 'json',
            data: {
                franja: $('#selectHoraSalto').val(),
                fecha: fechaConsulta,
            },
            success: function (result) {

                $('#consultar').prop('disabled', false).html('Consultar');

                 if ((result.Table).length == 0) {
                     Swal.fire({
                         type: 'error',
                         title: 'No existe información de esta fecha',
                         showConfirmButton: false,
                         timer: 2500
                     })
                     return false
                 }



                var i = 0;
                if (franja == 60) { //60 minutos

                    //thead
                    $.each(dataHoras, function (a, b) {
                        if (a < 20) {
                            strHTML +=
                                '<th class="text-center">' + (this.hInicio).substr(0, (this.hInicio).length - 3) + ' </th>';
                        }
                    });
                    $('#tablaGeneral thead').append('<tr><th>N°Ruta</th></tr>')
                    $('#tablaGeneral thead tr').append(strHTML)
                    strHTML = "";
                    //tbody
                    $.each(result.Table, function (b, data) {
                        i++
                        strHTML +=
                            '<tr>' +
                                            '<td class="text-center" ><b>' + data.NRO_RUTA + '</b></td>' +
                                            '<td class="text-center" >' + data.VAL04 + '</td>' +
                                            '<td class="text-center" >' + data.VAL05 + '</td>' +
                                            '<td class="text-center" >' + data.VAL06 + '</td>' +
                                            '<td class="text-center" >' + data.VAL07 + '</td>' +
                                            '<td class="text-center" >' + data.VAL08 + '</td>' +
                                            '<td class="text-center" >' + data.VAL09 + '</td>' +
                                            '<td class="text-center" >' + data.VAL10 + '</td>' +
                                            '<td class="text-center" >' + data.VAL11 + '</td>' +
                                            '<td class="text-center" >' + data.VAL12 + '</td>' +
                                            '<td class="text-center" >' + data.VAL13 + '</td>' +
                                            '<td class="text-center" >' + data.VAL14 + '</td>' +
                                            '<td class="text-center" >' + data.VAL15 + '</td>' +
                                            '<td class="text-center" >' + data.VAL16 + '</td>' +
                                            '<td class="text-center" >' + data.VAL17 + '</td>' +
                                            '<td class="text-center" >' + data.VAL18 + '</td>' +
                                            '<td class="text-center" >' + data.VAL19 + '</td>' +
                                            '<td class="text-center" >' + data.VAL20 + '</td>' +
                                            '<td class="text-center" >' + data.VAL21 + '</td>' +
                                            '<td class="text-center" >' + data.VAL22 + '</td>' +
                                            '<td class="text-center" >' + data.VAL23 + '</td>' +
                                        '</tr>';

                    });
                    console.log(strHTML)
                    $('#tablaGeneral tbody').append(strHTML);

                    $('#button_export').remove();

                    $("#tablaGeneral").tableExport({
                        formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
                        position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
                        bootstrap: false,//Usar lo estilos de css de bootstrap para los botones (true, false)
                        fileName: "PasajeeroValidaciones",    //Nombre del archivo 
                    });

                    //var button = $('#row_filtros').clone().attr('id', 'btnexport');

                    //$('#row_filtros').append(button)
                }



                else if (franja == 30) {  //30 minutos
                    console.log(result, 'result')

                    var NroRuta = [];
                    var colum_ruta = "";
                    var row_ruta = "";

                    var agrupadoPorRuta = _.groupBy(result.Table, function (d) { return d.NRO_RUTA })  //agrupado por fechas la data del comparativo A

                    $.each(result.Table, function (a, b) {
                        NroRuta.push(this.NRO_RUTA)
                    });
                    NroRuta = NroRuta.unique();
                    var num_ruta = NroRuta.length

                    //RUTAS UNIQUE
                    $.each(NroRuta, function (i, j) {
                        colum_ruta += '<th>' + this + '</th>';
                        row_ruta += '<td></td>'
                    });


                    $('#tablaGeneral thead').append('<tr><th>Franja</th>' + colum_ruta + '</tr>')

                    //tbody FRANJAS
                    strHTML = "";
                    $.each(dataHoras, function (a, b) {
                        if (a < 40) {
                            strHTML += '<tr>' +
                                '<td class="text-center">' + (this.hInicio).substr(0, (this.hInicio).length - 3) + '-' + (this.hFin).substr(0, (this.hFin).length - 3) + ' </td>' +
                                row_ruta +

                                '</tr>';
                        }
                    });
                    $('#tablaGeneral tbody').append(strHTML);

                    // franja start
                    var p = 4
                    var position = 0;
                    var veces = 1;
                    //TR
                    $.each($('#tablaGeneral tbody').children(), function (trc, tr) {
                        var th_fecha = "";
                        var td_total = "";

                        if (position < 20) {
                            //TD
                            var count = 0;
                            var counttd = 0;

                            $.each($(this).children(), function (c, d) {
                                if (c > 0) { counttd++; }
                                if (c == 0) { th_fecha = '<tr><td>' + this.textContent + '</td>' }
                            });

                            $(this).empty();

                            $.each(agrupadoPorRuta, function (i, j) {
                                var valor = veces;
                                var tipo = (valor % 2) ? "Impar" : "Par";
                                var ffranja = j[position].HORARIO_FRANJA
                                if (count < num_ruta) {
                                    if (ffranja == p) {
                                        var campo = (tipo == "Impar" ? j[position].MIN0_30 : j[position].MIN30_59)
                                        td_total += '<td>' + campo + '</td>'
                                        count++;
                                    }
                                }
                            });
                            if (count <= num_ruta) {
                                veces++;
                            }

                            if (veces == 3) {
                                veces = 1;
                                p++;
                                position++;
                            }


                            th_fecha = th_fecha + td_total + '</tr>'
                            $('#tablaGeneral tbody').append(th_fecha);
                        }
                    });
                    $('#button_export').remove();

                    $("#tablaGeneral").tableExport({
                        formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
                        position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
                        bootstrap: false,//Usar lo estilos de css de bootstrap para los botones (true, false)
                        fileName: "PasajeeroValidaciones",    //Nombre del archivo 
                    });

                } else {//10 minutos

                    var NroRuta = [];
                    var colum_ruta = "";
                    var row_ruta = "";

                    var agrupadoPorRuta = _.groupBy(result.Table, function (d) { return d.NRO_RUTA })  //agrupado por fechas la data del comparativo A

                    $.each(result.Table, function (a, b) {
                        NroRuta.push(this.NRO_RUTA)
                    });
                    NroRuta = NroRuta.unique();
                    var num_ruta = NroRuta.length

                    //RUTAS UNIQUE
                    $.each(NroRuta, function (i, j) {
                        colum_ruta += '<th>' + this + '</th>';
                        row_ruta += '<td></td>'
                    });


                    $('#tablaGeneral thead').append('<tr><th>Franja</th>' + colum_ruta + '</tr>')

                    //tbody FRANJAS
                    strHTML = "";
                    $.each(dataHoras, function (a, b) {
                        if (a < 120) {
                            strHTML += '<tr>' +
                                '<td class="text-center">' + (this.hInicio).substr(0, (this.hInicio).length - 3) + '-' + (this.hFin).substr(0, (this.hFin).length - 3) + ' </td>' +
                                row_ruta +

                                '</tr>';
                        }
                    });
                    $('#tablaGeneral tbody').append(strHTML);

                    // franja start
                    var p = 4
                    var position = 0;
                    var veces = 1;
                    var valor = 0
                    //TR
                    $.each($('#tablaGeneral tbody').children(), function (trc, tr) {
                        var th_fecha = "";
                        var td_total = "";


                        if (position < 20) {
                            //TD
                            var count = 0;
                            var counttd = 0;

                            $.each($(this).children(), function (c, d) {
                                if (c > 0) { counttd++; }
                                if (c == 0) { th_fecha = '<tr><td>' + this.textContent + '</td>' }
                            });

                            $(this).empty();

                            $.each(agrupadoPorRuta, function (i, j) {
                                var valor = veces;
                                var campo_franja = "";
                                if (valor == 1) { campo_franja = j[position].MIN10 }
                                else if (valor == 2) { campo_franja = j[position].MIN20 }
                                else if (valor == 3) { campo_franja = j[position].MIN30 }
                                else if (valor == 4) { campo_franja = j[position].MIN40 }
                                else if (valor == 5) { campo_franja = j[position].MIN50 }
                                else if (valor == 6) { campo_franja = j[position].MIN60 }


                                var ffranja = j[position].HORARIO_FRANJA
                                if (count < num_ruta) {
                                    if (ffranja == p) {
                                        var campo = campo_franja
                                        td_total += '<td>' + campo + '</td>'
                                        count++;
                                    }
                                }
                            });
                            if (count <= num_ruta) {
                                veces++;

                            }
                            if (veces == 7) {
                                veces = 1;
                                p++;
                                position++
                            }

                            th_fecha = th_fecha + td_total + '</tr>'
                            $('#tablaGeneral tbody').append(th_fecha);
                        }
                    });
                    $('#button_export').remove();

                    $("#tablaGeneral").tableExport({
                        formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
                        position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
                        bootstrap: false,//Usar lo estilos de css de bootstrap para los botones (true, false)
                        fileName: "PasajeeroValidaciones",    //Nombre del archivo 
                    });
                }
            }
        }, JSON);

        
    } else if (idfiltro == "dia") {

        var fechaConsultaInicio = $('#rangoFechaConsulta').val().split('-')[0].trim();
        var fechaConsultaFin = $('#rangoFechaConsulta').val().split('-')[1].trim();

        $.ajax({
            url: URL_GETLISTAR_DIAS_FRANJA,
            dataType: 'json',
            data: {
                franja: $('#selectHoraSalto').val(),
                idruta: $('#selectRutaConsulta').val(),
                inicio: fechaConsultaInicio,
                fin: fechaConsultaFin
            },
            success: function (result) {
                $('#consultar').prop('disabled', false).html('Consultar');

                if ((result.Table).length == 0) {
                    Swal.fire({
                        type: 'error',
                        title: 'No existe información de esta fecha',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return false
                }
                var i = 0;
                if (franja == 60) {
                    //thead
                    $.each(dataHoras, function (a, b) {
                        if (a < 20) {
                            strHTML +=
                                '<th class="text-center">' + (this.hInicio).substr(0, (this.hInicio).length - 3) + ' </th>';
                        }
                    });
                    $('#tablaGeneral thead').append('<tr><th>N°Ruta</th></tr>')
                    $('#tablaGeneral thead tr').append(strHTML)
                    strHTML = "";
                    //tbody
                    $.each(result.Table, function (b, data) {
                        i++
                        strHTML +=
                            '<tr>' +
                                            '<td class="text-center" ><b>' + data.FECHA + '</b></td>' +
                                            '<td class="text-center" >' + data.VAL04 + '</td>' +
                                            '<td class="text-center" >' + data.VAL05 + '</td>' +
                                            '<td class="text-center" >' + data.VAL06 + '</td>' +
                                            '<td class="text-center" >' + data.VAL07 + '</td>' +
                                            '<td class="text-center" >' + data.VAL08 + '</td>' +
                                            '<td class="text-center" >' + data.VAL09 + '</td>' +
                                            '<td class="text-center" >' + data.VAL10 + '</td>' +
                                            '<td class="text-center" >' + data.VAL11 + '</td>' +
                                            '<td class="text-center" >' + data.VAL12 + '</td>' +
                                            '<td class="text-center" >' + data.VAL13 + '</td>' +
                                            '<td class="text-center" >' + data.VAL14 + '</td>' +
                                            '<td class="text-center" >' + data.VAL15 + '</td>' +
                                            '<td class="text-center" >' + data.VAL16 + '</td>' +
                                            '<td class="text-center" >' + data.VAL17 + '</td>' +
                                            '<td class="text-center" >' + data.VAL18 + '</td>' +
                                            '<td class="text-center" >' + data.VAL19 + '</td>' +
                                            '<td class="text-center" >' + data.VAL20 + '</td>' +
                                            '<td class="text-center" >' + data.VAL21 + '</td>' +
                                            '<td class="text-center" >' + data.VAL22 + '</td>' +
                                            '<td class="text-center" >' + data.VAL23 + '</td>' +
                                        '</tr>';

                    });
                    console.log(strHTML)
                    $('#tablaGeneral tbody').append(strHTML);

                    $('#button_export').remove();

                    $("#tablaGeneral").tableExport({
                        formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
                        position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
                        bootstrap: false,//Usar lo estilos de css de bootstrap para los botones (true, false)
                        fileName: "PasajeeroValidaciones",    //Nombre del archivo 
                    });
                } else if (franja == 30) {  //30 minutos
                    var NroRuta = [];
                    var colum_ruta = "";
                    var row_ruta = "";

                    var agrupadoPorRuta = _.groupBy(result.Table, function (d) { return d.FECHA })  //agrupado por fechas la data del comparativo A

                    $.each(result.Table, function (a, b) {
                        NroRuta.push(this.FECHA)
                    });
                    NroRuta = NroRuta.unique();
                    var num_ruta = NroRuta.length

                    //RUTAS UNIQUE
                    $.each(NroRuta, function (i, j) {
                        colum_ruta += '<th>' + this + '</th>';
                        row_ruta += '<td></td>'
                    });


                    $('#tablaGeneral thead').append('<tr><th>Franja</th>' + colum_ruta + '</tr>')

                    //tbody FRANJAS
                    strHTML = "";
                    $.each(dataHoras, function (a, b) {
                        if (a < 40) {
                            strHTML += '<tr>' +
                                '<td class="text-center">' + (this.hInicio).substr(0, (this.hInicio).length - 3) + '-' + (this.hFin).substr(0, (this.hFin).length - 3) + ' </td>' +
                                row_ruta +

                                '</tr>';
                        }
                    });
                    $('#tablaGeneral tbody').append(strHTML);

                    // franja start
                    var p = 4
                    var position = 0;
                    var veces = 0;
                    //TR
                    $.each($('#tablaGeneral tbody').children(), function (trc, tr) {
                        var th_fecha = "";
                        var td_total = "";

                        if (position < 20) {
                            //TD
                            var count = 0;
                            var counttd = 0;

                            $.each($(this).children(), function (c, d) {
                                if (c > 0) { counttd++; }
                                if (c == 0) { th_fecha = '<tr><td>' + this.textContent + '</td>' }
                            });

                            $(this).empty();

                            $.each(agrupadoPorRuta, function (i, j) {
                                var valor = veces;
                                var tipo = (valor % 2) ? "Impar" : "Par";
                                var ffranja = j[position].HORARIO_FRANJA
                                if (count < num_ruta) {
                                    if (ffranja == p) {
                                        var campo = (tipo == "Impar" ? j[position].MIN0_30 : j[position].MIN30_59)
                                        td_total += '<td>' + campo + '</td>'
                                        count++;
                                    }
                                }
                            });
                            if (count <= num_ruta) {
                                veces++;
                            }

                            if (veces == 3) {
                                veces = 1;
                                p++;
                                position++;
                            }
                            th_fecha = th_fecha + td_total + '</tr>'
                            $('#tablaGeneral tbody').append(th_fecha);
                        }
                    });
                    $('#button_export').remove();

                    $("#tablaGeneral").tableExport({
                        formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
                        position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
                        bootstrap: false,//Usar lo estilos de css de bootstrap para los botones (true, false)
                        fileName: "PasajeeroValidaciones",    //Nombre del archivo 
                    });

                } else {//10 minutos

                    var NroRuta = [];
                    var colum_ruta = "";
                    var row_ruta = "";

                    var agrupadoPorRuta = _.groupBy(result.Table, function (d) { return d.FECHA })  //agrupado por fechas la data del comparativo A
                    console.log(agrupadoPorRuta)

                    $.each(result.Table, function (a, b) {
                        NroRuta.push(this.FECHA)
                    });
                    NroRuta = NroRuta.unique();
                    var num_ruta = NroRuta.length

                    //RUTAS UNIQUE
                    $.each(NroRuta, function (i, j) {
                        colum_ruta += '<th>' + this + '</th>';
                        row_ruta += '<td></td>'
                    });


                    $('#tablaGeneral thead').append('<tr><th>Franja</th>' + colum_ruta + '</tr>')

                    //tbody FRANJAS
                    strHTML = "";
                    $.each(dataHoras, function (a, b) {
                        if (a < 120) {
                            strHTML += '<tr>' +
                                '<td class="text-center">' + (this.hInicio).substr(0, (this.hInicio).length - 3) + '-' + (this.hFin).substr(0, (this.hFin).length - 3) + ' </td>' +
                                row_ruta +

                                '</tr>';
                        }
                    });
                    $('#tablaGeneral tbody').append(strHTML);

                    // franja start
                    var p = 4
                    var position = 0;
                    var veces = 1;
                    var valor = 0
                    //TR
                    $.each($('#tablaGeneral tbody').children(), function (trc, tr) {
                        var th_fecha = "";
                        var td_total = "";


                        if (position < 20) {
                            //TD
                            var count = 0;
                            var counttd = 0;

                            $.each($(this).children(), function (c, d) {
                                if (c > 0) { counttd++; }
                                if (c == 0) { th_fecha = '<tr><td>' + this.textContent + '</td>' }
                            });

                            $(this).empty();

                            $.each(agrupadoPorRuta, function (i, j) {
                                var valor = veces;
                                var campo_franja = "";
                                if (valor == 1) { campo_franja = j[position].MIN10 }
                                else if (valor == 2) { campo_franja = j[position].MIN20 }
                                else if (valor == 3) { campo_franja = j[position].MIN30 }
                                else if (valor == 4) { campo_franja = j[position].MIN40 }
                                else if (valor == 5) { campo_franja = j[position].MIN50 }
                                else if (valor == 6) { campo_franja = j[position].MIN60 }


                                var ffranja = j[position].HORARIO_FRANJA
                                if (count < num_ruta) {
                                    if (ffranja == p) {
                                        var campo = campo_franja
                                        td_total += '<td>' + campo + '</td>'
                                        count++;
                                    }
                                }
                            });
                            if (count <= num_ruta) {
                                veces++;

                            }
                            if (veces == 7) {
                                veces = 1;
                                p++;
                                position++
                            }

                            th_fecha = th_fecha + td_total + '</tr>'
                            $('#tablaGeneral tbody').append(th_fecha);
                        }
                    });
                    $('#button_export').remove();

                    $("#tablaGeneral").tableExport({
                        formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
                        position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
                        bootstrap: false,//Usar lo estilos de css de bootstrap para los botones (true, false)
                        fileName: "PasajeeroValidaciones",    //Nombre del archivo 
                    });
                }
            }
        }, JSON);

    } else if (idfiltro == "paradero") {



        $.ajax({
            url: URL_GETLISTAR_PARADEROS_FRANJA,
            dataType: 'json',
            data: {
                franja: $('#selectHoraSalto').val(),
                id_ruta: $('#selectRutaConsulta').val(),
                fecha: $('#txtFecha').val(),
                lado: $('#idlado').val(),
            },
            success: function (result) {
                $('#consultar').prop('disabled', false).html('Consultar');
                
                if ((result.Table).length == 0) {
                    Swal.fire({
                        type: 'error',
                        title: 'No existe información de esta fecha',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return false
                }
                var i = 0;
                if (franja == 60) { //60 minutos

                    //thead
                    $.each(dataHoras, function (a, b) {
                        if (a < 20) {
                            strHTML +=
                                '<th class="text-center">' + (this.hInicio).substr(0, (this.hInicio).length - 3) + ' </th>';
                        }
                    });
                    $('#tablaGeneral thead').append('<tr><th>N°Ruta</th></tr>')
                    $('#tablaGeneral thead tr').append(strHTML)
                    strHTML = "";
                    //tbody
                    $.each(result.Table, function (b, data) {
                        i++
                        strHTML +=
                            '<tr>' +
                                            '<td class="text-center" ><b>' + data.PARADERO_NOMBRE + '</b></td>' +
                                            '<td class="text-center" >' + data.VAL04 + '</td>' +
                                            '<td class="text-center" >' + data.VAL05 + '</td>' +
                                            '<td class="text-center" >' + data.VAL06 + '</td>' +
                                            '<td class="text-center" >' + data.VAL07 + '</td>' +
                                            '<td class="text-center" >' + data.VAL08 + '</td>' +
                                            '<td class="text-center" >' + data.VAL09 + '</td>' +
                                            '<td class="text-center" >' + data.VAL10 + '</td>' +
                                            '<td class="text-center" >' + data.VAL11 + '</td>' +
                                            '<td class="text-center" >' + data.VAL12 + '</td>' +
                                            '<td class="text-center" >' + data.VAL13 + '</td>' +
                                            '<td class="text-center" >' + data.VAL14 + '</td>' +
                                            '<td class="text-center" >' + data.VAL15 + '</td>' +
                                            '<td class="text-center" >' + data.VAL16 + '</td>' +
                                            '<td class="text-center" >' + data.VAL17 + '</td>' +
                                            '<td class="text-center" >' + data.VAL18 + '</td>' +
                                            '<td class="text-center" >' + data.VAL19 + '</td>' +
                                            '<td class="text-center" >' + data.VAL20 + '</td>' +
                                            '<td class="text-center" >' + data.VAL21 + '</td>' +
                                            '<td class="text-center" >' + data.VAL22 + '</td>' +
                                            '<td class="text-center" >' + data.VAL23 + '</td>' +
                                        '</tr>';

                    });
                    $('#tablaGeneral tbody').append(strHTML);

                    $('#button_export').remove();

                    $("#tablaGeneral").tableExport({
                        formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
                        position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
                        bootstrap: false,//Usar lo estilos de css de bootstrap para los botones (true, false)
                        fileName: "PasajeeroValidaciones",    //Nombre del archivo 
                    });
                }


                else if (franja == 30) {  //30 minutos
                    var NroRuta = [];
                    var colum_ruta = "";
                    var row_ruta = "";

                    var agrupadoPorRuta = _.groupBy(result.Table, function (d) { return d.PARADERO_NOMBRE })  //agrupado por fechas la data del comparativo A

                    $.each(result.Table, function (a, b) {
                        NroRuta.push(this.PARADERO_NOMBRE)
                    });
                    NroRuta = NroRuta.unique();
                    var num_ruta = NroRuta.length

                    //RUTAS UNIQUE
                    $.each(NroRuta, function (i, j) {
                        colum_ruta += '<th style="font-size: 14px;"><b>' + this + '</b></th>';
                        row_ruta += '<td></td>'
                    });


                    $('#tablaGeneral thead').append('<tr><th style="width: 460px;text-align:center;">FRANJA<b style="color:#1d96b2">SIT</b></th>' + colum_ruta + '</tr>')

                    //tbody FRANJAS
                    strHTML = "";
                    $.each(dataHoras, function (a, b) {
                        if (a < 40) {
                            strHTML += '<tr>' +
                                '<td class="text-center">' + (this.hInicio).substr(0, (this.hInicio).length - 3) + '-' + (this.hFin).substr(0, (this.hFin).length - 3) + ' </td>' +
                                row_ruta +

                                '</tr>';
                        }
                    });
                    $('#tablaGeneral tbody').append(strHTML);
                     

                    // franja start
                    var p = 4
                    var position = 0;
                    var veces = 0;
                    var countfinal = -1;
                    //TR
                    $.each($('#tablaGeneral tbody').children(), function (trc, tr) {
                        var th_fecha = "";
                        var td_total = "";

                        if (position < 20) {
                            //TD
                            var count = 0;
                            var counttd = 0;

                            $.each($(this).children(), function (c, d) {
                                if (c > 0) { counttd++; }
                                if (c == 0) { th_fecha = '<tr><td>' + this.textContent + '</td>' }
                            });

                            $(this).empty();

                            $.each(agrupadoPorRuta, function (i, j) {
                                countfinal++
                                var valor = veces;
                                var tipo = (valor % 2) ? "Impar" : "Par";
                                var ffranja = j[position].HORARIO_FRANJA
                                if (count < num_ruta) {
                                    if (ffranja == p) {
                                        var campo = (tipo == "Impar" ? j[position].MIN0_30 : j[position].MIN30_59)
                                        td_total += '<td>' + campo + '</td>'
                                        count++;
                                    }
                                }
                            });
                            if (count <= num_ruta) {
                                veces++;
                            }

                            if (veces == 3) {
                                veces = 1;
                                p++;
                                position++;
                            }

                            th_fecha = th_fecha + td_total + '</tr>'
                            $('#tablaGeneral tbody').append(th_fecha);
                        }
                    });

                    $('#button_export').remove();

                    $("#tablaGeneral").tableExport({
                        formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
                        position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
                        bootstrap: false,//Usar lo estilos de css de bootstrap para los botones (true, false)
                        fileName: "PasajeeroValidaciones",    //Nombre del archivo 
                    });

                } else {//10 minutos

                    var NroRuta = [];
                    var colum_ruta = "";
                    var row_ruta = "";

                    var agrupadoPorRuta = _.groupBy(result.Table, function (d) { return d.PARADERO_NOMBRE })  //agrupado por fechas la data del comparativo A
                    console.log(agrupadoPorRuta)

                    $.each(result.Table, function (a, b) {
                        NroRuta.push(this.PARADERO_NOMBRE)
                    });
                    NroRuta = NroRuta.unique();
                    var num_ruta = NroRuta.length

                    //RUTAS UNIQUE
                    $.each(NroRuta, function (i, j) {
                        colum_ruta += '<th style="font-size: 14px;"><b>' + this + '</b></th>';
                        row_ruta += '<td></td>'
                    });


                    $('#tablaGeneral thead').append('<tr><th style="width: 460px;text-align:center;">FRANJA<b style="color:#1d96b2">SIT</b></th>' + colum_ruta + '</tr>')

                    //tbody FRANJAS
                    strHTML = "";
                    $.each(dataHoras, function (a, b) {
                        if (a < 120) {
                            strHTML += '<tr>' +
                                '<td class="text-center">' + (this.hInicio).substr(0, (this.hInicio).length - 3) + '-' + (this.hFin).substr(0, (this.hFin).length - 3) + ' </td>' +
                                row_ruta +

                                '</tr>';
                        }
                    });
                    $('#tablaGeneral tbody').append(strHTML);

                    // franja start
                    var p = 4
                    var position = 0;
                    var veces = 1;
                    var valor = 0
                    //TR
                    $.each($('#tablaGeneral tbody').children(), function (trc, tr) {
                        var th_fecha = "";
                        var td_total = "";


                        if (position < 20) {
                            //TD
                            var count = 0;
                            var counttd = 0;

                            $.each($(this).children(), function (c, d) {
                                if (c > 0) { counttd++; }
                                if (c == 0) { th_fecha = '<tr><td>' + this.textContent + '</td>' }
                            });

                            $(this).empty();

                            $.each(agrupadoPorRuta, function (i, j) {
                                var valor = veces;
                                var campo_franja = "";
                                if (valor == 1) { campo_franja = j[position].MIN10 }
                                else if (valor == 2) { campo_franja = j[position].MIN20 }
                                else if (valor == 3) { campo_franja = j[position].MIN30 }
                                else if (valor == 4) { campo_franja = j[position].MIN40 }
                                else if (valor == 5) { campo_franja = j[position].MIN50 }
                                else if (valor == 6) { campo_franja = j[position].MIN60 }


                                var ffranja = j[position].HORARIO_FRANJA
                                if (count < num_ruta) {
                                    if (ffranja == p) {
                                        var campo = campo_franja
                                        td_total += '<td>' + campo + '</td>'
                                        count++;
                                    }
                                }
                            });
                            if (count <= num_ruta) {
                                veces++;

                            }
                            if (veces == 7) {
                                veces = 1;
                                p++;
                                position++
                            }

                            th_fecha = th_fecha + td_total + '</tr>'
                            $('#tablaGeneral tbody').append(th_fecha);
                        }
                    });
                    $('#button_export').remove();

                    $("#tablaGeneral").tableExport({
                        formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
                        position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
                        bootstrap: false,//Usar lo estilos de css de bootstrap para los botones (true, false)
                        fileName: "PasajeeroValidaciones",    //Nombre del archivo 
                    });
                }
            }
        }, JSON);
        
    }
}





function FiltroCambio() {

    var filtro = $('#idfiltro').val();
    if (filtro == "ruta") {
        $('#selectCorredores').parent().parent().css('display', 'none')
        $('#selectRutaConsulta').parent().parent().css('display', 'none')
        $('#rangoFechaConsulta').parent().parent().css('display', 'none')
        $('#txtFecha').parent().parent().css('display', 'block')
        $('#idlado').parent().parent().css('display', 'none')


    } else if (filtro == "dia") {
        $('#selectCorredores').parent().parent().css('display', 'block')
        $('#selectRutaConsulta').parent().parent().css('display', 'block')
        $('#txtFecha').parent().parent().css('display', 'none')
        $('#rangoFechaConsulta').parent().parent().css('display', 'block')
        $('#idlado').parent().parent().css('display', 'none')


    } else {
        $('#selectCorredores').parent().parent().css('display', 'block')
        $('#selectRutaConsulta').parent().parent().css('display', 'block')
        $('#rangoFechaConsulta').parent().parent().css('display', 'none')
        $('#txtFecha').parent().parent().css('display', 'block')
        $('#idlado').parent().parent().css('display', 'block')
         
    }
}

function getRutaPorCorredor() {
    $.ajax({
        url: URL_GET_RUTA_X_CORREDOR,
        dataType: 'json',
        data: { idCorredor: $('#selectCorredores').val() },
        success: function (result) {
            $('#selectRutaConsulta').empty();
            if (result.length == 0) { // si la lista esta vacia
                $('#selectRutaConsulta').append('<option value="0">' + '--No hay información--' + '</option>');
                return false;
            } else {
                $.each(result, function () {
                    $('#selectRutaConsulta').append('<option value="' + this.ID_RUTA + '" >' + this.NRO_RUTA + '</option>');
                });
            }
        }
    }, JSON);
}

function consultarData() {

    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');
    $.ajax({
        url: URL_GET_REPORTE_FECHA,
        dataType: 'json',
        data: { mes_año: $('#mes_año').val(), idruta: $('#selectRutaConsulta').val() },
        success: function (result) {

            $('#consultar').prop('disabled', false).html('Consultar');
            $('#tbPasajeroVal tbody').empty();
            var strHTML = '';
            var i = 0;
            if (result.length <= 0) {
                strHTML += '<tr><td colspan="13" class="text-center">No hay información para mostrar</td></tr>';
            } else {
                $.each(result, function () {
                    i++;
                    if (this.length < 0) {
                        strHTML += '<tr><td colspan="13" class="text-center">No hay información para mostrar</td></tr>';
                    } else {
                        strHTML += '<tr data-ID_MAESTRO="' + this.ID_MAESTROPASAJERO + '">' +
                                        '<td class="text-center" >' + i + '</td>' +
                                        '<td class="text-center" >' + this.FECHA + '</td>' +

                                        '<td class="text-center" >' + this.NRO_RUTA + '</td>' +
                                        '<td class="text-center" >' + this.USU_REG + '</td>' +
                                        '<td class="text-center" >' + this.FECHA_REG + '</td>' +
                                    '</tr>';
                    }

                });
            }
            $('#tbPasajeroVal tbody').append(strHTML);
        }
    }, JSON);
}

function getCorredoresByModalidad() {
    $('#selectCorredores, #selectRuta').empty();
    $('#selectCorredor, #selectRuta').empty();

    var cantidadRegistros = 0;
    $.each(JSON_DATA_CORREDORES, function () {
        $('#selectCorredores').append('<option value="' + this.ID_CORREDOR + '">' + this.ABREVIATURA + '</option>');

        $('#selectCorredor').append('<option value="' + this.ID_CORREDOR + '">' + this.ABREVIATURA + '</option>');

    });

    getRutaPorCorredor();
}


Array.prototype.unique = function (a) {
    return function () { return this.filter(a) }
}(function (a, b, c) {
    return c.indexOf(a, b + 1) < 0
});