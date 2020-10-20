var DATAJSONAUTOCOMPLETE_CONDUCTOR = []
var DATAJSONAUTOCOMPLETE_UNIDAD = []

var SERVICIO = 0
var IDSALIDA_PROGDETA = 0
var IDSALIDA_ESPECIFICO = 0
var HORA_SALIDA_FINAL = "";

var util = new Util();
var Validaciones = new validaciones();

$(document).ready(function () {


    $("#conduc_cac").click(function () {
        if ($("#conduc_cac").is(':checked')) {
            $('#conductor_div').css('display', 'none')
            $('#conductor_div2').css('display', 'none')
        } else {
            $('#conductor_div').css('display', 'block')
            $('#conductor_div2').css('display', 'block')

        }
    });




    $(".simpleExample").timepicker({
    });


    $('.timepicker').timepicker('option', 'hours', { starts: 05, ends: 23 });

    getSelectRutaxCorredor($("#txtRuta").val());
    getLado_X_Ruta($('#txtRuta').val());
    getPlaca_Buses($('#txtRuta').val());
    getConductores();


    $('#txtRuta').on('change', function () {
        getSelectRutaxCorredor($("#txtRuta").val());
        $('#txtrutaserv-button').children('span').css('font-size', '11px')
        $('#txtrutaserv-button').children('span').css('margin-left', '-15px')
        getLado_X_Ruta($('#txtRuta').val());

        getPlaca_Buses($('#txtRuta').val());

    });

    $('#txtrutaserv').on('change', function () {
        $('#txtrutaserv-button').children('span').css('font-size', '11px')
        $('#txtrutaserv-button').children('span').css('margin-left', '-15px')
    });

    $('#txtrutaserv-button').css('height', '46px')

    $('#txtDispo').on('click', function () {
        if ($(this).is(':checked')) {

            $('#idDespachoUnidad_Serv').children('tbody').children().each(function () {
                if ($(this).attr('data-vacio') == 0) {
                    $(this).css('display', 'none')
                }
            });

        } else {

            $('#idDespachoUnidad_Serv').children('tbody').children().each(function () {
                if ($(this).attr('data-vacio') == 0) {
                    $(this).removeAttr('style')
                }
            });
        }
    });
});




function ListarAutocomple_Conductor(elememt) {
    var textoexist = "";
    $('#idtr_conductor').empty()


    var text = $('#idcond_atuocomp').val()

    if (text.length >= 3) {
        var textoIngresado = text;
        _.filter(DATAJSONAUTOCOMPLETE_CONDUCTOR, function (obj) {
            var result = _.startsWith(obj.label, textoIngresado);
            if (result == true) {
                textoexist = "Existe"
                $('#idtr_conductor').append('<tr  onclick="seleccionaFila_Conductor($(this))" class="select_results__option"><td>' + obj.label + '</td> </tr>')
            }
        });
    }
    if (textoexist == "") {
        Swal.fire({ title: 'No existe Conductor', type: 'warning', })
        return false;
    }

}


function ListarAutocomple_Unidad(elememt) {
    var textoexist = "";
    $('#idtr_unidad').empty()

    var text = $('#txtunidad_').val()

    if (text.length >= 3) {
        var textoIngresado = text;
        _.filter(DATAJSONAUTOCOMPLETE_UNIDAD, function (obj) {
            var result = _.startsWith(obj.label, textoIngresado);
            if (result == true) {
                textoexist = "Existe"
                $('#idtr_unidad').append('<tr onclick="seleccionaFila_Unidad($(this))" class="select_results__option"><td style="text-align:center">' + obj.label + '</td> </tr>')
            }
        });
    }
    if (textoexist == "") {
        Swal.fire({ title: 'No existe Unidad', type: 'warning', })
        return false;
    }
}



