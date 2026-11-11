$(document).ready(function () {
    $('#btnupload').on('click', function () {
        var files = $('#files')[0].files; // get the selected files from the file input
        var propertyID = $('#txtimagePropertyId').val().trim();
        if (files.length > 0) {
            var formData = new FormData(); // create a new FormData object
            formData.append('propertyID', propertyID); // append the property ID
            for (var i = 0; i < files.length; i++) {
                formData.append('files', files[i]); // append each selected file to the FormData object
            }
            $("#stepalert").show();
            $.ajax({
                url: '/listing/uploadPropertyImages',
                data: formData, // pass the FormData object as the data parameter
                type: 'POST', // use POST method
                contentType: false, // set contentType to false, jQuery will automatically set the correct content type based on the FormData object
                processData: false, // set processData to false to prevent jQuery from processing the FormData object
                success: function (result) {
                    $('#images').empty();
                    //console.log('inside success function');
                    $.each(result, function (i, data) {
                        if (data != null) {
                            console.log(data);
                            var html = '<div class="col-md-2">' +
                                '<div class="p-1">' +
                                '<img src="' + data.imagePath + '" style="height:80px; width:80px" alt="" class="img-a img-fluid">' +
                                '</div>' +
                                '<div class="p-1">' +
                                '<a href="#" id="' + data.id + '" onclick="DeletePropertyImage(this.id);">' +
                                '<i class="far fa-trash-alt"></i> Delete' +
                                '</a>' +
                                '</div>' +
                                '</div>';
                            $('#images').append(html);
                            $("#stepalert").hide();
                        }
                    });
                },
                error: function (xhr, status, error) {
                    // Handle error
                    $("#stepalert").hide();
                    var errorMessage = "Error saving listing image(s)!"
                    toastr.error(errorMessage, "Error");
                }
            });
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Validation Error',
                text: 'Must select one or more images for property!',
            })
            return false;
        }
    });
});





//$(document).ready(function () {
//    $('#upload').on('click', function () {
//        var files = $('#files')[0].files; // get the selected files from the file input
//        var propertyID = $('#txtimagePropertyId').val();
//        if (files.length > 0) {             

//            var formData = {
//                propertyID: propertyID,
//                files: files
//            };

//            $.ajax({
//                url: '/listing/uploadPropertyImages', 
//                data: JSON.stringify(formData),
//                type: 'POST',
//                contentType: 'application/json',                 
//                success: function (result) {
//                    $('#images').empty();
//                    console.log('inside success function');
//                    $.each(result, function (i, data) {
//                        if (data != null) {
//                            console.log(data);
//                            var html = '<div class="col-md-2">' +
//                                '<div class="p-1">' +
//                                '<img src="' + data.imagePath + '" style="height:70px; width:120px" alt="" class="img-a img-fluid">' +
//                                '</div>' +
//                                '<div class="p-1">' +
//                                '<a href="#" id="' + data.id + '" onclick="DeletePropertyImage(this.id);">' +
//                                '<i class="far fa-trash-alt"></i> Delete'+                                 
//                                '</a>' +
//                                '</div>' +
//                                '</div>';
//                            $('#images').append(html);
//                        }
//                    });

//                }
//            });
//        }
//        else {
//            Swal.fire({
//                icon: 'error',
//                title: 'Validation Error',
//                text: 'Must select one or more images for property!',
//                showConfirmationButton: false,
//                timer: 2000,
//            })
//            return false;
//        }
//    });
//});
