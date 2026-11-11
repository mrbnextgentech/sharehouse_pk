$(document).ready(function () {

    $('#frmroomfeatures').submit(function (event) {
        event.preventDefault(); // Prevent default form submission
        var formId = 'frmroomfeatures';
        var formData = new FormData($('#' + formId)[0]);
        // Perform AJAX form submission         
        var closeButton = $('.btn-close');
        /// call to update property details
        $.ajax({
            url: "/listing/updateroomfeatures",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                // Update the rent element with the data from the data object
                generateRoomFeaturesHtml(data);
                closeButton.trigger('click');
                swal.fire(
                    {
                        icon: 'success',
                        title: 'Success',
                        text: 'Property rent updated successfully.',
                        showConfirmButton: false,
                        timer: 2000,
                    }
                );
            },
            error: function (xhr, status, error) {
                swal.fire(
                    {
                        icon: 'error',
                        title: 'Error',
                        text: 'Error updating profile.',
                        showConfirmButton: false,
                        timer: 2000,
                    }
                );
            }
        });

        /// call to deactivate account starts
    });   
    function generateRoomFeaturesHtml(data) {
        var roomFeaturesHtml = '';
        var roomFeaturesContainer = $('#roomfeatures');
        console.log(data);
        for (var i = 0; i < data.roomFurnishings.length; i++) {
            var roomFurnishing = data.roomFurnishings[i];
            var iconPath = roomFurnishing.furnishing.svgd;
            if (roomFurnishing.isSelected === true) {
                roomFeaturesHtml += '<div class="col-6 mb-3">';
                roomFeaturesHtml += '<div class="media" style="margin-left:5px;">';
                roomFeaturesHtml += '<img src="' + iconPath +'" style="height:35px;width:35px;" />'                 
                roomFeaturesHtml += '<div class="ms-2">';
                roomFeaturesHtml += '<h5 class="mb-0">' + roomFurnishing.furnishing.title + '</h5>';
                roomFeaturesHtml += '</div>';
                roomFeaturesHtml += '</div>';
                roomFeaturesHtml += '</div>';
            }
        };

        roomFeaturesContainer.html(roomFeaturesHtml);
    }

});