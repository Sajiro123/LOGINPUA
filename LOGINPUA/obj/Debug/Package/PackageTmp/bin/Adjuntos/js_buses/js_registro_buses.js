$(document).ready(function () {

    getIdCorredor();
    getRutaCorredor();


    $("#formSubirExcelBuses").on("submit", function (e) {

        e.preventDefault();

        if ($('#archivoSubido').val().length == 0) {
            Swal.fire({
                type: 'info',
                title: "Debe cargar un archivo para enviar la información.",
                showConfirmButton: false,
                //timer: 2000
            });
            return false;
        }
        var formularioDatos = new FormData(document.getElementById("formSubirExcelBuses"));
        $('#registrar_excel_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Subiendo...');
        $.ajax({
            url: URL_ACTION_UPLOAD_FILE,
            type: "post",
            dataType: "json",
            data: formularioDatos,
            cache: false,
            contentType: false,
            processData: false
        }).done(function (respuesta) {
            ACTION_REEMPLAZAR_DATOS = false;
            $('#registrar_excel_btn').prop('disabled', false).html('Subir archivo');
            Swal.fire({
                type: respuesta.COD_ESTADO == 1 ? 'success' : 'error',
                title: respuesta.DES_ESTADO,
                showConfirmButton: false,
                //timer: 2000
            });

            if (respuesta.COD_ESTADO == 1) {
                $('#myModal ').modal('hide');

                getListBuses();

            }
        });
    })

    $("#sectionParametros").on("submit", function (e) {

        e.preventDefault();

        if ($('#BS_PLACA').val().length == 0) {
            Swal.fire({
                type: 'info',
                title: "Debe llenar los campos obligatorios para realizar el registro.",
                showConfirmButton: false,
            });
            return false;
        }

        var ModelBus = $('#sectionParametros').serializeObject();
        $('#registrar_indiv_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');

        $.ajax({
            url: URL_REGISTRAR_BUS_INDIV,
            data: ModelBus,
            dataType: 'json',
            success: function (result) {
                Swal.fire({
                    type: result.COD_ESTADO == 0 ? 'error' : 'success',
                    title: result.DES_ESTADO,
                    showConfirmButton: false,
                    //timer: 2000
                })
                $('#registrar_indiv_btn').prop('disabled', false).html('Guardar');

                if (result.COD_ESTADO == 1) {
                    $('#myModal').modal('hide');
                }


                var id_buses = $('#selectAbrevCorredor').val();
                getListBusesbyId(id_buses);

            },
            error: function (xhr, status, error) {

            },

        }, JSON);

    })

    $("#sectionParametrosEditar").on("submit", function (e) {

        e.preventDefault();
        var ModelBus = $('#sectionParametrosEditar').serializeObject();

        $.ajax({
            url: URL_MODIFICA_BUS,
            data: ModelBus,
            dataType: 'json',
            success: function (result) {
                Swal.fire({
                    type: result.COD_ESTADO == 0 ? 'error' : 'success',
                    title: result.DES_ESTADO,
                    showConfirmButton: false,
                    //timer: 2000
                })
                var id_buses = $('#selectRutaCorredor').val();

                getListBusesbyId(id_buses)
                $('#ModalEditar').modal('hide');

            },
            error: function (xhr, status, error) {

            },

        }, JSON);

    })
});




var BS_ID_AFECTADO = 0;


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

function modal_indiv() {
    jQuery('#modal-dialog').removeClass();
    jQuery('#modal-dialog').addClass('modal-dialog modal-xl')

}

function modal_Masivo() {
    jQuery('#modal-dialog').removeClass();
    jQuery('#modal-dialog').addClass('modal-dialog modal-LG')
}


function getRutaCorredor() {
    $.ajax({
        url: URL_LIST_CORREDORES,
        dataType: 'json',
        success: function (result) {

            $.each(result, function () {
                $('#selectRutaCorredor').append('<option value="' + this.ID_CORREDOR + '">' + this.CORREDOR_NOMBRE + '</option>');
            });
            var id_buses = $('#selectRutaCorredor').val();
            getListBusesbyId(id_buses);
        }
    }, JSON);
}


function getIdCorredor() {
    $.ajax({
        url: URL_LIST_CORREDORES,
        dataType: 'json',
        success: function (result) {

            $.each(result, function () {
                $('#selectAbrevCorredor').append('<option value="' + this.ID_CORREDOR + '">' + this.ABREVIATURA + '</option>');
                $('#selectAbrevCorredorEditar').append('<option value="' + this.ID_CORREDOR + '">' + this.ABREVIATURA + '</option>');


            })

        }
    }, JSON);
}




function Semaforo_Logica(fecha_colum) {

    if (fecha_colum != null) {
        var fecha_parametro = new Date(fecha_colum.split('/')[1] + "/" + fecha_colum.split('/')[0] + "/" + fecha_colum.split('/')[2])
        var fecha_parametro_comp = fecha_parametro.setHours(0, 0, 0, 0);
        var color_semaforo = "";
        var hoy = new Date();
        var fecha_hoy = hoy.setHours(0, 0, 0, 0);

        var fecha_colum_mes = new Date(fecha_parametro.getFullYear(), fecha_parametro.getMonth(), fecha_parametro.getDate());
        fecha_colum_mes.setHours(0, 0, 0, 0);

        var fecha_mas_mes = new Date(hoy.getFullYear(), hoy.getMonth() + 1, hoy.getDate());
        fecha_mas_mes.setHours(0, 0, 0, 0);

        var ult_dia_mes = new Date(hoy.getFullYear(), hoy.getMonth() + 1, 0);

        if (fecha_parametro_comp < fecha_hoy) {
            color_semaforo = '<div title="' + fecha_colum + '" style="background: rgb(191, 34, 0);width: 30px;height: 30px;border: 1px solid black;margin: auto;border-radius: 50%;">' + '</div>';
        } else if (ult_dia_mes >= fecha_parametro_comp || fecha_colum_mes <= fecha_mas_mes) {
            color_semaforo = '<div title="' + fecha_colum + '" style="background: rgb(247, 255, 0);width: 30px;height: 30px;border: 1px solid black;margin: auto;border-radius: 50%;">' + '</div>';
        } else if (fecha_parametro_comp > fecha_hoy) {
            color_semaforo = '<div title="' + fecha_colum + '" style="background: rgb(48, 207, 92);width: 30px;height: 30px;border: 1px solid black;margin: auto;border-radius: 50%;">' + '</div>';
        }

        return color_semaforo;
    } else {

        return '';
    }
}
var DATA_LISTA_BUSES = [];


