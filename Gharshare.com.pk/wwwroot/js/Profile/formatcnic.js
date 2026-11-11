function formatCnic(cnicInput) {
    var cnicInput = document.getElementById("cnic");
    var cnicValue = cnicInput.value;

    // Remove any non-numeric characters
    cnicValue = cnicValue.replace(/[^0-9]/g, '');

    // Add dashes in the appropriate places
    cnicValue = cnicValue.replace(/^(\d{5})(\d{7})(\d{1})$/, '$1-$2-$3');

    // Update the input field with the formatted value
    cnicInput.value = cnicValue;
}