function getObtenerInformacion(elemet) {
 
    $("#txtDispo").prop("checked", true);

    $('#modal_RegistroProgDetall').modal('show')

    $('#txtlado_unidad2').empty()

    var cloned = $('#txtlado').clone();
    cloned.attr('id', 'txtlado_unidad');
    cloned.attr('class', 'form-control');
    cloned.appendTo($("#txtlado_unidad2"));


    $('#idDespachoUnidad_Serv tbody').empty();

    SERVICIO = 0;
    IDSALIDA_PROGDETA = 0;

    $('#txtTitle').empty()

    var ruta = $('#txtRuta :selected').text()
    var lado = $('#txtlado').val()

    SERVICIO = elemet.attr('data-servicio')
    IDSALIDA_PROGDETA = elemet.attr('data-id_msalida_prog_det')

    $('#txtservicio_unidad').val(SERVICIO)
    $('#txtlado_unidad').val(lado)
    var title_ = "Despacho Unidad" + " " + ruta;
    $('#txtTitle').text(title_)

    getListarViajesXServ(1);

}

function getSelectRutaxCorredor(id_ruta_) {
    $('#txtrutaserv').empty();
    $('#txtrutaserv-button').children('span').empty();

    $.ajax({
        url: URL_GET_RUTATIPO_SERVICIO,
        dataType: 'json',
        data: { id_ruta: id_ruta_ },
        success: function (result) {
            if (result.length == 0) { // si la lista esta vacia
                $('#txtrutaserv').append('<option value="0">' + 'No existen datos' + '</option>');
                $('#txtrutaserv-button').children('span').css('font-size', '11px')
                $('#txtrutaserv-button').children('span').css('margin-left', '-15px')
                return false;
            } else {
                $.each(result, function () {
                    $('#txtrutaserv').append('<option value="' + this.ID_RUTA_TIPO_SERVICIO + '">' + this.NOMBRE + '</option>');
                });
            }

            $('#txtrutaserv-button').children('span').css('font-size', '11px')
            $('#txtrutaserv-button').children('span').css('margin-left', '-15px')

        }, error: function (xhr, status, error) {

            Validaciones.ValidarSession();
        },
    }, JSON);
}



function getLado_X_Ruta(id_ruta_) {
    $('#txtlado').empty();
    $('#txtlado-button').children('span').empty();

    $.ajax({
        url: URL_GET_LADO_X_RUTA,
        dataType: 'json',
        data: { id_ruta: id_ruta_ },
        success: function (result) {
            if (result.length == 0) { // si la lista esta vacia
                $('#txtlado').append('<option value="0">' + 'No existen datos' + '</option>').selectmenu('refresh');
                return false;
            } else {
                $.each(result, function () {
                    $('#txtlado').append('<option value="' + this.SENTIDO + '">' + this.LADO + '</option>');
                });
            }
        }, error: function (xhr, status, error) {

            Validaciones.ValidarSession();
        },
    }, JSON);
}



function getConductores() {


    $.ajax({
        url: URL_GET_CONDUCTORES,
        dataType: 'json',
        success: function (result) {

            $('#idcond_atuocomp').empty();
            $.each(result, function () {
                $('#idcond_atuocomp').append('<option value="' + this.CODIGO + '">' + this.APELLIDOS + '-' + this.CODIGO + '</option>')

            });
            $('#idcond_atuocomp').append('<option value="0" selected disabled>Seleccionar el Conductor</option>')
            $('#idcond_atuocomp').parent().children('button').css("width", "auto")
            $($('#idcond_atuocomp').parent().children('div')[0]).css("transform", "inherit")
            $('.selectpicker').selectpicker('refresh');

        }, error: function (xhr, status, error) {

            Validaciones.ValidarSession();
        },
    }, JSON);
}




function getPlaca_Buses(id_ruta_) {

    $.ajax({
        url: URL_GET_PLACAS,
        data: {
            id_ruta: id_ruta_
        },
        dataType: 'json',
        success: function (result) {
            $('#txtunidad_').empty();
            $.each(result, function () {
                $('#txtunidad_').append('<option value="' + this.BS_PLACA + '">' + this.BS_PLACA + '</option>')

            });
            $('#txtunidad_').append('<option value="0" selected disabled>Seleccionar la unidad</option>')

            $('#txtunidad_').parent().children('button').css("width", "auto")
            $('.selectpicker').selectpicker('refresh');
        }, error: function (xhr, status, error) {

            Validaciones.ValidarSession();
        },
    }, JSON);
}



