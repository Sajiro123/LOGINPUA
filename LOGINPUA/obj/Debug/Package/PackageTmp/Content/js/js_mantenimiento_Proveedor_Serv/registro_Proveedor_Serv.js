var ID_USUARIO_PROV = 0;
var ID_PROV_SERV = 0;
var ESTADO_ACCION =0;
$(document).ready(function () {
    ListarProveedorServ();
    ListarProvServ_Usuario();
    $('#idusuario_prove').append('<option value="0" selected="selected">Seleccionar Usuario</option>');

});


$("body").on("click", ".btn-password", function () {
    $.ajax({
        url: URL_GET_ACTUALIZAR_CONTRASEÑA,
        data: {
            id_prov_usuario: ID_USUARIO_PROV,
        },
        dataType: 'json',
        success: function (result) {
            ListarProvServ_Usuario();
            Swal.fire({
                type: result.COD_ESTADO == 1 ? 'success' : 'error',
                title: result.DES_ESTADO,
                showConfirmButton: false,
            });
            $('#ActualizarModal').modal('hide');
        }
    }, JSON);
});

$("body").on("click", "#modal_registroprovserv", function () {
    $('#idusuario_prov').val('');
    $('#clave').val('');     
});





$("body").on("click", ".btn-update", function () {


    ID_USUARIO_PROV = 0;
    ID_PROV_SERV = 0;
    ID_USUARIO_PROV = $(this).parents("tr").attr('data-id_prov_usuario');
    ID_PROV_SERV = $(this).parents("tr").attr('data-id_prov_serv');
    var id_usuario = $(this).parents("tr").attr('data-ID_USUARIO');
    ESTADO_ACCION = $(this).children().attr('data-estado');
  
    if (id_usuario == 0) {
        $('.selectpicker').selectpicker('val', 0);
        $(".selectpicker").prop('disabled', false);

    } else {
        $('.selectpicker').selectpicker('val', id_usuario);
        $(".selectpicker").prop('disabled', true);
    }

    $("#div_context").remove();
    ListCorredores(ID_PROV_SERV, ID_USUARIO_PROV);
});



function AgregarUsuarios_Corredor(elemet) {
    var idcorredores = $('#id_corredores').val();
    var idusuario = $('#idusuario_prove').val();

    if (idcorredores.length <= 0) {
        Swal.fire({ title: 'Seleccionar Corredor ', type: 'warning', }); return false;

    }
    if (idusuario == 0) {
        Swal.fire({ title: 'Seleccionar usuario', type: 'warning', }); return false;
    }

    var id_corredor_string = [];
    $.each(idcorredores, function (idx2, val2) {
        var str = val2;
        id_corredor_string.push(str);
    });
    var id_corredor = id_corredor_string.join("|");
    $('#consultar1').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');
    $.ajax({
        url: URL_GET_AGREGARUSUARIO_CORREDOR,
        data: {
            idcorredores: id_corredor,
            idusuario: idusuario,
            id_prov_usuario: ID_USUARIO_PROV,
            id_prov_serv: ID_PROV_SERV,
            tipo_accion: ESTADO_ACCION
        },
        dataType: 'json',
        success: function (result) {
            $('#consultar1').prop('disabled', false).html('Guardar');
            ListarProvServ_Usuario();
            Swal.fire({
                type: result.COD_ESTADO == 1 ? 'success' : 'error',
                title: result.DES_ESTADO,
                showConfirmButton: false,
            });
            if (result.COD_ESTADO == 1) {
                $('#ActualizarModal').modal('hide');
            }

        }
    }, JSON);


}

function ListCorredores(id_prov_serv, ID_USUARIO_PROV) {


    $.ajax({
        url: URL_GET_LIST_CORREDOR,
        dataType: 'json',
        data: {
            id_prov_serv: id_prov_serv,
            id_usuario_pro: ID_USUARIO_PROV
        },
        success: function (result) {

            $('#idcorredores_div').append('<div id="div_context"><select  multiple id="id_corredores"></select></div>')

            if (result.length == 0) { // si la lista esta vacia
                $('#id_corredores').append('<option value="0">' + '--No hay información--' + '</option>');
                return false;
            } else {
                $.each(result, function () {
                    $('#id_corredores').append('<option value="' + this.ID_CORREDOR + '">' + this.ABREVIATURA + '</option>');
                });
            }

            $('#id_corredores').multiselect({
                columns: 1,
                selectedOptions: 'PHP',
                placeholder: 'Seleccionar Corredor'
            });
            $('#ms-list-1').children().css("border", "1px solid #ced4da")




            //LIST PUT SELECTED CORREDOR BY ID_PROV_USU
            $.ajax({
                url: URL_GET_LIST_CORREDOR_X_USUPROV,
                data: {
                    id_prov_usu: ID_USUARIO_PROV,
                },
                dataType: 'json',
                success: function (result) {
                    //console.log(result, '<----')
                    //$('#id_corredores').val('4,3')

                    var corredorUsuario = [];
                    $.each(result, function () {
                        corredorUsuario.push(this.ID_CORREDOR)
                    });

                    $.each($('#id_corredores').parent().find('ul > li'), function () {
                        var idCorredor = Number($(this).find('input').val());
                        if (corredorUsuario.indexOf(idCorredor) != -1) {
                            $(this).find('input').click();
                            $(this).addClass('selected');
                        }
                    });

                }
            }, JSON);



        }
    }, JSON);
}

