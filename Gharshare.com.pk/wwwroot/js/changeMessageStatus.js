$(document).ready(function () {

    $('.status').each(function (index, elem) {
        $(elem).children().each(function (i, elmnt) // Get all divs in row
        {
            $(elmnt).change(function () {
                
                var enquiryId;
                enquiryId = $(this).children().attr('data-eid');

                //$(this).children().each(function (j, e) { // alternatively, we can get data-eid 
                                                            //of selected option by iterating thourgh options.iterating
                // enquiryId = $(e).attr('data-eid');
                //});
                var statusId = $(this).val();

                $.ajax({
                    url: '/property/updateEnquiryStatus?statusId=' + statusId + '&&enquiryId=' + enquiryId,
                    success: function (result) {
                        window.location.reload();
                    }
                });
            });
        });
    });

    //$('#ddlstatus').change(function () {
    //    var statusId = $(this).val();
    //    var enquiryId = $('option:selected').attr('data-eid');               
    //    var propertyId = $('option:selected').attr('data-pid'); 
    //    //alert(propertyId);
    //    $.ajax({
    //        url: '/property/updateEnquiryStatus?statusId=' + statusId + '&&enquiryId=' + enquiryId,
    //        success: function (result) {
    //            window.location.reload();
    //        }
    //    });
    //});
});