function getListarProgramacionUnidades() {


    $('#idsearch').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>...');
    var sentido_ = $('#txtlado').val();
    var id_ruta_ = $('#txtRuta').val();


    if (sentido_ == null || sentido_ == 0) {
        Swal.fire({ title: 'Seleccionar un Sentido', type: 'warning', })
        $('#idsearch').prop('disabled', false).html('<svg class="svg-inline--fa fa-search fa-w-16" aria-hidden="true" focusable="false" data-prefix="fa" data-icon="search" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" data-fa-i2svg=""><path fill="currentColor" d="M505 442.7L405.3 343c-4.5-4.5-10.6-7-17-7H372c27.6-35.3 44-79.7 44-128C416 93.1 322.9 0 208 0S0 93.1 0 208s93.1 208 208 208c48.3 0 92.7-16.4 128-44v16.3c0 6.4 2.5 12.5 7 17l99.7 99.7c9.4 9.4 24.6 9.4 33.9 0l28.3-28.3c9.4-9.4 9.4-24.6.1-34zM208 336c-70.7 0-128-57.2-128-128 0-70.7 57.2-128 128-128 70.7 0 128 57.2 128 128 0 70.7-57.2 128-128 128z"></path></svg>');
        return false;
    }

    if (id_ruta_ == null || id_ruta_ == 0) {
        Swal.fire({ title: 'Seleccionar una Rutao', type: 'warning', })
        $('#idsearch').prop('disabled', false).html('<svg class="svg-inline--fa fa-search fa-w-16" aria-hidden="true" focusable="false" data-prefix="fa" data-icon="search" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" data-fa-i2svg=""><path fill="currentColor" d="M505 442.7L405.3 343c-4.5-4.5-10.6-7-17-7H372c27.6-35.3 44-79.7 44-128C416 93.1 322.9 0 208 0S0 93.1 0 208s93.1 208 208 208c48.3 0 92.7-16.4 128-44v16.3c0 6.4 2.5 12.5 7 17l99.7 99.7c9.4 9.4 24.6 9.4 33.9 0l28.3-28.3c9.4-9.4 9.4-24.6.1-34zM208 336c-70.7 0-128-57.2-128-128 0-70.7 57.2-128 128-128 70.7 0 128 57.2 128 128 0 70.7-57.2 128-128 128z"></path></svg>');
        return false;
    }

    var i = 0;
    var strHTML = "";
    $('#tbProDet tbody').empty();
    $.ajax({

        url: URL_GET_GETVIAJES_PROGRUNIDADES,
        dataType: 'json',
        data: {
            id_ruta: id_ruta_,
            sentido: sentido_
        },
        success: function (result) {

            $('#idsearch').prop('disabled', false).html('<svg class="svg-inline--fa fa-search fa-w-16" aria-hidden="true" focusable="false" data-prefix="fa" data-icon="search" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" data-fa-i2svg=""><path fill="currentColor" d="M505 442.7L405.3 343c-4.5-4.5-10.6-7-17-7H372c27.6-35.3 44-79.7 44-128C416 93.1 322.9 0 208 0S0 93.1 0 208s93.1 208 208 208c48.3 0 92.7-16.4 128-44v16.3c0 6.4 2.5 12.5 7 17l99.7 99.7c9.4 9.4 24.6 9.4 33.9 0l28.3-28.3c9.4-9.4 9.4-24.6.1-34zM208 336c-70.7 0-128-57.2-128-128 0-70.7 57.2-128 128-128 70.7 0 128 57.2 128 128 0 70.7-57.2 128-128 128z"></path></svg>');
            if (result.length == 0) {
                strHTML += '<tr><td colspan="6" class="text-center">No hay información para mostrar</td></tr>';
            } else {
                $.each(result, function () {
                    i++;

                    var td_cac = this.CAC_TIEMPOREAL

                    if (td_cac == 0) {
                        td_cac = "<span style='color:blue'>DISP</span>"
                    } else if (td_cac == 1 || td_cac == 2) {
                        td_cac = "<span>-</span>"
                    } else {
                        td_cac = this.CAC_TIEMPOREAL
                    }


                    var salida_ejecutada = ""
                    if (this.PLACA_TIEMPOREAL == null || this.PLACA_TIEMPOREAL == " ") {
                        salida_ejecutada = "<span style='color:blue'>DISP</span>"
                    } else {
                        salida_ejecutada = this.PLACA_TIEMPOREAL
                    }

                    strHTML += '<tr data-servicio="' + this.SERVICIO + '" data-id_msalida_prog_det="' + this.ID_MSALIDA_PROG_DET + '" class="modalhref" style="cursor:pointer" onclick="getObtenerInformacion($(this))">' +
                       '<td style="text-align: center;"> ' + i + '</td>' +
                       '<td style="text-align: center;">' + this.SERVICIO + '</td>' +
                       '<td style="text-align: center;">' + this.HSALIDA + '</td>' +
                       '<td style="text-align: center;">' + salida_ejecutada + '</td>' +
                       '<td style="text-align: center;">' + td_cac + '</td>' +
                       '</tr>';
                });
            }
            $('#tbProDet tbody').append(strHTML);

            $('#tbProDet tr').click(function () {
                $('#modal_RegistroProgDetall').show()
            });

            setTimeout(function () {
                unblock();
            }, 500);

        },
        error: function (xhr, status, error) {
            strHTML += '<tr><td colspan="6" class="text-center">No hay información para mostrar</td></tr>';
            Validaciones.ValidarSession();
        },
    }, JSON);
}


