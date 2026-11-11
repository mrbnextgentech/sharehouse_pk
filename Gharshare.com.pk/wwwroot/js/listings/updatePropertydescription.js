$(document).ready(function () {

    $('#frmPropertydescription').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var formId = 'frmPropertydescription';        
        var formData = new FormData($('#' + formId)[0]);
        var description = $('#txtdescription').val().trim();
        // Perform AJAX form submission
        if (description != "") {
            var closeButton = $('.btn-close');
            /// call to update property details
            $.ajax({
                url: "/listing/updatepropertydescription",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    $('#description').html(data.description);
                    closeButton.trigger('click');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Property description updated successfully.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                },
                error: function (xhr, status, error) {
                    swal.fire(
                        {
                            icon: 'error',
                            title: 'Error',
                            text: 'Error updating profile.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                }
            });
        }
        else {
            $('#spandescription').html('&nbsp;&nbsp;required');
        }

                /// call to deactivate account starts
      
    });

    $('#txtdescription').on('input', function () {
        var description = $('#txtdescription').val().trim();

        if (description != "") {
            $('#spandescription').html('');
        }
        else {
            $('#spandescription').html('&nbsp;&nbsp;required');
        }
    });
});