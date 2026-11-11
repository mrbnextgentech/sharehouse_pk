function validateAndUploadImage(input) {
    if (input.files && input.files[0]) {
        var file = input.files[0];
        var fileType = file.type.toLowerCase();
        var fileSize = file.size;

        // Check if the file type is an image
        if (fileType.startsWith("image/")) {
            uploadProfileImage(input);
            //commented out below as image resize done in backend for large size pic. 
            //Check if the file size is within the allowed limit(e.g., 5MB)
            //var maxSizeInBytes = 100 * 1024; // 100kb
            //if (fileSize <= maxSizeInBytes) {
            //    // Proceed with the image upload
            //    uploadProfileImage(input);
            //} else {
            //    // Display an error message or perform any other action
            //    //alert("Please select an image file within the allowed size limit (5MB).");
            //    Swal.fire({
            //        icon: 'error',
            //        title: 'Validation Error',
            //        text: 'Please select an image file within the allowed size limit (5MB).',
            //    })
            //    input.value = ""; // Clear the selected file from the input
            //}
        } else {
            // Display an error message or perform any other action
            //alert("Please select a valid image file.");
            input.value = ""; // Clear the selected file from the input
            console.log('invalid file');
            Swal.fire({
                icon: 'error',
                title: 'Validation Error',
                text: 'Please select a valid image file.',
                showConfirmButton: false,
                timer: 2000,
            });            
        }
    }
}

function uploadProfileImage(input) {
    if (input.files && input.files[0]) {
        var formData = new FormData();
        formData.append("image", input.files[0]);
        /*formData.append("listingId", document.getElementById("txtintroPersonId").value);*/
        formData.append("userId", document.getElementById("txtUserId").value);
        
       // debugger;
        $.ajax({
            /*url: "/findplace/uploadPicture4Listing",*/
            url: "/profile/uploadProfileImage",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                $('#profilePic').empty(); // profile pic in Account
                var html = `<img src="${data.profilePicture}" alt="Rounded Image" onclick="document.getElementById('file-upload').click();" />`;
                $('#profilePic').append(html);
               // console.log(data);
                //console.log("Image uploaded successfully!", data.profilePicture);                 
            },
            error: function (xhr, status, error) {
                console.error("Image upload failed!", error);
            }
        });
    }
}
