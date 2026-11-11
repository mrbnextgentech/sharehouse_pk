$(document).ready(function () {

    $('#frmaboutflatmates').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var formId = 'frmaboutflatmates';        
        var formData = new FormData($('#' + formId)[0]);
        var aboutflatmates = $('#txtaboutflatmates').val().trim();
        // Perform AJAX form submission
        /*if (description != "") {*/
            var closeButton = $('.btn-close');
            /// call to update property details
            $.ajax({
                url: "/listing/updateaboutflatmates",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    $('#aboutflatmates').html(data.aboutFlatmates ? data.aboutFlatmates : "NA");
                    closeButton.trigger('click');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'about flatmates updated successfully.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                },
                error: function (xhr, status, error) {
                    swal.fire(
                        {
                            icon: 'error',
                            title: 'Error',
                            text: 'Error updating about flatmates.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                }
            });
        //}
        //else {
        //    $('#spandescription').html('&nbsp;&nbsp;required');
        //}

                /// call to deactivate account starts
      
    });

    //$('#txtdescription').on('input', function () {
    //    var description = $('#txtdescription').val().trim();

    //    if (description != "") {
    //        $('#spandescription').html('');
    //    }
    //    else {
    //        $('#spandescription').html('&nbsp;&nbsp;required');
    //    }
    //});
});