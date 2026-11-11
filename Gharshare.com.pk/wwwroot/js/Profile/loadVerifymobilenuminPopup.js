function loadVerifymobilenuminPopup() { 
    
    // var userId = $('#txtUserId').val();
    //debugger;
    $.ajax({ 
        url: '/profile/sendOTPonMobile', //? userId =+ userId,
        type: 'GET',        
        beforeSend: function () {
            // Show the preloader before the AJAX request is sent
            $('#preloader').removeClass('d-none');
        },
        success: function (data) {
            console.log(data);             
            $('#preloader').css('display', 'none');
            $('#modalTitle').empty().html('Verify your mobile number');
            $('#modalContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}
