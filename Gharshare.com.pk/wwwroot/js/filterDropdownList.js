$(document).ready(function () {
    // Hide the input field container
    $(".custom-dropdown-input-container").hide();

    // Handle click event on the select list
    $(".custom-dropdown-list").on("click", function () {
        // Show the input field container
        $(".custom-dropdown-input-container").show();
    });

    // Handle keyup event on input field
    $(".custom-dropdown-input").on("keyup", function () {
        var inputVal = $(this).val().toLowerCase();
        // Loop through all options in the select list
        $(".custom-dropdown-list option").each(function () {
            var optionVal = $(this).text().toLowerCase();
            // Check if the option text matches the input text
            if (optionVal.indexOf(inputVal) > -1) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    });

    // Handle click event on an option in the select list
    $(".custom-dropdown-list option").on("click", function () {
        // Set the value of the select list to the selected option
        var selectedVal = $(this).val();
        $(".custom-dropdown-list").val(selectedVal);
        // Hide the input field container
        $(".custom-dropdown-input-container").hide();
    });
});
