$(document).ready(function () {

    $('#frmPropertyrent').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var formId = 'frmPropertyrent';        
        var formData = new FormData($('#' + formId)[0]);
        var rent = $('#txtrent').val().trim();
        // Perform AJAX form submission
        if (rent != "") {
            var closeButton = $('.btn-close');
            /// call to update property details
            $.ajax({
                url: "/listing/updatepropertyrent",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    // Update the rent element with the data from the data object
                    $('#rent').text('Rs ' + data.rent + '/month rent');

                    // Calculate the bond amount using the data from the data object
                    var bondAmount = data.rent * parseInt(data.bond);

                    // Update the bond element with the calculated bond amount
                    $('#bond').text('Rs ' + bondAmount + ' bond');

                    var billStatus = data.bill;
                    var billStatusMapping = {
                        1: "Bills not included in rent",
                        2: "Some bills included in rent",
                        3: "Bills included in the rent"
                    };
                    //update rent & bill status under address section.
                        var billStatusDisplayName = billStatusMapping[billStatus];
                    $('#billstatus').text(billStatusDisplayName); 
                    var rentandBill ='Rs.'+ data.rent + '/month <br>' + billStatusDisplayName;
                    $('#rentUnderAddress').html(rentandBill);

                    closeButton.trigger('click');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Property rent updated successfully.',
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
            $('#spanrent').html('&nbsp;&nbsp;required');
        }

                /// call to deactivate account starts
      
    });

    $('#txtrent').on('input', function () {
        var rentValue = $('#txtrent').val().trim();
        var rentInput = $('#txtrent');
        var span = $('#spanrent');
        if (rentValue != "") {
            span.html('');
        }
        else {
            span.html('&nbsp;&nbsp;required');
        }

        if (!/^\d+$/.test(rentValue) && rentInput.val().length > 0) {
            span.text('');
            //span.text(' invalid value entered');
            span.html('&nbsp;invalid value entered');
            rentInput.val('');
        }
        else if (rentValue.val().length === 0)
            span.html('&nbsp;required');
    });
});