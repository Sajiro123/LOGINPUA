$(document).ready(function () {

    getPersonaIncidencia();

    $('#selectInfraccion').on('change', function () {
        //console.log($("#selectInfraccion option:selected")[0], 'onchange');
        var tipo_infraccion = $("#selectInfraccion option:selected").attr('data-TIPO_INFRACCION');
        var tipo_sancion = $("#selectInfraccion option:selected").attr('data-SANCION');
        $('#txt_tipo_sancion').val(tipo_sancion)
        $('#txt_tipo_infraccion').val(tipo_infraccion)
        $('#txt_editar_tipo_infraccion').val(tipo_infraccion)


        if (tipo_sancion == "Amonestación" || tipo_sancion == "Restricción") {
            $('#txt_num_dias').css({ "display": "block" });
            $('#txt_tipo_date').css({ "display": "block" });
        } else if (tipo_sancion == "Ninguno") {
            $('#txt_num_dias').css({ "display": "none" });
            $('#txt_tipo_date').css({ "display": "none" });
        }
    });

    $('#selectAgregarTipoPersonaIncidencia').on('change', function () {
        var person = $("#selectAgregarTipoPersonaIncidencia option:selected").text();

    });

    $('#selectAgregarPersonaIncidencia').on('change', function () {
        var person = $("#selectAgregarPersonaIncidencia option:selected").text();
        var estado_situacion = $("#selectAgregarPersonaIncidencia option:selected").attr('data-Estado_Situacion');
        $('#txt_EstadoPersonaIncidencia').val(estado_situacion)
    });

    $('#selectRutaCorredor').on('change', function () {
        getListIncidencias($("#selectRutaCorredor option:selected").val());

    });

    $('#selectPlacaPadron').on('change', function () {
        $('#txt_contrato').val('')

        var paquete_servicio = $("#selectPlacaPadron option:selected").attr('data-PAQUETE_SERVICIO');
        $('#txt_paquete').val(paquete_servicio)

        //DURITO
        switch (paquete_servicio) {
            case '2.2': $('#txt_contrato').val('AUTORIZACIÓN');
                break;
            case '2.3': $('#txt_contrato').val('AUTORIZACIÓN');
                break;
            case '2.6': $('#txt_contrato').val('AUTORIZACIÓN');
                break;
            case '3.1': $('#txt_contrato').val('AUTORIZACIÓN');
                break;
            case '3.3': $('#txt_contrato').val('AUTORIZACIÓN');
                break;
            default:
                $('#txt_contrato').val('CONSECIÓN');
                break;
        }

    });




   $("#sectionParametrosAgregarIncidencia").on("submit", function (e) {
         e.preventDefault();
         $('#txtfechaincidenciacomp').val($('#txt_fecha_incidencia').val() + ' ' + $('#txt_hora_inci').val());
         $('#txtfechafinincidenciacomp').val($('#txt_fecha_fin').val() + ' ' + $('#txt_hora_fina').val());
 
 
         var ModelIncidenciaAgregar = $('#sectionParametrosAgregarIncidencia').serializeObject();
         $('#registrar_incid_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');
         $.ajax({
             url: URL_REGISTRAR_INCIDENCIA,
             data: ModelIncidenciaAgregar,
             dataType: 'json',
             success: function (result) {
                 Swal.fire({
                     type: result.COD_ESTADO == 0 ? 'error' : 'success',
                     title: result.DES_ESTADO,
                     showConfirmButton: false,
                     //timer: 2000
                 })
                 $('#registrar_incid_btn').prop('disabled', false).html('Guardar');
 
                 if (result.COD_ESTADO == 1) {
                     $('#modal_agregar_inciden').modal('hide');
                 }
 
                 getListIncidencias();
 
             },
             error: function (xhr, status, error) {
 
             },
 
         }, JSON);
 
     })



    $("#sectionParametrosEditarIncidencia").on("submit", function (e) {

        e.preventDefault();
        var ModelIncidenciaEditar = $('#sectionParametrosEditarIncidencia').serializeObject();
        $('#editar_incid_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');

        $.ajax({
            url: URL_EDITAR_INCIDENCIA,
            data: ModelIncidenciaEditar,
            dataType: 'json',
            success: function (result) {
                Swal.fire({
                    type: result.COD_ESTADO == 0 ? 'error' : 'success',
                    title: result.DES_ESTADO,
                    showConfirmButton: false,
                    //timer: 2000
                })

                $('#editar_incid_btn').prop('disabled', false).html('Guardar');
                $('#modal_editar_inciden').modal('hide');

                getListIncidencias();

            },
            error: function (xhr, status, error) {

            },

        }, JSON);

    })

    $("#sectionParametrosActualizarIncidencia").on("submit", function (e) {

        e.preventDefault();

        if ($('#txt_hora_fin').val().length == 0) {
            Swal.fire({
                type: 'info',
                title: "Debe ingresar el tiempo de solución.",
                showConfirmButton: false,
            });
            return false;
        }

        var ModelIncidenciaActualizar = $('#sectionParametrosActualizarIncidencia').serializeObject();
        $('#actualizar_incid_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');

        $.ajax({
            url: URL_ACTUALIZAR_INCIDENCIA,
            data: ModelIncidenciaActualizar,
            dataType: 'json',
            success: function (result) {
                Swal.fire({
                    type: result.COD_ESTADO == 0 ? 'error' : 'success',
                    title: result.DES_ESTADO,
                    showConfirmButton: false,
                    //timer: 2000
                })

                $('#actualizar_incid_btn').prop('disabled', false).html('Guardar');
                $('#modal_actualizar_inciden').modal('hide');

                getListIncidencias();

            },
            error: function (xhr, status, error) {

            },

        }, JSON);

    })


    $('#customRadio5').on('click', function () {
        if ($(this).is(':checked')) {
            $('#dis_proy_lbl').css({ "display": "block" });
            $('#dis_proy_checked').css({ "display": "block" });

            $('#customRadio3').prop('checked', true);



            $('#dis_num_inf_lbl').css({ "display": "block" });
            $('#dis_num_inf_txt').css({ "display": "block" });

        }
    });

    $('#customRadio6').on('click', function () {
        if ($(this).is(':checked')) {
            $('#dis_proy_lbl').css({ "display": "none" });
            $('#dis_proy_checked').css({ "display": "none" });

            $('#customRadio4').prop('checked', true);


            $('#dis_num_inf_lbl').css({ "display": "none" });
            $('#dis_num_inf_txt').css({ "display": "none" });

        }

    });

    $("#txt_editar_hora_fin, #txt_editar_fecha_incidencia").on("change keyup paste", function () {

        var tm_inicio_edit = $('#txt_editar_fecha_incidencia').val();
        var tm_final_edit = $('#txt_editar_hora_fin').val();

        var tiempo_array = restarHoras(tm_inicio_edit, tm_final_edit);

        $('#txt_editar_tiempo_solucion').val(tiempo_array[0])
        $('#txt_editar_tiempo_minutos').val(tiempo_array[1])

    });

    $("#txt_fecha_incidencia,#txt_hora_inci,#txt_fecha_fin,#txt_hora_fina").change(function () {
        $('#txt_tiempo_solucion').val('');
        $('#txt_tiempo_minutos').val('');
        //$('#txt_fecha_fin').val('');
        //$('#txt_hora_fina').val('');
    });

    $("#txt_fecha_incidencia").on("change keyup paste", function () {

        $('#txt_hora_inci').trigger("click");
        //$('#txt_hora_inci').show();

        //$('#txt_hora_inci').click();

    });

    $("#txt_fecha_fin").on("change keyup paste", function () {

        $('#txt_hora_fina').trigger("click");

    });
});


