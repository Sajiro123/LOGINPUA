var IDPERFILMENU = 0;
var ARRAYPERMISOS = "";

$(document).ready(function () {
    //ListarMenu_Padre();


    $('#selectPerfilModalidad').on('change', function () {
        ListarPermisos($(this).val());


    });
    $('#selectIdmodalidad').on('change', function () {
        //ListarPerfil($(this).find("option:selected").val());
        getPerfilesByIdModalidad($(this).val());
        ListarMenu_Padre($(this).val());


    });
    listarModalidad_Perfiles();
    ListarAcciones();
});





function clickInCheckbox(elemento, idMenu) {
    var perfil = $('#selectPerfilModalidad').val()

    //SE BUSCA SI EXISTE ELL MENU ID

    $.ajax({
        url: URL_GET_LISTAR_MENU_ID,
        data: {
            idmenu: idMenu,
            idperfil: perfil
        },
        dataType: 'json',
        success: function (result) {

            if (result.length <= 0) {

                //SE AGREGA NUEVO PERMISO

                $.ajax({
                    url: URL_GET_AGREGAR_PERMISO,
                    data: {
                        idperfil: perfil,
                        idmenu: idMenu
                    },
                    dataType: 'json',
                    success: function (result) {

                        alertify.set('notifier', 'position', 'top-right');
                        alertify.success('Se Agrego Correctamente ');

                        if (result.COD_ESTADO == 1) {
                            ListarMenu_Padre($('#selectIdmodalidad').val());
                        }
                    }
                }, JSON);
            }//SE MODIFICA EL PERMISO
            else {
                var idestado = 0;

                //SE VALIDA SI EL CHECKBOX ESTA ACTIVO 

                var checkMenuEstaCheckeado = elemento.is(':checked');
                if (checkMenuEstaCheckeado == true) {
                    idestado = 1;
                } else {
                    idestado = 3;
                }


                $.ajax({
                    url: URL_GET_MODIFICAR_MENU,
                    data: {
                        idmenu: idMenu,
                        idestado: idestado,
                        id_perfil: perfil
                    },
                    dataType: 'json',
                    success: function (result) {

                        alertify.set('notifier', 'position', 'top-right');
                        alertify.error('Se modifico correctamente ');


                        if (result.COD_ESTADO == 1) {
                            ListarMenu_Padre($('#selectIdmodalidad').val());
                        }
                    }
                }, JSON);
            }
        }
    }, JSON);
}


function ListarMenu_Padre(idmodalidad_) {
    $('#menu_principal').empty()
    $('#alerta').empty()

    var a = 0;
    $.ajax({
        url: URL_GET_MENU_PADRE,
        data: { idmodalidad: idmodalidad_ },
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
                id_padre.append("<span  style='width:auto;text-transform: uppercase;' id='accion_modulo" + item.modulo.idModulo + "'></span>");

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
                        li_padre.append("<span style='width:auto;text-transform: capitalize;font-weight: bolder;'  id='accion_padre" + itemPadre.idMenu + "'></span>");

                        //<ul></ul>
                        li_padre.append("<ul id='final_id_" + itemPadre.idMenu + "'></ul>");

                        //<a></a> 
                        var a_accion_padre = $("#accion_padre" + itemPadre.idMenu);
                        a_accion_padre.append("<a id='accion_padre_final_" + itemPadre.idMenu + "'   style='text-decoration:none;' data-toggle='collapse' href='#folder_" + itemPadre.idMenu + "' aria-expanded='true' aria-controls='folder_" + itemPadre.idMenu + "'></a>");
                        a_accion_padre.append("<input  id='check_" + itemPadre.idMenu + "' onclick='clickInCheckbox($(this)," + itemPadre.idMenu + ");' class='get_value checkmenu'  value='" + itemPadre.idMenu + "' type='checkbox'  style='width:20px; height: 20px;'>");



                        //<i></i>
                        var a_accion_padre_final = $("#accion_padre_final_" + itemPadre.idMenu);
                        a_accion_padre_final.append("<i class='collapsed'><i class='fas fa-plus-square'></i></i>");
                        a_accion_padre_final.append("<i class='expanded'><i class='far fa-minus-square'></i></i>" + " " + itemPadre.nombreMenu);
                        DIV_FOLDER = $("#final_id_" + itemPadre.idMenu);

                        //<div></div>
                        DIV_FOLDER.append("<div  class='' id='folder_" + itemPadre.idMenu + "' ></div>");
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
                                $("#folder_" + this.ID_MENU_PADRE).append("<li><span style='width:auto;text-transform: lowercase;'>&nbsp;<i class='far fa-file'></i><a href='#!' style='text-decoration: none;'><input id='check_" + this.ID_MENU + "' onclick='clickInCheckbox($(this)," + this.ID_MENU + ");' class='get_value checkmenu aqui' type='checkbox' style='width:20px; height:20px;'> " + "  " + this.NOMBRE + "</a> &nbsp;&nbsp;<a class='agregar_accion' onclick='abrirAgregarAcciones($(this));'></a></span><ul></ul> ");
                            }
                        }

                    })

                });
                dataMenu.push(item);
            });
            ListarPermisos($('#selectPerfilModalidad').val());




        }

    }, JSON);

}