function getListBuses() {


    // PERMISOS
    //1= EDITAR || 2==ELIMINAR 
    var permiso_edit = $('#permisoActualiza').val();
    var permiso_eliminar = $('#permisoEliminar').val();



    $('#Tbbuses tbody').empty();
    $('#mostrartodo_btn').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Mostrando...');
    DATA_LISTA_BUSES = "";
    var strHTML = '';


    $.ajax({
        url: URL_LIST_BUSES,
        dataType: 'json',
        success: function (result) {
            DATA_LISTA_BUSES = result
            //$('#listar_buses tbody').empty();
            $('#mostrartodo_btn').prop('disabled', false).html('<i class="fas fa-bus-alt"></i>&nbsp; Mostrar Todo');
            if (result.length <= 0) {
                strHTML += '<tr><td colspan="100" class="text-left" style="padding-left: 15%">No hay información para mostrar</td></tr>';

            } else {

                $.each(result, function (i) {


                    var BS_SOAT_FEC_FIN = Semaforo_Logica(this.BS_SOAT_FEC_FIN);
                    var BS_RTV_FIN = Semaforo_Logica(this.BS_RTV_FIN);
                    var BS_RC_FIN = Semaforo_Logica(this.BS_RC_FIN);
                    var BS_REVI_ANUAL_GNV_FIN = Semaforo_Logica(this.BS_REVI_ANUAL_GNV_FIN);
                    var BS_REVISION_CILINDROS_GNV_FIN = Semaforo_Logica(this.BS_REVISION_CILINDROS_GNV_FIN);
                    var BS_VEHICULOS_FIN = Semaforo_Logica(this.BS_VEHICULOS_FIN);
                    var BS_CVS_FEC_FIN = Semaforo_Logica(this.BS_CVS_FEC_FIN);
                    var BS_APCP_FIN = Semaforo_Logica(this.BS_APCP_FIN);
                    var BS_MULTIRESGOS_FIN = Semaforo_Logica(this.BS_MULTIRESGOS_FIN);


                    strHTML += '<tr  onclick="seleccionaFila($(this))" ' +
                        ' data-BS_ID="' + (this.BS_ID == null ? "" : this.BS_ID) + '" ' +
                        ' data-ID_CORREDOR="' + (this.ID_CORREDOR == null ? "" : this.ID_CORREDOR) + '" ' +
                        ' data-BS_FECINI_PT="' + (this.BS_FECINI_PT == null ? "" : this.BS_FECINI_PT) + '" ' +
                        ' data-BS_NOM_EMPRE="' + (this.BS_NOM_EMPRE == null ? "" : this.BS_NOM_EMPRE) + '" ' +
                        ' data-BS_PLACA="' + (this.BS_PLACA == null ? "" : this.BS_PLACA) + '" ' +
                        ' data-BS_PROPIETARIO="' + (this.BS_PROPIETARIO == null ? "" : this.BS_PROPIETARIO) + '" ' +
                        ' data-BS_PAQUETE_CONCESION="' + (this.BS_PAQUETE_CONCESION == null ? "" : this.BS_PAQUETE_CONCESION) + '" ' +
                        ' data-BS_PAQUETE_SERVICIO="' + (this.BS_PAQUETE_SERVICIO == null ? "" : this.BS_PAQUETE_SERVICIO) + '" ' +
                        ' data-BS_TIPO_SERVICIO="' + (this.BS_TIPO_SERVICIO == null ? "" : this.BS_TIPO_SERVICIO) + '" ' +
                        ' data-BS_ESTADO="' + (this.BS_ESTADO == null ? "" : this.BS_ESTADO) + '" ' +
                        ' data-BS_MARCA="' + (this.BS_MARCA == null ? "" : this.BS_MARCA) + '" ' +
                        ' data-BS_MODELO="' + (this.BS_MODELO == null ? "" : this.BS_MODELO) + '" ' +
                        ' data-BS_AÑO_FABRICACION="' + (this.BS_AÑO_FABRICACION == null ? "" : this.BS_AÑO_FABRICACION) + '" ' +
                        ' data-BS_COMBUSTIBLE="' + (this.BS_COMBUSTIBLE == null ? "" : this.BS_COMBUSTIBLE) + '" ' +
                        ' data-BS_TECNOLOGIA_EURO="' + (this.BS_TECNOLOGIA_EURO == null ? "" : this.BS_TECNOLOGIA_EURO) + '" ' +
                        ' data-BS_POTENCIA_MOTOR="' + (this.BS_POTENCIA_MOTOR == null ? "" : this.BS_POTENCIA_MOTOR) + '" ' +
                        ' data-BS_SERIE_MOTOR="' + (this.BS_SERIE_MOTOR == null ? "" : this.BS_SERIE_MOTOR) + '" ' +
                        ' data-BS_SERIE_CHASIS="' + (this.BS_SERIE_CHASIS == null ? "" : this.BS_SERIE_CHASIS) + '" ' +
                        ' data-BS_COLOR_VEHICULO="' + (this.BS_COLOR_VEHICULO == null ? "" : this.BS_COLOR_VEHICULO) + '" ' +
                        ' data-BS_LONGITUD="' + (this.BS_LONGITUD == null ? "" : this.BS_LONGITUD) + '" ' +
                        ' data-BS_ASIENTOS="' + (this.BS_ASIENTOS == null ? "" : this.BS_ASIENTOS) + '" ' +
                        ' data-BS_AREA_PASILLO="' + (this.BS_AREA_PASILLO == null ? "" : this.BS_AREA_PASILLO) + '" ' +
                        ' data-BS_PESO_NETO="' + (this.BS_PESO_NETO == null ? "" : this.BS_PESO_NETO) + '" ' +
                        ' data-BS_PESO_BRUTO="' + (this.BS_PESO_BRUTO == null ? "" : this.BS_PESO_BRUTO) + '" ' +
                        ' data-BS_ALTURA="' + (this.BS_ALTURA == null ? "" : this.BS_ALTURA) + '" ' +
                        ' data-BS_ANCHO="' + (this.BS_ANCHO == null ? "" : this.BS_ANCHO) + '" ' +
                        ' data-BS_CARGA_UTIL="' + (this.BS_CARGA_UTIL == null ? "" : this.BS_CARGA_UTIL) + '" ' +
                        ' data-BS_PUERTA_IZQUIERDA="' + (this.BS_PUERTA_IZQUIERDA == null ? "" : this.BS_PUERTA_IZQUIERDA) + '" ' +
                        ' data-BS_PARTIDA_REGISTRAL="' + (this.BS_PARTIDA_REGISTRAL == null ? "" : this.BS_PARTIDA_REGISTRAL) + '" ' +
                        ' data-BS_CODIGO_CVS="' + (this.BS_CODIGO_CVS == null ? "" : this.BS_CODIGO_CVS) + '" ' +
                        ' data-BS_CVS_FEC_INIC="' + (this.BS_CVS_FEC_INIC == null ? "" : this.BS_CVS_FEC_INIC) + '" ' +
                        ' data-BS_CVS_FEC_FIN="' + (this.BS_CVS_FEC_FIN == null ? "" : this.BS_CVS_FEC_FIN) + '" ' +
                        ' data-BS_SOAT_FEC_INIC="' + (this.BS_SOAT_FEC_INIC == null ? "" : this.BS_SOAT_FEC_INIC) + '" ' +
                        ' data-BS_SOAT_FEC_FIN="' + (this.BS_SOAT_FEC_FIN == null ? "" : this.BS_SOAT_FEC_FIN) + '" ' +
                        ' data-BS_POLIZA_VEHICULOS="' + (this.BS_POLIZA_VEHICULOS == null ? "" : this.BS_POLIZA_VEHICULOS) + '" ' +
                        ' data-BS_VEHICULOS_INIC="' + (this.BS_VEHICULOS_INIC == null ? "" : this.BS_VEHICULOS_INIC) + '" ' +
                        ' data-BS_VEHICULOS_FIN="' + (this.BS_VEHICULOS_FIN == null ? "" : this.BS_VEHICULOS_FIN) + '" ' +
                        ' data-BS_POLIZA_CIVIL="' + (this.BS_POLIZA_CIVIL == null ? "" : this.BS_POLIZA_CIVIL) + '" ' +
                        ' data-BS_RC_INICIO="' + (this.BS_RC_INICIO == null ? "" : this.BS_RC_INICIO) + '" ' +
                        ' data-BS_RC_FIN="' + (this.BS_RC_FIN == null ? "" : this.BS_RC_FIN) + '" ' +
                        ' data-BS_POLIZA_ACCI_COLECTIVOS="' + (this.BS_POLIZA_ACCI_COLECTIVOS == null ? "" : this.BS_POLIZA_ACCI_COLECTIVOS) + '" ' +
                        ' data-BS_APCP_INIC="' + (this.BS_APCP_INIC == null ? "" : this.BS_APCP_INIC) + '" ' +
                        ' data-BS_APCP_FIN="' + (this.BS_APCP_FIN == null ? "" : this.BS_APCP_FIN) + '" ' +
                        ' data-BS_POLIZA_MULTIRIESGOS="' + (this.BS_POLIZA_MULTIRIESGOS == null ? "" : this.BS_POLIZA_MULTIRIESGOS) + '" ' +
                        ' data-BS_MULTIRESGOS_INIC="' + (this.BS_MULTIRESGOS_INIC == null ? "" : this.BS_MULTIRESGOS_INIC) + '" ' +
                        ' data-BS_MULTIRESGOS_FIN="' + (this.BS_MULTIRESGOS_FIN == null ? "" : this.BS_MULTIRESGOS_FIN) + '" ' +
                        ' data-BS_RTV_INIC="' + (this.BSBS_RTV_INIC_ID == null ? "" : this.BS_RTV_INIC) + '" ' +
                        ' data-BS_RTV_FIN="' + (this.BS_RTV_FIN == null ? "" : this.BS_RTV_FIN) + '" ' +
                        ' data-BS_REVI_ANUAL_GNV_INIC="' + (this.BS_REVI_ANUAL_GNV_INIC == null ? "" : this.BS_REVI_ANUAL_GNV_INIC) + '" ' +
                        ' data-BS_REVI_ANUAL_GNV_FIN="' + (this.BS_REVI_ANUAL_GNV_FIN == null ? "" : this.BS_REVI_ANUAL_GNV_FIN) + '" ' +
                        ' data-BS_REVISION_CILINDROS_GNV_INIC="' + (this.BS_REVISION_CILINDROS_GNV_INIC == null ? "" : this.BS_REVISION_CILINDROS_GNV_INIC) + '" ' +
                        ' data-BS_REVISION_CILINDROS_GNV_FIN="' + (this.BS_REVISION_CILINDROS_GNV_FIN == null ? "" : this.BS_REVISION_CILINDROS_GNV_FIN) + '" ' + '>' + '>' +

                        '<td>' + (i + 1) + '</td>' +
                      (permiso_eliminar == 1 ? '<td>' + '<span class="far fa-trash-alt" aria-hidden="true" style="cursor:pointer;"  onclick="confirmarAnulacionBus(' + this.BS_ID + ');" ></span>' + '</td>' : '') +
                       (permiso_eliminar == 1 ? '<td>' + '<span class="fas fa-ban" aria-hidden="true" style="cursor:pointer;" onclick="confirmarDesafectacionBus($(this).parent().parent(), $(this), ' + this.BS_ID + ')"></span>' + '</td>' : '') +
                        (permiso_edit == 1 ? '<td>' + '<span class="far fa-edit" aria-hidden="true" style="cursor:pointer;" onclick="abrirEditarBus($(this).parent().parent());" ></span>' + '</td>' : '') +
                        '<td class="text-center" >' + (this.ABREVIATURA == null ? "" : this.ABREVIATURA) + '</td>' +
                        '<td class="text-center textoAchicado" >' + (this.BS_PLACA == null ? "" : this.BS_PLACA) + '</td>' +
                        '<td class="text-center"  >' + BS_SOAT_FEC_FIN + '</td>' +
                        '<td class="text-center" >' + BS_RTV_FIN + '</td>' +
                        '<td class="text-center" >' + BS_RC_FIN + '</td>' +
                        '<td class="text-center" >' + BS_REVI_ANUAL_GNV_FIN + '</td>' +
                        '<td class="text-center" >' + BS_REVISION_CILINDROS_GNV_FIN + '</td>' +
                        '<td class="text-center" >' + BS_VEHICULOS_FIN + '</td>' +
                        '<td class="text-center" >' + BS_CVS_FEC_FIN + '</td>' +
                        '<td class="text-center" >' + (this.BS_AÑO_FABRICACION == null ? "" : this.BS_AÑO_FABRICACION) + '</td>' +
                        '<td class="text-center" >' + (this.BS_COMBUSTIBLE == null ? "" : this.BS_COMBUSTIBLE) + '</td>' +
                        '<td class="text-center" >' + (this.BS_FECINI_PT == null ? "" : this.BS_FECINI_PT) + '</td>' +
                        '<td class="text-center textoAchicado"  title="' + (this.BS_NOM_EMPRE == null ? "" : this.BS_NOM_EMPRE) + '" >' + (this.BS_NOM_EMPRE == null ? "" : this.BS_NOM_EMPRE) + '</td>' +
                        '<td class="text-center textoAchicado"  title="' + (this.BS_PROPIETARIO == null ? "" : this.BS_PROPIETARIO) + '" >' + (this.BS_PROPIETARIO == null ? "" : this.BS_PROPIETARIO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_PAQUETE_CONCESION == null ? "" : this.BS_PAQUETE_CONCESION) + '</td>' +
                        '<td class="text-center" >' + (this.BS_PAQUETE_SERVICIO == null ? "" : this.BS_PAQUETE_SERVICIO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_TIPO_SERVICIO == null ? "" : this.BS_TIPO_SERVICIO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_ESTADO == null ? "" : this.BS_ESTADO) + '</td>' +
                        '<td class="text-center textoAchicado" >' + (this.BS_MARCA == null ? "" : this.BS_MARCA) + '</td>' +
                        '<td class="text-center textoAchicado" >' + (this.BS_MODELO == null ? "" : this.BS_MODELO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_TECNOLOGIA_EURO == null ? "" : this.BS_TECNOLOGIA_EURO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_POTENCIA_MOTOR == null ? "" : this.BS_POTENCIA_MOTOR) + '</td>' +
                        '<td class="text-center" >' + (this.BS_SERIE_MOTOR == null ? "" : this.BS_SERIE_MOTOR) + '</td>' +
                        '<td class="text-center" >' + (this.BS_SERIE_CHASIS == null ? "" : this.BS_SERIE_CHASIS) + '</td>' +
                        '<td class="text-center textoAchicado" >' + (this.BS_COLOR_VEHICULO == null ? "" : this.BS_COLOR_VEHICULO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_LONGITUD == null ? "" : this.BS_LONGITUD) + '</td>' +
                        '<td class="text-center" >' + (this.BS_ASIENTOS == null ? "" : this.BS_ASIENTOS) + '</td>' +
                        '<td class="text-center" >' + (this.BS_AREA_PASILLO == null ? "" : this.BS_AREA_PASILLO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_PESO_NETO == null ? "" : this.BS_PESO_NETO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_PESO_BRUTO == null ? "" : this.BS_PESO_BRUTO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_ALTURA == null ? "" : this.BS_ALTURA) + '</td>' +
                        '<td class="text-center" >' + (this.BS_ANCHO == null ? "" : this.BS_ANCHO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_CARGA_UTIL == null ? "" : this.BS_CARGA_UTIL) + '</td>' +
                        '<td class="text-center" >' + (this.BS_PUERTA_IZQUIERDA == null ? "" : this.BS_PUERTA_IZQUIERDA) + '</td>' +
                        '<td class="text-center" >' + (this.BS_PARTIDA_REGISTRAL == null ? "" : this.BS_PARTIDA_REGISTRAL) + '</td>' +
                        '<td class="text-center" >' + (this.BS_CODIGO_CVS == null ? "" : this.BS_CODIGO_CVS) + '</td>' +
                        '<td class="text-center" >' + (this.BS_CVS_FEC_INIC == null ? "" : this.BS_CVS_FEC_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_CVS_FEC_FIN == null ? "" : this.BS_CVS_FEC_FIN) + '</td>' +
                        '<td class="text-center" >' + (this.BS_SOAT_FEC_INIC == null ? "" : this.BS_SOAT_FEC_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_SOAT_FEC_FIN == null ? "" : this.BS_SOAT_FEC_FIN) + '</td>' +
                        '<td class="text-center textoAchicado" >' + (this.BS_POLIZA_VEHICULOS == null ? "" : this.BS_POLIZA_VEHICULOS) + '</td>' +
                        '<td class="text-center" >' + (this.BS_VEHICULOS_INIC == null ? "" : this.BS_VEHICULOS_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_VEHICULOS_FIN == null ? "" : this.BS_VEHICULOS_FIN) + '</td>' +
                        '<td class="text-center" >' + (this.BS_POLIZA_CIVIL == null ? "" : this.BS_POLIZA_CIVIL) + '</td>' +
                        '<td class="text-center" >' + (this.BS_RC_INICIO == null ? "" : this.BS_RC_INICIO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_RC_FIN == null ? "" : this.BS_RC_FIN) + '</td>' +
                        '<td class="text-center" >' + (this.BS_POLIZA_ACCI_COLECTIVOS == null ? "" : this.BS_POLIZA_ACCI_COLECTIVOS) + '</td>' +
                        '<td class="text-center" >' + (this.BS_APCP_INIC == null ? "" : this.BS_APCP_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_APCP_FIN == null ? "" : this.BS_APCP_FIN) + '</td>' +

                        '<td class="text-center" >' + BS_APCP_FIN + '</td>' +

                        '<td class="text-center" >' + (this.BS_POLIZA_MULTIRIESGOS == null ? "" : this.BS_POLIZA_MULTIRIESGOS) + '</td>' +
                        '<td class="text-center" >' + (this.BS_MULTIRESGOS_INIC == null ? "" : this.BS_MULTIRESGOS_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_MULTIRESGOS_FIN == null ? "" : this.BS_MULTIRESGOS_FIN) + '</td>' +

                        '<td class="text-center" >' + BS_MULTIRESGOS_FIN + '</td>' +

                        '<td class="text-center" >' + (this.BS_RTV_INIC == null ? "" : this.BS_RTV_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_RTV_FIN == null ? "" : this.BS_RTV_FIN) + '</td>' +
                        '<td class="text-center" >' + (this.BS_REVI_ANUAL_GNV_INIC == null ? "" : this.BS_REVI_ANUAL_GNV_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_REVI_ANUAL_GNV_FIN == null ? "" : this.BS_REVI_ANUAL_GNV_FIN) + '</td>' +
                        '<td class="text-center" >' + (this.BS_REVISION_CILINDROS_GNV_INIC == null ? "" : this.BS_REVISION_CILINDROS_GNV_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_REVISION_CILINDROS_GNV_FIN == null ? "" : this.BS_REVISION_CILINDROS_GNV_FIN) + '</td>' +

                        '</tr>';

                });

            }
            $('#listar_buses').append(strHTML);
            util.activarEnumeradoTabla('#Tbbuses', $('#btnBusquedaEnTabla'));


        },

    }, JSON);

}

