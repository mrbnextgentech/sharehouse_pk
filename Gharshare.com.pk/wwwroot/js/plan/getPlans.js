$(document).ready(function () {
    $('#ddlplantype').on('change', function () {
        var selectedPlantype = $(this).find(':selected').data('plantype');
        //alert(selectedPlantype);

        //ajax call
        $.ajax({
            url: '/admin/plan/getplanlist?plantype=' + selectedPlantype,
            type: 'GET',
            success: function (data) {
                createplanlist(data);
                //debugger;
                // Find all <a> tags with the class 'edit' to attach 'loadplan2edit()' on click.
                $("a.edit").on("click", function (event) {                    
                    event.preventDefault(); // Prevent default link behavior

                    // Extract the planId from the data-planid attribute
                    var planId = $(this).data("planid");

                    // Call the loadplan2edit function with the extracted planId
                    loadplan2edit(planId);
                });
                //End find all.....
                $("a.delete").on("click", function (event) {
                    event.preventDefault(); // Prevent default link behavior

                    // Extract the planId from the data-planid attribute
                    var planId = $(this).data("planid");

                    // Call the loadplan2edit function with the extracted planId
                    loadplan2delete(planId);
                });
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
