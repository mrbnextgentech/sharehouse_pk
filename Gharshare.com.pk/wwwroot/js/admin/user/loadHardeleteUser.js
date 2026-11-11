function loadHardelUserInPopup(userId) {
    //debugger;
    $.ajax({ 
        url: '/admin/user/harddeleteuser?userId='+userId,
        type: 'GET',
        beforeSend: function () {
            // Show the preloader before the AJAX request is sent
            $('#preloader').removeClass('d-none');
        },
        success: function (data) {
            //console.log(data);
            //$('#accountSettingsContainer').empty().html(data);
            $('#preloader').css('display', 'none');
            $('#modalTitle').empty().html('Permanantly Delete User Account');
            $('#modalContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}
