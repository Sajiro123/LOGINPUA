$(document).ready(function () {


    opcionDatePicker = {
        //startDate: new Date(),
        multidate: 31,
        multidateSeparator: ' - ',
        format: "dd/mm/yyyy",
        //daysOfWeekHighlighted: "0",
        //daysOfWeekDisabled: [1, 2, 3, 4, 5, 6],
        language: 'en'
    };
    getCorredoresByModalidad();

    $('#txtFechasMuestraFin').datepicker(opcionDatePicker).on('changeDate', function (e) { });

    $('#menu-toggle').click()
    

});

function getCorredoresByModalidad() {
    $('#selectCorredores, #selectRuta').empty();
    $('#selectCorredor, #selectRuta').empty();

    var cantidadRegistros = 0;
    $.each(JSON_DATA_CORREDORES, function () {
        $('#selectCorredores').append('<option value="' + this.ID_CORREDOR + '">' + this.ABREVIATURA + '</option>');

        $('#selectCorredor').append('<option value="' + this.ID_CORREDOR + '">' + this.ABREVIATURA + '</option>');

    });

    $('#selectCorredores').val(2)
    getRutaPorCorredor();
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
            $('#selectRutaConsulta').val(2)

        }
    }, JSON);


}



function Listarvalida_filtro_franjas() {

    $('#intervalos').remove();

    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');


    var HORA_OPERACIONES = {
        inicio: '04:00:00',
        fin: '23:59:00'
    };
    var fechaConsulta = "19/06/2020"
    var franja = $('#selectHoraSalto').val()
    var dataHoras = util.obtenerHorasEnTimestampValPasajero(fechaConsulta + ' ' + HORA_OPERACIONES.inicio, fechaConsulta + ' ' + HORA_OPERACIONES.fin, 60);




    $('#tablaGeneral thead').empty();
    $('#tablaGeneral tbody').empty();

    var strHTML = "";




    $.ajax({
        url: URL_GET_CALCULAR_FRANJA,
        dataType: 'json',
        data: {
            id_ruta: $('#selectRutaConsulta').val(),
            fechas: $('#txtFechasMuestraFin').val(),
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
                $('#tablaGeneral tbody').append(strHTML);

                $('#button_export').remove();

                $("#tablaGeneral").tableExport({
                    formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
                    position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
                    bootstrap: false,//Usar lo estilos de css de bootstrap para los botones (true, false)
                    fileName: "PasajeeroValidaciones",    //Nombre del archivo 
                });
                $('#button_export').append('<button id="btn_calcular" onclick="Calcular_promedio()" class="btn btn-dark btn-sm" style="float: right;"> Promedio</button>')

            }




        }


    }, JSON);

}

