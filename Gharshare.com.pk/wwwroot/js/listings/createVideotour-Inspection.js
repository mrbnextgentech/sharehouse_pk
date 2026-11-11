//This script is used to create 'Find a Place Listing'. Smart wizard instentiates here, On 'leaveStep' event 
//of jquery smart wizard, the current step is saved using an Ajax call. Form validations for each step  
//is also being created in below script.

$(document).ready(function () {    

    var smartWizard = $('#smartwizard').smartWizard({
        selected: 0,
        theme: 'arrows',
        isValidationEnabled: true, /// must be set to true for form validation        
        lang: { // Language variables for button
            next: 'Save & Move to Next',
            previous: 'Move to Previous Step'
        },
        autoAdjustHeight: false, //setting false resolves issue of contenting hiding
        toolbar: {
            position: 'both', // none|top|bottom|both
            showNextButton: true, // show/hide a Next button
            showPreviousButton: true, // show/hide a Previous button
            extraHtml: '' // Extra html to show on toolbar
        },
        validatorFunc: function () { 
            var isValid = $("#frmaccommodationtypestep").valid();
            var isValidStep2 = $("#frmareastep").valid();             
            return isValid && isValidStep2;
        }
    });
    // smartwizard leave event starts
    $('#smartwizard').on('leaveStep', function (e, anchorObject, stepIndex, currentStep, stepDirection,
        nextStep) {
        if (currentStep === 1) // validate and save step-1
        {
            var isValid = true;
            var selectedInspectionOption = $('#inspection input[name="Property.InspectionTime"]').is(':checked');
            if (!selectedInspectionOption) {
                Swal.fire({
                    icon: 'error',
                    icon: 'error',
                    title: 'Validation Error',
                    text: 'Please select one option regarding sharing your address',
                    showConfirmButton: false,
                    timer: 2000,
                });
                isValid = false;
                e.preventDefault();
            }
            if (!isValid) //// Validate step 1
            {
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);
            }
            else if (stepDirection === "forward") // Save step 1
            {
                //--Ajax call to save step in database starts---//
                var formId = 'frmInspectionsettings';                 
                var formData = new FormData($('#' + formId)[0]);
                
                $.ajax({
                    url: '/listing/saveinspectionsettings',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {                       
                        var successMessage = "Inspection settings saved successfully!"
                        toastr.success(successMessage, "Success");
                        //$('#txtInspectionPropertyId').val(data.id);
                    },
                    error: function (xhr, status, error) {
                        var errorMessage = "Error saving video tour!"
                        toastr.error(errorMessage, "Error");
                        // Handle error
                        //debugger;
                        //console.log(xhr);
                    }
                });                
                //---End of Ajax call to save step in database---//                    
            }
        } // step 1 ends
        else if (currentStep === 2) // validate and save step-2
        {
            var isValidSuburb = true;//validateSuburb(); /// use to validate

            if (!isValidSuburb) //// Validate step 2
            {
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);
            }
            else if (stepDirection === "forward") // Save step 2
            {
                //--Ajax call to save step in database starts---//
                var formId = 'frminspection';
                var formData = new FormData($('#' + formId)[0]);
                $.ajax({
                    url: '/listing/saveinspections',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {                         
                        var successMessage = "Inspections saved successfully!"
                        toastr.success(successMessage, "Success");
                    },
                    error: function (xhr, status, error) {
                        var errorMessage = "Error saving inspections !"
                        toastr.error(errorMessage, "Error");
                        // Handle error
                        //debugger;
                        //console.log(xhr);
                    }
                });

                //---End of Ajax call to save step in database---//                    
            }
        } // step 2 ends         
    });
   // End of // smartwizard leave event
 

