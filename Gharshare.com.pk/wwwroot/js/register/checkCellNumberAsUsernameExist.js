$(document).ready(function () {
    $('#txtmobile').on('change', function () {
        var cellNo = $('#txtmobile').val().trim();
        if (cellNo != null && cellNo !="") {
            // Ajax call to validate demo request starts
            $.ajax({
                url: '/home/checkCellNumberAsUsernameExist?cellNo=' + cellNo,
                success: function (result) {
                    if (result) {
                        $('#txtmobile').val('');
                        $('#txtmobile').focus();
                        $('#spanMobile').html('&nbsp;This mobile number already exists as user name');                         
                    }
                }
            });
            // Ajax call to validate demo request Ends
        }
    });

    // clear 'spanEmail' on input event of

    $('#txtmobile').on('input', function () {

        $('#spanMobile').html('');
    });

});
