$(document).ready(function () {
    var selectedTab = $('#txtviewselectedTab').val();
    switch (selectedTab) {
        case "rooms":
            $('#tabRooms').addClass('active');
            $('#tabRooms').addClass('show');
            $('#tabFlatmates').removeClass('active');
            $('#tabTeamups').removeClass('active');
            $('#divPropFilters').removeClass('d-none');
            $('#divFlatemateFilters').addClass('d-none');
            break;
        case "flatmates":
            $('#tabRooms').removeClass('active');
            $('#tabFlatmates').addClass('active');
            $('#tabTeamups').removeClass('active');
            $('#divPropFilters').addClass('d-none');
            /*$('#divPropFilters').attr('displane', 'none');*/
            $('#divPropFilters').css('display', 'none');
            $('#divFlatemateFilters').removeClass('d-none');
            break;
        case "teamups":
            $('#tabRooms').removeClass('active');
            $('#tabFlatmates').removeClass('active');
            $('#tabTeamups').addClass('active');
            /*$('#divPropFilters').addClass('d-none');*/
            $('#divPropFilters').css('display', 'none');
            $('#divFlatemateFilters').removeClass('d-none');
            break;
        default:
            // Handle any default behavior or other cases here
            break;
    }
});
