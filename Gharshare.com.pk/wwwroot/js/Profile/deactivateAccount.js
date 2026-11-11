$(document).ready(function () {

    $('#deactivateAccountForm').submit(function (event) {
        event.preventDefault(); // Prevent default form submission

        var userId = $('#txtUserId').val();

        var formData = new FormData();

        formData.append("userId", userId);         
        
        // Perform AJAX form submission
        if (userId != "") {

            Swal.fire({
                title: 'Confirm deactivating your account?',
                text: "Deactivating your account means your listings will be taken down and your listing alerts will be turned off. You can still access your account to create new listings",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#FF005D',
                cancelButtonColor: '#082642',
                confirmButtonText: 'Yes, deactivate it!'
            }).then((result) => {
                if (result.isConfirmed) {

                    /// call to deactivate account starts
                    $.ajax({
                        url: "/profile/deActivateaccount",
                        type: "POST",
                        data: formData,
                        contentType: false,
                        processData: false,
                        success: function (data) {
                            $('#AccountModal').css('display', 'none');
                            swal.fire(
                                {
                                    icon: 'success',
                                    title: 'Success',
                                    text: 'Account deactivated successfully.',
                                    showConfirmButton: false,
                                    timer: 2000,
                                }
                            );
                        },
                        error: function (xhr, status, error) {
                            swal.fire(
                                {
                                    icon: 'error',
                                    title: 'Error',
                                    text: 'Error updating profile.',
                                    showConfirmButton: false,
                                    timer: 2000,
                                }
                            );
                        }
                    });

                /// call to deactivate account starts


                }
            });        
        }         
    });
});