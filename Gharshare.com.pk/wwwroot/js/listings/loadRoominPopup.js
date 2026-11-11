function loadRoom(roomId) { 
    
    var propertyId = $('#txtPropertyId').val();
    
    $.ajax({ 
        url: '/listing/loadroominpopup?roomid=' + roomId,
        type: 'GET',         
        success: function (data) {
            console.log(data);
            $('#accountSettingsContainer').empty().html(data);
            /*bindDatetimepicker();*/
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}
