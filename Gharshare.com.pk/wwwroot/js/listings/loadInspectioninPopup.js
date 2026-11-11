function loadInspection(inspectionId) {
    //debugger;
    $.ajax({
        url: '/listing/editinspectionschedule?inspectionId=' + inspectionId,
        type: 'GET',
        success: function (data) {             
            // debugger;
            $('#modalTitle').empty().html('Edit Inspection Schedule');
            $('#modalContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}