 function loadservice2edit(serviceId)
{
    //debugger;
    $.ajax({ 
        url: '/admin/service/edit?serviceid=' + serviceId,
        type: 'GET',         
        success: function (data) {
            $('#modalTitle').empty().html('Edit Service');
            $('#modalContainer').empty().html(data);            
        },
        error: function (xhr, status, error) {
            // Handle error
            //debugger;
            //console.log(xhr);
        }
    });
}
