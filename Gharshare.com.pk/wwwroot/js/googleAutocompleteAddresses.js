$(document).ready(function () {
    var autocomplete;
    var input = document.getElementById('propAddress');
    //debugger;
    autocomplete = new google.maps.places.Autocomplete((input), {
        types: ['geocode'],
        componentRestrictions: { country: "PK" },
        fields: ['place_id', 'geometry', 'name'],
    })
    //var autocomplete1;
    //var input1 = document.getElementById('suburb');
    //// debugger;
    //autocomplete1 = new google.maps.places.Autocomplete((input1), {
    //    types: ['geocode'],
    //    componentRestrictions: { country: "PK" },
    //    fields: ['place_id', 'geometry', 'name'],
    //})
});
