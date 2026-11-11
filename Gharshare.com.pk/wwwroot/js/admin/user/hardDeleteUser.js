$(document).ready(function () {

    $('#frmUser').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
         
        var userId = $('#txtUserId').val();

        var formId = 'frmUser';
        var formData = new FormData($('#' + formId)[0]);
        var closeButton = $('.btn-close');
        // Perform AJAX form submission
        if (userId != "") {           
            //// confirm delete starts
            Swal.fire({
                title: 'Confirm deleting the user account?',
                text: "The account will be deleted permanantly. "+
                "This won't be revert back!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#FF005D',
                cancelButtonColor: '#082642',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.isConfirmed) {

                    /// ajax call to delete account starts
                    $.ajax({
                        url: "/admin/user/harddeleteuser",
                        type: "POST",
                        data: formData,
                        contentType: false,
                        processData: false,
                        success: function (data) {                           
                            createUserlist(data);
                            swal.fire(
                                {
                                    icon: 'success',
                                    title: 'Success',
                                    text: 'User permanantly deleted.',
                                    showConfirmButton: false,
                                    timer: 2000,
                                }                                
                            );
                            closeButton.trigger('click');                            
                            //window.location.reload();
                        },
                        error: function (xhr, status, error) {
                            swal.fire(
                                {
                                    icon: 'error',
                                    title: 'Error',
                                    text: 'internal server occur deleting the user account.',
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