$(function () {
    $(".datepicker").datepicker({
        forceParse: true,
        format: "yyyy-mm-dd",
        todayBtn: "linked",
        todayHighlight: true,
        daysOfWeekHighlighted: "0,6",
        calendarWeeks: true,
        weekStart: 1,
        todayHighlight: true,
        autoclose: true
    });
});
