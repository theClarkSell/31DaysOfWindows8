(function () {
    "use strict";

    var dtm = Windows.ApplicationModel.DataTransfer.DataTransferManager;

    WinJS.UI.Pages.define("/pages/notHome/notHome.html", {
        ready: function (element, options) {
            var dataTransferManager = dtm.getForCurrentView();
            dataTransferManager.addEventListener("datarequested", dataRequested);
        },
        unload: function () {
            var dataTransferManager = dtm.getForCurrentView();
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
