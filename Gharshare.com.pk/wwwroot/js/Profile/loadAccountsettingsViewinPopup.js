function loadAccountSettings() {    
    var userId = $('#txtUserId').val();
    //var formData = new FormData();
    //formData.append("userId", userId);
   // debugger;
    $.ajax({ 
        url: '/profile/accountsettings?userId=' + userId,
        type: 'GET',
        beforeSend: function () {
            // Show the preloader before the AJAX request is sent
            $('#preloader').removeClass('d-none');
        },
        success: function (data) {
            $('#preloader').css('display', 'none');
            //console.log(data);
            /*$('#accountSettingsContainer').empty().html(data);*/
            $('#modalTitle').empty().html('Edit My Account');
            $('#modalContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}