$('#selectRutaCorredor').on('change', function () {
    var ruta = $("#selectRutaCorredor option:selected").attr('data-ID_RUTA');
    getListIncidencias();

});





$("body").on("click", "#btnRegistrarIncidencia", function () {


    $('#txtSelectdivAdd').val('')
    $('#txt_tipo_infraccion').val('');
    $('#txt_tipo_sancion').val('');
    $('#txt_paquete').val('');
    $('#txt_supervisor').val('');
    $('#txt_EstadoPersonaIncidencia').val('');
    //$('#tblArchivo tbody').empty();


    var idselectRuta = $('#selectRutaCorredor').val()
    $('#txtSelectdivAdd').val(idselectRuta);


    var tipo_infraccion = $("#selectInfraccion option:selected").attr('data-TIPO_INFRACCION');
    $('#txt_tipo_infraccion').val(tipo_infraccion);

    var tipo_sancion = $("#selectInfraccion option:selected").attr('data-SANCION');
    $('#txt_tipo_sancion').val(tipo_sancion);


    if (tipo_sancion == "Amonestación" || tipo_sancion == "Restricción") {
        $('#txt_num_dias').css({ "display": "block" });
        $('#txt_tipo_date').css({ "display": "block" });
    } else if (tipo_sancion == "Ninguno") {
        $('#txt_num_dias').css({ "display": "none" });
        $('#txt_tipo_date').css({ "display": "none" });
    }

    var paquete_servicio = $("#selectPlacaPadron option:selected").attr('data-PAQUETE_SERVICIO');
    $('#txt_paquete').val(paquete_servicio);
    var estado_situacion = $("#selectAgregarPersonaIncidencia option:selected").attr('data-Estado_Situacion');
    $('#txt_EstadoPersonaIncidencia').val(estado_situacion)

    //DURITO
    switch (paquete_servicio) {
        case '2.2': $('#txt_contrato').val('AUTORIZACIÓN');
            break;
        case '2.3': $('#txt_contrato').val('AUTORIZACIÓN');
            break;
        case '2.6': $('#txt_contrato').val('AUTORIZACIÓN');
            break;
        case '3.1': $('#txt_contrato').val('AUTORIZACIÓN');
            break;
        case '3.3': $('#txt_contrato').val('AUTORIZACIÓN');
            break;
        default:
            $('#txt_contrato').val('CONSECIÓN');
            break;
    }

    $('.selectpicker').selectpicker('refresh');



});


