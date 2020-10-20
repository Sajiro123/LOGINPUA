
var CONTRASEÑABD = ""
var IDUSUARIO_PERFIL = "";

$(document).ready(function () {

    $("#button_cambio").on("click", function () {
        $('#fieldset_buton').toggle();
    });

    $('#AbexaRadio1').on('click', function () {
        if ($(this).is(':checked')) {
            $('#div_abexa').removeAttr('style')
        } else {
            $('#div_abexa').css('display', 'none')
        }
    });

    $('#MatrixRadio2').on('click', function () {
        if ($(this).is(':checked')) {
            $('#div_matrix').removeAttr('style')
        } else {
            $('#div_matrix').css('display', 'none')
        }
    });


    ListarProveedorServ();
    ListarPerfil_modalidad();
    getlistaUsuarios();

    $('#idpersona').on('change', function () {
        var person = $("#idpersona option:selected").text();
        var nombre = person.split(" ")
        var apellido = nombre[2];

        var nombre_final = nombre[0].toUpperCase()
        person = (apellido.substr(0, 1)).toUpperCase()
        var usuario = person + nombre_final
        $("#usuario").val(usuario);
    });
});


//CADA VEZ DE ABRIR EL MODAL AGREGAR
$("body").on("click", "#modal_registrousu", function () {
    $("#txtcontraseña_prov").empty();


    //LIMPIAR LOS INPUTS DEL MODAL REGISTRO
    $('#usuario').val(' ');
    $('#clave2').val('');
    $('#clave1').val('');
    $('#idpersona').val(0);
    $('#id_CORREDORES').val(0);
    $('#id_COSAC').val(0);
    $('#idpersona').selectpicker('refresh')
    $('#txtUsuarioProv').val(0);
    $('#txtcontraseña_prov').val('');





    ListarPersonas()

});


//CADA VEZ DE CAMBIA
$("body").on("change", "#txtUsuarioProv", function () {
    $("#txtcontraseña_prov").empty();
    var contraseñaProvers = $("#txtUsuarioProv option:selected").attr('data-contrseña');
    $("#txtcontraseña_prov").val(contraseñaProvers);
});



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
            $('#txtPROVEEDORSERV').val(2);
            $("#txtPROVEEDORSERV option[value='1']").remove();

            ListCorredores(1);
            ListCuentaxid($('#txtPROVEEDORSERV').val());
        }
    }, JSON);
}

