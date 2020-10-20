var hora_cargaAp = '';

var util = new Util();
var Validaciones = new validaciones();

$(document).ready(function () {
    //$('#selectModalidadTransporte').val(Number(modalidadUsuario))
    getCorredoresByModalidad();
    getVias();

    hora_cargaAp = HoraActual();
    //$('#selectCorredores').on('change', function () {
    //    //console.log($("#selectInfraccion option:selected")[0], 'onchange');
    //    idCorredor = $('#selectCorredores').val();
    //    getListPlacabyCorredor();
    //});
    $("#sectionParametrosPasajerosCampo").on("submit", function (e) {

        e.preventDefault();

        $('#txt_horafin').val(HoraActual());

        if ($('#selectParadero').val() == 0) {
            Swal.fire({
                type: 'info',
                title: "Debe seleccionar un paradero válido",
                showConfirmButton: false,
            });
            return false;
        }

        if ($('#txt_horainicio').val() == '') {
            $('#txt_horainicio').val(hora_cargaAp);

        }

        if ($('#txt_suben').val() == '' || $('#txt_bajan').val() == '' || $('#txt_cola').val() == '' || $('#txt_capacidad_llegada').val() == '' || $('#txt_capacidad_salida').val() == '') {
            Swal.fire({
                type: 'info',
                title: "Debe ingresar los pasajeros que suben, bajan y en cola",
                showConfirmButton: false,
            });
            return false;
        }

        var ModelPasajerosCampo = $('#sectionParametrosPasajerosCampo').serializeObject();
        $('#reg_pasaj_campo_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');
        //block();

        $.ajax({
            url: URL_REG_PASAJEROS_CAMPO,
            data: ModelPasajerosCampo,
            dataType: 'json',
            success: function (result) {
                Swal.fire({
                    type: result.COD_ESTADO == 0 ? 'error' : 'success',
                    title: result.DES_ESTADO,
                    showConfirmButton: false,
                    //timer: 2000
                })
                $('#reg_pasaj_campo_btn').prop('disabled', false).html('Guardar');


                $('#txt_horainicio').val('');
                $('#txt_horafin').val('');
                $('#txt_suben').val(0);
                $('#txt_bajan').val(0);
                $('#txt_cola').val(0);
                $('#txt_capacidad_llegada').val('');
                $('#txt_capacidad_salida').val('');
                $('#txt_num_serv').val('');
                $('#txt_as_libres').val('');
                $('#txtarea_observaciones').val('');
                $("input[type='radio']:first").attr("checked", "checked");
                //$("input[type='radio']").checkboxradio("refresh");

            },
            error: function (xhr, status, error) {

                Validaciones.ValidarSession();
             },

        }, JSON);

    })

    $('.btn dropdown-toggle btn-light').css("width", "73%");

});

//FUNCION QUE SE USARÁ PARA ENVIAR LOS INPUT EN UN MODEL
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};


function getRutaPorCorredor() {

    $.blockUI({
        message: null,
    });

    $.ajax({
        url: URL_GET_RUTA_X_CORREDOR,
        dataType: 'json',
        data: { idCorredor: $('#selectCorredores').val() },
        success: function (result) {
            $('#selectRuta').empty();
            if (result.length == 0) { // si la lista esta vacia
                $('#selectRuta').append('<option value="0">' + '--No hay información--' + '</option>');

            } else {
                $.each(result, function () {
                    $('#selectRuta').append('<option value="' + this.ID_RUTA + '">' + this.NRO_RUTA + '</option>')
                });
            }
            getRecorridoXRuta();
            getListPlacabyCorredor();
            //$('.selectRuta').selectpicker('refresh');
            $.unblockUI();
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
                    $('#selectLado').append('<option value="' + this.ID_RECORRIDO + '">' + this.LADO + '</option>')
                    strCodRecorrido += this.ID_RECORRIDO + '|';
                });

            }
            getListaParadero($('#selectLado').val());
            //$('.selectLado').selectpicker('refresh');

        }
    }, JSON);
}

function getListaParadero() {
    $.blockUI({
        message: null,

    });


    $.ajax({
        url: URL_GET_PARADEROS,
        dataType: 'json',
        data: { strCodRecorrido: $('#selectLado').val() },
        success: function (result) {


            $('#selectParadero').empty();
            $('#selectParaderoOrigen').empty();
            $('#selectParaderoDestino').empty();
            var ladoPorIdRecorrido = verificarLadoxIdRecorrido(this.ID_RECORRIDO);
            if (result.length == 0) { // si la lista esta vacia
                $('#selectParadero').append('<option value="0">' + '--No hay información--' + '</option>')
                $('#selectParaderoOrigen').append('<option value="0">' + '--No hay información--' + '</option>')
                $('#selectParaderoDestino').append('<option value="0">' + '--No hay información--' + '</option>')
                // si la lista esta vacia, achicar el mensaje
                $('#selectParadero-button').children('span').css('font-size', '13px')
                $('#selectParaderoOrigen-button').children('span').css('font-size', '13px')
                $('#selectParaderoDestino-button').children('span').css('font-size', '13px')
            } else {
                $.each(result, function () {
                    $('#selectParadero').append('<option value="' + this.ID_PARADERO + '">' + this.NOMBRE + '</option>')
                    $('#selectParaderoOrigen').append('<option value="' + this.ID_PARADERO + '">' + this.NOMBRE + '</option>')
                    $('#selectParaderoDestino').append('<option value="' + this.ID_PARADERO + '">' + this.NOMBRE + '</option>')
                });
            }

            $.unblockUI();

        }
    }, JSON);
}

