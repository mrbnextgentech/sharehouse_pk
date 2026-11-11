$(document).ready(function () {
    let referenceCount= document.querySelectorAll('div.ar').length;
    var rcount = referenceCount+1;
    var rIndex = referenceCount;
    $('#btnaddReference').click(function (e) {
        referenceCount = document.querySelectorAll('div.ar').length;
        rcount = referenceCount + 1;
        rIndex = referenceCount;
        if (rcount <= 3) {
            var newRef = '<div id=r' + rcount + ' class="r"><hr><strong class="rr">Reference-' + rcount + '</strong>';
            var html = newRef + '<div class="ar"><div class="row">';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<select name=' + 'ApplicantReference[' + rIndex + '].Type asp-for=@Model.ApplicantReference[' + rIndex + '].Type class="dropdown dl">';
            html = html + '<option selected value="">--Select type--</option>';
            html = html + '<option value="Reference">Reference</option>';
            html = html + '<option value="Emergency contact">Emergency contact</option>';
            html = html + '</select>';
            html = html + '<span data-valmsg-for=@Model.ApplicantReference[' + rIndex + '].Type class="text-danger"></span>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantReference[' + rIndex + '].FirstName asp-for=@Model.ApplicantReference[' + rIndex + '].FirstName placeholder="First Name" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=@Model.ApplicantReference[' + rIndex + '].FirstName class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" name="ApplicantReference[' + rIndex + '].LastName" asp-for=@Model.ApplicantReference[i].LastName placeholder="Last Name" oninput="this.className = """/>';
            //html = html + '<span data-valmsg-for=@Model.ApplicantReference[' + rIndex + '].StreetNo class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" name="ApplicantReference[' + rIndex + '].Relationship" asp-for=@Model.ApplicantReference[i].Relationship placeholder="Relationship" oninput="this.className = """/>';
            //html = html + '<span data-valmsg-for=@Model.ApplicantReference[' + rIndex + '].LastName class="text-danger"></span>';
            html = html + '</div>';
            html = html + '</div>';            
            html = html + '<div class="row">';            
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantReference[' + rIndex + '].Email asp-for=@Model.ApplicantReference[' + rIndex + '].Email placeholder="Email" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=@Model.ApplicantReference[' + rIndex + '].Email class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantReference[' + rIndex + '].Mobile asp-for=@Model.ApplicantReference[' + rIndex + '].Mobile placeholder="Mobile" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=@Model.ApplicantReference[' + rIndex + '].Mobile class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantReference[' + rIndex + '].Phone  asp-for=@Model.ApplicantReference[' + rIndex + '].Phone  placeholder="Phone" oninput="this.className = """ />';
           // html = html + '<span data-valmsg-for=@Model.ApplicantReference[' + rIndex + '].Phone class="text-danger"></span>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">';            
            html = html + '<div class="col-md-12 form-floating p-1">';
            html = html + '<textarea name=ApplicantReference[' + rIndex + '].AdditionalInformation type="text" asp-for=@Model.ApplicantReference[' + rIndex + '].AdditionalInformation placeholder="Additional Information" oninput="this.className = """></textarea>';
            html = html + '</div>';
            html = html + '</div>';            
            html = html + '</div>';
            html = html + '<div class="row">';
            html = html + '<div class="col-md-12 form-floating p-1 text-end">';
            html = html + '<button class="btn btn-danger delete"><i class="bi bi-trash"></i> Remove</button>';
            html = html + '</div>';
            $('#dvappendReference').append(html);
            ++rcount;
            ++rIndex;
        }
        else {
            var referenceNo = rcount - 1;
            e.preventDefault();
            Swal.fire({
                position: 'top-end',
                icon: 'error',
                title: 'You can not add more than ' + referenceNo + ' references',
                showConfirmButton: false,
                timer: 3000
            })

        }
        

        return false;

    });
    $('#dvappendReference').on('click', '.delete', function () {
        $(this).closest('.r').remove();
        referenceCount = document.querySelectorAll('div.ar').length;
        rcount = referenceCount + 1;
        rIndex = referenceCount;
        $('.r').each(function (index, elem) {
            var id = 'r' + ++index;
            $(this).attr('id', id);
        });
        // Get rows with class 'avc'. UI for each vehicle is rapped inside div with psudo class avc
        $('.ar').each(function (index, elem) {

            $(elem).children().each(function (i, elmnt) // Get all divs in row
            {
                $(elmnt).children().each(function (i, elm) // Get all html elements in row
                {
                    // console.log(elm.nodeName);
                    // loop through each element in div
                    $(elm).children('select,input,span,textarea').each(function (j, elem) {

                        var name;
                        if (elem.nodeName == 'INPUT' || elem.nodeName == 'SELECT' || elem.nodeName == 'TEXTAREA') {
                            //console.log(elem.nodeName);
                            //console.log(elem.name);
                            // debugger;
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
                           // debugger;
                            var setname = attr['data-valmsg-for'].value;
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
                        }
                    });
                });
            });// 2nd loop to end here
        });
        //Get all divs with class p that contain <strong> used for vehicle no
        $('.r').children('strong').each(function (index, elem) {
            elem.innerHTML = 'Reference-' + ++index;
            rcount = index + 1;
            rIndex = index;
        });
        return false;
    });
});