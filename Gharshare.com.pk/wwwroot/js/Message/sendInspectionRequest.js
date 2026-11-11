$(document).ready(function () {
    $('#frmInspection').submit(function (event) {
        event.preventDefault();
        var formId = 'frmInspection';
        var formData = new FormData($('#' + formId)[0]);
        //console.log(formData);
        var message = $('#txtMessage').val();
        var closeButton = $('.btn-close');
        if (message != "") {
            $.ajax({
                url: "/message/sendinspectionrequest",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (result) {
                    $('#txtMessage').val('');
                    closeButton.trigger('click');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Inspection request sent successfully.',
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
                            text: 'Error sending message.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                }
            });

        }
        else {
            $('#spanMessage').empty();
            $('#spanMessage').html('*required');
        }         
    });
   
    $('#txtMessage').on('input', function () {
        var message = $('#txtMessage').val().trim();
        if (message != "") {
            $('#spanMessage').html('');
        }
        else {
            $('#spanMessage').html('*required');
        }
    });
});