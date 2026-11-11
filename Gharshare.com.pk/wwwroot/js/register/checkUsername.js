$(document).ready(function () {
    $('#txtusername').on('change', function () {
        var userName = $('#txtusername').val().trim();
        if (userName != null && userName !="") {
            // Ajax call to validate demo request starts
            $.ajax({
                url: '/home/checkUsername?userName=' + userName,
                success: function (result) {
                    if (result) {
                        $('#txtusername').val('');
                        $('#txtusername').focus();
                        $('#spanEmail').html('&nbsp;User name already taken');                         
                    }
                }
            });
            // Ajax call to validate demo request Ends
        }
    });

    // clear 'spanEmail' on input event of

    $('#txtusername').on('input', function () {

        $('#spanEmail').html('');
    });

});
