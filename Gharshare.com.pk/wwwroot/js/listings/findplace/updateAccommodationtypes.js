$(document).ready(function () {

    $('#frmaccommodationtypes').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var formId = 'frmaccommodationtypes';        
        var formData = new FormData($('#' + formId)[0]);         
        // Perform AJAX form submission         
            var closeButton = $('.btn-close');
            /// call to update property details
            $.ajax({
                url: "/findplace/updateaccommodationtypes",
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
                            text: 'Your preferred accommodations updated successfully.',
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
                            text: 'Error updating preferred accommodations.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                }
            });
    });
});