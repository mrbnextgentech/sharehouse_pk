function loadVideotourView(propertyId) { 
    $.ajax({ 
        url: '/listing/loadvideotour?listingId=' + propertyId,
        type: 'GET',         
        success: function (data) {
            $('#modalTitle').empty().html('Add or update video tour');
            $('#modalContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}
