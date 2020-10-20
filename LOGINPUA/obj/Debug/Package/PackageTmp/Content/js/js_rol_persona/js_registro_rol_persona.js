$(document).ready(function () {

    getListRoles();

});

function AgregarRol() {

    var nombre = $('#nombre').val();

    if ($('#nombre').val().length == 0) {
        Swal.fire({
            type: 'info',
            title: "Debe llenar el campo obligatorio para realizar el registro.",
            showConfirmButton: false,
        });
        return false;
    }
    

    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');

    $.ajax({
        url: URL_AGREGAR_ROL,
        data: {
            nombre: nombre
        },
        dataType: 'json',
        success: function (result) {
            $('#consultar').prop('disabled', false).html('Guardar');

            Swal.fire({
                type: result.COD_ESTADO == 1 ? 'success' : 'error',
                title: result.DES_ESTADO,
                showConfirmButton: false,
            });
            if (result.COD_ESTADO == 0) {
                $("#nombre").focus();
            } else {
                $('#RegistrarRolModal').modal('hide');
                getListRoles();

            }
        },
    }, JSON);
}


function getListRoles() {
    var permiso_edit = $('#permisoActualiza').val();
    var permiso_eliminar = $('#permisoEliminar').val();

    $('#listar_roles').empty();
    //$('#tbRol tbody').empty();
    
    DATA_LISTA_ROLES = "";
    var strHTML = '';
    var i = 1;

    $.ajax({
        url: URL_LIST_ROLES,
        dataType: 'json',
        success: function (result) {
            DATA_LISTA_ROLES = result

            
            if (result.length <= 0) {
                strHTML += '<tr><td colspan="100" class="text-left" style="padding-left: 15%">No hay información para mostrar</td></tr>';

            } else {

                $.each(result, function () {

        
                    strHTML += '<tr  onclick="seleccionaFila($(this))" ' +
                        ' data-ID_ROLPERSONA="' + (this.ID_ROLPERSONA == null ? "" : this.ID_ROLPERSONA) + '" ' +
                        ' data-NOMBRE="' + (this.NOMBRE == null ? "" : this.NOMBRE) + '"> ' +
                        
                        '<td class="text-center" >' + i++  + '</td>' +
                        '<td class="text-center" >' + (this.NOMBRE == null ? "" : this.NOMBRE) + '</td>' +
                        '<td class="text-center" >' + (this.USU_REG == null ? "" : this.USU_REG) + '</td>' +
                        '<td class="text-center" >' + (this.FECHA_REG == null ? "" : this.FECHA_REG) + '</td>' +

                        (permiso_edit == 1 ? '<td>' + '<span class="far fa-edit" aria-hidden="true" style="cursor:pointer;" onclick="abrirEditarRol($(this).parent().parent());" ></span>' + '</td>' : '') +
                        (permiso_edit == 1 ? '<td>' + '<span class="far fa-trash-alt" aria-hidden="true" style="cursor:pointer;"  onclick="confirmarAnulacionRol(' + this.ID_ROLPERSONA + ');" ></span>' + '</td>' : '') +
                        
                        '</tr>';

                });

            }
            $('#listar_roles').append(strHTML);
            util.activarEnumeradoTabla('#tbRol', $('#btnBusquedaEnTabla'));


        },

    }, JSON);

}


function confirmarAnulacionRol(idRol) {

    Swal.fire({
        text: "Estas seguro que deseas eliminar el Rol ?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: URL_ANULAR_ROL,
                dataType: 'json',
                data: {
                    idRol: idRol
                },
                success: function (result) {
                   
                    Swal.fire({
                        type: (result.COD_ESTADO == 1 ? 'success' : 'error'),
                        title: result.DES_ESTADO,
                        showConfirmButton: false,
                        //timer: 2000
                    });
                    getListRoles(); 
                },
            }, JSON);
        }
        
    });

}


function abrirEditarRol(element) {

    var id_rolpersona = element.attr('data-ID_ROLPERSONA');
    var nombre = element.attr('data-NOMBRE');

    $('#TXT_ID_ROLPERSONA').val(Number(id_rolpersona));
    $('#TXT_NOMBRE').val(nombre);

        $('#ModalEditar').modal('show');
}


function EditarRol() {

    var id_rolpersona = $('#TXT_ID_ROLPERSONA').val();
    var nombre = $('#TXT_NOMBRE').val();

    if ($('#TXT_NOMBRE').val().length == 0) {
        Swal.fire({
            type: 'info',
            title: "Debe llenar el campo obligatorio para realizar el registro.",
            showConfirmButton: false,
        });
        return false;
    }
 
    $('#btnEditarRol').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');

    $.ajax({
        url: URL_MODIFICAR_ROL,
        data: {
            id_rolpersona : id_rolpersona,
            nombre: nombre
        },
        dataType: 'json',
        success: function (result) {
            $('#btnEditarRol').prop('disabled', false).html('Guardar');

            Swal.fire({
                type: result.COD_ESTADO == 1 ? 'success' : 'error',
                title: result.DES_ESTADO,
                showConfirmButton: false,
            });
            if (result.COD_ESTADO == 0) {
                $("#nombre").focus();
            } else {
                $('#ModalEditar').modal('hide');

            }
            getListRoles();
        },

    }, JSON);
}

function seleccionaFila(elementFila) {
    $.each($('#tbRol tbody > tr'), function () {
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