function Calcular_promedio() {
    $('#btn_calcular').remove();

    $('#button_export').append('<button id="btn_calcular" onclick="Calcular_Intervalos()" class="btn btn-dark btn-sm" style="float: right;"> Calcular</button>')
    $('#button_export').append('<input type="number" id="id_pasajero"  style="float: right;margin-right: 97px;padding: 3px 5px;text-align:center;display: inline-block;border: 1px solid #ccc;border-radius: 4px;box-sizing: border-box;" placeholder="Ingresar Aforo">')



    var promedio_04 = 0; var acumulado4 = 0; var cantidad4 = 0;
    var promedio_05 = 0; var acumulado5 = 0; var cantidad5 = 0;
    var promedio_06 = 0; var acumulado6 = 0; var cantidad6 = 0;
    var promedio_07 = 0; var acumulado7 = 0; var cantidad7 = 0;
    var promedio_08 = 0; var acumulado8 = 0; var cantidad8 = 0;
    var promedio_09 = 0; var acumulado9 = 0; var cantidad9 = 0;
    var promedio_10 = 0; var acumulado10 = 0; var cantidad10 = 0;
    var promedio_11 = 0; var acumulado11 = 0; var cantidad11 = 0;
    var promedio_12 = 0; var acumulado12 = 0; var cantidad12 = 0;
    var promedio_13 = 0; var acumulado13 = 0; var cantidad13 = 0;
    var promedio_14 = 0; var acumulado14 = 0; var cantidad14 = 0;
    var promedio_15 = 0; var acumulado15 = 0; var cantidad15 = 0;
    var promedio_16 = 0; var acumulado16 = 0; var cantidad16 = 0;
    var promedio_17 = 0; var acumulado17 = 0; var cantidad17 = 0;
    var promedio_18 = 0; var acumulado18 = 0; var cantidad18 = 0;
    var promedio_19 = 0; var acumulado19 = 0; var cantidad19 = 0;
    var promedio_20 = 0; var acumulado20 = 0; var cantidad20 = 0;
    var promedio_21 = 0; var acumulado21 = 0; var cantidad21 = 0;
    var promedio_22 = 0; var acumulado22 = 0; var cantidad22 = 0;
    var promedio_23 = 0; var acumulado23 = 0; var cantidad23 = 0;


    $('#tablaGeneral tbody tr').each(function (i) {

        $($(this).children()).each(function (i) {
            if (i == 1) { acumulado4 += parseInt(this.innerText); cantidad4++; }
            if (i == 2) { acumulado5 += parseInt(this.innerText); cantidad5++; }
            if (i == 3) { acumulado6 += parseInt(this.innerText); cantidad6++; }
            if (i == 4) { acumulado7 += parseInt(this.innerText); cantidad7++; }
            if (i == 5) { acumulado8 += parseInt(this.innerText); cantidad8++; }
            if (i == 6) { acumulado9 += parseInt(this.innerText); cantidad9++; }
            if (i == 7) { acumulado10 += parseInt(this.innerText); cantidad10++; }
            if (i == 8) { acumulado11 += parseInt(this.innerText); cantidad11++; }
            if (i == 9) { acumulado12 += parseInt(this.innerText); cantidad12++; }
            if (i == 10) { acumulado13 += parseInt(this.innerText); cantidad13++; }
            if (i == 11) { acumulado14 += parseInt(this.innerText); cantidad14++; }
            if (i == 12) { acumulado15 += parseInt(this.innerText); cantidad15++; }
            if (i == 13) { acumulado16 += parseInt(this.innerText); cantidad16++; }
            if (i == 14) { acumulado17 += parseInt(this.innerText); cantidad17++; }
            if (i == 15) { acumulado18 += parseInt(this.innerText); cantidad18++; }
            if (i == 16) { acumulado19 += parseInt(this.innerText); cantidad19++; }
            if (i == 17) { acumulado20 += parseInt(this.innerText); cantidad20++; }
            if (i == 18) { acumulado21 += parseInt(this.innerText); cantidad21++; }
            if (i == 19) { acumulado22 += parseInt(this.innerText); cantidad22++; }
            if (i == 20) { acumulado23 += parseInt(this.innerText); cantidad23++; }
        });

        promedio_04 = acumulado4 / cantidad4;
        promedio_05 = acumulado5 / cantidad5;
        promedio_06 = acumulado6 / cantidad6;
        promedio_07 = acumulado7 / cantidad7;
        promedio_08 = acumulado8 / cantidad8;
        promedio_09 = acumulado9 / cantidad9;
        promedio_10 = acumulado10 / cantidad10;
        promedio_11 = acumulado11 / cantidad11;
        promedio_12 = acumulado12 / cantidad12;
        promedio_13 = acumulado13 / cantidad13;
        promedio_14 = acumulado14 / cantidad14;
        promedio_15 = acumulado15 / cantidad15;
        promedio_16 = acumulado16 / cantidad16;
        promedio_17 = acumulado17 / cantidad17;
        promedio_18 = acumulado18 / cantidad18;
        promedio_19 = acumulado19 / cantidad19;
        promedio_20 = acumulado20 / cantidad20;
        promedio_21 = acumulado21 / cantidad21;
        promedio_22 = acumulado22 / cantidad22;
        promedio_23 = acumulado23 / cantidad23;

    });

    $('#tablaGeneral tbody').empty();
    $('#tablaGeneral thead').empty();

    $('#tablaGeneral tbody').append('<tr><td class="text-center"><b>Promedio</b></td>' +
        '<td class="text-center">' + Math.floor(promedio_04) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_05) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_06) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_07) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_08) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_09) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_10) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_11) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_12) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_13) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_14) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_15) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_16) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_17) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_18) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_19) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_20) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_21) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_22) + '</td>' +
        '<td class="text-center">' + Math.floor(promedio_23) + '</td>' +
        '</tr>');

    $('#tablaGeneral thead').append('<tr><th></th><th class="text-center">04:00 </th><th class="text-center">05:00 </th><th class="text-center">06:00 </th><th class="text-center">07:00 </th><th class="text-center">08:00 </th><th class="text-center">09:00 </th><th class="text-center">10:00 </th><th class="text-center">11:00 </th><th class="text-center">12:00 </th><th class="text-center">13:00 </th><th class="text-center">14:00 </th><th class="text-center">15:00 </th><th class="text-center">16:00 </th><th class="text-center">17:00 </th><th class="text-center">18:00 </th><th class="text-center">19:00 </th><th class="text-center">20:00 </th><th class="text-center">21:00 </th><th class="text-center">22:00 </th><th class="text-center">23:00 </th></tr>')

    $('#tablaGeneral tbody').append('<tr><td rowspan="2" class="text-center"><b>Porcentaje</b></td>' +
        '<td class="text-center"><input  data-position="1" onchange="showVal(this)" type="range" class="promedio" min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input data-position="2" onchange="showVal(this)" type="range" class="promedio" min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input  data-position="3" onchange="showVal(this)" type="range" class="promedio" min="1" max="100" value="50"></td></td>' +
       '<td class="text-center"><input data-position="4" onchange="showVal(this)" type="range" class="promedio" min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input data-position="5" onchange="showVal(this)" type="range" class="promedio" min="1" max="100" value="50"></td>' +
       '<td class="text-center"><input data-position="6" onchange="showVal(this)" type="range" class="promedio" min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input data-position="7" onchange="showVal(this)" type="range" class="promedio" min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input data-position="8" onchange="showVal(this)"  type="range" class="promedio" min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input data-position="9" onchange="showVal(this)" type="range" class="promedio" min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input data-position="10" onchange="showVal(this)" type="range" class="promedio" min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input onchange="showVal(this)" data-position="11" type="range" class="promedio" min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input onchange="showVal(this)" data-position="12" type="range" class="promedio" min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input onchange="showVal(this)" data-position="13" type="range" class="promedio" min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input onchange="showVal(this)" data-position="14" type="range" class="promedio" min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input onchange="showVal(this)" data-position="15" type="range" class="promedio"    min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input onchange="showVal(this)" data-position="16" type="range" class="promedio" min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input  onchange="showVal(this)" data-position="17" type="range" class="promedio" min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input onchange="showVal(this)" data-position="18" type="range"  class="promedio" min="1" max="100" value="50"></td>' +
        '<td class="text-center"><input onchange="showVal(this)" data-position="19"  type="range"class="promedio" min="1" max="100" value="50"></td>' +
         '<td class="text-center"><input onchange="showVal(this)" data-position="20"  type="range" class="promedio" min="1" max="100" value="50"></td></tr>');

    $('#tablaGeneral tbody').append('<tr><td style="display:none"><b>Valor</b></td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '<td class="text-center">50%</td>' +
        '</tr>');

    $(".promedio").css('width', '67px')
}

