$(document).ready(function () {
    let vehiclecount = document.querySelectorAll('div.avc').length;
    var vcount = vehiclecount +1;
    var vIndex = vehiclecount;
    $('#addVehicle').click(function (e) {
        
        if (vcount <= 4) {
            vehiclecount = document.querySelectorAll('div.avc').length;
            vcount = vehiclecount + 1;
            vIndex = vehiclecount;
            //console.log(vcount);
            var newVehicle = '<div id=D' + vcount + ' class="v"><hr><strong class="vv">Vehicle-' + vcount + '</strong>';
            var html = newVehicle + '<div class="avc row">';
            html = html + '<div class="col-md-3 form-floating p-1">';
            html = html + '<select name=' + 'ApplicantVehicles[' + vIndex + '].Type asp-for=@Model.ApplicantVehicles[' + vIndex + '].Type class="dropdown dl">';
            html = html + '<option selected value="">--Select Type of Vehicle--</option>';
            html = html + '<option value="Other">Other</option>';
            html = html + '<option value="Car">Car</option>';
            html = html + '<option value="Truck">Truck</option>';
            html = html + '<option value="Caravan">Caravan</option>';
            html = html + '<option value="Motorbike">Motorbike</option>';
            html = html + '<option value="Boat">Boat</option>';
            html = html + '</select>';
            html = html + '<span data-valmsg-for=ApplicantVehicles[' + vIndex + '].Type class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-3 form-floating p-1">';
            html = html + '<input name=' + 'ApplicantVehicles[' + vIndex + '].Registration  type = "text" asp-for=@Model.ApplicantVehicles[' + vIndex + '].Registration placeholder = "Registration" oninput = "this.className = """ />';
            html = html + '<span data-valmsg-for=ApplicantVehicles[' + vIndex + '].Registration class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-3 form-floating p-1">';
            html = html + '<input name=' + 'ApplicantVehicles[' + vIndex + '].Make type="text" asp-for=@Model.ApplicantVehicles[' + vIndex + '].Make placeholder="Make" oninput="this.className = """/>';
            html = html + '<span data-valmsg-for=ApplicantVehicles[' + vIndex + '].Make class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-3 form-floating p-1">';
            html = html + '<input name=' + 'ApplicantVehicles[' + vIndex + '].Model type="text" asp-for=@Model.ApplicantVehicles[' + vIndex + '].Model placeholder="Model" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=ApplicantVehicles[' + vIndex + '].Model class="text-danger"></span>';
            html = html + '</div>';
            //html = html + '</div>';
            html = html + '<div class="p-1 btn-group">';
            html = html + '<input data-val="true" type="checkbox"';
            html = html + 'name=ApplicantVehicles[' + vIndex + '].NeedParking ';
            html = html + 'value="true"/>';
            html = html + '<div class="col-md-12 p-1">';
            html = html + '<strong>Will you be parking the motor vehicle on the property?</strong>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">';
            html = html + '<div class="col-md-12 form-floating p-1 text-end">';
            html = html + '<button class="btn btn-danger delete"><i class="bi bi-trash"></i> Remove</button>';
            html = html + '</div>';
            $('#dvappendVehicles').append(html);
            ++vcount;
            ++vIndex;
        }
        else {
            var totalveh = vcount - 1;
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
    $('#dvappendVehicles').on('click', '.delete', function () {
        $(this).closest('.v').remove();
        vehiclecount = document.querySelectorAll('div.avc').length;
        vcount = vehiclecount + 1;
        vIndex = vehiclecount;
        $('.v').each(function (index, elem) {
            var id = 'v' + ++index;
            $(this).attr('id', id);
        });
        // Get rows with class 'avc'. UI for each vehicle is rapped inside div with psudo class avc
        $('.avc').each(function (index, elem) { 

            $(elem).children().each(function (i, elm) // Get all divs in row
            {
                // loop through each element in div
                $(elm).children('select,input,span').each(function (j, elem) {
                    var name;
                    if (elem.nodeName == 'INPUT' || elem.nodeName == 'SELECT') {
                        console.log(elem.nodeName);
                        //Split name of each element i.e. ApplicantVehicles[0].Registration
                        var p1 = elem.name.split("[");
                        // split second element of p1 array to remove old index e.g.[0]
                        var p2 = p1[1].split(".");
                        //form updated name of control, p2[1] contains part e.g. Registration
                         name = p1[0] + "[" + index + "]." + p2[1];
                        var id = p1[0] + "_" + index + "_" + p2[1];
                        $(elem).attr('name', name);
                        $(elem).attr('asp-for', "@Model" + name);
                        $(elem).attr('id', id);
                    }
                    if (elem.nodeName == 'SPAN') {
                        var attr = elem.attributes;
                        var setname = attr['data-valmsg-for'].value;
                       //debugger;
                        //console.log(name);
                        //Split name of each element i.e. ApplicantVehicles[0].Registration
                        var part1 = setname.split("[");
                        // split second element of p1 array to remove old index e.g.[0]
                        var part2 = part1[1].split(".");
                        //form updated name of control, p2[1] contains part e.g. Registration
                        var Newname = part1[0] + "[" + index + "]." + part2[1];
                        var id = part1[0] + "_" + index + "_" + part2[1];
                       // $(elem).attr('name', Newname);
                        //$(elem).attr('id', id);
                        $(elem).attr('data-valmsg-for', Newname);
                        //console.log(Newname);
                        //console.log(id);
                    }
                });
            });
        });
        var vehno = 2;
        //Get all divs with class v that contain <strong> used for vehicle no
        $('.v').children('strong').each(function (index, elem) {
            if (index == 0) {
                elem.innerHTML = 'Primary Vehicle';
                ++index;
            }
            else {
                elem.innerHTML = 'Vehicle-' + ++index;
                //vcount = index + 1;
            }
            vcount = index + 1;
            vIndex = index;
        });
        return false;
    });
});