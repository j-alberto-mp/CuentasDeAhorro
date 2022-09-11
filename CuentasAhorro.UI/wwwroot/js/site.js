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