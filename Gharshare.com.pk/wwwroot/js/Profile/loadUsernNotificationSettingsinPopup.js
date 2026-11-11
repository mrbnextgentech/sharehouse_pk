function loadUsernNotificationSettingsinPopup() {
    //debugger;
    //var userId = $('#txtUserId').val();
    //debugger;
    $.ajax({ 
        url: '/privacy/loadnotificationssettingsinpopup',//? userId =+ userId,
        type: 'GET',         
        success: function (data) {
           // console.log(data);             
            $('#modalTitle').empty().html('Email Alert Settings');
            $('#modalContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}
