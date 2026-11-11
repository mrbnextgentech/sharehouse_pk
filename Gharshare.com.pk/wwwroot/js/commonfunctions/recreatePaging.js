function recreatePaging(currentPage, totalPages, fromRec, toRec, totalItems, startPage, endPage, pageSize,
    sortOrderNo,funcName, searchPhrase, event,uid,id) {
    var params = totalPages + "," + fromRec + "," + toRec + "," + totalItems + ","
        + startPage + "," + endPage + "," + pageSize + "," + sortOrderNo;

    if (searchPhrase == null || searchPhrase == "")
        params += ",null";
    else
        params += ",'" + searchPhrase + "'";

    var html = `<ul class="pagination pagination-circle justify-content-center">`;
    if (currentPage > 1) {
        html += `<li class="page-item">                    
                    <a data-id=${id} data-uid=${uid} data-functionName=${funcName} class=page-link me-2" href="#" title="First"
                    onclick="return eval(${funcName}(1,${params},event,this));">
            <i class="fas fa-angle-double-left" aria-hidden="true"></i></a>
                </li>`
        html += `<li class="page-item">
                    <a data-id=${id} data-uid=${uid} data-functionName=${funcName} title="Previous" class="page-link me-2" href="#"
                    onclick="return eval(${funcName}(${currentPage - 1},${params},event,this))">
                        <i class="fa fa-angle-left" aria-hidden="true"></i>
                    </a>
                </li>`
    }
    //loop to create pages between 1 and last
    for (var pge = startPage; pge <= endPage; pge++) {
        html += `<li class="page-item ${pge == currentPage ? "active" : ""}">
                    <a data-id=${id} data-uid=${uid} data-functionName=${funcName} class="page-link me-2" href="#"
                    onclick="return eval(${funcName}(${pge},${params},event,this))">
                        ${pge}
                    </a>
                </li>`
    }
    if (currentPage < totalPages) {
        html += `<li class="page-item">
                    <a title="Next" data-id=${id} data-uid=${uid} data-functionName=${funcName} class="page-link me-2" href="#"
                    onclick="return eval(${funcName}(${currentPage + 1},${params},event,this))">
                        <i class="fa fa-angle-right" aria-hidden="true"></i>
                    </a>
                </li>`
        html += `<li class="page-item">
                    <a title="Last" data-id=${id} data-uid=${uid} data-functionName=${funcName} class="page-link me-2" href="#"
                    onclick="return eval(${funcName}(${totalPages},${params},event,this))">
                        <i class="fas fa-angle-double-right"></i>
                    </a>
                </li>`
    }
    html += `</ul >`;
    if (fromRec != toRec) {
        html += `<p class="text-center mb-3" style="color:#FF005D">
            Viewing ${fromRec}-${toRec} of ${totalItems} records</p ></nav >`;
    }
    else {
        html += `<p class="text-center mb-3" style="color:#FF005D">
            Viewing ${fromRec} of ${totalItems} records</p ></nav >`;
    }
    $('#paging').append(html);
}