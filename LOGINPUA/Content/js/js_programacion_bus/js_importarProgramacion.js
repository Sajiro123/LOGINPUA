var DATA_POG = [];

var ARCHIVOS_PERMITIDOS = {
    EXCEL: 'xlsx'
}
var ACTION_REEMPLAZAR_DATOS = false;
$(document).ready(function () {
    getCorredoresByModalidad();

    getTipoDias();
    getDataCorredor();
    verificarTipoDia($('#selectTipoDia'));

    $('.tooltip_descargar_excel').tooltipster();
     

    $("#formSubirProgramacion").on("submit", function (e) {
         
        e.preventDefault();
        var formularioDatos = new FormData(document.getElementById("formSubirProgramacion"));  
        var semana = $("#idsemana").val() 
        if (semana == 0) { Swal.fire({ type: 'info', title: "Debe ingresar el semana.", }); return false; }
         
        
        $('#btnSubirArchivo').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Subiendo...');
        $.ajax({
            url: URL_ACTION_UPLOAD_FILE + '/?fecha=' + $('#txtDiasSeleccionados').val()
                                           + '&TipoDia=' + $('#selectTipoDia').val() +
                                           '&reemplazar=' + (ACTION_REEMPLAZAR_DATOS ? 1 : 0) +
                                            '&ruta_ajax=' + $("#selectRutaConsulta option:selected").val() +
                                            '&ruta_text=' + $("#selectRutaConsulta option:selected").text() +
                                             '&semana=' + semana,
            type: "post",
            dataType: "json",
            data: formularioDatos,
            cache: false,
            contentType: false,
            processData: false
        }).done(function (respuesta) {



            ACTION_REEMPLAZAR_DATOS = false;
            $('#btnSubirArchivo').prop('disabled', false).html('Subir archivo');

            if (respuesta.COD_ESTADO == 1) {
                Swal.fire({
                    type: respuesta.COD_ESTADO == 1 ? 'success' : 'error',
                    title: respuesta.DES_ESTADO,
                    showConfirmButton: false,
                });
                $('#modalImportarProgramacion').modal('hide');
                getResumenProgramacion();
            }

            if (respuesta.COD_ESTADO == 0) { //encontró fecha 
                Swal.fire({
                    type: respuesta.COD_ESTADO == 1 ? 'success' : 'error',
                    title: respuesta.DES_ESTADO,
                    showConfirmButton: false,
                });
            }

            if (respuesta.COD_ESTADO == 3) { //encontró fecha 

                var texto = respuesta.DES_ESTADO.bold();
                texto = texto.replace(/ /g, "");
                texto = texto.substring(0, texto.length - 1)
                Swal.fire({
                    title: 'ALERTA !',
                    html: "Se eliminará la programacion de la fecha " + texto,
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Si'
                }).then((result) => {
                    //console.log('resultado prop->>', result);
                    if (result.value) { //si responde que s   
                        ACTION_REEMPLAZAR_DATOS = true;
                        $("#formSubirProgramacion").submit();
                    } else {
                        ACTION_REEMPLAZAR_DATOS = false;
                    }
                });
            }
        });
    })
});


function getCorredoresByModalidad() {
    $('#selectCorredores, #selectRuta').empty();
    var cantidadRegistros = 0;
    $.each(JSON_DATA_CORREDORES, function () {
        if (this.ID_MODALIDAD_TRANS == Number($('#mod').val())) {
            cantidadRegistros++;
            $('#selectCorredores').append('<option value="' + this.ID_CORREDOR + '">' + this.ABREVIATURA + '</option>');
        }
    });

    $('#selectCorredores').val(5);//DURITO BORRAR
    if (cantidadRegistros == 0) {
        $('#selectCorredores, #selectRuta').append('<option value="0">' + '-- No hay información --' + '</option>');
        $('#tbRutatipoServicio tbody').empty();
        var strHTMLtb = '<tr>' +
                             '<td colspan="8" style="padding-left: 0;padding-right: 0;">' +
                                 'No hay información para mostrar' +
                             '</td>' +
                          '</tr>';
        $('#tbRutatipoServicio tbody').append(strHTMLtb);
        return false
    }
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
                getTipoServicioByRuta();
                ListRecorrido($('#selectRutaConsulta').val());

            }
        }
    }, JSON);
}



function getTipoServicioByRuta() {

    $.ajax({
        url: URL_GET_TIPOOPER_X_RUTA,
        dataType: 'json',
        data: { idRuta: $('#selectRutaConsulta').val() },
        success: function (result) {
            //console.log('result-->', result);
            $('#selectRutaTipoServicio').empty();
            if (result.length == 0) {
                $('#selectRutaTipoServicio').append('<option value="0">' + '--No hay información--' + '</option>');
                return false;
            }

            $.each(result, function () {
                $('#selectRutaTipoServicio').append('<option value="' + this.ID_RUTA_TIPO_SERVICIO + '">' + this.TIPO_OPERACIONAL + ' ' + this.NOMBRE + '</option>');
            });
            getResumenProgramacion();
            ListRecorrido($('#selectRutaConsulta').val());

        }
    }, JSON);
}

function ListRecorrido(id_ruta) {

    $.ajax({
        url: URL_GET_SENTIDO_RUTA,
        dataType: 'json',
        data: { idRuta: id_ruta },
        success: function (result) {
            //console.log('result-->', result);
            $('#id_lado').empty();
            if (result.length == 0) {
                $('#id_lado').append('<option value="0">' + '--No hay información--' + '</option>');
                return false;
            }
            $.each(result, function () {
                $('#id_lado').append('<option value="' + this.SENTIDO + '">' + this.LADO + '</option>');
            });
            List_TipoServ(id_ruta);
        }
    }, JSON);
}


