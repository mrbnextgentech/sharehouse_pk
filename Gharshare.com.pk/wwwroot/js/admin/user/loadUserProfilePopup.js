function loadUserProfile(userId) {    
    $('#txtUserId').val(userId);
     
   //debugger;
    $.ajax({ 
        url: '/admin/user/edituser?userId=' + userId,
        type: 'GET',
        beforeSend: function () {
            // Show the preloader before the AJAX request is sent
            $('#preloader').removeClass('d-none');
        },
        success: function (data) {
            $('#preloader').css('display', 'none');             
            $('#modalTitle').empty().html('Edit Account');
            $('#modalContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}
