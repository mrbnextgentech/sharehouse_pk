function createContentlist(data) {
    var contentListDiv = document.getElementById("contentList");
    contentListDiv.innerHTML = ''; // Clear existing content     
    if (data.length > 0) {
        $('#response').empty();
        var tableHTML = `
        <table class="table table-responsive-md">
            <thead>
                <tr>
                    <th style="width: 50px;"><strong>No</strong></th>
                    <th><strong> 
                     Heading
                    </strong></th>
                    <th><strong>                     
                     TYPE
                    </strong></th>
                    <th><strong>Created On</strong></th>
                    <th><strong>Created By</strong></th>                     
                    <th><strong>Status</strong></th>
                    <th><strong>Action</strong></th>
                </tr>
            </thead>
            <tbody>
    `;

        for (var i = 0; i < data.length; i++) {
            var content = data[i];
            var statusIconClass = getStatusClass(content.status);
            var statusText = getStatusText(content.status);

            tableHTML += `
            <tr>
                <td>${i + 1}</td>
                <td>
                <div>             
             <span class="w-space-no">${content.heading}</span>
             </div>  
                </td>
                <td>${getContentTypeText(content.contentType)}</td>
                <td>${content.createdOn ? formatDateToDDMMYY(content.createdOn) : 'N/A'}</td>
                <td>${content.createdbyUser.name}</td>                 
                <td>
                    <div class="d-flex align-items-center">
                        <i class="fa fa-circle ${statusIconClass} me-1"></i> ${statusText}
                    </div>
                </td>
                <td>
                    <div class="d-flex">
                        <a title="Edit content" href="/admin/cms/edit/${content.id}" class="edit btn btn-primary shadow btn-xs sharp me-1"><i class="fa fa-pen"></i></a>
                        <a title="Delete content" href="/admin/cms/delete/${content.id}" class="btn btn-danger shadow btn-xs sharp"><i class="fa fa-trash"></i></a>
                    </div>
                </td>
            </tr>
        `;
        }

        tableHTML += `
            </tbody>
        </table>
    `;

        contentListDiv.innerHTML = tableHTML;

    }
    else {
        $('#contentList').empty();
        $('#response').html('<h4 class="text-center m-5">No record found</h4>');
        $('#response').show();
    }

}

function getStatusText(status) {
    if (status === 1)
        return "Active";
    else if (status === 2)
        return "Draft";
    else
        return "Canceled";
}

function getStatusClass(status) {
    if (status === 1)
        return "text-success";
    else if (status === 2)
        return "text-warning";
    else
        return "text-danger";
}
function getContentTypeText(value) {
    var contentType = '';
    switch (value) {         
        case 1:
            contentType = 'Article';
            break;
        case 2:
            contentType = 'How to guide';
            break;
        case 3:
            contentType = 'Legal guide';
            break;
        case 4:
            contentType = 'About us';
            break;
    }
    return contentType;
}
