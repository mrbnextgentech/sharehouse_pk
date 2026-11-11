$(document).ready(function () {
    var count = 2;
    var vals = { "c1": "rdoNeedparking", "c2": "addVehicle" };
    jQuery.each(vals, function (i, val) {
        $("#" + val).click(function (e) {
            if (count == 1 && val == "rdoNeedparking") {

                //++count;
            }
            else if (count <= 4 && val == "addVehicle") {
                var newVehicle = '<div id=D' + count + ' class="k"><hr><strong class="kk">Vehicle-' + count + '</strong>';
                var html = newVehicle + '<div class="row">';
                html = html + '<div class="col-md-3 form-floating p-1">';
                html = html + '<select asp-for=@Model.ApplicantVehicle.Type class="dropdown dl">';
                html = html + '<option selected value="">--Select Type of Vehicle--</option>';
                html = html + '<option value="Other">Other</option>';
                html = html + '<option value="Car">Car</option>';
                html = html + '<option value="Truck">Truck</option>';
                html = html + '<option value="Caravan">Caravan</option>';
                html = html + '<option value="Motorbike">Motorbike</option>';
                html = html + '<option value="Boat">Boat</option>';
                html = html + '</select>';
                html = html + '<span asp-validation-for=@Model.ApplicantVehicle.Type class="text-danger"></span>';
                html = html + '</div>';
                html = html + '<div class="col-md-3 form-floating p-1">';
                html = html + '<input type = "text" asp -for=@Model.ApplicantVehicle.Registration placeholder = "Registration" oninput = "this.className = """ />';
                html = html + '<span asp-validation-for=@Model.ApplicantVehicle.Registration class="text-danger"></span>';
                html = html + '</div>';
                html = html + '<div class="col-md-3 form-floating p-1">';
                html = html + '<input type="text" asp-for=@Model.ApplicantVehicle.Make placeholder="Make" oninput="this.className = """/>';
                html = html + '<span asp-validation-for=@Model.ApplicantVehicle.Make class="text-danger"></span>';
                html = html + '</div>';
                html = html + '<div class="col-md-3 form-floating p-1">';
                html = html + '<input type="text" asp-for=@Model.ApplicantVehicle.Model placeholder="Model" oninput="this.className = """ />';
                html = html + '<span asp-validation-for=@Model.ApplicantVehicle.Model class="text-danger"></span>';
                html = html + '</div>';
                html = html + '</div>';
                html = html + '<div class="row">';
                html = html + '<div class="col-md-12 form-floating p-1 text-end">';
                html = html + '<button class="btn btn-danger delete"><i class="bi bi-trash"></i> Remove Vehicle</button>';
                html = html + '</div>';
                $('#dvappendVehicles').append(html);
                ++count;
            }
            else if (count > 4 && val == "addVehicle") {
                var totalveh = count - 1;
                e.preventDefault();
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'You can not add more than ' + totalveh + ' vehicles',
                    showConfirmButton: false,
                    timer: 3000
                })
            }
            return false;
        });
    });
    $('#dvappendVehicles').on('click', '.delete', function () {
        $(this).closest('.k').remove();
        count = 2;
        $('.k').each(function (index, elem) {
            var id = 'D' + ++index;
            $(this).attr('id', id);
        });
        var vehno = 2;
        $('.k').children('strong').each(function (index, elem) {
            vehno = vehno + index;
            elem.innerHTML = 'Vehicle-' + vehno;
            count = index + 1;
        });
        return false;
    });
});