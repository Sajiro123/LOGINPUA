var DATA_POG = [];

var ARCHIVOS_PERMITIDOS = {
    EXCEL: 'xlsx',
    ZIP: 'zip'
}

var bool = false;
var name_ajax = "";

var ACTION_REEMPLAZAR_DATOS = false;
$(document).ready(function () {

    getCorredoresByModalidad();



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
        if (Number($('#selectRuta').val()) == 0) {
            Swal.fire({
                type: 'info',
                title: "Debe seleccionar una ruta válida.",
                showConfirmButton: false,
                timer: 2000
            });
            return false;
        }

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
            url: URL_ACTION_UPLOAD_FILE + '/?idRuta=' + $('#selectRuta').val() +
                                        '&fecha=' + $('#txtFechaRegistro').val() +
                                        '&reemplazar=' + (ACTION_REEMPLAZAR_DATOS ? 1 : 0) + '&abrevProveedorSistemasCorredor=' + $("#selectRuta option:selected").text(),
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
                    text: "Ya existe información para esta fecha " + respuesta.DES_ESTADO + "s, desea reemplazalo??",
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
        });
    })

    //VERSION ULTIMA 
    $("#formSubirExcelDespacho_actual").on("submit", function (e) {

        name_ajax = "formSubirExcelDespacho"

        bool = true
        e.preventDefault();
        if (Number($('#selectRuta').val()) == 0) {
            Swal.fire({
                type: 'info',
                title: "Debe seleccionar una ruta válida.",
                showConfirmButton: false,
                timer: 2000
            });
            return false;
        }

        if ($('#archivoSubido').val().length == 0) {
            Swal.fire({
                type: 'info',
                title: "Debe cargar un archivo para enviar la información.",
                showConfirmButton: false,
                timer: 2000
            });
            return false;
        }
        var formularioDatos = new FormData(document.getElementById("formSubirExcelDespacho_actual"));
        $('#btnSubirArchivo').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Subiendo...');
        $.ajax({
            url: URL_ACTION_UPLOAD_FILE + '/?tipoCarga=' + $('#selectTipocarga option:selected').val() +
                                         '&idRuta=' + $('#selectRuta').val() +
                                        '&fecha=' + $('#txtFechaRegistro').val() +
                                        '&reemplazar=' + (ACTION_REEMPLAZAR_DATOS ? 1 : 0) + '&abrevProveedorSistemasCorredor=' + $("#selectRuta option:selected").text(),
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
                bool = false;
            }

            if (respuesta.COD_ESTADO == 3) { //encontró fecha 
                name_ajax = "cambio"

                Swal.fire({
                    title: 'ALERTA !',
                    text: "Ya existe información para esta fecha " + respuesta.DES_ESTADO + "s, desea reemplazalo??",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Si'
                }).then((result) => {
                    bool = false;
                    //console.log('resultado prop->>', result);
                    if (result.value) { //si responde que s   
                        ACTION_REEMPLAZAR_DATOS = true;
                        bool = true;
                        $("#formSubirExcelDespacho").submit();
                        name_ajax = "formSubirExcelDespacho"
                    } else {
                        name_ajax = "cambio"
                        ACTION_REEMPLAZAR_DATOS = false;
                    }
                });
            }
        });
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

function subirArchivoTemp(element) {
    var nombreArchivo = element.val().split("\\").pop();
    var extensionArchivo = nombreArchivo.split('.')[3];
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


function subirArchivoTemp_Actualizado(element, tipoArchivoCarga) {
    var nombreArchivo = element.val().split("\\").pop();
    var extensionArchivo = nombreArchivo.split('.')[3];
    //console.log("extensionArchivo", extensionArchivo);
    element.siblings(".custom-file-label").html('');
    if (tipoArchivoCarga == 'I' ? extensionArchivo == ARCHIVOS_PERMITIDOS.EXCEL : extensionArchivo == ARCHIVOS_PERMITIDOS.ZIP) {
        $('#modalRegistroPicoPlaca .modal-footer .btn-success').prop('disabled', false);
        element.siblings(".custom-file-label").addClass("selected").html(nombreArchivo);
    } else {
        element.siblings(".custom-file-label").addClass("selected").html("Seleccionar Archivo");
        $('#modalRegistroPicoPlaca .modal-footer .btn-success').prop('disabled', true);
        Swal.fire({
            type: 'error',
            title: 'El archivo ' + nombreArchivo + ' no tiene el formato correcto.',
            showConfirmButton: false,
            //timer: 2500
        })
    }
}


function consultarData() {



    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');
    $.ajax({
        url: URL_GET_REPORTE_FECHA,
        dataType: 'json',
        data: { mes_año: $('#mes_año').val(), idruta: $('#selectRutaConsulta').val() },
        success: function (result) {

            $('#consultar').prop('disabled', false).html('Consultar');
            $('#tbPasajeroVal tbody').empty();
            var strHTML = '';
            var i = 0;
            if (result.length <= 0) {
                strHTML += '<tr><td colspan="13" class="text-center">No hay información para mostrar</td></tr>';
            } else {
                $.each(result, function () {
                    i++;
                    if (this.length < 0) {
                        strHTML += '<tr><td colspan="13" class="text-center">No hay información para mostrar</td></tr>';
                    } else {
                        strHTML += '<tr data-ID_MAESTRO="' + this.ID_MAESTROPASAJERO + '">' +
                                        '<td class="text-center" >' + i + '</td>' +
                                        '<td class="text-center" >' + this.FECHA + '</td>' +
                                        '<td class="text-center" >' + this.CANTIDAD_VIAJES + '</td>' +
                                        '<td class="text-center" >' + this.NRO_RUTA + '</td>' +
                                        '<td class="text-center" >' + this.USU_REG + '</td>' +
                                        '<td class="text-center" >' + this.FECHA_REG + '</td>' +
                                    '</tr>';
                    }

                });
            }
            $('#tbPasajeroVal tbody').append(strHTML);
        }
    }, JSON);
}

var time_final = 0;

$(document).ajaxStart(function (e, xhr, options) {

    if (bool) {
        if (name_ajax == "formSubirExcelDespacho") {
             
            setTimeout(function () {
                progressbar();
            }, 4000);
            console.log(time_final, 'final')
        }
    }
}).ajaxStop(function () {
    alert("ajax se completo")
    return
});


function progressbar() {



    $.ajax({
        url: URL_TIME,
        dataType: 'json',
        data: { mes_año: $('#mes_año').val(), idruta: $('#selectRutaConsulta').val() },

        success: function (result) {

            time_final += result;

            console.log(time_final, 'time_final')

            return result
        }
    }, JSON);

}