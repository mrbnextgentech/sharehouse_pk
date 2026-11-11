$(document).ready(function () {     
    $('#txtUsersearch').on('input', function () {
        //debugger;
        var searchPhrase = $(this).val();
       // debugger;
        //ajax call
        $.ajax({
            url: '/admin/user/getuserlist?SearchText=' + searchPhrase,
            type: 'GET',
            success: function (data) {
               // debugger;
                createUserlist(data);
                //debugger;
                // Find all <a> tags with the class 'edit' to attach 'loadplan2edit()' on click.
                //$("a.edit").on("click", function (event) {                    
                //    event.preventDefault(); // Prevent default link behavior

                //    // Extract the planId from the data-planid attribute
                //    var planId = $(this).data("planid");

                //    // Call the loadplan2edit function with the extracted planId
                //    loadplan2edit(planId);
                //});
                //End find all.....
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
