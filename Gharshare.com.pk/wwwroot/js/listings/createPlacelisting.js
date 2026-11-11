//This script is used for 'Create Place Listing'. Smart wizard instentiates here, On 'leaveStep' event of
// jquery smart wizard, current step is saved using an Ajax call. Form validations for each step and 
//dynamic html for step 3, 4 , 5 & 7 (acceptings) is also being created in below script.

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
            var isValid = $("#accommodationtypestep").valid();
            var isValidStep2 = $("#frmaboutstep").valid();
            var isValidStep3 = $("#frmroomfeaturesstep").valid();
            var isValidStep4 = $("#frmwholepropertyrentstep").valid();
            var isValidStep5 = $("#frmavailabilitystep").valid();
            var isValidStep8 = $("#frmintrostep").valid();
            return isValid && isValidStep2 && isValidStep4 && isValidStep5;
        }
    });
    $('#txtWholePropertyRent').on('input', handleInputValueChange);
    // This function will trigger validation on the 'showStep' event
    $('#smartwizard').on('leaveStep', function (e, anchorObject, stepIndex, currentStep, stepDirection,
        nextStep) {

        var accommodationType = $('#txtaccommodationtype').val();

        if (currentStep === 1) // validate and save step-1
        {
            var isValid = validationAccommodationtype();/// $('#accommodationtypestep').valid();

            if (!isValid) //// Validate step 1
            {
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);

                ///Step-1 field Validation
                // validation for Accommodationtype field                   
                var selectedAccommodationTypeOpt = $('#accommodationtype input[name="Property.Accommodationtype"]').is(':checked');
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
                var selectedPropertyTypeOpt = $('#accommodationtype input[name="Property.Propertytype"]').is(':checked');
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
                var formId = 'accommodationtypestep';
                // $('#' + formId).submit(); // Programmatically trigger form submission
                var formData = new FormData($('#' + formId)[0]);
                $.ajax({
                    url: '/listing/saveaccommodationtypestep',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        //debugger;
                        //console.log(data);
                        /*$('#txtaboutPropertyId').val(data.id);*/
                        $('#txtaboutPropertyId').val(data.property.id);
                        $('#txtacctypePropertyId').val(data.property.id);
                        $('#txtavailabilityPropertyId').val(data.property.id);
                        $('#txtpreferencePropertyId').val(data.property.id);
                        $('#txtwholePropertyId').val(data.property.id);
                        $('#txtroomfeaturesPropertyId').val(data.property.id);
                        $('#txtroomrentPropertyId').val(data.property.id);
                        $('#txtroomavailPropertyId').val(data.property.id);
                        $('#txtimagePropertyId').val(data.property.id);
                        $('#txtintroPropertyId').val(data.property.id);
                        $('#txtPreviewPropertyId').val(data.property.uId);

                        $('#dvCitylist').empty();
                        var cityList = '<select class="nice-select wide w-100 mb-3" required id="Property_cityid" name="Property.CityID"><option value="">--select city--</option>';
                        for (var i = 0; i < data.cityList.length; i++) {
                            cityList += `<option value="${data.cityList[i].value}">${data.cityList[i].text}</option>`;
                        }
                        cityList += '</select>';
                        $('#dvCitylist').html(cityList);                         
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

        else if (currentStep === 2) {

            // validate step2 before skipping step 3 to move to step 4 in case of whole property

            var isValidStep2 = $('#frmaboutstep').valid();
            if (!isValidStep2) {
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);
            }
            // End of validating step2 before skipping step 3 to move to step 4 in case of whole property
            else if (stepDirection === "forward") {

                //--Ajax call to save step in database starts---//
                var formId = 'frmaboutstep';                
                var formData = new FormData($('#' + formId)[0]);
                $.ajax({
                    url: '/listing/saveAboutstep',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                       // debugger;
                        console.log('inside success ....');
                        var successMessage = "Step-" + currentStep + " saved successfully!"
                        toastr.success(successMessage, "Success");
                       // console.log(data);
                        //$('#txtwholePropertyId').val(data.id)
                        if ($('#txtaccommodationtype').val() === "iswholepropertyfalse") {
                            createRoomUI(data);
                            //Bind 'handleInputValueChange' on 'input' event of every Room name input for validation
                            $('#divroomfeatures').find('input[type="text"].roomname').on('input', handleInputValueChange);
                            //Bind 'handleInputValueChange' on 'input' event of every Room rent input for validation
                            $('#divroomrent').find('input[type="text"].roomrent').on('input', handleInputValueChange);
                            //Must initialize dtp after dynamic html added to DOM
                            //otherwise, dtp controls will not work
                            $('.datepicker').each(function () {
                                $(this).datetimepicker({
                                    // datepicker options
                                    format: 'd/m/Y',
                                    minDate: new Date(),
                                    timepicker: false,
                                });
                            });
                            //end initialize dtp
                        }
                        else {

                           // alert('property whole');
                        }
                    },
                    error: function (xhr, status, error) {
                        // Handle error
                        debugger;
                        var errorMessage = "Error saving step-" + currentStep +" ,Error:"+ error + " !"
                        toastr.error(errorMessage, "Error");
                        //debugger;
                       console.log(xhr);
                    }
                });

                //---End of Ajax call to save step in database---//

                /// Skip step 3 if step 2 is valid and accommodation type is whole property
                if (accommodationType === 'iswholepropertytrue') {
                    if (stepDirection === 'forward') {
                        setTimeout(function () {
                            $('#smartwizard').smartWizard("next"); // Simulate clicking the "Next" button
                        }, 1);
                        // $('#smartwizard').find('.nav-item').eq(stepIndex + 2).css('display', 'none');
                    } else if (stepDirection === 'backward') {
                        setTimeout(function () {
                            $('#smartwizard').smartWizard("prev"); // Simulate clicking the "Previous" button
                        }, 1);
                    }
                }
                //End Skip step 3 if accommodation type is whole property
            }

        }
        // validate and save step-3 && accommodationType === 'iswholepropertyflase'
        else if (currentStep === 3 ) {
            var isValidStep3 = validateRoomName();  //$('#frmroomfeaturesstep').valid(); /// validation for this step left
            if (!isValidStep3)  
            {
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);
            }   //End of Step-3 form Validation

            else if (stepDirection === "forward" && accommodationType != 'iswholepropertytrue')//save step 3 
            {
                //--Ajax call to save step in database starts---//
                var formId = 'frmroomfeaturesstep';
                var formData = new FormData($('#' + formId)[0]);
                //for (var pair of formData.entries()) {  /// to check form data for debugging purpose
                //    console.log(pair[0] + ': ' + pair[1]);
                //}
                $.ajax({
                    url: '/listing/saveroomfeaturesstep',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        var successMessage = "Step-" + currentStep + " saved successfully!"
                        toastr.success(successMessage, "Success");
                        //console.log(data);
                    },
                    error: function (xhr, status, error) {
                        // Handle error
                        var errorMessage = "Error saving step-" + currentStep + " !"
                        toastr.error(errorMessage, "Error");
                        //debugger;
                        //console.log(xhr);
                    }
                });

                //---End of Ajax call to save step in database---//                    

            }

        }///End of validate and save step-3

        // validate and save step-4
        else if (currentStep === 4)  // validate step 4
        {
            var isValidStep4;
            if ($('#txtaccommodationtype').val() === "iswholepropertyfalse") { //Rooms scenario

                isValidStep4 = validateRoomrent();
            }
            else { // whole property scenario
                isValidStep4 = validatePropertyRent();
            }
            if (!isValidStep4) {
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);
            }  ///End of Step- 4 form Validation
            else if (stepDirection === "forward") {  //save step 4

                //--Ajax call to save step in database starts---//

                if ($('#txtaccommodationtype').val() === "iswholepropertyfalse") { //Rooms scenario

                    var formId = 'frmRoomrentstep';
                    var formData = new FormData($('#' + formId)[0]);
                    $.ajax({
                        url: '/listing/saveroomrentstep',
                        type: 'POST',
                        data: formData,
                        processData: false,
                        contentType: false,
                        success: function (result) {
                            //console.log(result);
                            var successMessage = "Step-" + currentStep + " saved successfully!"
                            toastr.success(successMessage, "Success");
                            /// create property acceptings UI for step 7
                            createPropertyAcceptingsUI(result);
                        },
                        error: function (xhr, status, error) {
                            // Handle error
                            debugger;
                            console.log(xhr);
                        }
                    }); // ajax call for room scenario ends
                }
                else if (stepDirection === "forward") { // whole property scenario
                    var formId = 'frmwholepropertyrentstep';

                    var formData = new FormData($('#' + formId)[0]);
                    $.ajax({
                        url: '/listing/savewholepropertyrentstep',
                        type: 'POST',
                        data: formData,
                        processData: false,
                        contentType: false,
                        success: function (result) {
                           // console.log(result);
                            var successMessage = "Step-" + currentStep + " saved successfully!"
                            toastr.success(successMessage, "Success");
                            /// create property acceptings UI for step 7
                            createPropertyAcceptingsUI(result);                            
                        },
                        error: function (xhr, status, error) {
                            // Handle error
                            var errorMessage = "Error saving step-" + currentStep + " !"
                            toastr.error(errorMessage, "Error");
                           // debugger;
                            //console.log(xhr);
                        }
                    });
                }

                //---End of Ajax call to save step in database---//                    
            }

        }///End of validate and save step-4

        // validate and save step-5
        else if (currentStep === 5) {
            var isValidStep5
            if ($('#txtaccommodationtype').val() === "iswholepropertyfalse") { //Rooms scenario
                isValidStep5 = validateRoomavailbility();
            }
            else {  // whole property scenario

                isValidStep5 = validatePropertyavailbilitydate();
            }

            if (!isValidStep5)  // Validate step 5
            {
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);
            }   //End of Step- 5 form Validation             
            else if (stepDirection === "forward")//save step 5 
            {
                if ($('#txtaccommodationtype').val() === "iswholepropertyfalse") { //Rooms scenario
                    //--Ajax call to save step in database starts---//

                    var formId = 'frmRoomavailabilitystep';
                    var formData = new FormData($('#' + formId)[0]);

                    $.ajax({
                        url: '/listing/saveroomavailabilitystep',
                        type: 'POST',
                        data: formData,
                        processData: false,
                        contentType: false,
                        success: function (data) {
                            var successMessage = "Step-" + currentStep + " saved successfully!"
                            toastr.success(successMessage, "Success");
                            //console.log(data);
                        },
                        error: function (xhr, status, error) {
                            // Handle error
                            var errorMessage = "Error saving step-" + currentStep + " !"
                            toastr.error(errorMessage, "Error");
                            //debugger;
                            //console.log(xhr);
                        }
                    });

                //---End of Ajax call to save step in database---//                    
                }
                else if (stepDirection === "forward") {  // whole property scenario

                    //--Ajax call to save step in database starts---//
                    var formId = 'frmpropavailabilitystep';
                    // $('#' + formId).submit(); // Programmatically trigger form submission
                    var formData = new FormData($('#' + formId)[0]);
                    $.ajax({
                        url: '/listing/savewholepropertyavailabilitystep',
                        type: 'POST',
                        data: formData,
                        processData: false,
                        contentType: false,
                        success: function (data) {
                            //console.log(data); 
                            var successMessage = "Step-" + currentStep + " saved successfully!"
                            toastr.success(successMessage, "Success");
                        },
                        error: function (xhr, status, error) {
                            // Handle error
                            var errorMessage = "Error saving step-" + currentStep + " !"
                            toastr.error(errorMessage, "Error");
                            //debugger;
                            //console.log(xhr);
                        }
                    });

                //---End of Ajax call to save step in database---//                    
                }
                
            }

        }///End of validate and save step-5

        // validate and save step-6
        else if (currentStep === 6) {

            var fileInput = document.getElementById('files');
            if (fileInput.files.length === 0 /*|| $('#images:empty').length === 1*/ ) {
                Swal.fire({
                    icon: 'error',
                    title: 'Validation Error',
                    text: 'Please upload property image(s) before moving to the next step',
                    showConfirmButton: false,
                    timer: 3000,
                });
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);
            }
            //End of Step- 6 form Validation

            else if (stepDirection === "forward")//save step 6 
            {
                var successMessage = "Step-" + currentStep + " saved successfully!"
                toastr.success(successMessage, "Success");

            }

        }///End of validate and save step-7
        // save step-7
        else if (currentStep === 7 && stepDirection === "forward") {

            //--Ajax call to save step in database starts---//
            var formId = 'frmflatmatepreferencestep';
            // $('#' + formId).submit(); // Programmatically trigger form submission
            var formData = new FormData($('#' + formId)[0]);
            $.ajax({
                url: '/listing/saveflatmatepreferencestep',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (data) {
                    var successMessage = "Step-" + currentStep + " saved successfully!"
                    toastr.success(successMessage, "Success");
                    //console.log(data);
                   /* $('#txtintroPropertyId').val(data.id);*/
                },
                error: function (xhr, status, error) {
                    // Handle error
                    var errorMessage = "Error saving step-" + currentStep + " !"
                    toastr.error(errorMessage, "Error");
                   // debugger;
                    console.log(xhr);
                }
            });

            //---End of Ajax call to save step in database---//                    
        }
        // validate and save step-8
        else if (currentStep === 8) {
            var isValidStep8 = $('#frmintrostep').valid();
            if (!isValidStep8)  // Validate step 5
            {
                e.preventDefault();
                $('#smartwizard').smartWizard('validator', stepIndex);
            }   //End of Step- 8 form Validation

            else if(stepDirection === "forward")//save step 8 
            {
                //--Ajax call to save step in database starts---//
                var formId = 'frmintrostep';
                // $('#' + formId).submit(); // Programmatically trigger form submission
                var formData = new FormData($('#' + formId)[0]);
                $.ajax({
                    url: '/listing/saveintrostep',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        var successMessage = "Step-" + currentStep + " saved successfully!"
                        toastr.success(successMessage, "Success");
                        //console.log(data);
                        /*$('#txtimagePropertyId').val(data.id);*/
                    },
                    error: function (xhr, status, error) {
                        // Handle error
                        var errorMessage = "Error saving step-" + currentStep + " !"
                        toastr.error(errorMessage, "Error");
                        //debugger;
                        //console.log(xhr);
                    }
                });

                //---End of Ajax call to save step in database---//                    

            }

        }///End of validate and save step-8        

    }); //leaveStep Ends


    /*Form Validation Begins*/

    /*Step-1 form validation starts */
    function validationAccommodationtype() {
        var radioInputs = document.getElementsByClassName('type');
        var isChecked = false;

        for (var i = 0; i < radioInputs.length; i++) {
            if (radioInputs[i].checked) {
                isChecked = true;
                break;
            }
        }

        return isChecked;
    }
    //$("#accommodationtypestep").validate({
    //    rules: {
    //        'Property.Accommodationtype': { required: true },
    //        'Property.Propertytype': { required: true },
    //        'Property.address': { required: true },
    //        'Property.Bed': { required: true },
    //        Address: { required: true },
    //    },
    //    messages: {
    //        'Property.Accommodationtype': {
    //            required: ""
    //        },
    //        'Property.Propertytype': {
    //            required: "<span class='text-danger'><strong>*<strong></span>"
    //        },
    //        'Property.address': {
    //            required: "<span class='text-danger'><strong>*<strong></span>"
    //        },
    //    }
    //});
    /*End of Step-1 form validation*/


    /*Step-2 form validation begins */
    $("#frmaboutstep").validate({
        rules: {
            'Property.Address': { required: true },
            'Property.Bed': { required: true },
            'Property.Bathroom': { required: true },
            'Property.CityID': { required: true },
        },
        messages: {
            'Property.Address': {
                required: "<span class='text-danger'> &nbsp; required</span>"
            },
            'Property.Bed': {
                required: "<span class='text-danger'></span>"
            },
            'Property.Bathroom': {
                required: "<span class='text-danger'></span>"
            },
            'Property.CityID': {
                required: "<span class='text-danger'> &nbsp;&nbsp;  required</span>"
            },
        }
    });
    /*End of Step-2 form validation */

    $('#frmroomfeaturesstep').validate({
        rules: {
            'Room.Name': { required: true },
        },
        messages: {
            required: "<span class='text-danger'>*</span>"
        }
    });
    /*Step-3 form validation begins*/

    /*End of Step-3 form validation begins*/

    /*Step-4 form validation begins*/
    $("#frmwholepropertyrentstep").validate({
        rules: {
            'Property.Rent': { required: true },
            'Property.BondType': { required: true },
            'Property.Billstatus': { required: true },
        },
        messages: {
            'Property.Rent': {
                required: "<span class='text-danger'> &nbsp; required</span>"
            },
            'Property.BondType': {
                required: "<span class='text-danger'>&nbsp; required</span>"
            },
            'Property.Billstatus': {
                required: "<span class='text-danger'>&nbsp; required</span>"
            },
        }
    });
    /*End of Step-4 form validation */

    /*Step-5 form validation begins */
    $("#frmavailabilitystep").validate({
        rules: {
            'Property.AvailableFrom': { required: true },
            'Property.MinStay': { required: true },
            'Property.MaxStay': { required: true },
        },
        messages: {
            'Property.AvailableFrom': {
                required: "<span class='text-danger'> &nbsp; required</span>"
            },
            'Property.MinStay': {
                required: "<span class='text-danger'>*</span>"
            },
            'Property.MaxStay': {
                required: "<span class='text-danger'>*</span>"
            },
        }
    });
    /*End of Step-5 form validation */

    /*Step-8 form validation begins*/
    $("#frmintrostep").validate({
        rules: {
            'Property.AboutFlatmates': { required: true },
            'Property.Description': { required: true }
        },
        messages: {
            'Property.AboutFlatmates': {
                required: "<span class='text-danger'> &nbsp; required</span>"
            },
            'Property.Description': {
                required: "<span class='text-danger'>&nbsp; required</span>"
            },
        }
    });
    /*End of Step-8 form validation */

    function createRoomUI(result) {
        $('#divroomfeatures').empty();
        $('#divroomrent').empty();
        $('#divroomavailbility').empty();
        $.each(result, function (i, data) {         
            if (data != null) {
                var roomHtml = '<div class="row justify-content-center">' +
                    '<div class="col-sm-6 mb-2">' +
                    '<div class="mb-3">' +
                    '<label class="text-label form-label">Room name</label>' +
                    '<input type="text" class="roomname form-control" data-val="true" data-val-required="The Name field is required." id="Room_' + i + '__Name" name="Room[' + i + '].Name" value="Room ' + (i + 1) + '">' +
                    '</div>' +
                    '<div class="mb-3">' +
                    '<label class="text-label form-label">Room type</label>' +
                    '<div class="btn-group w-100" role="group" aria-label="Basic radio toggle button group">' +
                    '<input type="radio" class="btn-check" id="radioprvate' + i + '" value="1" data-val="true" data-val-required="The RoomType field is required." name="Room[' + i + '].RoomType">' +
                    '<label class="btn btn-outline-primary" for="radioprvate' + i + '">Private</label>' +
                    '<input type="radio" class="btn-check" id="radioshared' + i + '"checked value="2" name="Room[' + i + '].RoomType">' +
                    '<label class="btn btn-outline-primary" for="radioshared' + i + '">Shared</label>' +
                    '</div>' +
                    '</div>' +
                    '<div class="mb-3">' +
                    '<label class="text-label form-label">Room furnishings</label>' +
                    '<div class="btn-group w-100" role="group" aria-label="Basic radio toggle button group">' +
                    '<input type="radio" class="btn-check" id="radioflexible' + i + '" value="1" data-val="true" data-val-required="The Furnishing field is required." name="Room[' + i + '].Furnishing">' +
                    '<label class="btn btn-outline-primary" for="radioflexible' + i + '">Flexible</label>' +
                    '<input type="radio" class="btn-check" id="radiofurnished' + i + '"checked value="2" name="Room[' + i + '].Furnishing">' +
                    '<label class="btn btn-outline-primary" for="radiofurnished' + i + '">Furnished</label>' +
                    '<input type="radio" class="btn-check" id="radioUnfurnished' + i + '" value="3" name="Room[' + i + '].Furnishing">' +
                    '<label class="btn btn-outline-primary" for="radioUnfurnished' + i + '">Unfurnished</label>' +
                    '</div>' +
                    '</div>' +
                    '<div class="mb-3">' +
                    '<label class="text-label form-label">Bathroom</label>' +
                    '<div class="btn-group w-100" role="group" aria-label="Basic radio toggle button group">' +
                    '<input type="radio" class="btn-check" id="radiobshared' + i + '"checked value="1" name="Room[' + i + '].BathroomType">' +
                    '<label class="btn btn-outline-primary" for="radiobshared' + i + '">Shared</label>' +
                    '<input type="radio" class="btn-check" id="radiown' + i + '" value="2" name="Room[' + i + '].BathroomType">' +
                    '<label class="btn btn-outline-primary" for="radiown' + i + '">Own</label>' +
                    '<input type="radio" class="btn-check" id="radioensuite' + i + '" value="3" name="Room[' + i + '].BathroomType">' +
                    '<label class="btn btn-outline-primary" for="radioensuite' + i + '">Ensuite</label>' +
                    '</div>' +
                    '</div>' +
                    '<div class="mb-3">' +
                    '<label class="text-label form-label">Bed Size</label>' +
                    '<div class="btn-group w-100" role="group" aria-label="Basic radio toggle button group">' +
                    '<input type="radio" class="btn-check" id="radiosingle' + i + '" value="Single" name="Room[' + i + '].BedSize">' +
                    '<label class="btn btn-outline-primary" for="radiosingle' + i + '">Single</label>' +
                    '<input type="radio" class="btn-check" id="radiodouble' + i + '"checked value="Double" name="Room[' + i + '].BedSize">' +
                    '<label class="btn btn-outline-primary" for="radiodouble' + i + '">Double</label>' +
                    '<input type="radio" class="btn-check" id="radioqueen' + i + '" value="Queen" name="Room[' + i + '].BedSize">' +
                    '<label class="btn btn-outline-primary" for="radioqueen' + i + '">Queen</label>' +
                    '<input type="radio" class="btn-check" id="radioking' + i + '" value="King" name="Room[' + i + '].BedSize">' +
                    '<label class="btn btn-outline-primary" for="radioking' + i + '">King</label>' +
                    '<input type="radio" class="btn-check" id="radionone' + i + '" value="None" name="Room[' + i + '].BedSize">' +
                    '<label class="btn btn-outline-primary" for="radionone' + i + '">None</label>' +
                    '</div>' +
                    '</div><hr/>' +
                    '<div class="mb-3">' +
                    '<label class="text-label form-label">Roomate Preference</label>' +
                    '<div class="btn-group w-100" role="group" aria-label="Basic radio toggle button group">' +
                    '<input type="radio" class="btn-check" id="radiormen' + i + 'value="1" name = "Room[' + i + '].PreferredFlatmate" > ' +
                    '<label class="btn btn-outline-primary" for="radiormen' + i + '">Men only</label>' +
                    '<input type="radio" class="btn-check" id="radiorwomen' + i + '" value="2" name="Room[' + i + '].PreferredFlatmate">' +
                    '<label class="btn btn-outline-primary" for="radiorwomen' + i + '">Women only</label>' +
                    '<input type="radio" class="btn-check" id="radiornocpl' + i + '" value="3" name="Room[' + i + '].PreferredFlatmate">' +
                    '<label class="btn btn-outline-primary" for="radiornocpl' + i + '">No couples</label>' +
                    '<input type="radio" class="btn-check" id="radioranyone' + i + '"checked value="4" name="Room[' + i + '].PreferredFlatmate">' +
                    '<label class="btn btn-outline-primary" for="radioranyone' + i + '">Anyone inc. couples</label>' +
                    '</div>' +
                    '</div>'
                    '</div>' +
                    '</div>';
                var hiddenRoomIdInputHtml = '<input type="hidden" id="Room_' + i + '__ID" name="Room[' + i + '].id" value="' + data.id + '">';
                hiddenRoomIdInputHtml += '<input type="hidden" id="Room_' + i + '__UId" name="Room[' + i + '].UId" value="' + data.uId + '">';
                //debugger;
                $('#divroomfeatures').append(roomHtml);
                $('#divroomfeatures').append(hiddenRoomIdInputHtml);

                ///create room features UI

                var roomFeatures = '<div class="row justify-content-center" > ' +
                    '<div class="col-sm-6 mb-2">' +
                    '<h4>Room furnishings and features</h4>' +
                    '<div class="row mt-3">';
                // Iterate through the roomFurnishings objects
                $.each(data.roomFurnishings, function (j, roomFurnishing) {
                    var checkboxId = 'roomFurnishing_' + i + '_' + j;
                    var furnishigId = 'hidden_' + i + '_' + j;
                    var roomID = 'RoomFurnishings[' + i + '][' + j + '].RoomID';

                    var propertyID = 'RoomFurnishings[' + i + '][' + j + '].propertyID';
                    var furnishingID = 'RoomFurnishings[' + i + '][' + j + '].furnishingID';
                    //var createdOn = 'RoomFurnishings[' + i + '][' + j + '].createdOn';
                    var createdbyUserID = 'RoomFurnishings[' + i + '][' + j + '].createdbyUserID';
                    var svgd = 'RoomFurnishings[' + i + '][' + j + '].svgd';
                    var uid = 'RoomFurnishings[' + i + '][' + j + '].uId';

                    roomFeatures += '<div class="col-lg-3 col-sm-6 col-6">' +
                        '<div class="text-center mt-3">' +
                        '<label for="' + checkboxId + '">' +
                        '<input class="radio-input" type="checkbox" id="' + checkboxId + '" style="display:block;" name="RoomFurnishings[' + i + '][' + j + '].isSelected" value="true">' +
                        '<input  type="hidden" id="' + furnishigId + '"name="RoomFurnishings[' + i + '][' + j + '].id" value="' + roomFurnishing.id + '">' +
                        '<input type="hidden" id=' + roomID + ' name=' + roomID + ' value="' + data.id + '">' +
                        '<input  type="hidden" id=' + propertyID + ' name=' + propertyID + ' value="' + roomFurnishing.propertyID + '">' +
                        '<input  type="hidden" id=' + furnishingID + ' name=' + furnishingID + ' value="' + roomFurnishing.furnishingID + '">' +
                        '<input  type="hidden" id=' + createdbyUserID + ' name=' + createdbyUserID + ' value="' + roomFurnishing.createdbyUserId + '">' +
                        '<input  type="hidden" id=' + svgd + ' name=' + svgd + ' value="' + roomFurnishing.svgd + '">' +
                        '<input  type="hidden" id=' + uid + ' name=' + uid + ' value="' + roomFurnishing.uId + '">' +
                        '<span class="radio-tile">' +
                        '<span class="radio-icon">' +
                        '<img src="' + roomFurnishing.furnishing.svgd + '" />' +                        
                        '</span>' +
                        '</span>' +
                        '</label>' +
                        '<p>' + roomFurnishing.furnishing.title + '</p>' +
                        '</div>' +
                        '</div>';
                });

                roomFeatures += '</div></div></div><hr>'
                $('#divroomfeatures').append(roomFeatures);

                ///End create room features UI

                // create Room rent, bond, bill 
                var roomRentHtml = '';
                if (i == 0) {  /// dont apply pt-5 
                    roomRentHtml = '<h4>Rent, bond, and bills</h4>' +
                        '<h4>Room-' + (i + 1) + '</h4>';
                }
                else { //apply pt - 5 
                    roomRentHtml = '<h4 class="pt-5">Room-' + (i + 1) + '</h4>';
                }

                roomRentHtml += '<div class="mb-3">' +
                    '<label class="text-label form-label">Monthly rent</label>' +
                    '<input type="hidden" id="RoomId4Rent_' + i + '__ID" name="Room[' + i + '].id" value="' + data.id + '">' +
                    '<input type="hidden" id="RoomUId4Rent_' + i + '__UId" name="Room[' + i + '].UId" value="' + data.uId + '">' +
                    '<input type="text" class="roomrent form-control" placeholder="Monthly rent" data-val="true" data-val-number="The field Rent must be a number." id="Room_' + i + '__Rent" name="Room[' + i + '].Rent">' +
                    '</div>' +
                    '<div class="mb-3">' +
                    '<label class="text-label form-label">Bond</label>' +
                    '<select class="mb-4 nice-select wide w-100" data-val="true" data-val-required="The bond field is required." id="Room_' + i + '__bond" name="Room[' + i + '].BondType">' +
                    '<option value="None">none</option>' +
                    '<option value="OneMonth">1 month</option>' +
                    '<option value="TwoMonth">2 months</option>' +
                    '<option value="ThreeMonths">3 months</option>' +
                    '<option value="FourMonths">4 months</option>' +
                    '</select>' +
                    '</div>' +
                    '<div class="mb-3">' +
                    '<label class="text-label form-label">Bills</label>' +
                    '<select class="nice-select wide w-100" data-val="true" id="Room_' + i + '__Status" name="Room[' + i + '].Billstatus">' +
                    '<option value="AdditionalToRent">Additional to the rent</option>' +
                    '<option value="SomeIncludedinRent">Some included in the rent</option>' +
                    '<option value="IncludedinRent">Included in rent</option>' +
                    '</select>' +
                    '<hr/>' +
                    '</div>';
                $('#divroomrent').append(roomRentHtml);


                // End create Room rent, bond, bill

                // create Room availibility UI for each room 
                var roomAvailability = '';
                if (i == 0)
                    roomAvailability = '<h4 class="pb-3">Availability</h4>';

                roomAvailability += '<div class="mb-3">' +
                    '<label class="text-label form-label">Room-' + (i + 1) + ' available from</label>' +
                    '<input type="hidden" id="RoomId4Avail_' + i + '__ID" name="Room[' + i + '].id" value="' + data.id + '">' +
                    '<input type="hidden" id="RoomUId4Avail_' + i + '__UId" name="Room[' + i + '].UId" value="' + data.uId + '">' +
                    '<div class="input-group">' +                    
                    '<input readonly type="text" class="datepicker roomavail form-control" readonly="" id="Room_' + i + '__AvailableFrom" name="Room[' + i + '].AvailableFrom" value="">' +
                    '<div class="input-group-text fa fa-calendar"></div>' +
                    '</div>' +
                    '</div>' +
                    '<div class="mb-3">' +
                    '<label class="text-label form-label">Minimum length of stay</label>' +
                    '<select class="nice-select wide w-100" id="Room_' + i + '__MinStay" name="Room[' + i + '].MinStay">' +
                    '<option value="None" selected="">No minimum stay</option>' +
                    '<option value="OneMonth">1 month</option>' +
                    '<option value="TwoMonth">2 months</option>' +
                    '<option value="FourMonth">4 months</option>' +
                    '<option value="Sixmonth">6 months</option>' +
                    '<option value="Twelvemonth">12 months</option>' +
                    '</select>' +
                    '</div>' +
                    '<div class="mb-3">' +
                    '<label class="text-label form-label mt-3">Maximum length of stay</label>' +
                    '<select class="mb-4 nice-select wide w-100" id="Room_' + i + '__MaxStay" name="Room[' + i + '].MaxStay">' +
                    '<option value="None">No maximum stay</option>' +
                    '<option value="OneMonth">1 month</option>' +
                    '<option value="SixMonths">6 months</option>' +
                    '<option value="OneYear">1 year</option>' +
                    '<option value="TwoYears">2 years</option>' +
                    '<option value="ThreeYears">3 years</option>' +
                    '<option value="FiveYears">5 years or more</option>' +
                    '</select>' +
                    '</div>' +
                    '</div>';

                // Append the dynamic HTML to the divroomavailbility div
                $('#divroomavailbility').append(roomAvailability);
                //End  create Room availibility UI for each room
            }

        });
        /// end of loop to create dynamic html for room

    } // end of function to create dynamic html for step 3,4 & 5

    //create Property Acceptings UI for step 6
    function createPropertyAcceptingsUI(result)
    {
        $('#divaccepting').empty();
        $.each(result, function (i, data) {
            if (data != null) {
                var isSelected = data.isSelected ? 'checked' : ''; // Determine if the checkbox should be checked or not
                var checkboxHtml = '<div class="col-lg-3 col-sm-6 col-6">' +
                    '<div class="text-center mt-3">' +
                    '<label>' +
                    '<input name="Property.PropertyAcceptings[' + i + '].isSelected" class="radio-input" type="checkbox" value="true" ' + isSelected + ' />' +
                    '<span class="radio-tile">' +
                    '<span class="radio-icon">' +
                    '<img src="' + data.accepting.iconpath + '" />' +
                    '</span>' +
                    '</span>' +
                    '</label>' +                     
                    '<p>' + data.accepting.title + '</p>' +
                    '</div>' +
                    '</div>';

                var hiddenIdInputHtml = '<input type="hidden" id="PropertyAcceptings_' + i + '__ID" name="Property.PropertyAcceptings[' + i + '].id" value="' + data.id + '">';
                var hiddenUIdInputHtml = '<input type="hidden" id="PropertyAcceptings_' + i + '__UId" name="Property.PropertyAcceptings[' + i + '].UId" value="' + data.uId + '">';
                var hiddenPIdInputHtml = '<input type="hidden" id="PropertyAcceptings_' + i + '__PID" name="Property.PropertyAcceptings[' + i + '].PropertyID" value="' + data.propertyID + '">';
                var hiddenCBUIdInputHtml = '<input type="hidden" id="PropertyAcceptings_' + i + '__CBUID" name="Property.PropertyAcceptings[' + i + '].CreatedbyUserID" value="' + data.createdbyUserID + '">';
                var hiddenAIdInputHtml = '<input type="hidden" id="PropertyAcceptings_' + i + '__AID" name="Property.PropertyAcceptings[' + i + '].AcceptingID" value="' + data.acceptingID + '">';
                $('#divaccepting').append(checkboxHtml);

                $('#divaccepting').append(hiddenIdInputHtml);
                $('#divaccepting').append(hiddenUIdInputHtml);
                $('#divaccepting').append(hiddenPIdInputHtml);
                $('#divaccepting').append(hiddenCBUIdInputHtml);
                $('#divaccepting').append(hiddenAIdInputHtml);
            }
        });        
    }
    //End create Property Acceptings UI for step 6

    //validation function for property availbility
    function validatePropertyavailbilitydate() {
        var isValid = true;
        var input = $('#txtAvailablefrom');
        var inputValue = input.val().trim();
        var span = input.next('span');
        // Clear existing error messages
        span.empty();

        if (inputValue === '') {
            isValid = false;
            span.text('required');
            input.focus();
        }
        return isValid;
    }