function getVias() {
    $.ajax({
        url: URL_GET_VIAS,
        dataType: 'json',
        data: {},
        success: function (result) {
            $('#selectVia').empty();
            $.each(result, function () {
                $('#selectVia').append('<option ' + (this.ID_VIA == 12 ? ' selected ' : '') + ' value="' + this.ID_VIA + '">' + (!this.NOMBRE ? '-- SELECCIONAR VIA --' : this.NOMBRE) + '</option>')
            });
            //$('.selectVia').selectpicker('refresh');
        }
    }, JSON);
}

function getCorredoresByModalidad() {
    //var modalidadTransporteSelecionado = $('#selectModalidadTransporte').val();
    var modalidadTransporteSelecionado = $('#mod').val();
    $('#selectCorredores').empty();

    //$('#selectCorredores, #selectRuta').empty();
    var cantidadRegistros = 0;
    $.each(JSON_DATA_CORREDORES, function () {
        //if (this.ID_MODALIDAD_TRANS == Number(modalidadTransporteSelecionado)) {
        if (this.ID_MODALIDAD_TRANS == Number(modalidadTransporteSelecionado)) {
            cantidadRegistros++;
            $('#selectCorredores').append('<option value="' + this.ID_CORREDOR + '">' + this.ABREVIATURA + '</option>')
        }
    });
    if (cantidadRegistros == 0) {
        $('#selectCorredores').append('<option value="0">' + '-- No hay información --' + '</option>')
        return false
    }
    getRutaPorCorredor();

}

