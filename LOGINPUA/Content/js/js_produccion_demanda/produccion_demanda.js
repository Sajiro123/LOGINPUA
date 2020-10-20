//var DATA_POG = [];

var ARCHIVOS_PERMITIDOS = {
    EXCEL: 'xlsx',
}

var Validaciones = new validaciones();
//var bool = false;
//var name_ajax = "";

var nombreadjunto = "";
$(document).ready(function () {

    //getCorredoresByModalidad();

    $('#txtFechaRegistro').datepicker({
        endDate: new Date(FECHA_HOY.split('/')[1] + "/" + FECHA_HOY.split('/')[0] + "/" + FECHA_HOY.split('/')[2]),
        format: "dd/mm/yyyy"
    });

    $('#mes_año').datepicker({
        format: "mm/yyyy",
        viewMode: "months",
        minViewMode: "months"
    });

    $("#formSubirExcelDespacho").on("submit", function (e) {
        e.preventDefault();


        if ($('#archivoSubido').val().length == 0) {
            Swal.fire({
                type: 'info',
                title: "Debe cargar un archivo para enviar la información.",
                showConfirmButton: false,
                timer: 2000
            });
            return false;
        }

        var formularioDatos = new FormData(document.getElementById("formSubirExcelDespacho"));
        $('#btnSubirArchivo').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Subiendo...');
        $.ajax({
            url: URL_ACTION_UPLOAD_FILE + '/?fecha=' + $('#txtFechaRegistro').val() +
                                        '&reemplazar=' + (ACTION_REEMPLAZAR_DATOS ? 1 : 0),
            type: "post",
            dataType: "json",
            data: formularioDatos,
            cache: false,
            contentType: false,
            processData: false
        }).done(function (respuesta) {
            ACTION_REEMPLAZAR_DATOS = false;
            $('#btnSubirArchivo').prop('disabled', false).html('Subir archivo');
            Swal.fire({
                type: respuesta.COD_ESTADO == 1 ? 'success' : 'error',
                title: respuesta.DES_ESTADO,
                showConfirmButton: false,
                //timer: 2500
            });

            if (respuesta.COD_ESTADO == 1) {
                $('#ModalRegistrarArchivo').modal('hide');
                consultarData();
            }

            if (respuesta.COD_ESTADO == 3) { //encontró fecha 
                Swal.fire({
                    title: 'ALERTA !',
                    text: "Ya existe información para esta fecha " + respuesta.DES_ESTADO + ", desea reemplazalo??",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Si'
                }).then((result) => {
                    //console.log('resultado prop->>', result);
                    if (result.value) { //si responde que s   
                        ACTION_REEMPLAZAR_DATOS = true;
                        $("#formSubirExcelDespacho").submit();
                    } else {
                        ACTION_REEMPLAZAR_DATOS = false;
                    }
                });
            }
        }).fail(function (data) {
            Validaciones.ValidarSession();
        });
    })

    $("#formEnviarCorreo").on("submit", function (e) {
        e.preventDefault();

        var para = $('#Para').val().trim();
        var copia = $('#Copia').val().trim();
        var asunto = $('#Asunto').val().trim();
        var cuerpo = $('#CuerpoMensaje').val().trim();

        if (para.length == 0 && asunto.length == 0) {
            Swal.fire({
                type: 'info',
                title: "Debes completar los campos necesarios para enviar el correo",
                showConfirmButton: false,
                timer: 2000
            });
            return false;
        }

        $('#btnEnviarCorreo').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Enviando...');
        $.ajax({
            url: URL_ENVIAR_CORREO,
            dataType: 'json',
            type: "POST",
            data: {
                para: para,
                copia: copia,
                asunto: asunto,
                nombreadjunto: nombreadjunto,
                cuerpo: cuerpo
            },
            success: function (result) {
                $('#btnEnviarCorreo').prop('disabled', false).html('Enviar');
                Swal.fire({
                    type: result.tipo == 1 ? 'success' : 'error',
                    title: result.mensaje,
                    showConfirmButton: false,
                    //timer: 2500
                });
                $('#modal_enviar_correo').modal('hide');
            }
        }, JSON);
    })
});


function AbrirModalImportar() {
    $('#ModalRegistrarArchivo').modal("show")

}


function getRutaPorCorredores() {

    $.ajax({
        url: URL_GET_RUTA_X_CORREDOR,
        dataType: 'json',
        data: { idCorredor: $('#selectCorredor').val() },
        success: function (result) {

            $('#selectRuta').empty();
            if (result.length == 0) { // si la lista esta vacia
                $('#selectRuta').append('<option value="0">' + '--No hay información--' + '</option>');

            } else {
                $.each(result, function () {
                    $('#selectRuta').append('<option value="' + this.ID_RUTA + '">' + this.NRO_RUTA + '</option>');
                });
            }
        }
    }, JSON);
}

function seleccionaFila(elementFila) {
    $.each($('#tbGeneral tbody > tr'), function () {
        $(this).css('background-color', '');
    });
    elementFila.css('background-color', '#17a2b838');
}
