$(document).ready(function () {    
    $('#state').change(function () {
        var id = $(this).val(); 
        //alert(id);
        $('#city').empty();
        $('#city').append('<Option selected="selected" value="">--Select city--</Option>');                
        $.ajax({
            url: '/Property/GetCities?stateid=' + id,
            success: function (result) {
                $('#city').empty();               
                //$('#suburb').empty();
                //$('#suburb').append('<Option>--Select suburb--</Option>');               
                $.each(result, function (i, data) {
                    $('#city').append('<Option selected value=' + data.id + '>' + data.name + '</Option>');                     
                });
                $('#city').append('<Option selected value="">--Select city--</Option>');
            }
        });
    });

    //$('#city').change(function () {
    //    var id = $(this).val();
    //    $('#suburb').empty();
    //    $('#suburb').append('<Option>--Select suburb--</Option>');
    //    $.ajax({
    //        url: '/Property/GetSuburbs?cityid=' + id,
    //        success: function (result) {
    //            $.each(result, function (i, data) {
    //                $('#suburb').append('<Option value=' + data.id + '>' + data.name + '</Option>');
    //            });
    //        }
    //    });
    //});
});