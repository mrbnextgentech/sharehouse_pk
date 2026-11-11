$(document).ready(function () {
    $('#ddlfilter').on('change', function () {
        var selectedFilter = $(this).find(':selected').val();
        $('#filter').val(selectedFilter);
        $('#frmSearch').submit();
    });
    //debugger;
    var address = $('#txthiddenaddress').val();    
    $('#address').val(address);
    var searchType = $('#frmSearch #txtsearchtype').val();
    switch (searchType) {
        case "rooms":
            $('#tabRooms').addClass('active');
            $('#tabRooms').addClass('show');
            $('#tabFlatmates').removeClass('active');
            $('#tabTeamups').removeClass('active');
            break;
        case "flatmates":
            $('#tabRooms').removeClass('active');            
            $('#tabFlatmates').addClass('active');
            $('#tabTeamups').removeClass('active');
            break;
        case "teamups":
            $('#tabRooms').removeClass('active');
            $('#tabFlatmates').removeClass('active');
            $('#tabTeamups').addClass('active');
            break;
        default:
            // Handle any default behavior or other cases here
            break;
    }


});
