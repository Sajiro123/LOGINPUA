$('#validacion_v').click(function (event) {
    var bool = $('.vacios').toArray().some(function (el) {
        return $(el).val().length < 1;
    });
    if (bool) {
        alert("esta vacio");
        event.preventDefault();

    }

});