
$(document).ready(function () {
    //$('#selectModalidadTransporte').val(Number(modalidadUsuario))
    getCorredoresByModalidad();
    //getVias();

    //$('#selectCorredores').on('change', function () {
    //    //console.log($("#selectInfraccion option:selected")[0], 'onchange');
    //    idCorredor = $('#selectCorredores').val();
    //    getListPlacabyCorredor();
    //});
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
            //getRecorridoXRuta();
            //$('.selectRuta').selectpicker('refresh');

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


function Listar_pasajeros_filtro() {
    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');


    //var HORA_OPERACIONES = {
    //    inicio: '04:00:00',
    //    fin: '23:59:00'
    //};
    //var fechaConsulta = $('#txtFecha').val()
    //var franja = $('#selectHoraSalto').val()
    //var dataHoras = util.obtenerHorasEnTimestampValPasajero(fechaConsulta + ' ' + HORA_OPERACIONES.inicio, fechaConsulta + ' ' + HORA_OPERACIONES.fin, franja);

    var fechaConsultaInicio = $('#rangoFechaConsulta').val().split('-')[0].trim();
    var fechaConsultaFin = $('#rangoFechaConsulta').val().split('-')[1].trim();

     var id_ruta = $('#selectRutaConsulta').val()


    $('#tablaGeneral thead').empty();
    $('#tablaGeneral tbody').empty();

    //var franja = franja;
    var idfiltro = $('#idfiltro').val();
    var strHTML = "";

    if (idfiltro == "oferta_demanda") {
        $.ajax({
            url: URL_GET_LISTAR_OFERTA_DEMANDA,
            dataType: 'json',
            data: {
                id_ruta: id_ruta,
                fechaConsultaInicio: fechaConsultaInicio,
                fechaConsultaFin : fechaConsultaFin
            },
            success: function (result) {

                $('#consultar').prop('disabled', false).html('Consultar');

                if ((result).length == 0) {
                    Swal.fire({
                        type: 'error',
                        title: 'No existe información de esta fecha',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return false
                }

                    //thead
                        strHTML +=
                            '<tr>' +
                                 '<th class="text-center">' + 'N°' + '</th>' +
                                 '<th class="text-center">' + 'CORREDOR' + '</th>' +
                                 '<th class="text-center">' + 'N° RUTA' + '</th>' +
                                 '<th class="text-center">' + 'LADO' + '</th>' +
                                 '<th class="text-center">' + 'PARADERO' + '</th>'+
                                 '<th class="text-center">' + 'PLACA' + '</th>'+
                                 '<th class="text-center">' + 'HORA_INICIO' + '</th>'+
                                 '<th class="text-center">' + 'HORA_FIN' + ' </th>'+
                                 '<th class="text-center">' + 'SUBEN' + ' </th>'+
                                 '<th class="text-center">' + 'BAJAN' + ' </th>' +
                                 '<th class="text-center">' + 'COLA' + ' </th>' +
                                 '<th class="text-center">' + 'CAP. LLEGADA' + ' </th>' +
                                 '<th class="text-center">' + 'CAP. SALIDA' + ' </th>' +
                                 '<th class="text-center">' + 'OBSERVACIONES' + ' </th>' +
                                 '<th class="text-center">' + 'USUARIO REGISTRO' + ' </th>' +
                                 '<th class="text-center">' + 'FECHA REGISTRO' + ' </th>'
                             '</tr>';
                        
                    $('#tablaGeneral thead').append(strHTML)
                    strHTML = "";
                    //tbody
                    $.each(result, function (i) {

                        strHTML +=
                            '<tr>' +
                                '<td>' + (i + 1) + '</td>' +
                                '<td class="text-center" >' + (this.NOMBRE_CORREDOR == null ? "" : this.NOMBRE_CORREDOR) + '</td>' +
                                '<td class="text-center" >' + (this.NUM_RUTA == null ? "" : this.NUM_RUTA) + '</td>' +
                                '<td class="text-center" >' + (this.LADO == null ? "" : this.LADO) + '</td>' +
                                '<td class="text-center" >' + (this.PARADERO == null ? "" : this.PARADERO) + '</td>' +
                                '<td class="text-center" >' + (this.PLACA_BUS == null ? "" : this.PLACA_BUS) + '</td>' +
                                '<td class="text-center" >' + (this.HORA_INICIO == null ? "" : this.HORA_INICIO) + '</td>' +
                                '<td class="text-center" >' + (this.HORA_FIN == null ? "" : this.HORA_FIN) + '</td>' +
                                '<td class="text-center" >' + (this.PASAJ_SUBEN == null ? "" : this.PASAJ_SUBEN) + '</td>' +
                                '<td class="text-center" >' + (this.PASAJ_BAJAN == null ? "" : this.PASAJ_BAJAN) + '</td>' +
                                '<td class="text-center" >' + (this.PASAJ_COLA == null ? "" : this.PASAJ_COLA) + '</td>' +
                                '<td class="text-center" >' + (this.CAPACIDAD_LLEGADA == null ? "" : this.CAPACIDAD_LLEGADA) + '</td>' +
                                '<td class="text-center" >' + (this.CAPACIDAD_SALIDA == null ? "" : this.CAPACIDAD_SALIDA) + '</td>' +
                                '<td class="text-center" >' + (this.OBSERVACIONES == null ? "" : this.OBSERVACIONES) + '</td>' +
                                '<td class="text-center" >' + (this.USU_REG == null ? "" : this.USU_REG) + '</td>' +
                                '<td class="text-center" >' + (this.FECHA_REG == null ? "" : this.FECHA_REG) + '</td>' +
                             '</tr>';

                    });

                    $('#tablaGeneral tbody').append(strHTML);
                    util.activarEnumeradoTabla('#tablaGeneral', $('#btnBusquedaEnTabla'));                    
                    $('#button_export').remove();

                    $("#tablaGeneral").tableExport({
                        formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
                        position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
                        bootstrap: false,//Usar lo estilos de css de bootstrap para los botones (true, false)
                        fileName: "PasajeeroValidaciones",    //Nombre del archivo 
                    }); 
            }
        }, JSON);


    } else if (idfiltro == "origen_destino") {


        $.ajax({
            url: URL_GET_LISTAR_ORIGEN_DESTINO,
            dataType: 'json',
            data: {
                id_ruta: id_ruta,
                fechaConsultaInicio: fechaConsultaInicio,
                fechaConsultaFin: fechaConsultaFin
            },
            success: function (result) {
                $('#consultar').prop('disabled', false).html('Consultar');

                if ((result).length == 0) {
                    Swal.fire({
                        type: 'error',
                        title: 'No existe información de esta fecha',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return false
                }

                    strHTML +=
                    '<tr>' +
                    '<th class="text-center">' + 'N°' + '</th>'+
                    '<th class="text-center">' + 'CORREDOR' + '</th>'+
                    '<th class="text-center">' + 'N° RUTA' + '</th>'+
                    '<th class="text-center">' + 'LADO' + '</th>'+
                    '<th class="text-center">' + 'PARADERO ORIGEN' + '</th>'+
                    '<th class="text-center">' + 'PARADERO DESTINO' + '</th>'+
                    '<th class="text-center">' + 'TIPO DE TARJETA' + '</th>' +
                    '<th class="text-center">' + 'USUARIO REGISTRO' + ' </th>' +
                    '<th class="text-center">' + 'FECHA REGISTRO' + ' </th>'
                    '</tr>';

                $('#tablaGeneral thead').append(strHTML)
                strHTML = "";
                //tbody
                $.each(result, function (i) {

                    strHTML +=
                        '<tr>' +
                            '<td>' + (i + 1) + '</td>' +
                            '<td class="text-center" >' + (this.NOMBRE_CORREDOR == null ? "" : this.NOMBRE_CORREDOR) + '</td>' +
                            '<td class="text-center" >' + (this.NUM_RUTA == null ? "" : this.NUM_RUTA) + '</td>' +
                            '<td class="text-center" >' + (this.LADO == null ? "" : this.LADO) + '</td>' +
                            '<td class="text-center" >' + (this.PARADERO_ORIG == null ? "" : this.PARADERO_ORIG) + '</td>' +
                            '<td class="text-center" >' + (this.PARADERO_DEST == null ? "" : this.PARADERO_DEST) + '</td>' +
                            '<td class="text-center" >' + (this.TIPO_TARJETA == null ? "" : this.TIPO_TARJETA) + '</td>' +
                            '<td class="text-center" >' + (this.USU_REG == null ? "" : this.USU_REG) + '</td>' +
                            '<td class="text-center" >' + (this.FECHA_REG == null ? "" : this.FECHA_REG) + '</td>' + 
                         '</tr>';

                });
                //console.log(strHTML)
                $('#tablaGeneral tbody').append(strHTML);
                util.activarEnumeradoTabla('#tablaGeneral', $('#btnBusquedaEnTabla'));

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
        }, JSON);

    } else if (idfiltro == "colectivo") {

        $.ajax({
            url: URL_GET_LISTAR_COLECTIVO,
            dataType: 'json',
            data: {
                id_ruta: id_ruta,
                fechaConsultaInicio: fechaConsultaInicio,
                fechaConsultaFin: fechaConsultaFin
            },
            success: function (result) {
                $('#consultar').prop('disabled', false).html('Consultar');

                if ((result).length == 0) {
                    Swal.fire({
                        type: 'error',
                        title: 'No existe información de esta fecha',
                        showConfirmButton: false,
                        timer: 2500
                    })
                    return false
                }
                strHTML +=
                '<tr>' +
                    '<th class="text-center">' + 'N°' + ' </th>'+
                    '<th class="text-center">' + 'CORREDOR' + ' </th>'+
                    '<th class="text-center">' + 'N° RUTA' + ' </th>'+
                    '<th class="text-center">' + 'LADO' + ' </th>'+
                    '<th class="text-center">' + 'PARADERO' + ' </th>'+
                    '<th class="text-center">' + 'TIPO DE VEHICULO' + ' </th>'+
                    '<th class="text-center">' + 'SUBEN' + ' </th>'+
                    '<th class="text-center">' + 'BAJAN' + ' </th>'+
                    '<th class="text-center">' + 'PLACA DE COLECTIVO' + ' </th>'
                    '<th class="text-center">' + 'USUARIO REGISTRO' + ' </th>' +
                    '<th class="text-center">' + 'FECHA REGISTRO' + ' </th>'
                '</tr>';

                        
                $('#tablaGeneral thead').append(strHTML)
                strHTML = "";
                //tbody
                $.each(result, function (i) {

                    strHTML +=
                        '<tr>' +
                            '<td>' + (i + 1) + '</td>' +
                            '<td class="text-center" >' + (this.NOMBRE_CORREDOR == null ? "" : this.NOMBRE_CORREDOR) + '</td>' +
                            '<td class="text-center" >' + (this.NUM_RUTA == null ? "" : this.NUM_RUTA) + '</td>' +
                            '<td class="text-center" >' + (this.LADO == null ? "" : this.LADO) + '</td>' +
                            '<td class="text-center" >' + (this.PARADERO == null ? "" : this.PARADERO) + '</td>' +
                            '<td class="text-center" >' + (this.TIPO_VEHICULO == null ? "" : this.TIPO_VEHICULO) + '</td>' +
                            '<td class="text-center" >' + (this.PASAJ_SUBEN == null ? "" : this.PASAJ_SUBEN) + '</td>' +
                            '<td class="text-center" >' + (this.PASAJ_BAJAN == null ? "" : this.PASAJ_BAJAN) + '</td>' +
                            '<td class="text-center" >' + (this.PLACA_COLECTIVO == null ? "" : this.PLACA_COLECTIVO) + '</td>' +
                            '<td class="text-center" >' + (this.USU_REG == null ? "" : this.USU_REG) + '</td>' +
                            '<td class="text-center" >' + (this.FECHA_REG == null ? "" : this.FECHA_REG) + '</td>' +
                         '</tr>';

                });
                $('#tablaGeneral tbody').append(strHTML);
                util.activarEnumeradoTabla('#tablaGeneral', $('#btnBusquedaEnTabla'));

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
        }, JSON); 
    }
}