function List_TipoServ(idruta) {
    $.ajax({
        url: URL_TIPO_SERV,
        dataType: 'json',
        data: { idRuta: idruta },
        success: function (result) {
            $('#id_TipoServicio').empty();
            if (result.length == 0) { // si la lista esta vacia
                $('#id_TipoServicio').append('<option value="0">' + '--No hay información--' + '</option>');
                return false;
            } else {
                $.each(result, function () {
                    $('#id_TipoServicio').append('<option value="' + this.ID_RUTA_TIPO_SERVICIO + '" >' + this.NOMBRE + '</option>');
                });
            }
        }
    }, JSON);
}



function AbrirModalImportar() {
    var textoDialog = '[' + $("#selectRutaConsulta option:selected").text() + '] ' +
                        $("#selectRutaTipoServicio option:selected").text();
    $('.modal-title').text(textoDialog);

    verificarTipoDia($('#selectTipoDia'))
    $('#txtDiasSeleccionados').val('');
    $('#archivoSubido').val('');
    $('#lblArchivoSubido').text('Seleccionar Archivo');
    $('#modalImportarProgramacion .modal-footer .btn-success').prop('disabled', true);

    getSelectRutaxCorredor($('#selectCorredores').val());

}

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

function subirArchivoTemp(element) {
    var nombreArchivo = element.val().split("\\").pop();
    var extensionArchivo = nombreArchivo.split('.')[1];
    element.siblings(".custom-file-label").html('');
    if (extensionArchivo == ARCHIVOS_PERMITIDOS.EXCEL) {
        $('#modalImportarProgramacion .modal-footer .btn-success').prop('disabled', false);
        element.siblings(".custom-file-label").addClass("selected").html(nombreArchivo);
    } else {
        element.siblings(".custom-file-label").addClass("selected").html("Seleccionar Archivo");
        $('#modalImportarProgramacion .modal-footer .btn-success').prop('disabled', true);
        Swal.fire({
            type: 'error',
            title: 'El archivo ' + nombreArchivo + ' no tiene el formato correcto.',
            showConfirmButton: false,
            timer: 2500
        })
    }
}

function getDataCorredor() {
    var txtCorredor = $("#selectCorredores option:selected").text();
    $('#txtNombreCorredor').text(txtCorredor);
}