function AgregarUsuarios() {

    var perfilxModalidad = '';
    var clave2 = $('#clave2').val();
    var clave1 = $('#clave1').val();
    var usuario = $("#usuario").val();
    var idcorredores_Abexa = $('#id_corredores').val();
    var idcorredores_Matrix = $('#id_corredor_MATRIX').val();


    var idpersona = $("#idpersona").val();

    if (usuario === "") { Swal.fire({ title: 'Seleccionar Persona', type: 'warning', }); return false; }
    if (clave2 !== clave1) { Swal.fire({ title: 'Ingresar la misma contraseña', type: 'warning', }); return false; }

    if (idpersona === "0") { Swal.fire({ title: 'Vuelve a Seleccionar Persona', type: 'warning', }); return false; }

    //OBTENGO LOS PERFILES PARA AGREGAR POR CLASE

    $.each($('.selectModalidadPerfil'), function () {
        var perfilSeleccionado = Number($(this).val());
        if (perfilSeleccionado != 0) {
            perfilxModalidad += $(this).val() + '|';
        }
    });
    perfilxModalidad = perfilxModalidad.substr(0, perfilxModalidad.length - 1);

    if (perfilxModalidad == "") { Swal.fire({ title: 'Seleccionar al menos un Perfil', type: 'warning', }); return false; }

    var txtUsuarioProv = $("#txtUsuarioProv").val();

      
    var idcorredorgeneral = "";
    var empresa = "";

    if ($('#AbexaRadio1').prop("checked") == true && $('#MatrixRadio2').prop("checked") == true) {
         
        //MATRIX
        if (txtUsuarioProv == null) { Swal.fire({ title: 'Seleccionar Cuenta del Usuario ', type: 'warning', }); return false; }
        if (idcorredores_Matrix.length == 0) { Swal.fire({ title: 'Seleccionar Corredor ', type: 'warning', }); return false; }
        if (idcorredores_Matrix.length != 0) {
            if (txtUsuarioProv == 0) {
                Swal.fire({ title: 'Seleccionar Cuenta del Usuario ', type: 'warning', }); return false;
            }
        }

        empresa = "MATRIX Y ABEXA"
        var id_corredor_stringMatrix = [];
        $.each(idcorredores_Matrix, function (idx2, val2) {
            var str = val2;
            id_corredor_stringMatrix.push(str);
        });
        var id_corredor_Matrix = id_corredor_stringMatrix.join("|");

        //ABEXA
        if (idcorredores_Abexa.length == 0) {
            Swal.fire({ title: 'Seleccionar Corredor ', type: 'warning', }); return false;
        }

        var id_corredor_stringAbexa = [];
        $.each(idcorredores_Abexa, function (idx2, val2) {
            var str = val2;
            id_corredor_stringAbexa.push(str);
        });
        var id_corredor_abexa = id_corredor_stringAbexa.join("|");

        //ABEXA + MATRIX
        idcorredorgeneral = id_corredor_Matrix +","+ id_corredor_abexa

        

    }
    else if ($('#AbexaRadio1').prop("checked") == false && $('#MatrixRadio2').prop("checked") == true) {

        
        //MATRIX
          
        if (txtUsuarioProv == null) { Swal.fire({ title: 'Seleccionar Cuenta del Usuario ', type: 'warning', }); return false; }
        if (idcorredores_Matrix.length == 0) { Swal.fire({ title: 'Seleccionar Corredor ', type: 'warning', }); return false; }
        if (idcorredores_Matrix.length != 0) {
            if (txtUsuarioProv == 0) {
                Swal.fire({ title: 'Seleccionar Cuenta del Usuario ', type: 'warning', }); return false;
            }
        }

        empresa = "MATRIX"
        var id_corredor_stringMatrix = [];
        $.each(idcorredores_Matrix, function (idx2, val2) {
            var str = val2;
            id_corredor_stringMatrix.push(str);
        });
        var id_corredor_Matrix = id_corredor_stringMatrix.join("|");
        idcorredorgeneral = id_corredor_Matrix
    }
    else if ($('#AbexaRadio1').prop("checked") == true && $('#MatrixRadio2').prop("checked") == false) {

        //ABEXA 

        if (idcorredores_Abexa.length == 0) {
            Swal.fire({ title: 'Seleccionar Corredor ', type: 'warning', }); return false;
        }
        empresa = "ABEXA"
        var id_corredor_stringAbexa = [];
        $.each(idcorredores_Abexa, function (idx2, val2) {
            var str = val2;
            id_corredor_stringAbexa.push(str);
        });
        var id_corredor_abexa = id_corredor_stringAbexa.join("|");
        idcorredorgeneral = id_corredor_abexa
    }



    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');

    $.ajax({
        url: URL_GET_AGREGAR_USUARIOS,
        data: {
            idpersona: idpersona,
            perfiles: perfilxModalidad,
            usuarios: usuario,
            contraseña: clave1,
            txtUsuarioProv: txtUsuarioProv == null ? txtUsuarioProv = 0 : txtUsuarioProv,
            idcorredores: idcorredorgeneral,
            empresa: empresa
            
        },
        dataType: 'json',
        success: function (result) {
            $('#consultar').prop('disabled', false).html('Guardar');

            $('input[type="text"]').val('');
            $('select').val('0');


            getlistaUsuarios();

            if (result.COD_ESTADO == 0) {
                $("#usuario").prop('disabled', false);
                $("#usuario").focus();
            } else {
                $('#exampleModal').modal('hide');

            }


            Swal.fire({
                type: result.COD_ESTADO == 1 ? 'success' : 'error',
                title: result.DES_ESTADO,
                showConfirmButton: false,
            });


        }
    }, JSON);
}


