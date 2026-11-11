
function addAccepting(Id) {
   //debugger;
    var url = null;     
    if (Id > 0)
        url = "/admin/site/addaccepting?Id=" + Id;
    else
        url = "/admin/site/addaccepting";

    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
           // debugger;
            if (Id > 0) {
                $('#basicTitle').empty().html('Edit Accepting');                 
            }
            else {
                $('#basicTitle').empty().html('Add Accepting');                
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
