
function loadviewAddservice2Plan(planid) {
    //debugger;
    $.ajax({ 
        url: '/admin/plan/loadviewAddservice2Plan?planid=' + planid,
        type: 'GET',         
        success: function (data) {
            $('#modalTitle').empty().html('Add service to plan');
            $('#modalContainer').empty().html(data);            
        },
        error: function (xhr, status, error)
        {
            // Handle error
            //debugger;
            //console.log(xhr);
        }
    });
}
