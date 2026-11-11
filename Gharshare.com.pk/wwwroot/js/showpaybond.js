$(document).ready(function () {
    $('#proceed2pay').click(function () {

        $('#divpay').show();
    });
    $('#chkacknowledge').change(function () {
        if (this.checked)           
            $('#btnpay').removeAttr('disabled');
        else
            $('#btnpay').attr('disabled', 'disabled');
    });
});