function loadlistingPhotos() { 
    
    var propertyId = $('#txtListingId').val();
    
    $.ajax({ 
        url: '/listing/loadlistingPhotos?propertyid=' + propertyId,
        type: 'GET',         
        success: function (data) {
            //console.log(data);
            $('#accountSettingsContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}
