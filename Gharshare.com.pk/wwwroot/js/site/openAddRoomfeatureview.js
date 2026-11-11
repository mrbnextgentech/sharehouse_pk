
function addRoomfeature(Id) {
   //debugger;
    var url = null;     
    if (Id > 0)
        url = "/admin/site/addRoomfeature?Id=" + Id;
    else
        url = "/admin/site/addRoomfeature";

    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
           // debugger;
            if (Id > 0) {
                $('#basicTitle').empty().html('Edit Room Feature');                 
            }
            else {
                $('#basicTitle').empty().html('Add Room Feature');                
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
