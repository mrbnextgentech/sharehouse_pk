$(document).ready(function () {
    // Bind the change event to the file input element
    $('#files').on('change', function (event) {
        // Get the number of selected files
        var numFiles = this.files.length;

        // Limit the number of selected files to 15
        if (numFiles > 15) {
            //alert('You can select a maximum of 15 files.');
            Swal.fire({
                icon: 'error',
                icon: 'error',
                title: 'Validation Error',
                text: 'You can select a maximum of 15 files.',
            });
            // Clear the file input element to prevent the user from submitting too many files
            $(this).val('');
        }
    });
});
