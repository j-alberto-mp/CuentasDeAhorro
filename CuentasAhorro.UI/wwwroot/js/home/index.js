$(function () {
    $('#registrar_cliente').on('click', function () {
        window.location.href = `/Cliente/Registrar`;
    });

    $('#buscar_cliente').on('click', function () {
        window.location.href = `/Cliente/Buscar`;
    });

    $('#aperturar_cuenta').on('click', function () {
        window.location.href = `/Cuenta/Aperturar`;
    });

    $('#buscar_cuenta').on('click', function () {
        window.location.href = `/Cuenta/Buscar`;
    });
});