var contexto = null;
var util = new Util();
var codModalidadTransporte = $('#mod').val();

$(document).ready(function () {
    getCorredoresByModalidad();
});


function getRutaPorCorredor_dos() {

    $.ajax({
        url: URL_GET_RUTA_X_CORREDOR,
        dataType: 'json',
        data: { idCorredor: $('#selectCorredores_dos').val() },
        success: function (result) {
            $('#selectRuta_dos').empty();
            if (result.length == 0) { // si la lista esta vacia
                $('#selectRuta_dos').append('<option value="0">' + '--No hay información--' + '</option>');
                return false;
            } else {
                $.each(result, function () {
                    $('#selectRuta_dos').append('<option value="' + this.ID_RUTA + '">' + this.NRO_RUTA + '</option>');
                });
            }
        }
    }, JSON);
}



//covierte la fecha tipo yyyy/mm/dd para el moment
function convertDateFormat(string) {
    var info = string.split('/').reverse().join('-');
    return info;
}


function Rango_fechas(desde, hasta) {
    var dia_actual = desde;
    var fechas = [];
    while (dia_actual.isSameOrBefore(hasta)) {
        fechas.push(dia_actual.format('DD/MM/YYYY'));
        dia_actual.add(1, 'days');
    }
    return fechas;
};
;


