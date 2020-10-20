$(document).ready(function () {



    getListConductores();
    
});

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

function getListConductores() {

    $('#Tbconductores tbody').empty();
    $('#consultar').prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>Mostrando...');
    DATA_LISTA_CONDUCTORES = "";
    var strHTML = '';


    $.ajax({
        url: URL_LIST_CONDUCTORES,
        dataType: 'json',
        success: function (result) {
            DATA_LISTA_CONDUCTORES = result
            //$('#listar_buses tbody').empty();

            console.log(DATA_LISTA_CONDUCTORES, 'DATA_LISTA_CONDUCTORES')

            $('#consultar').prop('disabled', false).html('Consultar');
            if (result.length <= 0) {
                strHTML += '<tr><td colspan="100" class="text-left" style="padding-left: 15%">No hay información para mostrar</td></tr>';

            } else {

                $.each(result, function (i) {

                    var VIGENCIA = Semaforo_Logica(this.VIGENCIA.substring(0, 10));

                    strHTML += '<tr  onclick="seleccionaFila($(this))" ' + '>' +


                        '<td>' + (i + 1) + '</td>' +
                        '<td class="text-center" >' + (this.CODIGO == null ? "" : this.CODIGO) + '</td>' +
                        '<td class="text-center"  >' + VIGENCIA + '</td>' +
                        '<td class="text-center textoAchicado"  title="' + (this.EMPRESA == null ? "" : this.EMPRESA) + '" >' + (this.EMPRESA == null ? "" : this.EMPRESA) + '</td>' +
                        '<td class="text-center textoAchicado"  title="' + (this.APELLIDOS == null ? "" : this.APELLIDOS) + '" >' + (this.APELLIDOS == null ? "" : this.APELLIDOS) + '</td>' +
                        '<td class="text-center textoAchicado"  title="' + (this.NOMBRES == null ? "" : this.NOMBRES) + '" >' + (this.NOMBRES == null ? "" : this.NOMBRES) + '</td>' +
                        '<td class="text-center" >' + (this.CONTIPLIC == null ? "" : this.CONTIPLIC) + '</td>' +
                        '<td class="text-center" >' + (this.CONNUMLIC == null ? "" : this.CONNUMLIC) + '</td>' +
                        '<td class="text-center" >' + (this.CONFECNAC == null ? "" : this.CONFECNAC.substring(0, 10)) + '</td>' +
                        '<td class="text-center" >' + (this.NUMDOC == null ? "" : this.NUMDOC) + '</td>' +
                        '<td class="text-center" >' + (this.INICIO == null ? "" : this.INICIO.substring(0,10)) + '</td>' +
                        '<td class="text-center" >' + (this.VIGENCIA == null ? "" : this.VIGENCIA.substring(0, 10)) + '</td>' +
                        '<td class="text-center" >' + (this.FECREG == null ? "" : this.FECREG.substring(0, 10)) + '</td>' +

                        '</tr>';

                });

            }
            $('#listar_conductores').append(strHTML);
            util.activarEnumeradoTabla('#Tbconductores', $('#btnBusquedaEnTabla'));


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
        fileName: "Reporte Conductores " + today
    };


    var tHeadExcel = [{ "text": "N°" },
        { "text": "CODIGO" },
        { "text": "EMPRESA" },
        { "text": "APELLIDOS" },
        { "text": "NOMBRES" },
        { "text": "CONTIPLIC" },
        { "text": "CONNUMLIC" },
        { "text": "CONFECNAC" },
        { "text": "NUMDOC" },
        { "text": "INICIO" },
        { "text": "VIGENCIA" },

    ]

    tableDataExportar[0].data.push(tHeadExcel);

    $.each(DATA_LISTA_CONDUCTORES, function (i) {

        var objeto = this;
        var itemArr = [];
        itemArr.push({ "text": (i + 1) },
            { "text": objeto.CODIGO },
            { "text": objeto.EMPRESA },
            { "text": objeto.APELLIDOS },
            { "text": objeto.NOMBRES },
            { "text": objeto.CONTIPLIC },
            { "text": objeto.CONNUMLIC },
            { "text": objeto.CONFECNAC },
            { "text": objeto.NUMDOC },
            { "text": objeto.INICIO },
            { "text": objeto.VIGENCIA }

        )
        tableDataExportar[0].data.push(itemArr);

    });
    Jhxlsx.export(tableDataExportar, options);
    $('#exportar').prop('disabled', false).html('Exportar');
}



function seleccionaFila(elementFila) {
    $.each($('#Tbconductores tbody > tr'), function () {
        $(this).css('background-color', '');
    });
    elementFila.css('background-color', '#17a2b838');
}
































////jQuery time
//var current_fs, siguiente_fs, atras_fs; //conjuntos de campo
//var left, opacity, scale; //propiedades fieldset que vamos a animar
//var animacion; //bandera para evitar problemas técnicos rápidos con varios clics

//$(".next").click(function () {
//    if (animacion) return false;
//    animacion = true;

//    current_fs = $(this).parent();
//    siguiente_fs = $(this).parent().next();

//    //activa el siguiente paso en la barra de progreso usando el índice de next_fs
//    $("#progressbar li").eq($("fieldset").index(siguiente_fs)).addClass("active");

//    //muestra el siguiente campo
//    siguiente_fs.show();
//    //ocultar el fieldset actual con estilo
//    current_fs.animate({ opacity: 0 }, {
//        step: function (now, mx) {
//            // como la opacidad de current_fs se reduce a 0 - almacenado en "ahora"
//            // 1. escala current_fs hasta 80%
//            scale = 1 - (1 - now) * 0.2;
//            // 2. traiga next_fs desde la derecha (50%)
//            left = (now * 50) + "%";
//            // 3. aumentar la opacidad de next_fs a 1 a medida que se mueve
//            opacity = 1 - now;
//            current_fs.css({
//                'transform': 'scale(' + scale + ')',
//                'position': 'absolute'
//            });
//            siguiente_fs.css({ 'left': left, 'opacity': opacity });
//        },
//        duration: 800,
//        complete: function () {
//            current_fs.hide();
//            animacion = false;
//        },
//        // esto viene del plugin easing personalizado
//        easing: 'easeInOutBack'
//    });
//});

//$(".previous").click(function () {
//    if (animacion) return false;
//    animacion = true;

//    current_fs = $(this).parent();
//    atras_fs = $(this).parent().prev();

//    // desactivar el paso actual en la barra de progreso
//    $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

//    // muestra el fieldset anterior
//    atras_fs.show();
//    // esconde el fieldset actual con estilo
//    current_fs.animate({ opacity: 0 }, {
//        step: function (now, mx) {
//            // como la opacidad de current_fs se reduce a 0 - almacenado en "ahora"
//            // 1. escala previous_fs de 80% a 100%
//            scale = 0.8 + (1 - now) * 0.2;
//            // 2. toma current_fs a la derecha (50%) - desde 0%
//            left = ((1 - now) * 50) + "%";
//            // 3. aumentar la opacidad de previous_fs a 1 a medida que se mueve
//            opacity = 1 - now;
//            current_fs.css({ 'left': left });
//            atras_fs.css({ 'transform': 'scale(' + scale + ')', 'opacity': opacity });
//        },
//        duration: 800,
//        complete: function () {
//            current_fs.hide();
//            animacion = false;
//        },
//        // esto viene del plugin easing personalizado
//        easing: 'easeInOutBack'
//    });
//});

//$(".submit").click(function () {
//    return false;
//})

//$('.action-button').click(function (e) {

//    $('html, body').animate({ scrollTop: 0 }, 800);

//});

//$('input[type="checkbox"]').on('change', function () {
//    $(this).siblings('input[type="checkbox"]').not(this).prop('checked', false);
//});