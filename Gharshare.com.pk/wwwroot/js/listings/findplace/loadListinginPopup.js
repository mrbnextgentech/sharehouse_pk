function loadlisting(viewname) { 
    
    var listingid = $('#txtPersonlistingId').val();
    
    $.ajax({ 
        url: '/findplace/loadlistinginpopup?listingid=' + listingid +'&&viewname='+ viewname,
        type: 'GET',         
        success: function (data) {
            console.log(data);
            $('#accountSettingsContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}
