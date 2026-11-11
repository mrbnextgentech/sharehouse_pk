$(document).ready(function () {

    $('#frmrent').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var formId = 'frmrent';        
        var formData = new FormData($('#' + formId)[0]);
        var rent = $('#txtrent').val().trim();
        // Perform AJAX form submission
        if (rent != "") {
            var closeButton = $('.btn-close');
            /// call to update property details
            $.ajax({
                url: "/findplace/updaterent",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    // Update the rent element with the data from the data object
                    $('#rent').text('Rs ' + data.monthlyBudget + '/month');

                    var stayLength = data.length;
                    var stayMapping = {
                        1: "1 month",
                        2: "2 months",
                        3: "3 months",
                        4: "6 months",
                        5: "9 months",
                        6: "12+ Months",
                        7: "unknown",                         
                    };

                    ////update rent & bill status under address section.
                    var stayDisplayName = stayMapping[stayLength];
                    $('#stay').text(stayDisplayName);
                    $('#moveDate').html(formatDate(data.moveDate));

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

    function formatDate(date) {
        var availableFromDate = new Date(date);
        var day = availableFromDate.getDate();
        var month = availableFromDate.getMonth() + 1; // Months are zero-based, so we add 1
        var year = availableFromDate.getFullYear();

        // Format day, month, and year as two digits if needed (e.g., '05' instead of '5')
        var formattedDay = day.toLocaleString('en-US', { minimumIntegerDigits: 2 });
        var formattedMonth = month.toLocaleString('en-US', { minimumIntegerDigits: 2 });

        // Concatenate the formatted date parts
        var formattedDate = formattedDay + '/' + formattedMonth + '/' + year;

        return formattedDate;
    }
});