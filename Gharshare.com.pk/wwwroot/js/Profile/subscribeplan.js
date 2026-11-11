$(document).ready(function () {
    // Find all <a> tags with the class 'edit'
    $("a.subscribe").on("click", function (event) {
        event.preventDefault(); // Prevent default link behavior

        // Extract the planId from the data-planid attribute
        var planId = $(this).data("planid");

        // Call the loadplan2edit function with the extracted planId
        loadplan(planId);
    });
});


function loadplan(planid)
{
    //debugger;
    $.ajax({ 
        url: '/profile/subscribeplan?planId=' + planid,
        type: 'GET',         
        success: function (data) {
            $('#modalTitle').empty().html('Subscribe Plan');
            $('#accountSettingsContainer').empty().html(data);            
        },
        error: function (xhr, status, error) {
            // Handle error
            //debugger;
            //console.log(xhr);
        }
    });
}
