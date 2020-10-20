$(document).ready(function () {



    getListInfracciones();
    getPersonaIncidencia();

    $("#sectionParametrosAgregarInfraccion").on("submit", function (e) {

        e.preventDefault();

        //if ($('#BS_PLACA').val().length == 0) {
        //    Swal.fire({
        //        type: 'info',
        //        title: "Debe llenar los campos obligatorios para realizar el registro.",
        //        showConfirmButton: false,
        //    });
        //    return false;
        //}

        var ModelInfraccionAgregar = $('#sectionParametrosAgregarInfraccion').serializeObject();
        $('#registrar_infrac_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');

        $.ajax({
            url: URL_REGISTRAR_INFRACCION,
            data: ModelInfraccionAgregar,
            dataType: 'json',
            success: function (result) {
                Swal.fire({
                    type: result.COD_ESTADO == 0 ? 'error' : 'success',
                    title: result.DES_ESTADO,
                    showConfirmButton: false,
                    //timer: 2000
                })
                $('#registrar_infrac_btn').prop('disabled', false).html('Guardar');

                if (result.COD_ESTADO == 1) {
                    $('#modal_agregar_infracc').modal('hide');
                }

                getListInfracciones();
                //var id_buses = $('#selectAbrevCorredor').val();
                //getListBusesbyId(id_buses);
                

            },
            error: function (xhr, status, error) {

            },

        }, JSON);

    })

    $("#sectionParametrosEditarInfraccion").on("submit", function (e) {

        e.preventDefault();
        var ModelInfraccionEditar = $('#sectionParametrosEditarInfraccion').serializeObject();
        $('#editar_infrac_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');

        $.ajax({
            url: URL_EDITAR_INFRACCION,
            data: ModelInfraccionEditar,
            dataType: 'json',
            success: function (result) {
                Swal.fire({
                    type: result.COD_ESTADO == 0 ? 'error' : 'success',
                    title: result.DES_ESTADO,
                    showConfirmButton: false,
                    //timer: 2000
                })

                $('#editar_infrac_btn').prop('disabled', false).html('Guardar');
                $('#modal_editar_infracc').modal('hide');

                getListInfracciones();

            },
            error: function (xhr, status, error) {

            },

        }, JSON);

    })

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
                $('#selectAgregarPersonaIncidencia').append('<option value="' + this.ID_PERSONA_INCIDENCIA + '">' + this.DESCRIPCION + '</option>');
                $('#selectEditarPersonaIncidencia').append('<option value="' + this.ID_PERSONA_INCIDENCIA + '">' + this.DESCRIPCION + '</option>');
                
            });


        }
    }, JSON);
}

