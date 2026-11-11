$(document).ready(function () {
    $('#addfriend').click(createDynamicHtml4Friend);

    $('#divotherPerson').on('click', '.delete', function () {
        $(this).closest('.frnd').remove();
        updateIdsAndNames();
    });
    function updateIdsAndNames() {
        // Get all inspection rows inside the "availability" div
        const $friendRows = $('#divotherPerson .frnd');

        // Loop through each inspection row and update IDs and names
        $friendRows.each(function (index) {
            const newIndex = index;
            // Update IDs and names of input elements inside the current inspection row
            $(this).find('input, select').each(function () {
                const oldName = $(this).attr('name');
                const newName = oldName.replace(/\[\d+\]/, `[${newIndex}]`);
                $(this).attr('name', newName);
                $(this).attr('id', newName.replace(/\./g, '_'));
            });
        });
    }


    $('#divpartner input[type="text"].age').on('input', handleInputValueChange);
    $('#divpartner input[type="text"].name').on('input', handleInputValueChange);
    $('#txtmyname').on('input', handleInputValueChange);
    $('#txtmyage').on('input', handleInputValueChange);

    $('#divotherPerson input[type="text"].age').on('input', handleInputValueChange);
    $('#divotherPerson input[type="text"].name').on('input', handleInputValueChange)

    $('#radioGroup').on('click', function () {
        $('#divotherPerson').empty();
        $('#divpartner').empty();
        $('#divpartner').css('display', 'none');
        $('#divotherPerson').css('display', 'block');
        $('#addfriend').css('display', 'block');
        createDynamicHtml4Friend();
        $('#divotherPerson input[type="text"].ag').on('input', handleInputValueChange);
        $('#divotherPerson input[type="text"].op').on('input', handleInputValueChange);
    });
    $('#radioCouple').on('click', function () {
        //$('#divotherPerson').css('display', 'none');
        $('#divpartner').css('display', 'block');
        $('#addfriend').css('display', 'none');
        $('#divotherPerson').empty();
        $('#divpartner').empty();
        createDynamicHtml4Partner();
        $('#divpartner input[type="text"].ag').on('input', handleInputValueChange);
        $('#divpartner input[type="text"].op').on('input', handleInputValueChange);
    });

    $('#radioMe').on('click', function () {
        $('#divotherPerson').css('display', 'none');
        $('#divpartner').css('display', 'none');
        $('#addfriend').css('display', 'none');
        $('#divotherPerson').empty();
        $('#divpartner').empty();
    });

    // function to create dynamic UI for Partner of person creating listing to find a place.
    function createDynamicHtml4Partner() {
        $('#divpartner').empty();
        var personListingId = $('#txtintroPersonId').val().trim();
        var i = 0;
        var dynamicHTML = '<div class="row mb-3">' +
            '<div class="col-md-4">' +
            '<label class="text-label form-label">' +
            'Your partner\'s first name' +
            '</label>' +
            '<input type="text" id="txtmypartnername" class="name form-control" name="Partner.Name" placeholder="Enter Your Partner Name">' +
            '</div>' +
            '<div class="col-md-4">' +
            '<label class="text-label form-label">' +
            'Your partner\'s age' +
            '</label>' +
            '<input type="text" id="txtmypartnerage" name="Partner.Age" class="age form-control" placeholder="Enter Partner age">' +
            '</div>' +
            '<div class="col-md-4">' +
            '<label class="text-label form-label">' +
            'Your partner\'s gender' +
            '</label>' +
            '<select id="ddlmypartnerGender" class="mb-4 nice-select wide w-100" name="Partner.gender">' +
            '<option value="">--select--</option>' +
            '<option value="Woman">Woman</option>' +
            '<option value = "Man"> Man</option > ' +
            '<option value = "Transgender" > Transgender</option > ' +
            '<option value = "Undisclosed" > Undisclosed</option > ' +
            '</select>' +
            '<input type="hidden" name="Partner.Partner"  value="true">' +
            '<input type="hidden" name="Partner.personListingID" value="' + personListingId + '">' +
            '</div>' +
            '</div>';
        $('#divpartner').append(dynamicHTML);
        $('#divpartner input[type="text"].age').on('input', handleInputValueChange);
        $('#divpartner input[type="text"].name').on('input', handleInputValueChange);
    }
    // End of function to created dynamic UI for Partner of person creating listing to find a place.

    // function to create dynamic UI for Partner of person creating listing to find a place.
    function createDynamicHtml4Friend() {         
        var personListingId = $('#txtintroPersonId').val().trim();
        let friendCount = document.querySelectorAll('div.frnd').length;
        var i = 0;
        if (friendCount != null)
            i = friendCount;

        var dynamicHTML = '<div class="frnd"><div class="row mb-3">' +
            '<div class="col-md-4">' +
            '<label class="text-label form-label">' +
            'Your friend\'s name' +
            '</label>' +
            '<input type="text" class="name form-control" id="PersonListing.Mates[' + i + '].Name" name="PersonListing.Mates[' + i + '].Name" placeholder="Enter Your Partner Name">' +
            '</div>' +
            '<div class="col-md-4">' +
            '<label class="text-label form-label">' +
            'Your friend\'s age' +
            '</label>' +
            '<input type="text" id="PersonListing.Mates[' + i + '].Age" name="PersonListing.Mates[' + i + '].Age" class="age form-control" placeholder="Enter Partner age">' +
            '</div>' +
            '<div class="col-md-4">' +
            '<label class="text-label form-label">' +
            'Your friend\'s gender' +
            '</label>' +
            '<select class="mb-4 nice-select wide w-100" name="PersonListing.Mates[' + i + '].gender">' +
            '<option value="">--select--</option>' +
            '<option value="Woman">Woman</option>' +
            '<option value = "Man"> Man</option > ' +
            '<option value = "Transgender" > Transgender</option > ' +
            '<option value = "Undisclosed" > Undisclosed</option > ' +
            '</select>' +
            '<input type="hidden" id="PersonListing.Mates[' + i + '].Partner" name="PersonListing.Mates[' + i + '].Partner"  value="false">' +
            '<input type="hidden" id="PersonListing.Mates[' + i + '].personListingID" name="PersonListing.Mates[' + i + '].personListingID" value="' + personListingId + '">' +
            '</div>' +
            '</div>' +
            '<div class="row">' +
            '<div class="col-md-12 text-end mb-3">' +
            '<a href="#" class="delete"><i class="bi bi-trash"></i> Remove</a>' +
            '</div>' +
            '</div>' +
            '</div>';

        $('#divotherPerson').append(dynamicHTML);
        $('#divotherPerson input[type="text"].age').on('input', handleInputValueChange);
        $('#divotherPerson input[type="text"].name').on('input', handleInputValueChange)
    }
    // End of function to created dynamic UI for Partner of person creating listing to find a place.

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

        if ((input.hasClass('rent') || input.hasClass('age')) && !/^\d+$/.test(inputValue) && input.val().length > 0) {
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
});
