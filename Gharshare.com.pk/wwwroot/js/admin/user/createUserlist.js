function createUserlist(data) {
    var userListDiv = document.getElementById("userList");
    userListDiv.innerHTML = ''; // Clear existing content     
    if (data.length > 0) {
        $('#response').empty();
        var tableHTML = `
        <table class="table table-responsive-md">
            <thead>
                <tr>
                    <th style="width: 50px;"><strong>No</strong></th>
                    <th><strong><i></i>
                    <a href="/admin/user/index?pg=1&amp;sortOrder=name">
                     NAME
                    </a></strong></th>
                    <th><strong> 
                    <i class="fa fa-arrow-down"></i>
                    <a href="/Admin/User/index?pg=1&amp;sortOrder=usertype_desc">
                     TYPE
                    </a></strong></th>
                    <th><strong>PHONE</strong></th>
                    <th><strong>EMAIL</strong></th>
                    <th><strong><i></i>
                    <a href="/Admin/User/index?pg=1&amp;sortOrder=address">ADDRESS</a></strong></th>
                    <th><strong>Status</strong></th>
                    <th><strong>Action</strong></th>
                </tr>
            </thead>
            <tbody>
    `;

        for (var i = 0; i < data.length; i++) {
            var user = data[i];
            var statusIconClass = getStatusClass(user.isActive);
            var statusText = getStatusText(user.isActive);

            var status = user.isActive === true ? 'Active' : 'Inactive';

            tableHTML += `
            <tr>
                <td>${i + 1}</td>
                <td>
                <div>
             <img src="${user.profilePicture ? user.profilePicture:'../img/user-image.png'}" class="rounded-lg me-2" width="24" alt="">
             <span class="w-space-no">${user.name}</span>
             </div>  
                </td>
                <td>${getUserTypeText(user.userType)}</td>
                <td>${user.phoneNumber ? user.phoneNumber : 'N/A'}</td>
                <td>${user.email}</td>
                <td>${user.address ? user.address : 'N/A'}</td>
                <td>
                    <div class="d-flex align-items-center">
                        <i class="fa fa-circle ${statusIconClass} me-1"></i> ${statusText}
                    </div>
                </td>
                <td>
                    <div class="d-flex">
                        <a href="#" title="Edit user" onclick="loadUserProfile('${user.id}')" data-bs-toggle="modal" data-bs-target="#largeModal" class="edit btn btn-primary shadow btn-xs sharp me-1"><i class="fa fa-pen"></i></a>
                        <a title="Delete user" href="#" class="btn btn-danger shadow btn-xs sharp" onclick="loadHardelUserInPopup('${user.id}')" data-bs-toggle="modal" data-bs-target="#largeModal"><i class="fa fa-trash"></i></a>
                        &nbsp; <a title="Deactivate user" href="#" class="btn btn-danger shadow btn-xs sharp"
   ${user.userType == 1
                    ? `onclick="loadUserProfile('${user.id}')"`
                    : `onclick="loadDeactivateAccountInPopup('${user.id}')"`}
   data-bs-toggle="modal" data-bs-target="#largeModal">
   <i class="fa fa-ban"></i>
</a>

                    </div>
                </td>
            </tr>
        `;
        }

        tableHTML += `
            </tbody>
        </table>
    `;

        userListDiv.innerHTML = tableHTML;

    }
    else {
        $('#userList').empty();
        $('#response').html('<h4 class="text-center m-5">No user found</h4>');
        //$('#response').show();
    }

}

function getStatusText(status) {
    if (status === true)
        return "Active";
    else
        return "Inactive";
}

function getStatusClass(status) {
    if (status === true)
        return "text-success";
    else
        return "text-danger";
}
function getUserTypeText(value) {
    var userType = '';
    switch (value) {
        case 5:
            userType = 'Super Admin';
            break;
        case 1:
            userType = 'Admin';
            break;
        case 2:
            userType = 'Free User';
            break;
        case 3:
            userType = 'Landlord';
            break;
        case 4:
            userType = 'Tenant';
            break;
    }
    return userType;
}
