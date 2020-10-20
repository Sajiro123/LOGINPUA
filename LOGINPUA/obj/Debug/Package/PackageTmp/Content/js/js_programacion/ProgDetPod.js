//var DATA_POG = [];

var ARCHIVOS_PERMITIDOS = {
    //EXCEL: 'xlsx',
    ZIP: 'zip',
}

var Validaciones = new validaciones();
//var bool = false;
//var name_ajax = "";

var nombreadjunto = "";
$(document).ready(function () {

    $('#txtFechaRegistro').datepicker({
        endDate: new Date(FECHA_HOY.split('/')[1] + "/" + FECHA_HOY.split('/')[0] + "/" + FECHA_HOY.split('/')[2]),
        format: "dd/mm/yyyy"
    });

    $('#mes_año').datepicker({
        format: "mm/yyyy",
        viewMode: "months",
        minViewMode: "months"
    });

    $("#formSubirZipDespacho").on("submit", function (e) {
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

        var formularioDatos = new FormData(document.getElementById("formSubirZipDespacho"));
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
                        $("#formSubirZipDespacho").submit();
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

function seleccionaFila(elementFila) {
    $.each($('#tbGeneral tbody > tr'), function () {
        $(this).css('background-color', '');
    });
    elementFila.css('background-color', '#17a2b838');
}

//function subirArchivoTemp(element) {
//    var nombreArchivo = element.val().split("\\").pop();
//    var extensionArchivo = nombreArchivo.split('.')[1];
//    element.siblings(".custom-file-label").html('');
//    if (extensionArchivo == ARCHIVOS_PERMITIDOS.EXCEL) {
//        $('#modalImportarProgramacion .modal-footer .btn-success').prop('disabled', false);
//        element.siblings(".custom-file-label").addClass("selected").html("Seleccionar Archivo");
//        $('#lblArchivoSubido').text(nombreArchivo)
//    } else {
//        element.siblings(".custom-file-label").addClass("selected").html("Seleccionar Archivo");
//        $('#modalImportarProgramacion .modal-footer .btn-success').prop('disabled', true);
//        Swal.fire({
//            type: 'error',
//            title: 'El archivo ' + nombreArchivo + ' no tiene el formato correcto.',
//            showConfirmButton: false,
//            timer: 2500
//        })
//    }
//}

function AbrirModal() {
    var para = $('#ParaRegistra').val('');
    var copia = $('#CopiaRegistra').val('');
    var asunto = $('#AsuntoRegistra').val('');
    var cuerpo = $('#CuerpoMensajeRegistra').val('');
    para.val('mvizcarra@atu.gob.pe')
    copia.val('jose.hermitano@atu.gob.pe,do10@atu.gob.pe,do30@atu.gob.pe,do25@atu.gob.pe,sstr57@atu.gob.pe,sstr44@atu.gob.pe,sstr81@atu.gob.pe,sstr83@atu.gob.pe')
    asunto.val('Placas Programadas Corredores Complementarios')
    $('#ModalRegistrarArchivo').modal('show')
}


function AbrirEnviarCorreo(e) {
    nombreadjunto = "";
    nombreadjunto = $(e[0]).attr('data-nombre')

    var para = $('#Para').val('');
    var copia = $('#Copia').val('');
    var asunto = $('#Asunto').val('');
    var cuerpo = $('#CuerpoMensaje').val('');
    para.val('mvizcarra@atu.gob.pe')
    copia.val('jose.hermitano@atu.gob.pe,do10@atu.gob.pe,do30@atu.gob.pe,do25@atu.gob.pe,sstr57@atu.gob.pe,sstr44@atu.gob.pe,sstr81@atu.gob.pe,sstr83@atu.gob.pe')
    asunto.val('Placas Programadas Corredores Complementarios')
    $('#modal_enviar_correo').modal("show")
}

function consultarData() {

    var id_perfil = $('#id_perfil').val()

    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');
    $.ajax({
        url: URL_GET_REPORTE_FECHA,
        dataType: 'json',
        data: {
            mes_año: $('#mes_año').val(),
        },
        success: function (result) {

            $('#consultar').prop('disabled', false).html('Consultar');
            $('#tbGeneral tbody').empty();
            var strHTML = '';
            var i = 0;
            if (result.Table.length <= 0) {
                strHTML += '<tr><td colspan="5" class="text-center">No hay información para mostrar</td></tr>';
            } else {
                $.each(result.Table, function () {
                    i++;
                    if (this.length < 0) {
                        strHTML += '<tr  ><td colspan="13" class="text-center">No hay información para mostrar</td></tr>';
                    } else {
                        strHTML += '<tr onclick="seleccionaFila($(this))" data-NOMBRE="' + this.NOMBRE + '">' +
                                        '<td class="text-center" >' + i + '</td>' +
                                        '<td class="text-center" >' + this.NOMBRE + '</td>' +
                                        '<td class="text-center" >' + this.FECHA + '</td>' +
                                          (id_perfil != 261 ? '<td class="text-center" >' + this.USU_REG + '</td>' : '') +
                                        (id_perfil != 261 ? '<td class="text-center" >' + this.FECHA_REG + '</td>' : '') +
                                        '<td class="text-center" ><button class="btn btn-primary btn-sm" onclick="Descargar($(this).parent().parent())"><i class="fas fa-download" ></i>     </button></td>' +
                                        '<td class="text-center" ><button class="btn btn-primary btn-sm" onclick="AbrirEnviarCorreo($(this).parent().parent())"><i class="fas fa-envelope"></i>     </button></td>' +
                                    '</tr>';
                    }

                });
            }
            $('#tbGeneral tbody').append(strHTML);

            $.each($('#tbGeneral').children('tbody').children(), function (j, i) {
                if (j == 0) {
                    $($(this)[0]).css("background-color", "rgba(23, 162, 184, 0.22)")
                    console.log(this)
                }
            });


        }, error: function (xhr, status, error) {
            Validaciones.ValidarSession();
        },
    }, JSON);
}