function ListCuentaxid(id_prov_serv_) {
    $('#txtUsuarioProv').empty()
    $('.selectpicker').selectpicker('refresh');

    $.ajax({
        url: URL_GET_LIST_CUENTAS_X_ID,
        dataType: 'json',
        data: {
            id_prov_serv: id_prov_serv_,
        },
        success: function (result) {

            if (result.length == 0) { // si la lista esta vacia
                $('#txtUsuarioProv').append('<option value="0">' + '--no hay información--' + '</option>');
                return false;
            } else {
                $.each(result, function () {
                    $('#txtUsuarioProv').append('<option data-contrseña="' + this.CONTRASENA + '" value="' + this.ID_PROV_USUARIO + '">' + this.USUARIO + '</option>');
                });
            }
            $('#txtUsuarioProv').append('<option value="0" selected="selected" disabled>Seleccionar Cuenta</option>');

            $('.selectpicker').selectpicker('refresh');
        }
    }, JSON);
}

function ListCorredores(id_prov_serv_) {

    $.ajax({
        url: URL_GET_LIST_CORREDOR,
        dataType: 'json',
        data: {
            id_prov_serv: id_prov_serv_,
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

            $('#id_corredor_MATRIX').multiselect({
                columns: 1,
                selectedOptions: 'PHP',
                placeholder: 'Seleccionar Corredor'
            });


            $('#div_context').children('div').css("width", "137%")
            $('#div_context').children().children('button').css("border", "1px solid #ced4da")

            $('#div_context_Matrix').children('div').css("width", "137%")
            $('#div_context_Matrix').children().children('button').css("border", "1px solid #ced4da")


        }
    }, JSON);
}