function getSelectRutaxCorredor(idCorredor) {
    $.ajax({
        url: URL_GET_RUTA_X_CORREDOR,
        dataType: 'json',
        data: { idCorredor: idCorredor },
        success: function (result) {
            $('#selectRuta').empty();
            if (result.length == 0) { // si la lista esta vacia
                Swal.fire({
                    type: 'error',
                    title: 'No hay rutas para mostrar',
                    showConfirmButton: false,
                    timer: 2000
                })
            } else {
                $('#modalImportarProgramacion').modal('show');
                $.each(result, function () {
                    $('#selectRuta').append('<option value="' + this.ID_RUTA + '">' + this.NRO_RUTA + '</option>');
                });
            }
        }
    }, JSON);
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

function getResumenProgramacion() {

    var strHTML = '';
    $.ajax({
        url: URL_GET_RESUMEN_PROGRAMACION,
        data: { idServicio: $('#selectRutaConsulta option:selected').val() },
        dataType: 'json',
        success: function (result) {

            $('#tbSalidaProgramada tbody').empty();
            if (result.length <= 0) {
                strHTML += '<tr><td colspan="13" class="text-center">No hay información para mostrar</td></tr>';
            } else {
                $.each(result, function (i) {
                    if (this.CANTIDAD_VIAJE > 0) {

                        strHTML += '<tr data-maestro="' + this.ID_MAESTRO_SALIDA_PROG + '" data-fecha="' + this.FECHA_PROGRAMACION + '">' +
                                        '<td>' + (i + 1) + '</td>' +
                                        '<td>' + this.FECHA_PROGRAMACION + '</td>' +
                                        //'<td class="text-center" >' + this.ABREVIATURA + '</td>' +
                                        //'<td class="text-center" >' + this.NRO_RUTA + '</td>' +
                                        '<td class="text-center" >' + this.SEMANA + '</td>' +
                                        '<td>' + this.TIPO_DIA + '</td>' +
                                         '<td>' + this.CANTIDAD_VIAJE + '</td>' +
                                         '<td>' + '<button type="button"  class="btn btn-info btn-sm" data-toggle="modal" style="font-size: 11px;" onclick="verPOG($(this));"><span class="fa fa-eye"></span>&nbsp;&nbsp;POG</button>' + '</td>' +
                                         '<td>' + '<button type="button"  class="btn btn-dark btn-sm" data-toggle="modal" style="font-size: 11px;" onclick="AgregarNuevoViajeModal($(this));"><i class="fas fa-road"></i></button>' + '</td>' +
                                     '<td>' + this.USU_REG + '</td>' +
                                        '<td class="text-center">' + this.FECHA_REG + '</td>' +
                                   '</tr>';
                    }
                });
            }
            $('#tbSalidaProgramada tbody').append(strHTML);
        }, error: function (xhr, status, error) {
            $('#tbSalidaProgramada tbody').empty();
            strHTML += '<tr><td colspan="13" class="text-center">No hay información para mostrar</td></tr>';
            $('#tbSalidaProgramada tbody').append(strHTML);
        },

    }, JSON);
}

var FECHA_TEMP = '27/02/2020';

var IDMAESTROSALIDAPROG_ = 0;

function AgregarNuevoViajeModal(element) {

    $('#id_TipoDia').val('');
    $('#id_SERVICIO').val('');
    $('#id_FNODE').val('');
    $('#id_TNODE').val('');
    $('#id_HLLEGADA').val('');
    $('#id_HSALIDA').val('');
    $('#id_DISTANCIA').val('');
    $('#id_triptime').val('');



    IDMAESTROSALIDAPROG_ = element.parent().parent().attr('data-maestro')
    var fecha = element.parent().parent().attr('data-fecha')
    var arrayfecha = fecha.split('-')
    $('#id_fechaNuevoViaje').empty();
    $.each(arrayfecha, function () {//
        $('#id_fechaNuevoViaje').append('<option>' + this + '</option>')
    });

    $('#modal_AgregarNuevaSalida').modal('show')

    $.ajax({
        url: URL_LISTSERVICIOMAESTROPROG,
        dataType: 'json',
        data: {
            id_maestro_salidaProgramada: IDMAESTROSALIDAPROG_
        },
        success: function (result) {

            $('#id_TipoDia').val(result.Table[0].TIPO_DIA)
            var servicio = result.Table[0].SERVICIO
            servicio = servicio + 1
            $('#id_SERVICIO').val(servicio)

        }
    }, JSON);
}

function RegistrarNuevoViaje() {


    var tipoDia = $('#id_TipoDia').val();
    var nroServicio = $('#id_SERVICIO').val();
    var id_TipoServicio = $('#id_TipoServicio option:selected').val();
    var fnode = $('#id_FNODE').val();
    var id_TNODE = $('#id_TNODE').val();
    var id_HLLEGADA = $('#id_HLLEGADA').val();
    var id_HSALIDA = $('#id_HSALIDA').val();
    var sentido = $('#id_lado option:selected').val();
    var id_DISTANCIA = $('#id_DISTANCIA').val();
    var id_triptime = $('#id_triptime').val();
    var id_fechaNuevoViaje = $('#id_fechaNuevoViaje option:selected').val();

    if (fnode == "") { Swal.fire({ type: 'info', title: "Debe ingresar el campo fnode.", }); return false; }
    if (id_TNODE == "") { Swal.fire({ type: 'info', title: "Debe ingresar el campo tnode.", }); return false; }
    if (id_HLLEGADA == "") { Swal.fire({ type: 'info', title: "Debe ingresar el campo hllegada.", }); return false; }
    if (id_HSALIDA == "") { Swal.fire({ type: 'info', title: "Debe ingresar el campo hsalida.", }); return false; }
    if (id_DISTANCIA == 0) { Swal.fire({ type: 'info', title: "Debe ingresar el campo distancia .", }); return false; }



    $.ajax({
        url: URL_REGISTRARNUEVOVIAJE,
        dataType: 'json',
        data: {
            id_maestro_salidaProgramada: IDMAESTROSALIDAPROG_,
            tipoDia: tipoDia,
            nroServicio: nroServicio,
            pog: '',
            pot: '',
            fnode: fnode,
            hsalida: id_HSALIDA,
            hllegada: id_HLLEGADA,
            tnode: id_TNODE,
            PIG: '',
            minutosDiferenciaLayover: 0,
            acumulado: 0,
            sentido: sentido,
            turno: 1,
            idtiposerv: id_TipoServicio,
            trip_time: id_triptime,
            distancia: id_DISTANCIA,
            placa: '',
            cacConductor: '',
            fecha: id_fechaNuevoViaje
        },
        success: function (result) {

            $('#modal_AgregarNuevaSalida').modal('hide')

 
            if (result.COD_ESTADO == 1) {
                Swal.fire({
                    type: result.COD_ESTADO == 1 ? 'success' : 'error',
                    title: result.DES_ESTADO,
                });
                getResumenProgramacion();
            }
        }
    }, JSON);

}
function verPOG(element) {

    var Count_ViajeFinal_7_to_9 = 0;
    var Count_ViajeFinal_9_to_10 = 0;
    var Count_ViajeFinal_10_to_11 = 0;
    var Count_ViajeFinal_11_to_12 = 0;
    var Count_ViajeFinal_12_to_13 = 0;
    var Count_ViajeFinal_13_to_14 = 0;
    var Count_ViajeFinal_14_to_15 = 0;
    var Count_ViajeFinal_15_to_16 = 0;
    var Count_ViajeFinal_16_to_17 = 0;
    var Count_ViajeFinal_17_to_18 = 0;
    var Count_ViajeInicial_13_to_14 = 0;
    var Count_ViajeInicial_14_to_15 = 0;
    var Count_ViajeInicial_15_to_16 = 0;
    var Count_ViajeInicial_16_to_17 = 0;
    var Count_ViajeInicial_17_to_18 = 0;
    var Count_ViajeFinal_19_to_20 = 0;
    var Count_ViajeFinal_20_to_21 = 0;
    var Count_ViajeFinal_21_to_22 = 0;
    var Count_ViajeFinal_22_to_23 = 0;
    var Count_POT_8_a_9 = 0;
    var Count_POT_9_a_10 = 0;
    var Count_POT_10_a_11 = 0;
    var Count_POT_11_a_12 = 0;
    var Count_POT_12_a_13 = 0;
    var count = 0;
    var total = 0;
    var i = 1;

    var idruta = $('#selectRutaConsulta option:selected').text()
    var idMaestroSalidaProg_ = element.parent().parent().attr('data-maestro')

    $('#idModalPOG').modal('show')

    $.ajax({
        url: URL_GET_DATA_VIAJES,
        data: { idMaestroSalidaProg: idMaestroSalidaProg_ },
        dataType: 'json',
        success: function (result) {
            //limpiar tabla
            $('#tbModalPOG').children('tbody').children().each(function () {
                $(this).remove()
            });

            if (result.length < 1) {
                $('#_ModalPOGRegistro').modal('show')
                $('#idModalPOG').modal('hide')
                return false
            }
            //TRAER LOS PARAMETROS PARA EL ANALISIS 
            var FnodeA = result[0].FNODE_A
            var TnodeA = result[0].TNODE_A
            var FnodeB = result[0].FNODE_B
            var TnodeB = result[0].TNODE_B
            var distanciaA = result[0].DISTANCIA_A
            var distanciaB = result[0].DISTANCIA_B

            var salto = 60;
            var dataFranjaHoras = util.obtenerHorasEnTimestampPOG(FECHA_TEMP + ' ' + '04:00:00', FECHA_TEMP + ' ' + '22:00:00', salto);
            var dataFranjaHorasLimpia = util.obtenerHorasEnTimestamp(FECHA_TEMP + ' ' + '04:00:00', FECHA_TEMP + ' ' + '22:00:00', salto);
            var distancias = 0.0

            $.each(result, function () {//
                distancias += parseFloat(this.DISTANCIA) || 0.0;
                if (this.POG != null) {//solo los primeros viajes 
                    verificaViajePorFranjaHoraria(this, dataFranjaHoras);//
                }
                //DATA SIN FILTRO
                verificaViajePorFranjaHoraria(this, dataFranjaHorasLimpia);// 
            });

            var ultimosViajes = getViajesPIG(result);

            $.each(ultimosViajes, function () {//agregando los viajes PIG en cada franja (solo los viajes finales)
                setViajesPIGinFranja(this, dataFranjaHoras);
            });
            var arrayPromedioGeneral = []


            //VELOCIDAD
            var PromedioVelLadoA = PromedioVelocidadesLado(dataFranjaHorasLimpia, arrayPromedioGeneral, FnodeA, TnodeA, distanciaA)
            var PromedioVelLadoB = PromedioVelocidadesLado(dataFranjaHorasLimpia, arrayPromedioGeneral, TnodeB, FnodeB, distanciaB)

            //TIEMPO
            var PromedioTiempoLadoA = PromedioTiempoLado(dataFranjaHorasLimpia, arrayPromedioGeneral, FnodeA, TnodeA)
            var PromedioTiempoLadoB = PromedioTiempoLado(dataFranjaHorasLimpia, arrayPromedioGeneral, TnodeB, FnodeB)

            //INTERVALO
            var PromedioTiempoIntervaloLadoA = PromedioTiempoIntervalosLado(dataFranjaHorasLimpia, arrayPromedioGeneral, FnodeA)
            var PromedioTiempoIntervaloLadoB = PromedioTiempoIntervalosLado(dataFranjaHorasLimpia, arrayPromedioGeneral, TnodeB)

            //VELOCIDAD OPERACIONAL
            var PromedioVelocidadOperacionalArray = []
            var PromedioVelocidadOperacional

            $.each(PromedioVelLadoA, function (i) {//agregando los viajes PIG en cada franja (solo los viajes finales)
                if (i > 0) {
                    PromedioVelocidadOperacionalArray.push(this)
                }
            });
            $.each(PromedioVelLadoB, function (i) {//agregando los viajes PIG en cada franja (solo los viajes finales)
                if (i > 0) {
                    PromedioVelocidadOperacionalArray.push(this)
                }
            });
            //PROMEDIO DE VELOCIDADES OPERACIONALES
            var PromedioVelocidadOperacional = PromedioArray(PromedioVelocidadOperacionalArray);
            PromedioVelocidadOperacional = round2Deciamles(PromedioVelocidadOperacional);


            //Armar los viajes finales totales SUMADOS
            $.each(dataFranjaHoras, function (i) {
                var item = {}
                for (i; i < PromedioVelLadoA.length; i++) {
                    item = {
                        PromedioVelocidadA: PromedioVelLadoA[i],
                        PromedioVelocidadB: PromedioVelLadoB[i],
                        TiempoPromedioA: PromedioTiempoLadoA[i],
                        TiempoPromedioB: PromedioTiempoLadoB[i],
                        TiempoIntervaloA: PromedioTiempoIntervaloLadoA[i],
                        TiempoIntervaloB: PromedioTiempoIntervaloLadoB[i],
                    }
                    break;
                }
                this.CamposPOG.push(item);
                $.each(this.arrViajes, function (position, j) {

                    var POT_ = util.convertDatetoTimeStamp(FECHA_TEMP + ' ' + this.POT);
                    //8 a 9 
                    if (POT_ >= 1582808400000 && POT_ <= 1582811940000) {
                        Count_POT_8_a_9 += $(POT_).length
                    }
                    //9 a 10 
                    if (POT_ >= 1582812000000 && POT_ <= 1582815540000) {
                        Count_POT_9_a_10 += $(POT_).length
                    }
                    //10 a 11 
                    if (POT_ >= 1582815600000 && POT_ <= 1582819140000) {
                        Count_POT_10_a_11 += $(POT_).length
                    }
                    //11 a 12 
                    if (POT_ >= 1582819200000 && POT_ <= 1582822740000) {
                        Count_POT_11_a_12 += $(POT_).length
                    }
                    //12 a 13 
                    if (POT_ >= 1582822800000 && POT_ <= 1582826340000) {
                        Count_POT_12_a_13 += $(POT_).length
                    }
                });

                //Armar los viajes finales totales SUMADOS 

                if (this.inicioTimestamp == 1582804800000 || this.inicioTimestamp == 1582808400000) {
                    Count_ViajeFinal_7_to_9 += $(this.arrViajesFinal).length
                }
                if (this.inicioTimestamp == 1582812000000) {
                    Count_ViajeFinal_9_to_10 += $(this.arrViajesFinal).length
                }
                if (this.inicioTimestamp == 1582815600000) {
                    Count_ViajeFinal_10_to_11 += $(this.arrViajesFinal).length
                }
                if (this.inicioTimestamp == 1582819200000) {
                    Count_ViajeFinal_11_to_12 += $(this.arrViajesFinal).length
                }
                if (this.inicioTimestamp == 1582822800000) {
                    Count_ViajeFinal_12_to_13 += $(this.arrViajesFinal).length
                }
                if (this.inicioTimestamp == 1582826400000) {
                    Count_ViajeFinal_13_to_14 += $(this.arrViajesFinal).length
                    Count_ViajeInicial_13_to_14 += $(this.arrViajes).length
                }
                if (this.inicioTimestamp == 1582830000000) {
                    Count_ViajeFinal_14_to_15 += $(this.arrViajesFinal).length
                    Count_ViajeInicial_14_to_15 += $(this.arrViajes).length
                }
                if (this.inicioTimestamp == 1582833600000) {
                    Count_ViajeFinal_15_to_16 += $(this.arrViajesFinal).length
                    Count_ViajeInicial_15_to_16 += $(this.arrViajes).length
                }
                if (this.inicioTimestamp == 1582837200000) {
                    Count_ViajeFinal_16_to_17 += $(this.arrViajesFinal).length
                    Count_ViajeInicial_16_to_17 += $(this.arrViajes).length
                }
                if (this.inicioTimestamp == 1582840800000) {
                    Count_ViajeFinal_17_to_18 += $(this.arrViajesFinal).length
                    Count_ViajeInicial_17_to_18 += $(this.arrViajes).length
                }
                if (this.inicioTimestamp == 1582848000000) {
                    Count_ViajeFinal_19_to_20 += $(this.arrViajesFinal).length
                }
                if (this.inicioTimestamp == 1582851600000) {
                    Count_ViajeFinal_20_to_21 += $(this.arrViajesFinal).length
                }
                if (this.inicioTimestamp == 1582855200000) {
                    Count_ViajeFinal_21_to_22 += $(this.arrViajesFinal).length
                }
                if (this.inicioTimestamp == 1582858800000) {
                    Count_ViajeFinal_22_to_23 += $(this.arrViajesFinal).length
                }
            });


            //RESTAR SALIDA CON ENTRADA 
            Count_ViajeFinal_7_to_9 = Count_ViajeFinal_7_to_9 - Count_POT_8_a_9
            Count_ViajeFinal_9_to_10 = Count_ViajeFinal_9_to_10 - Count_POT_9_a_10
            Count_ViajeFinal_10_to_11 = Count_ViajeFinal_10_to_11 - Count_POT_10_a_11
            Count_ViajeFinal_11_to_12 = Count_ViajeFinal_11_to_12 - Count_POT_11_a_12
            Count_ViajeFinal_12_to_13 = Count_ViajeFinal_12_to_13 - Count_POT_12_a_13

            Count_ViajeFinal_13_to_14 = Count_ViajeInicial_13_to_14 - Count_ViajeFinal_13_to_14
            Count_ViajeFinal_14_to_15 = Count_ViajeInicial_14_to_15 - Count_ViajeFinal_14_to_15
            Count_ViajeFinal_15_to_16 = Count_ViajeInicial_15_to_16 - Count_ViajeFinal_15_to_16
            Count_ViajeFinal_16_to_17 = Count_ViajeInicial_16_to_17 - Count_ViajeFinal_16_to_17
            Count_ViajeFinal_17_to_18 = Count_ViajeInicial_17_to_18 - Count_ViajeFinal_17_to_18


            var strHTML = '';
            var CantBuses = 0;
            var count = 1;
            var mayorCantBus = 0



            var PromedioVelocidadNS = 0
            var PromedioVelocidadSN = 0



            //ARMAR TABLA EN MODAL POG 
            $.each(dataFranjaHoras, function (j, i) {
                var tiempoNS = timeToDecimal(this.CamposPOG[0].TiempoPromedioA)
                var tiempoSN = timeToDecimal(this.CamposPOG[0].TiempoPromedioB)
                var TiempoTotal = tiempoNS + tiempoSN
                TiempoTotal = decimaltoTime(TiempoTotal)
                var getArrayCantBuses = 0;

                //RESTAR LOS VIAJES FINALES
                if (j == 0) {
                    CantBuses += this.arrViajes.length;
                }
                if (j == 1) { CantBuses += this.arrViajes.length; }
                else if (j == 2) { CantBuses += this.arrViajes.length; }
                else if (j == 3) { CantBuses += this.arrViajes.length; }
                else if (j == 4) { CantBuses = CantBuses - Count_ViajeFinal_7_to_9; }
                else if (j == 5) { CantBuses = CantBuses - Count_ViajeFinal_9_to_10; }
                else if (j == 6) { CantBuses = CantBuses - Count_ViajeFinal_10_to_11; }
                else if (j == 7) { CantBuses = CantBuses - Count_ViajeFinal_11_to_12; }
                else if (j == 8) { CantBuses = CantBuses - Count_ViajeFinal_12_to_13; }
                else if (j == 9) { CantBuses = CantBuses + Count_ViajeFinal_13_to_14; }
                else if (j == 10) { CantBuses = CantBuses + Count_ViajeFinal_14_to_15; }
                else if (j == 11) { CantBuses = CantBuses + Count_ViajeFinal_15_to_16; }
                else if (j == 12) { CantBuses = CantBuses + Count_ViajeFinal_16_to_17; }
                else if (j == 13) { CantBuses = CantBuses + Count_ViajeFinal_17_to_18; }
                else if (j == 14) { CantBuses = mayorCantBus; }
                else if (j == 15) { CantBuses = CantBuses - Count_ViajeFinal_19_to_20; }
                else if (j == 16) { CantBuses = CantBuses - Count_ViajeFinal_20_to_21; }
                else if (j == 17) { CantBuses = CantBuses - Count_ViajeFinal_21_to_22; }
                else if (j == 18) { CantBuses = CantBuses - Count_ViajeFinal_22_to_23; }
                if (j < 14) {
                    getArrayCantBuses = CantBuses
                    if (mayorCantBus < getArrayCantBuses) {
                        mayorCantBus = getArrayCantBuses
                    }
                }

                //REDONDEAR 
                var TiempoPromedioA = timeRedondear(this.CamposPOG[0].TiempoPromedioA)
                var TiempoPromedioB = timeRedondear(this.CamposPOG[0].TiempoPromedioB)
                TiempoTotal = timeRedondear(TiempoTotal)
                var KM_TOTAL = round2Deciamles(distancias);


                //CREANDO LA DATA PARA EXPORTAR
                var array_to_export_pog = {
                    SERVICIO: idruta,
                    HI: (this.hInicio == null ? "" : this.hInicio),
                    HF: (this.hFin == null ? "" : this.hFin),
                    CANTIDAD_BUSES: (CantBuses == null ? "" : CantBuses),
                    V_N_S: (this.CamposPOG[0].PromedioVelocidadA == null ? "" : Math.round(round2Deciamles(this.CamposPOG[0].PromedioVelocidadA))),
                    V_S_N: (this.CamposPOG[0].PromedioVelocidadB == null ? "" : Math.round(round2Deciamles(this.CamposPOG[0].PromedioVelocidadB))),
                    T_S_N: (TiempoPromedioA == null ? "" : TiempoPromedioA),
                    T_N_S: (TiempoPromedioB == null ? "" : TiempoPromedioB),
                    T_TOTAL: (TiempoTotal == null ? "" : TiempoTotal),
                    IN_NS: (this.CamposPOG[0].TiempoIntervaloA == null ? "" : this.CamposPOG[0].TiempoIntervaloA),
                    IN_SN: (this.CamposPOG[0].TiempoIntervaloB == null ? "" : this.CamposPOG[0].TiempoIntervaloB),
                    D_N_S: distanciaA,
                    D_S_N: distanciaB,
                    T_KM: KM_TOTAL,
                    V_P: PromedioVelocidadOperacional
                }
                DATA_POG.push(array_to_export_pog)



                strHTML += '<tr> ' +
                      '<td class="text-center" >' + count++ + '</td>' +
                       '<td class="text-center" >' + idruta + '</td>' +
                      '<td class="text-center" >' + (this.hInicio == null ? "" : this.hInicio) + '</td>' +
                      '<td class="text-center" >' + (this.hFin == null ? "" : this.hFin) + '</td>' +
                      '<td class="text-center" >' + (CantBuses == null ? "" : CantBuses) + '</td>' +
                      '<td class="text-center" >' + (this.CamposPOG[0].PromedioVelocidadA == null ? "" : Math.round(round2Deciamles(this.CamposPOG[0].PromedioVelocidadA))) + '</td>' +
                      '<td class="text-center" >' + (this.CamposPOG[0].PromedioVelocidadB == null ? "" : Math.round(round2Deciamles(this.CamposPOG[0].PromedioVelocidadB))) + '</td>' +
                      '<td class="text-center" >' + (TiempoPromedioA == null ? "" : TiempoPromedioA) + '</td>' +
                      '<td class="text-center" >' + (TiempoPromedioB == null ? "" : TiempoPromedioB) + '</td>' +
                      '<td class="text-center" >' + (TiempoTotal == null ? "" : TiempoTotal) + '</td>' +
                       '<td class="text-center" >' + (this.CamposPOG[0].TiempoIntervaloA == null ? "" : this.CamposPOG[0].TiempoIntervaloA) + '</td>' +
                      '<td class="text-center" >' + (this.CamposPOG[0].TiempoIntervaloB == null ? "" : this.CamposPOG[0].TiempoIntervaloB) + '</td>' +
                      '<td class="text-center" >' + distanciaA + '</td>' +
                      '<td class="text-center" >' + distanciaB + '</td>' +
                      '<td class="text-center" >' + KM_TOTAL + '</td>' +
                      '<td class="text-center" >' + PromedioVelocidadOperacional + '</td>' +
            '</tr>';
            });
            $('#tbModalPOG').append(strHTML);

            //ARMAR COLUMNA SERVICIO
            $.each($('#tbModalPOG').children('tbody').children(), function (j, i) {
                if (j != 0) {
                    $($(this).children()[1]).remove();
                    $($(this).children()[11]).remove()
                    $($(this).children()[12]).remove()

                } else {
                    $($(this).children()[1]).attr('rowspan', 19)
                    $($(this).children()[12]).attr('rowspan', 19)
                    $($(this).children()[13]).attr('rowspan', 19)
                    $($(this).children()[14]).attr('rowspan', 19)
                    $($(this).children()[15]).attr('rowspan', 19)
                }
            });

            $.each($('#tbModalPOG').children('tbody').children(), function (j, i) {
                if (j > 0) {
                    $($(this).children()[11]).remove()
                    $($(this).children()[11]).remove()
                }
            });
        }, error: function (xhr, status, error) {

        },
    }, JSON);
}



var tableDataExportar = [{
    "sheetName": "PROG_107_HÁBIL",
    "data": []
}];

function exportarTabla() {

    //$('#exportar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Exportando...');
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = dd + '_' + mm + '_' + yyyy;


    tableDataExportar = [{
        "sheetName": "Hoja",
        "data": []
    }];
    var options = {
        fileName: "Reporte Buses " + today
    };


    var tHeadExcel1 = [
         { "text": "" },
        {
            "text": "SERVICIO",
            "merge": {
                r: 1,
                c: 0
            },
            "style": {
                "font": {
                    "sz": "50"
                }
            }
        },
        {
            "text": "FRANJA HORARIA",
            "merge": {
                r: 0,
                c: 1
            }
        },
        { "text": "" },
        { "text": "CANTIDAD DE BUSES" },
        {
            "text": "VELOCIDAD   ",
            "merge": {
                r: 0,
                c: 1
            }
        },
        { "text": "" },
        {
            "text": "   TIEMPO",
            "merge": {
                r: 0,
                c: 1
            }
        },
        { "text": "" },
        { "text": "TIEMPO TOTAL" },
        {
            "text": "INTERVALO",
            "merge": {
                r: 0,
                c: 1
            }
        },
        { "text": "" },
        {
            "text": "DISTANCIA  KILOMETRO",
            "merge": {
                r: 0,
                c: 1
            }
        },
        { "text": "" },
        { "text": "KM TOTAL" },
        { "text": "VELOCIDAD OPERACIONAL" }
    ]
    var tHeadExcel2 = [
         { "text": "" },
       { "text": "" },
       { "text": "HI" },
       { "text": "HF" },
       { "text": "" },
       { "text": "N-S" },
       { "text": "S-N" },
       { "text": "N-S" },
       { "text": "S-N" },
       { "text": "" },
       { "text": "N-S" },
       { "text": "S-N" },
       { "text": "N-S" },
       { "text": "S-N" },
       { "text": "" },
       { "text": "" }
    ]
    var tHeadExcel3 = [
     { "text": "" },
     { "text": "" },
     { "text": "" },
     { "text": "" },
     { "text": "" },
     { "text": "" },
     { "text": "" },
     { "text": "" },
     { "text": "" },
     { "text": "" },
     { "text": "" },
     { "text": "" },
     { "text": "" },
     { "text": "" },
     { "text": "" }
    ]
    tableDataExportar[0].data.push(tHeadExcel3);
    tableDataExportar[0].data.push(tHeadExcel3);

    tableDataExportar[0].data.push(tHeadExcel1);
    tableDataExportar[0].data.push(tHeadExcel2);


    $.each(DATA_POG, function (i) {

        var objeto = this;
        var itemArr = [];
        itemArr.push(
             { "text": "    " },
            {
                "text": objeto.SERVICIO + "  ",
            },
            { "text": objeto.HI },
            { "text": objeto.HF },
            { "text": objeto.CANTIDAD_BUSES },
            { "text": objeto.V_N_S + "  " },
            { "text": objeto.V_S_N + "  " },
            { "text": objeto.T_S_N + "  " },
            { "text": objeto.T_N_S + "  " },
            { "text": objeto.T_TOTAL },
            { "text": objeto.IN_NS },
            { "text": objeto.IN_SN },
            { "text": objeto.D_N_S + "  " },
            { "text": objeto.D_S_N + "  " },
            { "text": objeto.T_KM },
            { "text": objeto.V_P }
        )
        tableDataExportar[0].data.push(itemArr);

    });
    Jhxlsx.export(tableDataExportar, options);
    $('#exportar').prop('disabled', false).html('Exportar');
}




function AgregarCamposPOG() {

    var idruta = $("#selectRutaConsulta option:selected").val();
    var FnodeA = $("#idFnodeA").val();
    var TnodeA = $("#idTnodeA").val();
    var FnodeB = $("#idFnodeB").val();
    var TnodeB = $("#idTnodeB").val();
    var DistanciaA = $("#idDistanciaA").val();
    var DistanciaB = $("#idDistanciaB").val();

    $.ajax({
        url: URL_GET_AGREGAR_CAMPOS_POG,
        data: {
            idruta: idruta,
            FnodeA: FnodeA,
            TnodeA: TnodeA,
            FnodeB: FnodeB,
            TnodeB: TnodeB,
            DistanciaA: DistanciaA,
            DistanciaB: DistanciaB
        },
        dataType: 'json',
        success: function (result) {
            Swal.fire({
                type: result.COD_ESTADO == 1 ? 'success' : 'error',
                title: result.DES_ESTADO,
                showConfirmButton: false,
            });
            if (result.COD_ESTADO == 1) {
                $('#_ModalPOGRegistro').modal('hide');
            }
        }
    }, JSON);

}

function setViajesPIGinFranja(objeto, franja) {//ultimos viajes

    $.each(franja, function () {
        var tmstmIniRango_ = util.convertDatetoTimeStamp(FECHA_TEMP + ' ' + this.hInicio);
        var tmstmFinRango_ = util.convertDatetoTimeStamp(FECHA_TEMP + ' ' + this.hFin);
        var tmstmFecSalida_ = util.convertDatetoTimeStamp(FECHA_TEMP + ' ' + objeto.HLLEGADA);

        if (tmstmFecSalida_ >= tmstmIniRango_ && tmstmFecSalida_ <= tmstmFinRango_) {
            var item = objeto;
            this.arrViajesFinal.push(item);
        }
    });
}




function getViajesPIG(dataJSON) {//ultimos viajes
    var rpta = [];
    $.each(dataJSON, function () {
        if (this.PIG != null) {
            rpta.push(this);
        }
    });
    return rpta;
}

function verificaViajePorFranjaHoraria(objSalida, dataFranjaHoraria) {
    //objSalida.arrViajesFinal = [];
    $.each(dataFranjaHoraria, function () {
        this.arrViajesFinal = [];

        var hora_salida = objSalida.HSALIDA
        hora_salida = hora_salida.split(':')
        var hora = hora_salida[0]
        var minutos = hora_salida[1]
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



function PromedioVelocidadesLado(dataFranjaHoras, arrayPromedioGeneral, Ida, Vuelta, distancia) {

    arrayPromedioGeneral = []

    $.each(dataFranjaHoras, function (i) {
        var velocidad = 0.0
        var arrayPromedio = []

        $.each(this.arrViajes, function (position, j) {
            //PROMEDIO DE LA VELOCIDAD POR TRAMOS
            if (this.FNODE == Ida && this.TNODE == Vuelta) {

                var trip_T = this.TRIP_TIME
                trip_T = timeToDecimal(trip_T)
                velocidad = (distancia / (trip_T * 24)) * 24//FORMULA
                arrayPromedio.push(velocidad)
            }
        });
        arrayPromedioGeneral.push(arrayPromedio)
    });
    var promedioGeneral = []

    //PROMEDIOS
    $.each(arrayPromedioGeneral, function () {

        var promedio = PromedioArray(this)
        promedio = parseFloat(promedio) || 0;
        promedioGeneral.push(promedio)
    });
    return promedioGeneral//ENVIAR ARRAY DE LOS  PROMEDIOS

}


function PromedioTiempoLado(dataFranjaHoras, arrayPromedioGeneral, Ida, Vuelta) {

    arrayPromedioGeneral = []

    $.each(dataFranjaHoras, function (i) {
        var velocidad = 0.0

        var arrayPromedio = []
        $.each(this.arrViajes, function (position, j) {
            //PROMEDIO DE LA VELOCIDAD POR TRAMOS
            if (this.FNODE == Ida && this.TNODE == Vuelta) {
                var trip_T = this.TRIP_TIME
                trip_T = timeToDecimal(trip_T)
                arrayPromedio.push(trip_T)
            }
        });
        arrayPromedioGeneral.push(arrayPromedio)
    });
    var promedioGeneral = []

    //PROMEDIOS
    $.each(arrayPromedioGeneral, function () {
        var promedio = PromedioArray(this)
        promedio = parseFloat(promedio) || 0.0;
        var time = decimaltoTime(promedio)
        promedioGeneral.push(time)
    });
    return promedioGeneral//ENVIAR ARRAY DE LOS  PROMEDIOS
}


function PromedioTiempoIntervalosLado(dataFranjaHoras, arrayPromedioGeneral, Ida) {

    arrayPromedioGeneral = []
    var contador = 0
    var HSALIDAanterior = 0.0

    $.each(dataFranjaHoras, function (i) {
        var arrayPromedio = []
        $.each(this.arrViajes, function (position, j) {
            //PROMEDIO DE LA VELOCIDAD POR TRAMOS
            if (this.FNODE == Ida) {
                var HSALIDA = this.HSALIDA
                arrayPromedio.push(HSALIDA)
            }
        });
        arrayPromedioGeneral.push(arrayPromedio)
    });

    var arraynuevo = []
    //order by viajes
    $.each(arrayPromedioGeneral, function () {
        arraynuevo.push(this.sort())
    });


    var arraynuevo3 = []

    //RESTAR VIAJES 
    $.each(arraynuevo, function () {
        var resta = 0.0
        var HSALIDA = this
        var arraynuevo2 = []

        $.each(HSALIDA, function () {
            contador++

            if (contador > 1) {
                HSALIDA = timeToDecimal(this)
                HSALIDAanterior = timeToDecimal(HSALIDAanterior)
                resta = HSALIDA - HSALIDAanterior
                arraynuevo2.push(resta)
            }
            HSALIDAanterior = this

        });
        arraynuevo3.push(arraynuevo2)
    });

    var promedioGeneral = []

    //PROMEDIOS
    $.each(arraynuevo3, function () {
        var promedio = PromedioArray(this)
        promedio = parseFloat(promedio) || 0.0;
        var time = decimaltoTime(promedio)

        var minutos = 0
        var segundos = 0;
        var tiemposeparado = time.split(':')
        var tiempofinal = "";
        minutos = parseInt(tiemposeparado[1])

        if (tiemposeparado[2] > 59) {
            minutos = minutos + 1
            minutos = minutos
            tiempofinal = "00:0" + minutos
        } else {
            if (minutos > 9) {
                tiempofinal = "00:" + minutos
            } else {
                tiempofinal = "00:0" + minutos
            }
        }

        promedioGeneral.push(tiempofinal)
    });
    return promedioGeneral//ENVIAR ARRAY DE LOS  PROMEDIOS
}



//MATEMATICA
function timeRedondear(time) {

    var minutos = 0
    var tiemposeparado = time.split(':')
    var hora = tiemposeparado[0];

    var tiempofinal = "";
    minutos = parseInt(tiemposeparado[1])

    if (tiemposeparado[2] > 59) {
        minutos = minutos + 1
        if (minutos > 9) {
            tiempofinal = hora + ":" + minutos
        } else {
            tiempofinal = hora + ":0" + minutos
        }
    } else {
        if (minutos > 9) {
            tiempofinal = hora + ":" + minutos
        } else {
            tiempofinal = hora + ":0" + minutos
        }
    }
    return tiempofinal
};

function timeToDecimal(t) {
    return t.split(':')
            .map(function (val) { return parseInt(val, 10); })
            .reduce(function (previousValue, currentValue, index, array) {
                return previousValue + currentValue / Math.pow(60, index);
            });
};




function PromedioArray(array) {
    var sum = 0;
    var elmt = 0;
    $.each(array, function () {
        sum += parseFloat(this, 10); //don't forget to add the base  
        elmt += $(this).length;
    });
    var avg = sum / elmt;
    //avg = Math.round(avg)
    return avg;
}
function decimaltoTime(decimalTimeString) {


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
    var tiempo = "" + hours + ":" + minutes + ":" + seconds
    return tiempo;
}

function round2Deciamles(num, decimales) {
    decimales = 2;
    var signo = (num >= 0 ? 1 : -1);
    num = num * signo;
    if (decimales === 0) //con 0 decimales
        return signo * Math.round(num);
    // round(x * 10 ^ decimales)
    num = num.toString().split('e');
    num = Math.round(+(num[0] + 'e' + (num[1] ? (+num[1] + decimales) : decimales)));
    // x * 10 ^ (-decimales)
    num = num.toString().split('e');
    return signo * (num[0] + 'e' + (num[1] ? (+num[1] - decimales) : -decimales));
}