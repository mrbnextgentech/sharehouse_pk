$(document).ready(function () {
    // Find all <a> tags with the class 'edit'
    $("a.edit").on("click", function (event) {
        event.preventDefault(); // Prevent default link behavior

        // Extract the planId from the data-planid attribute
        var planId = $(this).data("planid");

        // Call the loadplan2edit function with the extracted planId
        loadplan2edit(planId);
    });
    $("a.delete").on("click", function (event) {
        event.preventDefault(); // Prevent default link behavior

        // Extract the planId from the data-planid attribute
        var planId = $(this).data("planid");

        // Call the loadplan2edit function with the extracted planId
        loadplan2delete(planId);
    });
});


function loadplan2edit(planid) {
    //debugger;
    $.ajax({
        url: '/admin/plan/loadplan2edit?planId=' + planid,
        type: 'GET',
        success: function (data) {
            $('#modalTitle').empty().html('Edit Plan');
            $('#modalContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            //debugger;
            //console.log(xhr);
        }
    });
}
function loadplan2delete(planid) {
    //debugger;
    $.ajax({
        url: '/admin/plan/loadplan2delete?planId=' + planid,
        type: 'GET',
        success: function (data) {
            $('#modalTitle').empty().html('Delete Plan');
            $('#modalContainer').empty().html(data);
        },
        error: function (xhr, status, error) {
            // Handle error
            //debugger;
            //console.log(xhr);
        }
    });
}