function getlistaUsuarios() {

    // PERMISOS
    //1= EDITAR || 2==ELIMINAR 
    var permiso_edit = $('#permisoActualiza').val();
    var permiso_eliminar = $('#permisoEliminar').val();

    $.ajax({
        url: URL_GET_LISTAR_USUARIO,
        dataType: 'json',
        success: function (result) {
            var strHTML = '';
            var i = 0;
            DATA_LISTA_REGISTROS = result;
            $.each(result, function () {
                var usuario = this.ID_ESTADO
                var clave = this.CLAVE
                var tipo = ""

                if (usuario == 1) {
                    usuario = "ACTIVO"
                    tipo = '<span class="badge badge-pill badge-success">' + usuario + '</span>'

                }
                else if (usuario == 2) {
                    usuario = "DESACTIVADO"
                    tipo = '<span class="badge badge-pill badge-danger">' + usuario + '</span>'
                }


                i++;

                $('#tbUsuario tbody').empty();

                strHTML += '<tr onclick="seleccionaFila($(this))" ' +
                'data-USUARIO="' + this.ID_USUARIO + '"' +
                'data-CLAVE="' + this.CLAVE + '"' +
                 'data-NOMBRE="' + this.NOMBRE + '"' +
                'data-APEPAT="' + this.APEPAT + '"' +
                'data-APEMAT="' + this.APEMAT + '"' +
                'data-ID_ESTADO ="' + this.ID_ESTADO + '"' +
                'data-ID_ESTADO_NOMBRE ="' + usuario + '"' +
                'data-sr ="' + clave + '"' +

                 'data-FECHA_REG     ="' + this.FECHA_REG + '">' +
                               '<td> ' + i + '</td>' +
                                '<td>' + this.USUARIO + '</td>' +
                                '<td>' + this.CLAVE + '</td>' +
                                 '<td>' + this.NOMBRE + '</td>' +
                                '<td>' + this.APEPAT + '</td>' +
                                '<td>' + this.APEMAT + '</td>' +
                                '<td class="text-center">' + tipo + '</td>' +
                                 '<td class="text-center">' + this.USU_REG + '</td>' +
                                '<td class="text-center">' + this.FECHA_REG + '</td>' +
                                '<td>' + '<span class="far fa-trash-alt" aria-hidden="true" style="cursor:pointer;" onclick="confirmarAnulacionUsuario(' + this.ID_USUARIO + ');" ></span>' + '</td>'+
                             '<td>' + '<button  onclick="EditarUsuario($(this))" type="button" class="btn btn-sm" data-toggle="modal" data-target="#EditarModal"><span class="far fa-edit btn-edit" aria-hidden="true" style="cursor:pointer;"></span></button>' + '</td>'

                                //(permiso_eliminar == 1 ? '<td>' + '<span class="far fa-trash-alt" aria-hidden="true" style="cursor:pointer;" onclick="confirmarAnulacionUsuario(' + this.ID_USUARIO + ');" ></span>' + '</td>' : '') +
                                //(permiso_edit == 1 ? '<td>' + '<button  onclick="EditarUsuario($(this))" type="button" class="btn btn-sm" data-toggle="modal" data-target="#EditarModal"><span class="far fa-edit btn-edit" aria-hidden="true" style="cursor:pointer;"></span></button>' + '</td>' : '') +
                            '</tr>';
            });
            $('#tbUsuario tbody').append(strHTML);
            util.activarEnumeradoTabla('#tbUsuario', $('#btnBusquedaEnTabla'));

        }
    }, JSON);
}
var NOMBRE_MODALIDAD = "";
function EditarUsuario(element) {

    //$("#id_corredor_ABEXA_EDIT option:selected").removeAttr("selected");


 
    $('#txtmodalidad_perfil').empty()

    //CLONAMOS LOS PERFILES POR MODALIDADES
    var selectperfil = $('#txtperfil').clone();
    $('#txtmodalidad_perfil').append(selectperfil)

    //CAMBIAMOS EL ID CLONADO Y LA CLASE EN LA MODALIDAD
    $.each($('#txtmodalidad_perfil').find('select'), function (i) {
        var idSelect = $(this).attr('id');
        $(this).attr('id', idSelect + '_' + 'edit');
        $(this).removeClass('selectModalidadPerfil').addClass('selectModalidadPerfilEdit');
    });
    
    var id_usuario = element.parents("tr").attr('data-USUARIO');
    $('#clave_edit').val('');
    
    //TRAEMOS INFORMACION DEL USUARIO
    $.ajax({
        url: URL_GET_LISTAR_USUARIO_ID,
        data: { idusuario: id_usuario },
        dataType: 'json',
        success: function (result) {

            CONTRASEÑABD = "";
            IDUSUARIO_PERFIL = "";

            var usuario = result[0].USUARIO
            var id_estado = result[0].ID_ESTADO
            IDUSUARIO_PERFIL = result[0].ID_USUARIO;


            CONTRASEÑABD = result[0].CLAVE


            $('#usuario_edit').val(usuario);
            $('#idestado_edit').val(id_estado);

            var modalidades = result[0].IDMODALIDAD
            var perfiles = result[0].PERFILES

            if (perfiles === null) {

            } else {
                modalidades = modalidades.split('|');
                perfiles = perfiles.split('|');


                $.each(perfiles, function (key, values) {
                    var arrData = values.split(',')
                    var id_perfil = Number(arrData[0]);
                    var nombre = arrData[1]
                    var modalidad = arrData[2]

                    $('#id_' + modalidad + '_edit').val(id_perfil)
                });
            }


        }
    }, JSON);


    $.ajax({
        url: URL_GET_CONSULTAR_CUENTAPROVUSU,
        data: { id_usuario: id_usuario },
        dataType: 'json',
        success: function (result) {
            console.log(result, 'result')

            if (result.length == 0) {
                $('#fieldset_buton_edit').css("display", "none");
            } else {
                $('#fieldset_buton_edit').css("display", "block");

            }

            //fieldset_buton_edit

            $("#div_abexa_edit").css("display", "none");
            $("#div_matrix_edit").css("display", "none");


            $("#div_context_ABEXA").empty();
            $("#div_context_MATRIX").empty();
            
 
            $('#div_context_ABEXA').append('<div id="div_context_abexa_EDIT"><select disabled multiple id="id_corredor_ABEXA_EDIT"></select></div>')
            $('#div_context_MATRIX').append('<div id="div_context_Matrix_EDIT"><select disabled multiple id="id_corredor_MATRIX_EDIT"></select></div>')


            var agrupadoPorPROV_SERV = _.groupBy(result, function (d) { return d.ID_PROV_SERV })  //agrupado por fechas la data del comparativo A
 
            //ABEXA
             if ($(agrupadoPorPROV_SERV[1]).length != 0) {
                 $("#div_abexa_edit").css("display", "block");

                 //ELIMINAR DUPLICIADOS ABEXA
                
  
                $.each(agrupadoPorPROV_SERV[1], function () {
                    $('#id_corredor_ABEXA_EDIT').append('<option value="' + this.ID_CORREDOR + '">' + this.ABREVIATURA + '</option>');
                });

                var txtusuario_abexa = _.uniqBy(agrupadoPorPROV_SERV[1], function (e) {
                    return e.USUARIO;
                });

                $('#txtabexa_usuario_edit').val(txtusuario_abexa[0].USUARIO)
                $('#txtabexa_contraseña_edit').val(txtusuario_abexa[0].CONTRASENA)
                  
                 

                $('#id_corredor_ABEXA_EDIT').multiselect({
                    columns: 1,
                    selectedOptions: 'PHP',
                    placeholder: 'Seleccionar Corredor'
                });

                $('#div_context_abexa_EDIT').children('div').css("width", "137%")
                $('#div_context_abexa_EDIT').children().children('button').css("border", "1px solid #ced4da")
                $('#div_context_abexa_EDIT').children('div').children('button').css("color", "initial")
                $('#div_context_abexa_EDIT').children('div').children('button').css("background", "white")

                var corredorUsuario = [];
                $.each(agrupadoPorPROV_SERV[1], function () {
                    corredorUsuario.push(this.ID_CORREDOR)
                });

                $.each($('#id_corredor_ABEXA_EDIT').parent().find('ul > li'), function () {
                    var idCorredor = Number($(this).find('input').val());
                    if (corredorUsuario.indexOf(idCorredor) != -1) {
                        $(this).find('input').click();
                        $(this).addClass('selected');
                    }
                });
            }
            
             if ($(agrupadoPorPROV_SERV[2]).length != 0) {
                 $("#div_matrix_edit").css("display", "block");

                 //MATRIX
                 $.each(agrupadoPorPROV_SERV[2], function () {
                     $('#id_corredor_MATRIX_EDIT').append('<option value="' + this.ID_CORREDOR + '">' + this.ABREVIATURA + '</option>');
                 });


                 var txtusuario_matrix = _.uniqBy(agrupadoPorPROV_SERV[2], function (e) {
                     return e.USUARIO;
                 });
 
                 $('#txtUsuarioProvMATRIX_EDIT').val(txtusuario_matrix[0].USUARIO)
                 $('#txtcontraseña_MATRIX_edit').val(txtusuario_matrix[0].CONTRASENA)

                 
               

                 $('#id_corredor_MATRIX_EDIT').multiselect({
                     columns: 1,
                     selectedOptions: 'PHP',
                     placeholder: 'Seleccionar Corredor'
                 });

                 $('#div_context_Matrix_EDIT').children('div').css("width", "137%")
                 $('#div_context_Matrix_EDIT').children().children('button').css("border", "1px solid #ced4da")
                 $('#div_context_Matrix_EDIT').children('div').children('button').css("color", "initial")
                 $('#div_context_Matrix_EDIT').children('div').children('button').css("background", "white")

                 var corredorUsuario = [];
                 $.each(agrupadoPorPROV_SERV[2], function () {
                     corredorUsuario.push(this.ID_CORREDOR)
                 });

                 $.each($('#id_corredor_MATRIX_EDIT').parent().find('ul > li'), function () {
                     var idCorredor = Number($(this).find('input').val());
                     if (corredorUsuario.indexOf(idCorredor) != -1) {
                         $(this).find('input').click();
                         $(this).addClass('selected');
                     }
                 });


             }


            //$('#div_context_Matrix').children('div').css("width", "137%")
            //$('#div_context_Matrix').children().children('button').css("border", "1px solid #ced4da")


            

        }
    }, JSON);






}