function getListPlacabyCorredor() {

    idCorredor = $('#selectCorredores').val();

    $.ajax({
        url: URL_LIST_BUSES_BY_ID,
        data: {
            idCorredor: idCorredor
        },
        dataType: 'json',
        success: function (result) {
            $('#filter-menu').empty();
            $.each(result, function () {
                $('#filter-menu').append('<option value="' + this.BS_PLACA + '">' + this.BS_PLACA + '</option>')

            });
            $('.selectpicker').selectpicker('refresh');
        }
    }, JSON);
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

function checkTime(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

function HoraActual() {
    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();

    h = checkTime(h);
    m = checkTime(m);
    s = checkTime(s);
    var time = h + ":" + m + ":" + s;
    return time
}

//function registrar_pasajeroscampo() {

//    var id_corredor = $('#selectCorredores').val();
//    var id_ruta = $('#selectRuta').val();
//    var id_paradero = $('#selectParadero').val();
//    var id_bus = $('#filter-menu').val();
//    var hora_inicio = $('#txt_horainicio').val();
//    var hora_fin = $('#txt_horafin').val(HoraActual());
//    var suben = $('#txt_suben').val();
//    var bajan = $('#txt_bajan').val();
//    var cola = $('#txt_cola').val();
//    //var colectivo = $('#txt_colectivo').val();
//    var observaciones = $('#txtarea_observaciones').val();

//    var tipo_observacion = $(".Tipo_Observ:checked").val();

//    if (id_paradero == 0) {
//        Swal.fire({
//            type: 'info',
//            title: "Debe seleccionar un paradero válido",
//            showConfirmButton: false,
//        });
//        return false;
//    }
//    if (hora_inicio == '') {
//        hora_inicio = hora_cargaAp;
//    }

//    if (suben == '' || bajan == '' || cola == '') {
//        Swal.fire({
//            type: 'info',
//            title: "Debe ingresar los pasajeros que suben, bajan y en cola",
//            showConfirmButton: false,
//        });
//        return false;
//    }

//    block();
//    $.ajax({
//        url: URL_REG_PASAJEROS_CAMPO,
//        data: {
//            id_corredor: id_corredor,
//            id_ruta: id_ruta,
//            id_paradero: id_paradero,
//            id_bus: id_bus,
//            hora_inicio: hora_inicio,
//            hora_fin: hora_fin,
//            suben: suben,
//            bajan: bajan,
//            observaciones: observaciones,
//            cola: cola,
//            tipo_observacion: tipo_observacion

//        },
//        dataType: 'json',
//        success: function (result) {

//            //var elementoSwal = $("div").find("[aria-describedby='" + "swal2-content" + "']");

//            Swal.fire({
//                type: result.COD_ESTADO == 1 ? 'success' : 'error',
//                title: result.DES_ESTADO,
//                showConfirmButton: false
//            });
//            //var elementoSwal = $(".swal2-container div");
//            // console.log(elementoSwal[0]);

//            //console.log(elementoSwal[0])
//            //$('.swal2-header').parent().css({
//            //    'display': 'flex !important',
//            //    'width': '62% !important',
//            //    'margin-left': '0px !important',
//            //    'margin-right': '28% !important'
//            //})

//            setTimeout(function () {
//                unblock();
//            }, 500);

//            $('#txt_horainicio').val('');
//            $('#txt_horafin').val('');
//            $('#txt_suben').val('');
//            $('#txt_bajan').val('');
//            $('#txt_cola').val('');
//            $('#txtarea_observaciones').val('');


//        }
//    }, JSON);
//}

$("body").on("click", "#txt_suben", function () {
    $('#txt_horainicio').val(HoraActual());
});

function registrar_pasajeros_orig_dest() {

    var id_corredor = $('#selectCorredores').val();
    var id_ruta = $('#selectRuta').val();
    var id_paradero_orig = $('#selectParaderoOrigen').val();
    var id_paradero_dest = $('#selectParaderoDestino').val();
    var id_tarjeta = $('#selectTarjeta').val();

    if (id_paradero_orig == 0 || id_paradero_dest == 0) {
        Swal.fire({
            type: 'info',
            title: "Debe seleccionar un paradero válido",
            showConfirmButton: false,
        });
        return false;
    }
    $('#reg_pasaj_ori_des_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');

    //block();
    $.ajax({
        url: URL_REG_PASAJEROS_ORIG_DEST,
        data: {
            id_corredor: id_corredor,
            id_ruta: id_ruta,
            id_paradero_orig: id_paradero_orig,
            id_paradero_dest: id_paradero_dest,
            id_tarjeta: id_tarjeta
        },
        dataType: 'json',
        success: function (result) {

            //var elementoSwal = $("div").find("[aria-describedby='" + "swal2-content" + "']");

            Swal.fire({
                type: result.COD_ESTADO == 1 ? 'success' : 'error',
                title: result.DES_ESTADO,
                showConfirmButton: false
            });
            //var elementoSwal = $(".swal2-container div");
            // console.log(elementoSwal[0]);

            //console.log(elementoSwal[0])
            //$('.swal2-header').parent().css({
            //    'display': 'flex !important',
            //    'width': '62% !important',
            //    'margin-left': '0px !important',
            //    'margin-right': '28% !important'
            //})

            $('#reg_pasaj_ori_des_btn').prop('disabled', false).html('Guardar');
        },
        error: function (xhr, status, error) {
            Validaciones.ValidarSession();
        },
    }, JSON);
}

function registrar_pasajeros_colectivo() {

    var id_corredor = $('#selectCorredores').val();
    var id_ruta = $('#selectRuta').val();
    var id_paradero = $('#selectParadero').val();
    var tipo_vehiculo = $('#selectTipovehiculo').val();
    var suben = $('#txt_suben').val();
    var bajan = $('#txt_bajan').val();
    var placa = $('#txt_placa').val();

    if (id_paradero == 0) {
        Swal.fire({
            type: 'info',
            title: "Debe seleccionar un paradero válido",
            showConfirmButton: false,
        });
        return false;
    }

    if (suben == '' || bajan == '') {
        Swal.fire({
            type: 'info',
            title: "Debe ingresar los pasajeros que suben y bajan",
            showConfirmButton: false,
        });
        return false;
    }

    $('#reg_pasaj_colec_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');
    //block();
    $.ajax({
        url: URL_REG_PASAJEROS_COLECTIVO,
        data: {
            id_corredor: id_corredor,
            id_ruta: id_ruta,
            id_paradero: id_paradero,
            tipo_vehiculo: tipo_vehiculo,
            suben: suben,
            bajan: bajan,
            placa: placa

        },
        dataType: 'json',
        success: function (result) {

            //var elementoSwal = $("div").find("[aria-describedby='" + "swal2-content" + "']");

            Swal.fire({
                type: result.COD_ESTADO == 1 ? 'success' : 'error',
                title: result.DES_ESTADO,
                showConfirmButton: false
            });
            //var elementoSwal = $(".swal2-container div");
            // console.log(elementoSwal[0]);

            //console.log(elementoSwal[0])
            //$('.swal2-header').parent().css({
            //    'display': 'flex !important',
            //    'width': '62% !important',
            //    'margin-left': '0px !important',
            //    'margin-right': '28% !important'
            //})

            $('#reg_pasaj_colec_btn').prop('disabled', false).html('Guardar');


            $('#txt_suben').val('');
            $('#txt_bajan').val('');
            $('#txt_placa').val('');

        },
        error: function (xhr, status, error) {
            console.log(xhr + ' | ' + status + ' | ' + error)
            Validaciones.ValidarSession();
        },
    }, JSON);
}


