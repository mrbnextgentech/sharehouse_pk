$(document).ready(function () {
    $('#frmInspectionSchedule').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        debugger;
        var inspectionDate = $('#txtInspectionDate').val();
        var from = $('#ddlFrom').val();
        var to = $('#ddlTo').val();
        var formId = 'frmInspectionSchedule';

        var formData = new FormData($('#' + formId)[0]);
        var closeButton = $('.btn-close');
        if (inspectionDate != null && inspectionDate != "" && from != null && from != "" && to != null && to !="") {
            $.ajax({
                url: '/listing/updatepropertyinspection',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                    closeButton.trigger('click');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Property inspection schedule updated successfully!',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                    window.location.reload();
                },
                error: function (xhr, status, error) {
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Error property inspection!',
                            showConfirmButton: true,
                            //timer: 2000,
                        }
                    );
                }
            });
        }
        //---End of Ajax call to save step in database---//
        else {
            swal.fire(
                {
                    icon: 'error',
                    title: 'Validation error',
                    text: 'Fill in the form completly!',
                    showConfirmButton: true,
                    //timer: 2000,
                }
            );
        }
    });

    //$('#txtvideourl').on('input', function () {
    //    var videoUrl = $('#txtvideourl').val().trim();

    //    if (videoUrl != "") {
    //        $('#spanvideourl').html('');
    //    }
    //    else {
    //        $('#spanvideourl').html('&nbsp;&nbsp;required');
    //    }
    //});

});