$(document).ready(function () {

    $('#deleteAccount').submit(function (event) {
        event.preventDefault(); // Prevent default form submission

        var userId = $('#txtUserId').val();

        var formData = new FormData();

        formData.append("userId", userId);         
        
        // Perform AJAX form submission
        if (userId != "") {           
            //// confirm delete starts
            Swal.fire({
                title: 'Confirm deleting your account?',
                text: "Your account will be deleted permanantly. "+
                "You won't be able to revert this back!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#FF005D',
                cancelButtonColor: '#082642',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    /// ajax call to delete account starts
                    $.ajax({
                        url: "/profile/deleteaccount",
                        type: "POST",
                        data: formData,
                        contentType: false,
                        processData: false,
                        success: function (data) {
                            $('#AccountModal').css('display', 'none');
                           // window.location = "/home/";
                            swal.fire(
                                {
                                    icon: 'success',
                                    title: 'Success',
                                    text: 'Account deleted successfully.',
                                    showConfirmButton: false,
                                    timer: 2000,
                                }                                
                            );
                            setTimeout(function () {
                                window.location.href = "/";
                            }, 2000);
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
                    /// End of ajax call to delete account

                }
            })

            /////

        }         
    });
});