﻿let busqueda;

$(function () {
    $('input[type="radio"]').on('change', function () {
        $('.info__search div').hide();

        busqueda = $(this).data('busqueda');

        $(`#${busqueda}`).show();
    });
});