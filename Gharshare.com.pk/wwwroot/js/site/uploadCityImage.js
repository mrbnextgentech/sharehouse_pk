function validateUploadCityImage(input) {
    if (input.files && input.files[0]) {
        var file = input.files[0];
        var fileType = file.type.toLowerCase();
        var fileSize = file.size;

        // Check if the file type is an image
        if (fileType.startsWith("image/")) {
            uploadCityImage(input);            
        } else {
            // Display an error message or perform any other action
            //alert("Please select a valid image file.");
            input.value = ""; // Clear the selected file from the input
            console.log('invalid file');
            Swal.fire({
                icon: 'error',
                title: 'Validation Error',
                text: 'Please select a valid image file.',
                showConfirmButton: true,
                //timer: 2000,
            });            
        }
    }
}

function uploadCityImage(input) {
    debugger;
    if (input.files && input.files[0]) {
        var formData = new FormData();
        formData.append("image", input.files[0]);
        formData.append("cityId", document.getElementById("txtCityId").value);
       // debugger;
        $.ajax({
            url: "/admin/site/uploadCityImage",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                $('#imgCity').empty();
                var html = `<img id="imgCity" src="${data.image}" style="height:200px;width:100%;border-radius:10px;" onclick="document.getElementById('cityImage').click();" />`;
                $('#imgCity').append(html);
            },
            error: function (xhr, status, error) {
                console.error("Image upload failed!", error);
            }
        });
    }
}
