$(document).ready(function () {
    let empCount = document.querySelectorAll('div.ae').length;
    var ecount = empCount+1;
    var eIndex = empCount;
    $('#btnaddEmployer').click(function (e) {
       //console.log('Ecount=' + ecount);
        if (ecount <= 3) {
            empCount = document.querySelectorAll('div.ae').length;
            ecount = empCount;
            eIndex = empCount;
            if (ecount==0)
                var newemployer = '<div id=e' + ecount + ' class="e"><hr><strong class="ee">My Current Employer Details</strong>';
            else
                var newemployer = '<div id=e' + ecount + ' class="e"><hr><strong class="ee">Previous Employer-' + ecount + '</strong>';
            var html = newemployer + '<div class="ae"><div class="row">';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantEmployer[' + eIndex + '].Occupation asp-for=@Model.ApplicantEmployer[' + eIndex + '].Occupation placeholder="Occupation" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=@Model.ApplicantEmployer[' + eIndex + '].Occupation class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" name="ApplicantEmployer[' + eIndex + '].EmployerName" asp-for=@Model.ApplicantEmployer[' + eIndex + '].EmployerName placeholder="Employer Name" oninput="this.className = """/>';
            html = html + '<span data-valmsg-for=@Model.ApplicantEmployer[' + eIndex + '].EmployerName class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" name="ApplicantEmployer[' + eIndex + '].ContactName" asp-for=@Model.ApplicantEmployer[' + eIndex + '].ContactName placeholder="Contact Name" oninput="this.className = """/>';
            html = html + '<span data-valmsg-for=@Model.ApplicantEmployer[' + eIndex + '].ContactName class="text-danger"></span>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">';            
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantEmployer[' + eIndex + '].Email asp-for=@Model.ApplicantEmployer[' + eIndex + '].Email placeholder="Email" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=@Model.ApplicantEmployer[' + eIndex + '].Email class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantEmployer[' + eIndex + '].Mobile asp-for=@Model.ApplicantEmployer[' + eIndex + '].Mobile placeholder="Mobile" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=@Model.ApplicantEmployer[' + eIndex + '].Mobile class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantEmployer[' + eIndex + '].Phone  asp-for=@Model.ApplicantEmployer[' + eIndex + '].Phone  placeholder="Phone" oninput="this.className = """ />';
           // html = html + '<span data-valmsg-for=@Model.ApplicantEmployer[' + eIndex + '].Phone class="text-danger"></span>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">';
            html = html + '<div class="col-md-6 form-floating p-1">';
            html = html + '<input type = "text" name = ApplicantEmployer[' + eIndex + '].Website  asp-for=@Model.ApplicantEmployer[' + eIndex + '].Website  placeholder = "Website" oninput = "this.className = """ /> ';
            html = html + '</div>';
            html = html + '<div class="col-md-6 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantEmployer[' + eIndex + '].Address  asp-for=@Model.ApplicantEmployer[' + eIndex + '].Address  placeholder="Address" oninput="this.className = """ />';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" readonly name=ApplicantEmployer[' + eIndex + '].StartDate asp -for=@Model.ApplicantEmployer[' + eIndex + '].StartDate placeholder = "Start Date" oninput = "this.className = """ /> ';
            html = html + '</div>';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantEmployer[' + eIndex + '].AnnualIncome  asp-for=@Model.ApplicantEmployer[' + eIndex + '].AnnualIncome  placeholder="Annual Income" oninput="this.className = """ />';
            html = html + '</div>';
            html = html + '<div class="col-md-4 form-floating p-1">';
            html = html + '<select name="ApplicantEmployer[' + eIndex + '].JobType" asp-for=@Model.ApplicantEmployer[i].JobType class="dropdown dl">';
            html = html + '<option selected value="">--Select employment type--</option>';
            html = html + '<option value="Casual">Casual</option>';
            html = html + '<option value="PartTime">Part-Time</option>';
            html = html + '<option value="FullTime">Full-Time</option>';
            html = html + '<option value="Contract">Contract</option>';
            html = html + '</select>';             
            html = html + '<span data-valmsg-for=@Model.ApplicantEmployer[' + eIndex + '].JobType class="text-danger"></span>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">';            
            html = html + '<div class="col-md-12 form-floating p-1">';
            html = html + '<textarea name=ApplicantEmployer[' + eIndex + '].AdditionalInformation type="text" asp-for=@Model.ApplicantEmployer[' + eIndex + '].AdditionalInformation placeholder="Additional Information" oninput="this.className = """></textarea>';
            html = html + '</div>';
            html = html + '</div>';            
            html = html + '</div>';
            html = html + '<div class="row">';
            html = html + '<div class="col-md-12 form-floating p-1 text-end">';
            html = html + '<button class="btn btn-danger delete"><i class="bi bi-trash"></i> Remove</button>';
            html = html + '</div>';
            $('#dvappendEmployer').append(html);
            ++ecount;
            ++eIndex;
        }
        else {
            var employerNo = ecount - 1;
            e.preventDefault();
            Swal.fire({ 
                position: 'top-end',
                icon: 'error',
                title: 'You can not add more than ' + employerNo + ' employers',
                showConfirmButton: false,
                timer: 3000
            })
        }
        return false;
    });
    $('#dvappendEmployer').on('click', '.delete', function () {
        $(this).closest('.e').remove();
        empCount = document.querySelectorAll('div.ae').length;
        ecount = empCount;
        eIndex = empCount;
        $('.e').each(function (index, elem) {
            var id = 'e' + ++index;
            $(this).attr('id', id);
        });
        // Get rows with class 'avc'. UI for each vehicle is rapped inside div with psudo class avc
        $('.ae').each(function (index, elem) {

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
                            var setname = attr['data-valmsg-for'].value;
                           // debugger;
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
        //Get all divs with class e that contain <strong> used for vehicle no
        $('.e').children('strong').each(function (index, elem) {
           /// console.log('ecount=' + ecount);            
            if (index == 0) {
                elem.innerHTML = 'My Current Employer Details';
                //console.log('index before=' + index);
                ++index;
                //console.log('index after=' + index);
            }
            else { 
                elem.innerHTML = 'Previous Employer-' + index;
            }
            ecount = index + 1;
            eIndex = index;
            //console.log('index=' + eIndex + ' count=' + ecount);
        });
        return false;
    });
});