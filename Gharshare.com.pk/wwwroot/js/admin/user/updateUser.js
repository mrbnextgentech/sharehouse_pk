$(document).ready(function () {

    $('#frmUser').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var currentPageURL = window.location.href;
        var formId = 'frmUser';
        var formData = new FormData($('#' + formId)[0]);

        var closeButton = $('.btn-close');

        $.ajax({
            url: "/admin/user/edituser",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                debugger;
                if (currentPageURL.includes("admin/user")) {
                    createUserlist(data);
                }
                swal.fire(
                    {
                        icon: 'success',
                        title: 'Success',
                        text: 'Account updated successfully.',
                        showConfirmButton: false,
                        timer: 2000,
                    }
                );
                closeButton.trigger('click');
            },
            error: function (xhr, status, error) {

                swal.fire(
                    {
                        icon: 'error',
                        title: 'Error',
                        text: 'Error updating user account.',
                        showConfirmButton: false,
                        timer: 2000,
                    }
                );
            }
        });
    });
});