function getListarViajesXServ(action) {



    var idservicio_ = "";
    var i = 0;
    var strHTML = "";

    $('#idDespachoUnidad_Serv tbody').empty();
    var id_ruta_ = $('#txtRuta').val();
    var sentido_ = $('#txtlado_unidad').val();
    var idsalida_prog_deta_ = IDSALIDA_PROGDETA

    //ACCION DE LISTAR POR LA TABLA O POR EL BUTTON
    if (action == 1) { idservicio_ = SERVICIO; } else if (action == 2) { idservicio_ = $('#txtservicio_unidad').val(); }
    $('#btnBusquedaEnTabla').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>...');

    $.ajax({
        url: URL_GET_GETVIAJES_PROGRX_SERVICIO,
        dataType: 'json',
        data: {
            id_ruta: id_ruta_,
            sentido: sentido_,
            idservicio: idservicio_
        },
        success: function (result) {

            $('#btnBusquedaEnTabla').prop('disabled', false).html('<svg class="svg-inline--fa fa-search fa-w-16" aria-hidden="true" focusable="false" data-prefix="fa" data-icon="search" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" data-fa-i2svg=""><path fill="currentColor" d="M505 442.7L405.3 343c-4.5-4.5-10.6-7-17-7H372c27.6-35.3 44-79.7 44-128C416 93.1 322.9 0 208 0S0 93.1 0 208s93.1 208 208 208c48.3 0 92.7-16.4 128-44v16.3c0 6.4 2.5 12.5 7 17l99.7 99.7c9.4 9.4 24.6 9.4 33.9 0l28.3-28.3c9.4-9.4 9.4-24.6.1-34zM208 336c-70.7 0-128-57.2-128-128 0-70.7 57.2-128 128-128 70.7 0 128 57.2 128 128 0 70.7-57.2 128-128 128z"></path></svg>');

            if (result.length == 0) {
                strHTML += '<tr><td colspan="6" class="text-center">No hay información para mostrar</td></tr>';
            } else {

                $.each(result, function () {
                    var styleauto = "";
                    var vacios2 = 0;
                    var display = "";
                    var style = "";
                    var onclick = "";

                    if (this.CAC_TIEMPOREAL == 0) {
                        vacios2 = 1;
                        display = "display:'' "
                        style = "cursor: pointer;"
                        onclick = 'onclick="ButtonActualizarViajeTemporal($(this))';
                    } else {
                        display = "display: none "
                        //style = "cursor:not-allowed;"
                        //onclick = 'onclick="event.preventDefault();';
                        style = "cursor: pointer;"
                        onclick = 'onclick="ButtonActualizarViajeTemporal($(this))';

                    }

                    //vacios2 = 1;
                    //display = "display:'' "
                    //style = "cursor: pointer;"
                    //onclick = 'onclick="ButtonActualizarViajeTemporal($(this))';
                    var td_cac = this.CAC_TIEMPOREAL

                    if (td_cac == 0) {
                        td_cac = "<span style='color:blue'>DISP</span>"
                    } else if (td_cac == 1 || td_cac == 2) {
                        td_cac = "<span>-</span>"
                    } else {
                        td_cac = this.CAC_TIEMPOREAL
                    }
                    var placa = ""
                    if (this.PLACA_TIEMPOREAL == null || this.PLACA_TIEMPOREAL == " ") {
                        placa = "<span style='color:blue'>DISP</span>"
                    } else {
                        placa = this.PLACA_TIEMPOREAL
                    }


                    var salida_ejecutada = ""
                    if (this.HSALIDA_REAL == null || this.HSALIDA_REAL == " ") {
                        salida_ejecutada = "<span style='color:blue'>DISP</span>"
                    } else {
                        salida_ejecutada = this.HSALIDA_REAL
                    }



                    var id_perfil = $('#id_perfil').val()

                    var action_button = 0;
                    if (id_perfil == 21 || id_perfil == 41) {
                        action_button = 1;
                    }

                    (this.ID_MSALIDA_PROG_DET == idsalida_prog_deta_ ? styleauto = "background-color:#17a2b838;" : styleauto = "")

                    if (result.length == 0) {
                        strHTML += '<tr><td colspan="5" class="text-center">No hay información para mostrar</td></tr>';
                    } else {
                        i++;
                        strHTML += '<tr  data-OBSERVACIONES="' + this.OBSERVACIONES + '" data-vacio="' + vacios2 + '" style="' + styleauto + display + '" ' +
                        'data-id_msalida_prog_det="' + this.ID_MSALIDA_PROG_DET + '" data-HSALIDA="' + this.HSALIDA + '" data-PLACA="' + this.PLACA_TIEMPOREAL + '"   data-HSALIDAP="' + this.HSALIDA_REAL + '" data-CONDUCTOR="' + this.CAC_TIEMPOREAL + '">' +
                            '<td> ' + i + '</td>' +
                            '<td>' + this.HSALIDA + '</td>' +
                            '<td>' + salida_ejecutada + '</td>' +
                            '<td>' + placa + '</td>' +
                            '<td>' + td_cac + '</td>' +
                            '<td><a style="' + style + ' "' + onclick + '"> <svg  class="svg-inline--fa fa-plus fa-w-14" aria-hidden="true" focusable="false" data-prefix="fa" data-icon="plus" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" data-fa-i2svg=""><path fill="currentColor" d="M416 208H272V64c0-17.67-14.33-32-32-32h-32c-17.67 0-32 14.33-32 32v144H32c-17.67 0-32 14.33-32 32v32c0 17.67 14.33 32 32 32h144v144c0 17.67 14.33 32 32 32h32c17.67 0 32-14.33 32-32V304h144c17.67 0 32-14.33 32-32v-32c0-17.67-14.33-32-32-32z"></path></svg></a></td>' +
                            '<td style="text-align: center;cursor:pointer" onclick="ActualizarObserv($(this))"><i class="fas fa-car-crash" ></i></td>' +
                            (action_button == 1 ? '<td style="text-align: center;cursor:pointer" onclick="EliminarViajes($(this))"><i class="fas fa-eraser"></i></td>' : "") +
                           '</tr>';
                    }
                });
            }
            $('#idDespachoUnidad_Serv tbody').append(strHTML);
            setTimeout(function () {
                unblock();
            }, 500);

        }, error: function (xhr, status, error) {

            Validaciones.ValidarSession();
        },

    }, JSON);
}


