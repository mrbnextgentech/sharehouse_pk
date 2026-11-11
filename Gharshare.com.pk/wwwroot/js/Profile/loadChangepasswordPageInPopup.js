function loadChangepasswordPageInPopup() {
    var userId = $('#txtUserId').val();
    //debugger;
    $.ajax({
        url: '/identity/account/manage/changepassword',
        type: 'GET',
        success: function (data) {
            $('#modalTitle').empty().html('Change your account password');
            $('#modalContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}
