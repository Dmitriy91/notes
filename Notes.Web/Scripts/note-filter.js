$(function () {
    var filters = $(".filterable .filters input");

    $(".filterable .btn-filter").click(function () {
        if (filters.prop("disabled") == true) {
            filters.prop("disabled", false);
            filters.get(0).focus();
        } else {
            filters.val("").prop("disabled", true);
        }
    });

    $(".filter-wrapper").click(function (event) {
        event.stopPropagation();
    });

    filters.each(function (i, input) {
        if (input.value != "") {
            $(".filterable .btn-filter").trigger("click");
            input.focus();
            return false;
        }
    })
});
