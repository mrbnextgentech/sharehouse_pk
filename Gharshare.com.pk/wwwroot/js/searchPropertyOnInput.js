$(document).ready(function () {    
    $('#txtsearch,#txtlistviewsearch').on('input', function () {         
        var searchPhrase = $(this).val();

        if (this.id === "txtsearch") {            
            $('#txtlistviewsearch').val(searchPhrase);
        } else {
            $('#txtsearch').val(searchPhrase);
        }
        //debugger;
        $.ajax({
            url: '/Property/searchonInput?searchPhrase=' + searchPhrase,
            success: function (result) {                 
                $('#sort').empty();
                $('#sort').addClass('sort'); 
                $('#list').empty();
                $.each(result, function (i, data) {   
                    //debugger;
                    if (data != null) {
                        $('#message2').empty();
                        var html = '<div class="col-xl-12 col-sm-6 col-12">' +
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
                         $('#list').append(html2); // append the new HTML to the 'sort' div
                        //------------List View Starts---------------//
                    }

                    else {
                        debugger;
                        var msg = "no record found";
                        $('#message2').append(msg);
                    }
                });
                
            }
        });
    });
});