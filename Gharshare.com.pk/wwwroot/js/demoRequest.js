$('#dname').on('change', function () { 
    //var ip;
    // Ajax call to Get IP start 
    $.ajax({
        url: '/demo/GetIP',
        success: function (result) {
            if (result != null) {
                $('#ipaddress').val(result);
                //ip = result;
                console.log(result);
            }
        }
    });// Ajax call to Get IP End    
});

$('#demail').on('change', function () {
    // Ajax call to validate demo request starts
    $.ajax({
        url: '/demo/validateDemoRequest?IP=' + $('#ipaddress').val(),
        success: function (result) {
            if (result > 0) {
                document.querySelector('#btnwatchdemo').disabled = true;
                Swal.fire({
                    icon: 'info',
                    title: 'Record Exists',
                    text: 'Already registered for demo!',
                    footer: '<a href="/demo/registered">Clik here to watch demo</a>'
                })
                //window.location.href = '/demo/registered';
            }
            else {
                document.getElementById("btnwatchdemo").disabled = false;
            }
        }
    });

    // Ajax call to validate demo request Ends
});



$('#dname,#demail,#dmobile').on('input', function () {
    $('#sname').html($('#dname').val() === "" ? "*" : "");
    $('#semail').html($('#demail').val() === "" ? "*" : "");
    $('#smobile').html($('#dmobile').val() === "" ? "*" : "");

    // Email validation
    const emailInput = $('#demail')[0];
    if (emailInput.validity.typeMismatch || emailInput.value === "") {
        $('#msgSpan').html("* Invalid email address");
        $('#semail').html("*");
    } else {
        $('#semail').html("");
        $('#msgSpan').html("");
    }
 });
 
$('#btnwatchdemo').click(function () {
    event.preventDefault();
        var name = document.getElementById('dname').value;
        var email = document.getElementById('demail').value;
        var mobile = document.getElementById('dmobile').value;
        var ip = $('#ipaddress').val();

    //debugger;
    //console.log(name + email + mobile);
    if (name != "" && email != "" && mobile != "") {
        $.ajax({
            url: '/demo/SaveDemoRequest?name=' + name + '&&email=' + email + '&&mobile=' + mobile + '&&ip=' +ip,
            success: function (result) {
                if (result === "success") {
                    window.location.href = '/demo/registered';
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'Error saving demo request.',
                    })
                }
            }
        });
    }
    else {
        if (name==="") {
            document.getElementById('sname').innerHTML = "*";
            document.getElementById('dname').focus();
        }
        if (email === "") {
            document.getElementById('semail').innerHTML = "*";
            document.getElementById('demail').focus();
        }
        if (mobile === "") {
            document.getElementById('smobile').innerHTML = "*";
            document.getElementById('dmobile').focus();
        }
    }
    });