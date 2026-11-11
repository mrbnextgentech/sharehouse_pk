$(document).ready(function () {
    $('#ddlContentType').on('change', function () {
        var contentyType = parseInt($(this).val().trim());
        // debugger;
        //ajax call
        $.ajax({
            url: '/admin/cms/getcontentlist?type=' + contentyType,
            type: 'GET',
            success: function (data) {
               //debugger;                 
                createContentlist(data);
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
