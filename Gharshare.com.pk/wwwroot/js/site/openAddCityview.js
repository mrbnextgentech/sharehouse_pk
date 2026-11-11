
function addCity(cityId) {
    //debugger;
    var url = null;     
    if (cityId > 0)
        url = "/admin/site/addcity?cityId=" + cityId;
    else
        url = "/admin/site/addcity";

    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
           // debugger;
            if (cityId > 0) {
                $('#basicTitle').empty().html('Edit City');                 
            }
            else {
                $('#basicTitle').empty().html('Add City');                
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
