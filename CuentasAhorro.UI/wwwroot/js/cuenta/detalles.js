const cuentaRepository = new Repository('Cuenta');
const transaccionRepository = new Repository('Transaccion');

let mostrarModal = (tipo) => {
    $('.modal').toggleClass('visible');

    $('#tipoTransaccion').val(tipo);
    $('#concepto').html(`${(tipo === 1 ? 'Depósito' : 'Retiro')} de efectivo`);
    $('#indicacion').html(`Indica la cantidad a ${(tipo === 1 ? 'depositar' : 'retirar')}`);
    $('#guardar').html(tipo === 1 ? 'DEPOSITAR' : 'RETIRAR');
};

let obtenerCuenta = () => {
    cuentaRepository.httpGet('ObtenerDetalles', { id: $('#cuentaID').val() })
        .then((response) => {
            if (response.correct) {
                if (response.data) {
                    $('#numeroCuenta').html(format(response.data.numeroCuenta));
                    $('#saldoCuenta').html(`$${currency(response.data.saldo)}`);

                    obtenerTransacciones();
                }
                else {
                    info('No se encontraron resultados', 'No se encontró ninguna cuenta con el ID proporcionado');
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

let obtenerTransacciones = () => {
    transaccionRepository.httpGet('ObtenerTransacciones', { id: $('#cuentaID').val() })
        .then((response) => {
            if (response.correct) {
                $('.info__transactions tbody').empty();

                if (response.data[0]) {
                    $.each(response.data, function (key, value) {
                        $('.info__transactions tbody').append(
                            '<tr>' +
                                `<td>${value.tipoTransaccion}</td>` +
                                `<td class="${(value.tipoTransaccionID === 1 ? 'info--positive' : 'info--negative')}"> ${(value.tipoTransaccionID === 1 ? '$' : '-$')}${currency(value.monto)}</td>` +
                                `<td>${moment(value.fechaOperacion).format('YYYY-MM-DD')}</td>` +
                                `<td>${moment(value.fechaOperacion).format('LTS')}</td>` +
                            '</tr>'
                        );
                    });
                }

                swal.close();
            }
            else {
                requestError(response.message);
            }
        })
        .catch((error) => {
            serviceError(`${error.status} - ${error.statusText}`);
        });
};

let registrarTransaccion = () => {
    let modelo = {
        cuentaID: $('#cuentaID').val(),
        tipoTransaccionID: $('#tipoTransaccion').val(),
        monto: $('#monto').val().replace(',', '')
    }

    transaccionRepository.httpGet('Registrar', modelo)
        .then((response) => {
            if (response.correct) {
                swal({
                    icon: 'success',
                    title: 'Transacción registrada',
                    text: 'La transacción fue registrada correctamente',
                    buttons: false,
                    closeOnClickOutside: false,
                    closeOnEsc: false,
                    timer: 1500
                }).then(() => {
                    processing('Actualizando el saldo de la cuenta...');

                    setTimeout(function () {
                        obtenerCuenta();

                        obtenerTransacciones();
                    }, 1000);
                });
            }
            else {
                requestError(response.message);
            }
        })
        .catch((error) => {
            serviceError(`${error.status} - ${error.statusText}`);
        });

    clearInputs();
};

$(function () {
    moment.tz.setDefault('America/Mexico_City');

    if (parseInt($('#cuentaID').val()) > 0) {
        waiting();

        setTimeout(function () {
            obtenerCuenta();

            $('.transaction').prop('disabled', false);
        }, 1500);
    }

    $('#monto').mask('000,000,000,000,000.00', { reverse: true });

    $('.transaction').on('click', function () {
        mostrarModal($(this).data('tipo'));
    });

    $('#cancelar').on('click', function () {
        clearInputs();
        $('.modal').toggleClass('visible');
    });

    $('#guardar').on('click', function () {
        let monto = $('#monto').val().replace(',', '');

        if (parseFloat(monto) > 0) {
            $('.modal').toggleClass('visible');

            registrarTransaccion();
        }
        else {
            if (parseFloat(monto) === 0) {
                info('No se puede realizar la transacción', 'La cantidad no puede ser igual a 0');
            }
            else {
                info('No se puede realizar la transacción', 'No es posible ingresar una cantidad negativa');
            }
        }
    });
});