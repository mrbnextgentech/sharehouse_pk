$(document).ready(function () {
    // Initialize Firebase
    //debugger;
    var config = {
        apiKey: "AIzaSyCD-ubUdIJY7TUBnEGNJ6m_eG2gXuhmxvg",
        authDomain: "sharehousepk.firebaseapp.com",
        databaseURL: "https://groupinger-users.firebaseio.com",
        projectId: "sharehousepk",
        storageBucket: "sharehousepk.appspot.com",
        messagingSenderId: "1:859107969368:web:a0418aef5d944198e47d2d"
    };
    firebase.initializeApp(config);

    window.recaptchaVerifier = new firebase.auth.RecaptchaVerifier('recaptcha-container');
    firebase.auth().signInWithPhoneNumber("+923435289966", window.recaptchaVerifier)
        .then(function (confirmationResult) {
            window.confirmationResult = confirmationResult;
            console.log(confirmationResult);
        });

    var myFunction = function () {
        window.confirmationResult.confirm(document.getElementById("verificationcode").value)
            .then(function (result) {
                console.log(result);
            }).catch(function (error) {
                console.log(error);
            });
    };
});