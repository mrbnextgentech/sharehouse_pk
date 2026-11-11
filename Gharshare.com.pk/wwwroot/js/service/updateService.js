$(function () {    

    $('#frmservice').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
       // debugger;
        var formId = 'frmservice';
        var formData = new FormData($('#' + formId)[0]);
        if ($("#frmservice").valid()) {
            var closeButton = $('.btn-close');
            
            $.ajax({
                url: '/admin/service/update',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (data) {
                    //debugger;
                    createServicelist(data);                     
                    $('#noservice').html('');
                    var successMessage = "Service updated successfully!"
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

    //function getStatusText(planStatus) {
    //    if (planStatus === 1) {
    //        return "Active";
    //    } else if (planStatus === 2) {
    //        return "Pending";
    //    } else if (planStatus === 3) {
    //        return "Canceled";
    //    } else {
    //        return "Unknown"; // Default class or handle other cases as needed
    //    }
    //}

    //function getStatusClass(planStatus) {
    //    if (planStatus === 1) {
    //        return "text-success";
    //    } else if (planStatus === 2) {
    //        return "text-warning";
    //    } else if (planStatus === 3) {
    //        return "text-danger";
    //    } else {
    //        return ""; // Default class or handle other cases as needed
    //    }
    //}
});