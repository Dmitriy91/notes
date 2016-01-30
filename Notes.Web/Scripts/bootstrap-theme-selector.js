$(function () {
    markOutSelectedTheme();

    function markOutSelectedTheme() {
        if (getCookie("theme") != "") {
            var themeUrl = $("link[href*='bootstrap']:first").attr("href");
            var themeName = themeUrl.split(".")[1].toLowerCase();
            var uiThemeName = "";

            $("#theme-menu > li").each(function () {
                uiThemeName = $(this).text().trim().replace(/ /g, "").toLowerCase();

                if (uiThemeName == themeName) {
                    $(this).addClass("selected-theme");
                    return false;
                }
            });
        }
    }

    $("#theme-menu > li").on("click", function () {
        // cross-browser solution
        var target = $(event.target || event.srcElement || event.originalTarget);
        var themeName = target.text().trim().replace(/ /g, "").toLowerCase();
        switchTheme(themeName);
        target.siblings().removeClass("selected-theme");
        target.addClass("selected-theme");
    });

    var switchTheme = function (themeName) {
        var themeUrl = "/Content/css/bootstrap." + themeName + ".min.css";
        $("link[href*='bootstrap']:first").attr("href", themeUrl);
        setCookie("theme", themeUrl, 30);
    };

    function setCookie(cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + "; " + expires + "; path=/";
    };

    function getCookie(cname) {
        var name = cname + "=";
        var ca = document.cookie.split(';');

        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1);
            if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
        }
        return "";
    };
});
