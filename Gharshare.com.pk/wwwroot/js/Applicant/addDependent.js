$(document).ready(function () {
    let dependents = document.querySelectorAll('div.k').length;
    var count = dependents+1;
    var dIndex = dependents;
    $('#btnaddDependent').click(function (e) {
        if (count <= 8) {
            dependents = document.querySelectorAll('div.k').length;  
              count = dependents + 1;
              dIndex = dependents;
            var newDependent = '<div id=D' + count + ' class="k"><hr><strong class="kk">Dependent-' + count + '</strong>';
            var html = newDependent + '<div class="ad"><div class="row">';
            html = html + '<div class="col-md-4 form-floating p-1">';
            //html = html + '<input type="hidden" name=ApplicantDependents[' + dIndex + '].ID asp-for=@Model.ApplicantDependents[' + dIndex + '].ID >';
            //html = html + '<input type="hidden" name=ApplicantDependents[' + dIndex + '].ApplicantID asp-for=@Model.ApplicantDependents[' + dIndex + '].ApplicantID >';
            html = html + '<input type="text" name=ApplicantDependents[' + dIndex + '].FirstName asp-for=@Model.ApplicantDependents[' + dIndex + '].FirstName placeholder="First Name" oninput="this.className = """/>';
            html = html + '<span data-valmsg-for=@Model.ApplicantDependents[' + dIndex + '].FirstName class="text-danger"></span >';
            html = html + '</div>';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantDependents[' + dIndex + '].LastName asp-for=@Model.Dependents[' + dIndex + '].LastName placeholder="Last Name" oninput="this.className = """/>'
            html = html + '</div>';
            html = html + '<div class="col-md-2 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantDependents[' + dIndex + '].Age asp-for=@Model.ApplicantDependents[' + dIndex + '].Age placeholder="Age" oninput="this.className = """/>';
            html = html + '</div>';
            html = html + '<div class="col-md-2 form-floating p-1">';
            html = html + '<select name=' + 'ApplicantDependents[' + dIndex + '].Gender asp-for=@Model.ApplicantDependents[' + dIndex + '].Gender class="dropdown dl">';
            html = html + '<option selected value="">--Select Gender--</option>';
            html = html + '<option value="M">Male</option>';
            html = html + '<option value="F">Female</option>';
            html = html + '<option value="US">Unspecified</option>';
            html = html + '</select>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantDependents[' + dIndex + '].Relationship asp-for=@Model.ApplicantDependents[' + dIndex + '].Relationship placeholder="Relationship" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=@Model.ApplicantDependents[' + dIndex + '].Relationship class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<select name=' + 'ApplicantDependents[' + dIndex + '].IsLivingWithYou asp-for=@Model.ApplicantDependents[' + dIndex + '].IsLivingWithYou class="dropdown dl">';
            html = html + '<option selected value="">--Select Living Status--</option>';
            html = html + '<option value="Yes">Living with me</option>';
            html = html + '<option value="No" > Not living with me</option>';
            html = html + '</select>';
            html = html + '<span data-valmsg-for=@Model.ApplicantDependents[' + dIndex + '].IsLivingWithYou class="text-danger" ></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<select name=' + 'ApplicantDependents[' + dIndex + '].IsReceivingChildSupport asp-for=@Model.ApplicantDependents[' + dIndex + '].IsReceivingChildSupport class="dropdown dl">';
            html = html + '<option selected value="">--Receiving child support for? Select--</option>';
            html = html + '<option value="Yes">Yes</option>';
            html = html + '<option value="No">No</option>';
            html = html + '</select>';
            html = html + '<span data-valmsg-for=@Model.ApplicantDependents[' + dIndex + '].IsReceivingChildSupport class="text-danger" ></span>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">';
            html = html + '<div class="col-md-12 form-floating p-1">';
            html = html + '<textarea name=ApplicantDependents[' + dIndex + '].AdditionalInformation type="text" asp-for=@Model.ApplicantDependents[' + dIndex + '].AdditionalInformation placeholder="Additional Information" oninput="this.className = """></textarea>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">';
            html = html + '<div class="col-md-12 text-end">';
            html = html + '<button class="btn btn-danger delete"><i class="bi bi-trash"></i> Remove</button>';
            html = html + '</div>';
            html = html + '</div></div</div>';

            $('#dvappendDependent').append(html);
            ++count;
            ++dIndex;
        }
        else {
            var dependentNo = count - 1;
            e.preventDefault();
            Swal.fire({
                position: 'top-end',
                icon: 'error',
                title: 'You can not add more than ' + dependentNo + ' dependents',
                showConfirmButton: false,
                timer: 3000
            })

        }
        return false;
    });
    $('#dvappendDependent').on('click', '.delete', function () {
        $(this).closest('.k').remove();        
        dependents = document.querySelectorAll('div.k').length;
         count = dependents + 1;
         dIndex = dependents;
        $('.k').each(function (index, elem) {
            var id = 'k' + ++index;
            $(this).attr('id', id);
        });
        // Get rows with class 'ad'. UI for each dependet is rapped inside div with psudo class 
        //ad
         
        $('.ad').each(function (index, elem) {
           
            $(elem).children().each(function (i, elmnt) // Get all divs in row
            {
                $(elmnt).children().each(function (i, elm) // Get all divs in row
            {  
                // loop through each element in div
                    $(elm).children('select,input,span,textarea').each(function (j, elem) {
                        //console.log(elem.nodeName);
                        //console.log(elem.name);
                   var name;
                        if (elem.nodeName == 'INPUT' || elem.nodeName == 'SELECT' || elem.nodeName == 'TEXTAREA') {
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
                        //Split name of each element i.e. ApplicantVehicles[0].Registration
                        var part1 = setname.split("[");
                        // split second element of p1 array to remove old index e.g.[0]
                        var part2 = part1[1].split(".");
                        //form updated name of control, p2[1] contains part e.g. Registration
                        var Newname = part1[0] + "[" + index + "]." + part2[1];
                        var id = part1[0] + "_" + index + "_" + part2[1];
                        $(elem).attr('data-valmsg-for', Newname);
                    }
                });
            });
            });// 2nd loop to end here
        });
        //Get all divs with class k that contain <strong> used for dependent no
        $('.k').children('strong').each(function (index, elem) {
            elem.innerHTML = 'Dependent-' + ++index;
            count = index + 1;
            dIndex = index;
        });
        return false;
    });
});