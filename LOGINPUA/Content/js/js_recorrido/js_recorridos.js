
//var CONTRASEÑA = ""

$(document).ready(function () {
    ListarRecorrido();
});



//$("body").on("click", "#modal_registroperf", function () {

//    $('#nombre').val('');

//});


//function AgregarPerfil() {


//    var id_modalidad = $('#idmodalidad').val();
//    var nombre = $("#nombre").val();

//    if (id_modalidad == null) {
//        Swal.fire({ title: 'Seleccionar Modalidad', type: 'warning', })
//        return false;
//    }
//    if (nombre === "") {
//        Swal.fire({ title: 'Ingresar nombre', type: 'warning', })
//        return false;
//    }
//    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');

//    $.ajax({
//        url: URL_GET_AGREGAR_PERFIL,
//        data: {
//            nombre: nombre,
//            idmodalidad: id_modalidad
//        },
//        dataType: 'json',
//        success: function (result) {
//            $('#consultar').prop('disabled', false).html('Guardar');

//            Swal.fire({
//                type: result.COD_ESTADO == 1 ? 'success' : 'error',
//                title: result.DES_ESTADO,
//                showConfirmButton: false,
//            });
//            if (result.COD_ESTADO == 0) {
//                $("#nombre").focus();
//            } else {
//                $('#exampleModal').modal('hide');
//                ListarPerfil(id_modalidad);

//            }
//        }
//    }, JSON);
//}



//function ListarModalidad() {
//    $.ajax({
//        url: URL_GET_LISTAR_MODALIDAD,
//        dataType: 'json',
//        success: function (result) {
//            $('#idmodalidad').empty();
//            if (result.length == 0) { // si la lista esta vacia
//                $('#idmodalidad').append('<option value="0">' + '--No hay información--' + '</option>');
//            } else {
//                $.each(result, function () {
//                    $('#idmodalidad').append('<option value="' + this.ID_MODALIDAD_TRANS + '">' + this.NOMBRE + '</option>');
//                });
//            }
//            var id_modalidad = $('#idmodalidad').val();
//            ListarPerfil(id_modalidad);
//        }
//    }, JSON);
//}


function ListarRecorrido() {

    // PERMISOS
    //1= EDITAR || 2==ELIMINAR 


    $('#tbRecorrido tbody').empty();

    $.ajax({
        url: URL_GET_LISTAR_RECORRIDOS,
        dataType: 'json',
        success: function (result) {
           
            console.log(result)

            var strHTML = '';
            var i = 0;
            DATA_LISTA_REGISTROS = result;
            if (result.length == 0) {
                DATA_LISTA_REGISTROS = [];
                strHTML += '<tr><td colspan="9" class="text-center">No hay información para mostrar</td></tr>';
            } else {
                $.each(result.Table, function (i,data) {

                    i++;
                    strHTML += '<tr  data-ID_PERFIL=' + + ' onclick="seleccionaFila($(this))" ' + '">' +
                                  '<td> ' + i + '</td>' +
                                    '<td>' + data.DISTANCIA_A + '</td>' +
                                    '<td>' + data.DISTANCIA_B + '</td>' +
                                    '<td>' + data.FNODE_A + '</td>' +
                                    '<td>' + data.FNODE_B + '</td>' +
                                    '<td>' + data.TNODE_A + '</td>' +
                                    '<td>' + data.TNODE_B + '</td>' +
                                   '<td class="text-center">' + data.USU_REG + '</td>' +
                                   '<td class="text-center">' + data.FECHA_REG + '</td>' +
                                   '<td>' + "<span class='far fa-trash-alt' aria-hidden='true' style='cursor:pointer;'  </span>" + '</td>' +
                                   '<td>' + '<span class="far fa-edit btn-edit" aria-hidden="true" style="cursor:pointer;" </span>' + '</td>' +
                               '</tr>';
                });
            }
            $('#tbRecorrido tbody').append(strHTML);
            util.activarEnumeradoTabla('#tbRecorrido', $('#btnBusquedaEnTabla'));
        }
    }, JSON);
}


function seleccionaFila(elementFila) {
    $.each($('#tbRecorrido tbody > tr'), function () {
        $(this).css('background-color', '');
    });
    elementFila.css('background-color', '#17a2b838');
}

//var POSICIO_CLICK_ROW = null;
//$("body").on("click", ".btn-edit", function () {

