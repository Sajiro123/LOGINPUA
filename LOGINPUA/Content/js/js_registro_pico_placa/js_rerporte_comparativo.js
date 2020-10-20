 
    var btn_1 = document.getElementById('btnConsulta');
    var btn_2 = document.getElementById('btn-2');

    var rangoHorario = [
        "04:00:00 AM-04:29:00 AM", "04:30:00 AM-04:59:00 AM",
        "05:00:00 AM-05:29:00 AM", "05:30:00 AM-05:59:00 AM",
        "06:00:00 AM-06:29:00 AM", "06:30:00 AM-06:59:00 AM",
        "07:00:00 AM-07:29:00 AM", "07:30:00 AM-07:59:00 AM",
        "08:00:00 AM-08:29:00 AM", "08:30:00 AM-08:59:00 AM",
        "09:00:00 AM-09:29:00 AM", "09:30:00 AM-09:59:00 AM",
        "10:00:00 AM-10:29:00 AM", "10:30:00 AM-10:59:00 AM",
        "11:00:00 AM-11:29:00 AM", "11:30:00 AM-11:59:00 AM",
        "12:00:00 PM-12:29:00 PM", "12:30:00 PM-12:59:00 PM",
        "01:00:00 PM-01:29:00 PM", "01:30:00 PM-01:59:00 PM",
        "02:00:00 PM-02:29:00 PM", "02:30:00 PM-02:59:00 PM",
        "03:00:00 PM-03:29:00 PM", "03:30:00 PM-03:59:00 PM",
        "04:00:00 PM-04:29:00 PM", "04:30:00 PM-04:59:00 PM",
        "05:00:00 PM-05:29:00 PM", "05:30:00 PM-05:59:00 PM",
        "06:00:00 PM-06:29:00 PM", "06:30:00 PM-06:59:00 PM",
        "07:00:00 PM-07:29:00 PM", "07:30:00 PM-07:59:00 PM",
        "08:00:00 PM-08:29:00 PM", "08:30:00 PM-08:59:00 PM",
        "09:00:00 PM-09:29:00 PM", "09:30:00 PM-09:59:00 PM",
        "10:00:00 PM-10:29:00 PM", "10:30:00 PM-10:59:00 PM",
        "11:00:00 PM-11:29:00 PM", "11:30:00 PM-11:59:00 PM"
    ];

    var Utilidades = new Util();
  

    function exportarPDF() {

        $('#VEL_PROMEDIO_A_MANANA_IDA').val(VEL_PROMEDIO_A_MAÑANA_IDA);
        $('#VEL_PROMEDIO_A_MANANA_VUELTA').val(VEL_PROMEDIO_A_MAÑANA_VUELTA);
        $('#TIEMPO_PROM_A_1').val(TIEMPO_PROM_A_1);
        $('#TIEMPO_PROM_B_1').val(TIEMPO_PROM_B_1);
        $('#VEL_PROMEDIO_B_MANANA_IDA').val(VEL_PROMEDIO_B_MAÑANA_IDA);
        $('#VEL_PROMEDIO_B_MANANA_VUELTA').val(VEL_PROMEDIO_B_MAÑANA_VUELTA);
        $('#TIEMPO_PROM_A_2').val(TIEMPO_PROM_A_2);
        $('#TIEMPO_PROM_B_2').val(TIEMPO_PROM_B_2);
        $('#VEL_PROMEDIO_A_TARDE_IDA').val(VEL_PROMEDIO_A_TARDE_IDA);
        $('#VEL_PROMEDIO_A_TARDE_VUELTA').val(VEL_PROMEDIO_A_TARDE_VUELTA);
        $('#TIEMPO_PROM_A_3').val(TIEMPO_PROM_A_3);
        $('#TIEMPO_PROM_B_3').val(TIEMPO_PROM_B_3);
        $('#VEL_PROMEDIO_B_TARDE_IDA').val(VEL_PROMEDIO_B_TARDE_IDA);
        $('#VEL_PROMEDIO_B_TARDE_VUELTA').val(VEL_PROMEDIO_B_TARDE_VUELTA);
        $('#TIEMPO_PROM_A_4').val(TIEMPO_PROM_A_4);
        $('#TIEMPO_PROM_B_4').val(TIEMPO_PROM_B_4);
        $('#FECHA_INICIO').val(FECHA_INICIO);
        $('#FECHA_FIN').val(FECHA_FIN);
        $('#CORREDOR').val(CORREDOR);
        $('#MAÑANA_IDA').val(MAÑANA_IDA);
        $('#MAÑANA_VUELTA').val(MAÑANA_VUELTA);
        $('#TARDE_IDA').val(TARDE_IDA);
        $('#TARDE_VUELTA').val(TARDE_VUELTA);


        $('#M_IDA').val(M_IDA);
        $('#M_VUELTA').val(M_VUELTA);
        $('#T_IDA').val(T_IDA);
        $('#T_VUELTA').val(T_VUELTA);
        console.log(M_IDA);

        $('#frmExportaPDF').submit();
    }
    var VEL_PROMEDIO_A_MAÑANA_IDA = null;
    var VEL_PROMEDIO_A_MAÑANA_VUELTA = null;
    var TIEMPO_PROM_A_1 = null;
    var TIEMPO_PROM_B_1 = null;

    var VEL_PROMEDIO_B_MAÑANA_IDA = null;
    var VEL_PROMEDIO_B_MAÑANA_VUELTA = null;
    var VEL_PROMEDIO_A_TARDE_IDA = null;
    var TIEMPO_PROM_A_2 = null;
    var TIEMPO_PROM_B_2 = null;

    var VEL_PROMEDIO_A_TARDE_IDA = null;
    var VEL_PROMEDIO_A_TARDE_VUELTA = null;
    var TIEMPO_PROM_A_3 = null;
    var TIEMPO_PROM_B_3 = null;

    var VEL_PROMEDIO_B_TARDE_IDA = null;
    var VEL_PROMEDIO_B_TARDE_VUELTA = null;
    var TIEMPO_PROM_A_4 = null;
    var TIEMPO_PROM_B_4 = null;

    var FECHA_INICIO = null;
    var FECHA_FIN = null;
    var CORREDOR = null;

    var M_IDA = null;
    var M_VUELTA = null;
    var T_IDA = null;
    var T_VUELTA = null;




    function minTommss(minutes) {
        var sign = minutes < 0 ? "-" : "";
        var min = Math.floor(Math.abs(minutes));
        var sec = Math.floor((Math.abs(minutes) * 60) % 60);
        return sign + (min < 10 ? "0" : "") + min + ":" + (sec < 10 ? "0" : "") + sec;
    }


    function mostrarBoton1() {
        btn_2.style.display = 'none';
        btn_1.style.display = 'inline';
    }

    function cargarHorasComparativo() {

        $('#btnConsultaHoras').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');
        getFranjaHoraria();

        $.ajax({
            type: "POST",
            url: "@Url.Action(ConsultarHoras, PicoPlaca)",
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: { fechaInicio: $('#txtFechasMuestraIni').val(), fechaFin: $('#txtFechasMuestraFin').val(), id_ruta: $('#selectRutas').val() },
            success: function (data) {
                $('#btnConsultaHoras').prop('disabled', false).html('Consultar');
                console.log(data);
            }
           
        })

    }

    function cargarReporteComparativo() {
        $('#btnConsulta').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');
        $.ajax({
            type: "POST",
            url: "@Url.Action(Reporte_Comparativo, PicoPlaca)",
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: { fechaInicio: $('#txtFechasMuestraIni').val(), fechaFin: $('#txtFechasMuestraFin').val(), id_ruta: $('#selectRutas').val(), MAÑANA_IDA: $('#MAÑANA_IDA').val(), MAÑANA_VUELTA: $('#MAÑANA_VUELTA').val(), TARDE_IDA: $('#TARDE_IDA').val(), TARDE_VUELTA: $('#TARDE_VUELTA').val() },
            success: function (data) {
                $('#btnConsulta').prop('disabled', false).html('Consultar');
                $('#tbReporteComparativo_tiempo_mañana tbody').empty();
                var strHTMLtabla = "";
                if (Number(data.TIEMPO_PROM_A_2) == 0) {
                    strHTMLtabla += '<tr>' +
                                        '<td class="text-center" colspan="4">No hay información para mostrar</td>' +
                                    '</tr>';
                } else {

                    M_IDA = data.M_IDA;
                    M_VUELTA = data.M_VUELTA;
                    T_IDA = data.T_IDA;
                    T_VUELTA = data.T_VUELTA;

                    VEL_PROMEDIO_A_MAÑANA_IDA = Number(data.VEL_PROMEDIO_A_MAÑANA_IDA).toFixed(2);
                    VEL_PROMEDIO_A_MAÑANA_VUELTA = Number(data.VEL_PROMEDIO_A_MAÑANA_VUELTA).toFixed(2);
                    TIEMPO_PROM_A_1 = minTommss(data.TIEMPO_PROM_A_1);
                    TIEMPO_PROM_B_1 = minTommss(data.TIEMPO_PROM_B_1);

                    VEL_PROMEDIO_B_MAÑANA_IDA = Number(data.VEL_PROMEDIO_B_MAÑANA_IDA).toFixed(2);
                    VEL_PROMEDIO_B_MAÑANA_VUELTA = Number(data.VEL_PROMEDIO_B_MAÑANA_VUELTA).toFixed(2);
                    TIEMPO_PROM_A_2 = minTommss(data.TIEMPO_PROM_A_2);
                    TIEMPO_PROM_B_2 = minTommss(data.TIEMPO_PROM_B_2);

                    VEL_PROMEDIO_A_TARDE_IDA = Number(data.VEL_PROMEDIO_A_TARDE_IDA).toFixed(2);
                    VEL_PROMEDIO_A_TARDE_VUELTA = Number(data.VEL_PROMEDIO_A_TARDE_VUELTA).toFixed(2);
                    TIEMPO_PROM_A_3 = minTommss(data.TIEMPO_PROM_A_3);
                    TIEMPO_PROM_B_3 = minTommss(data.TIEMPO_PROM_B_3);

                    VEL_PROMEDIO_B_TARDE_IDA = Number(data.VEL_PROMEDIO_B_TARDE_IDA).toFixed(2);
                    VEL_PROMEDIO_B_TARDE_VUELTA = Number(data.VEL_PROMEDIO_B_TARDE_VUELTA).toFixed(2);
                    TIEMPO_PROM_A_4 = minTommss(data.TIEMPO_PROM_A_4);
                    TIEMPO_PROM_B_4 = minTommss(data.TIEMPO_PROM_B_4);

                    FECHA_INICIO = data.FECHA_INICIO;
                    FECHA_FIN = data.FECHA_FIN;

                    CORREDOR = data.CORREDOR;
                    strHTMLtabla += '<tr>' +
                                           '<td class="tablas">COMP A</td>' +
                                           '<td class="tablas">' + data.FECHA_INICIO + '</td>' +
                                           '<td class="text-center tablas">' + minTommss(data.TIEMPO_PROM_A_1) + '</td>' +
                                            '<td class="text-center tablas">' + minTommss(data.TIEMPO_PROM_B_1) + '</td>' +
                                        '</tr>';

                    strHTMLtabla += '<tr>' +
                                            '<td class="tablas">COMP B</td>' +
                                         '<td class="tablas">' + data.FECHA_FIN + '</td>' +

                                         '<td class="text-center tablas">' + minTommss(data.TIEMPO_PROM_A_2) + '</td>' +
                                          '<td class="text-center tablas">' + minTommss(data.TIEMPO_PROM_B_2) + '</td>'
                    '</tr>';
                }



                $('#strHTMLtabla_velocidad_mañana tbody').empty();
                var strHTMLtabla_velocidad_mañana = "";

                if (Number(data.TIEMPO_PROM_A_2) == 0) {
                    strHTMLtabla_velocidad_mañana += '<tr>' +
                                        '<td class="text-center" colspan="4">No hay información para mostrar</td>' +
                                    '</tr>';
                } else {
                    strHTMLtabla_velocidad_mañana += '<tr>' +
                                            '<td class="tablas">COMP A</td>' +
                                            '<td class="tablas">' + data.FECHA_INICIO + '</td>' +
                                            '<td class="text-center tablas">' + Number(data.VEL_PROMEDIO_A_MAÑANA_IDA).toFixed(2) + '</td>' +
                                             '<td class="text-center tablas">' + Number(data.VEL_PROMEDIO_A_MAÑANA_VUELTA).toFixed(2) + '</td>' +
                                         '</tr>';

                    strHTMLtabla_velocidad_mañana += '<tr>' +
                                            '<td class="tablas">COMP B</td>' +
                                         '<td class="tablas">' + data.FECHA_FIN + '</td>' +
                                         '<td class="text-center tablas">' + Number(data.VEL_PROMEDIO_B_MAÑANA_IDA).toFixed(2) + '</td>' +
                                          '<td class="text-center tablas">' + Number(data.VEL_PROMEDIO_B_MAÑANA_VUELTA).toFixed(2) + '</td>'
                    '</tr>';
                }

                $('#tbReporteComparativo_tiempo_tarde tbody').empty();
                var tbReporteComparativo_tiempo_tarde = "";

                if (Number(data.TIEMPO_PROM_A_2) == 0) {
                    tbReporteComparativo_tiempo_tarde += '<tr>' +
                                        '<td class="text-center" colspan="4">No hay información para mostrar</td>' +
                                    '</tr>';
                } else {
                    tbReporteComparativo_tiempo_tarde += '<tr>' +
                                            '<td  class="tablas">COMP A</td>' +
                                            '<td class="tablas">' + data.FECHA_INICIO + '</td>' +
                                            '<td class="text-center tablas">' + minTommss(data.TIEMPO_PROM_A_3) + '</td>' +
                                             '<td class="text-center tablas">' + minTommss(data.TIEMPO_PROM_B_3) + '</td>' +
                                         '</tr>';

                    tbReporteComparativo_tiempo_tarde += '<tr>' +
                                            '<td class="tablas">COMP B</td>' +
                                         '<td class="tablas">' + data.FECHA_FIN + '</td>' +
                                         '<td class="text-center tablas">' + minTommss(data.TIEMPO_PROM_A_4) + '</td>' +
                                          '<td class="text-center tablas">' + minTommss(data.TIEMPO_PROM_B_4) + '</td>'
                    '</tr>';
                }


                $('#strHTMLtabla_velocidad_tarde tbody').empty();
                var strHTMLtabla_velocidad_tarde = "";

                if (Number(data.TIEMPO_PROM_A_2) == 0) {
                    strHTMLtabla_velocidad_tarde += '<tr>' +
                                        '<td class="text-center" colspan="4">No hay información para mostrar</td>' +
                                    '</tr>';
                } else {
                    strHTMLtabla_velocidad_tarde += '<tr>' +
                                            '<td class="tablas">COMP A</td>' +
                                            '<td class="tablas">' + data.FECHA_INICIO + '</td>' +
                                            '<td class="text-center tablas">' + Number(data.VEL_PROMEDIO_A_TARDE_IDA).toFixed(2) + '</td>' +
                                             '<td class="text-center tablas">' + Number(data.VEL_PROMEDIO_A_TARDE_VUELTA).toFixed(2) + '</td>' +
                                         '</tr>';

                    strHTMLtabla_velocidad_tarde += '<tr>' +
                                            '<td  class="tablas">COMP B</td>' +
                                         '<td  class="tablas">' + data.FECHA_FIN + '</td>' +
                                         '<td class="text-center tablas">' + Number(data.VEL_PROMEDIO_B_TARDE_IDA).toFixed(2) + '</td>' +
                                          '<td class="text-center tablas">' + Number(data.VEL_PROMEDIO_B_TARDE_VUELTA).toFixed(2) + '</td>'
                    '</tr>';
                }

                $('#tbReporteComparativo_tiempo_mañana tbody').append(strHTMLtabla);
                $('#strHTMLtabla_velocidad_mañana tbody').append(strHTMLtabla_velocidad_mañana);
                $('#tbReporteComparativo_tiempo_tarde tbody').append(tbReporteComparativo_tiempo_tarde);
                $('#strHTMLtabla_velocidad_tarde tbody').append(strHTMLtabla_velocidad_tarde);
            },
            error: function (xhr, textStatus, errorThrown) {
            }
        })
    }

    function getFranjaHoraria() {
        $('#MAÑANA_IDA, #TARDE_IDA, #MAÑANA_VUELTA, #TARDE_VUELTA').empty();
        //llena select mañana
        for (var i = 0; i < rangoHorario.length; i++) {
            var horaInicio = rangoHorario[i].split('-')[0];
            var horafin = rangoHorario[i].split('-')[1];

            $((rangoHorario[i].indexOf('AM') != -1 ? '#MAÑANA_IDA' : '#TARDE_IDA')).append('<option value="' + horaInicio + '">' + Utilidades.ConvertTimeformat24H(horaInicio) + '</option>');
            $((rangoHorario[i].indexOf('AM') != -1 ? '#MAÑANA_VUELTA' : '#TARDE_VUELTA')).append('<option value="' + horafin + '">' + Utilidades.ConvertTimeformat24H(horafin) + '</option>');
        }

    }
 