$(document).ready(function () {
    // Bind the dragstart event to the parent element using event delegation
    $('#images').on('dragstart', '.img-a', function (event) {
        // Save the dragged item and its original position
        event.originalEvent.dataTransfer.setData('text/plain', $(this).closest('.col-md-2').index());
        event.originalEvent.dataTransfer.effectAllowed = 'move';

        // Add the 'dragging' class to the dragged item
        $(this).closest('.col-md-2').addClass('dragging');

        // Add the 'dragging-cursor' class to the parent element
        $(this).closest('.col-md-2').parent().addClass('dragging-cursor');
    });

    // Bind the dragover event to the parent element using event delegation
    $('#images').on('dragover', '.col-md-2', function (event) {
        // Prevent the default behavior of the dragover event
        event.preventDefault();

        // Set the dropEffect to 'move'
        event.originalEvent.dataTransfer.dropEffect = 'move';
    });

    // Bind the dragleave event to the parent element using event delegation
    $('#images').on('dragleave', '.col-md-2', function (event) {
        // Remove the 'dragging-cursor' class from the parent element
        $(this).parent().removeClass('dragging-cursor');
    });

    // Bind the drop event to the parent element using event delegation
    $('#images').on('drop', '.col-md-2', function (event) {
        // Prevent the default behavior of the drop event
        event.preventDefault();

        // Get the dragged item's original position
        var originalPosition = parseInt(event.originalEvent.dataTransfer.getData('text/plain'), 10);

        // Get the dropped item's new position
        var newPosition = $(this).index();

        // Move the dragged item to its new position
        if (originalPosition < newPosition) {
            $(this).after($('#images .col-md-2').eq(originalPosition));
        } else {
            $(this).before($('#images .col-md-2').eq(originalPosition));
        }

        // Remove the 'dragging' class from the dragged item
        $('#images .col-md-2').eq(originalPosition).removeClass('dragging');

        // Remove the 'dragging-cursor' class from the parent element
        $(this).parent().removeClass('dragging-cursor');
    });
});
