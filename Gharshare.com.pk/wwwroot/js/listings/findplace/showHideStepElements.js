$(document).ready(function () {

    $('#radioGroup').on('click', function () { ///In 'This property if for',when group radio is clicked,
        $('#divgroup').css('display', 'block'); //show divgroup
        $('#divotherPerson').empty();
    });
    $('#radioCouple,#radioMe').on('click', function () { /// When group radio is clicked, show divgroup
        $('#divgroup').css('display', 'none');
        $('#divotherPerson').empty();
        var radioButtonclID = $(this).attr('id');
        if (radioButtonclID === "radioCouple") {
            createDynamicHtml4Partner();
            $('#divotherPerson input[type="text"].ag').on('input', handleInputValueChange);
            $('#divotherPerson input[type="text"].op').on('input', handleInputValueChange);
        }
        else { $('#divotherPerson').empty(); }
    });

    $('#radioRoom').on('click', function () {
        $('#divExistinRoom').css('display', 'block');
        $('#divwholeproperty').css('display', 'none');
        $('#txtaccommodationtype').val('notWholeproperty');

        $('#btnradiostudent').prop('checked', false); // to unselect radio buttons for student accommodation property type
        $('#btnradiohstay').prop('checked', false); //to unselect radio buttons for home property type

        $('#btnradiohouse').prop('checked', true); // to select house as property type when Room in existing.. radio clicked

    });

    $('#radiowholeproperty').on('click', function () {
        $('#divwholeproperty').css('display', 'block');
        $('#divExistinRoom').css('display', 'none');
        $('#txtaccommodationtype').val('wholeproperty');

        $('#btnradiostudent').prop('checked', false); // to unselect radio buttons for student accommodation property type
        $('#btnradiohstay').prop('checked', false); //to unselect radio buttons for home property type

        $('#radio2bd').prop('checked', true); // for default selection when whole property is selected

    });

    $('#radiostudentaccommodation,#radionhomestay').on('click', function () {
        $('#divExistinRoom').css('display', 'none');
        $('#divwholeproperty').css('display', 'none');
        $('#txtaccommodationtype').val('notWholeproperty');

        var radioButtonId = $(this).attr('id');

        if (radioButtonId === 'radiostudentaccommodation') {
            // Code for 'radiostudentaccommodation' radio button clicked            
            $('#btnradiostudent').prop('checked', true);
        } else if (radioButtonId === 'radionhomestay') {
            // Code for 'radionhomestay' radio button clicked             
            $('#btnradiohstay').prop('checked', true);

        }
    });

    // function to create dynamic UI for Partner of person creating listing to find a place.
    function createDynamicHtml4Partner() {
        $('#divotherPerson').empty();
        var personListingId = $('#txtintroPersonId').val().trim();
        var i = 0;
        var dynamicHTML = '<div class="mb-3">' +
            '<label class="text-label form-label">' +
            'Your partner\'s first name' +
            '</label>' +
            '<input type="text" class="op form-control" id="PersonListing.Mates[' + i + '].Name" name="PersonListing.Mates[' + i + '].Name" placeholder="Enter Your Partner Name">' +
            '</div>' +
            '<div class="mb-3">' +
            '<label class="text-label form-label">' +
            'Your partner\'s age' +
            '</label>' +
            '<input type="text" id="PersonListing.Mates[' + i + '].Age" name="PersonListing.Mates[' + i + '].Age" class="ag form-control" placeholder="Enter Partner age">' +
            '</div>' +
            '<label class="text-label form-label">' +
            'The gender your partner identifies with' +
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
            '<input type="hidden" id="PersonListing.Mates[' + i + '].Partner" name="PersonListing.Mates[' + i + '].Partner"  value="true">' +
            '<input type="hidden" id="PersonListing.Mates[' + i + '].personListingID" name="PersonListing.Mates[' + i + '].personListingID" value="' + personListingId + '">' +
            '</div>';

        $('#divotherPerson').append(dynamicHTML);

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

        if (input.hasClass('ag') && !/^\d+$/.test(inputValue) && input.val().length > 0) {
            span.text('invalid value entered');
            input.val('');
        }
        else if (input.val().length === 0)
            span.text('required');
    }

    //end of function to show validation error if required input is empty
});
