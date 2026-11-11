$(function () {

    // Get the element with the ID "dropdownMenuInfo"
    const dropdownMenuInfo = document.getElementById("dropdownMenuInfo");

    // Get the dropdown menu (the <ul> element)
    const dropdownMenu = document.querySelector(".dropdown-menu");

    // Add a mouseover event listener to open the dropdown
    dropdownMenuInfo.addEventListener("mouseover", function () {
        // Trigger a click event to open the dropdown
        dropdownMenuInfo.click();
    });

    // Add a mouseout event listener to close the dropdown when the mouse leaves the entire dropdown area
    dropdownMenu.addEventListener("mouseout", function (e) {
        // Check if the mouse is leaving the entire dropdown area
        if (!dropdownMenu.contains(e.relatedTarget)) {
            // Close the dropdown by removing the "show" class
            dropdownMenu.classList.remove("show");
        }
    });

});
