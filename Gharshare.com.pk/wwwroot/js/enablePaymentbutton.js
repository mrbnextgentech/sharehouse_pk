$(document).ready(function () {
    $('#chkacknowledge').change(function () {
        if (this.checked)           
            $('#btnpay').removeAttr('disabled');
        else
            $('#btnpay').attr('disabled', 'disabled');
    });
});