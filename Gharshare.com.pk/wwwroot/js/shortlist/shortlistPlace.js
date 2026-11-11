function shortPlacelisting(placelistingId, userId) {
    // Perform AJAX form submission
    if (userId != "" && placelistingId != null) {
        /// ajax call to delete account starts
        $.ajax({
            url: "/listing/addtoshortlist?id=" + placelistingId + "&&userId=" + userId,
            type: "Get",
            success: function (result) {
                if (result == "shortlisted") {
                    swal.fire(
                        {
                            icon: 'info',
                            title: 'Information',
                            text: 'This listing has already been shortlisted',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                }
                else { 
                swal.fire(
                    {
                        icon: 'success',
                        title: 'Success',
                        text: 'Listing added to the shortlist',
                        showConfirmButton: false,
                        timer: 2000,
                    }
                    );
                }
            },
            error: function (xhr, status, error) {
                swal.fire(
                    {
                        icon: 'error',
                        title: 'Error',
                        text: 'internal server occured shortlisting the property.',
                        showConfirmButton: false,
                        timer: 2000,
                    }
                );
            }
        });
        /// End of ajax call to delete account

    }
}