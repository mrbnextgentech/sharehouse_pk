$('#quick_view_modal').on('show.bs.modal', function (event) {
    alert('hello');
    var button = $(event.relatedTarget) // Button that triggered the modal
    var propertyId = button.data('bs-pid') // Extract value from data-bs-pid attribute
    console.log(propertyId);
    //var modal = $(this)
    //modal.find('.property-id').text(propertyId) // Set the text content of .property-id element
})



//$('#exampleModal').on('show.bs.modal', function (event) {
//    var button = $(event.relatedTarget) // Button that triggered the modal
//    var recipient = button.data('whatever') // Extract info from data-* attributes
//    // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
//    // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
//    var modal = $(this)
//    modal.find('.modal-title').text('New message to ' + recipient)
//    modal.find('.modal-body input').val(recipient)
//})