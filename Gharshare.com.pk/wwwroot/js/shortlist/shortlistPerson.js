function shortlistPerson(personlistingId, userId) {     
    // Perform AJAX form submission
    if (userId != "" && personlistingId != null) {
        /// ajax call to delete account starts
        $.ajax({
            url: "/listing/addPersontoshortlist?id=" + personlistingId + "&&userId=" + userId,
            type: "Get",
            success: function (result) {
                if (result == "shortlisted") {
                    swal.fire(
                        {
                            icon: 'info',
                            title: 'Information',
                            text: 'This person has already been shortlisted',
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
                        text: 'The person has successfully shortlisted',
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
                        text: 'internal server error occur shortlisting the person.',
                        showConfirmButton: false,
                        timer: 2000,
                    }
                );
            }
        });
        /// End of ajax call to delete account

    }
}