function formatMobileNumber(input) {
    var mobileValue = input.value;

    // Remove any non-numeric characters
    mobileValue = mobileValue.replace(/[^0-9]/g, '');

    // Remove the first 0 if it exists
    mobileValue = mobileValue.replace(/^0/, '');

    // Add dashes in the appropriate places
    mobileValue = mobileValue.replace(/^(\d{3})(\d{7})$/, '+92-$1-$2');

    //let txtmobile = document.getElementById("mobile");
    //txtmobile.addEventListener("keydown", (e) => {
    input.addEventListener("keydown", (e) => {
    var mobileValue = input.value;
        if (e.key == "Backspace") {
            mobileValue = mobileValue.substring(0, mobileValue.length - 1);
            input.value = mobileValue;
        }        
    });
    // Update the input field with the formatted value
    input.value = mobileValue;
    //$(input).valid();
    
}