function ListarProveedorServ() {
    $.ajax({
        url: URL_GET_PROVVEDOR_SERV,
        dataType: 'json',
        success: function (result) {

            $('#txtPROVEEDORSERV').empty();
            if (result.length == 0) { // si la lista esta vacia
                $('#txtPROVEEDORSERV').append('<option value="0">' + '--No hay información--' + '</option>');
                return false;
            } else {
                $.each(result, function () {
                    $('#txtPROVEEDORSERV').append('<option value="' + this.ID_PROV_SERV + '">' + this.NOMBRE + '</option>');
                });
            }
        }
    }, JSON);
}


function AgregarUsuarios_Proveedor() {


    var idprovserv =  parseInt($('#txtPROVEEDORSERV').val());
    var usuario = $('#idusuario_prov').val();
    var clave = $('#clave').val();




    if (usuario == "") { Swal.fire({ title: 'Seleccionar usuario', type: 'warning', }); return false; }
    if (clave === "") { Swal.fire({ title: 'Ingresar contraseña', type: 'warning', }); return false; }

    //OBTENGO LOS PERFILES PARA AGREGAR POR CLASE
    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');
    $.ajax({
        url: URL_GET_AGREGAR_USUARIO_PROVEEDOR,
        data: {
            idprov: idprovserv,
            usuario: usuario,
            contraseña: clave,
        },
        dataType: 'json',
        success: function (result) {
            $('#consultar').prop('disabled', false).html('Guardar');
            ListarProvServ_Usuario();

            Swal.fire({
                type: result.COD_ESTADO == 1 ? 'success' : 'error',
                title: result.DES_ESTADO,
                showConfirmButton: false,
            });
            if (result.COD_ESTADO == 1) {
                $('#exampleModal').modal('hide');

            }


        }
    }, JSON);
}

function ListarProvServ_Usuario() {
    // PERMISOS
    //1= EDITAR || 2==ELIMINAR 
    var permiso_edit = $('#permisoActualiza').val();
    var permiso_eliminar = $('#permisoEliminar').val();

    var class_span = "";
    $.ajax({
        url: URL_GET_LISTAR_USUARIO_PROVEEDOR,
        dataType: 'json',
        success: function (result) {
            var strHTML = '';
            var i = 0;
            DATA_LISTA_REGISTROS = result;
            $.each(result, function () {
                i++;
                var  estado=0
                var Disponibilidad_usu = this.ID_USUARIO == null ? "" : this.ID_USUARIO;
                class_span = this.ID_USUARIO === 0 ? "fa fa-user-plus" : "fa fa-edit";

                var USUARIO_ = this.USUARIO == null ? "" : this.USUARIO;
            
                var function_confirmarAnulacionUsuario = USUARIO_ == "" ? '<span  class="fa fa-user-times" aria-hidden="true" style="cursor:not-allowed; color:red;" ></span>' : '<span class="fa fa-user-times" aria-hidden="true" style="cursor:pointer; color:red;" onclick="confirmarAnulacionCuenta_Prov(' + this.ID_PROV_USUARIO + ');" ></span>';


                if (Disponibilidad_usu === 0) {
                    Disponibilidad_usu = 'green';
                    estado = 0;
                } else {
                    Disponibilidad_usu = '#8e8e2e';
                    estado = 1;
                }

                $('#tbListarProvServ_Usuario tbody').empty();
                strHTML += '<tr onclick="seleccionaFila($(this))" data-ID_USUARIO="' + this.ID_USUARIO + '" data-ID_PROV_SERV="' + this.ID_PROV_SERV + '"  data-ID_PROV_USUARIO="' + this.ID_PROV_USUARIO + '">' +
                               '<td> ' + i + '</td>' +
                                '<td>' + '<span class="badge badge-secondary">'+USUARIO_+'</span>   </td>' +
                                '<td>' + this.NOMBRE_PROVE + '</td>' +
                                '<td>' + this.USUARIO_PROV + '</td>' +
                                '<td>' + this.CONTRASENA + '</td>' +
                                '<td class="text-center">' + this.USU_REG + '</td>' +
                                '<td class="text-center">' + this.FECHA_REG + '</td>' +
                               (permiso_edit == 1 ? '<td>' + '<button    type="button" class="btn btn-sm btn-update" data-toggle="modal" data-target="#ActualizarModal"><span data-estado=' + estado + ' class="' + class_span + '" style="color:' + Disponibilidad_usu + '" aria-hidden="true" style="cursor:pointer;"></span></button>' + '</td>' : '') +
                            (permiso_edit == 1 ? '<td>' + function_confirmarAnulacionUsuario + '</td>' : '') +
                            (permiso_eliminar == 1 ? '<td>' + '<span class="far fa-trash-alt" aria-hidden="true" style="cursor:pointer;" onclick="confirmarAnulacionUsuario(' + this.ID_PROV_USUARIO + ');" ></span>' + '</td>' : '') +
                           '</tr>';
            });
            $('#tbListarProvServ_Usuario tbody').append(strHTML);
            util.activarEnumeradoTabla('#tbListarProvServ_Usuario', $('#btnBusquedaEnTabla'));
            //activarEnumeradoTabla('#tbListarProvServ_Usuario', $('#btnBusquedaEnTabla'));
        }
    }, JSON);
}