function getListBusesbyId(id) {

    // PERMISOS
    //1= EDITAR || 2==ELIMINAR 
    var permiso_edit = $('#permisoActualiza').val();
    var permiso_eliminar = $('#permisoEliminar').val();

    $('#Tbbuses tbody').empty();
    DATA_LISTA_BUSES = "";

    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Cargando...');
    if (typeof id == 'undefined') {
        id = $('#selectRutaCorredor').val();

    }
    var strHTML = '';
    $.ajax({
        url: URL_LIST_BUSES_BY_ID,
        data: {
            idCorredor: id
        },
        dataType: 'json',
        success: function (result) {
            DATA_LISTA_BUSES = result;

            if (result.length <= 0) {
                strHTML += '<tr><td colspan="100" class="text-left" style="padding-left: 15%">No hay información para mostrar</td></tr>';

            } else {
                $.each(result, function (i) {
                    var BS_SOAT_FEC_FIN = Semaforo_Logica(this.BS_SOAT_FEC_FIN);
                    var BS_RTV_FIN = Semaforo_Logica(this.BS_RTV_FIN);
                    var BS_RC_FIN = Semaforo_Logica(this.BS_RC_FIN);
                    var BS_REVI_ANUAL_GNV_FIN = Semaforo_Logica(this.BS_REVI_ANUAL_GNV_FIN);
                    var BS_REVISION_CILINDROS_GNV_FIN = Semaforo_Logica(this.BS_REVISION_CILINDROS_GNV_FIN);
                    var BS_VEHICULOS_FIN = Semaforo_Logica(this.BS_VEHICULOS_FIN);
                    var BS_CVS_FEC_FIN = Semaforo_Logica(this.BS_CVS_FEC_FIN);
                    var BS_APCP_FIN = Semaforo_Logica(this.BS_APCP_FIN);
                    var BS_MULTIRESGOS_FIN = Semaforo_Logica(this.BS_MULTIRESGOS_FIN);


                    strHTML += '<tr  onclick="seleccionaFila($(this))" ' +

                        ' data-BS_ID="' + (this.BS_ID == null ? "" : this.BS_ID) + '" ' +
                        ' data-ID_CORREDOR="' + (this.ID_CORREDOR == null ? "" : this.ID_CORREDOR) + '" ' +
                        ' data-BS_FECINI_PT="' + (this.BS_FECINI_PT == null ? "" : this.BS_FECINI_PT) + '" ' +
                        ' data-BS_NOM_EMPRE="' + (this.BS_NOM_EMPRE == null ? "" : this.BS_NOM_EMPRE) + '" ' +
                        ' data-BS_PLACA="' + (this.BS_PLACA == null ? "" : this.BS_PLACA) + '" ' +
                        ' data-BS_PROPIETARIO="' + (this.BS_PROPIETARIO == null ? "" : this.BS_PROPIETARIO) + '" ' +
                        ' data-BS_PAQUETE_CONCESION="' + (this.BS_PAQUETE_CONCESION == null ? "" : this.BS_PAQUETE_CONCESION) + '" ' +
                        ' data-BS_PAQUETE_SERVICIO="' + (this.BS_PAQUETE_SERVICIO == null ? "" : this.BS_PAQUETE_SERVICIO) + '" ' +
                        ' data-BS_TIPO_SERVICIO="' + (this.BS_TIPO_SERVICIO == null ? "" : this.BS_TIPO_SERVICIO) + '" ' +
                        ' data-BS_ESTADO="' + (this.BS_ESTADO == null ? "" : this.BS_ESTADO) + '" ' +
                        ' data-BS_MARCA="' + (this.BS_MARCA == null ? "" : this.BS_MARCA) + '" ' +
                        ' data-BS_MODELO="' + (this.BS_MODELO == null ? "" : this.BS_MODELO) + '" ' +
                        ' data-BS_AÑO_FABRICACION="' + (this.BS_AÑO_FABRICACION == null ? "" : this.BS_AÑO_FABRICACION) + '" ' +
                        ' data-BS_COMBUSTIBLE="' + (this.BS_COMBUSTIBLE == null ? "" : this.BS_COMBUSTIBLE) + '" ' +
                        ' data-BS_TECNOLOGIA_EURO="' + (this.BS_TECNOLOGIA_EURO == null ? "" : this.BS_TECNOLOGIA_EURO) + '" ' +
                        ' data-BS_POTENCIA_MOTOR="' + (this.BS_POTENCIA_MOTOR == null ? "" : this.BS_POTENCIA_MOTOR) + '" ' +
                        ' data-BS_SERIE_MOTOR="' + (this.BS_SERIE_MOTOR == null ? "" : this.BS_SERIE_MOTOR) + '" ' +
                        ' data-BS_SERIE_CHASIS="' + (this.BS_SERIE_CHASIS == null ? "" : this.BS_SERIE_CHASIS) + '" ' +
                        ' data-BS_COLOR_VEHICULO="' + (this.BS_COLOR_VEHICULO == null ? "" : this.BS_COLOR_VEHICULO) + '" ' +
                        ' data-BS_LONGITUD="' + (this.BS_LONGITUD == null ? "" : this.BS_LONGITUD) + '" ' +
                        ' data-BS_ASIENTOS="' + (this.BS_ASIENTOS == null ? "" : this.BS_ASIENTOS) + '" ' +
                        ' data-BS_AREA_PASILLO="' + (this.BS_AREA_PASILLO == null ? "" : this.BS_AREA_PASILLO) + '" ' +
                        ' data-BS_PESO_NETO="' + (this.BS_PESO_NETO == null ? "" : this.BS_PESO_NETO) + '" ' +
                        ' data-BS_PESO_BRUTO="' + (this.BS_PESO_BRUTO == null ? "" : this.BS_PESO_BRUTO) + '" ' +
                        ' data-BS_ALTURA="' + (this.BS_ALTURA == null ? "" : this.BS_ALTURA) + '" ' +
                        ' data-BS_ANCHO="' + (this.BS_ANCHO == null ? "" : this.BS_ANCHO) + '" ' +
                        ' data-BS_CARGA_UTIL="' + (this.BS_CARGA_UTIL == null ? "" : this.BS_CARGA_UTIL) + '" ' +
                        ' data-BS_PUERTA_IZQUIERDA="' + (this.BS_PUERTA_IZQUIERDA == null ? "" : this.BS_PUERTA_IZQUIERDA) + '" ' +
                        ' data-BS_PARTIDA_REGISTRAL="' + (this.BS_PARTIDA_REGISTRAL == null ? "" : this.BS_PARTIDA_REGISTRAL) + '" ' +
                        ' data-BS_CODIGO_CVS="' + (this.BS_CODIGO_CVS == null ? "" : this.BS_CODIGO_CVS) + '" ' +
                        ' data-BS_CVS_FEC_INIC="' + (this.BS_CVS_FEC_INIC == null ? "" : this.BS_CVS_FEC_INIC) + '" ' +
                        ' data-BS_CVS_FEC_FIN="' + (this.BS_CVS_FEC_FIN == null ? "" : this.BS_CVS_FEC_FIN) + '" ' +
                        ' data-BS_SOAT_FEC_INIC="' + (this.BS_SOAT_FEC_INIC == null ? "" : this.BS_SOAT_FEC_INIC) + '" ' +
                        ' data-BS_SOAT_FEC_FIN="' + (this.BS_SOAT_FEC_FIN == null ? "" : this.BS_SOAT_FEC_FIN) + '" ' +
                        ' data-BS_POLIZA_VEHICULOS="' + (this.BS_POLIZA_VEHICULOS == null ? "" : this.BS_POLIZA_VEHICULOS) + '" ' +
                        ' data-BS_VEHICULOS_INIC="' + (this.BS_VEHICULOS_INIC == null ? "" : this.BS_VEHICULOS_INIC) + '" ' +
                        ' data-BS_VEHICULOS_FIN="' + (this.BS_VEHICULOS_FIN == null ? "" : this.BS_VEHICULOS_FIN) + '" ' +
                        ' data-BS_POLIZA_CIVIL="' + (this.BS_POLIZA_CIVIL == null ? "" : this.BS_POLIZA_CIVIL) + '" ' +
                        ' data-BS_RC_INICIO="' + (this.BS_RC_INICIO == null ? "" : this.BS_RC_INICIO) + '" ' +
                        ' data-BS_RC_FIN="' + (this.BS_RC_FIN == null ? "" : this.BS_RC_FIN) + '" ' +
                        ' data-BS_POLIZA_ACCI_COLECTIVOS="' + (this.BS_POLIZA_ACCI_COLECTIVOS == null ? "" : this.BS_POLIZA_ACCI_COLECTIVOS) + '" ' +
                        ' data-BS_APCP_INIC="' + (this.BS_APCP_INIC == null ? "" : this.BS_APCP_INIC) + '" ' +
                        ' data-BS_APCP_FIN="' + (this.BS_APCP_FIN == null ? "" : this.BS_APCP_FIN) + '" ' +
                        ' data-BS_POLIZA_MULTIRIESGOS="' + (this.BS_POLIZA_MULTIRIESGOS == null ? "" : this.BS_POLIZA_MULTIRIESGOS) + '" ' +
                        ' data-BS_MULTIRESGOS_INIC="' + (this.BS_MULTIRESGOS_INIC == null ? "" : this.BS_MULTIRESGOS_INIC) + '" ' +
                        ' data-BS_MULTIRESGOS_FIN="' + (this.BS_MULTIRESGOS_FIN == null ? "" : this.BS_MULTIRESGOS_FIN) + '" ' +
                        ' data-BS_RTV_INIC="' + (this.BSBS_RTV_INIC_ID == null ? "" : this.BS_RTV_INIC) + '" ' +
                        ' data-BS_RTV_FIN="' + (this.BS_RTV_FIN == null ? "" : this.BS_RTV_FIN) + '" ' +
                        ' data-BS_REVI_ANUAL_GNV_INIC="' + (this.BS_REVI_ANUAL_GNV_INIC == null ? "" : this.BS_REVI_ANUAL_GNV_INIC) + '" ' +
                        ' data-BS_REVI_ANUAL_GNV_FIN="' + (this.BS_REVI_ANUAL_GNV_FIN == null ? "" : this.BS_REVI_ANUAL_GNV_FIN) + '" ' +
                        ' data-BS_REVISION_CILINDROS_GNV_INIC="' + (this.BS_REVISION_CILINDROS_GNV_INIC == null ? "" : this.BS_REVISION_CILINDROS_GNV_INIC) + '" ' +
                        ' data-BS_REVISION_CILINDROS_GNV_FIN="' + (this.BS_REVISION_CILINDROS_GNV_FIN == null ? "" : this.BS_REVISION_CILINDROS_GNV_FIN) + '" ' + '>' +

                         '<td>' + (i + 1) + '</td>' +
                        (permiso_eliminar == 1 ? '<td>' + '<span class="far fa-trash-alt" aria-hidden="true" style="cursor:pointer;"  onclick="confirmarAnulacionBus(' + this.BS_ID + ');" ></span>' + '</td>' : '') +
                        (permiso_eliminar == 1 ? '<td>' + '<span class="fas fa-ban" aria-hidden="true" style="cursor:pointer;" onclick="confirmarDesafectacionBus($(this).parent().parent(),' + this.BS_ID + ')"></span>' + '</td>' : '') +
                        (permiso_edit == 1 ? '<td>' + '<span class="far fa-edit" aria-hidden="true" style="cursor:pointer;" onclick="abrirEditarBus($(this).parent().parent());" ></span>' + '</td>' : '') +

                        '<td class="text-center" >' + (this.ABREVIATURA == null ? "" : this.ABREVIATURA) + '</td>' +
                        '<td class="text-center textoAchicado" >' + (this.BS_PLACA == null ? "" : this.BS_PLACA) + '</td>' +
                        '<td class="text-center"  >' + BS_SOAT_FEC_FIN + '</td>' +
                        '<td class="text-center" >' + BS_RTV_FIN + '</td>' +
                        '<td class="text-center" >' + BS_RC_FIN + '</td>' +
                        '<td class="text-center" >' + BS_REVI_ANUAL_GNV_FIN + '</td>' +
                        '<td class="text-center" >' + BS_REVISION_CILINDROS_GNV_FIN + '</td>' +
                        '<td class="text-center" >' + BS_VEHICULOS_FIN + '</td>' +
                        '<td class="text-center" >' + BS_CVS_FEC_FIN + '</td>' +
                        '<td class="text-center" >' + (this.BS_AÑO_FABRICACION == null ? "" : this.BS_AÑO_FABRICACION) + '</td>' +
                        '<td class="text-center" >' + (this.BS_COMBUSTIBLE == null ? "" : this.BS_COMBUSTIBLE) + '</td>' +
                        '<td class="text-center" >' + (this.BS_FECINI_PT == null ? "" : this.BS_FECINI_PT) + '</td>' +
                        '<td class="text-center textoAchicado"  title="' + (this.BS_NOM_EMPRE == null ? "" : this.BS_NOM_EMPRE) + '" >' + (this.BS_NOM_EMPRE == null ? "" : this.BS_NOM_EMPRE) + '</td>' +
                        '<td class="text-center textoAchicado"  title="' + (this.BS_PROPIETARIO == null ? "" : this.BS_PROPIETARIO) + '" >' + (this.BS_PROPIETARIO == null ? "" : this.BS_PROPIETARIO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_PAQUETE_CONCESION == null ? "" : this.BS_PAQUETE_CONCESION) + '</td>' +
                        '<td class="text-center" >' + (this.BS_PAQUETE_SERVICIO == null ? "" : this.BS_PAQUETE_SERVICIO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_TIPO_SERVICIO == null ? "" : this.BS_TIPO_SERVICIO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_ESTADO == null ? "" : this.BS_ESTADO) + '</td>' +
                        '<td class="text-center textoAchicado" >' + (this.BS_MARCA == null ? "" : this.BS_MARCA) + '</td>' +
                        '<td class="text-center textoAchicado" >' + (this.BS_MODELO == null ? "" : this.BS_MODELO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_TECNOLOGIA_EURO == null ? "" : this.BS_TECNOLOGIA_EURO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_POTENCIA_MOTOR == null ? "" : this.BS_POTENCIA_MOTOR) + '</td>' +
                        '<td class="text-center" >' + (this.BS_SERIE_MOTOR == null ? "" : this.BS_SERIE_MOTOR) + '</td>' +
                        '<td class="text-center" >' + (this.BS_SERIE_CHASIS == null ? "" : this.BS_SERIE_CHASIS) + '</td>' +
                        '<td class="text-center textoAchicado" >' + (this.BS_COLOR_VEHICULO == null ? "" : this.BS_COLOR_VEHICULO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_LONGITUD == null ? "" : this.BS_LONGITUD) + '</td>' +
                        '<td class="text-center" >' + (this.BS_ASIENTOS == null ? "" : this.BS_ASIENTOS) + '</td>' +
                        '<td class="text-center" >' + (this.BS_AREA_PASILLO == null ? "" : this.BS_AREA_PASILLO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_PESO_NETO == null ? "" : this.BS_PESO_NETO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_PESO_BRUTO == null ? "" : this.BS_PESO_BRUTO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_ALTURA == null ? "" : this.BS_ALTURA) + '</td>' +
                        '<td class="text-center" >' + (this.BS_ANCHO == null ? "" : this.BS_ANCHO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_CARGA_UTIL == null ? "" : this.BS_CARGA_UTIL) + '</td>' +
                        '<td class="text-center" >' + (this.BS_PUERTA_IZQUIERDA == null ? "" : this.BS_PUERTA_IZQUIERDA) + '</td>' +
                        '<td class="text-center" >' + (this.BS_PARTIDA_REGISTRAL == null ? "" : this.BS_PARTIDA_REGISTRAL) + '</td>' +
                        '<td class="text-center" >' + (this.BS_CODIGO_CVS == null ? "" : this.BS_CODIGO_CVS) + '</td>' +
                        '<td class="text-center" >' + (this.BS_CVS_FEC_INIC == null ? "" : this.BS_CVS_FEC_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_CVS_FEC_FIN == null ? "" : this.BS_CVS_FEC_FIN) + '</td>' +
                        '<td class="text-center" >' + (this.BS_SOAT_FEC_INIC == null ? "" : this.BS_SOAT_FEC_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_SOAT_FEC_FIN == null ? "" : this.BS_SOAT_FEC_FIN) + '</td>' +
                        '<td class="text-center textoAchicado" >' + (this.BS_POLIZA_VEHICULOS == null ? "" : this.BS_POLIZA_VEHICULOS) + '</td>' +
                        '<td class="text-center" >' + (this.BS_VEHICULOS_INIC == null ? "" : this.BS_VEHICULOS_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_VEHICULOS_FIN == null ? "" : this.BS_VEHICULOS_FIN) + '</td>' +
                        '<td class="text-center" >' + (this.BS_POLIZA_CIVIL == null ? "" : this.BS_POLIZA_CIVIL) + '</td>' +
                        '<td class="text-center" >' + (this.BS_RC_INICIO == null ? "" : this.BS_RC_INICIO) + '</td>' +
                        '<td class="text-center" >' + (this.BS_RC_FIN == null ? "" : this.BS_RC_FIN) + '</td>' +
                        '<td class="text-center" >' + (this.BS_POLIZA_ACCI_COLECTIVOS == null ? "" : this.BS_POLIZA_ACCI_COLECTIVOS) + '</td>' +
                        '<td class="text-center" >' + (this.BS_APCP_INIC == null ? "" : this.BS_APCP_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_APCP_FIN == null ? "" : this.BS_APCP_FIN) + '</td>' +
                        '<td class="text-center" >' + BS_APCP_FIN + '</td>' +
                        '<td class="text-center" >' + (this.BS_POLIZA_MULTIRIESGOS == null ? "" : this.BS_POLIZA_MULTIRIESGOS) + '</td>' +
                        '<td class="text-center" >' + (this.BS_MULTIRESGOS_INIC == null ? "" : this.BS_MULTIRESGOS_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_MULTIRESGOS_FIN == null ? "" : this.BS_MULTIRESGOS_FIN) + '</td>' +
                        '<td class="text-center" >' + BS_MULTIRESGOS_FIN + '</td>' +
                        '<td class="text-center" >' + (this.BS_RTV_INIC == null ? "" : this.BS_RTV_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_RTV_FIN == null ? "" : this.BS_RTV_FIN) + '</td>' +
                        '<td class="text-center" >' + (this.BS_REVI_ANUAL_GNV_INIC == null ? "" : this.BS_REVI_ANUAL_GNV_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_REVI_ANUAL_GNV_FIN == null ? "" : this.BS_REVI_ANUAL_GNV_FIN) + '</td>' +
                        '<td class="text-center" >' + (this.BS_REVISION_CILINDROS_GNV_INIC == null ? "" : this.BS_REVISION_CILINDROS_GNV_INIC) + '</td>' +
                        '<td class="text-center" >' + (this.BS_REVISION_CILINDROS_GNV_FIN == null ? "" : this.BS_REVISION_CILINDROS_GNV_FIN) + '</td>' +
                        '</tr>';

                });
            }

            $('#consultar').prop('disabled', false).html('Consultar');
            $('#listar_buses').append(strHTML);
            util.activarEnumeradoTabla('#Tbbuses', $('#btnBusquedaEnTabla'));


        },
        error: function (xhr, status, error) {
            strHTML += '<tr><td colspan="13" class="text-center" style="padding-left: 15%">No hay información para mostrar</td></tr>';
            $('#Tbbuses tbody').append(strHTML);
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


function confirmarAnulacionBus(idBus) {
    Swal.fire({
        text: "Estas seguro que deseas eliminar el bus ?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: URL_ANULA_BUS,
                dataType: 'json',
                data: {
                    idBus: idBus
                },
                success: function (result) {
                    getListBuses();
                    Swal.fire({
                        type: (result.COD_ESTADO == 1 ? 'success' : 'error'),
                        title: result.DES_ESTADO,
                        showConfirmButton: false,
                        //timer: 2000
                    });
                }
            }, JSON);
        }
    });
}

function confirmarDesafectacionBus(element, idBus) {


    var placaSeleccionada = element.attr('data-BS_PLACA');
    console.log(idBus);

    Swal.fire({
        html: "Deseas desafectar el bus de placa <strong>" + placaSeleccionada + '</strong> ?' + '<br>' + '<br>' +
              '<textarea id="txtObservacionBus" style="text-align: center;margin-top: 10px;" autofocus class="form-control" type="text" placeholder="Ingresar la Observacion" />',

        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.value) {
            var observacion = $('#txtObservacionBus').val();
            window.location.replace(URL_GET_PLACA_DESAFECT + '/?id_bus_desafec=' + idBus + '&ObservacionBus=' + observacion);
        }
    });

}

