const clienteRepository = new Repository('Cliente');
const cuentaRepository = new Repository('Cuenta');

$(function () {
    moment.tz.setDefault('America/Mexico_City');

    waiting();

    if ($('#clienteID').val()) {
        clienteRepository.httpGet('ObtenerDetalles', { id: $('#clienteID').val() })
            .then((response) => {
                if (response.correct) {
                    $('#nombre').html(response.data.nombreCompleto);
                    $('#numero').html(`<b>${response.data.clienteID}</b>`);
                    $('#fecha').html(`<b>${moment(response.data.fechaRegistro).format('YYYY-MM-DD')}</b>`);

                    $('.cards').empty();

                    cuentaRepository.httpGet('ObtenerCuentas', { id: $('#clienteID').val() })
                        .then((response) => {
                            if (response.correct) {
                                if (response.data[0]) {
                                    $.each(response.data, function (key, value) {
                                        $('.cards').append(`<div class="cards__card" data-cuenta="${value.cuentaID}"><label><i class="fi fi-rr-wallet"></i></label><p>${value.numeroCuenta}</p></div>`);
                                    })
                                }
                                else {
                                    $('.cards').append('<div class="cards__card--empty"><i class="fi fi-rr-engine-warning"></i><h6>El cliente aún no ha aperturado ninguna cuenta de ahorro</h5></div>');
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
                }
                else {
                    requestError(response.message);
                }
            })
            .catch((error) => {
                serviceError(`${error.status} - ${error.statusText}`);
            });
    }

    $('#aperturar').on('click', function () {
        window.location.href = `/Cuenta/Aperturar/${parseInt($('#clienteID').val())}`;
    });

    $('.cards__card').on('click', function () {
        window.location.href = `/Cuenta/Detalles/${parseInt($(this).data('cuenta'))}`;
    });
});