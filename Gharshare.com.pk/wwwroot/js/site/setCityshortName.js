$(function () {
    // Function to capitalize the first letter of txtCity on change
    $("#txtCity").on("change", function () {
        var inputValue = $(this).val();
        if (inputValue.length > 0) {
            inputValue = inputValue.charAt(0).toUpperCase() + inputValue.slice(1);
            $(this).val(inputValue);
        }
    });

    // Function to update txtCityShortName based on txtCity
    $("#txtCity").on("input", function () {
        var inputValue = $(this).val();
        if (inputValue.length >= 3) {
            var shortName = inputValue.substring(0, 3).toUpperCase();
            $("#txtCityShortName").val(shortName);
        }
    });
});
