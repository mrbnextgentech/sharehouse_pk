$(function () {
    $('#frmCity').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        //debugger;
        var formId = 'frmCity';
        var formData = new FormData($('#' + formId)[0]);

        var closeButton = $('.btn-close');
        $.ajax({
            url: '/admin/site/deletecity',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                //debugger;                     
                var successMessage = "City deleted successfully!"
                swal.fire(
                    {
                        icon: 'success',
                        title: 'Success',
                        text: successMessage,
                        showConfirmButton: false,
                        timer: 2000,
                    }
                );
                closeButton.trigger('click');
                window.location.reload();
            },
            error: function (xhr, status, error) {
                //debugger;
                var responseText = JSON.parse(xhr.responseText);
                swal.fire(
                    {
                        icon: 'error',
                        title: 'Error',
                        text: responseText.error,
                        showConfirmButton: false,
                        timer: 2000,
                    }
                );
            }
        });
    });

});