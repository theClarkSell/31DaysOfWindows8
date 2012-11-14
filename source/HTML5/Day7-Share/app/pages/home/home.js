(function () {
    "use strict";

    var _dtm = Windows.ApplicationModel.DataTransfer.DataTransferManager;

    WinJS.UI.Pages.define("/pages/home/home.html", {
        ready: function (element, options) {
           
            WinJS.Utilities.query("a").listen("click",
                function (eventInfo) {
                    eventInfo.preventDefault();
                    var link = eventInfo.target;
                    WinJS.Navigation.navigate(link.href);
                }, false);
                
            
            var dataTransferManager = _dtm.getForCurrentView();
            dataTransferManager.addEventListener("datarequested", dataRequested);

        },
        unload: function () {
            var dataTransferManager = _dtm.getForCurrentView();
            dataTransferManager.removeEventListener("datarequested", dataRequested);
        }

    });

    function dataRequested(e) {
        var request = e.request;

        request.data.properties.title =
            "31 Days of Windows 8";

        request.data.properties.description =
            "Have you heard of 31 Days of Windows 8?";

        request.data.setText(
            "Check out 31 Days of Windows 8 at http://31DaysOfWindows8.com");
    }

})();
