function sendReply(event, enqType, propertyId, recepientUser, senderUser, roomId,
parentEnqId) {
    event.preventDefault();
    //debugger;
    ///in case of message to personlisting, below two params will be null.
    if (propertyId === 0)
        propertyId = null;
    if (roomId === 0)
        roomId = null;
   //debugger;
    enqType = getEnquirytype(enqType);
    var reply = $('#Message').val();
    var formId = 'replyenquiry';
    var formData = new FormData($('#' + formId)[0]);
    if (enqType != "" && recepientUser != "" && senderUser != ""
         && reply != "") {
        $.ajax({
            url: "/message/sendReply?enqType=" + enqType + "&propertyId=" + propertyId + " &recepient=" + recepientUser +
                "&sender=" + senderUser + "&roomId=" + roomId + "&reply=" + encodeURIComponent(reply) + "&parentEnquiryId=" + parentEnqId,
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function (result) {
                swal.fire(
                    {
                        icon: 'success',
                        title: 'Success',
                        text: 'Reply sent successfully.',
                        showConfirmButton: false,
                        timer: 2000,
                    }
                );
                $('.btn-close').trigger('click');
            },
            error: function (xhr, status, error) {
                //debugger;
                swal.fire(
                    {
                        icon: 'error',
                        title: 'Error',
                        text: 'Error sending message.',
                        showConfirmButton: false,
                        timer: 2000,
                    }
                );
            }
        });
    }
} 

function getEnquirytype(enqType) {
    switch (enqType) {
        case 'aboutproperty':
            return 1;
        case 'aboutperson':
            return 2;
        case 'aboutinspection':
            return 3;
        default:
            return 1; // Add a default case in case none of the above cases match
    }
}