//    if (POSICIO_CLICK_ROW) {
//        $('#tbPerfil tbody').find('tr').eq(POSICIO_CLICK_ROW - 1).find('td:eq(6)').find('button:eq(1)').click()
//    }
//    POSICIO_CLICK_ROW = Number($(this).parents("tr").find('td').eq(0).text());


//    var nombre = $(this).parents("tr").attr('data-NOMBRE');
//    var idmodalidad = $(this).parents("tr").attr('data-MODALIDAD');
//    var selectperfil = $('#idmodalidad').clone();


//    $(this).parents("tr").find("td:eq(1)").html('<input name="edit_nombre" type="text" style="text-align:center;color: black;background-color: #51656463;" class="form-control form-control-sm" value="' + nombre + '">');
//    var selectperfil = $(this).parents("tr").find("td:eq(2)").html('<select name="edit_perfil" class="form-control form-control-sm" style="color: black;background-color: #51656463;">' + selectperfil.html() + '</select>');

//    selectperfil.find('select').val(idmodalidad);


//    $(this).parents("tr").find("td:eq(6)").prepend("<button class='btn-info btn-xs btn-update' style='margin:0 4px' ><i class='fas fa-check'></i></button>" +
//         "<button class='btn-warning btn-xs btn-cancel' style='margin:0 4px'><i class='fas fa-ban'></i></button>")
//    $(this).hide();
//});

//$("body").on("click", ".btn-cancel", function () {

//    var nombre = $(this).parents("tr").attr('data-NOMBRE');
//    var modalidad = $(this).parents("tr").attr('data-NOMBRE_MODALIDAD');


//    $(this).parents("tr").find("td:eq(1)").text(nombre);
//    $(this).parents("tr").find("td:eq(2)").text(modalidad);


//    $(this).parents("tr").find(".btn-edit").show();
//    $(this).parents("tr").find(".btn-update").remove();
//    $(this).parents("tr").find(".btn-cancel").remove();

//});

//$("body").on("click", ".btn-update", function () {

//    var nombre_perfil = $(this).parents("tr").find("input[name='edit_nombre']").val();
//    console.log(nombre_perfil)
//    var id_modalidad = $(this).parents("tr").find("select[name='edit_perfil']").val();

//    var id_modalidad_perfil = $(this).parents("tr").attr('data-ID_PERFIL');
//    var val_modalidad = $('#idmodalidad').val();


//    $.ajax({
//        url: URL_GET_MODIFICAR_PERFIL,
//        data: {
//            idmodalidad: id_modalidad,
//            id_modalidad_perfil: id_modalidad_perfil,
//            nombre_perfil: nombre_perfil
//        },
//        dataType: 'json',
//        success: function (result) {
//            ListarPerfil(val_modalidad);
//            Swal.fire({
//                type: result.COD_ESTADO == 1 ? 'success' : 'error',
//                title: result.DES_ESTADO,
//                showConfirmButton: false,
//            });
//            $('#exampleModal').modal('hide');
//        }
//    }, JSON);

//    $(this).parents("tr").find(".btn-edit").show();
//    $(this).parents("tr").find(".btn-cancel").remove();
//    $(this).parents("tr").find(".btn-update").remove();
//});


//function confirmarAnulacionUsuario(idperfil, element) {

//    var val_modalidad = $('#idmodalidad').val();
//    var nombre = element.parent().parent("tr").attr('data-NOMBRE');


//    Swal.fire({
//        text: "Estas seguro que deseas anular el registro ?",
//        type: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Si',
//        cancelButtonText: 'No'
//    }).then((result) => {
//        if (result.value) {
//            $.ajax({
//                url: URL_ANULAR_PERFIL,
//                dataType: 'json',
//                data: {
//                    idperfil: idperfil,
//                    nombre_perfil: nombre
//                },
//                success: function (result) {
//                    Swal.fire({
//                        type: (result.COD_ESTADO == 1 ? 'success' : 'error'),
//                        title: result.DES_ESTADO,
//                        showConfirmButton: false,
//                        timer: 2500
//                    });

//                    if (result.COD_ESTADO == 1) {
//                        ListarPerfil(val_modalidad);
//                    }
//                }
//            }, JSON);
//        }
//    });
//}



//function seleccionaFila(elementFila) {

//    $.each($('#tbPerfil tbody > tr'), function () {
//        $(this).css('background-color', '');
//    });
//    elementFila.css('background-color', '#17a2b838');
//}


