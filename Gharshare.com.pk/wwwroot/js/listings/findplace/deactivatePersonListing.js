$(document).ready(function () {

    $('#btndeactivate').click(function (event) {
        event.preventDefault(); // Prevent default form submission
        var listingId = $('#txtpreviewPersonId').val().trim();
        //debugger;
        // Perform AJAX form submission
        if (listingId != null) {
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
                        url: "/findplace/deactivatelisting?personlistingId=" + listingId,
                        type: "GET",
                        //data: formData,
                        //contentType: false,
                        //processData: false,
                        success: function (data) {
                            closeButton.trigger('click');  
                            $('#btndeactivate').prop('disabled', true);
                            swal.fire(
                                {
                                    icon: 'success',
                                    title: 'Success',
                                    text: 'Listing deactivated successfully.',
                                    showConfirmButton: false,
                                    timer: 2000,
                                }
                            );
                           // window.location.reload();
                        },
                        error: function (xhr, status, error) {
                            swal.fire(
                                {
                                    icon: 'error',
                                    title: 'Error',
                                    text: 'internal server occur deactivating your listing.',
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