function getListInfracciones() {
    var DATA_LISTA_INFRACCIONES = [];

    // PERMISOS
    //1= EDITAR || 2==ELIMINAR 
    var permiso_edit = $('#permisoActualiza').val();
    var permiso_eliminar = $('#permisoEliminar').val();



    $('#Tbinfraccion tbody').empty();
    //$('#mostrartodo_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Mostrando...');
    DATA_LISTA_INFRACCIONES = "";
    var strHTML = '';


    $.ajax({
        url: URL_LIST_INFRACCIONES,
        dataType: 'json',
        success: function (result) {
            DATA_LISTA_INFRACCIONES = result
            //$('#listar_buses tbody').empty();
            //$('#mostrartodo_btn').prop('disabled', false).html('<i class="fas fa-bus-alt"></i>&nbsp; Mostrar Todo');
            if (result.length <= 0) {
                strHTML += '<tr><td colspan="100" class="text-left" style="padding-left: 50%">No hay información para mostrar</td></tr>';

            } else {

                $.each(result, function (i) {

                    strHTML += '<tr  onclick="seleccionaFila($(this))" ' +
                        ' data-ID_INFRACCION="' + (this.ID_INFRACCION == null ? "" : this.ID_INFRACCION) + '" ' +
                        ' data-ID_PERSONA_INCIDENCIA="' + (this.ID_PERSONA_INCIDENCIA == null ? "" : this.ID_PERSONA_INCIDENCIA) + '" ' +
                        ' data-PERSONA_INCIDENCIA="' + (this.PERSONA_INCIDENCIA == null ? "" : this.PERSONA_INCIDENCIA) + '" ' +
                        ' data-COD_INFRACCION="' + (this.COD_INFRACCION == null ? "" : this.COD_INFRACCION) + '" ' +
                        ' data-DESCRIPCION="' + (this.DESCRIPCION == null ? "" : this.DESCRIPCION) + '" ' +
                        ' data-TIPO_INCIDENCIA="' + (this.TIPO_INCIDENCIA == null ? "" : this.TIPO_INCIDENCIA) + '" ' +
                        ' data-CALIFICACION="' + (this.CALIFICACION == null ? "" : this.CALIFICACION) + '" ' +
                        ' data-MULTA_UIT="' + (this.MULTA_UIT == null ? "" : this.MULTA_UIT) + '" ' +
                        ' data-REINCIDENCIA_UIT="' + (this.REINCIDENCIA_UIT == null ? "" : this.REINCIDENCIA_UIT) + '" ' +
                        ' data-TIPO_INFRACCION="' + (this.TIPO_INFRACCION == null ? "" : this.TIPO_INFRACCION) + '" ' +
                        ' data-SANCION="' + (this.SANCION == null ? "" : this.SANCION) + '" ' +
                         + '>' + '>' +

                        '<td>' + (i + 1) + '</td>' +
                        //(permiso_eliminar == 1 ? '<td>' + '<span class="far fa-trash-alt" aria-hidden="true" style="cursor:pointer;"  onclick="confirmarAnulacionBus(' + this.ID_INFRACCION + ');" ></span>' + '</td>' : '') +
                        '<td class="text-center" >' + (this.PERSONA_INCIDENCIA == null ? "" : this.PERSONA_INCIDENCIA) + '</td>' +
                        '<td class="text-center" >' + (this.COD_INFRACCION == null ? "" : this.COD_INFRACCION) + '</td>' +
                        '<td class="text-center">' + '<button type="button" class="btn btn-primary btn-sm" data-toggle="popover" data-trigger="focus" title="Descripcion" data-content="' + (this.DESCRIPCION == null ? "No hay informacion" : this.DESCRIPCION) + '">' + '<i class="fas fa-eye"></i>' + '</button>' + '</td>' +
                        '<td class="text-center textoAchicado" title="' + (this.TIPO_INCIDENCIA == null ? "" : this.TIPO_INCIDENCIA) + '" >' + (this.TIPO_INCIDENCIA == null ? "" : this.TIPO_INCIDENCIA) + '</td>' +
                        '<td class="text-center" >' + (this.CALIFICACION == null ? "" : this.CALIFICACION) + '</td>' +
                        '<td class="text-center" >' + (this.MULTA_UIT == null ? "" : this.MULTA_UIT) +'%'+ '</td>' +
                        //'<td class="text-center textoAchicado"  title="' + (this.BS_NOM_EMPRE == null ? "" : this.BS_NOM_EMPRE) + '" >' + (this.BS_NOM_EMPRE == null ? "" : this.BS_NOM_EMPRE) + '</td>' +
                        //'<td class="text-center textoAchicado"  title="' + (this.BS_PROPIETARIO == null ? "" : this.BS_PROPIETARIO) + '" >' + (this.BS_PROPIETARIO == null ? "" : this.BS_PROPIETARIO) + '</td>' +
                        '<td class="text-center"  >' + (this.REINCIDENCIA_UIT == null ? "" : this.REINCIDENCIA_UIT) + '%' + '</td>' +
                        '<td class="text-center">' + '<button type="button" class="btn btn-primary btn-sm" data-toggle="popover" data-trigger="focus" title="Motivo" data-content="' + (this.TIPO_INFRACCION == null ? "No hay informacion" : this.TIPO_INFRACCION) + '">' + '<i class="fas fa-eye"></i>' + '</button>' + '</td>' +
                        '<td class="text-center textoAchicado"  title="' + (this.SANCION == null ? "" : this.SANCION) + '" >' + (this.SANCION == null ? "" : this.SANCION).toUpperCase() + '</td>' +
                         (permiso_edit == 1 ? '<td>' + '<span class="far fa-edit" aria-hidden="true" style="cursor:pointer;" onclick="abrirEditarInfraccion($(this).parent().parent());" ></span>' + '</td>' : '') +

                        '</tr>';

                });

            }
            $('#listar_infracciones').append(strHTML);
            util.activarEnumeradoTabla('#Tbinfraccion', $('#btnBusquedaEnTabla'));

            $('[data-toggle="popover"]').popover({
                placement: 'right'
            });

        },

    }, JSON);

}


var tableDataExportar = [{
    "sheetName": "Hoja",
    "data": []
}];

