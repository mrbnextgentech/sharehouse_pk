$(document).ready(function () {
    $('#frmvideotour').submit(function (event) {
        event.preventDefault(); // Prevent default form submission

        var videoUrl = $('#txtvideourl').val();
        var formId = 'frmvideotour';
        var formData = new FormData($('#' + formId)[0]);
        var closeButton = $('.btn-close');
        if (videoUrl != null && videoUrl != "") {
            $.ajax({
                url: '/listing/savepropertyvideotour',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (data) { 
                    closeButton.trigger('click');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Video tour saved successfully!',
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
                            text: 'Error saving video tour!',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                }
            });
        }
        //---End of Ajax call to save step in database---//
        else {
            $('#spanvideourl').html('&nbsp;&nbsp;required');
        }

    });

    $('#txtvideourl').on('input', function () {
        var videoUrl = $('#txtvideourl').val().trim();

        if (videoUrl != "") {
            $('#spanvideourl').html('');
        }
        else {
            $('#spanvideourl').html('&nbsp;&nbsp;required');
        }
    });

});