function deltelistingimage(event, propertyId,imageId) {
    event.preventDefault(); // Prevent default form submission
    event.stopPropagation();
    //var propertyId = $('#txtListingId').val();
    //var imgId = $('#delete').id;
    // Perform AJAX form submission
    if (propertyId != "" && imageId!="") {
        //// confirm video tour starts
        Swal.fire({
            title: 'Confirm deleting this image?',
            text: "This image will be deleted permanantly. ",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#FF005D',
            cancelButtonColor: '#082642',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                /// ajax call to delete account starts
                //var closeButton = $('.btn-close');
                $.ajax({
                    url: "/listing/deletelistingimage?listingId=" + propertyId + "&imageid=" + imageId,
                    type: "GET",
                    //data: formData,
                    //contentType: false,
                    //processData: false,
                    success: function (data) {
                        //closeButton.trigger('click');
                        // window.location = "/home/";
                        swal.fire(
                            {
                                icon: 'success',
                                title: 'Success',
                                text: 'Image deleted successfully.',
                                showConfirmButton: false,
                                timer: 2000,
                            }
                        );
                        window.location.reload();
                    },
                    error: function (xhr, status, error) {
                        swal.fire(
                            {
                                icon: 'error',
                                title: 'Error',
                                text: 'internal server occur deleting image.',
                                showConfirmButton: false,
                                timer: 2000,
                            }
                        );
                    }
                });
                /// End of ajax call to delete video tour

            }
        })
    }
}
   