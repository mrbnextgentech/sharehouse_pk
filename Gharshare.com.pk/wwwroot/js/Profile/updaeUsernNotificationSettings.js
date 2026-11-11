$(document).ready(function () {
    var usrId = $('#userId').val();
    //debugger;
    $('#frmNotifications').submit(function (event) {
        event.preventDefault();
        var formData = new FormData($(this).get(0)); //use this approach, especially with list         
        var closeButton = $('.btn-close');

        $.ajax({
            url: '/privacy/updaeusernotificationsettings',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {                 
                closeButton.trigger('click');
                swal.fire(
                    {
                        icon: 'success',
                        title: 'Success',
                        text: 'notification alerts setting successfully updated.',
                        showConfirmButton: false,
                        timer: 2000,
                    }
                );
                
            },
            error: function (xhr, status, error) {
                console.error("Notification alerts update failed!", error);
                swal.fire(
                    {
                        icon: 'error',
                        title: 'Error',
                        text: 'Error updating notification alerts setting.',
                        showConfirmButton: false,
                        timer: 2000,
                    }
                );
            }
        });
    }); // <-- Add closing parenthesis here
});