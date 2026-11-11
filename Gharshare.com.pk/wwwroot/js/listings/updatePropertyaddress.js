$(document).ready(function () {

    $('#frmPropertyaddress').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var formId = 'frmPropertyaddress';        
        var formData = new FormData($('#' + formId)[0]);
        var address = $('#txtaddress').val().trim();
        // Perform AJAX form submission
        if (address != "") {        
            var closeButton = $('.btn-close');
            /// call to update property details
            $.ajax({
                url: "/listing/updatepropertyaddress",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    $('#address').html(data.address);
                    // $('#AccountModal').css('display', 'none');
                    closeButton.trigger('click');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Property details updated successfully.',
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
                            text: 'Error updating profile.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                }
            });
        }
        else {
            $('#spanAddress').html('&nbsp;&nbsp;required');
        }

                /// call to deactivate account starts
      
    });

    $('#txtaddress').on('input', function () {
        var address = $('#txtaddress').val().trim();

        if (address != "") {
            $('#spanAddress').html('');
        }
        else {
            $('#spanAddress').html('&nbsp;&nbsp;required');
        }
    });

    $('#AccountModal').on('hidden.bs.modal', function () {
        $(this).removeData('bs.modal');
    });
});