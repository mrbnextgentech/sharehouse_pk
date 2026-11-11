function initializeCKE(event, enqType, propertyId, recepientUser, senderUser, address ) {
   //debugger;
    event.preventDefault();
    if (enqType === 'aboutinspection') {
        ///Get property inspections from database
        $.ajax({
            url: '/message/getPropertyInspections?propertyId=' + propertyId + "&recepient=" + recepientUser +
                "&sender=" + senderUser + "&propAddress=" + address,
            type: 'GET',
            success: function (inspectionData) {
                //debugger;
                $('#Message').val('');
                $('#Message').val(inspectionData);
                initializeCKEditor('Message');
            },
            error: function (xhr, status, error) {
                // Handle error
                debugger;
                console.log(xhr);
            }
        });
    }
    else {
        $('#Message').val('');
        initializeCKEditor('Message');    }    
    $('#dvReply').addClass('d-none');
    $('#dvSend').removeClass('d-none');
}

$(function () {
    // Call the function to initialize CKEditor in enquiry.cshtml to reply.
    
    ///.............
    $('#cancel').click(function (e) {
        e.preventDefault();        
        $('.btn-close').trigger('click');
        //$('#dvReply').removeClass('d-none');
        //$('#dvSend').addClass('d-none');
        //$('#editor').empty();
         
    });
    // Function to scroll the popup to the bottom
    function scrollPopupToBottom() {
        debugger;
        var popup = $('#basicModal');
        popup.animate({ scrollTop: popup[0].scrollHeight }, 500); // Adjust the animation speed as needed
    }

    // Call this function whenever the content in your popup changes
    function updatePopupContent() {
        // Your code to update the popup content goes here

        // After updating the content, scroll the popup to the bottom
        scrollPopupToBottom();
    }

});