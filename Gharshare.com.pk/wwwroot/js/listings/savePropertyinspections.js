$(function () {
    $('#frminspection').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var formId = 'frminspection';
        var formData = new FormData($('#' + formId)[0]);
        if (validateInspection()) {
            $.ajax({
                url: '/listing/saveinspections',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (uid) {
                    var successMessage = "Inspections saved successfully!"
                    swal.fire(
                        {
                            icon: 'success',
                            title: 'Success',
                            text: successMessage,
                            showConfirmButton: false,
                            timer: 2000,
                        }                       
                    );
                    window.location.href = "/listing/listplacepreview?propertyId=" + uid;
                },
                error: function (xhr, status, error) {
                    var errorMessage = "Error saving inspections !"
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
    function validateInspection() {
        const inspectionDiv = document.getElementById("inspection");
        const datepickerInputs = document.querySelectorAll('#inspection input[type="text"].datepicker');
        const allSelectelements = document.querySelectorAll('#inspection select');         
        let isValid = true;

        for (const input of datepickerInputs) {
            if (input.value.trim() === "") {
                isValid = false;
                break;
            }
        }    

        for (const select of allSelectelements) {
            if (select.value.trim() === "") {
                isValid = false;
                break;
            }
        }    

        const ddllength = document.getElementById("ddllength");
        const ddlbreak = document.getElementById("ddlbreak");
        if (ddllength.value.trim() === "" || ddlbreak.value.trim() === "")
            isValid = false;

        return isValid;
    }
    
});