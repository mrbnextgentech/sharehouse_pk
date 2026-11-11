$(document).ready(function() {
  $('#chkinspection').on('click', function() {
    var chkInspection = $('#chkinspection').is(':checked');
    console.log(chkInspection);

    if (chkInspection) {
      $('#inspection').css('display', 'flex');
      $('#inspectiondetail').css('display', 'block');
    } else {
      $('#inspection').css('display', 'none');
      $('#inspectiondetail').css('display', 'none');
    }
  });

  $('#chkHideAddress').on('click', function() {
    var checkBox = document.getElementById("chkHideAddress");
    var text = document.getElementById("full-address-recommend");
    if (checkBox.checked == true) {
      text.style.display = "block";
    } else {
      text.style.display = "none";
    }
  });
});