function abrirEditarBus(element) {


    var bs_id = element.attr('data-BS_ID');
    var id_corredor = element.attr('data-ID_CORREDOR');
    var fecini_pt = element.attr('data-BS_FECINI_PT');
    var nom_empre = element.attr('data-BS_NOM_EMPRE');
    var placa = element.attr('data-BS_PLACA');
    var propietario = element.attr('data-BS_PROPIETARIO');
    var paquete_concesion = element.attr('data-BS_PAQUETE_CONCESION');
    var paquete_servicio = element.attr('data-BS_PAQUETE_SERVICIO');
    var tipo_servicio = element.attr('data-BS_TIPO_SERVICIO');
    var estado = element.attr('data-BS_ESTADO');
    var marca = element.attr('data-BS_MARCA');
    var modelo = element.attr('data-BS_MODELO');
    var año_fabricacion = element.attr('data-BS_AÑO_FABRICACION');
    var combustible = element.attr('data-BS_COMBUSTIBLE');
    var tecnologia_euro = element.attr('data-BS_TECNOLOGIA_EURO');
    var potencia_motor = element.attr('data-BS_POTENCIA_MOTOR');
    var serie_motor = element.attr('data-BS_SERIE_MOTOR');
    var serie_chasis = element.attr('data-BS_SERIE_CHASIS');
    var color_vehiculo = element.attr('data-BS_COLOR_VEHICULO');
    var longitud = element.attr('data-BS_LONGITUD');
    var asientos = element.attr('data-BS_ASIENTOS');
    var area_pasillo = element.attr('data-BS_AREA_PASILLO');
    var peso_neto = element.attr('data-BS_PESO_NETO');
    var peso_bruto = element.attr('data-BS_PESO_BRUTO');
    var altura = element.attr('data-BS_ALTURA');
    var ancho = element.attr('data-BS_ANCHO');
    var carga_util = element.attr('data-BS_CARGA_UTIL');
    var puerta_izquierda = element.attr('data-BS_PUERTA_IZQUIERDA');
    var partida_registral = element.attr('data-BS_PARTIDA_REGISTRAL');
    var codigo_cvs = element.attr('data-BS_CODIGO_CVS');
    var cvs_fec_inic = element.attr('data-BS_CVS_FEC_INIC');
    var cvs_fec_fin = element.attr('data-BS_CVS_FEC_FIN');
    var soat_fec_inic = element.attr('data-BS_SOAT_FEC_INIC');
    var soat_fec_fin = element.attr('data-BS_SOAT_FEC_FIN');
    var poliza_vehiculos = element.attr('data-BS_POLIZA_VEHICULOS');
    var vehiculos_inic = element.attr('data-BS_VEHICULOS_INIC');
    var vehiculos_fin = element.attr('data-BS_VEHICULOS_FIN');
    var poliza_civil = element.attr('data-BS_POLIZA_CIVIL');
    var rc_inicio = element.attr('data-BS_RC_INICIO');
    var rc_fin = element.attr('data-BS_RC_FIN');
    var poliza_acci_colectivos = element.attr('data-BS_POLIZA_ACCI_COLECTIVOS');
    var apcp_inic = element.attr('data-BS_APCP_INIC');
    var apcp_fin = element.attr('data-BS_APCP_FIN');
    var poliza_multiriesgos = element.attr('data-BS_POLIZA_MULTIRIESGOS');
    var multiresgos_inic = element.attr('data-BS_MULTIRESGOS_INIC');
    var multiresgos_fin = element.attr('data-BS_MULTIRESGOS_FIN');
    var rtv_inic = element.attr('data-BS_RTV_INIC');
    var rtv_fin = element.attr('data-BS_RTV_FIN');
    var revi_anual_gnv_inic = element.attr('data-BS_REVI_ANUAL_GNV_INIC');
    var revi_anual_gnv_fin = element.attr('data-BS_REVI_ANUAL_GNV_FIN');
    var revision_cilindros_gnv_inic = element.attr('data-BS_REVISION_CILINDROS_GNV_INIC');
    var revision_cilindros_gnv_fin = element.attr('data-BS_REVISION_CILINDROS_GNV_FIN');


    $('#TXT_BS_ID').val(Number(bs_id));
    $('#selectAbrevCorredorEditar').val(Number(id_corredor));
    $('#TXT_BS_FECINI_PT').val(fecini_pt);
    $('#TXT_BS_NOM_EMPRE').val(nom_empre);
    $('#TXT_BS_PLACA').val(placa);
    $('#TXT_BS_PROPIETARIO').val(propietario);
    $('#TXT_BS_PAQUETE_CONCESION').val(paquete_concesion);
    $('#TXT_BS_PAQUETE_SERVICIO').val(paquete_servicio);
    $('#TXT_BS_TIPO_SERVICIO').val(tipo_servicio);
    $('#TXT_BS_ESTADO').val(estado);
    $('#TXT_BS_MARCA').val(marca);
    $('#TXT_BS_MODELO').val(modelo);
    $('#TXT_BS_AÑO_FABRICACION').val(año_fabricacion);
    $('#TXT_BS_COMBUSTIBLE').val(combustible);
    $('#TXT_BS_TECNOLOGIA_EURO').val(tecnologia_euro);
    $('#TXT_BS_POTENCIA_MOTOR').val(potencia_motor);
    $('#TXT_BS_SERIE_MOTOR').val(serie_motor);
    $('#TXT_BS_SERIE_CHASIS').val(serie_chasis);
    $('#TXT_BS_COLOR_VEHICULO').val(color_vehiculo);
    $('#TXT_BS_LONGITUD').val(longitud);
    $('#TXT_BS_ASIENTOS').val(asientos);
    $('#TXT_BS_AREA_PASILLO').val(area_pasillo);
    $('#TXT_BS_PESO_NETO').val(peso_neto);
    $('#TXT_BS_PESO_BRUTO').val(peso_bruto);
    $('#TXT_BS_ALTURA').val(altura);
    $('#TXT_BS_ANCHO').val(ancho);
    $('#TXT_BS_CARGA_UTIL').val(carga_util);
    $('#TXT_BS_PUERTA_IZQUIERDA').val(puerta_izquierda);
    $('#TXT_BS_PARTIDA_REGISTRAL').val(partida_registral);
    $('#TXT_BS_CODIGO_CVS').val(codigo_cvs);
    $('#TXT_BS_CVS_FEC_INIC').val(cvs_fec_inic);
    $('#TXT_BS_CVS_FEC_FIN').val(cvs_fec_fin);
    $('#TXT_BS_SOAT_FEC_INIC').val(soat_fec_inic);
    $('#TXT_BS_SOAT_FEC_FIN').val(soat_fec_fin);
    $('#TXT_BS_POLIZA_VEHICULOS').val(poliza_vehiculos);
    $('#TXT_BS_VEHICULOS_INIC').val(vehiculos_inic);
    $('#TXT_BS_VEHICULOS_FIN').val(vehiculos_fin);
    $('#TXT_BS_POLIZA_CIVIL').val(poliza_civil);
    $('#TXT_BS_RC_INICIO').val(rc_inicio);
    $('#TXT_BS_RC_FIN').val(rc_fin);
    $('#TXT_BS_POLIZA_ACCI_COLECTIVOS').val(poliza_acci_colectivos);
    $('#TXT_BS_APCP_INIC').val(apcp_inic);
    $('#TXT_BS_APCP_FIN').val(apcp_fin);
    $('#TXT_BS_POLIZA_MULTIRIESGOS').val(poliza_multiriesgos);
    $('#TXT_BS_MULTIRESGOS_INIC').val(multiresgos_inic);
    $('#TXT_BS_MULTIRESGOS_FIN').val(multiresgos_fin);
    $('#TXT_BS_RTV_INIC').val(rtv_inic);
    $('#TXT_BS_RTV_FIN').val(rtv_fin);
    $('#TXT_BS_REVI_ANUAL_GNV_INIC').val(revi_anual_gnv_inic);
    $('#TXT_BS_REVI_ANUAL_GNV_FIN').val(revi_anual_gnv_fin);
    $('#TXT_BS_REVISION_CILINDROS_GNV_INIC').val(revision_cilindros_gnv_inic);
    $('#TXT_BS_REVISION_CILINDROS_GNV_FIN').val(revision_cilindros_gnv_fin);

    $('#ModalEditar').modal('show');
}



function seleccionaFila(elementFila) {
    $.each($('#Tbbuses tbody > tr'), function () {
        $(this).css('background-color', '');
    });
    elementFila.css('background-color', '#17a2b838');
}

$('#myModal .save').click(function (e) {

    e.preventDefault();
    addImage(5);
    $('#myModal').modal('hide');
    return false;
})

$('#ModalEditar .save').click(function (e) {
    e.preventDefault();
    addImage(5);
    $('#ModalEditar').modal('hide');
    return false;
})





