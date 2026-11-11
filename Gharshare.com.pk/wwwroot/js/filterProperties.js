$(document).ready(function () {
    $('input[type="checkbox"]').click(function () {
        var selection = {
            Type: [],
            Feature: [],
            Rent: [],
            Bed: []
        };
        // Uncheck single, double and triple bed if option 5 or More bed is checked
        if ($(this).attr('id') == 'chk5bed' && $(this).is(':checked')) {
            $('.not5filter').prop('checked', false);
        }
        else if ($(this).attr('id') === 'single' || $(this).attr('id') === 'double'
            || $(this).attr('id') === 'triple') {
            $('#chk5bed').prop('checked', false);
        }

        // Collect selected checkbox values
        $('input[type="checkbox"]:checked').each(function () {
            if ($(this).hasClass('type')) {
                selection.Type.push($(this).val());
            } else if ($(this).hasClass('feature')) {
                selection.Feature.push($(this).val());
            } else if ($(this).hasClass('rent')) {
                selection.Rent.push($(this).val());
            } else if ($(this).hasClass('bed')) {
                selection.Bed.push($(this).val());
            }
        });
        
        console.log(selection);
        // Make AJAX call passing selected checkbox values
        $.ajax({
            url: '/property/filter',
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ selection: selection }), // pass an array with one element
            success: function (result) {
                console.log(result);
                $('#sort').empty();
                $('#sort').addClass('sort');
                $('#list').empty();
                var html;
                $.each(result, function (i, data) {
                    html = '<div class="col-xl-12 col-sm-6 col-12">' +
                        '<div class="ltn__product-item ltn__product-item-4 ltn__product-item-5 text-center---">' +
                        '<div class="product-img">' +
                        '<a href="/Property/detail/' + data.id + '"><img src="' + data.coverImage + '" alt="#"></a>' +
                        '</div>' +
                        '<div class="product-info">' +
                        '<div class="product-badge">' +
                        '<ul>' +
                        '<li class="sale-badg">For Rent</li>' +
                        '</ul>' +
                        '</div>' +
                        '<h2 class="product-title">' +
                        '<a href="/Property/detail/' + data.id + '">' + data.adTitle + '</a></h2>' +
                        '<div class="product-img-location">' +
                        '<ul>' +
                        '<li>' +
                        '<a href="locations.html"><i class="flaticon-pin"></i>' + data.address + '</a>' +
                        '</li>' +
                        '</ul>' +
                        '</div>' +
                        '<ul class="ltn__list-item-2--- ltn__list-item-2-before--- ltn__plot-brief">' +
                        '<li><span>' + data.bed + '</span> Bed</li>' +
                        '<li><span>' + data.bathroom + '</span> Bath</li>' +
                        '<li><span>' + data.totalArea + '</span> Square Ft</li>' +
                        '</ul>' +
                        '<div class="product-hover-action">' +
                        '<ul>' +
                        '<li><a href="#" title="Quick View" data-bs-toggle="modal" data-bs-target="#quick_view_modal"><i class="flaticon-expand"></i></a></li>' +
                        '<li><a href="#" title="Wishlist" data-bs-toggle="modal" data-bs-target="#liton_wishlist_modal"><i class="flaticon-heart-1"></i></a></li>' +
                        '<li><a href="/Property/detail/' + data.id + '" title="Property Details"><i class="flaticon-add"></i></a></li>' +
                        '</ul>' +
                        '</div>' +
                        '</div>' +
                        '<div class="product-info-bottom">' +
                        '<div class="product-price">' +
                        '<span>Rs ' + (data.rent ? data.rent.toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,') : "") + '<label>/Month</label></span>' +
                        '</div>' +
                        '</div>' +
                        '</div>' +
                        '</div>';
                    $('#sort').append(html); // append the new HTML to the 'sort' div
                    //------------Grid view ended-----------------//

                    //------------List View Starts---------------//
                    var html2 = '<div class="col-lg-12">' +
                        '<div class="ltn__product-item ltn__product-item-4 ltn__product-item-5">' +
                        '<div class="product-img">' +
                        '<a href="/Property/detail/' + data.id + '"><img src="' + data.coverImage + '" alt="#"></a>' +
                        '</div>' +
                        '<div class="product-info">' +
                        '<div class="product-badge-price">' +
                        '<div class="product-badge">' +
                        '<ul>' +
                        '<li class="sale-badg">For Rent</li>' +
                        '</ul>' +
                        '</div>' +
                        '<div class="product-price">' +
                        '<span>Rs ' + (data.rent ? data.rent.toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,') : "") + '<label>/Month</label></span>' +
                        '</div>' +
                        '</div>' +
                        '<h2 class="product-title">' +
                        '<a href="/Property/detail/' + data.id + '">' + data.adTitle + '</a>' +
                        '</h2>' +
                        '<div class="product-img-location">' +
                        //'<ul>' +
                        //'<li>' +
                        //'<a href="locations.html"><i class="flaticon-pin"></i> ' + data.location + '</a>' +
                        //'</li>' +
                        //'</ul>' +
                        '</div>' +
                        '<ul class="ltn__list-item-2--- ltn__list-item-2-before--- ltn__plot-brief">' +
                        '<li>' +
                        '<span>' + data.bed + '</span> Bed' +
                        '</li>' +
                        '<li>' +
                        '<span>' + data.bathroom + '</span> Bath' +
                        '</li>' +
                        '<li>' +
                        '<span>' + data.totalArea + '</span> Square Ft' +
                        '</li>' +
                        '</ul>' +
                        '</div>' +
                        '<div class="product-info-bottom">' +
                        '<div class="real-estate-agent">' +
                        '<div class="agent-img">' +
                        '<a href="/Property/detail/' + data.id + '"></a>' +
                        '</div>' +
                        //'<div class="agent-brief">' +
                        //'<h6><a href="#">' + data.ownerName + '</a></h6>' +
                        //'<small>Landlord</small>' +
                        //'</div>' +
                        '</div>' +
                        '<div class="product-hover-action">' +
                        '<ul>' +
                        '<li>' +
                        '<a href="#" title="Quick View" data-bs-toggle="modal" data-bs-target="#quick_view_modal">' +
                        '<i class="flaticon-expand"></i>' +
                        '</a>' +
                        '</li>' +
                        '<li>' +
                        '<a href="#" title="Wishlist" data-bs-toggle="modal" data-bs-target="#liton_wishlist_modal">' +
                        '<i class="flaticon-heart-1"></i>' +
                        '</a>' +
                        '</li>' +
                        '<li>' +
                        '<a href="/Property/detail/' + data.id + '" title="Property Details">' +
                        '<i class="flaticon-add"></i>' +
                        '</a>' +
                        '</li>' +
                        '</ul>' +
                        '</div>' +
                        '</div>' +
                        '</div>' +
                        '</div>';
                    $('#list').append(html2); // append the new HTML to the 'list' div
                    //------------List View Ends---------------//

                });
            } //success function ends
        });
    });
});
