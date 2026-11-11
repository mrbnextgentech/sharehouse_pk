function loadEnquiryinPopup(enquiryId) {    
     
    $.ajax({ 
        url: '/message/loadenquiry?enquiryid=' + enquiryId,
        type: 'GET',         
        success: function (data) {
            //debugger;
            $('#modalTitle').empty().html('Enquiry');
            $('#modalContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}
