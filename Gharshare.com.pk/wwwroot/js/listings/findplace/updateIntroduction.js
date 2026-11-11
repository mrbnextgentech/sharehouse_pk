$(document).ready(function () {

    $('#frmIntroduction').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var formId = 'frmIntroduction';        
        var formData = new FormData($('#' + formId)[0]);
        var introduction = $('#txtMyintro').val().trim();
        // Perform AJAX form submission
        if (introduction != "") {
            var closeButton = $('.btn-close');
            /// call to update property details
            $.ajax({
                url: "/findplace/updateintroduction",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    $('#introduction').html(data.introduction);
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
            $('#spanintro').html('&nbsp;&nbsp;required');
        }

                /// call to deactivate account starts
      
    });

    $('#txtMyintro').on('input', function () {
        var introduction = $('#txtMyintro').val().trim();

        if (introduction != "") {
            $('#spanintro').html('');
        }
        else {
            $('#spanintro').html('&nbsp;&nbsp;required');
        }
    });
});