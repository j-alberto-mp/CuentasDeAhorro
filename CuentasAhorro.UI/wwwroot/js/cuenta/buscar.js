const repository = new Repository('Cuenta');

let busqueda;

let buscarCuentas = (modelo) => {
    waiting();

    repository.httpGet('BuscarCuentas', modelo)
        .then((response) => {
            if (response.correct) {
                $('.info__result tbody').empty();

                if (response.data[0]) {
                    $.each(response.data, function (key, value) {
                        $('.info__result tbody').append(
                            '<tr>' +
                                `<td>${key + 1}</td>` +
                                `<td>${value.clienteID}</td>` +
                                `<td>${value.nombre} ${value.apellidoPaterno} ${value.apellidoMaterno}</td>` +
                                `<td>${format(value.numeroCuenta)}</td>` +
                                `<td><button type="button" data-cuenta="${value.cuentaID}">VER DETALLE</button></td>` +
                            '</tr>'
                        )
                    });

                    swal.close();
                }
                else {
                    info('No se encontraron resultados', 'No se encontró ninguna cuenta de ahorro que coincida con los datos proporcionados');
                }
            }
            else {
                requestError(response.message);
            }
        })
        .catch((error) => {
            serviceError(`${error.status} - ${error.statusText}`);
        });
};

$(function () {
    clearInputs();

    $('#numeroCliente').mask('0#');
    $('#nombre, #apellidoPaterno, #apellidoMaterno').mask('AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA', {
        byPassKeys: [8, 32],
        translation: {
            'A': { pattern: /^[a-zA-Z\s]$/ }
        }
    });
    $('#numeroCuenta').mask('AA00 0000 0000 0000', {
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

                buscarCuentas(modelo);
            }
            else {
                info('No se puede realizar la búsqueda', 'Debes ingresar el número del cliente');
            }
        }
        else if (busqueda === 'b_2') {
            if ($('#nombre').val() || $('#apellidoPaterno').val() || $('#apellidoMaterno').val()) {
                let modelo = {
                    nombre: $('#nombre').val(),
                    apellidoPaterno: $('#apellidoPaterno').val(),
                    apellidoMaterno: $('#apellidoMaterno').val()
                };

                buscarCuentas(modelo);
            }
            else {
                info('No se puede realizar la búsqueda', 'Debes ingresar un valor en al menos uno de los campos');
            }
        }
        else {
            if ($('#numeroCuenta').val()) {
                let modelo = { numeroCuenta: $('#numeroCuenta').val().replace(' ', '') };

                buscarCuentas(modelo);
            }
            else {
                info('No se puede realizar la búsqueda', 'Debes ingresar el número de cuenta');
            }
        }
    });

    $('.info__result tbody').on('click', 'button', function () {
        window.location.href = `/Cuenta/Detalles/${parseInt($(this).data('cuenta'))}`
    });
});