function Listar_Data_Pog() {
    var fecha_reporte = "";


    var fechaConsultaInicio = $('#rangoFechaConsulta').val().split('-')[0].trim();
    var fechaConsultaFin = $('#rangoFechaConsulta').val().split('-')[1].trim();

    var fecha_ini = convertDateFormat(fechaConsultaInicio);
    var fecha_fin = convertDateFormat(fechaConsultaFin);

    var desde = moment(fecha_ini);
    var hasta = moment(fecha_fin);


    var diasEntreFechas = Rango_fechas(desde, hasta)

    $.each(diasEntreFechas, function (i, val) {
        fecha_reporte += val + '-';
    });
    fecha_reporte = fecha_reporte.substr(0, fecha_reporte.length - 1)



    $('#tbtable_POG tbody').empty();

    $.ajax({
        url: URL_GET_DATA_POG,
        data: {
            id_ruta: $('#selectRuta_dos').val(),
            fecha: fecha_reporte,
        },
        dataType: 'json',
        success: function (result) {
            strHTML = "";
            //tbody

 
            if (result.COD_ESTADO == 3 || result==null) {
                Swal.fire({
                    type: 'info',
                    title: result.DES_ESTADO,
                    showConfirmButton: false,
                });
                $('#tbtable_POG').children('caption').remove()
                $('#tbtable_POG tbody').append('<tr><td colspan="19">Presionar el boton para consultar la información.</td></tr>');
                $('#msj_fecha').text('')
                return false
            }

            $.each(result.resultado, function (i, data) {
                $('#msj_fecha').text("La fecha programada : " + data.fecha)
            });

            $.each(result.Table, function (i, data) {

                //TIEMPO
                var tiempo_a = data.T_VIAJE_PROM_A;//CAMBIAR
                var tiempo_b = data.T_VIAJE_PROM_B;//CAMBIAR

                var tiempo_D_A = util.timeToDecimal(tiempo_a)
                var tiempo_D_B = util.timeToDecimal(tiempo_b)
                var total_tiempo = util.decimaltoTime(tiempo_D_A + tiempo_D_B)


                //INTERVALOS
                var intervalos_a = data.INTERVALO_A;//CAMBIAR
                var intervalos_b = data.INTERVALO_B;//CAMBIAR

                var intervalos_D_A = util.timeToDecimal(intervalos_a)
                var intervalos_D_B = util.timeToDecimal(intervalos_b)
                var total_intervalos = util.decimaltoTime((intervalos_D_A + intervalos_D_B) / 2)

                //CANTIDAD BUSES
                var buses_n_s = (tiempo_D_A + tiempo_D_B) / intervalos_D_A
                var buses_s_n = tiempo_D_B / intervalos_D_B;
                var cantidad_buses = buses_n_s + buses_s_n

                //velocidades
                var velocidad_a = 40;//CAMBIAR
                var velocidad_b = 33;//CAMBIAR

                //ciclos
                var Tiempo_ciclo = "1:00";
                var ciclos = util.timeToDecimal(Tiempo_ciclo) / ((intervalos_D_A + intervalos_D_B) / 2)

                //DISTANCIA KILOMETRO
                var distancia_A = 34.5
                var distancia_B = 34

                //KM ciclo

                var km_ciclo = (distancia_A + distancia_B) * ciclos

                strHTML += '<tr> ' +
                      '<td class="text-center">' + '>' + data.FRANJA_HI + '</td>' +
                    '<td class="text-center" >' + '<=' + data.FRANJA_HF + '</td>' +
                    '<td class="text-center" >' + cantidad_buses + '</td>' +//CANTIDAD DE BUSES
                    '<td class="text-center" >' + buses_n_s + '</td>' +//CANTIDAD BUSES N-S
                    '<td class="text-center" >  ' + buses_s_n + '</td>' +//CANTIDAD BUSES S-N
                    '<td class="text-center">' + tiempo_a + '</td>' +//TIEMPO VIAJE PROM N-S
                    '<td class="text-center">' + tiempo_b + '</td>' +//TIEMPO VIAJE PROM S-N
                    '<td class="text-center" >' + util.timeRedondear(total_tiempo) + '</td>' +//TIEMPO CICLO TOTAL
                    '<td class="text-center" >' + intervalos_a + '</td>' +//INTERVALO N-S  data.INTERVALO_A 
                     '<td class="text-center" >' + intervalos_b + '</td>' +//INTERVALO S-N data.INTERVALO_B
                    '<td class="text-center" >' + util.timeRedondear(total_intervalos) + '</td>' +//INTERVALO PROMEDIO
                    '<td class="text-center" >' + velocidad_a + '</td>' +//VELOCIDAD PROMEDIO N-S
                    '<td class="text-center" >' + velocidad_b + '</td>' +//VELOCIDAD PROMEDIO S-N
                    '<td class="text-center" >' + distancia_A + '</td>' +//DISTANCIA KILOMETRO N-S
                    '<td class="text-center" >' + distancia_B + '</td>' +//DISTANCIA KILOMETRO S-N
                    '<td class="text-center" >' + 2 + '</td>' +//KM TOTAL
                    '<td class="text-center" >' + 3 + '</td>' +//VELOCIDAD OPERACIONAL
                    '<td class="text-center" >' + ciclos + '</td>' +//#CICLO	
                    '<td class="text-center" >' + km_ciclo + '</td>' +//KM POR CICLOS 
          '</tr>';

            });
            $('#tbtable_POG tbody').append(strHTML);

            var KM_TOTAL = 0;


            var VELOCIDAD_OPERACIONAL = 0;
            var cantidad = 0;

            //ARMAR  
            $('#tbtable_POG tbody tr').each(function (j) {
                $($(this).children()).each(function (i) {

                    if (i == 11) {
                        VELOCIDAD_OPERACIONAL += parseInt(this.innerText)
                        cantidad++;
                    }

                    if (i == 12) {
                        VELOCIDAD_OPERACIONAL += parseInt(this.innerText)
                        cantidad++;
                    }


                    if (i == 18) {

                        KM_TOTAL += parseInt(this.innerText)
                    }
                });
            });

            VELOCIDAD_OPERACIONAL = VELOCIDAD_OPERACIONAL / cantidad;//PROMEDIO VELOVIDAD OPERACIONAL


            $.each($('#tbtable_POG').children('tbody').children(), function (j, i) {

                if (j != 0) {
                    $($(this).children()[13]).remove()
                } else {
                    $($(this).children()[13]).attr('rowspan', 19)
                    $($(this).children()[14]).attr('rowspan', 19)
                    $($(this).children()[15]).text(KM_TOTAL)
                    $($(this).children()[15]).attr('rowspan', 19)
                    $($(this).children()[16]).text(VELOCIDAD_OPERACIONAL)
                    $($(this).children()[16]).attr('rowspan', 19)
                }
            });


            //ARMAR  
            $('#tbtable_POG tbody tr').each(function (j) {
                if (j != 0) {
                    $($(this).children()).each(function (i) {
                        if (i == 13) {
                            $(this).remove()
                        }
                        if (i == 14) {
                            $(this).remove()
                        }
                        if (i == 15) {
                            $(this).remove()
                        }
                    });
                }
            });

            
            $('#tbtable_POG').children('caption').remove()

            var fecha = new Date(); //Fecha actual
            var mes = fecha.getMonth() + 1; //obteniendo mes
            var dia = fecha.getDate(); //obteniendo dia
            var ano = fecha.getFullYear(); //obteniendo año


            $("#tbtable_POG").tableExport({
                formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
                position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
                bootstrap: false,//Usar lo estilos de css de bootstrap para los botones (true, false)
                fileName: "Reporte_POG_" + dia + '-' + mes + '-' + ano,    //Nombre del archivo 
            });
            $('#tbtable_POG').children('caption').children().removeClass()
            $('#tbtable_POG').children('caption').children().addClass("btn btn-success btn-sm")

        }
    }, JSON);
}

 