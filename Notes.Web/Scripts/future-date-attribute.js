$.validator.unobtrusive.adapters.addSingleVal("futuredate", "none");
$.validator.addMethod("futuredate", function (value, element) {
    if (value) {
        var currentDate = new Date();
        var dateParts = value.split("-");
        var year = dateParts[0];
        var month = dateParts[1];
        var day = dateParts[2];

        if (year < currentDate.getFullYear())
            return false;

        if (month < currentDate.getMonth() + 1)
            return false;

        if (day < currentDate.getDate())
            return false;
    }

    return true;
});
