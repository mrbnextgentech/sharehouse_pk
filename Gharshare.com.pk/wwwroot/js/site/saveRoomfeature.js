$(function () {
    $('#frmRoomfeature').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        //debugger;
        var formId = 'frmRoomfeature';
        var formData = new FormData($('#' + formId)[0]);
        if ($("#frmRoomfeature")[0].checkValidity()) {
            var closeButton = $('.btn-close');
            $.ajax({
                url: '/admin/site/addRoomfeature',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                   // debugger; 
                    var successMessage = "";
                    if (result == "add")
                        successMessage = "Room furnishing saved successfully!"
                    else
                        successMessage = "Room furnishing updated successfully!"
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