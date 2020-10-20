function validaciones() {

}
validaciones.prototype.ValidarSession = function () { 

    Swal.fire({
        type: 'error',
        title: 'Sesión caducada',
        text: 'Tu sesión ha caducado, vuelve a iniciar sesión',
        footer: '<a href=>Volver a iniciar sesión</a>',
        allowOutsideClick: false
     })
};