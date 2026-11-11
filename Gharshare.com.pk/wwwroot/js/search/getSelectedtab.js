$(document).ready(function () {
    // Attach click event handlers to the tab links
    $(".nav a").click(function () {
        // Get the value associated with the clicked tab
        var tabValue = $(this).attr("href").split("_").pop();
        var currentPageURL = window.location.href;
        // Update the hidden input field value based on the clicked tab
        switch (tabValue) {
            case "1":
                $("#txtsearchtype").val("rooms");
                break;
            case "2":
                $("#txtsearchtype").val("flatmates");
                break;
            case "3":
                $("#txtsearchtype").val("teamups");
                break;
            default:
                // Handle any other cases here
                break;
        }
        if (currentPageURL.includes("search/bycity")) {
            if (tabValue == "1") {
                // Update form action to 'filter'
                $('#frmsearch').attr('action', 'bycity');

                // Remove the 'd-none' class from 'divPropFilters'
                $('#divPropFilters').removeClass('d-none');
                $('#divFlatemateFilters').addClass('d-none');
                $('#divFlatemateFilters').css('display', 'none');
                $('#divPropFilters').css('display', 'block');
                $('#filterType').val('Property'); //search property listings
                
            }
            else if (tabValue == "2") {
                $('#frmsearch').attr('action', 'bycity');

                // Remove the 'd-none' class from 'divPropFilters'
                $('#divPropFilters').addClass('d-none');
                $('#divFlatemateFilters').removeClass('d-none');
                $('#divFlatemateFilters').css('display', 'block');
                $('#divPropFilters').css('display', 'none');
                $('#filterType').val('Person'); //search person listings
            }
            else if (tabValue == "3") {
                $('#frmsearch').attr('action', 'bycity');

                // Remove the 'd-none' class from 'divPropFilters'
                $('#divPropFilters').addClass('d-none');
                $('#divFlatemateFilters').removeClass('d-none');
                $('#divFlatemateFilters').css('display', 'block');
                $('#divPropFilters').css('display', 'none');
                $('#filterType').val('Teamsup'); //search Teamsup person listings
            }
        } else {

            // Update form action to 'index'
            if (currentPageURL.includes("search/index"))
                $('#frmsearch').attr('action', 'index');
            else
                $('#frmsearch').attr('action', 'search/index');

            // Add the 'd-none' class to 'divPropFilters' & 'divFlatemateFilters'
            $('#divPropFilters').addClass('d-none');
            $('#divFlatemateFilters').addClass('d-none');
        }
    });
});
