    $(document).ready(function() {
            //var form = document.getElementById("frmreport");
            var signature;
    $('#exampleModal').one('shown.bs.modal', function(e) {
        signature = $("#signature").jSignature({ 'UndoButton': true });
    document.getElementsByTagName('canvas')[0].setAttribute('width', '400');
            });

    $('#preview').click(function() {
                var data = signature.jSignature('getData', 'image');
    $('#signaturePreview').attr('src', "data:" + data);
    $('#sigval').val(data[1]); // signature data
            });
        });