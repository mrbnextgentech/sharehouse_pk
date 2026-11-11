$(document).ready(function () {

    $('#frmRoomavailability').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var formId = 'frmRoomavailability';
        var formData = new FormData($('#' + formId)[0]);
        // Perform AJAX form submission         
        var closeButton = $('.btn-close');
        /// call to update property details
        $.ajax({
            url: "/listing/updateroomavailability",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                //Map & update minStay date
                var minStay = data.minStay;
                var minStayMapping = {
                    1: "1 Month",
                    2: "2 Months",
                    3: "4 Months",
                    4: "6 Month",
                    5: "12 Month",
                    6: "no limit"
                };
                var minStayDisplayName = minStayMapping[minStay];
                //$('#roomminstay').text(minStayDisplayName);

                //Map & update maxStay date
                var maxStay = data.maxStay;
                var maxStayMapping = {
                    1: "1 month",
                    2: "6 months",
                    3: "1 year",
                    4: "2 years",
                    5: "3 years",
                    6: "5 years",
                    7: "no limit",
                    8: "None"
                };
                var maxStayDisplayName = maxStayMapping[maxStay];
                var roomStay = minStayDisplayName + "-" + maxStayDisplayName;
                $('#roomstay').text(roomStay);
                //update Available from date
                $('#roomavailable').text("Available "+formatDate(data.availableFrom));

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

        /// call to deactivate account starts

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