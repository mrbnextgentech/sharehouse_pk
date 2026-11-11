$(document).ready(function () {
    let pets = document.querySelectorAll('div.p').length;
    var pcount = pets+1;
    var pIndex = pets;
    $('#btnaddPet').click(function (e) {
        if (pcount <= 3) {
            pets = document.querySelectorAll('div.p').length;
            pcount = pets + 1;
            pIndex = pets;
            var newpet = '<div id=p' + pcount + ' class="p"><hr><strong class="pp">Pet-' + pcount + '</strong>';
            var html = newpet + '<div class="ap"><div class="row">';
            html = html + '<div class="col-md-6 form-floating p-1">';            
            html = html + '<input type="text" name=ApplicantPets[' + pIndex +'].Name asp-for=@Model.ApplicantPets[' + pIndex +'].Name placeholder="Name" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=@Model.ApplicantPets[' + pIndex +'].Name class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-6 form-floating p-1">';
            html = html + '<select name="ApplicantPets[' + pIndex +'].Type" asp-for=@Model.ApplicantPets[i].Type class="dropdown dl">';
            html = html + ' <option selected value="">--Select Type--</option>';
            html = html + '<option value="Bird">Bird</option>';
            html = html + '<option value="Cat">Cat</option>';
            html = html + '<option value="Dog">Dog</option>';
            html = html + '<option value="Other">Other</option>';
            html = html + '</select>';
            html = html + '<span data-valmsg-for=@Model.ApplicantPets[' + pIndex +'].Type class="text-danger"></span>';
            html = html + '</div>';
            html = html + '</div>';            
            html = html + '<div class="row">';
            html = html + '<div class="col-md-6 form-floating p-1">';            
            html = html + '<input type="text" name=ApplicantPets[' + pIndex +'].Breed asp-for=@Model.ApplicantPets[' + pIndex +'].Breed placeholder="Breed" oninput="this.className = """ />';
            html = html + '<span data-valmsg-for=@Model.ApplicantPets[' + pIndex +'].Breed class="text-danger"></span>';
            html = html + '</div>';
            html = html + '<div class="col-md-6 form-floating p-1">';             
            html = html + '<input type="text" name=ApplicantPets[' + pIndex +'].age asp-for=@Model.ApplicantPets[' + pIndex +'].age placeholder="Age" oninput="this.className = """ />';
            html = html + '</div>';
            html = html + '</div>';            
            html = html + '<div class="row">';
            html = html + '<div class="col-md-6 form-floating p-1">';            
            html = html + '<input type="text" name=ApplicantPets[' + pIndex +'].Size asp-for=@Model.ApplicantPets[' + pIndex +'].Size placeholder="Size" oninput="this.className = """ />';
            html = html + '</div>';
            html = html + '<div class="col-md-1 form-floating p-1 custom-file">';
            html = html + '<label for="petimage">Picture</label>';
            html = html + '</div>';
            html = html + '<div class="col-md-5 form-floating p-1 custom-file">';
            html = html + '<input type="file" asp-for=@Model.ApplicantPets[' + pIndex +'].PicturePath name=ApplicantPets[' + pIndex +'].PicturePath class="custom-file-input pb-2" />';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">'; 
            html = html + '<div class="col-md-12 form-floating p-1">';            
            html = html + '<textarea name=ApplicantPets[' + pIndex + '].AdditionalInformation type="text" asp-for=@Model.ApplicantPets[' + pIndex + '].AdditionalInformation placeholder="Tell us about your pet" oninput="this.className = """></textarea>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '</div>';
            html = html + '<div class="row">';
            html = html + '<div class="col-md-12 form-floating p-1 text-end">';
            html = html + '<button class="btn btn-danger delete"><i class="bi bi-trash"></i> Remove</button>';
            html = html + '</div>';
            $('#dvappendPet').append(html);
            ++pcount;
            ++pIndex;
        }
        else {
            var petNo = pcount - 1;
            e.preventDefault();
            Swal.fire({
                position: 'top-end',
                icon: 'error',
                title: 'You can not add more than ' + petNo + ' pets',
                showConfirmButton: false,
                timer: 3000
            })

        }
        return false;
    });
    $('#dvappendPet').on('click', '.delete', function () {
        $(this).closest('.p').remove();
        pets = document.querySelectorAll('div.p').length;
        pcount = pets + 1;
        pIndex = pets;
        $('.p').each(function (index, elem) {
            var id = 'p' + ++index;
            $(this).attr('id', id);
        });
        // Get rows with class 'avc'. UI for each vehicle is rapped inside div with psudo class avc
        $('.ap').each(function (index, elem) {

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
        $('.p').children('strong').each(function (index, elem) {
            elem.innerHTML = 'Pet-' + ++index;
            pcount = index + 1;
            pIndex = index;
        });
        return false;
    });
});