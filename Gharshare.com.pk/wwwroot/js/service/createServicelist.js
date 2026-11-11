function createServicelist(data) {
    var planListDiv = document.getElementById("servicelist");
    planListDiv.innerHTML = ''; // Clear existing content
    if (data.length > 0) {
        $('#message').hide();
        $('#noservice').hide();
        var tableHTML = `
        <table class="table table-responsive-md">
            <thead>
                <tr>
                    <th style="width: 50px;"><strong>No</strong></th>
                    <th><strong>Service  Name</strong></th>
                    <th><strong>Description</strong></th>                     
                    <th><strong>Status</strong></th>
                    <th><strong>Action</strong></th>
                </tr>
            </thead>
            <tbody>
    `;

        for (var i = 0; i < data.length; i++) {
            var service = data[i];
            var statusIconClass = getStatusClass(service.status);
            var statusText = getStatusText(service.status);             

            tableHTML += `
            <tr>
                <td>${i + 1}</td>
                <td>
                    <div class="d-flex align-items-center">
                        <span class="w-space-no">${service.title}</span>
                    </div>
                </td>
                <td>${service.description}</td>                 
                <td>
                    <div class="d-flex align-items-center">
                        <i class="fa fa-circle ${statusIconClass} me-1"></i> ${statusText}
                    </div>
                </td>
                <td>
                    <div class="d-flex">
                        <a href="#" href="#" title="Edit service" onclick="loadservice2edit(${service.id})" data-bs-toggle="modal" data-bs-target="#largeModal" class="edit btn btn-primary shadow btn-xs sharp me-1"><i class="fa fa-pen"></i></a>                         
                        <a href="#" title="Delete service" onclick="loadservice2delete(${service.id})" data-bs-toggle="modal" data-bs-target="#largeModal" class="btn btn-danger shadow btn-xs sharp"><i class="fa fa-trash"></i></a>
                    </div>
                </td>
            </tr>
        `;
        }

        tableHTML += `
            </tbody>
        </table>
    `;

        planListDiv.innerHTML = tableHTML;
    }
    else {
        $('#message').show();
        $('#noservice').hide();
    }
}

function getStatusText(status) {
    if (status === 1) {
        return "Active";
    } else if (status === 2) {
        return "Pending";
    } else if (status === 3) {
        return "Canceled";
    } else {
        return "Unknown"; // Default class or handle other cases as needed
    }
}

function getStatusClass(status) {
    if (status === 1) {
        return "text-success";
    } else if (status === 2) {
        return "text-warning";
    } else if (status === 3) {
        return "text-danger";
    } else {
        return ""; // Default class or handle other cases as needed
    }
}