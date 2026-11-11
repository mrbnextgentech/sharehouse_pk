$(document).ready(function () {

    $('#frmListingimages').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var formId = 'frmListingimages';        
        var formData = new FormData($('#' + formId)[0]);         
        // Perform AJAX form submission        
            var closeButton = $('.btn-close');
            /// ajax call starts
            $.ajax({
                url: "/listing/savelistingimagesneworder",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {                     
                    closeButton.trigger('click');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Listing images order updated successfully.',
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
                            text: 'Error updating listing images order.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                }
            });

                /// ajax call ends 
    });
});