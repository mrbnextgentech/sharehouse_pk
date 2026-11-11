$(document).ready(function () {
    $("#delete").removeAttr('disabled');    
    $("#delete").on("click", function (e) {        
        e.preventDefault();
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $('#DeleteForm').submit();
            }
        })
        
    });
}); 


/// second method through javascript
//<script type="text/javascript">
//    function confirmDelete(e)
//    {
//        e.preventDefault();

//Swal.fire({
//  title: 'Are you sure?',
//  text: "You won't be able to revert this!",
//  icon: 'warning',
//  showCancelButton: true,
//  confirmButtonColor: '#3085d6',
//  cancelButtonColor: '#d33',
//  confirmButtonText: 'Yes, delete it!'
//}).then((result) => {
//  if (result.isConfirmed) {
//      $('#frmDelete').submit();
//  }
//})
//    }
//</script>
