
function addTemplateType(typeId) {
   // debugger;
    var url = null;     
    if (typeId > 0)
        url = "/admin/site/addtemplatetype?typeId=" + typeId;
    else
        url = "/admin/site/addtemplatetype";

    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
           // debugger;
            if (typeId > 0) {
                $('#basicTitle').empty().html('Edit Template Type');                 
            }
            else {
                $('#basicTitle').empty().html('Add Template Type');                
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