var HPROGRAMADA = "";
var PLACA_GLOBAL = "";
var CAC = "";
var OBSERVACIONES = "";
function ButtonActualizarViajeTemporal(element) {



    $("#conduc_cac").removeAttr('checked');



    $('#txtunidad_').val(0)
    $('#idcond_atuocomp').val(0)
    $('#id_observacion').val('')


    $('#modal_DetallSalida').modal('show');
    IDSALIDA_ESPECIFICO = 0;
    IDSALIDA_ESPECIFICO = element.parent().parent().attr('data-id_msalida_prog_det')
    HORA_SALIDA_FINAL = element.parent().parent().attr('data-HSALIDA')

    HPROGRAMADA = element.parent().parent().attr('data-HSALIDAP')
    PLACA_GLOBAL = element.parent().parent().attr('data-PLACA')
    CAC = element.parent().parent().attr('data-CONDUCTOR')
    OBSERVACIONES = element.parent().parent().attr('data-OBSERVACIONES')


    $('#conductor_div').css('display', 'block')
    $('#conductor_div2').css('display', 'block')




    $('#idcond_atuocomp').val();

    if (PLACA_GLOBAL != "null") {
        $('#txtunidad_').val(PLACA_GLOBAL);
    }
    if (HPROGRAMADA != "null") {
        $('#txtHorasalida_').val(HPROGRAMADA);
    }

    if (OBSERVACIONES != "null") {
        $('#id_observacion').val(OBSERVACIONES);
    }

    if (CAC == "2") {
         document.getElementById("conduc_cac").checked = true;
         $('#conductor_div').css('display', 'none')
         $('#conductor_div2').css('display', 'none')

    } else if (CAC != "0") {
        $('#idcond_atuocomp').val(CAC);
    }

    $('.selectpicker').selectpicker('refresh');


}


