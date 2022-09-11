let mostrarModal = (tipo) => {
    $('.modal').toggleClass('visible');

    $('#concepto').html(`${(tipo === 1 ? 'Depósito' : 'Retiro')} de efectivo`);
    $('#indicacion').html(`Indica la cantidad a ${(tipo === 1 ? 'depositar' : 'retirar')}`);
    $('#guardar').html(tipo === 1 ? 'DEPOSITAR' : 'RETIRAR');
};

$(function () {
    $('#depositar').on('click', function () {
        mostrarModal(1);
    });

    $('#retirar').on('click', function () {
        mostrarModal(2);
    });

    $('#guardar').on('click', function () {
        $('.modal').toggleClass('visible');
    });
});