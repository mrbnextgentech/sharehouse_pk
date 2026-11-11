
function loadAddserviceView() {
    //debugger;
    $.ajax({ 
        url: '/admin/service/create',
        type: 'GET',         
        success: function (data) {
            $('#modalTitle').empty().html('Add Service');
            $('#modalContainer').empty().html(data);            
        },
        error: function (xhr, status, error) {
            // Handle error
            //debugger;
            //console.log(xhr);
        }
    });
}
