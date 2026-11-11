$(document).ready(function () {
    // Get the URL query parameters
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    // Check if the 'type' parameter is equal to 'to'
    if (urlParams.get('type') === 'to') {
        $('#inbox').addClass('active');
        $('#sent').removeClass('active');
    }
    // Check if the 'type' parameter is equal to 'from'
    else if (urlParams.get('type') === 'from') {
        $('#sent').addClass('active');
        $('#inbox').removeClass('active');
    }
});