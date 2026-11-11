function getPlaceListings(currentPage, totalPages, fromRec, toRec, totalItems, startPage, endPage, pageSize,
    sortOrderNo,search, event,atag) {
    //debugger;
    var funcName = atag.getAttribute('data-functionName');
    event.preventDefault();
    //ajax call
    $.ajax({
        url: '/home/GetPlaceListings?pg=' + currentPage,
        type: 'GET',
        success: function (result) {
            //debugger;
            $('#divPropertyListing').empty();
            $('#paging').empty();
            createPropertyList(result.placeListings, result.isLoggedIn, result.userId);
            var itemCount = result.placeListings.length;
            fromRec = ((currentPage - 1) * pageSize) + 1;
            toRec = ((currentPage - 1) * pageSize) + itemCount;            
            recreatePaging(currentPage, totalPages, fromRec, toRec, totalItems, startPage, endPage, pageSize,
                 sortOrderNo,funcName, search, event);

            //window.scrollTo({
            //    top: 4100,
            //    behavior: 'smooth' // Smooth scrolling behavior
            //});
        },
        error: function (xhr, status, error) {
            // Handle error
            debugger;
            //console.log(xhr);
        }
    });
    //end of ajax call

};