//End validation function for property availbility

    //validation function for Room name
    function validateRoomName() {
        var isValid = true;
        var firstEmptyInput = null;

        $('input[type="text"].roomname').each(function () {
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
    //end of validation function for Room name

    //validation function for Room rent
    function validateRoomrent() {
        var isValid = true;
        var firstEmptyInput = null;

        $('input[type="text"].roomrent').each(function () {
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
    //end of validation function for Room rent

    //validation function for Room availbility
    function validateRoomavailbility() {
        var isValid = true;
        var firstEmptyInput = null;

        $('input[type="text"].roomavail').each(function () {
            var input = $(this);
            var inputValue = input.val().trim();
            var span = input.next('span');

            // Clear existing error messages
            span.empty();

            if (inputValue === '') {
                isValid = false;
                span.text('required');
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
    //end of validation function for Room availbility

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
            span.html('&nbsp;required');
        }

        if ((input.hasClass('roomrent') || input.hasClass('rent')) && !/^\d+$/.test(inputValue) && input.val().length > 0) {
            span.text('');
            //span.text(' invalid value entered');
            span.html('&nbsp;invalid value entered');
            input.val('');
        }
        else if (input.val().length === 0)
            span.html('&nbsp;required');
    }

    //end of function to show validation error if required input is empty

    //validation function for rent
    function validatePropertyRent() {
        var isValid = true;

        var input = $('#txtWholePropertyRent');
        var inputValue = input.val().trim();
        var span = input.next('span');

        // Clear existing error messages
        span.empty();

        if (inputValue === '') {
            isValid = false;
            input.next('span').empty();
            input.after('<span class="text-danger">&nbsp;required</span>');
            input.focus();
        } else if (!/^\d+$/.test(inputValue)) {
            isValid = false;
            span.html(' invalid value entered');
            input.val(''); // Empty the input when invalid value is entered
        }

        return isValid;
    }
    //end of validation function for rent

});


