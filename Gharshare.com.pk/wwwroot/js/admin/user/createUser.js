$(document).ready(function () {
    $('#registerForm').submit(function (event) {
        debugger;
        event.preventDefault(); // Prevent default form submission

        var formData = new FormData($('#registerForm')[0]);
        formData.append("returnUrl", "");
        var closeButton = $('.btn-close');
        if ($('#registerForm')[0].checkValidity()) {
            $.ajax({
                url: '/admin/register',
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);                     
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Password updated successfully.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                    createUserlist(data);
                    closeButton.trigger('click');
                },
                error: function (xhr, status, error) {
                    // Handle error
                    debugger;
                    console.log(xhr);
                    swal.fire(
                        {
                            icon: 'error',
                            title: 'Error',
                            text: 'Error updating password.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                }
            });
        }
        else {

            swal.fire(
                {
                    icon: 'error',
                    title: 'Validation Error',
                    text: 'Fill in the form completly.',
                    showConfirmButton: false,
                    timer: 2000,
                }
            );
        }
    });
});