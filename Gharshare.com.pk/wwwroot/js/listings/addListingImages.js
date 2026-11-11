$(document).ready(function () {
    $('#photoUploader').on('change', function () {
        var files = $('#photoUploader')[0].files; // get the selected files from the file input
        var propertyID = $('#txtListingId').val().trim();
        if (files.length > 0 && files.length <= 14) {
            var formData = new FormData(); // create a new FormData object
            formData.append('propertyID', propertyID); // append the property ID
            for (var i = 0; i < files.length; i++) {
                formData.append('files', files[i]); // append each selected file to the FormData object
            }
            $("#stepalert").show();
            $.ajax({
                url: '/listing/addlistingnewphotos',
                data: formData, // pass the FormData object as the data parameter
                type: 'POST', // use POST method
                contentType: false, // set contentType to false, jQuery will automatically set the correct content type based on the FormData object
                processData: false, // set processData to false to prevent jQuery from processing the FormData object
                success: function (result) {
                    $("#stepalert").hide();
                    $("#photoCount").html(result.length + ' Photos')
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Listing photos saved successfully.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                    window.location.reload();
                },
                error: function (xhr, status, error) {
                    // Handle error
                     $("#stepalert").hide();
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Error saving property photos!',
                        showConfirmButton: false,
                        timer: 2000,
                    })
                }
            });
        }
        else if (files.length > 14)
        {
            Swal.fire({
                icon: 'error',
                title: 'Validation Error',
                text: 'You can save up to 14 property photos!',
                showConfirmButton: false,
                timer: 2000,
            });
            return false;
        }
    });
});