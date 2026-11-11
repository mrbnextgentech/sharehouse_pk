function getPersonListings(currentPage, totalPages, fromRec, toRec, totalItems, startPage, endPage, pageSize,
    sortOrderNo,search, event,atag) {
    //debugger;
    var funcName = atag.getAttribute('data-functionName');
    event.preventDefault();
    //ajax call
    $.ajax({
        url: '/home/GetPersonListings?pg=' + currentPage,
        type: 'GET',
        success: function (result) {
            //debugger;
            $('#divPersonListing').empty();
            $('#paging-2').empty();
            createPersonList(result.personListings, result.isLoggedIn, result.userId);
            var itemCount = result.personListings.length;
            fromRec = ((currentPage - 1) * pageSize) + 1;
            toRec = ((currentPage - 1) * pageSize) + itemCount;            
            recreatePaging2(currentPage, totalPages, fromRec, toRec, totalItems, startPage, endPage, pageSize,
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