var DIV_FOLDER = ""
var DIV_FOLDER_FINAL = ""



$(document).ready(function () {
    ListarModalidad()

    $('#menu_padre').append('<option value="0" selected="selected" disabled>Seleccionar menú</option>');

    document.getElementById("contenido").style.display = "none";


    $('input:radio[name=customRadio]').change(function () {
        if (this.value == 'customRadio1') {
            document.getElementById("contenido").style.display = "none";
            $("#id_modulo").prop("disabled", false);
        }
        else if (this.value == 'on') {//submenu
            document.getElementById("contenido").style.display = "block";
        }
    });
    //LISTAR MENUS POR MODULOS
    $('#id_modulo').on('change', function () {


        var id = $("#id_modulo option:selected").val();
        var idmodalidad_ = $("#idmodalidad").val();

        if (idmodalidad_ == null) {
            Swal.fire({ title: 'Seleccionar Modalidad', type: 'warning', }); return false;
            selectperfil.find('select').val(perfil);

        }
        $.ajax({
            url: URL_GET_MODULOS_X_ID,
            data: {
                id: id,
                idmodalidad: idmodalidad_
            },
            dataType: 'json',
            success: function (result) {

                $('#menu_padre').empty();

                if (result.length == 0) { // si la lista esta vacia
                    $('#menu_padre').append('<option value="0">' + '--No hay información--' + '</option>');
                } else {
                    $.each(result, function () {
                        $('#menu_padre').append('<option value="' + this.ID_MENU + '">' + this.NOMBRE + '</option>');
                    });
                }
            }
        }, JSON);
    });


    $('#idmodalidad').on('change', function () {


        ListarModulo($(this).find("option:selected").val())
        ListarMenu_Padre($(this).find("option:selected").val());
    });
});





$("body").on("click", "#modal_registromenu", function () {

    $('#nombre').val('');
    $('#url').val('');
    $('#id_modulo').val(0);
    $('#menu_padre').val('');
});





function ListarModulo(idmodalidad) {
    $('#mensaje_menu').html('ESTRUCTURA DE MENÚ PARA' + ' <p style="color: #004e86;">' + $('#idmodalidad option:selected').text() + '</p>');
    $('#id_modulo').empty();
    $.ajax({
        url: URL_GET_MODULOS,
        data: { id_modalidad: idmodalidad },
        dataType: 'json',
        success: function (result) {
            $('#id_modulo').append('<option value="0" selected="selected" disabled>Seleccionar Modulo</option>');

            if (result.length == 0) { // si la lista esta vacia
                $('#id_modulo').append('<option value="0">' + '--No hay información--' + '</option>');
            } else {
                $.each(result, function () {
                    $('#id_modulo').append('<option value="' + this.ID_MODULO_SISTEMA + '">' + this.NOMBRE + '</option>');
                });
            }
        }
    }, JSON);
}

function ListarModalidad() {
    $.ajax({
        url: URL_GET_LISTAR_MODALIDAD,
        dataType: 'json',
        success: function (result) {

            $('#idmodalidad').empty();
            if (result.length == 0) { // si la lista esta vacia
                $('#idmodalidad').append('<option value="0">' + '--No hay información--' + '</option>');
                return false;
            } else {
                $.each(result, function () {
                    $('#idmodalidad').append('<option value="' + this.ID_MODALIDAD_TRANS + '">' + this.NOMBRE + '</option>');
                });
            }
            ListarModulo($('#idmodalidad').val())
            ListarMenu_Padre($('#idmodalidad').val());
        }
    }, JSON);
}




