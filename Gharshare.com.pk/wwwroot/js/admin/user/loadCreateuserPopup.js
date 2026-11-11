
function loadCreateUser() {
    //debugger;
    $.ajax({
        url: '/admin/register',
        type: 'GET',
        success: function (data) {
            $('#basicTitle').empty().html('Create User');
            $('#basicContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            //debugger;
            //console.log(xhr);
        }
    });
}