function Actualizar_ViajeTemporal() {

    //check sin cac
    var idcac = $('#idcond_atuocomp').val();

    if ($('#conduc_cac').is(":checked")) {
        idcac = 2
    } else if (conduc_cac != null) {
        idcac = $('#idcond_atuocomp').val();

    }

    var id_unidad_ = $('#txtunidad_').val();
    var id_hora_salida = $('#txtHorasalida_').val();
    var observacion = $('#id_observacion').val()

    if (idcac == null) {
        Swal.fire({ title: 'Seleccionar el conductor', type: 'warning', })
        return false
    }
    if (id_unidad_ == null) {
        Swal.fire({ title: 'Seleccionar la unidad', type: 'warning', })
        return false
    }

    if (id_hora_salida == " ") {
        Swal.fire({ title: 'Seleccionar la hora', type: 'warning', })
        return false
    }


    //var idcac = idcac.split('-');
    //idcac = idcac[1];
    var id_salida_ = IDSALIDA_ESPECIFICO;


    var hora_ADELANTE = ""
    var hora_ATRAS = ""

    var media_hora = ""
    var media_atras = ""

    var hora_actual = id_hora_salida.substr(0, 8)
    var media_actual = id_hora_salida.substr(3, 2)



    var hora_adelante = new Date("May 1,2019 " + HORA_SALIDA_FINAL)
    hora_adelante.setHours(hora_adelante.getHours() + 1);
    hora_adelante = hora_adelante.toTimeString()
    hora_ADELANTE = hora_adelante.substr(0, 8)
    //media_hora = hora_adelante.substr(3, 2)

    var hora_atras = new Date("May 1,2019 " + HORA_SALIDA_FINAL);
    hora_atras.setHours(hora_atras.getHours() - 1);
    hora_atras = hora_atras.toTimeString()
    hora_ATRAS = hora_atras.substr(0, 8)
    //media_atras = hora_atras.substr(3, 2)
    var FECHA_TEMP = '27/02/2020';



    var now = util.convertDatetoTimeStamp(FECHA_TEMP + ' ' + hora_actual + ':00');
    var horaatras = util.convertDatetoTimeStamp(FECHA_TEMP + ' ' + hora_ATRAS);
    var horaadelante = util.convertDatetoTimeStamp(FECHA_TEMP + ' ' + hora_ADELANTE);


    // if (now <= horaadelante && now >= horaatras) { //una hora mas 

    if (id_hora_salida.length > 5) {
        id_hora_salida = id_hora_salida.substr(0, 8)

    } else {
        id_hora_salida = id_hora_salida + ':00'
    }

    $.ajax({
        url: URL_ACTUALIRZAR_VIAJES_PROGR,
        dataType: 'json',
        data: {
            idsalida: id_salida_,
            unidad: id_unidad_,
            cac_temporal: idcac,
            hora_salida: id_hora_salida,
            observacion: observacion
        },
        success: function (result) {

            if (result.COD_ESTADO == 1) {

                Swal.fire({
                    type: result.COD_ESTADO == 1 ? 'success' : 'error',
                    title: result.DES_ESTADO,
                    showConfirmButton: false,
                });

                getListarProgramacionUnidades();

                getSelectRutaxCorredor($("#txtRuta").val())


                $('#modal_DetallSalida').modal('hide')
                $('#modal_RegistroProgDetall').modal('hide')

            }
        }, error: function (xhr, status, error) {

            Validaciones.ValidarSession();
        },
    }, JSON);
    //}
    //  else {
    //Swal.fire({ title: 'La hora registrada no es correcta ', type: 'warning', })
    //return false
    // }
    //}
}


