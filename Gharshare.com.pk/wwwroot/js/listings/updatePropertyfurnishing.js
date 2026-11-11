$(document).ready(function () {

    $('#frmPropertyfurnishing').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var formId = 'frmPropertyfurnishing';        
        var formData = new FormData($('#' + formId)[0]);  
        
        // Perform AJAX form submission         
        var closeButton = $('.btn-close');
            /// call to update property details
            $.ajax({
                url: "/listing/updatepropertyfurnishing",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                   // $('#description').html(data.description);
                    closeButton.trigger('click');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Property furnishing updated successfully.',
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
        
                /// call to deactivate account starts
      
    });     
});