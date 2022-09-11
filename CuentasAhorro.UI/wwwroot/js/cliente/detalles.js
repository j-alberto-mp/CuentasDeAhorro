$(function () {
    $('#aperturar').on('click', function () {
        window.location.href = `/Cuenta/Aperturar/1`;
    });

    $('.cards__card').on('click', function () {
        window.location.href = `/Cuenta/Detalles/1`;
    });
});