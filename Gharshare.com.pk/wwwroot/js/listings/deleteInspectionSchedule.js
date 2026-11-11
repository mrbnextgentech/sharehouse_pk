$(document).ready(function () {

    $('#delete').click(function (event) {
        event.preventDefault(); // Prevent default form submission

        var propertyId = $('#txtListingId').val();
        debugger;
        // Perform AJAX form submission
        if (propertyId != "") {
            //// confirm video tour starts
            Swal.fire({
                title: 'Confirm deleting inspection schedule?',
                text: "Your property inspection schedule will be deleted permanantly",
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
                        url: "/listing/deleteInspectionSchedule?propertyId=" + propertyId,
                        type: "POST",                        
                        success: function (data) {
                            closeButton.trigger('click');
                            // window.location = "/home/";
                            swal.fire(
                                {
                                    icon: 'success',
                                    title: 'Success',
                                    text: 'Property inspection schedule deleted successfully.',
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
                                    text: 'internal server occured deleting record.',
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