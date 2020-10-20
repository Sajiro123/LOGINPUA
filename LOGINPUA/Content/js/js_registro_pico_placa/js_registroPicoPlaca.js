var ARCHIVOS_PERMITIDOS = {
    EXCEL: 'xlsx',
    ZIP: 'zip'
}
var ACTION_REEMPLAZAR_DATOS = false;

$(document).ready(function () {
    $('#selectCorredores').find('option').clone().appendTo('#selectCorredor');
    getRutaPorCorredor();
    $('#txtFechaRegistro').val(FECHA_HOY)
    $('#txtFechaConsultaIni, #txtFechaConsultaFin').datepicker({
        endDate: new Date(FECHA_HOY.split('/')[1] + "/" + FECHA_HOY.split('/')[0] + "/" + FECHA_HOY.split('/')[2]),
        format: "dd/mm/yyyy"
    });

    $('#txtFechaRegistro').datepicker({
        //startDate: new Date(),
        //endDate: new Date(FECHA_HOY),
        //minDate: moment("15/08/2019"),
        //maxDate: moment("20/08/2019"),
        //startDate: new Date("08/01/2019"),
        endDate: new Date(FECHA_HOY.split('/')[1] + "/" + FECHA_HOY.split('/')[0] + "/" + FECHA_HOY.split('/')[2]),
        format: "dd/mm/yyyy"
    });
    //consultarData();
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
                                        '&reemplazar=' + (ACTION_REEMPLAZAR_DATOS ? 1 : 0) + '&abrevProveedorSistemasCorredor=' + $("#selectCorredor option:selected").text(),
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

            //consultarData();
            if (respuesta.COD_ESTADO == 1) {
                $('#modalRegistroPicoPlaca').modal('hide');
            }

            if (respuesta.COD_ESTADO == 3) { //encontró fecha 
                Swal.fire({
                    title: 'ALERTA !',
                    text: "Ya existe información para esta fecha, desea reemplazalo??",
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
});

function subirArchivoTemp(element, tipoArchivoCarga) {
    var nombreArchivo = element.val().split("\\").pop();
    var extensionArchivo = nombreArchivo.split('.')[1];
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


function demoImportar() {
    $.ajax({
        url: URL_IMPORTAR_DATA_DESPACHO,
        data: { idRuta: 1, fecha: '08/08/2019' },
        dataType: 'json',
        success: function (result) {
            console.log("importar result---->", result);
        }
    }, JSON);
}


function getRutaPorCorredor() {

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

function AbrirModalRegistroPicoPlaca() {
    if ($('#selectRuta').val() == 0) {
        Swal.fire({
            type: 'error',
            title: "Debe seleccionar una ruta válida.",
            showConfirmButton: false,
            timer: 2000
        });
    } else {

        $('#txtFechaRegistro').val(FECHA_HOY);
        $('#btnSubirArchivo').prop('disabled', false).html('Subir archivo');
        $('#archivoSubido').val('');
        $('#lblArchivoSubido').text('Seleccionar Archivo');
        $('#modalRegistroPicoPlaca').modal('show');
    }
}

function minTommss(minutes) {
    var sign = minutes < 0 ? "-" : "";
    var min = Math.floor(Math.abs(minutes));
    var sec = Math.floor((Math.abs(minutes) * 60) % 60);
    return sign + (min < 10 ? "0" : "") + min + ":" + (sec < 10 ? "0" : "") + sec;
}

var DATA_LISTA_REGISTROS = [];
function consultarData() {

    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');
    $.ajax({
        url: URL_GET_REPORTE_BY_FECHAS,
        dataType: 'json',
        data: { fechaInicio: $('#txtFechaConsultaIni').val(), fechaFin: $('#txtFechaConsultaFin').val(), idruta: $('#selectRutaCorredor').val() },
        success: function (result) {
            $('#consultar').prop('disabled', false).html('Consultar');
            $('#tbReportePicoPlaca tbody').empty();
            var strHTML = '';
            if (result.length == 0) {
                DATA_LISTA_REGISTROS = [];
                strHTML += '<tr><td colspan="13" class="text-center">No hay información para mostrar</td></tr>';
            } else {
                DATA_LISTA_REGISTROS = result;
                $.each(result, function () {
                    var velocidadPromedioAB = Number(this.VEL_PROMEDIO_AB);
                    var velocidadPromedioBA = Number(this.VEL_PROMEDIO_BA);

                    var tiempoPromedioAB = Number(this.TIEMPO_PROM_A).toFixed(2);
                    var tiempoPromedioBA = Number(this.TIEMPO_PROM_B).toFixed(2);

                    //console.log(Utilidades.ConvertTimeformat24H(this.HINICIO), Utilidades.ConvertTimeformat24H(this.HFIN), this.TIEMPO_PROM_A)
                    strHTML += '<tr>' +
                                    '<td>' + this.FECHA_REGISTRO.split(' ')[0] + '</td>' +
                                    '<td>' + this.ABREV_CORREDOR + '</td>' +
                                    '<td class="text-center" >' + this.NRO_RUTA + '</td>' +
                                    '<td class="text-center" >' + Utilidades.ConvertTimeformat24H(this.HINICIO) + '</td>' +
                                    '<td class="text-center" >' + Utilidades.ConvertTimeformat24H(this.HFIN) + '</td>' +
                                    //'<td class="text-center" >' + this.HINICIO + '</td>' +
                                    //'<td class="text-center" >' + this.HFIN + '</td>' +
                                    '<td class="text-center" >' + this.DISTANCIA_A + '</td>' +
                                    '<td class="text-center" >' + this.DISTANCIA_B + '</td>' +
                                    '<td class="text-center" >' + velocidadPromedioAB.toFixed(2) + '</td>' +
                                    '<td class="text-center" >' + velocidadPromedioBA.toFixed(2) + '</td>' +
                                    '<td class="text-center" >' + minTommss(tiempoPromedioAB) + '</td>' +
                                    '<td class="text-center" >' + minTommss(tiempoPromedioBA) + '</td>' +
                                    '<td class="text-right">' + this.USU_REG + '</td>' +
                                    '<td class="text-right">' + this.FECHA_REG + '</td>' +
                                '</tr>';
                });
            }
            $('#tbReportePicoPlaca tbody').append(strHTML);
        }
    }, JSON);
}

var tableDataExportar = [
    {
        "sheetName": "Hoja",
        "data": []
    }
];

function exportarTabla() {
    tableDataExportar = [{ "sheetName": "Hoja", "data": [] }];
    var options = {
        fileName: $("#selectRutaCorredor option:selected").text() + "_Registro Pico Placa " + $('#txtFechaConsultaIni').val() + ' - ' + $('#txtFechaConsultaFin').val()
    };
    var tHeadExcel =
    [
        { "text": "FECHA" },
        { "text": "CORREDOR" },
        { "text": "RUTA" },
        { "text": "HORA.INI" },
        { "text": "HORA.FIN" },
        { "text": "DISTANCIA.AB" },
        { "text": "DISTANCIA.BA" },
        { "text": "VEL PROM AB" },
        { "text": "VEL PROM BA" },
        { "text": "TIEMPO PROM AB" },
        { "text": "TIEMPO PROM BA" },
        { "text": "USUARIO REG" },
        { "text": "FECHA RED" },
    ]
    //console.log("DATA_LISTA_REGISTROS", DATA_LISTA_REGISTROS);

    tableDataExportar[0].data.push(tHeadExcel);
    $.each(DATA_LISTA_REGISTROS, function () {
        //console.log("this", this);
        var objeto = this;
        var itemArr = [];
        itemArr.push(
            { "text": objeto.FECHA_REGISTRO.split(' ')[0] },
            { "text": objeto.ABREV_CORREDOR },
            { "text": objeto.NRO_RUTA },
            { "text": Utilidades.ConvertTimeformat24H(objeto.HINICIO) },
            { "text": Utilidades.ConvertTimeformat24H(objeto.HFIN) },
            { "text": objeto.DISTANCIA_A },
            { "text": objeto.DISTANCIA_B },
            { "text": Number(objeto.VEL_PROMEDIO_AB) },
            { "text": Number(objeto.VEL_PROMEDIO_BA) },
            { "text": minTommss(objeto.TIEMPO_PROM_A) },
            { "text": minTommss(objeto.TIEMPO_PROM_B) },
            { "text": objeto.USU_REG },
            { "text": objeto.FECHA_REG }
        )
        tableDataExportar[0].data.push(itemArr);
    });
    Jhxlsx.export(tableDataExportar, options);
}

function verificatipoCarga() {
    switch ($('#selectTipocarga').val()) {
        case 'I':
            $('#sectionFecha').css('display', '');
            $('#archivoSubido').attr('accept', 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet')
            break;
        case 'M':
            $('#sectionFecha').css('display', 'none');
            $('#archivoSubido').attr('accept', '.zip')
            break;
        default:
            break;
    }
}
//function guardarRegistrosPicoPlaca() {
//    var idruta = $('#selectRuta').val();
//    var turno = $('#selectTurno').val();
//    var fecha = FECHA_HOY;
//    var serializado = '';
//    //
//    $.each($('#tbRegistroPicoPlaca tbody > tr'),function () {
//        var horaInicio = $(this).attr('data-hini');
//        var horaFin = $(this).attr('data-hfin');
//        var velPromedioAB = $(this).find('td').eq(0).find('input').val();
//        var velPromedioBA = $(this).find('td').eq(1).find('input').val();
//        //
//        if (velPromedioAB.length != 0 && velPromedioAB.length != 0) {
//            serializado += horaInicio + '|' + horaFin + '|' + (velPromedioAB.length == 0 ? 0 : velPromedioAB) + '|' + ( velPromedioBA.length == 0 ? 0 : velPromedioBA ) + '~';
//        }
//    });
//    //
//    serializado = serializado.substring(0, serializado.length - 1);
//    $.ajax({
//        url: URL_REGISTRAR_VELOCIDAD_PPLACA,V
//        dataType: 'json',
//        data: { idRuta :idruta , turno:turno , fecha :fecha, serializado: serializado  },
//        success: function (result) {
//            Swal.fire({
//                type:  result.COD_ESTADO == 0 ? 'error' : 'success',
//                title: result.DES_ESTADO,
//                showConfirmButton: false,
//                timer: 2000
//            })
//            console.log('result.COD_ESTADO', result.COD_ESTADO);
//            if (result.COD_ESTADO == 1) { // si la lista esta vacia
//                $('#modalRegistroPicoPlaca').modal('hide');
//            }
//        }
//    }, JSON);
//}