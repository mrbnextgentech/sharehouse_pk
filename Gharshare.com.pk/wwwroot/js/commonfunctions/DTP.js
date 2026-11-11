//$('.datepicker').each(function () {
//    $(this).datepicker({
//        // datepicker options
//        format: 'd/m/Y',
//        minDate: new Date(),
//        timepicker: false,
//    });
//});

$(document).ready(function () {
    //var jq = jQuery.noConflict();
    //setTimeout(function () {
    //    $(".datepicker").datetimepicker({
    //        format: 'd/m/Y',
    //        date: new Date(),
    //        minDate: new Date(),
    //        timepicker: false,
    //    });
    //}, 5000);
    $(".datepicker").datetimepicker(
        {
            //format: 'm/d/Y',
            //beforeShowDay: $(".datepicker").noWeekends,
            format: 'd/m/Y',
            date:new Date(),
            minDate: new Date(),
            timepicker: false,
        }
    );

    $(".datetimepicker").datetimepicker(
        {
            format: 'm/d/Y H:i',
            /*beforeShowDay: $.datepicker.noWeekends,*/
            minDate: new Date(),
            maxDate: GetMaxDate(new Date, 1),
            timepicker: true,
            allowTimes: ['9:00', '9:30', '10:00', '10:30', '11:00', '11:30', '12:00', '12:30', '13:00', '13:30', '14:00', '14:30', '15:00', '15:30', '16:00', '16:30', '17:00'],
            defaultTime: '9:0',
            //onSelectDate: function() { alert(document.getElementById("datepicker").value; )},
            //allowTimes: ['9:00','9:30','10:00','10:30','11:00','11:30','12:00','12:30',
            //'13:00','13:30', '14:00','14:30','15:00','15:30', '16:00','16:30','17:00']                
        }
    );
    function GetMaxDate(date, numMonths) {
        var month = date.getMonth();
        var milliSeconds = new Date(date).setMonth(month + numMonths);
        return new Date(milliSeconds);
    }
});