function activarEnumeradoTabla(idTabla, botonActivaBusqueda) {
    var _posicionRowBusqueda = 0;
    var posicionRowBusqueda = 0;
    //
    var _idTHead = idTabla + ' thead';
    var _elementBusquedaTR = idTabla + ' thead > tr';
    var _activoBusqueda = null;
    $.each($(elementBusquedaTR), function (i) {
        _activoBusqueda = ($(this).find('th').find('input').length > 0 ? true : false);
        _posicionRowBusqueda = i;
    });

    console.log('_activoBusqueda', _activoBusqueda);
    
    $(idTabla).fancyTable({
        //sortColumn: 0,// column number for initial sorting
        //sortOrder: 'descending',// 'desc', 'descending', 'asc', 'ascending', -1 (descending) and 1 (ascending)
        //sortable: true,
        pagination: true,// default: false
        searchable: true,
        inputPlaceholder: "Buscar...",
        perPage: 15,
        // globalSearch: true,
        //globalSearchExcludeColumns: [2, 5]// exclude column 2 & 5
    });
    var idTHead = idTabla + ' thead';
    var elementBusquedaTR = idTabla + ' thead > tr';
    $.each($(elementBusquedaTR), function (i) {
        activoBusqueda = ($(this).find('th').find('input').length > 0 ? true : false);
        posicionRowBusqueda = i;
    });
    $(idTHead).find('tr').eq(posicionRowBusqueda).css('display', 'none')
    //agregandole el estado de la busqueda como un atributo de la tabla
    $(idTabla).attr('data-estadobusqueda', 'false');

    botonActivaBusqueda.click(function () {
        //console.log(this);
        //return;
        var dataBusqueda = ($(idTabla).attr('data-estadobusqueda') == 'true' ? true:false);
    

        if (dataBusqueda) {
            $(this).css({
                'background-color': '#17a2b8',
                'border-color': '#17a2b8',
            });
            $(idTHead).find('tr').eq(posicionRowBusqueda).css('display', 'none')

        } else {
            $(this).css({
                'background-color': '#138496',
                'border-color': '#117a8b',
            });
            $(idTHead).find('tr').eq(posicionRowBusqueda).css('display', 'table-row')
        }
        $(idTabla).attr('data-estadobusqueda', (dataBusqueda ? 'false' : 'true'));
    });
}

var busqueda = false;
function mostrarBusqueda() {
    if (busqueda) {
        $('#btnBusquedaEnTabla').css({
            'background-color': '#17a2b8',
            'border-color': '#17a2b8',
        });
        $('#tbListarProvServ_Usuario thead').find('tr').eq(posicionRowBusqueda).css('display', 'none')
        
    } else {
        $('#btnBusquedaEnTabla').css({
            'background-color': '#138496',
            'border-color': '#117a8b',
        });
        $('#tbListarProvServ_Usuario thead').find('tr').eq(posicionRowBusqueda).css('display', 'table-row')
    }
    busqueda = (busqueda ? false : true);
}

function confirmarAnulacionUsuario(idusuario) {
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
                url: URL_GET_ELIMINAR_USUARIO_PROVEEDOR,
                dataType: 'json',
                data: { idusuario: idusuario },
                success: function (result) {
                    ListarProvServ_Usuario();
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

function confirmarAnulacionCuenta_Prov(id_prov_usuario) {
    Swal.fire({
        text: "Estas seguro que deseas anular la cuenta ?",
        type: 'error',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: URL_GET_ELIMINAR_USUARIO_CUENTA,
                dataType: 'json',
                data: { id_prov_usuario: id_prov_usuario },
                success: function (result) {
                    ListarProvServ_Usuario();
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
    $.each($('#tbListarProvServ_Usuario tbody > tr'), function () {
        $(this).css('background-color', '');
    });
    elementFila.css('background-color', '#17a2b838');
}





