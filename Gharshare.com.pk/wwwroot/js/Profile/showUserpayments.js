function showUserpayments() {
     
    //debugger;
    $.ajax({ 
        url: '/profile/userpayments',
        type: 'GET',        
        beforeSend: function () {
            // Show the preloader before the AJAX request is sent
            $('#preloader').removeClass('d-none');
        },
        success: function (data) {
            console.log(data);             
            $('#preloader').css('display', 'none');
            $('#Title').empty().html('Transaction History');
            $('#accountSettingsContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            console.log(xhr);
        }
    });
}
