function addInspection(propertyId) {
    //debugger;
    $.ajax({
        url: '/listing/addinspection?propertyId=' + propertyId,
        type: 'GET',
        success: function (data) {
            // debugger;
            $('#modalTitle').empty().html('Add Inspection');
            $('#modalContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}