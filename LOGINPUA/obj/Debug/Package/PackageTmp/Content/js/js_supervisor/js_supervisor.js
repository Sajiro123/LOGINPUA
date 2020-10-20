$(document).ready(function () {
 

});


function ConsultarFecha() {

    $('#tbDescargarDespacho tbody').empty()
    var id_corredor_ = $('#selectCorredores').val();
    var fecha_ = $('#txtFechaConsultaFin').val();

    var strHTML = "";
    var i=1

    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');

    $.ajax({
        url: URL_GET_CONSULTAR_FECHA,
        data: {
            id_corredor: id_corredor_,
            fecha: fecha_
        },
        dataType: 'json',
        success: function (result) {
            console.log(result)
             $('#consultar').prop('disabled', false).html('Consultar');

            DATA_LISTA_ROLES = result
            if (result.length == 0) {
                strHTML += '<tr><td colspan="100" class="text-left" style="padding-left: 35%">No hay información para mostrar</td></tr>';

            } else {

                $.each(result, function () {


                    strHTML += '<tr  onclick="seleccionaFila($(this))" ' +
                        ' data-ID_MAESTRO_BUSESD="' + (this.ID_MAESTRO_BUSESD == null ? "" : this.ID_MAESTRO_BUSESD) + '"> ' +
                        '<td class="text-center" >' + i++ + '</td>' +
                        '<td class="text-center" >' + (this.PLACA == null ? "" : this.PLACA) + '</td>' +
                        '<td class="text-center" >' + (this.CONCESIONARIO == null ? "" : this.CONCESIONARIO) + '</td>' +
                        '<td class="text-center" >' + (this.DIRECCION_INSPECCION == null ? "" : this.DIRECCION_INSPECCION) + '</td>' +
                        '<td class="text-center" >' + (this.ESTADO_BUS == null ? "" : this.ESTADO_BUS) + '</td>' +
                        '<td class="text-center" >' + (this.USU_REG == null ? "" : this.USU_REG) + '</td>' +
                        '<td class="text-center" >' + (this.FECHA_REG == null ? "" : this.FECHA_REG) + '</td>' +

                       '<td>'+
                       '<form action="' + URL_GET_EDITAR_FORMATO + '" enctype="multipart/form-data" method="post">' +
                       '<button type="submit"> <span class="far fa-edit"></span></button> ' +
                       ' <input type="text" name="placa" id="placa" value="' + this.PLACA + '" hidden />'+
                       '<input type="text" name="direccion" id="direccion" value="' + this.DIRECCION_INSPECCION + '" hidden />' +
                       '<input type="text" name="km" value="' + this.KM + '" hidden />' +
                       ' <input type="text" name="latitud" value="' + this.LATITUD + '" hidden />' +
                       '<input type="text" name="longitud" value="' + this.LONGITUD + '" hidden />' +
                       '<input type="text" name="accion" value="EDITAR"  hidden />' +
                       '<input type="text" name="id_maestro" value="'+this.ID_MAESTRO_BUSESD+'"  hidden />' +
                       '</form> </td>' +

                          '<td>' +
                       '<form action="' + URL_GET_UBICACION + '" enctype="multipart/form-data" method="post">' +
                       '<button type="submit"> <i class="fas fa-map-marker-alt"></i></button> ' +
                       ' <input type="text" name="latitud" value="' + this.LATITUD + '" hidden />' +
                       '<input type="text" name="longitud" value="' + this.LONGITUD + '" hidden />' +
                       '</form> </td>' +
                        //(permiso_edit == 1 ? '<td>' + '<span class="far fa-trash-alt" aria-hidden="true" style="cursor:pointer;"  onclick="confirmarAnulacionRol(' + this.ID_ROLPERSONA + ');" ></span>' + '</td>' : '') +

                        '</tr>';
                });
                
            }
            $('#tbDescargarDespacho').append(strHTML);
            util.activarEnumeradoTabla('#tbDescargarDespacho', $('#btnBusquedaEnTabla'));
        },
     }, JSON);
}

function abrirEditarFormato(element) {

    //var id_rolpersona = element.attr('data-ID_ROLPERSONA');
    //var nombre = element.attr('data-NOMBRE');

    //$('#TXT_ID_ROLPERSONA').val(Number(id_rolpersona));
    //$('#TXT_NOMBRE').val(nombre);

    $('#idEditarFormato').modal('show');
}
function seleccionaFila(elementFila) {

    $.each($('#tbDescargarDespacho tbody > tr'), function () {
        $(this).css('background-color', '');
    });
    elementFila.css('background-color', '#17a2b838');
}