function Update_Usuario() {
    var perfiles = "";
    var clavefinal = "";
    var estado_contraseña = "";
    var usuario = $('#usuario_edit').val();
    var estado_usuario = $('#idestado_edit').val();
    var clave_usuario = $('#clave_edit').val();

    var idcosac = $('#id_COSAC_edit').val();
    var idcorredor = $('#id_CORREDORES_edit').val()


    if (idcorredor === "0" && idcosac === "0") {
        Swal.fire({ title: 'Seleccionar al menos un Perfil', type: 'warning', }); return false;
    }
    if (idcorredor === "0" && idcosac == null) {
        Swal.fire({ title: 'Seleccionar al menos un Perfil', type: 'warning', }); return false;
    }
    if (idcorredor == null && idcosac === "0") {
        Swal.fire({ title: 'Seleccionar al menos un Perfil', type: 'warning', }); return false;
    }
    if (idcorredor == null && idcosac == null) {
        Swal.fire({ title: 'Seleccionar al menos un Perfil', type: 'warning', }); return false;
    }

    if (idcosac == "0" || idcosac == null && idcorredor != 0) {
        idperfiles = idcorredor
    } else if (idcorredor == "0" || idcorredor == null && idcosac != 0) {
        idperfiles = idcosac
    }

    else if (idcosac != 0 && idcorredor != 0) {
        idperfiles = idcosac + ',' + idcorredor
    }


    if (clave_usuario === "") {
        clavefinal = CONTRASEÑABD
        estado_contraseña = "encriptado"
    } else {
        clavefinal = clave_usuario
        estado_contraseña = "sin encriptado"

    }
    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Consultando...');



    $.ajax({
        url: URL_GET_MODIFICAR_USUARIO,
        dataType: 'json',
        data: {
            idusuario: IDUSUARIO_PERFIL,
            clave: clavefinal,
            estado_contraseña: estado_contraseña,
            estado_usuario: estado_usuario,
            idperfiles: idperfiles

        },
        success: function (result) {
            $('#consultar').prop('disabled', false).html('Guardar');

            getlistaUsuarios();
            Swal.fire({
                type: result.COD_ESTADO == 1 ? 'success' : 'error',
                title: result.DES_ESTADO,
                showConfirmButton: false,
            });


            $('#EditarModal').modal('hide');
        }
    }, JSON);

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
                url: URL_ANULA_USUARIO,
                dataType: 'json',
                data: { idusuario: idusuario },
                success: function (result) {
                    getlistaUsuarios();
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
    $.each($('#tbUsuario tbody > tr'), function () {
        $(this).css('background-color', '');
    });
    elementFila.css('background-color', '#17a2b838');
}

function ListarPerfil_modalidad() {


    $.ajax({
        url: URL_GET_PERFIL_MODALIDAD,
        dataType: 'json',
        success: function (result) {

        }
    }, JSON);
}

function ListarPersonas() {
    $.ajax({
        url: URL_GET_LISTAR_PERSONA,
        dataType: 'json',
        success: function (result) {
            console.log(result)
            $('#idpersona').empty();
            if (result.length == 0) { // si la lista esta vacia
                $('#idpersona').append('<option value="0">' + '--No hay información--' + '</option>');
            } else {
                $.each(result, function () {
                    $('#idpersona').append('<option value="' + this.ID_PERSONA + '">' + this.APEPAT + ' ' + this.APEMAT + ', ' + this.NOMBRES + '</option>');
                });
            }
            $('#idpersona').append('<option value="0" selected="selected">Seleccionar Persona</option>');

            $('.selectpicker').selectpicker('refresh');

        }
    }, JSON);
}



function ListarPerfil_modalidad(idmodalidad) {
    var idmodalidad = [];


    $.ajax({
        url: URL_GET_PERFIL_MODALIDAD,
        dataType: 'json',
        success: function (result) {

            var agrupado = _.groupBy(result, function (d) { return d.MODALIDAD })  //agrupado por fechas la data del comparativo A

            $.each(agrupado, function (key, values) {

                $("#txtperfil").append('<div class="col-sm-2">' +
                           '  <label class="font-weight-bold">' + key + '</label></div>' +
                      '<div class="col-sm-3">' +
                         ' <select  id=id_' + key + ' class="form-control selectModalidadPerfil" style="width:130%"> </select>' +
                      '</div><div class="col-sm-1"> ');
                $('#id_' + key + '').append('<option value="0" selected="selected">Seleccionar Modalidad</option>');

                $.each(values, function () {
                    $('#id_' + key + '').append('<option value="' + this.ID_PERFIL + '">' + this.PERFIL + '</option>');
                });
            });
        }
    }, JSON);
}


function abrirModalRegistro() {
    ListarPersonas()
}

