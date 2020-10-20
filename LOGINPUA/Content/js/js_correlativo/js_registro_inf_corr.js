$(document).ready(function () {
    getListInfCorr();
    $("#sectionParametrosAgregarInfCorr").on("submit", function (e) {

        e.preventDefault();

        var ModelInfCorrAgregar = $('#sectionParametrosAgregarInfCorr').serializeObject();
        $('#registrar_infcorr_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');
        $.ajax({
            url: URL_REGISTRAR_INFCORR,
            data: ModelInfCorrAgregar,
            dataType: 'json',
            success: function (result) {
                Swal.fire({
                    type: result.COD_ESTADO == 0 ? 'error' : 'success',
                    title: result.DES_ESTADO,
                    showConfirmButton: false,
                    //timer: 2000
                })
                $('#registrar_infcorr_btn').prop('disabled', false).html('Guardar');

                if (result.COD_ESTADO == 1) {

                    $('#modal_agregar_inf_corr').modal('hide');
                }

                getListInfCorr();

            },
            error: function (xhr, status, error) {

            },

        }, JSON);

    })

    $("#sectionParametrosEditarInfCorr").on("submit", function (e) {

        e.preventDefault();
        var ModelInfCorrEditar = $('#sectionParametrosEditarInfCorr').serializeObject();
        $('#editar_infcorr_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');

        $.ajax({
            url: URL_EDITAR_INFCORR,
            data: ModelInfCorrEditar,
            dataType: 'json',
            success: function (result) {
                Swal.fire({
                    type: result.COD_ESTADO == 0 ? 'error' : 'success',
                    title: result.DES_ESTADO,
                    showConfirmButton: false,
                    //timer: 2000
                })

                $('#editar_infcorr_btn').prop('disabled', false).html('Guardar');
                $('#modal_editar_inf_corr').modal('hide');

                getListInfCorr();

            },
            error: function (xhr, status, error) {

            },

        }, JSON);

    })


    $("#sectionParametrosActualizarInfCorr").on("submit", function (e) {

        e.preventDefault();
        var ModelInfCorrActualizar = $('#sectionParametrosActualizarInfCorr').serializeObject();
        $('#actualizar_inf_corr_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');

        $.ajax({
            url: URL_ACTUALIZAR_INFCORR,
            data: ModelInfCorrActualizar,
            dataType: 'json',
            success: function (result) {
                Swal.fire({
                    type: result.COD_ESTADO == 0 ? 'error' : 'success',
                    title: result.DES_ESTADO,
                    showConfirmButton: false,
                    //timer: 2000
                })

                $('#actualizar_inf_corr_btn').prop('disabled', false).html('Guardar');
                $('#modal_actualizar_inf_corr').modal('hide');

                getListInfCorr();

            },
            error: function (xhr, status, error) {

            },

        }, JSON);

    })

});

