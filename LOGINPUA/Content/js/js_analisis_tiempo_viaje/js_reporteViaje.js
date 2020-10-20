$(document).ready(function () {
    //$('#menu-toggle').click();
    //$('#txtFechaConsultaIni').val('06/01/2020');//DURITO
    //$('#txtFechaConsultaFin').val('06/01/2020');//DURITO
    //$('#txtFechaConsultaIni, #txtFechaConsultaFin').datepicker({
    //    endDate: new Date(FECHA_HOY.split('/')[1] + "/" + FECHA_HOY.split('/')[0] + "/" + FECHA_HOY.split('/')[2]),
    //    format: "dd/mm/yyyy"
    //});
    opcionDatePicker = {
        //startDate: new Date(),
        multidate: 7,
        multidateSeparator: ' - ',
        format: "dd/mm/yyyy",
        //daysOfWeekHighlighted: "0",
        //daysOfWeekDisabled: [1, 2, 3, 4, 5, 6],
        language: 'en'
    };
    $('#txtFechaConsulta').datepicker(opcionDatePicker).on('changeDate', function (e) {
        // `e` here contains the extra attributes
        //$(this).find('.input-group-addon .count').text(' ' + e.dates.length);
    });
    getCorredoresByModalidad();
});

var countboton = 0;
function getViajesPorRuta() {

    
    if ($('#txtFechaConsulta').val().length == 0) {
        Swal.fire({
            type: 'warning',
            text: 'Verificar las fechas ingresadas',
            showConfirmButton: false,
            timer: 2000
        });
        return;
    }
    $('#tbViajesPromediados tbody').empty();

    var dataHoras = util.obtenerHorasEnTimestamp(fechaConsultaTimestamp + ' ' + HORA_OPERACIONES.inicio, fechaConsultaTimestamp + ' ' + HORA_OPERACIONES.fin, $('#selectHoraSalto').val());
    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');
    $('#tbViajesPromediados tbody').append('<tr><td colspan="21" class="text-center"><span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Cargando ...</td></tr>');
    //
    if ($('#selectLado').val() == 'A') { $('.texto-sentido').text('AB'); }
    else { $('.texto-sentido').text('BA'); }//para que cambie la etiqueta del THEAD 
    //
    var fechaConsulta = $('#txtFechaConsulta').val();
    $.ajax({
        url: URL_GET_VIAJES_X_RUTA,
        dataType: 'text',
        data: {
            id_ruta: $('#selectRutas').val(),
            fechaConsulta: fechaConsulta,
            lado: $('#selectLado').val(),
        },
        success: function (result) {



            $('#tbViajesPromediados tbody').empty();
            $('#consultar').prop('disabled', false).html('Consultar');
            HTML_TABLA_PRINCIPAL = '';

            var dataJSON = JSON.parse(result);
            if (dataJSON['dt0'].length == 0) {
                $('#tbViajesPromediados tbody').append('<tr><td colspan="13" class="text-center">No hay información para mostrar</td></tr>');
                return;
            }
            //

            var nroOrdenParaderoIniSelec = Number($("#selectParaderoIni option:selected").text().split('.-')[0]);
            var nroOrdenParaderoFinSelec = Number($("#selectParaderoFin option:selected").text().split('.-')[0]);
            var dataPorFecha = _.groupBy(dataJSON['dt0'], function (d) { return d.FECHA })  //agrupado por fechas la data del comparativo A
            //
            var registros = dataJSON['dt0'].length;

            PROMEDIADO_COMPARATIVO = {
                A: [],
                B: []
            }
            DATA_VIAJES = { //para obtener todas las salidas en timestamp agrupados por turno mañana o tarde
                TURNO: {
                    MAÑANA: {
                        arr: [],
                        minimo: null,
                        maximo: null
                    },
                    TARDE: {
                        arr: [],
                        minimo: null,
                        maximo: null
                    }
                }
            };
            var fechaConsultaIni = fechaConsulta.substring(0, 10);
            var fechaConsultaFin = fechaConsulta.substring(fechaConsulta.length - 10);
            //calculando la diferencia de dias
            var diferenciaTimestamp = util.convertDatetoTimeStamp(fechaConsultaFin + ' 00:00:00') - util.convertDatetoTimeStamp(fechaConsultaIni + ' 00:00:00');
            var diferenciaEnDias = (((((diferenciaTimestamp / 1000) / 60) / 60)) / 24);//minutos
            var fechasEnConsulta = incrementarDias(fechaConsultaIni, diferenciaEnDias);
            //
            var htmlRpta = '';
            if (registros > 0) {
                fechasEnConsulta.forEach(function (fecha, i) {
                    var aux_id_tab = fecha.split('/')[0] + fecha.split('/')[1];
                    aux_id_tab = aux_id_tab.trim();
                    var fechaConsulta = fecha.trim();
                    var dataComparativoAxFecha = dataPorFecha[fechaConsulta];
                    //

                    if (dataComparativoAxFecha) {
                        htmlRpta = getPromediadoFranjaHorarias(fechaConsulta, dataPorFecha[fechaConsulta], dataHoras, nroOrdenParaderoIniSelec, nroOrdenParaderoFinSelec);
                    }
                });
                $('#tbViajesPromediados tbody').append(HTML_TABLA_PRINCIPAL);
                countboton++;
            }
            if (countboton<=1) {
                $('#total_result').append('<br><div style="text-align: center;"><a href="#" id="guardar_prom" class="btn btn-info btn-sm" data-toggle="modal" onclick="Guardar_Prom()">Guardar Promedios</a></div>');
            }
            
        }

    });
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

function limpiarContenedorDeViajesxFranja(dataFranja) {
    $.each(dataFranja, function () {
        this.arrViajes = [];
    });
}

function getPromediadoFranjaHorarias(fecha, dataJSON, dataFranjaHoraria, ordenIni, ordenFin) {
    var agrupadoDataDetallePorViajes = _.groupBy(dataJSON, function (d) { return d.ID_SALIDAEJECUTADA });  //agrupado por fechas la data del comparativo A
    var idParaderoInicialSeleccionado = Number($('#selectParaderoIni').val());
    //recorriendo data por viajes
    var viajesFiltrados = [];
    //
    $.each(agrupadoDataDetallePorViajes, function (idViaje, data) {
        var itemViaje = {
            idViaje: idViaje,
            objPasoInicio: null,
            objPasoFin: null
        }

        $.each(data, function () { //recorriendo el detalle del viaje
            //aqui se valida el tramo solicitado
            var nroOrdenParaderoPaso = getNroOrdenByIdParadero(this.ID_PARADERO); //obtiene el nro de orden del paradero paso para validar los tramos de busqueda
            nroOrdenParaderoPaso = (nroOrdenParaderoPaso ? nroOrdenParaderoPaso.NRO_ORDEN : null);

            if (ordenIni == nroOrdenParaderoPaso) { //paradero inicial
                itemViaje.objPasoInicio = this;
            }

            if (ordenFin == nroOrdenParaderoPaso) {
                itemViaje.objPasoFin = this;
            }
        });
        viajesFiltrados.push(itemViaje); //aqui se almacena el viaje su hora inicio y hora fin segun la consulta por paraderos (ini - fin)
    });
    //obteniendo el menor viaje por lado
    var minMenorViaje = getMenorHoraDeViajeMinutos(viajesFiltrados);//viaje min por lado + 2H y 30
    //
    limpiarContenedorDeViajesxFranja(dataFranjaHoraria);

    $.each(viajesFiltrados, function (n) { //recorriendo nueva lista de viajes filtrados
        //console.log(this.objPasoInicio, this.objPasoFin)
        if (this.objPasoInicio && this.objPasoFin) {
            if (this.objPasoInicio.HORA_PASO != null && this.objPasoInicio.HORA_PASO != null) { //llenos porque sino no se puede calcular el tiempo de viaje
                verificaViajePorFranjaHoraria(this, dataFranjaHoraria, fecha);
            }
        }
    });

    var distancias = {
        A: 0,
        B: 0
    }

    /***** get distancias segun paradero ini y fin *****/
    $.each(JSONparaderos, function () {
        var nroOrdenBusquedaIni = Number($('#selectParaderoIni option:selected').text().split('.-')[0]);
        var nroOrdenBusquedaFin = Number($('#selectParaderoFin option:selected').text().split('.-')[0]);
        var nroOrdenParaderoLista = this.NRO_ORDEN;
        var distanciaParcial = this.DISTANCIA_PARCIAL;
        if (idParaderoInicialSeleccionado == this.ID_PARADERO) {
            distanciaParcial = 0;
        }

        if ($('#selectLado').val() == this.LADO) { //verificando el LADO
            if (nroOrdenParaderoLista >= nroOrdenBusquedaIni && nroOrdenParaderoLista <= nroOrdenBusquedaFin) { //verificando el rango de busqueda tramo
                distancias[$('#selectLado').val()] += distanciaParcial;
            }
        }
    });
    /****************************/
    //console.log('dataFranjaHoraria', dataFranjaHoraria);
    distancias.B = Number(distancias.B.toFixed(3)); //dbee tener un solo formato 2 o 3 QUITARRR!!
    return getHTMLPromediadoPorFecha(distancias, dataFranjaHoraria, fecha, minMenorViaje);
}
//
var HTML_TABLA_PRINCIPAL = '';
//
var Prueba = {};
var promediadoPorFranjaHoraria =
{
    fecha: "",
    detalle: []
};
function getHTMLPromediadoPorFecha(dataDistanciasxRecorrido, dataFranjaHoraria, fecha, minutosViajeMin, etiquetaComparativo) {
    var ladoSeleccionado = $('#selectLado').val();
    var corredorSeleccionado = $('#selectCorredores option:selected').text();
    var rutaSelecionada = $('#selectRutas option:selected').text();
    var distanciaRecorridoSeleccionado = dataDistanciasxRecorrido[$('#selectLado').val()];
    promediadoPorFranjaHoraria =
    {
        fecha: fecha,
        detalle: []
    };
    //
    var strHTML = "";
    $.each(dataFranjaHoraria, function () {
        var franjaHorariaInicioTimestamp = this.inicioTimestamp;
        var turnoFranja = this.turno;
        var velocidadKMPromedio = 0;
        var tiempoPromedio = 0;
        var tiempoPromedioMinutos = 0;
        var cantidadViajesValidos = 0;
        var item = {
            inicio: this.hInicio,
            fin: this.hFin,
            tiempo_promedio: null,
            velocidad_promedio: null
        }
        //
        //strHTML += '<tr>' +
        //                '<td>' + fecha + '</td>' +
        //                '<td class="text-center" >' + corredorSeleccionado + '</td>' +
        //                '<td class="text-center" >' + rutaSelecionada + '</td>' +
        //                '<td class="text-center" style="background-color: #cae8e6;" >' + this.hInicio.split(':')[0] + ':' + this.hInicio.split(':')[1] + '</td>' +
        //                '<td class="text-center" style="background-color: #cae8e6;" >' + this.hFin.split(':')[0] + ':' + this.hFin.split(':')[1] + '</td>';

        //if ((item.inicio + ' ' + item.fin) == '05:00:00 05:59:00') {
        //console.log("################### " + this.hInicio + ' ' + this.hFin + " ########################");
        //}

        $.each(this.arrViajes, function () {
            var timestampFechaHoraInicio = (this.objPasoInicio.HORA_PASO ? util.convertDatetoTimeStamp(this.objPasoInicio.FECHA + ' ' + this.objPasoInicio.HORA_PASO) : null);
            var timestampFechaHoraFin = (this.objPasoFin.HORA_PASO ? util.convertDatetoTimeStamp(this.objPasoFin.FECHA + ' ' + this.objPasoFin.HORA_PASO) : null);
            var diferenciaMilisegundos = null;
            var diferenciaMinutos = null;
            var diferenciaHoras = null;
            var velocidadKM = null;
            //
            if (timestampFechaHoraInicio && timestampFechaHoraFin) {
                diferenciaMilisegundos = timestampFechaHoraFin - timestampFechaHoraInicio;
                if (diferenciaMilisegundos < 0) { //si es negativo
                    diferenciaMilisegundos = 0;
                    timestampFechaHoraFin = (timestampFechaHoraFin + 86400000);// 86400; //le agrega un dia
                    diferenciaMilisegundos = timestampFechaHoraFin - timestampFechaHoraInicio;
                }
                diferenciaMinutos = ((diferenciaMilisegundos / 1000) / 60);//minutos

                //if (diferenciaMinutos <= minutosViajeMin && diferenciaMinutos >= 30) {//diferencia de minutos debe ser mayor de los minutos minimospor viaje y mayorigual a 30 minutos

                if (diferenciaMinutos <= minutosViajeMin) {//diferencia de minutos debe ser mayor de los minutos minimospor viaje y mayorigual a 30 minutos
                    if (diferenciaMinutos > 0) {
                        //console.log('--->', this.objPasoInicio.FECHA + ' ' + this.objPasoInicio.HORA_PASO, this.objPasoFin.FECHA + ' ' + this.objPasoFin.HORA_PASO, diferenciaMinutos);
                        DATA_VIAJES.TURNO[turnoFranja].arr.push(franjaHorariaInicioTimestamp);
                        diferenciaHoras = (diferenciaMinutos / 60);
                        velocidadKM = (diferenciaHoras == 0 ? 0 : (distanciaRecorridoSeleccionado / diferenciaHoras));
                        //
                        //console.log(item.inicio + ' ' + item.fin)
                        velocidadKMPromedio += velocidadKM;
                        tiempoPromedio += diferenciaHoras;
                        tiempoPromedioMinutos += diferenciaMinutos;
                        cantidadViajesValidos++;
                        // if ((item.inicio + ' ' + item.fin) == '05:00:00 05:59:00') {
                        //console.log('diferenciaMinutos', diferenciaMinutos);
                        // }
                    }
                }
            }
        });
        //


        //console.log('tiempoPromedio--->', tiempoPromedio)
        velocidadKMPromedio = (cantidadViajesValidos == 0 ? 0 : velocidadKMPromedio / cantidadViajesValidos);
        tiempoPromedio = (cantidadViajesValidos == 0 ? 0 : tiempoPromedio / cantidadViajesValidos);
        //console.log('velocidadKMPromedio', velocidadKMPromedio, 'cantidadViajesValidos', cantidadViajesValidos, 'velocidadKMPromedio', velocidadKMPromedio)
        var tiempoViajeFinal = convertHHMMSS(tiempoPromedio);
        //if ((item.inicio + ' ' + item.fin) == '05:00:00 05:59:00') {
        //console.log('tiempoPromedio->', tiempoPromedio, ' tiempo FORMATO', convertToHHMM(tiempoPromedio));
        //console.log(convertToHHMM(tiempoPromedio))
        //}

        //strHTML += '<td class="text-center"  >' + distanciaRecorridoSeleccionado.toFixed(2) + '</td>';
        //if (velocidadKMPromedio <= 0 && tiempoPromedio <= 0) {
        //    strHTML += '<td colspan="2" style="background: #e9ecef;">' + "No hay información para mostrar" + '</td>';
        //} else {
        //    strHTML += '<td>' + velocidadKMPromedio.toFixed(2) + '</td>';
        //    //strHTML += '<td>' + util.decimalTommss(tiempoPromedio.toFixed(1)) + '</td>';
        //    strHTML += '<td>' + tiempoViajeFinal + '</td>';
        //}
        ////
        //strHTML += '</tr>';
        //
        if (tiempoPromedio > 0 && velocidadKMPromedio > 0) {
            item.tiempo_promedio = tiempoPromedio;
            //item.tiempo_promedio = convertHHMMSS(tiempoPromedio);
            item.velocidad_promedio = velocidadKMPromedio.toFixed(2);
            //
            promediadoPorFranjaHoraria.detalle.push(item);
        }
    });
    //console.log(PROMEDIADO_COMPARATIVO);
    //console.log('--->>',promediadoPorFranjaHoraria);
    //PROMEDIADO_COMPARATIVO[ladoSeleccionado].push(promediadoPorFranjaHoraria);
    PromediarViajes();
    PromediarViajes_HTML();


}

var prom_vel_04 = 0; var velocidad4 = 0; var tiempo4 = 0; var prom_tiempo_04 = 0; var cantidad4 = 0;
var prom_vel_05 = 0; var velocidad5 = 0; var tiempo5 = 0; var prom_tiempo_05 = 0;var cantidad5 = 0;
var prom_vel_06 = 0; var velocidad6 = 0; var tiempo6 = 0; var prom_tiempo_06 = 0;var cantidad6 = 0;
var prom_vel_07 = 0; var velocidad7 = 0; var tiempo7 = 0; var prom_tiempo_07 = 0; var cantidad7 = 0;
var prom_vel_08 = 0; var velocidad8 = 0; var tiempo8 = 0; var prom_tiempo_08 = 0; var cantidad8 = 0;
var prom_vel_09 = 0; var velocidad9 = 0; var tiempo9 = 0; var prom_tiempo_09 = 0; var cantidad9 = 0;
var prom_vel_10 = 0; var velocidad10 = 0; var tiempo10 = 0; var prom_tiempo_10 = 0; var cantidad10 = 0;
var prom_vel_11 = 0; var velocidad11 = 0; var tiempo11 = 0; var prom_tiempo_11 = 0; var cantidad11 = 0;
var prom_vel_12 = 0; var velocidad12 = 0; var tiempo12 = 0; var prom_tiempo_12 = 0; var cantidad12 = 0;
var prom_vel_13 = 0; var velocidad13 = 0; var tiempo13 = 0; var prom_tiempo_13 = 0; var cantidad13 = 0;
var prom_vel_14 = 0; var velocidad14 = 0; var tiempo14 = 0; var prom_tiempo_14 = 0; var cantidad14 = 0;
var prom_vel_15 = 0; var velocidad15 = 0; var tiempo15 = 0; var prom_tiempo_15 = 0; var cantidad15 = 0;
var prom_vel_16 = 0; var velocidad16 = 0; var tiempo16 = 0; var prom_tiempo_16 = 0; var cantidad16 = 0;
var prom_vel_17 = 0; var velocidad17 = 0; var tiempo17 = 0; var prom_tiempo_17 = 0; var cantidad17 = 0;
var prom_vel_18 = 0; var velocidad18 = 0; var tiempo18 = 0; var prom_tiempo_18 = 0; var cantidad18 = 0;
var prom_vel_19 = 0; var velocidad19 = 0; var tiempo19 = 0; var prom_tiempo_19 = 0; var cantidad19 = 0;
var prom_vel_20 = 0; var velocidad20 = 0; var tiempo20 = 0; var prom_tiempo_20 = 0; var cantidad20 = 0;
var prom_vel_21 = 0; var velocidad21 = 0; var tiempo21 = 0; var prom_tiempo_21 = 0; var cantidad21 = 0;
var prom_vel_22 = 0; var velocidad22 = 0; var tiempo22 = 0; var prom_tiempo_22 = 0; var cantidad22 = 0;
var prom_vel_23 = 0; var velocidad23 = 0; var tiempo23 = 0; var prom_tiempo_23 = 0; var cantidad23 = 0;

var tiempoViajeFinal_04=''
var tiempoViajeFinal_05=''
var tiempoViajeFinal_06=''
var tiempoViajeFinal_07=''
var tiempoViajeFinal_08=''
var tiempoViajeFinal_09=''
var tiempoViajeFinal_10=''
var tiempoViajeFinal_11=''
var tiempoViajeFinal_12=''
var tiempoViajeFinal_13=''
var tiempoViajeFinal_14=''
var tiempoViajeFinal_15=''
var tiempoViajeFinal_16=''
var tiempoViajeFinal_17=''
var tiempoViajeFinal_18=''
var tiempoViajeFinal_19=''
var tiempoViajeFinal_20=''
var tiempoViajeFinal_21=''
var tiempoViajeFinal_22=''
var tiempoViajeFinal_23=''

function PromediarViajes() {

    //console.log('--->>', promediadoPorFranjaHoraria);

    $(promediadoPorFranjaHoraria).each(function () {

        $(this.detalle).each(function (i, data) {
            //if (data.inicio == "04:00:00") { console.log(this.velocidad_promedio) }
            if (data.inicio == "04:00:00") { velocidad4 += Number(this.velocidad_promedio); tiempo4 += this.tiempo_promedio; cantidad4++; console.log(tiempo4) }
            if (data.inicio == "05:00:00") { velocidad5 += Number(this.velocidad_promedio); tiempo5 += this.tiempo_promedio; cantidad5++; console.log(tiempo5) }
            if (data.inicio == "06:00:00") { velocidad6 += Number(this.velocidad_promedio); tiempo6 += this.tiempo_promedio; cantidad6++; console.log(tiempo6) }
            if (data.inicio == "07:00:00") { velocidad7 += Number(this.velocidad_promedio); tiempo7 += this.tiempo_promedio; cantidad7++; console.log(tiempo7) }
            if (data.inicio == "08:00:00") { velocidad8 += Number(this.velocidad_promedio); tiempo8 += this.tiempo_promedio; cantidad8++; console.log(tiempo8) }
            if (data.inicio == "09:00:00") { velocidad9 += Number(this.velocidad_promedio); tiempo9 += this.tiempo_promedio; cantidad9++; console.log(tiempo9) }
            if (data.inicio == "10:00:00") { velocidad10 += Number(this.velocidad_promedio); tiempo10 += this.tiempo_promedio; cantidad10++; console.log(tiempo10) }
            if (data.inicio == "11:00:00") { velocidad11 += Number(this.velocidad_promedio); tiempo11 += this.tiempo_promedio; cantidad11++; console.log(tiempo11) }
            if (data.inicio == "12:00:00") { velocidad12 += Number(this.velocidad_promedio); tiempo12 += this.tiempo_promedio; cantidad12++; console.log(tiempo12) }
            if (data.inicio == "13:00:00") { velocidad13 += Number(this.velocidad_promedio); tiempo13 += this.tiempo_promedio; cantidad13++; console.log(tiempo13) }
            if (data.inicio == "14:00:00") { velocidad14 += Number(this.velocidad_promedio); tiempo14 += this.tiempo_promedio; cantidad14++; console.log(tiempo14) }
            if (data.inicio == "15:00:00") { velocidad15 += Number(this.velocidad_promedio); tiempo15 += this.tiempo_promedio; cantidad15++; console.log(tiempo15) }
            if (data.inicio == "16:00:00") { velocidad16 += Number(this.velocidad_promedio); tiempo16 += this.tiempo_promedio; cantidad16++; console.log(tiempo16) }
            if (data.inicio == "17:00:00") { velocidad17 += Number(this.velocidad_promedio); tiempo17 += this.tiempo_promedio; cantidad17++; console.log(tiempo17) }
            if (data.inicio == "18:00:00") { velocidad18 += Number(this.velocidad_promedio); tiempo18 += this.tiempo_promedio; cantidad18++; console.log(tiempo18) }
            if (data.inicio == "19:00:00") { velocidad19 += Number(this.velocidad_promedio); tiempo19 += this.tiempo_promedio; cantidad19++; console.log(tiempo19) }
            if (data.inicio == "20:00:00") { velocidad20 += Number(this.velocidad_promedio); tiempo20 += this.tiempo_promedio; cantidad20++; console.log(tiempo20) }
            if (data.inicio == "21:00:00") { velocidad21 += Number(this.velocidad_promedio); tiempo21 += this.tiempo_promedio; cantidad21++; console.log(tiempo21) }
            if (data.inicio == "22:00:00") { velocidad22 += Number(this.velocidad_promedio); tiempo22 += this.tiempo_promedio; cantidad22++; console.log(tiempo22) }
            if (data.inicio == "23:00:00") { velocidad23 += Number(this.velocidad_promedio); tiempo23 += this.tiempo_promedio; cantidad23++; console.log(tiempo23) }

        });  

    });

}

function PromediarViajes_HTML() {

    prom_vel_04 = velocidad4 / cantidad4; prom_tiempo_04 = tiempo4 / cantidad4;
    prom_vel_05 = velocidad5 / cantidad5; prom_tiempo_05 = tiempo5 / cantidad5;
    prom_vel_06 = velocidad6 / cantidad6; prom_tiempo_06 = tiempo6 / cantidad6;
    prom_vel_07 = velocidad7 / cantidad7; prom_tiempo_07 = tiempo7 / cantidad7;
    prom_vel_08 = velocidad8 / cantidad8; prom_tiempo_08 = tiempo8 / cantidad8;
    prom_vel_09 = velocidad9 / cantidad9; prom_tiempo_09 = tiempo9 / cantidad9;
    prom_vel_10 = velocidad10 / cantidad10; prom_tiempo_10 = tiempo10 / cantidad10;
    prom_vel_11 = velocidad11 / cantidad11; prom_tiempo_11 = tiempo11 / cantidad11;
    prom_vel_12 = velocidad12 / cantidad12; prom_tiempo_12 = tiempo12 / cantidad12;
    prom_vel_13 = velocidad13 / cantidad13; prom_tiempo_13 = tiempo13 / cantidad13;
    prom_vel_14 = velocidad14 / cantidad14; prom_tiempo_14 = tiempo14 / cantidad14;
    prom_vel_15 = velocidad15 / cantidad15; prom_tiempo_15 = tiempo15 / cantidad15;
    prom_vel_16 = velocidad16 / cantidad16; prom_tiempo_16 = tiempo16 / cantidad16;
    prom_vel_17 = velocidad17 / cantidad17; prom_tiempo_17 = tiempo17 / cantidad17;
    prom_vel_18 = velocidad18 / cantidad18; prom_tiempo_18 = tiempo18 / cantidad18;
    prom_vel_19 = velocidad19 / cantidad19; prom_tiempo_19 = tiempo19 / cantidad19;
    prom_vel_20 = velocidad20 / cantidad20; prom_tiempo_20 = tiempo20 / cantidad20;
    prom_vel_21 = velocidad21 / cantidad21; prom_tiempo_21 = tiempo21 / cantidad21;
    prom_vel_22 = velocidad22 / cantidad22; prom_tiempo_22 = tiempo22 / cantidad22;
    prom_vel_23 = velocidad23 / cantidad23; prom_tiempo_23 = tiempo23 / cantidad23;

    //comversion a hh:mm:ss
    tiempoViajeFinal_04 = convertHHMMSS(prom_tiempo_04);
    tiempoViajeFinal_05 = convertHHMMSS(prom_tiempo_05);
    tiempoViajeFinal_06 = convertHHMMSS(prom_tiempo_06);
    tiempoViajeFinal_07 = convertHHMMSS(prom_tiempo_07);
    tiempoViajeFinal_08 = convertHHMMSS(prom_tiempo_08);
    tiempoViajeFinal_09 = convertHHMMSS(prom_tiempo_09);
    tiempoViajeFinal_10 = convertHHMMSS(prom_tiempo_10);
    tiempoViajeFinal_11 = convertHHMMSS(prom_tiempo_11);
    tiempoViajeFinal_12 = convertHHMMSS(prom_tiempo_12);
    tiempoViajeFinal_13 = convertHHMMSS(prom_tiempo_13);
    tiempoViajeFinal_14 = convertHHMMSS(prom_tiempo_14);
    tiempoViajeFinal_15 = convertHHMMSS(prom_tiempo_15);
    tiempoViajeFinal_16 = convertHHMMSS(prom_tiempo_16);
    tiempoViajeFinal_17 = convertHHMMSS(prom_tiempo_17);
    tiempoViajeFinal_18 = convertHHMMSS(prom_tiempo_18);
    tiempoViajeFinal_19 = convertHHMMSS(prom_tiempo_19);
    tiempoViajeFinal_20 = convertHHMMSS(prom_tiempo_20);
    tiempoViajeFinal_21 = convertHHMMSS(prom_tiempo_21);
    tiempoViajeFinal_22 = convertHHMMSS(prom_tiempo_22);
    tiempoViajeFinal_23 = convertHHMMSS(prom_tiempo_23);

    $('#tbViajesPromediados tbody').empty();
    $('#tbViajesPromediados thead').empty();

    $('#tbViajesPromediados tbody').append('<tr><td class="text-center"><b>Velocidad Promedio</b></td>' +
        '<td class="text-center">' + Math.floor(prom_vel_04) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_05) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_06) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_07) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_08) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_09) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_10) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_11) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_12) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_13) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_14) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_15) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_16) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_17) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_18) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_19) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_20) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_21) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_22) + '</td>' +
        '<td class="text-center">' + Math.floor(prom_vel_23) + '</td>' +
        '</tr>'+
        '<tr><td class="text-center"><b>Tiempo Promedio</b></td>' +
        '<td class="text-center">' + tiempoViajeFinal_04 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_05 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_06 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_07 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_08 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_09 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_10 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_11 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_12 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_13 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_14 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_15 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_16 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_17 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_18 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_19 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_20 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_21 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_22 + '</td>' +
        '<td class="text-center">' + tiempoViajeFinal_23 + '</td>' +
        '</tr>');

    $('#tbViajesPromediados thead').append('<tr><th></th><th class="text-center">04:00 </th><th class="text-center">05:00 </th><th class="text-center">06:00 </th><th class="text-center">07:00 </th><th class="text-center">08:00 </th><th class="text-center">09:00 </th><th class="text-center">10:00 </th><th class="text-center">11:00 </th><th class="text-center">12:00 </th><th class="text-center">13:00 </th><th class="text-center">14:00 </th><th class="text-center">15:00 </th><th class="text-center">16:00 </th><th class="text-center">17:00 </th><th class="text-center">18:00 </th><th class="text-center">19:00 </th><th class="text-center">20:00 </th><th class="text-center">21:00 </th><th class="text-center">22:00 </th><th class="text-center">23:00 </th></tr>')
        $('#tbViajesPromediados tbody').append('<tr><td rowspan="2" class="text-center"><b>Porcentaje</b></td>' +
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

        $('#tbViajesPromediados tbody').append('<tr><td style="display:none"><b>Valor</b></td>' +
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
function Guardar_Prom(){
    $('#guardar_prom').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');
   
    Actualizar_Prom_Temp(1, prom_vel_04, tiempoViajeFinal_04)
    Actualizar_Prom_Temp(2, prom_vel_05, tiempoViajeFinal_05)
    Actualizar_Prom_Temp(3, prom_vel_06, tiempoViajeFinal_06)
    Actualizar_Prom_Temp(4, prom_vel_07, tiempoViajeFinal_07)
    Actualizar_Prom_Temp(5, prom_vel_08, tiempoViajeFinal_08)
    Actualizar_Prom_Temp(6, prom_vel_09, tiempoViajeFinal_09)
    Actualizar_Prom_Temp(7, prom_vel_10, tiempoViajeFinal_10)
    Actualizar_Prom_Temp(8, prom_vel_11, tiempoViajeFinal_11)
    Actualizar_Prom_Temp(9, prom_vel_12, tiempoViajeFinal_12)
    Actualizar_Prom_Temp(10, prom_vel_13, tiempoViajeFinal_13)
    Actualizar_Prom_Temp(11, prom_vel_14, tiempoViajeFinal_14)
    Actualizar_Prom_Temp(12, prom_vel_15, tiempoViajeFinal_15)
    Actualizar_Prom_Temp(13, prom_vel_16, tiempoViajeFinal_16)
    Actualizar_Prom_Temp(14, prom_vel_17, tiempoViajeFinal_17)
    Actualizar_Prom_Temp(15, prom_vel_18, tiempoViajeFinal_18)
    Actualizar_Prom_Temp(16, prom_vel_19, tiempoViajeFinal_19)
    Actualizar_Prom_Temp(17, prom_vel_20, tiempoViajeFinal_20)
    Actualizar_Prom_Temp(18, prom_vel_21, tiempoViajeFinal_21)
    Actualizar_Prom_Temp(19, prom_vel_22, tiempoViajeFinal_22)
    Actualizar_Prom_Temp(20, prom_vel_23, tiempoViajeFinal_23)

    $('#guardar_prom').prop('disabled', false).html('Guardar Promedios');

    Swal.fire({
        type:  'success',
        title: 'Se registro correctamente',
        showConfirmButton: false,
        //timer: 2000
    })

}
function Actualizar_Prom_Temp(id_temporal,vel_prom,tiempo_prom) {

    $.ajax({
        url: URL_ACTUALIZAR_PROM_TEMP,
        dataType: 'json',
        data: {
            id_temporal:id_temporal,
            vel_prom: vel_prom,
            tiempo_prom:tiempo_prom,
            lado: $('#selectLado').val(),
        },
        success: function (result) {

            //if ((result.Table).length == 0) {
            //    Swal.fire({
            //        type: 'error',
            //        title: 'No existe información de esta fecha',
            //        showConfirmButton: false,
            //        timer: 2500
            //    })
            //    return false
            //}

        }
    }, JSON);

}
function showVal(e) {
    var position_valor = $(e).attr('data-position');
    var valor = $(e).val();

    var position = $(e).parent().parent().parent();
    var tr_valor = $(position[0]).children()[3]

    $(tr_valor).each(function () {
        $($(this).children()).each(function (i) {
            if (i == position_valor) {
                this.innerHTML = valor + '%';
            }
        });
    });
}


function convertToHHMM(info) {
    var hrs = parseInt(Number(info));
    var min = Math.round((Number(info) - hrs) * 60);
    return hrs + ':' + min;
}

function convertHHMMSS(timeDecimal) {
    var rpta = null;
    var decimalTimeString = timeDecimal;
    var decimalTime = parseFloat(decimalTimeString);
    decimalTime = decimalTime * 60 * 60;
    var hours = Math.floor((decimalTime / (60 * 60)));
    decimalTime = decimalTime - (hours * 60 * 60);
    var minutes = Math.floor((decimalTime / 60));
    decimalTime = decimalTime - (minutes * 60);
    var seconds = Math.round(decimalTime);
    if (hours < 10) {
        hours = "0" + hours;
    }
    if (minutes < 10) {
        minutes = "0" + minutes;
    }
    if (seconds < 10) {
        seconds = "0" + seconds;
    }
    //alert("" + hours + ":" + minutes + ":" + seconds);
    rpta = "" + hours + ":" + minutes + ":" + seconds;
    //console.log('rpta', rpta);
    return rpta;
}

function getMenorHoraDeViajeMinutos(viajesFiltrados) {
    var HORAS_MINUTOS = 60;
    var ladoSeleccionado = $('#selectLado').val();
    var arrTiempoViajes = {
        A: [],
        B: []
    }
    $.each(viajesFiltrados, function () {
        var timestampFechaHoraInicio = this.objPasoInicio ? (this.objPasoInicio.HORA_PASO ? util.convertDatetoTimeStamp(this.objPasoInicio.FECHA + ' ' + this.objPasoInicio.HORA_PASO) : null) : null;
        var timestampFechaHoraFin = this.objPasoFin ? (this.objPasoFin.HORA_PASO ? util.convertDatetoTimeStamp(this.objPasoFin.FECHA + ' ' + this.objPasoFin.HORA_PASO) : null) : null;
        var tiempoViajeMinutos = 0;
        var _diferenciaMilisegundos = 0;
        var _diferenciaMinutos = 0;
        //
        if (timestampFechaHoraInicio && timestampFechaHoraFin) {
            if (this.objPasoInicio.LADO == ladoSeleccionado) {
                _diferenciaMilisegundos = timestampFechaHoraFin - timestampFechaHoraInicio;
                if (_diferenciaMilisegundos < 0) { //si es negativo
                    _diferenciaMilisegundos = 0;
                    timestampFechaHoraFin = (timestampFechaHoraFin + 86400000);// 86400; //le agrega un dia
                    _diferenciaMilisegundos = timestampFechaHoraFin - timestampFechaHoraInicio;
                }
                _diferenciaMinutos = ((_diferenciaMilisegundos / 1000) / 60); //minutos
                if (_diferenciaMinutos >= 30) {
                    arrTiempoViajes[this.objPasoInicio.LADO].push(_diferenciaMinutos);
                }
            }
        }
    });
    var cantidadMinutosMin = (Math.min.apply(Math, arrTiempoViajes[ladoSeleccionado]) + (HORAS_MINUTOS * 2.5));
    return cantidadMinutosMin;
}

function verificaViajePorFranjaHoraria(objSalida, dataFranjaHoraria, fechaTemp) {

    $.each(dataFranjaHoraria, function () {
        //if (this.hInicio == '07:00:00') {
        var tmstmIniRango = util.convertDatetoTimeStamp(fechaTemp + ' ' + this.hInicio);
        var tmstmFinRango = util.convertDatetoTimeStamp(fechaTemp + ' ' + this.hFin);
        var tmstmFecSalida = util.convertDatetoTimeStamp(objSalida.objPasoInicio.FECHA + ' ' + objSalida.objPasoInicio.HORA_PASO);
        //
        if (tmstmFecSalida >= tmstmIniRango && tmstmFecSalida <= tmstmFinRango) {
            var item = objSalida;
            this.arrViajes.push(item);
        }
        //}
    });
}

function getParaderosxRuta(idRuta) {
    JSONparaderos = [];
    $.ajax({
        type: "POST",
        url: URL_GET_PARADEROS_BY_RUTA,
        dataType: "json",
        data: {
            //id_ruta: idRuta,
            id_ruta : 3, //durito
        },
        success: function (data) {
            $('#selectParaderoIni').empty();
            $('#selectParaderoFin').empty();
            if (data.length == 0) {
                //$('#btnConsultaHoras, #exportar').prop('disabled', true);
                $('#selectParaderoIni, #selectParaderoFin').append('<option value="0">-- No hay información para mostrar --</option>');
                return;
            }
            $('#btnConsultaHoras, #exportar').prop('disabled', false);
            $('#exportar').prop('disabled', true);                    //QUITAR SOLO PARA DESARROLLO

            var agrupadoPorLados = _.groupBy(data, function (d) { return d.LADO })  //agrupado por fechas la data del comparativo A
            $.each(agrupadoPorLados[$('#selectLado').val()], function (i) {
                var item = {
                    ID_PARADERO: this.ID_PARADERO,
                    NRO_ORDEN: (this.NRO_ORDEN - 1),
                    NOMBRE: this.NOMBRE,
                    LADO: this.LADO,
                    ETIQUETA_NOMBRE: this.ETIQUETA_NOMBRE,
                    DISTANCIA_PARCIAL: this.DISTANCIA_PARCIAL
                }
                JSONparaderos.push(item);//nuevo json para paradero con el nro orden -1 por el terminal
                $('#selectParaderoIni').append('<option value="' + this.ID_PARADERO + '">' + (this.NRO_ORDEN - 1) + '.- ' + this.NOMBRE + '</option>');
                $('#selectParaderoFin').append('<option value="' + this.ID_PARADERO + '"  ' + (agrupadoPorLados[$('#selectLado').val()].length - 1 == i ? ' selected ' : '') + ' >' + (this.NRO_ORDEN - 1) + '.- ' + this.NOMBRE + '</option>');
            });
            //$('#selectParaderoIni').val(955);//DURITO
            //$('#selectParaderoFin').val(959);//DURITO
            //getViajesPorRuta();//DURITO
        }
    });
}
//
function cambioDeRuta() {
    getParaderosxRuta($('#selectRutas').val())
}

function incrementarDias(fecha, cantidadDiasAumenta) {
    var rpta = [];
    var timestampFecha = util.convertDatetoTimeStamp(fecha + ' 00:00:00');

    var diaEnMilisegundos = ((1440 * 60) * 1000); //1 dia en milisegundos
    var fechaFormateada = util.convertTimestampToDate(timestampFecha);
    fechaFormateada = fechaFormateada.split(' ')[0];
    rpta.push(fechaFormateada);
    //
    var timestampTemp = timestampFecha;
    for (var i = 1; i <= cantidadDiasAumenta; i++) {
        timestampFecha += diaEnMilisegundos;
        var fechaFormateado = util.convertTimestampToDate(timestampFecha);
        fechaFormateado = fechaFormateado.split(' ')[0];
        rpta.push(fechaFormateado);
    }
    return rpta;
}

function exportarTabla() {
    var fechaConsulta = $('#txtFechaConsulta').val();
    var fechaConsultaIni = fechaConsulta.substring(0, 10);
    var fechaConsultaFin = fechaConsulta.substring(fechaConsulta.length - 10);
    tableDataExportar = [{ "sheetName": "Hoja", "data": [] }];
    var options = {
        fileName: "Reporte de velocidades " + $("#selectRutas option:selected").text() + " desde" + fechaConsultaIni + ' hasta ' + fechaConsultaFin
    };
    var tHeadExcel =
    [
        { "text": "FECHA" },
        { "text": "CORREDOR" },
        { "text": "RUTA" },
        { "text": "HORA.INI" },
        { "text": "HORA.FIN" },
        { "text": "DISTANCIA.AB" },
        { "text": "VEL PROM AB" },
        { "text": "TIEMPO PROM AB" }
    ]
    //console.log("DATA_LISTA_REGISTROS", DATA_LISTA_REGISTROS);

    tableDataExportar[0].data.push(tHeadExcel);

    $.each($('#tbViajesPromediados tbody > tr'), function () {
        var fecha = $(this).find('td').eq(0).text();
        var corredor = $(this).find('td').eq(1).text();
        var ruta = $(this).find('td').eq(2).text();
        var horaIni = $(this).find('td').eq(3).text();
        var horaFin = $(this).find('td').eq(4).text();
        var distancia_ab = $(this).find('td').eq(5).text();
        var velocidad_prom_ab = $(this).find('td').eq(6).text();
        var tiempo_prom_ab = $(this).find('td').eq(7).text();
        var itemArr = [];
        itemArr.push(
            { "text": fecha },
            { "text": corredor },
            { "text": ruta },
            { "text": horaIni },
            { "text": horaFin },
            { "text": distancia_ab },
            { "text": velocidad_prom_ab },
            { "text": (tiempo_prom_ab.length == 0 ? 'No hay información para mostrar' : tiempo_prom_ab) }
        )
        tableDataExportar[0].data.push(itemArr);
    });

    //$.each(DATA_LISTA_REGISTROS, function () {
    //    var objeto = this;
    //    var itemArr = [];
    //    itemArr.push(
    //        { "text": objeto.FECHA_REGISTRO.split(' ')[0] },
    //        { "text": objeto.ABREV_CORREDOR },
    //        { "text": objeto.NRO_RUTA },
    //        { "text": Utilidades.ConvertTimeformat24H(objeto.HINICIO) },
    //        { "text": Utilidades.ConvertTimeformat24H(objeto.HFIN) },
    //        { "text": objeto.DISTANCIA_A },
    //        { "text": objeto.DISTANCIA_B },
    //        { "text": Number(objeto.VEL_PROMEDIO_AB).toFixed(2) },
    //        { "text": Number(objeto.VEL_PROMEDIO_BA).toFixed(2) },
    //        { "text": minTommss(objeto.TIEMPO_PROM_A) },
    //        { "text": minTommss(objeto.TIEMPO_PROM_B) },
    //        { "text": objeto.USU_REG },
    //        { "text": objeto.FECHA_REG }
    //    )
    //    tableDataExportar[0].data.push(itemArr);
    //});
    Jhxlsx.export(tableDataExportar, options);
}

function getCorredoresByModalidad() {

    var modalidadTransporteSelecionado = $('#mod').val();
    $('#selectCorredores, #selectRuta').empty();
    var cantidadRegistros = 0;
    //
    $.each(JSON_DATA_CORREDORES, function () {
        if (this.ID_MODALIDAD_TRANS == Number(modalidadTransporteSelecionado)) {
            cantidadRegistros++;
            $('#selectCorredores').append('<option value="' + this.ID_CORREDOR + '">' + this.ABREVIATURA + '</option>');
        }
    });
    if (cantidadRegistros == 0) {
        $('#selectCorredores, #selectRuta').append('<option value="0">' + '-- No hay información --' + '</option>');
        $('#tbRutatipoServicio tbody').empty();
        return false
    }
    //durito
    $('#selectCorredores').val(1)
    getRutaPorCorredor();
}

function getRutaPorCorredor() {

    $.ajax({
        url: URL_GET_RUTA_X_CORREDOR,
        dataType: 'json',
        data: { idCorredor: $('#selectCorredores').val() },
        success: function (result) {
            $('#selectRutas').empty();
            if (result.length == 0) { // si la lista esta vacia
                $('#selectRutas').append('<option value="0">' + '--No hay información--' + '</option>');
                return false;
            } else {
                $.each(result, function () {
                    $('#selectRutas').append('<option value="' + this.ID_RUTA + '">' + this.NRO_RUTA + '</option>');
                });
                getParaderosxRuta($('#selectRutas').val())
            }
            //durito
            $('#selectRutas').val(3)
        }
    }, JSON);
}