var JSON_MODALIDAD_PERFIL = [];

function listarModalidad_Perfiles() {
    $.ajax({
        url: URL_GET_PERFIL_MODALIDAD,

        dataType: 'json',
        success: function (result) {
            var agrupadoPoridModalidad = _.groupBy(result, function (d) { return d.ID_MODALIDAD_TRANS + "|" + d.MODALIDAD })  //agrupado por fechas la data del comparativo A

            $('#selectIdmodalidad').empty();
            var strSelectModalidad = '';
            //
            $.each(agrupadoPoridModalidad, function (key, values) {
                var idModalidad = key.split('|')[0];
                var nombreModalidad = key.split('|')[1];

                var item = {
                    idModalidad: Number(idModalidad),
                    perfiles: []
                }
                $('#selectIdmodalidad').append('<option value="' + idModalidad + '" >' + nombreModalidad + '</option>');
                $.each(values, function () {

                    var itemPerfil = {
                        idperfilModalidad: this.ID_PERFIL_MODALIDAD,
                        nombrePerfil: this.PERFIL
                    }
                    item.perfiles.push(itemPerfil);
                });
                JSON_MODALIDAD_PERFIL.push(item);
            });

            getPerfilesByIdModalidad($('#selectIdmodalidad').val());
            ListarMenu_Padre($('#selectIdmodalidad').val());

        }
    }, JSON);
}

function getPerfilesByIdModalidad(idModalidad) {
    $('#selectPerfilModalidad').empty();
    $.each(JSON_MODALIDAD_PERFIL, function () {
        if (Number(idModalidad) == this.idModalidad) {
            $.each(this.perfiles, function () {
                $('#selectPerfilModalidad').append('<option value="' + this.idperfilModalidad + '" >' + this.nombrePerfil + '</option>');
            });
        }
    });
}


//AQUI  SE OBTIENE LA DATA DE LAS ACCIONES


function ListarPermisos(idperfil_modalidad) {

    $.each($(".agregar_accion"), function (e,k) {
         $(k).empty()
    });

    $.each($('.aqui'), function (elemento, value) {
        $(value).parent().parent().parent().children('ul').empty()
    });


    $("input").removeAttr("data-id")
    clearChecksMenu();

    $.ajax({
        url: URL_GET_LISTAR_PERMISOS,
        data: {
            idperfil: idperfil_modalidad,
        },
        dataType: 'json',
        success: function (result) {
             $.each(result, function (i) {
 
              var limpiar_agregaracciones=  $('#check_' + this.ID_MENU).parent().parent().children()[2];
              $(limpiar_agregaracciones).empty()

              if (this.ID_ESTADO == 1) {
                  var limpiar_agregaracciones = $('#check_' + this.ID_MENU).parent().parent().children()[2];
                  $(limpiar_agregaracciones).append("<a class='agregar_accion' onclick='abrirAgregarAcciones($(this));'><span class='svg-inline--fa fa fa-plus'></span></a>")



                    $('#check_' + this.ID_MENU).prop('checked', true);
                    $('#check_' + this.ID_MENU).attr('data-id', this.ID_MENUSUARIOPERFIL)
                }
            })
            ListarAcciones_x_id($('#selectPerfilModalidad').val());

        }
    }, JSON);
}



