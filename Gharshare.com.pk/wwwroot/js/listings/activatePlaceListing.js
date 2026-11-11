$(document).ready(function () {
    $('#btnActivateListing').click(function () {
        var placeListingId = $('#txtPlaceListingId').val().trim();
        //console.log(placeListingId);
        if (placeListingId > 0) {
            $.ajax({
                url: '/listing/activatelisting',
                type: 'GET',
                data: { placeListingId: placeListingId },
                success: function (result) {                     
                   // $('#btnActivateListing').prop('disabled', true);
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Listing activated successfully!',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                    window.location.reload();
                },
                error: function (xhr, status, error) {
                    var errorMessage = "Error saving step!";
                    toastr.error(errorMessage, "Error");
                    // Handle error
                    debugger;
                    console.log(xhr);
                }
            });
        }
    });
});
