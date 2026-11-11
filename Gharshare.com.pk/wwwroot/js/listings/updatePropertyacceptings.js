$(document).ready(function () {

    $('#frmPropertyacceptings').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var formId = 'frmPropertyacceptings';        
        var formData = new FormData($('#' + formId)[0]);         
        // Perform AJAX form submission         
        var closeButton = $('.btn-close');
            /// call to update property details
        if (validate()) {
            $.ajax({
                url: "/listing/updatepropertyacceptings",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    //$('#description').html(data.description);
                    $('#divAcceptings').empty();
                    cretePropertyAcceptings(data);
                    closeButton.trigger('click');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Property acceptings updated successfully.',
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
                            text: 'Error updating acceptings.',
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
                    title: 'Validation Error',
                    text: 'Must select at least one value from accepting list.',
                    showConfirmButton: false,
                    timer: 2000,
                }
            );

        }
                /// ajax call ends
    });
    function cretePropertyAcceptings(data)
    {
        var divAcceptings = $('#divacceptings');

        // Clear any existing content in the div
        divAcceptings.empty();

        // Loop through each element of the data object
        for (var i = 0; i < data.length; i++) {
            var accepting = data[i];

            // Create the dynamic HTML markup
            var span = $('<span>', {
                class: 'mt-2 m-1 btn btn-outline-primary',
                html: '<img src="/img/icons/detail/tick.svg" style="width:20px;height:14px;" />' + accepting.accepting.title
            });
            // Append the dynamic HTML to the div
            divAcceptings.append(span);
        }
    }
    function validate() {
        var checkboxes = $('input[type="checkbox"]');
        var isValid = false;

        checkboxes.each(function () {
            if (this.checked) {
                isValid = true;
                return false; // break the loop
            }
        });

        return isValid;
    }

});