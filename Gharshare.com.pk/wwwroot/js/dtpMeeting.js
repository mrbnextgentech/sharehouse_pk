$(function () {
    $("#datepicker").datetimepicker(
        {
           format: 'm/d/Y H:i',
            beforeShowDay: $.datepicker.noWeekends,
            minDate: new Date(),
            maxDate: GetMaxDate(new Date, 1),
            timepicker: true,
            allowTimes: ['9:00', '10:00', '11:00', '12:00', '13:00', '14:00', '15:00', '16:00', '17:00'],
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