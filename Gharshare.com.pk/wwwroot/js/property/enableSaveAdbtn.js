$(document).ready(function() {
  $('#chkacknowledge').on('click', function() {
    var chkAcknowledge = $('#chkacknowledge').is(':checked');
    console.log(chkAcknowledge);

    if (chkAcknowledge) {
      $('#btnSavead').prop('disabled', false);
    } else {
      $('#btnSavead').prop('disabled', true);
    }
  });
});
