$(function () {
    $('#btnclose').click(function (event) {
        event.preventDefault();         
        var closeButton = $('.btn-close');
        closeButton.trigger('click');
    });
});
