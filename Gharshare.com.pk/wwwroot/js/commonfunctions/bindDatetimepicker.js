 
$(document).ready(function () {

    function bindDatetimepicker() {
        // Select all inputs of type text with class .datepicker
        const datepickerInputs = $('input[type="text"].datepicker');

        // Iterate through each input and bind the DateTimePicker plugin
        datepickerInputs.each(function () {
            $(this).datetimepicker({
                // Add your desired options here
                // For example:
                // format: 'm/d/Y',
                // beforeShowDay: noWeekends,
                format: 'd/m/Y',
                date: new Date(),
                minDate: new Date(),
                timepicker: false,
            });
        });
    }
});
      
});