
function openDeleteCityView(cityId) {

    $.ajax({
        url: "/admin/site/deletecity?cityId=" + cityId,
        type: 'GET',
        success: function (data) {
            // debugger;
            $('#basicTitle').empty().html('Delete City');
            $('#basicContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            //debugger;
            //console.log(xhr);
        }
    });
}
