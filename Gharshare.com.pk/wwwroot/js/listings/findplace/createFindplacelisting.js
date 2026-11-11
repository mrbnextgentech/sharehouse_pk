//This script is used to create 'Find a Place Listing'. Smart wizard instentiates here, On 'leaveStep' event 
//of jquery smart wizard, the current step is saved using an Ajax call. Form validations for each step  
//is also being created in below script.

$(document).ready(function () {
    // function to created dynamic UI for Friends/Partner of person creating listing to find a place.
    $('.tp[type="radio"]').click(function () {
        //var value =  $(this).val();
        var value = mapEnumToInteger($(this).val());
        if ($(this).is(':checked')) {
            createDynamicHTML4Persons(value);
            $('#divotherPerson input[type="text"].ag').on('input', handleInputValueChange);
            $('#divotherPerson input[type="text"].op').on('input', handleInputValueChange);
        } else {
            $('#divotherPerson').empty();
        }
    });
    // End of function to created dynamic UI for Friends/Partner of person creating listing to find a place.
    

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
            var isValidStep3 = $("#frmrentstep").valid();            
            var isValidStep5 = $("#frmintrostep").valid();             
            return isValid && isValidStep2 && isValidStep3 &&  isValidStep5;
        }
    });
    //Bind rent, my name & age field's oninput event to 'handleInputValueChange' function (intro step)
    $('#txtmyage').on('input', handleInputValueChange);
    $('#txtmyname').on('input', handleInputValueChange);
    $('#myintro').on('input', handleInputValueChange);
    $('#txtrent').on('input', handleInputValueChange);
    $('#txtSuburb').on('input', handleInputValueChange);
    
    //End of bind rent, my name & age field's oninput event to 'handleInputValueChange' function

    // smartwizard leave event starts
    $('#smartwizard').on('leaveStep', function (e, anchorObject, stepIndex, currentStep, stepDirection,
        nextStep) {
        if (currentStep === 1) // validate and save step-1
        {
            var isValid = validationAccommodationtype();

            if (!isValid) //// Validate step 1
            {
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);

                ///Step-1 field Validation
                // validation for Accommodationtype field                   
                var selectedAccommodationTypeOpt = $('#accommodationtype input[name="PersonListing.Propertytype"]').is(':checked');
                if (!selectedAccommodationTypeOpt) {
                    Swal.fire({
                        icon: 'error',
                        icon: 'error',
                        title: 'Validation Error',
                        text: 'Please select type of accommodation',
                        showConfirmButton: false,
                        timer: 2000,
                    });
                    e.preventDefault();
                }
                // validation for Propertytype field
                var selectedPropertyTypeOpt = $('#accommodationtype input[name="PersonListing.Propertytype"]').is(':checked');
                if (!selectedPropertyTypeOpt) {
                    Swal.fire({
                        icon: 'error',
                        icon: 'error',
                        title: 'Validation Error',
                        text: 'Please select one property type',
                        showConfirmButton: false,
                        timer: 2000,
                    });
                    e.preventDefault();
                }  // End of Step-1 field Validation                    
            }
            else if (stepDirection === "forward") // Save step 1
            {
                //--Ajax call to save step in database starts---//
                var formId = 'frmaccommodationtypestep';                 
                var formData = new FormData($('#' + formId)[0]);
                $.ajax({
                    url: '/findplace/saveaccommodationtypestep',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                       // console.log(data);
                        $('#txtacctypePersonId').val(data.id);
                        $('#txtareaPersonId').val(data.id);                        
                        $('#txtrentPersonId').val(data.id);
                        $('#txtpreferencePersonId').val(data.id);
                        $('#txtintroPersonId').val(data.id);
                        $('#txtempstatusPersonId').val(data.id);
                        $('#txtLifestylePersonId').val(data.id);
                        $('#txtpreviewPersonId').val(data.uId);
                        
                        var successMessage = "Step-" + currentStep + " saved successfully!"
                        toastr.success(successMessage, "Success");
                    },
                    error: function (xhr, status, error) {
                        var errorMessage = "Error saving step-" + currentStep + " !"
                        toastr.error(errorMessage, "Error");
                        // Handle error
                        debugger;
                        console.log(xhr);
                    }
                });

                //---End of Ajax call to save step in database---//                    
            }
        } // step 1 ends
        else if (currentStep === 2) // validate and save step-2
        {
            var isValidSuburb = validateSuburb(); /// use to validate

            if (!isValidSuburb) //// Validate step 2
            {
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);
            }
            else if (stepDirection === "forward") // Save step 2
            {
                //--Ajax call to save step in database starts---//
                var formId = 'frmareastep';
                var formData = new FormData($('#' + formId)[0]);
                $.ajax({
                    url: '/findplace/saveareastep',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (listingEmps) {
                        //console.log(listingEmps);                       
                        var successMessage = "Step-" + currentStep + " saved successfully!"
                        toastr.success(successMessage, "Success");
                        createEmploymentStatusUI(listingEmps);                        
                    },
                    error: function (xhr, status, error) {
                        var errorMessage = "Error saving step-" + currentStep + " !"
                        toastr.error(errorMessage, "Error");
                        // Handle error
                        debugger;
                        console.log(xhr);
                    }
                });

                //---End of Ajax call to save step in database---//                    
            }
        } // step 2 ends
        else if (currentStep === 3) // validate and save step-3
        {
            var isValidrent = validateRent();  //use to validate rent integer values
            var isValidMoveDate = validateMoveDate();

            if (!(isValidrent && isValidMoveDate)) //// Validate step 3
            {
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);
            }
            else if (stepDirection === "forward") // Save step 3
            {
                //--Ajax call to save step in database starts---//
                var formId = 'frmrentstep';
                var formData = new FormData($('#' + formId)[0]);
                $.ajax({
                    url: '/findplace/saverentstep',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (listinglifestyles) {
                        // console.log(data);                         
                        var successMessage = "Step-" + currentStep + " saved successfully!"
                        toastr.success(successMessage, "Success");
                        createLifestylesUI(listinglifestyles);
                    },
                    error: function (xhr, status, error) {
                        var errorMessage = "Error saving step-" + currentStep + " !"
                        toastr.error(errorMessage, "Error");
                        // Handle error
                        debugger;
                        console.log(xhr);
                    }
                });

                //---End of Ajax call to save step in database---//                    
            }
        } // step 3 ends
        else if (currentStep === 4) // validate and save step-2
        {
            var isValid4 = $('#frmpreferencestep').valid();

            if (!isValid4) //// Validate step 4
            {
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);
            }
            else if (stepDirection === "forward") // Save step 4
            {
                //--Ajax call to save step in database starts---//
                var formId = 'frmpreferencestep';
                var formData = new FormData($('#' + formId)[0]);
                $.ajax({
                    url: '/findplace/savepreferencestep',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        //debugger;
                        //var userId = data.personListing.user.id;
                        //$('#txtListingUserId').val(userId);

                        $('#dvCitylist').empty();
                        var cityId = data.personListing.user.cityID
                        var cityList = '<select onchange="validateCity()" id="ddlCityId" class="nice-select wide w-100 mb-3" required id="personListing.User.cityid" name="personListing.User.CityID"><option value="">--select city--</option>';
                        for (var i = 0; i < data.cityList.length; i++) {
                            var optionValue = parseInt(data.cityList[i].value,10);
                            var optionText = data.cityList[i].text;
                            var selected = (optionValue === cityId) ? 'selected' : '';
                            cityList += `<option value="${optionValue}" ${selected}>${optionText}</option>`;
                        }
                        cityList += '</select>';
                        $('#dvCitylist').html(cityList);
                        $('#ddlCityId').on('change', validateCity);
                        var successMessage = "Step-" + currentStep + " saved successfully!"
                        toastr.success(successMessage, "Success");
                    },
                    error: function (xhr, status, error) {
                        var errorMessage = "Error saving step-" + currentStep + " !"
                        toastr.error(errorMessage, "Error");
                        // Handle error
                        debugger;
                        console.log(xhr);
                    }
                });

                //---End of Ajax call to save step in database---//                    
            }
        } // step 4 ends
        else if (currentStep === 5) // validate and save step-3
        {
            //var isValid5 = $('#frmintrostep').valid();
            var isValidPerson = validateOtherPerson();
            var isValidAge = validateAge();
            var isValidCity = validateCity();

            if (!(isValidPerson && isValidAge && isValidCity )) //// Validate step 5
            {
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);
            }
            else if (stepDirection === "forward") // Save step 5
            {
                //--Ajax call to save step in database starts---//
                var formId = 'frmintrostep';
                var formData = new FormData($('#' + formId)[0]);
                $.ajax({
                    url: '/findplace/saveintrostep',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        // console.log(data);                         
                        var successMessage = "Step-" + currentStep + " saved successfully!"
                        toastr.success(successMessage, "Success");
                    },
                    error: function (xhr, status, error) {
                        var errorMessage = "Error saving step-" + currentStep + " !"
                        toastr.error(errorMessage, "Error");
                        // Handle error
                        debugger;
                        console.log(xhr);
                    }
                });

                //---End of Ajax call to save step in database---//                    
            }
        } // step 5 ends
        else if (currentStep === 6) // validate and save step-3
        {
            var isValid6 = $('#frmemploymentstatusstep').valid();

            if (!isValid6) //// Validate step 6
            {
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);
            }
            else if (stepDirection === "forward") // Save step 6
            {
                //--Ajax call to save step in database starts---//
                var formId = 'frmemploymentstatusstep';
                var formData = new FormData($('#' + formId)[0]);
                $.ajax({
                    url: '/findplace/saveemploymenttatusstep',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        // console.log(data);                         
                        var successMessage = "Step-" + currentStep + " saved successfully!"
                        toastr.success(successMessage, "Success");
                    },
                    error: function (xhr, status, error) {
                        var errorMessage = "Error saving step-" + currentStep + " !"
                        toastr.error(errorMessage, "Error");
                        // Handle error
                        debugger;
                        console.log(xhr);
                    }
                });

                //---End of Ajax call to save step in database---//                    
            }
        } // step 6 ends
        else if (currentStep === 7) // validate and save step-3
        {
            var isValid7 = $('#frmLifestylestep').valid();

            if (!isValid7) //// Validate step 6
            {
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);
            }
            else if (stepDirection === "forward") // Save step 7
            {
                //--Ajax call to save step in database starts---//
                var formId = 'frmLifestylestep';
                var formData = new FormData($('#' + formId)[0]);
                $.ajax({
                    url: '/findplace/saveLifestylestep',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        // console.log(data);                         
                        var successMessage = "Step-" + currentStep + " saved successfully!"
                        toastr.success(successMessage, "Success");
                    },
                    error: function (xhr, status, error) {
                        var errorMessage = "Error saving step-" + currentStep + " !"
                        toastr.error(errorMessage, "Error");
                        // Handle error
                        debugger;
                        console.log(xhr);
                    }
                });

                //---End of Ajax call to save step in database---//                    
            }
        } // step 7 ends

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
    //validation function for city
    function validateCity() {
        var isValid = true;

        var input = $('#ddlCityId');
        var inputValue = input.val().trim();
        var span = input.next('span');

        // Clear existing error messages
        span.empty();

        if (inputValue === '') {
            isValid = false;
            input.next('span').empty();
            input.after('<span class="text-danger">required</span>');
            input.focus();
        }
        return isValid;
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


//*****************************Functions to create dynamic Html **********************///
    function createEmploymentStatusUI(listingEmps) {
        $('#divEmploymentStatus').empty();
        // Iterate through the PersonListing.Employments object
        $.each(listingEmps, function (i, employment) {
            var checkboxId = 'Employments_' + i;
            var listingEmploymentId = 'PersonListing.Employments[' + i + '].Id';
            var uId = 'PersonListing.Employments[' + i + '].UId';
            var personListingId = 'PersonListing.Employments[' + i + '].PersonListingID';
            var employmentId = 'PersonListing.Employments[' + i + '].EmploymentID';
            var createdOn = 'PersonListing.Employments[' + i + '].createdOn';
            var createdbyUserID = 'PersonListing.Employments[' + i + '].CreatedbyUserID';
            var iconPath = 'PersonListing.Employments[' + i + '].iconPath';

            var employmentStatus = '<div class="col-lg-3 col-sm-6 col-6">' +
                '<div class="text-center mt-3">' +
                '<label for="' + checkboxId + '">' +
                '<input class="radio-input" type="checkbox" id="' + checkboxId + '"  name="PersonListing.Employments[' + i + '].isSelected" value="true">' +
                '<input  type="hidden" id="' + listingEmploymentId + '" name="' + listingEmploymentId + '" value="' + employment.id + '" > ' +                       
                '<input  type="hidden" id="' + uId + '" name="' + uId + '" value="' + employment.uId + '" > ' +                    
                '<input type="hidden" id="' + personListingId + '" name="PersonListing.Employments[' + i + '].personListingID" value="' + employment.personListingID + '">' +
                '<input  type="hidden" id="' + employmentId + '" name="PersonListing.Employments[' + i + '].EmploymentID" value="' + employment.employmentID + '" > ' +
            '<input  type="hidden" id="' + createdOn + '" name="' + createdOn + '" value="' + employment.createdOn + '">' +
                '<input  type="hidden" id="' + createdbyUserID + '" name="' + createdbyUserID + '" value="' + employment.createdbyUserID + '">' +
                '<input  type="hidden" id=' + iconPath + ' name=' + iconPath + ' value="' + employment.employment.iconPath + '">' +
                '<span class="radio-tile">' +
                '<span class="radio-icon">' +
                '<img src="' + employment.employment.iconPath + '" />' +
                //'<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512">' +
                //'<path d="' + employment.employment.iconPath + '"></path>' +
                //'</svg>' +
                '</span>' +
                '</span>' +
                '</label>' +
            '<p>' + employment.employment.title + '</p>' +                 
                '</div>' +
                '</div>';
            $('#divEmploymentStatus').append(employmentStatus);
        });
    }
    function createLifestylesUI(listinglifestyles) {
        $('#divLifestyles').empty();
        // Iterate through the PersonListing.Employments object
        $.each(listinglifestyles, function (i, lifestyle) {
            var checkboxId = 'PersonListing.Lifestyles_' + i;
            var listingLifestylesId = 'PersonListing.Lifestyles[' + i + '].Id';
            var lLifestyleUId = 'PersonListing.Lifestyles[' + i + '].UId';
            var personListingId = 'PersonListing.Lifestyles[' + i + '].PersonListingID';
            var lifestyleId = 'PersonListing.Lifestyles[' + i + '].LifestyleID';
            var createdOn = 'PersonListing.Lifestyles[' + i + '].createdOn';
            var createdbyUserID = 'PersonListing.Lifestyles[' + i + '].CreatedbyUserID';
            var iconPath = 'PersonListing.Lifestyles[' + i + '].iconPath';

            var lifeStyles = '<div class="col-lg-3 col-sm-6 col-6">' +
                '<div class="text-center mt-3">' +
                '<label for="' + checkboxId + '">' +
                '<input class="radio-input" type="checkbox" id="' + checkboxId + '"  name="PersonListing.Lifestyles[' + i + '].isSelected" value="true">' +
                '<input  type="hidden" id="' + listingLifestylesId + '" name="' + listingLifestylesId + '" value="' + lifestyle.id + '" > ' +
                '<input  type="hidden" id="' + lLifestyleUId + '" name="' + lLifestyleUId + '" value="' + lifestyle.uId + '" > ' +
                '<input type="hidden" id="' + personListingId + '" name="PersonListing.Lifestyles[' + i + '].personListingID" value="' + lifestyle.personListingID + '">' +
                '<input  type="hidden" id="' + lifestyleId + '" name="PersonListing.Lifestyles[' + i + '].LifestyleID" value="' + lifestyle.lifestyleID + '" > ' +
                '<input  type="hidden" id="' + createdOn + '" name="' + createdOn + '" value="' + lifestyle.createdOn + '">' +
                '<input  type="hidden" id="' + createdbyUserID + '" name="' + createdbyUserID + '" value="' + lifestyle.createdbyUserID + '">' +
                '<input  type="hidden" id=' + iconPath + ' name=' + iconPath + ' value="' + lifestyle.lifestyle.iconPath + '">' +
                '<span class="radio-tile">' +
                '<span class="radio-icon">' +
                '<img src="' + lifestyle.lifestyle.iconPath + '" />' +
                //'<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512">' +
                //'<path d="' + lifestyle.lifestyle.iconPath + '"></path>' +
                //'</svg>' +
                '</span>' +
                '</span>' +
                '</label>' +
                '<p>' + lifestyle.lifestyle.title + '</p>' +
                '</div>' +
                '</div>';
            $('#divLifestyles').append(lifeStyles);          
            
        });
    }
    // function to created dynamic UI for Friends/Partner of person creating listing to find a place.
    function createDynamicHTML4Persons(value) {
        $('#divotherPerson').empty();
        var personListingId = $('#txtintroPersonId').val().trim();        
        for (var i = 0; i < value; i++) {
            var dynamicHTML = '<div class="mb-3">' +
                '<label class="text-label form-label">' +
                'Your friend\'s first name' +
                '</label>' +
                '<input type="text" class="op form-control" name="PersonListing.Mates[' + i + '].Name" placeholder="Enter friend First Name">' +
                '</div>' +
                '<div class="mb-3">' +
                '<label class="text-label form-label">' +
                'Your friend\'s age' +
                '</label>' +
                '<input type="text" name="PersonListing.Mates[' + i + '].Age" class="ag form-control" placeholder="Enter Your friend age">' +
                '</div>' +
                '<label class="text-label form-label">' +
                'The gender your friend identifies with' +
                '</label>' +
                '<div class="mb-3">' +
                '<div class="btn-group w-100" role="group" aria-label="Basic radio toggle button group">' +
                '<input name="PersonListing.Mates[' + i + '].gender" value="Woman" type="radio" class="gender btn-check" id="Person_' + i + '_Woman">' +
                '<label class="btn btn-outline-primary" for="Person_' + i + '_Woman">Woman</label>' +
                '<input name="PersonListing.Mates[' + i + '].gender" value="Man" type="radio" class="gender btn-check" id="Person_' + i + '_Man">' +
                '<label class="btn btn-outline-primary" for="Person_' + i + '_Man">Man</label>' +
                '<input name="PersonListing.Mates[' + i + '].gender" value="Transgender" type="radio" class="gender btn-check" id="Person_' + i + '_Non_Binary">' +
                '<label class="btn btn-outline-primary" for="Person_' + i + '_Non_Binary">Transgender</label>' +
                '<input name="PersonListing.Mates[' + i + '].gender" value="Undisclosed" checked type="radio" class="gender btn-check" id="Person_' + i + '_Gender_Diverse">' +
                '<label class="btn btn-outline-primary" for="Person_' + i + '_Gender_Diverse">Undisclosed</label>' +
                '</div>' +
                '<input type="checkbox" name="PersonListing.Mates[' + i + '].Partner" style="display:none;" value="false">' +
                '<input type="hidden" id="PersonListing.Mates[' + i + '].personListingID" name="PersonListing.Mates[' + i + '].personListingID" value="' + personListingId + '">' +
                '</div>';

            $('#divotherPerson').append(dynamicHTML);
        }
    }
    // End of function to created dynamic UI for Friends/Partner of person creating listing to find a place.

//*****************************End of functions to create dynamic Html **********************///

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


