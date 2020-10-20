
$(document).ready(function () {
    $('#txtBuscar_').keyup(function (e) {
        if (e.which) {
            $.uiTableFilter($('#tbPersona'), this.value);
        }
    });
    //
    getTipoDocumento();
    getTipoRol();
    getlistaPersonas();

    $('#numdocu').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    var validateEmail = function (email) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    }
});


$("body").on("click", "#modal_registroperso", function () {

    //LIMPIAR LOS INPUTS DEL MODAL REGISTRO
    $('#nombre').val('');
    $('#apepaterno').val('');
    $('#apematerno').val('');
    $('#numdocu').val('');
    $('#correo').val('');

    $('input[name="NOMBRE"]').focus();


    $('#idpersona').selectpicker('refresh')
});








function getTipoDocumento() {
    $.ajax({
        url: URL_GET_TIPO_DOCUMENTO,
        dataType: 'json',
        success: function (result) {
            $('#tipodocu').empty();
            if (result.length == 0) { // si la lista esta vacia
                $('#tipodocu').append('<option value="0">' + '--No hay información--' + '</option>');
            } else {
                $.each(result, function () {
                    $('#tipodocu').append('<option value="' + this.ID_TIPO_DOCUMENTO + '">' + this.NOMBRE + '</option>');
                });
            }
        }
    }, JSON);
}


function getTipoRol() {
    $.ajax({
        url: URL_GET_TIPO_ROL,
        dataType: 'json',
        success: function (result) {
            $('#tiporol').empty();
            if (result.length == 0) { // si la lista esta vacia
                $('#tiporol').append('<option value="0">' + '--No hay información--' + '</option>');
            } else {
                $.each(result, function () {
                    $('#tiporol').append('<option value="' + this.ID_ROLPERSONA + '">' + this.NOMBRE + '</option>');
                });
            }
        }
    }, JSON);
}

function AgregarPersona() {

    var nombre = $("#nombre").val();
    var apepaterno = $("#apepaterno").val();
    var apematerno = $("#apematerno").val();
    var numdocu = $("#numdocu").val();
    var correo = $("#correo").val();



    if (nombre === "") {
        Swal.fire({ title: 'Ingresar el Nombre', type: 'warning', })
        return false;
    }
    if (apepaterno === "") {
        Swal.fire({ title: 'Ingresar el apellido paterno', type: 'warning', })
        return false;
    } if (apematerno === "") {
        Swal.fire({ title: 'Ingresar el apellido materno', type: 'warning', })
        return false;
    } if (numdocu === "") {
        numdocu = "0";
    } if (correo === "") {
        correo = "";
    }
    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');
    $.ajax({
        url: URL_GET_AGREGAR_PERSONA,
        data: {
            nombre: nombre,
            apepaterno: apepaterno,
            apematerno: apematerno,
            tipodocu: $('#tipodocu').val(),
            numdocu: numdocu,
            correo: correo,
            tiporol: $('#tiporol').val()
        },
        dataType: 'json',
        success: function (result) {
            $('#consultar').prop('disabled', false).html('Guardar');

            $('input[type="text"]').val('');


            getlistaPersonas();
            Swal.fire({
                type: result.COD_ESTADO == 1 ? 'success' : 'error',
                title: result.DES_ESTADO,
                showConfirmButton: false,
            });
            $('#exampleModal').modal('hide');
        }
    }, JSON);
}




