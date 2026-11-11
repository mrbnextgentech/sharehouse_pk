function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 30.3753, lng: 69.3451 },
        zoom: 6 // Adjust the initial zoom level as needed
    });
    //debugger;
    console.log(document.getElementById('txtAddresslist').value);
    var addressList = document.getElementById('txtAddresslist').value.split('&');
    var count=1;
    addressList.forEach(function (address) {
        //console.log(address);
        var mapValues = address.split(":");
        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({ address: address }, function (results, status) {
            if (status === 'OK') {
                var marker = new google.maps.Marker({
                    map: map,
                    position: results[0].geometry.location,
                    title: mapValues[0], //address,
                    label: {
                        text: "Rs " + mapValues[1],
                        color: "#3878C7", // #FFFF00 Set the desired text color
                        fontSize: "12px", // Set the desired font size
                        fontWeight: "bold" // Set the desired font weight
                    }
                    //"Rs "+mapValues[1]+"/month" //count.toString(),
                });
                count += 1;
            } else {
                console.error('Geocoding failed for address: ' + address);
            }
        });
    });
}