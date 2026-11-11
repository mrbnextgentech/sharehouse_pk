$(document).ready(function () {

    $('#deactivate').click(function (event) {
        event.preventDefault(); // Prevent default form submission

        var propertyId = $('#txtListingId').val();

        //var formData = new FormData();

        //formData.append("listingId", propertyId);

        // Perform AJAX form submission
        if (propertyId != "") {
            /// confirm dialouge starts
            Swal.fire({
                title: 'Confirm deactivating your listing?',
                text: "Your listing will be taken down & will become inactive.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#FF005D',
                cancelButtonColor: '#082642',
                confirmButtonText: 'Yes, deactivate it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    /// ajax call to delete account starts
                    var closeButton = $('.btn-close');
                    $.ajax({
                        url: "/listing/deactivatelisting?listingId=" + propertyId,
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
                                    text: 'Listing deactivated successfully.',
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
                    /// End of ajax call
                }
            })
        }
    });
});