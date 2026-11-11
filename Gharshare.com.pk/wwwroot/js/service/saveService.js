$(function () {

    $('#frmservice').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        debugger;
        var formId = 'frmservice';
        var formData = new FormData($('#' + formId)[0]);
        var isvalid = true;
        /*if ($("#frmservice").valid()) {*/
        if (isvalid) {
        var closeButton = $('.btn-close');

        $.ajax({
            url: '/admin/service/saveservice',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                debugger;
                createServicelist(data);                 
                $('#noplan').html('');
                var successMessage = "Service saved successfully!"
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
                //window.location.reload();
            },
            error: function (xhr, status, error) {
                var errorMessage = "Error saving plan !"
                toastr.error(errorMessage, "Error");
                // Handle error
                //debugger;
                //console.log(xhr);
            }
        });
        }
        else {
            swal.fire(
                {
                    icon: 'error',
                    title: 'Validation error',
                    text: 'All fields required.Please fill in the form completely',
                    showConfirmButton: false,
                    timer: 2000,
                }
            );

        }
    });


    $("#frmservice").validate({
        rules: {
            'service.title': { required: true },
            'service.description': { required: true }             
        },
        messages: {
            'service.title': {
                required: "&nbsp; required"
            },
            'service.description': {
                required: "<span class='text-danger'>&nbsp; required</span>"
            }
        },

    });
});