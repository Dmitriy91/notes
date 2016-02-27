$(function () {
    $(".datepicker").datepicker({
        format: "yyyy-mm-dd",
        weekStart: 1,
        todayBtn: "linked",
        clearBtn: true,
        daysOfWeekHighlighted: "0,6",
        calendarWeeks: true,
        autoclose: true,
        todayHighlight: true,
        toggleActive: true
    });
});
