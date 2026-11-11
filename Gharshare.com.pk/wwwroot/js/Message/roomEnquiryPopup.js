function roomEnquiryPopup(roomId,propertyid) {    
     
    $.ajax({ 
        url: '/message/openRoomEnquiryPopup?roomid=' + roomId +"&propertyid=" + propertyid,
        type: 'GET',
        beforeSend: function () {
            // Show the preloader before the AJAX request is sent
            $('#preloader').removeClass('d-none');
        },
        success: function (data) {
            $('#preloader').css('display', 'none');             
            $('#modalTitle').empty().html('Enquire about this room');
            $('#modalContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}
