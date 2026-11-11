function removePersonfromShortlist(personlistingId) {
    // Perform AJAX form submission
    if (personlistingId != null) {
        /// ajax call to delete account starts
        $.ajax({
            url: "/listing/removePersonfromshortlist?id=" + personlistingId,
            type: "Get",
            success: function (result) {
                if (result == "shortlisted") {
                    swal.fire(
                        {
                            icon: 'info',
                            title: 'Information',
                            text: 'This listing has already been removed from the shortlist',
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
                        text: 'Listing removed from the shortlist',
                        showConfirmButton: false,
                        timer: 2000,
                    }
                    );
                    window.location.reload();
                }
            },
            error: function (xhr, status, error) {
                swal.fire(
                    {
                        icon: 'error',
                        title: 'Error',
                        text: 'internal server error occur adding list to shotlist.',
                        showConfirmButton: false,
                        timer: 2000,
                    }
                );
            }
        });
        /// End of ajax call to delete account

    }
}