function getlistaPersonas() {

    // PERMISOS
    //1= EDITAR || 2==ELIMINAR 
    var permiso_edit = $('#permisoActualiza').val();
    var permiso_eliminar = $('#permisoEliminar').val();


    $.ajax({
        url: URL_GET_LISTAR_PERSONA,
        dataType: 'json',
        success: function (result) {
            var strHTML = '';
            var i = 0;
            DATA_LISTA_REGISTROS = result;
            $.each(result, function () {
                i++;

                var DNI = this.NRODOCUMENTO == 0 ? "" : this.NRODOCUMENTO 
                var CORREO = this.CORREO == null ? "" : this.CORREO  

                $('#tbPersona tbody').empty();

                strHTML += '<tr onclick="seleccionaFila($(this))" data-NRODOCUMENTO="' + DNI + '"' +
                'data-NOMBRES="' + this.NOMBRES + '"' +
                'data-APEPAT="' + this.APEPAT + '"' +
                'data-APEMAT="' + this.APEMAT + '"' +
                'data-ROL="' + this.ROL + '"' +
                'data-ID_PERSONA="' + this.ID_PERSONA + '"' +
                'data-CORREO="' +CORREO + '"' +
                'data-TIPO_DOCUMENTO ="' + this.ID_TIPO_DOCUMENTO + '"' +
                'data-TIPO_ID_ROLPERSONA ="' + this.ID_ROLPERSONA + '"' +
                'data-TIPODOC="' + this.TIPODOC + '">' +
                                       '<td>' + i + '</td>' +
                                '<td>' + this.TIPODOC + '</td>' +
                                '<td>' + DNI + '</td>' +
                                '<td>' + this.NOMBRES + '</td>' +
                                '<td> ' + this.APEPAT + '</td>' +
                                '<td>' + this.APEMAT + '</td>' +
                                '<td>' + CORREO + '</td>' +
                                  '<td>' + this.ROL + '</td>' +
                                '<td class="text-center">' + this.USU_REG + '</td>' +
                                '<td class="text-center">' + this.FECHA_REG + '</td>' +
                                (permiso_eliminar == 1 ? '<td>' + '<span class="far fa-trash-alt" aria-hidden="true" style="cursor:pointer;" onclick="confirmarAnulacionParadero(' + this.ID_PERSONA + ');" ></span>' + '</td>' : '') +
                                 (permiso_edit == 1 ? '<td>' + '<span class="far fa-edit btn-edit" aria-hidden="true" style="cursor:pointer;" </span>' + '</td>' : '') +
                            '</tr>';
            });
            $('#tbPersona tbody').append(strHTML);
            util.activarEnumeradoTabla('#tbPersona', $('#btnBusquedaEnTabla'));

        }
    }, JSON);
}

var POSICIO_CLICK_ROW = null;
$("body").on("click", ".btn-edit", function () {
    if (POSICIO_CLICK_ROW) {
        $('#tbPersona tbody').find('tr').eq(POSICIO_CLICK_ROW - 1).find('td:eq(11)').find('button:eq(1)').click()
    }
    POSICIO_CLICK_ROW = Number($(this).parents("tr").find('td').eq(0).text());
    var codTipoDocumento = Number($(this).parents("tr").attr('data-TIPO_DOCUMENTO'));
    var tipodoc = $(this).parents("tr").attr('data-TIPODOC');
    var nrodocu = $(this).parents("tr").attr('data-NRODOCUMENTO');
    var nombres = $(this).parents("tr").attr('data-NOMBRES');
    var apepat = $(this).parents("tr").attr('data-APEPAT');
    var apemat = $(this).parents("tr").attr('data-APEMAT');
    var rol = $(this).parents("tr").attr('data-ROL');
    var tiporol = $(this).parents("tr").attr('data-TIPO_ID_ROLPERSONA');
    var correo = $(this).parents("tr").attr('data-CORREO');


    var selectRol = $('#tiporol').clone();
    var selectTipodoc = $('#tipodocu').clone();

    var selectTipodoc = $(this).parents("tr").find("td:eq(1)").html('<select name="edit_tipodoc" class="form-control form-control-sm" style="color: black;background-color: #51656463;">' + selectTipodoc.html() + '</select>');

    $(this).parents("tr").find("td:eq(2)").html('<input name="edit_doc" type="number" style="color: black;background-color: #51656463;" class="form-control form-control-sm" value="' + nrodocu + '">');
    $(this).parents("tr").find("td:eq(3)").html('<input name="edit_nombres" style="color: black;background-color: #51656463;" class="form-control form-control-sm" value="' + nombres + '">');
    $(this).parents("tr").find("td:eq(4)").html('<input name="edit_apepat"  style="color: black;background-color: #51656463;" class="form-control form-control-sm" value="' + apepat + '">');
    $(this).parents("tr").find("td:eq(5)").html('<input name="edit_apemat" style="color: black;background-color: #51656463;" class="form-control form-control-sm" value="' + apemat + '">');
    $(this).parents("tr").find("td:eq(6)").html('<input name="edit_correo" style="color: black;background-color: #51656463;" class="form-control form-control-sm " value="' + correo + '">');
    var selectRol = $(this).parents("tr").find("td:eq(7)").html('<select name="edit_rol" class="form-control form-control-sm" style="color: black;background-color: #51656463;">' + selectRol.html() + '</select>');


    selectTipodoc.find('select').val(codTipoDocumento);
    selectRol.find('select').val(tiporol);

    $(this).parents("tr").find("td:eq(11)").prepend("<button class='btn-info btn-xs btn-update' style='margin:0 4px' ><i class='fas fa-check'></i></button>" +
         "<button class='btn-warning btn-xs btn-cancel' style='margin:0 4px'><i class='fas fa-ban'></i></button>")
    $(this).hide();
});

