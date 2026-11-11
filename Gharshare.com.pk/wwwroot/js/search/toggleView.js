function toggleView() {
    var btn = $('#btnShowmap');
    if (btn.text() === "Show Map") {
        $('#listingRow').hide()
        $('#mapRow').show()
        btn.text("Show Listings");
    } else {
        $('#listingRow').show()
        $('#mapRow').hide()
        btn.text("Show Map");
    }
}   