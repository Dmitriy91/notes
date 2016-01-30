$(function () {
    $("#note-table").tablesorter({
        headers: {
            3: {
                // disable sorting for the last column
                sorter: false
            }
        }
    });
});

