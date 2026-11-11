$(function () {    

    $('#frmservice').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        //debugger;
        var formId = 'frmservice';
        var formData = new FormData($('#' + formId)[0]);
        if ($("#frmservice").valid()) {
            var closeButton = $('.btn-close');
            
            $.ajax({
                url: '/admin/service/delete',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (data) {
                    //debugger;
                    createServicelist(data);                     
                    $('#noservice').html('');
                    var successMessage = "Service deleted successfully!"
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
                    text: 'Please fill in all the required fields.',
                    showConfirmButton: false,
                    timer: 2000,
                }
            );

        }
    });

    $("#frmservice").validate({
        rules: {
            'Service.Title': { required: true },
            'Service.Description': { required: true },             
        },
        messages: {
            'Service.Title': {
                required: "&nbsp; required"
            },
            'Service.Description': {
                required: "<span class='text-danger'></span>"
            }
        }
    });
});