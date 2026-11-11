$(function () {
    $('#frmTemptype').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        //debugger;
        var formId = 'frmTemptype';
        var formData = new FormData($('#' + formId)[0]);
        if ($("#frmTemptype")[0].checkValidity()) {
            var closeButton = $('.btn-close');
            $.ajax({
                url: '/admin/site/addtemplatetype',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                   // debugger; 
                    var successMessage = "";
                    if (result == "add")
                        successMessage = "Email template type saved successfully!"
                    else
                        successMessage = "Email template type updated successfully!"
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: successMessage,
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );

                    if (result == "add")
                        $("#frmTemptype")[0].reset();
                    else {
                        closeButton.trigger('click');
                        window.location.reload();
                    }

                },
                error: function (xhr, status, error) {
                    //debugger;
                    var responseText = JSON.parse(xhr.responseText);
                    swal.fire(
                        {
                            icon: 'error',
                            title: 'Error',
                            text: responseText.error,
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
                    title: 'Validation error',
                    text: 'Name is required.',
                    showConfirmButton: false,
                    timer: 2000,
                }
            );

        }
    });
});