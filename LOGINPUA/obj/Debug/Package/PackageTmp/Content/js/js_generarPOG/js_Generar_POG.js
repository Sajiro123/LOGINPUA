var contexto = null;
var util = new Util();
var codModalidadTransporte = $('#mod').val();

$(document).ready(function () {


    getHeightTabs();

    $('#menu-toggle').click()
    getCorredoresByModalidad();

    getTipoDias();
    verificarTipoDia($('#selectTipoDia'));
});

var CANTIDAD_DIAS_MAXIMO_SELECCION = 5;

function verificarTipoDia(elemento) {
    $('#txtDiasSeleccionados').val('');
    var codTipoDia = Number(elemento.val());
    var opcionDatePicker = null;
    $('#txtDiasSeleccionados').datepicker('destroy');
    switch (codTipoDia) {

        case 1: //Hábil
            opcionDatePicker = {
                startDate: new Date(),
                multidate: CANTIDAD_DIAS_MAXIMO_SELECCION,
                multidateSeparator: ' - ',
                //multidate: true,
                format: "dd/mm/yyyy",
                //daysOfWeekHighlighted: "0,6",
                daysOfWeekHighlighted: "1,2,3,4,5",
                datesDisabled: ['31/08/2017'],
                daysOfWeekDisabled: [0, 6],
                //daysOfWeekDisabled: [1, 2, 3, 4, 5],
                language: 'es'
            }
            break;
        case 2: //Sábado
            opcionDatePicker = {
                startDate: new Date(),
                multidate: CANTIDAD_DIAS_MAXIMO_SELECCION,
                multidateSeparator: ' - ',
                format: "dd/mm/yyyy",
                daysOfWeekHighlighted: "6",
                datesDisabled: ['31/08/2017'],
                daysOfWeekDisabled: [0, 1, 2, 3, 4, 5],
                language: 'es'
            }
            break;
        case 3: //Domingo
            opcionDatePicker = {
                startDate: new Date(),
                multidate: CANTIDAD_DIAS_MAXIMO_SELECCION,
                multidateSeparator: ' - ',
                format: "dd/mm/yyyy",
                daysOfWeekHighlighted: "0",
                daysOfWeekDisabled: [1, 2, 3, 4, 5, 6],
                language: 'es'
            }
            break;
        case 4: //Feriado
            opcionDatePicker = {
                startDate: new Date(),
                multidate: CANTIDAD_DIAS_MAXIMO_SELECCION,
                multidateSeparator: ' - ',
                format: "dd/mm/yyyy",
                //daysOfWeekHighlighted: "0",
                //daysOfWeekDisabled: [1, 2, 3, 4, 5, 6],
                language: 'es'
            }
            break;
        case 5: //Domingo
            opcionDatePicker = {
                startDate: new Date(),
                multidate: CANTIDAD_DIAS_MAXIMO_SELECCION,
                multidateSeparator: ' - ',
                format: "dd/mm/yyyy",
                daysOfWeekHighlighted: "0",
                daysOfWeekDisabled: [0, 1, 2, 3, 4, 6],
                language: 'es'
            }
            break;

        default:
            break;
    }
    $('#txtDiasSeleccionados').datepicker(opcionDatePicker).on('changeDate', function (e) {
    });
}

function getTipoDias() {

    $.ajax({
        url: URL_GET_TIPO_DIAS,
        dataType: 'json',
        success: function (result) {
            $('#selectTipoDia').empty();
            $.each(result, function () {
                $('#selectTipoDia').append('<option value="' + this.ID_TIPO_DIA + '">' + this.NOMBRE + '</option>');
            });
        }
    }, JSON);
}





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

function getHeightTabs() {
    var rpta = 0;
    var heightContenedortab = $('html').height() - (40 + $('.imagen_logo').height() + $('#lblAnalisisTiempoViaje').height() + 63 + $('#contentParametros').height() + $('#tabList').height());
    $('#tabContenido').height(heightContenedortab);
}

// script for tab steps
$('li[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    var href = $(e.target).attr('href');
    var $curr = $(".process-model  a[href='" + href + "']").parent();
    $('.process-model li').removeClass();
    $curr.addClass("active");
});

// end  script for tab steps
function inicializarGrafico() {
    ObjetosEnGrafico.contenedor = contexto.rect(0, 0, "100%", '100%').attr({ "fill": "black", "stroke": "#ffffff" });
}