function ListarMenu_Padre(idmodalidad_) {

    // PERMISOS
    //1= EDITAR || 2==ELIMINAR 
    var permiso_edit = $('#permisoActualiza').val();
    var permiso_eliminar = $('#permisoEliminar').val();


    $('#menu_padre').empty();
    $('#menu_principal').empty();
    var a = 0;
    $.ajax({
        url: URL_GET_MENU_PADRE,
        data: { id_modalidad: idmodalidad_ },
        dataType: 'json',
        success: function (result) {
            var dataMenu = [];
            //var dataPadres = [];
            var agrupadoPorIdModulo = _.groupBy(result, function (d) { return d.ID_MODULO })  //agrupado por fechas la data del comparativo A

            $.each(agrupadoPorIdModulo, function (key, arrMenuPadreHijos) {
                var item = {
                    modulo: {
                        nombre: arrMenuPadreHijos[0].MODULO,
                        idModulo: arrMenuPadreHijos[0].ID_MODULO,
                        padres: []
                    }
                }
                //PINTAR MODULOS

                //<li></li>
                var menu_principal = $("#menu_principal");
                menu_principal.append("<li id='modulo_inicio" + item.modulo.idModulo + "'></li>");

                //<span></span> 
                var id_padre = $("#modulo_inicio" + item.modulo.idModulo);
                id_padre.append("<span class='' id='accion_modulo" + item.modulo.idModulo + "'></span>");
                //<ul></ul>
                id_padre.append("<ul id='padre_id_" + item.modulo.idModulo + "'></ul>");

                //<a></a> 
                var id_modulo_accion = $("#accion_modulo" + item.modulo.idModulo);
                id_modulo_accion.append("<a id='accion_modulo_final_" + item.modulo.idModulo + "'   style='text-decoration:none;' data-toggle='collapse' href='#modulo_" + item.modulo.idModulo + "' aria-expanded='true' aria-controls='modulo_" + item.modulo.idModulo + "'></a>");

                //<i></i>
                var a_accion = $("#accion_modulo_final_" + item.modulo.idModulo);
                a_accion.append("<i class='collapsed'><i class='fas fa-plus-square'></i></i>");
                a_accion.append("<i class='expanded'><i class='far fa-minus-square'></i></i>" + " " + item.modulo.nombre);

                //<div></div>
                var id_padre_HIJO = $("#padre_id_" + item.modulo.idModulo);
                id_padre_HIJO.append("<div  class='collapse show' id='modulo_" + item.modulo.idModulo + "' ></div>");



                $.each(arrMenuPadreHijos, function () {
                    if (Number(this.IS_PADRE) == 1) {
                        var itemPadre = {
                            idMenu: this.ID_MENU,
                            nombreMenu: this.NOMBRE,
                            hijos: []
                        }
                        item.modulo.padres.push(itemPadre);
                        //PINTAR PADRES MENUS

                        //<li></li>

                        var id_padre_final = $("#modulo_" + item.modulo.idModulo);
                        id_padre_final.append("<li id='li_padre" + itemPadre.idMenu + "'></li>");

                        //<span></span> 
                        var li_padre = $("#li_padre" + itemPadre.idMenu);
                        li_padre.append("<span id='accion_padre" + itemPadre.idMenu + "'></span>");

                        //<ul></ul>
                        li_padre.append("<ul id='final_id_" + itemPadre.idMenu + "'></ul>");

                        //<a></a> 
                        var a_accion_padre = $("#accion_padre" + itemPadre.idMenu);
                        a_accion_padre.append("<a id='accion_padre_final_" + itemPadre.idMenu + "'   style='text-decoration:none;' data-toggle='collapse' href='#folder_" + itemPadre.idMenu + "' aria-expanded='false' aria-controls='folder_" + itemPadre.idMenu + "'></a>");

                        //<i></i>
                        var a_accion_padre_final = $("#accion_padre_final_" + itemPadre.idMenu);
                        a_accion_padre_final.append("<i class='collapsed'><i class='fas fa-plus-square'></i></i>");
                        a_accion_padre_final.append("<i class='expanded'><i class='far fa-minus-square'></i></i>" + "<spon data-menu-padre=" + itemPadre.idMenu + " > " + itemPadre.nombreMenu + "</spon>&nbsp&nbsp&nbsp&nbsp" + (permiso_edit == 1 ? "<i class='fas fa-edit edit'></i>" : '') + (permiso_eliminar == 1 ? "<span  class='fas fa-ban delete'  aria-hidden='true' style='cursor:pointer;' onclick='Eliminar_Menu(" + itemPadre.idMenu + ',"' + itemPadre.nombreMenu + '"' + ")';></span>" : ''));
                        DIV_FOLDER = $("#final_id_" + itemPadre.idMenu);

                        //<div></div>
                        DIV_FOLDER.append("<div  class='collapse' id='folder_" + itemPadre.idMenu + "' ></div>");
                        DIV_FOLDER_FINAL = $("#folder_" + itemPadre.idMenu);


                    }
                });



                $.each(item.modulo.padres, function (i, dataPadres) {

                    $.each(arrMenuPadreHijos, function () {
                        if (Number(this.IS_PADRE) != 1) {
                            if (dataPadres.idMenu == this.ID_MENU_PADRE) {
                                dataPadres.hijos.push(this);
                                //PINTAR HIJOS SUBMENUS

                                //<li></li>
                                $("#folder_" + this.ID_MENU_PADRE).append("<li><span><i class='far fa-file'></i><a href='#!' style='text-decoration: none;'><spon data-menu-padre=" + this.ID_MENU + " > " + this.NOMBRE + "</spon>&nbsp&nbsp&nbsp&nbsp" + (permiso_edit == 1 ? "<i class='fas fa-edit edit'></i>" : '') + (permiso_eliminar == 1 ? "<span class='fas fa-ban delete'  aria-hidden='true' style='cursor:pointer;' onclick='Eliminar_Menu(" + this.ID_MENU + ',"' + this.NOMBRE + '"' + ")';></span></a></span>" : '') + "</li>");
                            }
                        }

                    })

                });
                dataMenu.push(item);
            });

        }
    }, JSON);
}


