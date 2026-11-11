function loadInspection2Delete(inspectionId) {
    //debugger;
    $.ajax({
        url: '/listing/deleteinspection?inspectionId=' + inspectionId,
        type: 'GET',
        success: function (data) {
            //console.log(data);
            // debugger;
            $('#modalTitle').empty().html('Delete Inspection');
            $('#modalContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}