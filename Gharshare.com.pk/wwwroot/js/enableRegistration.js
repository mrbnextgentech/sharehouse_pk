$(document).ready(function () {
    $('#chkTerms').click(function () {         
        if (this.checked)
        {
            document.getElementById("registerSubmit").disabled = false;            
        }
        else
        {
            document.getElementById("registerSubmit").disabled = true;
        }
    });
});