$("body").on("click", "#btnRegistrarInfCorr", function () {

    getUltCorr();
    //$("#selectfecharecepcion").datepicker().datepicker("setDate", new Date());

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

function textAreaAdjust(o) {
    o.style.height = "1px"; o.style.height = (25 + o.scrollHeight) + "px";
}


function getListInfCorr() {


    // PERMISOS
    //1= EDITAR || 2==ELIMINAR 
    var permiso_edit = $('#permisoActualiza').val();
    var permiso_eliminar = $('#permisoEliminar').val();


    $('#TbInfCorr tbody').empty();
    //$('#mostrartodo_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Mostrando...');
    DATA_LISTA_INFCORR = "";
    var strHTML = '';

    //var id_ruta_ = $('#selectRutaCorredor').val();
    var class_estado = "";

    $.ajax({
        url: URL_LIST_INFCORR,
        dataType: 'json',
        success: function (result) {
            DATA_LISTA_INFCORR = result

            //$('#listar_buses tbody').empty();
            //$('#mostrartodo_btn').prop('disabled', false).html('<i class="fas fa-bus-alt"></i>&nbsp; Mostrar Todo');
            if (result.length <= 0) {
                strHTML += '<tr><td colspan="100" class="text-left" style="padding-left: 50%">No hay información para mostrar</td></tr>';

            } else {

                $.each(result, function (i) {
                    var estado_informe = this.ESTADO_INFORME

                    if (estado_informe == 'PENDIENTE') {
                        estado_informe = "PENDIENTE"
                        tipo_estado = '<span class="badge badge-pill badge-danger">' + estado_informe + '</span>'
                        class_estado = "style='cursor:pointer;'";
                        habilitar_act = "onclick='abrirActualizarInfCorr($(this).parent().parent());'";
                        habilitar_edit = "onclick='abrirEditarInfCorr($(this).parent().parent());'";

                    }
                    else if (estado_informe == 'FINALIZADO') {
                        estado_informe = "FINALIZADO"
                        tipo_estado = '<span class="badge badge-pill badge-primary" style="background-color:#3981ce;" >' + estado_informe + '</span>'
                        class_estado = "style='cursor:not-allowed;'";
                        habilitar_act = "onclick='event.preventDefault();'";
                        habilitar_edit = "onclick='event.preventDefault();'";

                    }

                    strHTML += '<tr  onclick="seleccionaFila($(this))" ' +
                        ' data-ID_INF_CORR="' + (this.ID_INF_CORR == null ? "" : this.ID_INF_CORR) + '" ' +
                        ' data-PERS_DIRIG="' + (this.PERS_DIRIG == null ? "" : this.PERS_DIRIG) + '" ' +
                        ' data-PERS_RESP="' + (this.PERS_RESP == null ? "" : this.PERS_RESP) + '" ' +
                        ' data-ASUNTO="' + (this.ASUNTO == null ? "" : this.ASUNTO) + '" ' +
                        ' data-REFERENCIA="' + (this.REFERENCIA == null ? "" : this.REFERENCIA) + '" ' +
                        ' data-FECHA_INFORME="' + (this.FECHA_INFORME == null ? "" : this.FECHA_INFORME) + '" ' +
                        ' data-ARCHIVADOR="' + (this.ARCHIVADOR == null ? "" : this.ARCHIVADOR) + '" ' +
                        ' data-FECHA_RECEPCION="' + (this.FECHA_RECEPCION == null ? "" : this.FECHA_RECEPCION) + '" ' + '>' + '>' +

                        '<td>' + (i + 1) + '</td>' +
                        '<td class="text-center" >' + (this.NUM_CORRELATIVO == null ? "" : this.NUM_CORRELATIVO) + '</td>' +
                        '<td class="text-center" >' + (this.FECHA_INFORME == null ? "" : this.FECHA_INFORME) + '</td>' +
                        '<td class="text-center" >' + (this.NOMBRES_USU == null ? "" : this.NOMBRES_USU) + ' ' + (this.APEPAT_USU == null ? "" : this.APEPAT_USU) + ' ' + (this.APEMAT_USU == null ? "" : this.APEMAT_USU) + '</td>' +
                        '<td class="text-center" >' + (this.PERS_DIRIG == null ? "" : this.PERS_DIRIG) + '</td>' +
                        '<td class="text-center" >' + (this.PERS_RESP == null ? "" : this.PERS_RESP) + '</td>' +
                        '<td class="text-center" >' + '<button type="button" class="btn btn-primary btn-sm" data-toggle="popover" data-trigger="focus" title="Asunto" data-content="' + (this.ASUNTO == null ? "No hay informacion" : this.ASUNTO) + '">' + '<i class="fas fa-eye"></i>' + '</button>' + '</td>' +
                        '<td class="text-center" >' + tipo_estado + '</td>' +
                        '<td class="text-center" >' + (this.REFERENCIA == null ? "" : this.REFERENCIA) + '</td>' +
                        '<td class="text-center" >' + (this.FECHA_RECEPCION == null ? "" : this.FECHA_RECEPCION) + '</td>' +
                        '<td class="text-center" >' + (this.ARCHIVADOR == null ? "" : this.ARCHIVADOR) + '</td>' +

                         //editar
                         (permiso_edit == 1 ? '<td>' + '<span class="far fa-edit" aria-hidden="true" ' + class_estado + ' ' + habilitar_edit + ' ></span>' + '</td>' : '') +
                         //actualizar
                         (permiso_edit == 1 ? '<td>' + '<span class="fas fa-sync-alt" aria-hidden="true" ' + class_estado + ' ' + habilitar_act + '></span>' + '</td>' : '') +
                         (permiso_eliminar == 1 ? '<td>' + '<span class="far fa-trash-alt" aria-hidden="true" style="cursor:pointer;"  onclick="confirmarAnulacionInfCorr(' + this.ID_INF_CORR + ');" ></span>' + '</td>' : '') +
                        '</tr>';

                });

            }
            $('#listar_inf_corr').append(strHTML);
            util.activarEnumeradoTabla('#TbInfCorr', $('#btnBusquedaEnTabla'));

            $('[data-toggle="popover"]').popover({
                placement: 'right'
            });

        },


    }, JSON);

}

function confirmarAnulacionInfCorr(idInfCorr) {
    Swal.fire({
        text: "Estas seguro que deseas eliminarlo ?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: URL_ANULA_INFCORR,
                dataType: 'json',
                data: {
                    idInfCorr: idInfCorr
                },
                success: function (result) {
                    getListInfCorr();
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

function abrirEditarInfCorr(element) {

    var id_infcorr = element.attr('data-ID_INF_CORR');
    var pers_dirig = element.attr('data-PERS_DIRIG');
    var pers_resp = element.attr('data-PERS_RESP');
    var asunto = element.attr('data-ASUNTO');
    var referencia = element.attr('data-REFERENCIA');
    var archivador = element.attr('data-ARCHIVADOR');
    
 

    $('#txt_infcorr').val(Number(id_infcorr));
    $('#txt_edit_pers_dirig').val(pers_dirig);
    $('#txt_edit_pers_resp').val(pers_resp);
    $('#txt_edit_asunto').val(asunto);
    $('#txt_edit_referencia').val(referencia);
    $('#txt_edit_archivador').val(archivador);

    $('#modal_editar_inf_corr').modal('show');
}

function abrirActualizarInfCorr(element) {

    var id_infcorr = element.attr('data-ID_INF_CORR');
    $('#txt_act_infcorr').val(Number(id_infcorr));

    $("#selectActualizarfechaRecepcion").datepicker().datepicker("setDate", new Date());

    $('#modal_actualizar_inf_corr').modal('show');
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
        fileName: "Reporte Informe Correlativo " + today
    };


    var tHeadExcel = [{ "text": "N°" },
        { "text": "Nº DE INFORME" },
        { "text": "FECHA INFORME" },
        { "text": "PERSONA ASIGNADA" },
        { "text": "PERSONA DIRIGIDA" },
        { "text": "PERSONA RESPONSABLE" },
        { "text": "ASUNTO" },
        { "text": "REFERENCIA" },
        { "text": "FECHA RECEPCION" },
        { "text": "ARCHIVADOR" },

    ]

    tableDataExportar[0].data.push(tHeadExcel);

    $.each(DATA_LISTA_INFCORR, function (i) {


        var nom_usu = this.NOMBRES_USU;
        var apepat_usu = this.APEPAT_USU;
        var apemat_usu = this.APEMAT_USU;
        var nombre_usuario = nom_usu + ' ' + apepat_usu + ' ' + apemat_usu;

        var objeto = this;
        var itemArr = [];
        itemArr.push({ "text": (i + 1) },
            { "text": objeto.NUM_CORRELATIVO },
            { "text": objeto.FECHA_INFORME },
            { "text": nombre_usuario },
            { "text": objeto.PERS_DIRIG },
            { "text": objeto.PERS_RESP },
            { "text": objeto.ASUNTO },
            { "text": objeto.REFERENCIA },
            { "text": objeto.FECHA_RECEPCION },
            { "text": objeto.ARCHIVADOR }

        )
        tableDataExportar[0].data.push(itemArr);

    });
    Jhxlsx.export(tableDataExportar, options);
    $('#exportar').prop('disabled', false).html('Exportar');
}

function getUltCorr() {

    $('#ult_corr').empty();
    DATA_ULT_INFCORR = "";
    var strHTML = '';

    $.ajax({
        url: URL_ULT_INF_CORR,
        dataType: 'json',
        success: function (result) {
            DATA_ULT_INFCORR = result

            console.log(result, "res");

            //$('#listar_buses tbody').empty();
            //$('#mostrartodo_btn').prop('disabled', false).html('<i class="fas fa-bus-alt"></i>&nbsp; Mostrar Todo');
            if (result.length <= 0) {
                strHTML += '<tr><td colspan="100" class="text-left" style="padding-left: 50%">###</td></tr>';

            } else {
                $.each(result, function () {
                    var ult_corr = this.ULT_NUM_CORRELATIVO+1;
                    $('#ult_corr').val(Number(ult_corr));
                });
            }
        },

    }, JSON);

}


$('#modal_agregar_inf_corr .save').click(function (e) {
    e.preventDefault();
    addImage(5);
    $('#modal_agregar_inf_corr').modal('hide');
    return false;
})

$('#modal_editar_inf_corr .save').click(function (e) {
    e.preventDefault();
    addImage(5);
    $('#modal_editar_inf_corr').modal('hide');
    return false;
})
function seleccionaFila(elementFila) {
    $.each($('#TbInfCorr tbody > tr'), function () {
        $(this).css('background-color', '');
    });
    elementFila.css('background-color', '#17a2b838');
}