function exportarTabla() {

    $('#exportar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Exportando...');
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


    var tHeadExcel = [{ "text": "N°" },
        { "text": "CORREDOR" },
        { "text": "FECHA DE INCIO A PT" },
        { "text": "NOMBRE DE LA EMPRESA" },
        { "text": "PLACA" },
        { "text": "PROPIETARIO" },
        { "text": "PAQUETE CONCESIÓN" },
        { "text": "PAQUETE DONDE BRINDA SERVICIO" },
        { "text": "TIPO_SERVICIO" },
        { "text": "ESTADO" },
        { "text": "MARCA" },
        { "text": "MODELO" },
        { "text": "AÑO FABRICACION" },
        { "text": "COMBUSTIBLE" },
        { "text": "TECNOLOGIA EURO" },
        { "text": "POTENCIA MOTOR" },
        { "text": "SERIE MOTOR" },
        { "text": "SERIE CHASIS" },
        { "text": "COLOR VEHICULO" },
        { "text": "LONGUITUD" },
        { "text": "ASIENTOS" },
        { "text": "AREA PASILLO" },
        { "text": "PESO NETO" },
        { "text": "PESO BRUTO" },
        { "text": "ALTURA" },
        { "text": "ANCHO" },
        { "text": "UTIL" },
        { "text": "PUERTA IZQUIERDA" },
        { "text": "PARTIDA REGISTRAL" },
        { "text": "CODIGO CVS" },
        { "text": "CVS INICIO" },
        { "text": "CVS VIGENCIA" },
        { "text": "SOAT INICIO" },
        { "text": "SOAT VIGENCIA" },
        { "text": "N° PÓLIZA VEHÍCULOS" },
        { "text": "VEHICULOS INICIO" },
        { "text": "VEHICULOS FIN" },
        { "text": "N° POLIZA RESPONSABILIDAD CIVIL" },
        { "text": "RC INICIO" },
        { "text": "RC VIGENCIA" },
        { "text": "N° POLIZA ACCIDENTES PERSONALES COLECTIVOS PARTICULARES" },
        { "text": "APCP INICIO" },
        { "text": "APCP VIGENCIA" },
        { "text": "N° POLIZA MULTIRIESGOS" },
        { "text": "MULTIRESGOS INICIO" },
        { "text": "MULTIRIESGOS VIGENCIA" },
        { "text": "RTV INICIO" },
        { "text": "RTV VIGENCIA" },
        { "text": "INICIO REVISION ANUAL GNV" },
        { "text": "VIGENCIA REVISION ANUAL GNV" },
        { "text": "INICIO REVISION CILINDROS GNV" },
        { "text": "VIGENCIA REVISION CILINDROS GNV" }
    ]

    tableDataExportar[0].data.push(tHeadExcel);

    $.each(DATA_LISTA_BUSES, function (i) {

        var objeto = this;
        var itemArr = [];
        itemArr.push({ "text": (i + 1) },
            { "text": objeto.ABREVIATURA },
            { "text": objeto.BS_FECINI_PT },
            { "text": objeto.BS_NOM_EMPRE },
            { "text": objeto.BS_PLACA },
            { "text": objeto.BS_PROPIETARIO },
            { "text": objeto.BS_PAQUETE_CONCESION },
            { "text": objeto.BS_PAQUETE_SERVICIO },
            { "text": objeto.BS_TIPO_SERVICIO },
            { "text": objeto.BS_ESTADO },
            { "text": objeto.BS_MARCA },
            { "text": objeto.BS_MODELO },
            { "text": objeto.BS_AÑO_FABRICACION },
            { "text": objeto.BS_COMBUSTIBLE },
            { "text": objeto.BS_TECNOLOGIA_EURO },
            { "text": objeto.BS_POTENCIA_MOTOR },
            { "text": objeto.BS_SERIE_MOTOR },
            { "text": objeto.BS_SERIE_CHASIS },
            { "text": objeto.BS_COLOR_VEHICULO },
            { "text": objeto.BS_LONGITUD },
            { "text": objeto.BS_ASIENTOS },
            { "text": objeto.BS_AREA_PASILLO },
            { "text": objeto.BS_PESO_NETO },
            { "text": objeto.BS_PESO_BRUTO },
            { "text": objeto.BS_ALTURA },
            { "text": objeto.BS_ANCHO },
            { "text": objeto.BS_CARGA_UTIL },
            { "text": objeto.BS_PUERTA_IZQUIERDA },
            { "text": objeto.BS_PARTIDA_REGISTRAL },
            { "text": objeto.BS_CODIGO_CVS },
            { "text": objeto.BS_CVS_FEC_INIC },
            { "text": objeto.BS_CVS_FEC_FIN },
            { "text": objeto.BS_SOAT_FEC_INIC },
            { "text": objeto.BS_SOAT_FEC_FIN },
            { "text": objeto.BS_POLIZA_VEHICULOS },
            { "text": objeto.BS_VEHICULOS_INIC },
            { "text": objeto.BS_VEHICULOS_FIN },
            { "text": objeto.BS_POLIZA_CIVIL },
            { "text": objeto.BS_RC_INICIO },
            { "text": objeto.BS_RC_FIN },
            { "text": objeto.BS_POLIZA_ACCI_COLECTIVOS },
            { "text": objeto.BS_APCP_INIC },
            { "text": objeto.BS_APCP_FIN },
            { "text": objeto.BS_POLIZA_MULTIRIESGOS },
            { "text": objeto.BS_MULTIRESGOS_INIC },
            { "text": objeto.BS_MULTIRESGOS_FIN },
            { "text": objeto.BS_RTV_INIC },
            { "text": objeto.BS_RTV_FIN },
            { "text": objeto.BS_REVI_ANUAL_GNV_INIC },
            { "text": objeto.BS_REVI_ANUAL_GNV_FIN },
            { "text": objeto.BS_REVISION_CILINDROS_GNV_INIC },
            { "text": objeto.BS_REVISION_CILINDROS_GNV_FIN }
        )
        tableDataExportar[0].data.push(itemArr);

    });
    Jhxlsx.export(tableDataExportar, options);
    $('#exportar').prop('disabled', false).html('Exportar');
}


