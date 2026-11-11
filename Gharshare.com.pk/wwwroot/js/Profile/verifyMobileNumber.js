$(document).ready(function () {

    //validation
    $('#txtOtp').on('input', function () {
        $('#spanOtp').html($('#txtOtp').val() === "" ? "*" : "");
    });
    //End Validation

    $('#verifyMobilenumForm').submit(function (event) {
        event.preventDefault(); // Prevent default form submission

        var userId = $('#txtUserId').val();
        var otp = $('#txtOtp').val();

        var formData = new FormData();

        formData.append("userId", userId);         
        formData.append("otp", otp);         

        var closeButton = $('.btn-close');
        
        // Perform AJAX form submission
        if (userId != "" && otp!="") {
            $.ajax({
                url: "/profile/verifymobilenumber",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    //debugger;
                    $('#AccountModal').css('display', 'none');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Mobile number verified successfully.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                    closeButton.trigger('click');
                },
                error: function (xhr, status, error) {
                    swal.fire(
                        {
                            icon: 'error',
                            title: 'Error',
                            text: 'Invalid security code, mobile number verification failed.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                }
            });
        } 
        else // validation else part
        {
            if (otp === "") {
                $('#spanOtp').html('*');
                //document.getElementById('spanMobile').innerHTML = "*";
                $('#txtOtp').focus();
            }
            if (userid === "") {
                swal.fire(
                    {
                        icon: 'error',
                        title: 'Error',
                        text: 'sign in again to verify mobile number',
                        showConfirmButton: false,
                        timer: 2000,
                    }
                );
            }

        }
    });
});