function ActualizarObserv(element) {
    HORA_SALIDA_FINAL = element.parent().attr('data-HSALIDA')
    IDSALIDA_ESPECIFICO = element.parent().attr('data-id_msalida_prog_det')
    $('#idhora_programada').val(HORA_SALIDA_FINAL)
    $('#modal_ActualizarIncidencias').modal('show')
}

function ActualizarObserv_BUTTON() {

    var id_maestro = IDSALIDA_ESPECIFICO;
    var incidencia = $('#id_incidencia option:selected').val()
    var horaejecutada = "-";
    var observacion = $('#id_observacion2').val()

    if (observacion == "") {
        observacion = incidencia
    } else {
        observacion = incidencia + ' - ' + observacion
    }

    $.ajax({
        url: URL_ACTUALIRZAR_OBSERV,
        dataType: 'json',
        data: {
            id_maestro: id_maestro,
            observacion: observacion,
            horaejecutada: horaejecutada
        },
        success: function (result) {

            if (result.COD_ESTADO == 1) {

                Swal.fire({
                    type: result.COD_ESTADO == 1 ? 'success' : 'error',
                    title: result.DES_ESTADO,
                    showConfirmButton: false,
                });

                getListarProgramacionUnidades();
                getSelectRutaxCorredor($("#txtRuta").val())

                getPlaca_Buses($('#txtRuta').val());
                getConductores();

                $('#modal_DetallSalida').modal('hide')
                $('#modal_RegistroProgDetall').modal('hide')
                $('#modal_ActualizarIncidencias').modal('hide')
            }
        }, error: function (xhr, status, error) {

            Validaciones.ValidarSession();
        },
    }, JSON);

}





function EliminarViajes(element) {

    IDSALIDA_ESPECIFICO = element.parent().attr('data-id_msalida_prog_det')

    $.ajax({
        url: URL_LIMPIAR_VIAJE,
        dataType: 'json',
        data: {
            id_maestro: IDSALIDA_ESPECIFICO,
        },
        success: function (result) {

            if (result.COD_ESTADO == 1) {

                Swal.fire({
                    type: result.COD_ESTADO == 1 ? 'success' : 'error',
                    title: result.DES_ESTADO,
                    showConfirmButton: false,
                });

                getListarProgramacionUnidades();

                getPlaca_Buses($('#txtRuta').val());
                getConductores();

                getSelectRutaxCorredor($("#txtRuta").val())

                $('#modal_DetallSalida').modal('hide')
                $('#modal_RegistroProgDetall').modal('hide')
                $('#modal_ActualizarIncidencias').modal('hide')
            }
        }, error: function (xhr, status, error) {

            Validaciones.ValidarSession();
        },
    }, JSON);
}





//BLOQUEO
function block() {
    var body = $('#panel-body');
    var w = body.css("width");
    var h = body.css("height");
    var trb = $('#throbber');
    var position = body.offset(); // top and left coord, related to document

    trb.css({
        width: w,
        height: h,
        opacity: 0.7,
        position: 'absolute',

    });
    trb.show();
}
function unblock() {
    var trb = $('#throbber');
    trb.hide();
}


function seleccionaFila_Conductor(elementFila) {


    $('#idcond_atuocomp').val(elementFila.children()[0].innerText)

    $.each($('#tbConductor tbody > tr'), function () {
        $(this).css('background-color', '');
    });
    elementFila.css('background-color', '#17a2b838');
    $('#idtr_conductor').empty()
}


function seleccionaFila_Unidad(seleccionaFila_Unidad) {


    $('#txtunidad_').val(seleccionaFila_Unidad.children()[0].innerText)

    $.each($('#tbUnidad tbody > tr'), function () {
        $(this).css('background-color', '');
    });
    seleccionaFila_Unidad.css('background-color', '#17a2b838');
    $('#idtr_unidad').empty()
}



function mayus(e) {
    e.value = e.value.toUpperCase();
}