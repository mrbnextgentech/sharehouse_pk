$(document).ready(function () {
    //validation    
    $('#txtPassword').on('change', function () {
        //debugger;
        var password = $('#txtPassword').val();
        var hasUpperCase = /[A-Z]/.test(password);
        var hasLowerCase = /[a-z]/.test(password);
        if (!(hasUpperCase && hasLowerCase)) {
            $('#txtPassword').val("");
            swal.fire(
                {
                    icon: 'error',
                    title: 'Invalid Password',
                    text: 'Password must be at least 6 digit long and contain one capital & one small alphabet.',
                    showConfirmButton: true,
                    //timer: 2000,
                }
            );
        }
    });
    //End Validation
});
