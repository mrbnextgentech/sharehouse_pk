window.onload = function () {
    render();
};

function render() {
    window.recaptchaVerifier = new firebase.auth.RecaptchaVerifier('recaptcha-container');
    recaptchaVerifier.render();
} 
function phoneAuth() {
    debugger;
        var number = "+92" + document.getElementById('number').value;
        //debugger;
        firebase.auth().signInWithPhoneNumber(number, recaptchaVerifier)
            .then(function (confirmationResult) {
                window.confirmationResult = confirmationResult;
                console.log(confirmationResult);
                alert("Message Sent");
            })
            .catch(function (error) {
                console.error(error);
                alert(error.message);
            });
    }
    // Function for verifying the code
    function codeVerify() {
        var code = document.getElementById('verificationCode').value;
        confirmationResult.confirm(code)
            .then(function (result) {
                alert("Message Verified");
                var user = result.user;
                console.log(user);
                //window.location.href = "/index/registration";
            })
            .catch(function (error) {
                console.error(error);
                alert(error.message);
            });
    } 