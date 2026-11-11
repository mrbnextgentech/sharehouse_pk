$(document).ready(function () {
    $('#radioRoom').on('click', function () {
        $('#divExistinRoom').css('display', 'block');
        $('#divwholeproperty').css('display', 'none');
        $('#txtaccommodationtype').val('iswholepropertyfalse');

        $('#divrooms4rent').css('display', 'block');
        

        $('#divwhocurrentlylives').css('display', 'block'); //show who curretnly div on about property step        
        $('#wholepropertyFurnishing').css('display', 'none'); //hide room furnishing on about property step

        $('#divroomavailbility').css('display', 'block'); //show room availability UI on 'Availbility' step
        $('#divpropertyavailbility').css('display', 'none'); //hide property availability UI on 'Availbility' step

        $('#divpropertyrent').css('display', 'none'); //show property rent UI on 'rent,bond,bill' step
        $('#divroomrent').css('display', 'block'); //hide room rent UI on 'rent,bond,bill' step

        var contentHeight = $(".step-content.active").outerHeight();

        $('#btnradiostudent').prop('checked', false); // to unselect radio buttons for student accommodation property type
        $('#btnradiohstay').prop('checked', false); //to unselect radio buttons for home property type

        $('#btnradiohouse').prop('checked', true); // to select house as property type when Room in existing.. radio clicked
       
    });                                                         

    $('#radiowholeproperty').on('click', function () {
        $('#divwholeproperty').css('display', 'block');
        $('#divExistinRoom').css('display', 'none');
        $('#txtaccommodationtype').val('iswholepropertytrue');

        $('#divrooms4rent').css('display', 'none');

        $('#divwhocurrentlylives').css('display', 'none'); //hide who curretnly div on about property step        
        $('#wholepropertyFurnishing').css('display', 'block'); //show room furnishing on about property step

        $('#divpropertyavailbility').css('display', 'block'); //show property availability UI on 'Availbility' step
        $('#divroomavailbility').css('display', 'none'); //hide room availability UI on 'Availbility' step

        $('#divpropertyrent').css('display', 'block'); //show property rent UI on 'rent,bond,bill' step
        $('#divroomrent').css('display', 'none'); //hide room rent UI on 'rent,bond,bill' step

        $('#btnradiostudent').prop('checked', false); // to unselect radio buttons for student accommodation property type
        $('#btnradiohstay').prop('checked', false); //to unselect radio buttons for home property type

        $('#radio2bd').prop('checked', true); // for default selection when whole property is selected
        
    });

    $('#radiostudentaccommodation,#radionhomestay').on('click', function () {
        $('#divExistinRoom').css('display', 'none');
        $('#divwholeproperty').css('display', 'none');
        $('#txtaccommodationtype').val('iswholepropertyfalse'); 

        $('#divrooms4rent').css('display', 'block');

        $('#divwhocurrentlylives').css('display', 'block'); //show who curretnly div on about property step        
        $('#wholepropertyFurnishing').css('display', 'none'); //hide room furnishing on about property step

        $('#divroomavailbility').css('display', 'block'); //show room availability UI on 'Availbility' step
        $('#divpropertyavailbility').css('display', 'none'); //hide property availability UI on 'Availbility' step

        $('#divpropertyrent').css('display', 'none'); //show property rent UI on 'rent,bond,bill' step
        $('#divroomrent').css('display', 'block'); //hide room rent UI on 'rent,bond,bill' step

        var radioButtonId = $(this).attr('id');

        if (radioButtonId === 'radiostudentaccommodation') {
            // Code for 'radiostudentaccommodation' radio button clicked
           // $('#txtstudentaccommodation').val(7);
            $('#btnradiostudent').prop('checked', true);
        } else if (radioButtonId === 'radionhomestay') {
            // Code for 'radionhomestay' radio button clicked
            //$('#txthomestay').val(8);
            $('#btnradiohstay').prop('checked', true);
            
        }
    });
});
