 function loadservice2delete(serviceId)
{
    //debugger;
    $.ajax({ 
        url: '/admin/service/delete?serviceid=' + serviceId,
        type: 'GET',         
        success: function (data) {
            $('#modalTitle').empty().html('Delete Service');
            $('#modalContainer').empty().html(data);            
        },
        error: function (xhr, status, error) {
            // Handle error
            //debugger;
            //console.log(xhr);
        }
    });
}
