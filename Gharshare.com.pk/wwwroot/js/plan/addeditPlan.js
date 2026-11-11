
function loadAddplanView() {
    //debugger;
    $.ajax({
        url: '/admin/plan/newplan',
        type: 'GET',
        success: function (data) {
            $('#modalTitle').empty().html('Add Plan');
            $('#modalContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            //debugger;
            //console.log(xhr);
        }
    });
}