function ListarAcciones_x_id(idperfil_modalidad) {

    $.ajax({
        url: URL_GET_LISTAR_ACCIONES_IDPERFIL,
        data: {
            idperfil: idperfil_modalidad,
        },
        dataType: 'json',
        success: function (result) {


            var agrupadoPorIdmenuperfil = _.groupBy(result, function (d) { return d.ID_MENUSUARIOPERFIL })  //agrupado por fechas la data del comparativo A
            $.each(agrupadoPorIdmenuperfil, function (key, array_accion) {

                var data_id = $('#check_' + array_accion[0].ID_MENU).parent().parent().parent().children('ul')

                $.each(array_accion, function () {
                    data_id = $(data_id).append('<li data-id="' + this.ID_ACCION + '"> &nbsp;&nbsp;&nbsp<i class="' + this.ICON_ACCION + '"></i>&nbsp;' + this.NOMBRE + '&nbsp;&nbsp;</li>')
                });
            })
        }
    }, JSON);
}



function ListarAcciones() {
    $('#modal_add').empty();
    $.ajax({
        url: URL_GET_LISTAR_ACCIONES,
        dataType: 'json',
        success: function (result) {

            $.each(result, function (i) {
                $('#modal_add').append('<tr><td>' + this.NOMBRE + '</td> <td><input class="check_accion" style="height:20px" type="checkbox" name="id_acciones[]" value="' + this.ID_ACCION + '"/></td></tr> ');
            })

        }
    }, JSON);
}


function clearChecksMenu() {
    $('.checkmenu').prop('checked', false);
}


function abrirAgregarAcciones(element) {        
    var elemento = element.parent().children()[1];
    var _input = $(elemento).children();
    var idmenuperfil = _input.attr('data-id')
    $(".check_accion").removeAttr('checked');
    $(".check_accion").prop('checked',false);
    ARRAYPERMISOS = "";
    idperfil_modalidad = $('#selectPerfilModalidad').val();
    $.ajax({
        url: URL_GET_LISTAR_ACCIONES_X_MENUPERFIL,
        data: {
            idperfil: idperfil_modalidad,
            idmenuperfil: idmenuperfil
        },
        dataType: 'json',
        success: function (result) {
            //OBTENGO EL IDPERFILMENU QUE SE ENCUENTRA EN UN DATA-ID DE UN INPUT
            var agrupadoPorIdmenuperfil = _.groupBy(result, function (d) { return d.ID_MENUSUARIOPERFIL })  //agrupado por fechas la data del comparativo A
            $.each(agrupadoPorIdmenuperfil, function (key, array_accion) {

                $.each(array_accion, function () {

                    var itemAccion = {
                        idAccion: this.ID_ACCION,
                    }

                    if (this.ID_MENUSUARIOPERFIL == idmenuperfil) {
                        $.each($('.check_accion'), function (elemento, value) {
                            if ($(value).attr('value') == itemAccion.idAccion) {
                                 $(value).attr('checked', true);
                                $(value).prop('checked', true);
                            }
                        });
                    }

                });
            })



            IDPERFILMENU = idmenuperfil;
            $('#add_accion').modal('show');

        }
    }, JSON);

}





function AgregarAcciones_Idmenuperfil() {

    if (typeof IDPERFILMENU == 'undefined') {
        Swal.fire({ title: 'No existe el acceso del menú', type: 'error', }); return false;
    }

    var acciones_checknam = '';

    $.each($('[name="id_acciones[]"]:checked'), function () {
        var idaccion = Number($(this).val());
        if (idaccion != 0) {
            acciones_checknam += $(this).val() + '|';
        }
    });

    if (acciones_checknam == "") { acciones_checknam = ""; }

    $.ajax({
        type: "POST",
        url: URL_GET_AGREGAR_ACCIONES,
        data: {
            idmenu_perfil: IDPERFILMENU,
            acciones: acciones_checknam
        },
        content: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (result) {

            ListarMenu_Padre($('#selectIdmodalidad').val());

            if (result.COD_ESTADO == 1) { $('#add_accion').modal('hide'); }


            Swal.fire({
                type: result.COD_ESTADO == 1 ? 'success' : 'error',
                title: result.DES_ESTADO,
                showConfirmButton: false,
            });

        }
    }, JSON);

}



