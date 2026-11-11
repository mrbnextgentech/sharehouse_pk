$(document).ready(function () {

    $('#frmpreferences').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var formId = 'frmpreferences';        
        var formData = new FormData($('#' + formId)[0]);
         
        // Perform AJAX form submission         
            var closeButton = $('.btn-close');
            /// call to update property details
            $.ajax({
                url: "/findplace/updatepreferences",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                   // $('#introduction').html(data.introduction);
                    closeButton.trigger('click');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'You preferences updated successfully.',
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
                            text: 'Error updating preferences.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                }
            });
    });
    
});