$(document).ready(function () {

    $('#delete').click(function (event) {
        event.preventDefault(); // Prevent default form submission

        var inspectionId = $('#txtInspectionId').val();

        //var formData = new FormData();

        //formData.append("listingId", propertyId);

        // Perform AJAX form submission
        if (inspectionId != "") {
            //// confirm dialog starts
            Swal.fire({
                title: 'Confirm deleting inspection schedule entry?',
                text: "This inspection entry will be deleted permanantly.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#FF005D',
                cancelButtonColor: '#082642',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    /// ajax call to delete starts
                    var closeButton = $('.btn-close');
                    $.ajax({
                        url: "/listing/deleteinspectionentry?inspectionId=" + inspectionId,
                        type: "POST",
                        //data: formData,
                        //contentType: false,
                        //processData: false,
                        success: function (data) {
                            closeButton.trigger('click');                            
                            swal.fire(
                                {
                                    icon: 'success',
                                    title: 'Success',
                                    text: 'Inspection entry deleted successfully.',
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
                                    text: 'internal server occur deleting the record.',
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