$(document).ready(function () {
    // Get the current page URL
    var currentPageURL = window.location.href;
    //debugger;
    // Check if the URL contains "search/bycity"
    //debugger;
    if (currentPageURL.includes("search/bycity")) {
        // Update form action to 'filter'
        $('#frmsearch').attr('action', 'bycity');

        // Remove the 'd-none' class from 'divFilters'
        $('#divPropFilters').removeClass('d-none'); 
        $('#btnfilter').removeClass('d-none');
        var cityName = $('#txtcityname').val();
        $('#address').val(cityName);
        var cityId = $('#txtviewcityId').val();
        $('#txtCityId').val(cityId);
        
    } else if (currentPageURL.includes("/search/index")) {

        $('#frmsearch').attr('action', 'index');

        // Add the 'd-none' class to 'divFilters'
        $('#divPropFilters').addClass('d-none');
        $('#btnfilter').addClass('d-none');
    }
    else {
        // Update form action to 'index'
        $('#frmsearch').attr('action', '/search/index');

        // Add the 'd-none' class to 'divFilters'
        $('#divPropFilters').addClass('d-none');
        $('#btnfilter').addClass('d-none');
    }
});