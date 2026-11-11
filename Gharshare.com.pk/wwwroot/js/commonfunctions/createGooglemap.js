function initMap() {
        // Create a new map instance
        var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 15, // Adjust the zoom level as needed
        });

    // Geocode the provided address and display it on the map
        var geocoder = new google.maps.Geocoder();
        var address = $('#txtAddress').val(); 
    geocoder.geocode({'address': address }, function (results, status) {
            if (status === 'OK') {
        map.setCenter(results[0].geometry.location);
    var marker = new google.maps.Marker({
        map: map,
        position: results[0].geometry.location,
        title: address,         
                });
            } else {
        alert('Geocode was not successful for the following reason: ' + status);
            }
        });
    }
