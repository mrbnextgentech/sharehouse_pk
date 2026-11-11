$(document).ready(function () {
    //validation
    $('#txtMobile').on('input', function () {
        $('#spanMobile').html($('#txtMobile').val() === "" ? "*" : "");
    });
    //End Validation
    $('#sendOTPForm').submit(function (event) {
        event.preventDefault(); // Prevent default form submission        
        var userId = $('#txtUserId').val();
        var mobileNum = $('#txtMobile').val();
        // Removing hyphen (-) if it exists
        mobileNum = mobileNum.replace(/-/g, '');

        // Adding '+92' at the start if it doesn't already exist
       // debugger;
        if (!mobileNum.startsWith('+92') && mobileNum!="") {
            mobileNum = '+92' + mobileNum;
        }
        var formData = new FormData();
         
        formData.append("userId", userId);
        formData.append('mobilenumber', mobileNum)

        var closeButton = $('.btn-close');        

        //debugger;
        // Perform AJAX form submission
        if (userId != "" && mobileNum!="") {
            $.ajax({
                url: "/profile/sendOTPonMobile",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    //closeButton.trigger('click');
                    //debugger;
                    loadSecurityCodeViewinPopup(mobileNum);
                    //$('#basicModal').css('display', 'block');
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
        }
        else // validation else part
        {
            if (mobileNum === "") {
               $('#spanMobile').html('*');
                //document.getElementById('spanMobile').innerHTML = "*";
                $('#txtMobile').focus();
            }
            if (userid === "") {
                swal.fire(
                    {
                        icon: 'error',
                        title:'Error',
                        text: 'sign in again to verify mobile number',
                        showConfirmButton: false,
                        timer:2000,
                    }
                );
            }
            
        }
    });

    /// Load varify mobile/OTP view
    function loadSecurityCodeViewinPopup(mobileNum) {        
        $.ajax({
            /* url: '/profile/verifymobilenumber?mobilenumber=' + mobileNum,*/
            url: '/profile/verifymobilenumber?mobilenumber=' + encodeURIComponent(mobileNum),
            type: 'GET',
            success: function (data) {
                //console.log(data);
                //$('#accountSettingsContainer').html(data);
                $('#modalTitle').empty().html('Enter your security code');
                $('#modalContainer').empty().html(data);
            },
            error: function (xhr, status, error) {
                // Handle error
                debugger;
                console.log(xhr);
            }
        });
    }

    ///End load varify OTP

});