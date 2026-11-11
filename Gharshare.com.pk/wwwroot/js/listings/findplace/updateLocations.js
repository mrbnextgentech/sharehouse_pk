$(document).ready(function () {

    $('#frmmylocations').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var formId = 'frmmylocations';        
        var formData = new FormData($('#' + formId)[0]);
        var locations = $('#txtMylocations').val().trim();
        // Perform AJAX form submission
        if (locations != "") {
            var closeButton = $('.btn-close');
            /// call to update property details
            $.ajax({
                url: "/findplace/updatelocations",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    $('#location').html(data.suburb);
                    closeButton.trigger('click');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Your preferred locations updated successfully.',
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
                            text: 'Error updating preferred locations.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                }
            });
        }
        else {
            $('#spanlocations').html('&nbsp;&nbsp;required');
        }

                /// call to deactivate account starts
      
    });

    $('#txtMylocations').on('input', function () {
        var introduction = $('#txtMylocations').val().trim();

        if (introduction != "") {
            $('#spanlocations').html('');
        }
        else {
            $('#spanlocations').html('&nbsp;&nbsp;required');
        }
    });
});