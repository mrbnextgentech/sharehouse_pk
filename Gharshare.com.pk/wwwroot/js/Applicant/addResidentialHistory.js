$(document).ready(function () {
    let homecount = document.querySelectorAll('div.ah').length;
    var hcount = homecount+1;
    var hIndex = homecount;
    $('#btnaddHistory').click(function (e) {
        if (hcount <= 3) {
            homecount = document.querySelectorAll('div.ah').length;
            hcount = homecount + 1;
            hIndex = homecount;
            var newhistory = '<div id=h' + hcount + ' class="h"><hr><strong class="hh">Residence-' + hcount + '</strong>';
            var html = newhistory + '<div class="ah"><div class="row">';
            html = html + '<div class="col-md-3 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantHistory[' + hIndex + '].UnitNo asp-for=@Model.ApplicantHistory[' + hIndex + '].UnitNo placeholder="UnitNo" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=@Model.ApplicantHistory[' + hIndex + '].UnitNo class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-3 form-floating p-1">';
            html = html + '<input type="text" name="ApplicantHistory[' + hIndex + '].StreetNo" asp-for=@Model.ApplicantHistory[i].StreetNo placeholder="StreetNo" oninput="this.className = """/>';
            html = html + '<span data-valmsg-for=@Model.ApplicantHistory[' + hIndex + '].StreetNo class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-6 form-floating p-1">';
            html = html + '<input type="text" name="ApplicantHistory[' + hIndex + '].StreetName" asp-for=@Model.ApplicantHistory[i].StreetName placeholder="StreetName" oninput="this.className = """/>';
            html = html + '<span data-valmsg-for=@Model.ApplicantHistory[' + hIndex + '].StreetName class="text-danger"></span>';
            html = html + '</div>';
            html = html + '</div>';            
            html = html + '<div class="row">';
            html = html + '<div class="col-md-3 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantHistory[' + hIndex + '].Suburb asp-for=@Model.ApplicantHistory[' + hIndex + '].Suburb placeholder="Suburb" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=@Model.ApplicantHistory[' + hIndex + '].Suburb class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-3 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantHistory[' + hIndex + '].PostCode asp-for=@Model.ApplicantHistory[' + hIndex + '].PostCode placeholder="PostCode" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=@Model.ApplicantHistory[' + hIndex + '].PostCode class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-3 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantHistory[' + hIndex + '].State asp-for=@Model.ApplicantHistory[' + hIndex + '].State placeholder="State" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=@Model.ApplicantHistory[' + hIndex + '].State class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-3 form-floating p-1">';
            html = html + '<input type="text" name=ApplicantHistory[' + hIndex + '].Country asp-for=@Model.ApplicantHistory[' + hIndex + '].Country placeholder="Country" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=@Model.ApplicantHistory[' + hIndex + '].Country class="text-danger"></span>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">';
            html = html + '<div class="col-md-6 form-floating p-1">';
            html = html + '<select name="ApplicantHistory[' + hIndex + '].LandlordType" asp-for=@Model.ApplicantHistory[i].LandlordType class="dropdown dl">';
            html = html + '<option selected value="">--Select type of landlord--</option>';
            html = html + '<option value="Real estate agent">Real estate agent</option>';
            html = html + '<option value="through landlord">Through a landlord</option>';
            html = html + '<option value="I own this home">I own this home</option>';
            html = html + '<option value="Living with family/friends">Living with family/friends</option>';
            html = html + '</select>';
            html = html + '<span data-valmsg-for=@Model.ApplicantHistory[' + hIndex + '].LandlordType class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-6 form-floating p-1 custom-file">';
            html = html + '<input type="text" readonly  name=ApplicantHistory[' + hIndex + '].DateStartedLiving asp-for=@Model.ApplicantHistory[' + hIndex + '].DateStartedLiving placeholder="Date Started Living" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=@Model.ApplicantHistory[' + hIndex + '].DateStartedLiving class="text-danger"></span>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">';            
            html = html + '<div class="col-md-12 form-floating p-1">';
            html = html + '<textarea name=ApplicantHistory[' + hIndex + '].AdditionalInformation type="text" asp-for=@Model.ApplicantHistory[' + hIndex + '].AdditionalInformation placeholder="Additional Information" oninput="this.className = """></textarea>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">';
            html = html + '<div class="col-md-12 form-floating p-1">';
            html = html + '<textarea name=ApplicantHistory[' + hIndex + '].Reasonforleaving type="text" asp-for=@Model.ApplicantHistory[' + hIndex + '].Reasonforleaving placeholder="Reason for leaving" oninput="this.className = """></textarea>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">';
            html = html + '<div class="col-md-12 form-floating p-1 text-end">';
            html = html + '<button class="btn btn-danger delete"><i class="bi bi-trash"></i> Remove</button>';
            html = html + '</div>';
            $('#dvappendHistory').append(html);
            ++hcount;
            ++hIndex;
        }
        else {
            var residenceNo = hcount - 1;
            e.preventDefault();
            Swal.fire({
                position: 'top-end',
                icon: 'error',
                title: 'You can not add more than ' + residenceNo + ' residences',
                showConfirmButton: false,
                timer: 3000
            })

        }
        return false;
    });
    $('#dvappendHistory').on('click', '.delete', function () {
        $(this).closest('.h').remove();
        homecount = document.querySelectorAll('div.ah').length;
        hcount = homecount + 1;
        hIndex = homecount;
        $('.h').each(function (index, elem) {
            var id = 'h' + ++index;
            $(this).attr('id', id);
        });
        // Get rows with class 'avc'. UI for each vehicle is rapped inside div with psudo class avc
        $('.ah').each(function (index, elem) {

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
                            //debugger;
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
        $('.h').children('strong').each(function (index, elem) {
          //  console.log('index before=' + index);
            elem.innerHTML = 'Residence-' + ++index;  
            //console.log('index after=' + index);
            hcount = index + 1;
            hIndex = index;
            //console.log('hcount=' + hcount);
            //console.log('hIndex=' + hIndex);
        });
        return false;
    });
});