//$(document).ready(function () {
    $('#name').change(function () {
        var IPAddress = document.getElementById('IPAddress').value;
        var kittype = document.getElementById('kittype').value;
        $.ajax({
            url: '/toolkit/ValidateToolkitRequest?IP=' + IPAddress + '&&toolkittype=' + kittype,
            success: function (result) {
                if (result > 0) {
                    document.querySelector('#mySubmit').disabled = true;
                    //document.getElementById('mySubmit').disabled = true;
                    //alert('You have already placed the request.');
                    Swal.fire({
                        icon: 'error',
                        title: 'Validation Error',
                        text: 'You have already placed the request !',
                        //footer: '<a href="">Why do I have this issue?</a>'
                    })
                }
                else {
                    document.getElementById("mySubmit").disabled = false;
                    // alert('No record found');
                }
            }
        });
    });
//})

//$(document).ready(function () {
//    $('#email').change(function () {
//        var IPAddress = document.getElementById('IPAddress').value;
//        var kittype = document.getElementById('kittype').value;        
//        $.ajax({
//            url: '/toolkit/ValidateToolkitRequest?IP=' + IPAddress + '&&toolkittype=' + kittype,
//            success: function (result) {
//                if (result > 0) {
//                    document.querySelector('#mySubmit').disabled = true;
//                    //document.getElementById('mySubmit').disabled = true;                    
//                    alert('You have already place the request.');
//                }
//                else { 
//                   document.getElementById("mySubmit").disabled = false;
//                   // alert('No record found');
//                }
//            }
//        });
//    });
//});