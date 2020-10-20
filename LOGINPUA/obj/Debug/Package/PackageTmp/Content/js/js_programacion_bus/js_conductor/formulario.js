
//jQuery time
var current_fs, siguiente_fs, atras_fs; //conjuntos de campo
var left, opacity, scale; //propiedades fieldset que vamos a animar
var animacion; //bandera para evitar problemas técnicos rápidos con varios clics

$(".next").click(function () {
    if (animacion) return false;
    animacion = true;

    current_fs = $(this).parent();
    siguiente_fs = $(this).parent().next();

    //activa el siguiente paso en la barra de progreso usando el índice de next_fs
    $("#progressbar li").eq($("fieldset").index(siguiente_fs)).addClass("active");

    //muestra el siguiente campo
    siguiente_fs.show();
    //ocultar el fieldset actual con estilo
    current_fs.animate({ opacity: 0 }, {
        step: function (now, mx) {
            // como la opacidad de current_fs se reduce a 0 - almacenado en "ahora"
            // 1. escala current_fs hasta 80%
            scale = 1 - (1 - now) * 0.2;
            // 2. traiga next_fs desde la derecha (50%)
            left = (now * 50) + "%";
            // 3. aumentar la opacidad de next_fs a 1 a medida que se mueve
            opacity = 1 - now;
            current_fs.css({
                'transform': 'scale(' + scale + ')',
                'position': 'absolute'
            });
            siguiente_fs.css({ 'left': left, 'opacity': opacity });
        },
        duration: 800,
        complete: function () {
            current_fs.hide();
            animacion = false;
        },
        // esto viene del plugin easing personalizado
        easing: 'easeInOutBack'
    });
});

$(".previous").click(function () {
    if (animacion) return false;
    animacion = true;

    current_fs = $(this).parent();
    atras_fs = $(this).parent().prev();

    // desactivar el paso actual en la barra de progreso
    $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

    // muestra el fieldset anterior
    atras_fs.show();
    // esconde el fieldset actual con estilo
    current_fs.animate({ opacity: 0 }, {
        step: function (now, mx) {
            // como la opacidad de current_fs se reduce a 0 - almacenado en "ahora"
            // 1. escala previous_fs de 80% a 100%
            scale = 0.8 + (1 - now) * 0.2;
            // 2. toma current_fs a la derecha (50%) - desde 0%
            left = ((1 - now) * 50) + "%";
            // 3. aumentar la opacidad de previous_fs a 1 a medida que se mueve
            opacity = 1 - now;
            current_fs.css({ 'left': left });
            atras_fs.css({ 'transform': 'scale(' + scale + ')', 'opacity': opacity });
        },
        duration: 800,
        complete: function () {
            current_fs.hide();
            animacion = false;
        },
        // esto viene del plugin easing personalizado
        easing: 'easeInOutBack'
    });
});

$(".submit").click(function () {
    return false;
})

$('.action-button').click(function (e) {

    $('html, body').animate({ scrollTop: 0 }, 800);

});

$('input[type="checkbox"]').on('change', function () {
    $(this).siblings('input[type="checkbox"]').not(this).prop('checked', false);
});