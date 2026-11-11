$(document).ready(function () {

    $("#addProperty").validate({
        rules: {
            email: {
                required: true,
                email: true
            },
            CityID: { required: true },
            Ownermobile: { required: true },
            Address: { required: true },
            Propertytype: { required: true, },
            CatogryID: { required: true, },            
        },
        messages: {
            email: {
                required: "<span class='text-danger'>*</span>",
                email: "Please enter a valid email address"
            },
            CityID: { required: "<span class='text-danger'>*</span>", },
            Address: { required: "<span class='text-danger'>*</span>", },            
            Ownermobile: { required: "<span class='text-danger'>*</span>", },
            CatogryID: { required: "<span class='text-danger'>*</span>", },
            Propertytype:{ required: "Please select property type" },
        //    phone: {
        //        required: "Please enter your phone number",
        //        phoneUS: "Please enter a valid US phone number"
        //    }
        }
    });

    var smartWizard = $('#smartwizard').smartWizard({
        selected: 0,
        theme: 'arrows',
        isValidationEnabled: true,
        validatorFunc: function () {
            var isValid = $("#addProperty").valid();
            return isValid;
        }
    });

    // This function will trigger validation on the 'showStep' event
    $('#smartwizard').on('leaveStep', function (e, anchorObject, stepIndex, stepDirection) {
        console.log(stepIndex);
        var isValid = $('#addProperty').valid();
        if (!isValid) {
            e.preventDefault();
            $('#smartwizard').smartWizard('validator', stepIndex);
        }
        ///Step-1 Validation
        if (stepIndex === 0) { 
        var selectedRadio = $('#step-1 input[name="Propertytype"]:checked');
        if (selectedRadio.length === 0) {
            e.preventDefault();
            $('#step-1').find('.sw-btn-next').prop('disabled', true); // disable next button
            //alert('select property type');
            Swal.fire({
                icon: 'error',
                title: 'Validation Error',
                text: 'Must select one option from property type',
                //footer: '<a href="">Why do I have this issue?</a>'
            });
        }
        } //////Step-1 Validation Ends
        //------------------
        else if(stepIndex === 1)   // Validation check for step-2
        {
            var selectedtenantopt = $('#step-2 input[name="PlanId"]:checked');
            if (selectedtenantopt.length === 0) {
                e.preventDefault();
                $('#step-2').find('.sw-btn-next').prop('disabled', true); // disable next button             
                Swal.fire({
                    icon: 'error',
                    title: 'Validation Error',
                    text: 'Must select one option from tenant status',
                });
            }
        }
         /// Validation for step-2 ends
    });
    //-------------------------------//--------------------------------------------

    //Move to step-2 as user selects option on step-1
    var radioButtons = $('#step-1 input[name="Propertytype"]');

    // Add an event listener to the radio buttons
    radioButtons.on('click', function () {
        // Get the selected radio button value
        var selectedValue = $(this).val();

        // Move to the next step using the smartWizard API
        $('#smartwizard').smartWizard('next');
    });
    ////-----------------///--------------------------
    //Move to step-2 as user selects option on step-1
    var radioTenantButtons = $('#step-2 input[name="PlanId"]');

    // Add an event listener to the radio buttons
    radioTenantButtons.on('click', function () {
        // Get the selected radio button value
        var selectedValue = $(this).val();

        // Move to the next step using the smartWizard API
        $('#smartwizard').smartWizard('next');
    });
    //---------------------------//---------------------------------
});
