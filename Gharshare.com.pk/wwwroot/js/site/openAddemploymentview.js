
function addEmployment(Id) {
   //debugger;
    var url = null;     
    if (Id > 0)
        url = "/admin/site/addemployment?Id=" + Id;
    else
        url = "/admin/site/addemployment";

    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
           // debugger;
            if (Id > 0) {
                $('#basicTitle').empty().html('Edit Employment');                 
            }
            else {
                $('#basicTitle').empty().html('Add Employment');                
            }
            $('#basicContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            //debugger;
            //console.log(xhr);
        }
    });
}
