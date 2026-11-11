$(document).ready(function () {

    $("#createAdd").validate({
        rules: {
            'Property.AvailableFrom': {
                required: true
            },
            'Property.BondValue': { required: true },
            'Property.Parkingtype': { required: true },
            'Property.BondValue': {
                required: true,
                number: true,
            },
            'Property.Rent': {
                required: true,
                number: true,
            },
            'Property.CatogryID': {
                required: true
            },
            'Property.AdTitle': {
                required: true
            },

            'Property.Description': {
                required: true
            },
            'Property.Owner.Name': {
                required: true
            },
            'Property.Owner.Email': {
                required: true,
                email:true
            },
            'Property.Owner.PhoneNumber': {
        required: true
    },
            'Property.Street': { required: true },
            files: { required: true },
            CityID: { required: true },
            Address: { required: true },
        },
        messages: {
            'Property.AvailableFrom': {
                required: "<span class='text-danger'><strong>*<strong></span>"
            },
            'Property.BondValue': {
                required: "<span class='text-danger'><strong>*<strong></span>",
                number: "<span class='text-danger'>&nbspPlease enter a valid number</span>"
                //pattern: "<span class='text-danger'><strong>Enter a valid number<strong></span>"
            },
            'Property.Rent': {
                required: "<span class='text-danger'><strong>*<strong></span>",
                number: "<span class='text-danger'>&nbspPlease enter a valid number</span>"
            },
            'Property.CatogryID': {
                required: "<span class='text-danger'><strong>*<strong></span>"
            },
            'Property.AdTitle': {
                required: "<span class='text-danger'><strong>*<strong></span>"
            },
            'Property.Description': {
                required: "<span class='text-danger'><strong>*<strong></span>"
            },
            'Property.Parkingtype': {
                required: "<span class='text-danger'><strong>*<strong></span>"
            },
            'Property.Street': {
                required: "<span class='text-danger'><strong>*<strong></span>"
            },
            files: { required: "<span class='text-danger'>*</span>", },
            CityID: { required: "<span class='text-danger'>*</span>", },
            Address: { required: "<span class='text-danger'>*</span>", },

            'Property.Owner.Name': { required: "<span class='text-danger'>*</span>", },
            'Property.Owner.Email': {
                required: "<span class='text-danger'>*</span>",
                email: "<span class='text-danger'>&nbspPlease enter a valid email</span>"
                },
            'Property.Owner.PhoneNumber': { required: "<span class='text-danger'>*</span>", },

        }
    });

    var smartWizard = $('#smartwizard').smartWizard({
        selected: 0,
        theme: 'arrows',
        isValidationEnabled: true,
        validatorFunc: function () {
            var isValid = $("#createAdd").valid();
            return isValid;
        }
    });

    // This function will trigger validation on the 'showStep' event
    $('#smartwizard').on('leaveStep', function (e, anchorObject, stepIndex, stepDirection) {
        console.log(stepIndex);
        var isValid = $('#createAdd').valid();
        if (!isValid) {
            e.preventDefault();
            $('#smartwizard').smartWizard('validator', stepIndex);
        }
        /*Step-5 Validation*/
        if (stepIndex === 4) {
            if ($('#images').is(':empty')) {                
                Swal.fire({
                    icon: 'error',
                    icon: 'error',
                    title: 'Validation Error',
                    text: 'Please save images before moving to the next step',
                });
                e.preventDefault();
            }
        }
    });
});
    //-------------------------------//--------------------------------------------