function subirArchivoTemp(element) {
    var nombreArchivo = element.val().split("\\").pop();
    var extensionArchivo = nombreArchivo.split('.')[1];
    element.siblings(".custom-file-label").addClass("selected").html(nombreArchivo);
}

function seleccionaFila(elementFila) {
    $.each($('#Tbinfraccion tbody > tr'), function () {
        $(this).css('background-color', '');
    });
    elementFila.css('background-color', '#17a2b838');
}

$('#modal_agregar_infracc .save').click(function (e) {

    e.preventDefault();
    addImage(5);
    $('#modal_agregar_infracc').modal('hide');
    return false;
})

$('#modal_editar_infracc .save').click(function (e) {
    e.preventDefault();
    addImage(5);
    $('#modal_editar_infracc').modal('hide');
    return false;
})

function abrirEditarInfraccion(element) {

    var id_infraccion = element.attr('data-ID_INFRACCION');
    var id_persona_incidencia = element.attr('data-ID_PERSONA_INCIDENCIA');
    var cod_infraccion = element.attr('data-COD_INFRACCION');
    var descripcion = element.attr('data-DESCRIPCION');
    var tipo_incidencia = element.attr('data-TIPO_INCIDENCIA');
    var calificacion = element.attr('data-CALIFICACION');
    var multa_uit = element.attr('data-MULTA_UIT');
    var reincidencia_uit = element.attr('data-REINCIDENCIA_UIT');
    var tipo_infraccion = element.attr('data-TIPO_INFRACCION');
    var sancion = element.attr('data-SANCION');

    $('#TXT_ID_INFRACCION').val(Number(id_infraccion));
    $('#selectEditarPersonaIncidencia').val(Number(id_persona_incidencia));
    $('#TXT_COD_INFRACCION').val(cod_infraccion);
    $('#TXT_DESCRIPCION').val(descripcion);
    $('#TXT_TIPO_INCIDENCIA').val(tipo_incidencia);
    $('#TXT_CALIFICACION').val(calificacion);
    $('#TXT_MULTA_UIT').val(multa_uit);
    $('#TXT_REINCIDENCIA').val(reincidencia_uit);
    $('#TXT_TIPO_INFRACCION').val(tipo_infraccion);
    $('#TXT_SANCION').val(sancion);

    $('#modal_editar_infracc').modal('show');
}

function textAreaAdjust(o) {
    o.style.height = "1px"; o.style.height = (25 + o.scrollHeight) + "px";
}
//function confirmarAnulacionInfraccion(idInfraccion) {
//    Swal.fire({
//        text: "Estas seguro que deseas eliminar el bus ?",
//        type: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Si',
//        cancelButtonText: 'No'
//    }).then((result) => {
//        if (result.value) {
//            $.ajax({
//                url: URL_ANULA_BUS,
//                dataType: 'json',
//                data: {
//                    idInfraccion: idInfraccion
//                },
//                success: function (result) {
//                    getListInfracciones();
//                    Swal.fire({
//                        type: (result.COD_ESTADO == 1 ? 'success' : 'error'),
//                        title: result.DES_ESTADO,
//                        showConfirmButton: false,
//                        //timer: 2000
//                    });
//                }
//            }, JSON);
//        }
//    });
//}