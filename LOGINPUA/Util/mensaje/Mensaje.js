var $Contenedor = $('<div style="width: 100%;height:100%;z-index:3500;position:absolute;top:0;left:0;opacity: 0.5;background:#000000;"></div>');
var $Mensaje = $('<div style="z-index:3510;background:#FFFFFF;position:absolute;"></div>');
var styleTitulo = "background:#3B5998;color:#FFFFFF;font-weight: bold;text-align:center;padding: 10px;";
var styleMensaje = "padding: 10px;border:none;";
var styleBotones = "";
var $ContenedorCargando = $('<div style="width: 100%;height:100%;z-index:4500;position:absolute;top:0;left:0;opacity: 0.5;background:#000000;"></div>');
var $MensajeCargando;

$(document).ready(function () {
    //alert(1);
    $('body').append($Contenedor);
    $('body').append($Mensaje);
    $('body').append($ContenedorCargando);
    $MensajeCargando = $('<div style="z-index:4510;background:#FFFFFF;position:absolute;"><img src="' + urlRutaMensaje + 'loading.gif" /></div>');
    $('body').append($MensajeCargando);
    //alert(2);
    $Contenedor.hide();
    $Mensaje.hide();
    $ContenedorCargando.hide();
    $MensajeCargando.hide();  
});

function MostrarMensaje(_param) {
    var tipo = 1;
    var mensaje = '';
    var titulo = 'Mensaje';
    var icono = '';
    if (_param.tipo == 1) {
        tipo = 1;
    }
    else if (_param.tipo == 2) {
        tipo = 2;
    }
    if (_param.mensaje != undefined && _param.mensaje != null) {
        mensaje = _param.mensaje;
    }
    if (_param.titulo != undefined && _param.titulo != null) {
        titulo = _param.titulo;
    }
    if (_param.icono != undefined && _param.icono != null) {
        if (_param.icono == 0) {
            icono = urlRutaMensaje + "error.png";
        }
        else if (_param.icono == 1) {
            icono = urlRutaMensaje + "bien.jpg";
        }
        else if (_param.icono == 2) {
            icono = urlRutaMensaje + "alerta.gif";
        }
    }
    $Mensaje.html("");
    if (titulo != '') {
        $Mensaje.append('<div style="' + styleTitulo + '">' + titulo + '</div>');
    }
    else {
        $Mensaje.append('<div style="' + styleTitulo + '">Alerta</div>');
    }
    $Mensaje.append('<table style="' + styleMensaje + '"><tr>' + (icono == '' ? '' : ('<td style="padding: 10px;"><img src="' + icono + '" style="width: 50px;height:50px;" /></td>')) + '<td style="padding: 10px;">' + mensaje + '</td></tr></table>');
    var $Botonera = $('<div style="text-align:center;padding: 10px;"></div>');
    if (tipo == 1) {
        var $BotonAceptar = $('<input type="button" value="Aceptar" class="btn btnfocus" />');
        if (_param.funcionAceptar != undefined && _param.funcionAceptar != null) {
            $BotonAceptar.click(function () {
                CerrarMensaje();
                _param.funcionAceptar();
            });
        }
        else {
            $BotonAceptar.click(function () {
                CerrarMensaje();
            });
        }
        $Botonera.append($BotonAceptar);
    }
    else if (tipo == 2) {
        var $BotonSi = $('<input type="button" value="Si" class="btn btnfocus" />');
        var $BotonNo = $('<input type="button" value="No" class="btn" />');
        if (_param.funcionSi != undefined && _param.funcionSi != null) {
            $BotonSi.click(function () {
                CerrarMensaje();
                _param.funcionSi();
            });
        }
        else {
            $BotonSi.click(function () {
                CerrarMensaje();
            });
        }
        if (_param.funcionNo != undefined && _param.funcionNo != null) {
            $BotonNo.click(function () {
                CerrarMensaje();
                _param.funcionNo();
            });
        }
        else {
            $BotonNo.click(function () {
                CerrarMensaje();
            });
        }
        $Botonera.append($BotonSi);
        $Botonera.append("&nbsp;&nbsp;");
        $Botonera.append($BotonNo);
    }
    $Mensaje.append($Botonera);
    $Mensaje.css({
        left: $(document).scrollLeft() + parseInt(($Contenedor.width() - $Mensaje.width()) / 2),
        top: $(document).scrollTop() + parseInt(($Contenedor.height() - $Mensaje.height()) / 2)
    });
    $Mensaje.show();
    $Contenedor.show();
    $(".btnfocus").focus();
}

