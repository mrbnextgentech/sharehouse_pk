$(document).ready(function () {
    $('#ddluserType').on('change', function () {
        var usertype = $(this).val().trim();
        // debugger;
        //ajax call
        $.ajax({
            url: '/admin/user/getuserlist?usertype=' + usertype,
            type: 'GET',
            success: function (data) {
                //debugger;                 
                createUserlist(data);
            },
            error: function (xhr, status, error) {
                // Handle error
                //debugger;
                //console.log(xhr);
            }
        });
        //end of ajax call
    });
});
