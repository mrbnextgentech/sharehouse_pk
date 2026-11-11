$(document).ready(function () {
    $('#frmtenant').submit(function (event) {
        event.preventDefault();// Prevent default form submission

        var formId = 'frmtenant';
        var formData = new FormData($('#' + formId)[0]);
         
        // Perform AJAX form submission
       // debugger;
        if (validateInput()) {
            var closeButton = $('.btn-close');
            /// call to update property details
            $.ajax({
                url: "/findplace/updatetenant",
                type: "POST",
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {                     
                    closeButton.trigger('click');
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: 'Tenant information updated successfully.',
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
                            text: 'Error updating tenant information.',
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
                    text: 'Please complelty fill in the form.',
                    showConfirmButton: false,
                    timer: 2000,
                }
            );

        }
    });

    function validateInput() {
        const names = document.querySelectorAll('#divotherPerson input[type="text"].name');
        const ages = document.querySelectorAll('#divotherPerson input[type = "text"].age');
        const allSelectelements = document.querySelectorAll('#divotherPerson select');
        let isValid = true;

        for (const input of names) {
            if (input.value.trim() === "") {
                isValid = false;
                input.focus();
                break;
            }
        }
        for (const inputage of ages) {
            if (inputage.value.trim() === "") {
                isValid = false;
                inputage.focus();
                var inputValue = inputage.value.trim();
                if (!/^\d+$/.test(inputValue) && inputValue.value.trim().length > 0) {
                    isValid = false;
                    inputage.val(''); // Empty the input when invalid value is entered
                    inputage.focus();
                }
                break;
            }
        }

        for (const select of allSelectelements) {
            if (select.value.trim() === "") {
                isValid = false;
                select.focus();
                break;
            }
        }
       // debugger;
        if ($('#txtmypartnername').length > 0) {
            let partnerName = $('#txtmypartnername').val();
            let partnerAge = $('#txtmypartnerage').val();
            let partnerGender = $('#ddlmypartnerGender').val();

            if (partnerName.trim() === "" || partnerAge.trim() === "" || partnerGender.trim()==="")
            {
                isValid = false;

                if (partnerName.trim()==="")
                    $('#txtmypartnername').focus(); 
                else if (partnerAge.trim() === "")
                    $('#txtmypartnerage').focus(); 
                else if (partnerGender.trim() === "")
                    $('#ddlmypartnerGender').focus();
            }
                
        }

        return isValid;
    }
});