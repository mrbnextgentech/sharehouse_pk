$(document).ready(function () {
    $('input:checkbox').click(function () {
        if (this.checked)
        {
            $(this).closest('.pa').next('div').show();
        }
        else
        {
            $(this).closest('.pa').next('div').hide();
        }
    });
});