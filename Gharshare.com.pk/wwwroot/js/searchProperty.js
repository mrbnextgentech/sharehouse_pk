$(document).ready(function () {
    // Get the anchor tag
    var findNowBtn = $('#findNowBtn');

    // Get the two dropdowns
    var cityDropdown = $('#cityDropdown');
    var propertyTypeDropdown = $('#propertyTypeDropdown');

    // Attach a click event handler to the anchor tag
    findNowBtn.on('click', function (event) {
        // Prevent the default behavior of the anchor tag
        event.preventDefault();

        // Get the selected values from the two dropdowns
        var selectedCity = cityDropdown.val();
        var selectedPropertyType = propertyTypeDropdown.val();
        console.log(selectedPropertyType);

        // Construct the URL with the selected values as route parameters
        var url = '/property/search' + '?cid=' + selectedCity + '&type=' + selectedPropertyType;
        //debugger;
        //console.log(url);
        window.location.href = url;
        
    });
});