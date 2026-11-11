$(document).ready(function () {
    $('#frmMessage').submit(function (event) {
        event.preventDefault();
        debugger;
        var formId = 'frmMessage';
        var formData = new FormData($('#' + formId)[0]);
        console.log(formData);
        var message = $('#txtMessage').val();
        if (message != "" && ValidateRoomValue()) {
            $.ajax({
                url: "/message/sendmessage",
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
        if(message == "") {
            $('#spanMessage').empty();
            $('#spanMessage').html('*Message is required');
        }
        if (!ValidateRoomValue()) {
            $('#spanRoom').empty();
            $('#spanRoom').html('*Room is required');
        }
    });

    function ValidateRoomValue() {
        const selectElement = document.getElementById('ddlSelect');

        // Check if the select element exists in the DOM and its value is not null or ""
        if (selectElement)
        {
            if (selectElement.value !== "" && selectElement.value !== null) {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }  
    }
    $('#txtMessage').on('input', function () {
        var message = $('#txtMessage').val().trim();
        if (message != "") {
            $('#spanMessage').html('');
        }
        else {
            $('#spanMessage').html('*Message is required');
        }
    });

    $('#ddlSelect').on('change', function () {
        var roomValue = $('#ddlSelect').val().trim();
        if (roomValue != "") {
            $('#spanRoom').html('');
        }
        else {
            $('#spanRoom').html('*Room is required');
        }
    });
});