$("body").on("click", ".bootstrap-select", function () {
    var Selectstyle = $('#selectAgregarPersonaIncidencia').parent().children()[2]

    $(Selectstyle).removeAttr('style')

    $('.selectpicker').selectpicker('refresh');

    //$(Selectstyle).css('min-width', '444px');

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

function getPersonaIncidencia() {
    $.ajax({
        url: URL_LIST_PERSONA_INCIDENCIA,
        dataType: 'json',
        success: function (result) {
            $.each(result, function () {
                $('#selectAgregarTipoPersonaIncidencia').append('<option value="' + this.ID_PERSONA_INCIDENCIA + '">' + this.DESCRIPCION + '</option>');
                $('#selectEditarTipoPersonaIncidencia').append('<option value="' + this.ID_PERSONA_INCIDENCIA + '">' + this.DESCRIPCION + '</option>');

            });

            $('.selectpicker').selectpicker('refresh');
            getListConductores();

        }
    }, JSON);
}


function getListConductores() {

    $.ajax({
        url: URL_LIST_CONDUCTORES,
        dataType: 'json',
        success: function (result) {
            $.each(result, function () {
                $('#selectAgregarPersonaIncidencia').append('<option value="' + this.CODIGO + '" data-Estado_Situacion="' + this.SITUACION + '">' + this.CODIGO + ' - ' + this.APELLIDOS + ' ' + this.NOMBRES + '</option>');
                $('#selectEditarPersonaIncidencia').append('<option value="' + this.CODIGO + '">' + this.CODIGO + ' - ' + this.APELLIDOS + ' ' + this.NOMBRES + '</option>');


            });

            $('.selectpicker').selectpicker('refresh');
            getListInfracciones();

        }
    }, JSON);
}


function getListInfracciones() {

    $.ajax({
        url: URL_LIST_INFRACCIONES,
        dataType: 'json',
        success: function (result) {
            $.each(result, function () {
                $('#selectInfraccion').append('<option data-SANCION="' + this.SANCION + '" data-TIPO_INFRACCION="' + this.TIPO_INFRACCION + '" value="' + this.ID_INFRACCION + '">' + this.COD_INFRACCION + ' ' + this.DESCRIPCION + '</option>');
                $('#selectEditarInfraccion').append('<option data-SANCION="' + this.SANCION + '" data-TIPO_INFRACCION="' + this.TIPO_INFRACCION + '" value="' + this.ID_INFRACCION + '">' + this.COD_INFRACCION + ' ' + this.DESCRIPCION + '</option>');
            });

            $('.selectpicker').selectpicker('refresh');

            //var tipo_infraccion = $("#selectInfraccion option:selected").attr('data-TIPO_INFRACCION');
            //var tipo_sancion = $("#selectInfraccion option:selected").attr('data-SANCION');
            //$('#txt_tipo_sancion').val(tipo_sancion)
            //$('#txt_tipo_infraccion').val(tipo_infraccion)

            getCorredoresByModalidad();
        }

    }, JSON);
}

function getListPlacabyCorredor() {
    $('#selectPlacaPadron').empty();
    $('#selectEditarPlacaPadron').empty();

    idCorredor = $('#selectCorredores').val();

    $.ajax({
        url: URL_LIST_BUSES_BY_ID,
        data: {
            idCorredor: idCorredor
        },
        dataType: 'json',
        success: function (result) {
            $.each(result, function () {
                $('#selectPlacaPadron').append('<option data-PAQUETE_SERVICIO="' + this.BS_PAQUETE_SERVICIO + '" value="' + this.BS_PLACA + '">' + this.BS_PLACA + '</option>');
                $('#selectEditarPlacaPadron').append('<option data-PAQUETE_SERVICIO="' + this.BS_PAQUETE_SERVICIO + '" value="' + this.BS_PLACA + '">' + this.BS_PLACA + '</option>');
            });

        }
    }, JSON);
}

var DATA_LISTA_INCIDENCIAS = ""

function getListIncidencias(id_ruta_) {


    // PERMISOS
    //1= EDITAR || 2==ELIMINAR 
    var permiso_edit = $('#permisoActualiza').val();
    var permiso_eliminar = $('#permisoEliminar').val();


    $('#Tbincidencias tbody').empty();
    //$('#mostrartodo_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Mostrando...');
    DATA_LISTA_INCIDENCIAS = "";
    var strHTML = '';
    if (typeof id_ruta_ == 'undefined') {
        id_ruta_ = $('#selectRutaCorredor').val();

    }
    //var id_ruta_ = $('#selectRutaCorredor').val();
    var class_estado = "";


    $.ajax({
        url: URL_LIST_INCIDENCIAS,
        data: {
            id_ruta: id_ruta_
        },
        dataType: 'json',
        success: function (result) {

            //console.log(result,'resultado')
            DATA_LISTA_INCIDENCIAS = result.Table
            //$('#listar_buses tbody').empty();
            //$('#mostrartodo_btn').prop('disabled', false).html('<i class="fas fa-bus-alt"></i>&nbsp; Mostrar Todo');
            if (result.Table.length <= 0) {
                strHTML += '<tr><td colspan="100" class="text-left" style="padding-left: 50%">No hay información para mostrar</td></tr>';

            } else {
                //console.log("resultado", result)
                var count = 0;
                $.each(result.Table, function (i, data) {
                    var dateFecha_Incidencia = data.FECHA_INCIDENCIA
                    var Fecha_Incidencia = dateFecha_Incidencia.substr(0, 10);
                    var hora_incidencia = dateFecha_Incidencia.substr(11, 16)

                    //var parts = dateFecha_Incidencia.split(/[- :]/);

                    var clase_button = "";
                    var clase_button_cerrar = ""
                    var tipo_estado = ""
                    var estado_incidencia = data.ESTADO_INCIDENCIA

                    if (estado_incidencia == 'PENDIENTE') {
                        estado_incidencia = "PENDIENTE"
                        tipo_estado = '<span class="badge badge-pill badge-danger">' + estado_incidencia + '</span>'
                        class_estado = "style='cursor:pointer;'";
                        habilitar_act = "onclick='abrirActualizarIncidencia($(this).parent().parent());'";
                        clase_button = "far fa-edit"
                        clase_button_cerrar = "fas fa-lock-open"

                    }
                    else if (estado_incidencia == 'FINALIZADO') {
                        estado_incidencia = "FINALIZADO"
                        tipo_estado = '<span class="badge badge-pill badge-primary" style="background-color:#3981ce;" >' + estado_incidencia + '</span>'
                        class_estado = "style='cursor:not-allowed;'";
                        habilitar_act = "onclick='event.preventDefault();'";
                        clase_button = "fas fa-search"
                        clase_button_cerrar = "fas fa-lock"


                    }

                    strHTML += '<tr  onclick="seleccionaFila($(this))" ' +
                        ' data-ID_INCIDENCIA="' + (data.ID_INCIDENCIA == null ? "" : data.ID_INCIDENCIA) + '" ' +
                        ' data-ID_RUTA="' + (data.ID_RUTA == null ? "" : data.ID_RUTA) + '" ' +
                        //' data-NUM_DOCUMENTO="' + (data.COD_INFRACCION == null ? "" : data.COD_INFRACCION) + '" ' +
                        ' data-NUM_DOCUMENTO="' + (data.NUM_DOCUMENTO == null ? "" : data.NUM_DOCUMENTO) + '" ' +
                        ' data-EMPRESA="' + (data.EMPRESA == null ? "" : data.EMPRESA) + '" ' +
                        ' data-PLACA_PADRON="' + (data.PLACA_PADRON == null ? "" : data.PLACA_PADRON) + '" ' +
                        ' data-LADO="' + (data.LADO == null ? "" : data.LADO) + '" ' +
                        ' data-LUGAR_HECHO="' + (data.LUGAR_HECHO == null ? "" : data.LUGAR_HECHO) + '" ' +
                        ' data-INTERVENCION_POLICIAL="' + (data.INTERVENCION_POLICIAL == null ? "" : data.INTERVENCION_POLICIAL) + '" ' +
                        ' data-INFORMANTE="' + (data.INFORMANTE == null ? "" : data.INFORMANTE) + '" ' +
                        ' data-PARADERO="' + (data.PARADERO == null ? "" : data.PARADERO) + '" ' +
                        ' data-FECHA_INCIDENCIA="' + (data.FECHA_INCIDENCIA == null ? "" : data.FECHA_INCIDENCIA) + '" ' +
                        ' data-PROYECTO_CARTA="' + (data.PROYECTO_CARTA == null ? "" : data.PROYECTO_CARTA) + '" ' +
                        ' data-DESCRIPCION_INCIDENCIA="' + (data.DESCRIPCION_INCIDENCIA == null ? "" : data.DESCRIPCION_INCIDENCIA) + '" ' +
                        //OBSERVACION CAMBIO A PAQUETE_SERVICIO
                        ' data-PAQUETE_CONCESION="' + (data.PAQUETE_CONCESION == null ? "" : data.PAQUETE_CONCESION) + '" ' +
                        ' data-ID_INFRACCION="' + (data.ID_INFRACCION == null ? "" : data.ID_INFRACCION) + '" ' +
                        ' data-HORA_FIN="' + (data.HORA_FIN == null ? "" : data.HORA_FIN) + '" ' +
                        ' data-INFORME="' + (data.INFORME == null ? "" : data.INFORME) + '" ' +
                        ' data-CONTRATO="' + (data.CONTRATO == null ? "" : data.CONTRATO) + '" ' +
                        ' data-NUM_SERVICIO="' + (data.NUM_SERVICIO == null ? "" : data.NUM_SERVICIO) + '" ' +
                        ' data-NUM_INFORME="' + (data.NUM_INFORME == null ? "" : data.NUM_INFORME) + '" ' +
                        ' data-CUMPLIMIENTO_PLAZO="' + (data.CUMPLIMIENTO_PLAZO == null ? "" : data.CUMPLIMIENTO_PLAZO) + '" ' +
                        ' data-OBSERVACION_CUMPLIMIENTO="' + (data.OBSERVACION_CUMPLIMIENTO == null ? "" : data.OBSERVACION_CUMPLIMIENTO) + '" ' +

                        ' data-COD_INFRACCION="' + (data.COD_INFRACCION == null ? "" : data.COD_INFRACCION) + '" ' +
                        ' data-DESCRIPCION_INFRACCION="' + (data.DESCRIPCION_INFRACCION == null ? "" : data.DESCRIPCION_INFRACCION) + '" ' +
                        ' data-SANCION="' + (data.SANCION == null ? "" : data.SANCION) + '" ' +
                        ' data-ESTADO_INCIDENCIA="' + (data.ESTADO_INCIDENCIA == null ? "" : data.ESTADO_INCIDENCIA) + '" ' +
                        ' data-DESCARGO="' + (data.DESCARGO == null ? "" : data.DESCARGO) + '" ' +
                        ' data-SUPERVISOR="' + (data.SUPERVISOR == null ? "" : data.SUPERVISOR) + '" ' + '>' + '>' +


                        '<td>' + (count + 1) + '</td>' +

                        '<td class="text-center" >' + Fecha_Incidencia + '</td>' +
                        '<td class="text-center" >' + (data.FECHA_INCIDENCIA == null ? "" : data.FECHA_INCIDENCIA).substring(11, 16) + '</td>' +
                        '<td class="text-center" >' + (data.PLACA_PADRON == null ? "" : data.PLACA_PADRON) + '</td>' +
                        '<td class="text-center" >' + (data.COD_INFRACCION == null ? "" : data.COD_INFRACCION) + ' - ' + (data.APELLIDOS == null ? "" : data.APELLIDOS) + ' ' + (data.NOMBRES == null ? "" : data.NOMBRES) + '</td>' +
                        '<td class="text-center" >' + '<button type="button" class="btn btn-primary btn-sm" data-toggle="popover" data-trigger="focus" title="Infraccion" data-content="' + (data.DESCRIPCION_INFRACCION == null ? "No hay informacion" : data.DESCRIPCION_INFRACCION) + '">' + '<i class="fas fa-eye"></i>' + '</button>' + '</td>' +
                        '<td class="text-center" >' + (data.SANCION == null ? "" : data.SANCION).toUpperCase() + '</td>' +
                        '<td class="text-center" >' + '<button type="button" class="btn btn-primary btn-sm" data-toggle="popover" data-trigger="focus" title="Descripcion" data-content="' + (data.DESCRIPCION_INCIDENCIA == null ? "No hay informacion" : data.DESCRIPCION_INCIDENCIA) + '">' + '<i class="fas fa-eye"></i>' + '</button>' + '</td>' +
                        '<td class="text-center" >' + tipo_estado + '</td>' +
                        '<td class="text-center" >' + (data.USU_REG == null ? "" : data.USU_REG) + '</td>' +
                        '<td class="text-center" >' + (data.FECHA_REG == null ? "" : data.FECHA_REG) + '</td>' +
                        '<td>' + '<span class="' + clase_button + '" aria-hidden="true" style="cursor:pointer;" onclick="abrirEditarIncidencia($(this).parent().parent());" ></span>' + '</td>' +
                         (permiso_eliminar == 1 ? '<td>' + '<span class="far fa-trash-alt" aria-hidden="true" style="cursor:pointer;"  onclick="confirmarAnulacionIncidencia(' + data.ID_INCIDENCIA + ');" ></span>' + '</td>' : '') +
                        '<td>' + '<span class="' + clase_button_cerrar + '" aria-hidden="true" style="cursor:pointer;" onclick="CambiarEstadoIncidencias($(this).parent().parent());" ></span>' + '</td>' +
                        '</tr>';
                    count++;
                });

            }
            $('#listar_incidencias').append(strHTML);
            util.activarEnumeradoTabla('#Tbincidencias', $('#btnBusquedaEnTabla'));

            $('[data-toggle="popover"]').popover({
                placement: 'right'
            });
        },

    }, JSON);

}

function CambiarEstadoIncidencias(elemet) {

    var id_incidencia_ = elemet.attr('data-id_incidencia');
    var estado_ = elemet.attr('data-estado_incidencia');

    if (estado_ == "FINALIZADO") {
        estado_ = "PENDIENTE"
    } else {
        estado_ = "FINALIZADO"
    }

    $.ajax({
        url: URL_ESTADO_INCIDENCIA,
        dataType: 'json',
        data: {
            id_incidencias: id_incidencia_,
            estado: estado_
        },
        success: function (result) {

            Swal.fire({
                type: result.COD_ESTADO == 0 ? 'error' : 'success',
                title: result.DES_ESTADO,
                showConfirmButton: false,
            })
            getListIncidencias();
        }
    }, JSON);

}



function abrirEditarIncidencia(element) {

    if (element.attr('data-estado_incidencia') == "FINALIZADO") {
        $('#editar_incid_btn').prop("disabled", true);

    } else {
        $("#editar_incid_btn").removeAttr('disabled');

    }

    var id_incidencia = element.attr('data-ID_INCIDENCIA');
    var persona_inc = element.attr('data-NUM_DOCUMENTO');
    var concesionario = element.attr('data-EMPRESA');
    var infraccion = element.attr('data-ID_INFRACCION');
    var placa_padron = element.attr('data-PLACA_PADRON');
    var tipo_infraccion = element.attr('data-COD_INFRACCION');
    var paquete = element.attr('data-PAQUETE_CONCESION');
    var contrato = element.attr('data-CONTRATO');
    var fecha_incidencia = element.attr('data-FECHA_INCIDENCIA');
    var hora_fin = element.attr('data-HORA_FIN');
    var lado = element.attr('data-LADO');
    var desc_incid = element.attr('data-DESCRIPCION_INCIDENCIA');
    var num_servicio = element.attr('data-NUM_SERVICIO');
    var sancion = element.attr('data-SANCION');
    var lugarhecho = element.attr('data-LUGAR_HECHO');
    var customRadio1 = element.attr('data-INTERVENCION_POLICIAL');
    var customRadio2 = element.attr('data-INTERVENCION_POLICIAL');
    var informante = element.attr('data-INFORMANTE');
    var paradero = element.attr('data-PARADERO');
    var customRadio5 = element.attr('data-INFORME');
    var customRadio6 = element.attr('data-INFORME');
    var customRadio3 = element.attr('data-PROYECTO_CARTA');
    var customRadio4 = element.attr('data-PROYECTO_CARTA');
    var num_informe = element.attr('data-NUM_INFORME');
    var cumplimiento_plazo_si = element.attr('data-CUMPLIMIENTO_PLAZO');
    var cumplimiento_plazo_no = element.attr('data-CUMPLIMIENTO_PLAZO');
    var observacion_cumplimiento = element.attr('data-OBSERVACION_CUMPLIMIENTO');
    var supervisor = element.attr('data-SUPERVISOR');

    //durito
    $('#selectEditarTipoPersonaIncidencia').val(Number(1));

    $('#txt_incidencia').val(Number(id_incidencia));
    $('#selectEditarPersonaIncidencia').val(Number(persona_inc));
    $('#selectEditarConcesionario').val(Number(concesionario));
    $('#selectEditarInfraccion').val(Number(infraccion));
    $('#selectEditarPlacaPadron').val(placa_padron);
    $('#txt_editar_tipo_infraccion').val(tipo_infraccion);
    $('#txt_editar_paquete').val(paquete);
    $('#txt_editar_contrato').val(contrato);



    var fecha_in = convertirTipoDateLocal(fecha_incidencia);
    $('#txt_editar_fecha_incidencia').val(fecha_in);


    var fecha_fin = convertirTipoDateLocal(hora_fin)
    $('#txt_editar_hora_fin').val(fecha_fin);

    $('#select_editar_lado').val(lado);
    $('#txta_editar_desc_incid').val(desc_incid);
    $('#txt_editar_num_servicio').val(num_servicio);

    $('#txt_editar_tipo_sancion').val(sancion);
    $('#selectEditarlugarhecho').val(lugarhecho);

    $('#txt_editar_num_informe').val(num_informe);
    $('#txta_edit_Obser_Cumpl').val(observacion_cumplimiento);
    $('#txt_edit_supervisor').val(supervisor);

    //calculo tiempo

    var tm_inicio_edit = fecha_in;
    var tm_final_edit = fecha_fin;

    var tiempo_array = restarHoras(tm_inicio_edit, tm_final_edit);

    $('#txt_editar_tiempo_solucion').val(tiempo_array[0])
    $('#txt_editar_tiempo_minutos').val(tiempo_array[1])

    //Intervencion Policial
    if (customRadio1 == 'SI') {
        $('#editar_customRadio1').is(':checked')
        $('#editar_customRadio1').prop('checked', true);
    } else if (customRadio2 == 'NO') {
        $('#editar_customRadio2').is(':checked')
        $('#editar_customRadio2').prop('checked', true);
    }

    if (editarradioPlazoSi == 'SI') {
        $('#editarradioPlazoSi').is(':checked')
        $('#editarradioPlazoSi').prop('checked', true);
    } else if (editarradioPlazoNo == 'NO') {
        $('#editarradioPlazoNo').is(':checked')
        $('#editarradioPlazoNo').prop('checked', true);
    }

    $('#txt_editar_informante').val(informante);
    $('#txt_editar_paradero').val(paradero);

    if (customRadio5 == 'SI') {
        $('#editar_customRadio5').is(':checked')
        $('#editar_customRadio5').prop('checked', true);

        $('#editar_dis_proy_lbl').css({ "display": "block" });
        $('#editar_dis_proy_checked').css({ "display": "block" });

        $('#dis_edit_num_inf_lbl').css({ "display": "block" });
        $('#dis_edit_num_inf_txt').css({ "display": "block" });

        if (customRadio3 == 'SI') {

            $('#editar_customRadio3').is(':checked')
            $('#editar_customRadio3').prop('checked', true);

        } else if (customRadio4 == 'NO') {

            $('#editar_customRadio4').is(':checked')
            $('#editar_customRadio4').prop('checked', true);

        }

    } else if (customRadio6 == 'NO') {

        $('#editar_customRadio6').is(':checked')
        $('#editar_customRadio6').prop('checked', true);

        $('#editar_dis_proy_lbl').css({ "display": "none" });
        $('#editar_dis_proy_checked').css({ "display": "none" });

        $('#dis_edit_num_inf_lbl').css({ "display": "none" });
        $('#dis_edit_num_inf_txt').css({ "display": "none" });
    }

    $('.selectpicker').selectpicker('refresh')

    $('#modal_editar_inciden').modal('show');
}


function seleccionaFila(elementFila) {
    $.each($('#Tbincidencias tbody > tr'), function () {
        $(this).css('background-color', '');
    });
    elementFila.css('background-color', '#17a2b838');
}


$('#modal_agregar_inciden .save').click(function (e) {
    e.preventDefault();
    addImage(5);
    $('#modal_agregar_inciden').modal('hide');
    return false;
})

$('#modal_actualizar_inciden .save').click(function (e) {
    e.preventDefault();
    addImage(5);
    $('#modal_actualizar_inciden').modal('hide');
    return false;
})

function confirmarAnulacionIncidencia(idIncidencia) {
    Swal.fire({
        text: "Estas seguro que deseas eliminar la incidencia ?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: URL_ANULA_INCIDENCIA,
                dataType: 'json',
                data: {
                    idIncidencia: idIncidencia
                },
                success: function (result) {
                    getListIncidencias();
                    Swal.fire({
                        type: (result.COD_ESTADO == 1 ? 'success' : 'error'),
                        title: result.DES_ESTADO,
                        showConfirmButton: false,
                    });
                }
            }, JSON);
        }
    });
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
    $('#selectCorredores').find('option').clone().appendTo('#selectCorredor');
    getRutaPorCorredores_inicial();


}

