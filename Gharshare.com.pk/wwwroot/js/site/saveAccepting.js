$(function () {
    $('#frmAccepting').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        //debugger;
        var formId = 'frmAccepting';
        var formData = new FormData($('#' + formId)[0]);
        if ($("#frmAccepting")[0].checkValidity()) {
            var closeButton = $('.btn-close');
            $.ajax({
                url: '/admin/site/addaccepting',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                   // debugger; 
                    var successMessage = "";
                    if (result == "add")
                        successMessage = "Accepting item saved successfully!"
                    else
                        successMessage = "Accepting item updated successfully!"
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: successMessage,
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                    window.location.reload();
                    //if (result == "add")
                    //    $("#frmTemptype")[0].reset();
                    //else {
                    //    closeButton.trigger('click');
                    //    window.location.reload();
                    //}

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