function AgregarMenu() {

    var tipomenu = 0
    var nombre = $("#nombre").val();
    var url = $("#url").val();
    var icono = $("#icono").val();
    var tipo_menu_principal = $("#customRadio1").val();
    var tipo_menu_hijo = $("#customRadio2").val();
    var menu_padre = $("#menu_padre").val();
    var id_modulo = $("#id_modulo").val();

    var idmodalidad = $("#idmodalidad").val();


    if (nombre === "") {
        Swal.fire({ title: 'Ingresar el nombre del menú', type: 'warning', })
        return false;
    }
    if (url === "") {
        Swal.fire({ title: 'Ingresar la url', type: 'warning', })
        return false;
    }
    if (id_modulo === "0") {
        Swal.fire({ title: 'Ingresar modulo', type: 'warning', })
        return false;
    }

    $('#menu_principal').empty()


    if (document.getElementById('customRadio1').checked) {
        tipomenu = 1
        menu_padre = 0
    } else if (document.getElementById('customRadio2').checked) {
        tipomenu = 0
    }

    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Guardando...');

    $.ajax({
        url: URL_GET_AGREGAR_MENU,
        data: {
            nombre: nombre,
            url: url,
            icono: icono,
            tipomenu: tipomenu,
            menupadre: menu_padre,
            modulo: id_modulo,
            idmodalidad: idmodalidad
        },
        dataType: 'json',
        success: function (result) {
            $('#consultar').prop('disabled', false).html('Guardar');

            ListarMenu_Padre(idmodalidad);

            Swal.fire({
                type: result.COD_ESTADO == 1 ? 'success' : 'error',
                title: result.DES_ESTADO,
                showConfirmButton: false,
            });
            $('#exampleModal').modal('hide');
        }
    }, JSON);
}



$("body").on("click", ".edit", function (e) {
    //console.log(this);
    e.stopPropagation();


    if ($('#txtTemporal')[0]) {
        var texto = $('#txtTemporal').attr('data-textotemp');
        $('#txtTemporal').parent().parent().find('spon').html(texto);
    }

    //var elementoHTML = $(this).parent().find('spon').html();
    var textoMenu = $(this).parent().find('spon').text();
    $(this).parent().find('spon').html('<input id="txtTemporal" data-textotemp="' + textoMenu + '" onkeyup="cambiarEstadoInputTemporal($(this),event)" type="text" value="' + textoMenu + '" />').focus().select();

});


function cambiarEstadoInputTemporal(elemento, e) {

    var keyTecla = e.keyCode;
    var textoTemp = elemento.attr('data-textotemp');

    var id_edit = elemento.parent().attr('data-menu-padre');

    switch (keyTecla) {
        case 13: //  enter

            $.ajax({
                url: URL_EDITAR_MENU,
                dataType: 'json',
                data: {
                    id: id_edit,
                    nombre: $('#txtTemporal').val().trim()
                },
                success: function (result) {
                    ListarMenu_Padre($('#idmodalidad').val());
                    Swal.fire({
                        type: (result.COD_ESTADO == 1 ? 'success' : 'error'),
                        title: result.DES_ESTADO,
                        showConfirmButton: false,
                        timer: 2500
                    });
                }
            }, JSON);


            break;
        case 27: //escape
            elemento.parent().parent().find('spon').html(textoTemp);
            break;
        default:
            break;

    }

    //$(document).keyup(function (e) {
    //    if (e.keyCode === 13) $('.save').click();     // enter
    //    if (e.keyCode === 27) $('.cancel').click();   // esc
    //});
}
function Eliminar_Menu(id, nombre) {




    Swal.fire({

        html: "Estas seguro que deseas anular el menu <b> " + nombre + " </b>?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: URL_ANULAR_PERFIL,
                dataType: 'json',
                data: { id: id },
                success: function (result) {
                    ListarMenu_Padre($('#idmodalidad').val());
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