function getRutaPorCorredores_inicial() {

    $.ajax({
        url: URL_GET_RUTA_X_CORREDOR,
        dataType: 'json',
        data: { idCorredor: $('#selectCorredores').val() },
        success: function (result) {
            $('#selectRutaCorredor').empty();
            if (result.length == 0) { // si la lista esta vacia
                $('#selectRutaCorredor').append('<option value="0">' + '--No hay información--' + '</option>');
                return false;
            } else {
                $.each(result, function () {
                    $('#selectRutaCorredor').append('<option value="' + this.ID_RUTA + '">' + this.NRO_RUTA + '</option>');
                });
            }
            getListPlacabyCorredor();
            getListConcesionarios();
            getListIncidencias();
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

function getListConcesionarios() {
    $('#selectConcesionario').empty();
    $('#selectEditarConcesionario').empty();

    id_corredor = $('#selectCorredores').val();

    $.ajax({
        url: URL_LIST_CONCESIONARIOS,
        data: {
            id_corredor: id_corredor
        },
        dataType: 'json',
        success: function (result) {
            $.each(result, function () {
                $('#selectConcesionario').append('<option value="' + this.ID_CONCESIONARIO + '">' + this.NOMBRE + '</option>');
                $('#selectEditarConcesionario').append('<option value="' + this.ID_CONCESIONARIO + '">' + this.NOMBRE + '</option>');
            });
            $('.selectpicker').selectpicker('refresh');
        }
    }, JSON);
}

function exportarTabla() {

    $('#exportar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Exportando...');
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();
    var time = today.getHours() + "_" + today.getMinutes() + "_" + today.getSeconds();


    today = dd + '_' + mm + '_' + yyyy + '_' + time;


    tableDataExportar = [{
        "sheetName": "Hoja",
        "data": []
    }];
    var options = {
        fileName: "Reporte Incidencias " + today
    };

    var tHeadExcel = [{ "text": "N°" },
        { "text": "FECHA INCIDENCIA" },
        { "text": "TIPO INCIDENCIA" },
        { "text": "UBICACIÓN DE PARADERO" },
        { "text": "SENTIDO" },
        { "text": "SUPERVISOR" },
        { "text": "DESCRIPCIÓN" },
        { "text": "CONTRATO" },
        { "text": "CONSORCIO" },
        { "text": "PQ." },
        { "text": "C.C" },
        { "text": "PLACA" },
        { "text": "CODIGO CAC" },
        { "text": "CONDUCTOR" },
        { "text": "INFORMANTE" },
        { "text": "TIEMPO SOLUCIÓN" },
        { "text": "MINUTOS" },
    ]

    tableDataExportar[0].data.push(tHeadExcel);

    $.each(DATA_LISTA_INCIDENCIAS, function (i) {

        //var dateFecha_Incidencia = this.FECHA_INCIDENCIA.substring(0, 10);
        //var parts = dateFecha_Incidencia.split(/[- :]/);
        //var Fecha_Incidencia = parts[2] + '/' + parts[1] + '/' + parts[0];

        //var nom_usu = this.NOMBRES_USU;
        //var apepat_usu = this.APEPAT_USU;
        //var apemat_usu = this.APEMAT_USU;

        var ape_conductor = this.APELLIDOS;
        var nom_conductor = this.NOMBRES;
        var ape_nom_conductor = ape_conductor + ' ' + nom_conductor;

        var fecha_inicio = this.FECHA_INCIDENCIA;
        var fecha_fin = this.HORA_FIN;

        var tiempo_array = restarHoras(fecha_inicio, fecha_fin);

        //var nombre_usuario = nom_usu + ' ' + apepat_usu + ' ' + apemat_usu;

        var objeto = this;
        var itemArr = [];
        itemArr.push({ "text": (i + 1) },
            { "text": fecha_inicio },
            { "text": objeto.DESCRIPCION_INFRACCION },
            { "text": objeto.PARADERO },
            { "text": objeto.LADO },
            { "text": objeto.SUPERVISOR },
            { "text": objeto.DESCRIPCION_INCIDENCIA },
            { "text": objeto.CONTRATO },
            { "text": objeto.EMPRESA_NOMBRE },
            { "text": objeto.PAQUETE_CONCESION },
            { "text": objeto.ABREVIATURA_CORREDOR },
            { "text": objeto.PLACA_PADRON },
            { "text": objeto.CODIGO_CAC },
            { "text": ape_nom_conductor },
            { "text": objeto.INFORMANTE },
            { "text": tiempo_array[0] },
            { "text": tiempo_array[1] }
        )
        tableDataExportar[0].data.push(itemArr);

    });
    Jhxlsx.export(tableDataExportar, options);
    $('#exportar').prop('disabled', false).html('Exportar');
}

function textAreaAdjust(o) {
    o.style.height = "1px"; o.style.height = (25 + o.scrollHeight) + "px";
}

function restarHoras(tm_inicio, tm_final) {
    var values = [];

    let fecha_inicio = new Date(tm_inicio);
    let fecha_final = new Date(tm_final)

    let resta = fecha_final.getTime() - fecha_inicio.getTime()

    var minutos = Math.round(resta / (1000 * 60))
    var hora_min = (resta / (1000 * 60 * 60));
    var hora_parte_decimal = Math.round((hora_min % 1) * 60);
    var hora_parte_entera = Math.floor(hora_min);

    hora_parte_entera = checkTime(hora_parte_entera);
    hora_parte_decimal = checkTime(hora_parte_decimal);

    var hora_junto = (hora_parte_entera + ':' + hora_parte_decimal);

    values.push(hora_junto);
    values.push(minutos);

    return values;
}

function checkTime(i) {
    if (i < 10 && i >= 0) {
        i = "0" + i;
    }
    return i;
}
function convertirTipoDateLocal(fechaddmmyyyy) {
    //fechaddmmyyyy = '14/09/2020 18:29:00'

    var fechanormal = fechaddmmyyyy.substr(0, 10)
    var horanormal = fechaddmmyyyy.substr(11, 18)

    var partsfecha = fechanormal.split("/", 3);
    var fechacompleta = partsfecha[2] + '-' + partsfecha[1] + '-' + partsfecha[0] + 'T' + horanormal;

    return fechacompleta;
}

function CalcularTiempo() {

    var fech_ini = $('#txt_fecha_incidencia').val();
    var hora_ini = $('#txt_hora_inci').val();

    var fech_fin = $('#txt_fecha_fin').val();
    var hora_fin = $('#txt_hora_fina').val();

    if (fech_ini == '' || hora_ini == '' || fech_fin == '' || hora_fin == '') {
        Swal.fire({
            type: 'info',
            title: "Debe ingresar los tiempos de la incidencia",
            showConfirmButton: false,
        });
        return false;
    }

    var tm_inicio = convertirTipoDateLocal(fech_ini + ' ' + hora_ini);
    var tm_final = convertirTipoDateLocal(fech_fin + ' ' + hora_fin);

    var tiempo_array = restarHoras(tm_inicio, tm_final);

    $('#txt_tiempo_solucion').val(tiempo_array[0])
    $('#txt_tiempo_minutos').val(tiempo_array[1])
}

function subirArchivoTemp(element) {
    var nombreArchivo = element.val().split("\\").pop();
    var extensionArchivo = nombreArchivo.split('.')[1];
    element.siblings(".custom-file-label").addClass("selected").html(nombreArchivo);
}

function adjuntar() {
    $('#tblArchivo tbody').empty();
    var data = new FormData();
    data.append("fileadj", $("#fileadj")[0].files[0]);

    $.ajax({
        type: "POST",
        url: URL_SUBIR_ARCHIVO_INCIDENCIA,
        data: data,
        processData: false,
        contentType: false,
        success: function (result) {
            $.each(result.lista, function (x, item) {
                $('#tblArchivo tbody').append("<tr><td>" + item + "</td><td><img src=\"" + urlPage + "/Upload/" + result.carpeta + "/" + item + "\" style=\"height: 30px;\"/></td><td><i class=\"fas fa-minus-circle\" style=\"cursor:pointer;\" onclick=\"AnularArchivo('" + item + "', this)\" ></i></td></tr>");
                if (result.mensaje!="") {
                    Swal.fire({
                        type: 'info',
                        title: result.mensaje,
                        showConfirmButton: false,
                    });
                }
            });
        }
    });
}

function AnularArchivo(archivo,objeto) {

        $.ajax({
                type: "POST",
                url: URL_ELIMINAR_ARCHIVO_INCIDENCIA,
                dataType: 'json',
                data: {
                    nombre: archivo
                },
                success: function (result) {
                    $(objeto).parent().parent().remove();
                }
            }, JSON);
        
}
