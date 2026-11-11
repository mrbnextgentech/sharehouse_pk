$(function () {
    $("#chkstudent").click(function () {
        if ($(this).is(":checked")) {
            $("#dvstudent").show();
        } else {
            $("#dvstudent").hide();
        }
    });
});