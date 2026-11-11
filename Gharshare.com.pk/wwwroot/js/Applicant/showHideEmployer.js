$(function () {
    $('#ddljobstatus').change(function () {
        var val = $(this).val();
        if (val == 'employed' || val =='selfemployed') {
            $("#dvemployer").show();
        } else {
            $("#dvemployer").hide();
        }
    });
});