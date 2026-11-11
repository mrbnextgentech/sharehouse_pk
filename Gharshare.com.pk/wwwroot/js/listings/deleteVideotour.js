$(document).ready(function () {

    $('#delete').click(function (event) {
        event.preventDefault(); // Prevent default form submission

        var propertyId = $('#txtListingId').val();

        //var formData = new FormData();

        //formData.append("listingId", propertyId);

        // Perform AJAX form submission
        if (propertyId != "") {
            //// confirm video tour starts
            Swal.fire({
                title: 'Confirm deleting your video tour?',
                text: "Link to your video tour will be deleted permanantly. " +
                    "You won't be able to revert this back!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#FF005D',
                cancelButtonColor: '#082642',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    /// ajax call to delete account starts
                    var closeButton = $('.btn-close');
                    $.ajax({
                        url: "/listing/deletevideotour?listingId=" + propertyId,
                        type: "GET",
                        //data: formData,
                        //contentType: false,
                        //processData: false,
                        success: function (data) {
                            closeButton.trigger('click');
                            // window.location = "/home/";
                            swal.fire(
                                {
                                    icon: 'success',
                                    title: 'Success',
                                    text: 'Video tour deleted successfully.',
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
                                    text: 'internal server occur deleting your account.',
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
    });
});