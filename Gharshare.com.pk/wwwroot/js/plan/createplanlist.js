function createplanlist(data) {
    var planListDiv = document.getElementById("planlist");
    planListDiv.innerHTML = ''; // Clear existing content
    if (data.length > 0) {
        $('#message').hide();
        $('#noplan').hide();
        var tableHTML = `
        <table class="table table-responsive-md">
            <thead>
                <tr>
                    <th style="width: 50px;"><strong>No</strong></th>
                    <th><strong>Plan Name</strong></th>
                    <th><strong>Price</strong></th>
                    <th><strong>For</strong></th>
                    <th><strong>Status</strong></th>
                    <th><strong>Action</strong></th>
                </tr>
            </thead>
            <tbody>
    `;

        for (var i = 0; i < data.length; i++) {
            var plan = data[i];
            var statusIconClass = getStatusClass(plan.planStatus);
            var statusText = getStatusText(plan.planStatus);

            var forText = plan.planType === 1 ? 'For Landlord' :
                plan.planType === 2 ? 'For Tenant' : 'For both';

            tableHTML += `
            <tr>
                <td>${i + 1}</td>
                <td>
                    <div class="d-flex align-items-center">
                        <span class="w-space-no">${plan.title}</span>
                    </div>
                </td>
                <td>${formatCurrency(plan.price)}</td>
                <td>${forText}</td>
                <td>
                    <div class="d-flex align-items-center">
                        <i class="fa fa-circle ${statusIconClass} me-1"></i> ${statusText}
                    </div>
                </td>
                <td>
                    <div class="d-flex">
                        <a href="#" data-planid=${plan.id}  data-bs-toggle="modal" data-bs-target="#largeModal" class="edit btn btn-primary shadow btn-xs sharp me-1"><i class="fa fa-pen"></i></a>
                        <a href="#" onclick="loadviewAddservice2Plan(${plan.id})" data-bs-toggle="modal" data-bs-target="#largeModal"
                                            title="add service to plan" class="btn btn-danger shadow btn-xs sharp me-1"><i class="fa fa-plus"></i></a>
                        <a href="#" data-planid="${plan.id}" data-bs-toggle="modal" data-bs-target="#largeModal"
                        class="delete btn btn-danger shadow btn-xs sharp" title="Delete Plan"><i class="fa fa-trash"></i></a>
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
        $('#noplan').hide();
    }
}

function getStatusText(planStatus) {
    if (planStatus === 1) {
        return "Active";
    } else if (planStatus === 2) {
        return "Pending";
    } else if (planStatus === 3) {
        return "Canceled";
    } else {
        return "Unknown"; // Default class or handle other cases as needed
    }
}

function getStatusClass(planStatus) {
    if (planStatus === 1) {
        return "text-success";
    } else if (planStatus === 2) {
        return "text-warning";
    } else if (planStatus === 3) {
        return "text-danger";
    } else {
        return ""; // Default class or handle other cases as needed
    }
}

function formatCurrency(value) {
    if (typeof value === 'number') {
        return value.toLocaleString('en-PK', { style: 'currency', currency: 'PKR', minimumFractionDigits: 0, maximumFractionDigits: 0 });
    } else {
        return '';
    }
}