var DeletePropertyImage = function (ID) {
    var ImageID = ID;
    var PID = document.getElementById('PID').value;

    $.ajax({
        url: '/Property/DeletePropertyImage',
        type: "POST",
        dataType: "JSON",
        data: { ID: ImageID },
        success: function (result) {
            $('#images').empty();
            //console.log('inside success function');
            $.each(result, function (i, data) {
                if (data != null) {
                    //console.log(data);
                    var html = '<div class="col-md-2">' +
                        '<div class="p-1">' +
                        '<img src="' + data.imagePath + '" style="height:100px; width:95%" alt="" class="img-a img-fluid">' +
                        '</div>' +
                        '<div class="p-1">' +
                        '<a href="#" id="' + data.id + '" onclick="DeletePropertyImage(this.id);">' +
                        '<i class="far fa-trash-alt"></i> Delete' +
                        '</a>' +
                        '</div>' +
                        '</div>';
                    $('#images').append(html);
                }
            });
        }
    })
}