const repository = new Repository('Cliente');

let busqueda;

let buscar = (modelo) => {
    waiting();

    repository.httpGet('BuscarClientes', modelo)
        .then((response) => {
            if (response.correct) {
                $('.info__customers tbody').empty();

                if (response.data[0]) {
                    $.each(response.data, function (key, value) {
                        $('.info__customers tbody').append(
                            '<tr>' +
                                `<td>${key + 1}</td>` +
                                `<td>${value.clienteID}</td>` +
                                `<td>${value.nombreCompleto}</td>` +
                                `<td>${moment(value.fechaRegistro).format('YYYY-MM-DD') }</td>` +
                                `<td><button type="button" data-cliente="${value.clienteID}">VER DETALLE</button></td>` +
                            '</tr>'
                        )
                    });

                    swal.close();
                }
                else {
                    info('No se encontraron resultados', 'No se encontró ningún cliente que coincida con los datos proporcionados');
                }
            }
            else {
                requestError(response.message);
            }
        })
        .catch((error) => {
            serviceError(`${error.status} - ${error.statusText}`);
        });
}

$(function () {
    moment.tz.setDefault('America/Mexico_City');

    clearInputs();

    $('#numeroCliente').mask('0#');
    $('#nombre, #apellidoPaterno, #apellidoMaterno').mask('AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA', {
        byPassKeys: [8, 32],
        translation: {
            'A': { pattern: /^[a-zA-Z\s]$/ }
        }
    });

    $('input[type="radio"]').on('change', function () {
        clearInputs();
        $('.info__search div').hide();

        busqueda = $(this).data('busqueda');

        $(`#${busqueda}`).show();

        $('#buscar').prop('disabled', false);
    });

    $('#buscar').on('click', function () {
        if (busqueda === 'b_1') {
            if ($('#numeroCliente').val()) {
                let modelo = { clienteID: $('#numeroCliente').val() };

                buscar(modelo);
            }
            else {
                info('No se puede realizar la búsqueda', 'Debes ingresar el número del cliente');
            }
        }
        else {
            if ($('#nombre').val() || $('#apellidoPaterno').val() || $('#apellidoMaterno').val()) {
                let modelo = {
                    nombre: $('#nombre').val(),
                    apellidoPaterno: $('#apellidoPaterno').val(),
                    apellidoMaterno: $('#apellidoMaterno').val()
                };

                buscar(modelo);
            }
            else {
                info('No se puede realizar la búsqueda', 'Debes ingresar un valor en al menos uno de los campos');
            }
        }
    });

    $('.info__customers tbody').on('click', 'button', function () {
        window.location.href = `/Cliente/Detalles/${parseInt($(this).data('cliente'))}`
    })
});