function Listarvalida_filtro_franjas() {

    $('#tbtable_POG tbody').empty()

    $.ajax({
        url: URL_LISTAR_POG,
        dataType: 'json',
        success: function (result) {
            strHTML = "";
            //tbody

            

            $.each(result.Table, function (i, data) {

                //if (data.INTERVALO_A == null && data.T_VIAJE_PROM_A == null) {
                //    //$('Guardar_tabla')
                //    $('#tbtable_POG tbody').append('<tr><td colspan="19">No existe información cargar intervalos de viaje.</td></tr>');
                //} else if (data.INTERVALO_A == null) {

                //    $('#tbtable_POG tbody').append('<tr><td colspan="19">No existe información cargar intervalos de viaje.</td></tr>');
                //    return false
                //} else if (data.T_VIAJE_PROM_A == null) {
                //    $('#tbtable_POG tbody').append('<tr><td colspan="19">No existe informacion cargar de reporte de viajes.</td></tr>');
                //    return false
                //}

                //TIEMPO
                var tiempo_a = "01:15";//CAMBIAR
                var tiempo_b = "01:15";//CAMBIAR

                var tiempo_D_A = util.timeToDecimal(tiempo_a)
                var tiempo_D_B = util.timeToDecimal(tiempo_b)
                var total_tiempo = util.decimaltoTime(tiempo_D_A + tiempo_D_B)


                //INTERVALOS
                var intervalos_a = "00:15";//CAMBIAR
                var intervalos_b = "00:15";//CAMBIAR

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





        }
    }, JSON);
}

function RegistraMaestroPog(reemplazar, id_maestro) {

    $('#Guardar_tabla').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');

    $.ajax({
        url: URL_GUARDAR_MAESTRO_POG + '/?fecha=' + $('#txtDiasSeleccionados').val()
                                           + '&TipoDia=' + $('#selectTipoDia').val() +
                                           '&reemplazar=' + reemplazar +
                                           '&id_maestro=' + id_maestro +
                                            '&id_ruta=' + $("#selectRuta_dos option:selected").val(),
        type: "post",
        dataType: "json",
        cache: false,
        contentType: false,
        processData: false,
        success: function (respuesta) {

            if (respuesta.COD_ESTADO == 1) { //encontró fecha 
                if (respuesta.DES_ESTADO == "Se registró correctamente") {
                    RegistrarDetalladoPog(respuesta.AUX)
                }
            }

            if (respuesta.COD_ESTADO == 3) { //encontró fecha 

                Swal.fire({
                    title: 'ALERTA !',
                    text: respuesta.DES_ESTADO,
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Si'
                }).then((result) => {

                    if (result.value) { //si responde que s   
                        RegistraMaestroPog(1, respuesta.AUX);
                    } else {
                        return false;
                    }

                });
            }
        }
    }, JSON);

}


function RegistrarDetalladoPog(id_maestro_pog) {


    $('#tbtable_POG tbody tr').each(function (l) {

        var p_franja_hi = ""
        var p_franja_hf = ""
        var t_viaje_prom_a = 0
        var t_viaje_prom_b = 0
        var intervalo_a = ""
        var intervalo_b = ""
        var v_promedio_a = 0
        var v_promedio_b = 0

        $($(this).children()).each(function (i) {

            if (i == 0) { p_franja_hi = this.innerText }
            if (i == 1) { p_franja_hf = this.innerText }
            if (i == 5) { t_viaje_prom_a = this.innerText }
            if (i == 6) { t_viaje_prom_b = this.innerText }
            if (i == 8) { intervalo_a = this.innerText }
            if (i == 9) { intervalo_b = this.innerText }
            if (i == 11) { v_promedio_a = this.innerText }
            if (i == 12) { v_promedio_b = this.innerText }
        });


        p_franja_hi = p_franja_hi.substring(1, p_franja_hi.length);
        p_franja_hf = p_franja_hf.substring(2, p_franja_hf.length);

        if (l == 18) {

            $.ajax({
                url: URL_GUARDAR_DETALLE_POG,
                dataType: 'json',
                data: {
                    id_maestro_pog: id_maestro_pog,
                    p_franja_hi: p_franja_hi,
                    p_franja_hf: p_franja_hf,
                    t_viaje_prom_a: t_viaje_prom_a,
                    t_viaje_prom_b: t_viaje_prom_b,
                    intervalo_a: intervalo_a,
                    intervalo_b: intervalo_b,
                    v_promedio_a: v_promedio_a,
                    v_promedio_b: v_promedio_b
                },
                success: function (result) {

                    Swal.fire({
                        type: 'success',
                        title: 'se registro correctamente',
                        showConfirmButton: false,
                    });
                    $('#tbtable_POG tbody').empty()
                    $('#Guardar_tabla').prop('disabled', false).html('Guardar tabla');
                }

            }, JSON);

        } else {
            $.ajax({
                url: URL_GUARDAR_DETALLE_POG,
                dataType: 'json',
                data: {
                    id_maestro_pog: id_maestro_pog,
                    p_franja_hi: p_franja_hi,
                    p_franja_hf: p_franja_hf,
                    t_viaje_prom_a: t_viaje_prom_a,
                    t_viaje_prom_b: t_viaje_prom_b,
                    intervalo_a: intervalo_a,
                    intervalo_b: intervalo_b,
                    v_promedio_a: v_promedio_a,
                    v_promedio_b: v_promedio_b
                },
                success: function (result) {
                }
            }, JSON);
        }
    });
}



