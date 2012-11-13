(function () {
    "use strict";

    WinJS.UI.Pages.define("/pages/home/home.html", {
        ready: function (element, options) {
           
            WinJS.Utilities.query("a").listen("click", anchorHandler, false);
        }
    });

    var myData = {
        firstName: "Clark",
        lastName: "Sell"
    };

    function anchorHandler(eventInfo) {
        eventInfo.preventDefault();
        var link = eventInfo.target;

        WinJS.Navigation.navigate(link.href, myData);
    }

})();
