$(function () {
    $("#chkPet").click(function () {
        if ($(this).is(":checked")) {
            $("#dvPet").show();
        } else {
            $("#dvPet").hide();
        }
    });
});