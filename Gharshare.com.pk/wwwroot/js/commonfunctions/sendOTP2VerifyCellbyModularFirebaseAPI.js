// Import the functions you need from the SDKs you need
import { initializeApp } from "https://www.gstatic.com/firebasejs/10.5.2/firebase-app.js";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
const firebaseConfig = {
    apiKey: "AIzaSyCD-ubUdIJY7TUBnEGNJ6m_eG2gXuhmxvg",
    authDomain: "sharehouse.pk",
    projectId: "sharehousepk",
    storageBucket: "sharehousepk.appspot.com",
    messagingSenderId: "859107969368",
    appId: "1:859107969368:web:a0418aef5d944198e47d2d"
};
let confirmationResult; // Define confirmationResult as a global variable
// Initialize Firebase
const app = initializeApp(firebaseConfig);
console.log(app);
import { getAuth, RecaptchaVerifier, signInWithPhoneNumber } from "https://www.gstatic.com/firebasejs/10.5.2/firebase-auth.js";
const auth = getAuth(app);
console.log(auth);
window.recaptchaVerifier = new RecaptchaVerifier(auth, 'recaptcha-container', {});
//Render the reCAPTCHA widget
recaptchaVerifier.render().then((widgetId) => {
    // Store the widgetId for future reference
    window.recaptchaWidgetId = widgetId;
});
// Get the phone number from user input (replace with your logic)
const getPhoneNumberFromUserInput = () => {
    // Replace this with your logic to get the user's phone number
    var phoneNum = document.getElementById('number').value;
    phoneNum = phoneNum.replace(/-/g, '');
    if (!phoneNum.startsWith('+92') && phoneNum != "") {
        phoneNum = '+92' + phoneNum;
    }
    console.log(phoneNum);
    return phoneNum; // Example phone number
};

// Attach a click event handler to the button with ID 'sendOTP'
document.getElementById('sendOTP').addEventListener('click', () => {
     //debugger;
    //console.log('sendOTP clicked');
    const phoneNumber = getPhoneNumberFromUserInput();
    var userId = $('#txtUserId').val();
    // Create the appVerifier
    const appVerifier = recaptchaVerifier;
    if (userId != "" && phoneNumber != "") {
        // Sign in with phone number using Firebase authentication
        signInWithPhoneNumber(auth, phoneNumber.trim(), appVerifier)
            .then((result) => {
                // SMS sent. Show UI to verify OTP   
                // Store the confirmation result in the global variable
                confirmationResult = result;
                $('#divSendOTP').hide();
                $('#divVerifyOTP').show();
                //alert('OTP send out'); 
            })
            .catch((error) => {
                console.error(error);
                alert(error.message);
            });
    }
    else // validation else part
    {
        if (phoneNumber === "") {
            $('#spanMobile').html('*');
            //document.getElementById('spanMobile').innerHTML = "*";
            $('#number').focus();
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
// Function to handle verify OTP after entry
function verifyOTP() {
    //debugger;
    const otpInput = document.getElementById('otpInput');
    const otp = otpInput.value;

    //$('#frmVerifyMobilenum').submit(function (event) {
    //    event.preventDefault(); // Prevent default form submission         
         
        var userId = $('#txtUserId').val();
        var formData = new FormData();
        
        formData.append("userId", userId);
        formData.append("otp", otp);

        var closeButton = $('.btn-close');
        if (userId != "" && otp != "") {
            // Confirm the verification with the entered OTP
            confirmationResult.confirm(otp)
                .then((userCredential) => {
                    // User is signed in after successful verification
                    const user = userCredential.user;
                    console.log('User signed in:', user);
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
                                    timer: 3000,
                                }
                            );
                            //closeButton.trigger('click');
                        }
                    }); // end of ajax call                     
                })
                .catch((error) => {
                    //console.error(error);
                    //alert(error.message);
                    swal.fire(
                        {
                            icon: 'error',
                            title: 'Error',
                            text: 'Invalid OTP entered. Mobilie number verfication failed.',
                            showConfirmButton: false,
                            timer: 3000,
                        }
                    );
                    closeButton.trigger('click');
                });
        }
        else // validation else part
        {
            if (otp === "") {
                $('#spanOtp').html('*');
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
    /*});*/
}
// Attach a click event listener to the sign-in button
document.getElementById('btnVerify').addEventListener('click', verifyOTP);
//validation for OTP input
$('#otpInput').on('input', function () {
    $('#spanOtp').html($('#otpInput').val() === "" ? "*" : "");
});
    //End Validation