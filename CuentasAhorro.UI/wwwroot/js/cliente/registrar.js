const repository = new Repository('Cliente');

$(function () {
    $('#guardar').on('click', function () {
        let modelo = {
            nombre: $('#nombre').val(),
            apellidoPaterno: $('#apellidoPaterno').val(),
            apellidoMaterno: $('#apellidoMaterno').val()
        };

        repository.httpPost('Guardar', modelo)
            .then((response) => {
                if (response.correct) {
                    success('Registro exitoso', 'El cliente fue registrado correctamente', `/Cliente/Detalles/${response.data.clienteID}`);
                }
                else {
                    requestError(response.message);
                }
            })
            .catch((error) => {
                serviceError(`${error.status} - ${error.statusText}`);
            });
    });
});