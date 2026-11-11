$(function () {    

    $('#frmplanservice').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
       // debugger;
        var formId = 'frmplanservice';
        var formData = new FormData($('#' + formId)[0]);
        var isvalid = true;
        /*if ($("#frmplanservice").valid()) {*/
        if (isvalid) { 
           // var closeButton = $('.btn-close');
            
            $.ajax({
                url: '/admin/plan/saveplanservice',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (data) {                     
                    $('#noplan').html('');
                    var successMessage = "Service added to plan successfully!"
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: successMessage,
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );  
                    document.getElementById('frmplanservice').reset();
                   // closeButton.trigger('click');
                    
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
                    text: 'All fields required.Please fill in the form completly',
                    showConfirmButton: false,
                    timer: 2000,
                }
            );

        }
    });
});