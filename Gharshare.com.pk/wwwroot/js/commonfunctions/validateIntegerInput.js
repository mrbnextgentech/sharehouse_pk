function validateIntegerInput(input) {
    input.value = input.value.replace(/\D/g, '');
    document.getElementById('spanOtp').innerHTML = "only integer values";
}