$("body").on("click", ".btn-cancel", function () {


    var tipodoc = $(this).parents("tr").attr('data-TIPODOC');
    var nrodocu = $(this).parents("tr").attr('data-NRODOCUMENTO');
    var nombres = $(this).parents("tr").attr('data-NOMBRES');
    var apepat = $(this).parents("tr").attr('data-APEPAT');
    var apemat = $(this).parents("tr").attr('data-APEMAT');
    var rol = $(this).parents("tr").attr('data-ROL');
    var correo = $(this).parents("tr").attr('data-CORREO');

    $(this).parents("tr").find("td:eq(1)").text(tipodoc);
    $(this).parents("tr").find("td:eq(2)").text(nrodocu);
    $(this).parents("tr").find("td:eq(3)").text(nombres);
    $(this).parents("tr").find("td:eq(4)").text(apepat);
    $(this).parents("tr").find("td:eq(5)").text(apemat);
    $(this).parents("tr").find("td:eq(6)").text(correo);
    $(this).parents("tr").find("td:eq(7)").text(rol);


    $(this).parents("tr").find(".btn-edit").show();
    $(this).parents("tr").find(".btn-update").remove();
    $(this).parents("tr").find(".btn-cancel").remove();

});

$("body").on("click", ".btn-update", function () {
    var tipodoc = $(this).parents("tr").find("select[name='edit_tipodoc']").val();
    var nrodocu = $(this).parents("tr").find("input[name='edit_doc']").val();
    var nombres = $(this).parents("tr").find("input[name='edit_nombres']").val();
    var apepat = $(this).parents("tr").find("input[name='edit_apepat']").val();
    var apemat = $(this).parents("tr").find("input[name='edit_apemat']").val();
    var rol = $(this).parents("tr").find("select[name='edit_rol']").val();

    var correo = $(this).parents("tr").find("input[name='edit_correo']").val();

    var idpersona = $(this).parents("tr").attr('data-ID_PERSONA');

    if (nrodocu === "") {
        nrodocu = 0;
    }
    if (apepat === "") {
        Swal.fire({ title: 'Ingresar el apellido paterno', type: 'warning', })
        return false;
    } if (apemat === "") {
        Swal.fire({ title: 'Ingresar el apellido materno', type: 'warning', })
        return false;
    }

    $.ajax({
        url: URL_GET_MODIFICAR_PERSONA,
        data: {
            idpersona: idpersona,
            nombre: nombres,
            apepaterno: apepat,
            apematerno: apemat,
            numdocu: nrodocu,
            tipodocu: tipodoc,
            correo: correo,
            tiporol: rol,
        },
        dataType: 'json',
        success: function (result) {
            getlistaPersonas();
            Swal.fire({
                type: result.COD_ESTADO == 1 ? 'success' : 'error',
                title: result.DES_ESTADO,
                showConfirmButton: false,
            });
            $('#exampleModal').modal('hide');
        }
    }, JSON);

    $(this).parents("tr").find(".btn-edit").show();
    $(this).parents("tr").find(".btn-cancel").remove();
    $(this).parents("tr").find(".btn-update").remove();
});


function confirmarAnulacionParadero(idpersona) {
    Swal.fire({
        text: "Estas seguro que deseas anular el registro ?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: URL_ANULA_PARADERO,
                dataType: 'json',
                data: { idpersona: idpersona },
                success: function (result) {
                    getlistaPersonas();
                    Swal.fire({
                        type: (result.COD_ESTADO == 1 ? 'success' : 'error'),
                        title: result.DES_ESTADO,
                        showConfirmButton: false,
                        timer: 2500
                    });
                }
            }, JSON);
        }
    });
}




function seleccionaFila(elementFila) {

    $.each($('#tbPersona tbody > tr'), function () {
        $(this).css('background-color', '');
    });
    elementFila.css('background-color', '#17a2b838');
}


