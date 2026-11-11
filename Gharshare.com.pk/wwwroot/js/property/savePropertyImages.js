$(document).ready(function () {
    $('#upload').on('click', function () {
        var files = $('#files')[0].files; // get the selected files from the file input
        var propertyID = $('#PID').val();
        if (files.length > 0) {
            var formData = new FormData(); // create a new FormData object
            formData.append('propertyID', propertyID); // append the property ID
            for (var i = 0; i < files.length; i++) {
                formData.append('files', files[i]); // append each selected file to the FormData object
            }

            $.ajax({
                url: '/Property/savePropertyImages',
                data: formData, // pass the FormData object as the data parameter
                type: 'POST', // use POST method
                contentType: false, // set contentType to false, jQuery will automatically set the correct content type based on the FormData object
                processData: false, // set processData to false to prevent jQuery from processing the FormData object
                success: function (result) {
                    $('#images').empty();
                    console.log('inside success function');
                    $.each(result, function (i, data) {
                        if (data != null) {
                            console.log(data);
                            var html = '<div class="col-md-2">' +
                                '<div class="p-1">' +
                                '<img src="' + data.imagePath + '" style="height:100%; width:95%" alt="" class="img-a img-fluid">' +
                                '</div>' +
                                '<div class="p-1">' +
                                '<a href="#" id="' + data.id + '" onclick="DeletePropertyImage(this.id);">' +
                                '<i class="far fa-trash-alt"></i> Delete'+
                                //'Click here to delete picture' +
                                //'<span class="bi bi-chevron-right"></span>' + 
                                '</a>' +
                                '</div>' +
                                '</div>';
                            $('#images').append(html);
                        }
                    });

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
