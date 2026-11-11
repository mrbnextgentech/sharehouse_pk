$(function () {
    $('#frmCity').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        //debugger;
        var formId = 'frmCity';
        var formData = new FormData($('#' + formId)[0]);
        if ($("#frmCity")[0].checkValidity()) {
            var closeButton = $('.btn-close');
            $.ajax({
                url: '/admin/site/addcity',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (result) {
                   // debugger; 
                    var successMessage = "";
                    if (result == "add")
                        successMessage = "City saved successfully!"
                    else
                        successMessage = "City updated successfully!"
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: successMessage,
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );

                    if (result == "add")
                        $("#frmCity")[0].reset();
                    else {
                        closeButton.trigger('click');
                        window.location.reload();
                    }

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
        }
        else {
            swal.fire(
                {
                    icon: 'error',
                    title: 'Validation error',
                    text: 'Name & short name are required.Please fill in the form completly',
                    showConfirmButton: false,
                    timer: 2000,
                }
            );

        }
    });

    $("#frmCity").validate({
        rules: {
            'City.Name': { required: true },
            'Plan.Shortname': { required: true },
        },
        messages: {
            'City.Name': {
                required: "&nbsp; required"
            },
            'City.Shortname': {
                required: "<span class='text-danger'></span>"
            },
        }
    });
});