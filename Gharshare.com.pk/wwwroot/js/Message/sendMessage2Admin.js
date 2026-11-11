$(document).ready(function () {
    $('#frmMessage2Admin').submit(function (event) {
        event.preventDefault();
        //debugger;
        var formId = 'frmMessage2Admin';
        var formData = new FormData($('#' + formId)[0]);
        console.log(formData);
        var message = $('#txtMessage2Admin').val();
        if (message != "") {
            $.ajax({
                url: "/message/sendmessage",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (result) {
                    $('#txtMessage2Admin').val('');                    
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
        if(message == "") {
            $('#spanMessage2Admin').empty();
            $('#spanMessage2Admin').html('*Message is required');
        }
         
    });
     
    $('#txtMessage2Admin').on('input', function () {
        var message = $().val().trim();
        if (message != "") {
            $('#spanMessage2Admin').html('');
        }
        else {
            $('#spanMessage2Admin').html('*Message is required');
        }
    });
});