/*******************Functions used for validation ***********************/

    // validation function for accommodation type
    function validationAccommodationtype() {
        var checkboxInputs = document.getElementsByClassName('type');
        var isChecked = false;

        for (var i = 0; i < checkboxInputs.length; i++) {
            if (checkboxInputs[i].checked) {
                isChecked = true;
                break;
            }
        }

        return isChecked;
    }

    //validation function for rent
    function validateRent() {
        var isValid = true;

            var input = $('#txtrent');
            var inputValue = input.val().trim();
            var span = input.next('span');

            // Clear existing error messages
            span.empty();

            if (inputValue === '') {
                isValid = false;
                input.next('span').empty();
                input.after('<span class="text-danger">required</span>');
                input.focus();                 
            } else if (!/^\d+$/.test(inputValue)) {
                isValid = false;
                span.text('invalid value entered');
                input.val(''); // Empty the input when invalid value is entered
            }

        return isValid;
    }
    //end of validation function for rent

    //validation function for age
    function validateAge() {
        var isValid = true;
        var firstEmptyInput = null;

        $('input[type="text"].ag').each(function () {
            var input = $(this);
            var inputValue = input.val().trim();
            var span = input.next('span');

            // Clear existing error messages
            span.empty();

            if (inputValue === '') {
                isValid = false;
                input.next('span').empty();
                input.after('<span class="text-danger">required</span>');
                //span.text('required');
                if (firstEmptyInput === null) {
                    firstEmptyInput = input;
                }
            } else if (!/^\d+$/.test(inputValue)) {
                isValid = false;
                span.text('invalid value entered');
                input.val(''); // Empty the input when invalid value is entered
            }
        });

        if (firstEmptyInput !== null) {
            firstEmptyInput.focus();
        }

        return isValid;
    }
    //end of validation function for age

    //validation function for suburb
    function validateSuburb() {
        var isValid = true;
        var input = $('#txtSuburb');
        var inputValue = input.val().trim();
        var span = input.next('span');

        // Clear existing error messages
        span.empty();

        if (inputValue === '') {
            input.next('span').empty();
            input.after('<span class="text-danger">required</span>');
            isValid = false;
            //span.text('required');
            input.focus();
        }
        return isValid;
    }
    //end of validation function for suburb

    //validation function for Move Date
    function validateMoveDate() {
            var isValid = true;
            var input = $('#txtMovedate');
            var inputValue = input.val().trim();
            //var span = input.next('span');
        var span = $('#divCalenderIcon').next('span'); 

            // Clear existing error messages
            span.empty();

            if (inputValue === '') {
                isValid = false;
                span.text('required');
                input.focus();
            }  
        return isValid;
    }
    //end of validation function for Move Date

    //function to show validation error if required input is empty
    function handleInputValueChange() {
       var input = $(this);
        var span = input.next('span');

        if (span.length === 0) {
            input.after('<span class="text-danger"></span>');
            span = input.next('span');
        }

        var inputValue = input.val().trim();

        if (inputValue !== '') {
            span.text('');
        } else {
            span.text('required');
        }

        if ((input.hasClass('rent') || input.hasClass('ag')) && !/^\d+$/.test(inputValue) && input.val().length>0) {
            span.text('invalid value entered');
            input.val('');
        }
        else if (input.val().length === 0)
            span.text('required');
    }

    //end of function to show validation error if required input is empty

    //validation function for Friend/Partner name & age
    function validateOtherPerson() {
        var isValid = true;
        var firstEmptyInput = null;

        $('input[type="text"].op, input[type="number"].op').each(function () {
            var input = $(this);
            if (input.val().trim() === '') {
                isValid = false;
                input.next('span').empty();
                input.after('<span class="text-danger">required</span>');
                if (firstEmptyInput === null) {
                    firstEmptyInput = input;
                }
            }
        });

        if (firstEmptyInput !== null) {
            firstEmptyInput.focus();
        }

        return isValid;
    }
    //end of validation function for Friend/Partner name & age

//*****************************End of functions used for validation **********************//

//function to map enum value to corresponding integer value
    function mapEnumToInteger(enumValue) {
        switch (enumValue) {
            case 'One':
                return 1;
            case 'Two':
                return 2;
            case 'Three':
                return 3;
            case 'Four':
                return 4;
            case 'Five':
                return 5;
            case 'Six':
                return 6;
            case 'Seven':
                return 7;
            case 'Eight':
                return 8;
            default:
                return 0; // Default value if the enum value is not recognized
        }
    }

//End function to map enum value

});


