$(document).ready(function () {

    //Validation
    //$('#txtName,#txtEmail').on('input', function () {
    //    $('#spanName').html($('#txtName').val() === "" ? "*" : "");         
    //});
    //End Validation

    $('#frmPlansubscription').submit(function (event) {
        event.preventDefault(); // Prevent default form submission 
        //debugger;
        var cardNo = "12324434";
        var formId = 'frmPlansubscription';
        var formData = new FormData($('#' + formId)[0]);
        var closeButton = $('.btn-close');
        //debugger;
        // Perform AJAX form submission
        if (cardNo != "") {
            $.ajax({
                url: "/profile/saveplansubscription",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Plan subscribed successfully.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                    closeButton.trigger('click');
                },
                error: function (xhr, status, error) {
                    swal.fire(
                        {
                            icon: 'error',
                            title: 'Error',
                            text: 'Error subscribing plan.',
                            showConfirmButton: false,
                            timer: 2000,
                        }
                    );
                }
            });
        }
        else {   /// validation else block
            if (name === "") {
                document.getElementById('spancard').innerHTML = "*";
                document.getElementById('txtcard').focus();
            }
        }
    });
});
