const waiting = () => {
    swal({
        icon: 'info',
        title: 'Espera un momento',
        text: 'Consultando la información, esto puede demorar un poco...',
        buttons: false,
        closeOnClickOutside: false,
        closeOnEsc: false
    });
};

const success = (title, text, redirect) => {
    swal({
        icon: 'success',
        title: title,
        text: text,
        buttons: false,
        closeOnClickOutside: false,
        closeOnEsc: false,
        timer: 2500,
    }).then(() => {
        if (redirect) {
            window.location.href = redirect;
        }
    });
};

const serviceError = (text) => {
    swal({
        icon: 'error',
        title: 'Ocurrió un error en el servidor',
        text: text,
        buttons: false,
        closeOnClickOutside: false,
        closeOnEsc: false,
        timer: 2500,
    });
};

const requestError = (text) => {
    swal({
        icon: 'warning',
        title: 'Ocurrió un error en la solicitud',
        text: text,
        buttons: false,
        closeOnClickOutside: false,
        closeOnEsc: false,
        timer: 2500,
    });
};

$(function () {
    let collapsed = sessionStorage.getItem('collapsed');

    if (collapsed === 'closed') {
        $('#sidebar').addClass('active');
        $('.sidebar__menu').addClass('no-animation');
    }
    else {
        $('#sidebar').removeClass('active');
        $('.sidebar__menu').removeClass('no-animation');
    }

    $('#collapse').on('click', function () {
        $('#sidebar').toggleClass('active');

        sessionStorage.setItem('collapsed', $('#sidebar').hasClass('active') ? 'closed' : 'open');
    });

    $('.sidebar__items-menu').click(function () {
        $(this).next('.sidebar__items-submenu').slideToggle();
        $(this).find('.dropdown').toggleClass('rotate');
    });
});