function showVal(e) {
    var position_valor = $(e).attr('data-position');
    var valor = $(e).val();

    var position = $(e).parent().parent().parent();
    var tr_valor = $(position[0]).children()[2]

    $(tr_valor).each(function () {
        $($(this).children()).each(function (i) {
            if (i == position_valor) {
                this.innerHTML = valor + '%';
            }
        });
    });
}

function Calcular_Intervalos() {

    var id_pasajero = $('#id_pasajero').val();
    var promedio = 0;
    var valor = 0;

    if (id_pasajero == "") {
        Swal.fire({ type: 'error', title: 'Ingresar Aforo', showConfirmButton: false, timer: 2500 })
        return false
    }
    var count = 0;

    var total = [];

    var array = {
        promedio: [],
        valor: []
    }

    $('#tablaGeneral tbody tr').each(function (j) {
        if (j == 0 || j == 2) {
            $($(this).children()).each(function (i) {
                if (i > 0) {
                    if (count == 0) { promedio = parseInt(this.innerText); array.promedio.push(promedio); }
                    else if (count == 1) { valor = parseInt(this.innerText); array.valor.push(valor); }
                }
            });
            total.push(array);
            count++;
        }
    });

    var id_pasajero = $('#id_pasajero').val()
    var td_total = "<td><b>Cantidad de Buses</b></td>";
    var td_division = "<td><b>Intervalos</b></td>";


    var division = 0;
    $.each(total, function (l) {
        if (l == 0) {
            for (var i = 0; i < 20; i++) {
                var promedio = total[0].promedio[i];
                promedio = promedio / 100;
                var valor = total[0].valor[i];
                valor = valor * promedio;
                if (valor < id_pasajero) {
                    valor = 1;
                    division = 60 / valor
                } else {
                    valor = Math.ceil((valor / id_pasajero));
                    division = (60 / valor).toFixed(2)
                }
                if (i == 0 || i == 19) {
                    td_total += "<td>" + valor + "</td>"
                    td_division += '<td><input style="width: 66px;" type="text" class="form-control text-center" value="' + division + '"></td>';

                } else {
                    td_total += "<td>" + valor + "</td>"
                    td_division += "<td>" + division + "</td>";
                }


            }
        }
    });

    $('#intervalos').remove();

    $('#tablaGeneral tbody').empty()
    $('#tablaGeneral tbody').append('<tr>' + td_total + '</tr>');
    $('#tablaGeneral tbody').append('<tr>' + td_division + '</tr>');
    $('#tablaGeneral tfoot').append('<tr><td></td>/tr><tr><td></td>/tr><tr><td></td>/tr><tr><td></td>/tr> <tr> <td colspan="28"><button class="btn btn-info btn-sm" id="intervalos" onclick="Guardar_Intervalos()">Guardar Intervalos</button> </td></tr>');

    $('#btn_calcular').remove();


}

function Guardar_Intervalos() {

    $('#intervalos').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');
    var lado = $('#idlado').val()

    var array_intervalos = []

    $('#tablaGeneral tbody tr').each(function (l) {

        if (l == 1) {
            $($(this).children()).each(function (i) {
                if (i > 0) {
                    if (i == 1 || i == 20) {
                        var val = $(this).children().val();
                        array_intervalos.push(val)
                    } else {
                        array_intervalos.push(this.innerText)
                    }
                }
            });
        }
    });








    $.ajax({
        url: URL_GETREGISTRAR_TEMPORALPOG,
        type: 'POST',
        data: JSON.stringify({
            'intervalos': array_intervalos,
            'lado': lado
        }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (result) {
            $('#intervalos').prop('disabled', false).html('Guardar Intervalos');


            Swal.fire({
                type: 'success',
                title: 'se registro correctamente',
                showConfirmButton: false,
            });


            $('#tablaGeneral thead').empty();
            $('#tablaGeneral tbody').empty();
            $('#tablaGeneral tfoot').empty();

            $('#button_export').remove();

        }
    }, JSON);




}