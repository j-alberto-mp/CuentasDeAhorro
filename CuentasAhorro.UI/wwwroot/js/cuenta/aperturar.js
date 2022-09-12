const clienteRepository = new Repository('Cliente');
const cuentaRepository = new Repository('Cuenta');

let obtenerCliente = () => {
    clienteRepository.httpGet('ObtenerDetalles', { id: $('#clienteID').val() })
        .then((response) => {
            if (response.correct) {
                if (response.data) {
                    $('#numeroCliente').val(response.data.clienteID);
                    $('#nombre').val(response.data.nombre);
                    $('#apellidoPaterno').val(response.data.apellidoPaterno);
                    $('#apellidoMaterno').val(response.data.apellidoMaterno);

                    swal.close();
                }
                else {
                    info('No se encontraron resultados', 'No se encontró ningún cliente que coincida con el número proporcionado');
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

let guardarRegistro = () => {
    processing('Creando cuenta de ahorro...');

    let modelo = {
        clienteID: $('#numeroCliente').val(),
        saldo: $('#montoCuenta').val().replace(',', '')
    };

    cuentaRepository.httpPost('Guardar', modelo)
        .then((response) => {
            if (response.correct) {
                if (response.data) {
                    success('Nueva cuenta de ahorro', 'Se realizó la apertura de una cuenta de ahorro', `/Cuenta/Detalles/${response.data.cuentaID}`);
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
    $('#numeroCliente').mask('0#');
    $('#montoCuenta').mask('000,000,000,000,000.00', { reverse: true });

    if (parseInt($('#clienteID').val()) > 0) {
        waiting();

        setTimeout(function () {
            obtenerCliente();

            $('#guardar').prop('disabled', false);
        }, 1500);
    }

    $('#guardar').on('click', function () {
        if ($('#montoCuenta').val()) {
            guardarRegistro();
        }
        else {
            swal({
                icon: 'info',
                title: 'No se ingresó un monto inicial',
                text: 'No ingresaste un monto inicial, la cuenta de ahorro será creada con un saldo inicial de $0, ¿Deseas continuar?',
                buttons: {
                    cancel: {
                        text: "CANCELAR",
                        value: false,
                        visible: true,
                        className: "no--padding",
                        closeModal: true,
                    },
                    confirm: {
                        text: "ACEPTAR",
                        value: true,
                        visible: true,
                        className: "no--padding",
                        closeModal: true
                    }
                },
                closeOnClickOutside: false,
                closeOnEsc: false
            })
            .then((value) => {
                if (value) {
                    $('#montoCuenta').val(0);
                    guardarRegistro();
                }
            });
        }
    });
});