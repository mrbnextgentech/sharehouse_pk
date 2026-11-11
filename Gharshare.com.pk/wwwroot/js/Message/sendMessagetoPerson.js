$(document).ready(function () {
    $('#frmMessagetoperson').submit(function (event) {
        event.preventDefault();
        var formId = 'frmMessagetoperson';
        var formData = new FormData($('#' + formId)[0]);
        console.log(formData);
        var message = $('#txtMessage').val();
        if (message != "")
        {
            $.ajax({
                url: "/message/sendmessagetoperson",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (result) {
                    $('#txtMessage').val('');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Message sent successfully.',
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
        else
        {
            $('#spanMessage').empty();
            $('#spanMessage').html('&nbsp;&nbsp;required');
        }
    });

    $('#txtMessage').on('input', function () {
        var message = $('#txtMessage').val().trim();
        if (message != "") {
            $('#spanMessage').html('');
        }
        else {
            $('#spanMessage').html('&nbsp;&nbsp;required');
        }
    });
});