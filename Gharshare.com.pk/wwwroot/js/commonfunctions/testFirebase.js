// Import the functions you need from the SDKs you need
import { initializeApp } from "https://www.gstatic.com/firebasejs/10.5.2/firebase-app.js";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
const firebaseConfig = {
    apiKey: "AIzaSyCD-ubUdIJY7TUBnEGNJ6m_eG2gXuhmxvg",
    authDomain: "localhost",// "sharehousepk.firebaseapp.com",
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
    return phoneNum; // Example phone number
};

// Attach a click event handler to the button with ID 'sendOTP'
document.getElementById('sendOTP').addEventListener('click', () => {
    // Get the phone number from user input
    console.log('sendOTP clicked');
    const phoneNumber = getPhoneNumberFromUserInput();
    console.log(phoneNumber);
    // Create the appVerifier
    const appVerifier = recaptchaVerifier;
    //debugger;
    // Sign in with phone number using Firebase authentication
    signInWithPhoneNumber(auth, phoneNumber.trim(), appVerifier)
        .then((result) => {
            // SMS sent. Show UI to verify OTP   
            // Store the confirmation result in the global variable
            confirmationResult = result;
            alert('OTP send out'); 
        })
        .catch((error) => {
            console.error(error);
            alert(error.message);
        });
});
// Function to handle sign-in after OTP entry
function verifyOTP() {
    const otpInput = document.getElementById('otpInput');
    const otp = otpInput.value;

    // Confirm the verification with the entered OTP
    confirmationResult.confirm(otp)
        .then((userCredential) => {
            // User is signed in after successful verification
            const user = userCredential.user;
            console.log('User signed in:', user);
            alert('you entered correct OTP');
        })
        .catch((error) => {
            console.error(error);
            alert(error.message);
        });
}
// Attach a click event listener to the sign-in button
document.getElementById('btnVerify').addEventListener('click', verifyOTP);