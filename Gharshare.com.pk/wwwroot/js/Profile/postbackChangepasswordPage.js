function postbackChangepasswordPage() {
    //debugger;
    

    //validation
    $('#txtOldpassword, #txtNewpassword,#txtConfirmpassword').on('input', function () {
        $('#spanOldpassword').html($('#txtOldpassword').val() === "" ? "*" : "");
        $('#spanNewpassword').html($('#txtNewpassword').val() === "" ? "*" : "");
        $('#spanConfirmpassword').html($('#txtConfirmpassword').val() === "" ? "*" : "");
    });

    $('#txtConfirmpassword').on('change', function () {        
        var Oldpassword = $('#txtOldpassword').val();
        var newPassword = $('#txtNewpassword').val();
        var confirmPassword = $('#txtConfirmpassword').val();
       // debugger;
        if (newPassword != confirmPassword) {             
            swal.fire(
                {
                    icon: 'error',
                    title: 'Error',
                    text: 'Confirm password mismatch.',
                    showConfirmButton: false,
                    timer: 2000,
                }
            );
            $('#txtConfirmpassword').val("");            
            $('#txtConfirmpassword').focus();
        }
    });

    //End Validation

    $('#change-password-form').submit(function (event) {
       // debugger;
        var Oldpassword = $('#txtOldpassword').val();
        var newPassword = $('#txtNewpassword').val();
        var confirmPassword = $('#txtConfirmpassword').val();

        event.preventDefault(); // Prevent default form submission
        var formData = $(this).serialize(); // Serialize the form data
        
        if (Oldpassword != "" && newPassword != "" && confirmPassword != ""
            && (newPassword === confirmPassword)) {
            $.ajax({
                url: '/identity/account/manage/changepassword',
                type: 'POST',
                data: formData, // Send the serialized form data
                success: function (data) {
                    console.log(data);
                    $('#AccountModal').css('display', 'none');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Password updated successfully.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                },
                error: function (xhr, status, error) {
                    // Handle error
                    debugger;
                    console.log(xhr);
                    swal.fire(
                        {
                            icon: 'error',
                            title: 'Error',
                            text: 'Error updating password.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                }
            });
        }
        else { //else part of validation
            if (Oldpassword === "") {
                $('#spanOldpassword').html('*');
                $('#txtOldpassword').focus();
            }
            if (newPassword === "") {
                $('#spanNewpassword').html('*');
                $('#txtOldpassword').focus();
            }
            if (confirmPassword === "") {
                $('#spanConfirmpassword').html('*');
                $('#txtConfirmpassword').focus();
            }
            if (newPassword != confirmPassword) {
                swal.fire(
                    {
                        icon: 'error',
                        title: 'Error',
                        text: 'Confirm password mismatch.',
                        showConfirmButton: true,
                        //timer: 2000,
                    }
                );
            }
        }
        
    });
}
