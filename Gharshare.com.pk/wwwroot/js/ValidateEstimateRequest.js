//$(document).ready(function () {
    $('#address').change(function () {
        var IPAddress = document.getElementById('IPAddress').value;
        var kittype = document.getElementById('kittype').value;
        var address = document.getElementById('address').value;        
        $.ajax({
            url: '/toolkit/ValidateToolkitRequest?IP=' + IPAddress + '&&toolkittype=' + kittype + '&&address=' + address,
            success: function (result) {
                if (result > 0) {
                    document.querySelector('#mySubmit').disabled = true;                    
                    Swal.fire({
                        icon: 'error',
                        title: 'Validation Error',
                        text: 'You have already placed request for this property.',
                        //footer: '<a href="">Why do I have this issue?</a>'
                    })
                }
                else { 
                   document.getElementById("mySubmit").disabled = false;                   
                }
            }
        });
    });
//});