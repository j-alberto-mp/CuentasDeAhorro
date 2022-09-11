let iniciarSesion = () => {
    return new Promise((resolve, reject) => {
        let modelo = {
            userName: $('#userName').val(),
            password: $('#password').val()
        };

        $.ajax({
            method: 'POST',
            url: '/Account/Autenticar',
            data: { modelo },
            dataType: 'JSON',
            success: resolve,
            error: reject
        });
    });
};

$(function () {
    $('#acceder').on('click', function () {
        iniciarSesion()
            .then((response) => {
                if (response.correct) {
                    window.location.href = '/Home/Index';
                }
                else {
                    $('#mensaje').html(response.message);

                    $('.modal').addClass('visible');
                }
            });
    });

    $('#aceptar').on('click', function () {
        $('.modal').removeClass('visible');
    });
});