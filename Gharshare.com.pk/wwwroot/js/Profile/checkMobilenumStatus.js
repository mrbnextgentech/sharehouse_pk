$(document).ready(function () {
    $('#txtMobile').on('change', function () {
        var userId = $('#txtUserId').val().trim();
        var userMobilenum = $('#txtMobile').val().trim();
        console.log(userMobilenum);
        //console.log($('#txtMobile').val());
        if (userId != null && userId != "" && userMobilenum != null && userMobilenum != "" ) {
            // Ajax call
            $.ajax({
                url: '/profile/checkmobilenumberstatus?userId=' + userId +'&&userMobilenum='+userMobilenum,
                success: function (result) {
                    if (result) {
                        $('#txtMobile').val('');
                        $('#txtMobile').focus();
                        $('#spanMobile').html('&nbsp;Mobile number is already verified');                         
                    }
                }
            });
            // End of Ajax call
        }
    });
});
