$(document).ready(function () {

    $('#ename,#eemail,#emobile,#emessage').on('input', function () {       
        $('#sename').html($('#ename').val() === "" ? "*" : "");
        $('#seemail').html($('#eemail').val() === "" ? "*" : "");
        $('#semobile').html($('#emobile').val() === "" ? "*" : "");
        $('#semessage').html($('#emessage').val() === "" ? "*" : "");
        

        // Email validation
        const emailInput = $('#eemail')[0];
        if (emailInput.validity.typeMismatch || emailInput.value === "") {
            $('#msgeSpan').html("* Invalid email address");
            $('#seemail').html("*");
        } else {
            $('#seemail').html("");
            $('#msgeSpan').html("");
        }
    });
    $('#ddlAbout').on('change', function () {
        debugger;
        $('#seAbout').html($('#ddlAbout').val() === "" ? "*" : "");
    });

    $('#btnesumbit').click(function () {
        event.preventDefault();
        var name = document.getElementById('ename').value;
        var email = document.getElementById('eemail').value;
        var mobile = document.getElementById('emobile').value;
        var about = $("#ddlAbout").val();
        var propertyId = $('#pid').val();
        var message = $('#emessage').val(); 

        //debugger;
        //console.log(name + email + mobile);
        if (name != "" && email != "" && mobile != "" && about != ""
            && propertyId != "" && message != "") {
            $.ajax({
                url: '/home/SaveEnquiry?about=' + about + '&&name=' + name +
                    '&&email=' + email + '&&mobile=' + mobile +
                    '&&pid=' + propertyId + '&&message=' + message,
                success: function (result) {
                    if (result === "success") {
                        Swal.fire({
                            icon: 'success',
                            title: 'Success',
                            text: 'Enquiry sent. We will get back to you soon',
                            showConfirmButton: false,
                            timer: 3000,
                        })
                        // clear controls after saving record
                        $('#ename').val("");
                        $('#eemail').val("");
                        $('#emobile').val("");
                        $("#ddlAbout").val("");
                        $('#emessage').val(""); 
                        ///
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: 'Error sending enquiry !',
                        })
                    }
                }
            });
        }
        else {
            if (message === "") {
                $("#semessage").html("*");
                $("#emessage").focus();
            }            
            if (mobile === "") {
                document.getElementById('semobile').innerHTML = "*";
                document.getElementById('emobile').focus();
            }
            if (email === "") {
                document.getElementById('seemail').innerHTML = "*";
                document.getElementById('eemail').focus();
            }
            if (name === "") {
                document.getElementById('sename').innerHTML = "*";
                document.getElementById('ename').focus();
            }
            if (about === "") {
                $("#seAbout").html("*");
                $("#ddlAbout").focus();
            }
        }
    });
     

});