
$(document).ready(function () {

    opcionDatePicker = {
        //startDate: new Date(),
        multidate: 7,
        multidateSeparator: ' - ',
        format: "dd/mm/yyyy",
        //daysOfWeekHighlighted: "0",
        //daysOfWeekDisabled: [1, 2, 3, 4, 5, 6],
        language: 'en'
    };
    $('#txtFechasMuestraIni, #txtFechasMuestraFin').datepicker(opcionDatePicker).on('changeDate', function (e) {
        // `e` here contains the extra attributes
        //$(this).find('.input-group-addon .count').text(' ' + e.dates.length);
    });

    function consultar() {};
    //function consultarData() {
    //    $.ajax({
    //        url: URL_GET_REPORTE_BY_FECHAS,
    //        dataType: 'json',
    //        data: { fechaInicio: $('#txtFechaConsultaIni').val(), fechaFin: $('#txtFechaConsultaFin').val() },
    //        success: function (result) {
    //            //console.log(result)
    //            $('#tbReportePicoPlaca tbody').empty();
    //            var strHTML = '';
    //            if (result.length == 0) {
    //                DATA_LISTA_REGISTROS = [];
    //                strHTML += '<tr><td colspan="13" class="text-center">No hay información para mostrar</td></tr>';
    //            } else {
    //                DATA_LISTA_REGISTROS = result;
    //                $.each(result, function () {
    //                    var velocidadPromedioAB = Number(this.VEL_PROMEDIO_AB);
    //                    var velocidadPromedioBA = Number(this.VEL_PROMEDIO_BA);
    //                    strHTML += '<tr>' +
    //                                    '<td>' + this.FECHA_REGISTRO.split(' ')[0] + '</td>' +
    //                                    '<td>' + this.ABREV_CORREDOR + '</td>' +
    //                                    '<td class="text-center" >' + this.NRO_RUTA + '</td>' +
    //                                    '<td class="text-center" >' + this.HINICIO + '</td>' +
    //                                    '<td class="text-center" >' + this.HFIN + '</td>' +
    //                                    '<td class="text-center" >' + this.DISTANCIA_A + '</td>' +
    //                                    '<td class="text-center" >' + this.DISTANCIA_B + '</td>' +
    //                                    '<td class="text-center" >' + velocidadPromedioAB.toFixed(2) + '</td>' +
    //                                    '<td class="text-center" >' + velocidadPromedioBA.toFixed(2) + '</td>' +
    //                                    '<td class="text-center" >' + minTommss(this.TIEMPO_PROM_A) + '</td>' +
    //                                    '<td class="text-center" >' + minTommss(this.TIEMPO_PROM_B) + '</td>' +
    //                                    '<td class="text-right">' + this.USU_REG + '</td>' +
    //                                    '<td class="text-right">' + this.FECHA_REG + '</td>' +
    //                                '</tr>';
    //                });
    //            }
    //            $('#tbReportePicoPlaca tbody').append(strHTML);
    //        }
    //    }, JSON);
    //}







});