function CerrarMensaje() {
    $Mensaje.html("");
    $Contenedor.hide();
    $Mensaje.hide();
}

function EvaluarResultado(result) {
    if (result.tipo == 1) {
        return true;
    }
    else if (result.tipo == -1)
    {
        MostrarMensaje({mensaje: "La sesión a caducado<br />¿Deseas volver a iniciar Sesión?", icono: 0, tipo: 2, funcionSi: function () {
                window.open(urlSitio + "/Home/Login?CADUCA=1", "", "width=375, height=406");
            }});
    }
    else if (result.tipo == 0)
    {
        //MostrarMensaje({mensaje: "Estamos teniendo problemas por favor intentelo luego", icono: 0});
        MostrarMensaje({mensaje: result.mensaje, icono: 0});
    }
    return false;
}

$(window).resize(function () {
    $Contenedor.css({left: $(document).scrollLeft(), top: $(document).scrollTop()});
    if ($Mensaje != null) {
        $Mensaje.css({
            left: $(document).scrollLeft() + parseInt(($Contenedor.width() - $Mensaje.width()) / 2),
            top: $(document).scrollTop() + parseInt(($Contenedor.height() - $Mensaje.height()) / 2)
        });
    }
    $ContenedorCargando.css({left: $(document).scrollLeft(), top: $(document).scrollTop()});
    if ($MensajeCargando != null) {
        $MensajeCargando.css({
            left: $(document).scrollLeft() + parseInt(($ContenedorCargando.width() - $MensajeCargando.width()) / 2),
            top: $(document).scrollTop() + parseInt(($ContenedorCargando.height() - $MensajeCargando.height()) / 2)
        });
    }
});

$(window).scroll(function () {
    $Contenedor.css({left: $(document).scrollLeft(), top: $(document).scrollTop()});
    if ($Mensaje != null) {
        $Mensaje.css({
            left: $(document).scrollLeft() + parseInt(($Contenedor.width() - $Mensaje.width()) / 2),
            top: $(document).scrollTop() + parseInt(($Contenedor.height() - $Mensaje.height()) / 2)
        });
    }
    $ContenedorCargando.css({left: $(document).scrollLeft(), top: $(document).scrollTop()});
    if ($MensajeCargando != null) {
        $MensajeCargando.css({
            left: $(document).scrollLeft() + parseInt(($ContenedorCargando.width() - $MensajeCargando.width()) / 2),
            top: $(document).scrollTop() + parseInt(($ContenedorCargando.height() - $MensajeCargando.height()) / 2)
        });
    }
});

function msgErrorAjax(error, textStatus, errorThrown) {
    MostrarMensaje({mensaje: error.status + "---" + error.statusText + "\n" + textStatus + "---" + errorThrown});
}

//----------------
//Mensaje Cargando
//----------------
$(document).ajaxStart(function () {
    $ContenedorCargando.show();
    $MensajeCargando.css({
        left: $(document).scrollLeft() + parseInt(($Contenedor.width() - $Mensaje.width()) / 2),
        top: $(document).scrollTop() + parseInt(($Contenedor.height() - $Mensaje.height()) / 2)
    });
    $MensajeCargando.show();
}).ajaxStop(function () {
    $ContenedorCargando.hide();
    $MensajeCargando.hide();
});


