$(document).ready(function () {
    $('#btnActivateListing').click(function () {
        var listingId = $('#txtpreviewPersonId').val().trim();
        //console.log(listingId);
        if (listingId > 0) {
            $.ajax({
                url: '/findplace/activatelisting',
                type: 'GET',
                data: { personlistingId: listingId },
                success: function (result) {
                    //console.log(result);
                    //var successMessage = "Listing activated successfully!";
                    //toastr.success(successMessage, "Success");
                    $('#btnActivateListing').prop('disabled', true);
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Listing activated successfully!',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
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
