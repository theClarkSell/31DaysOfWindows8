
(function () {
    'use strict';

    var _app = WinJS.Application,
        _activation = Windows.ApplicationModel.Activation;
    
    var _shareOperation,%
        _dt = Windows.ApplicationModel.DataTransfer;

    _app.addEventListener("activated", function (args) {
       
        if (args.detail.kind === _activation.ActivationKind.shareTarget) {
            args.setPromise(WinJS.UI.processAll());

            _shareOperation = args.detail.shareOperation;

            document.querySelector("#btnReportCompleted").addEventListener("click", btnReportCompleted, false);

            WinJS.Application.addEventListener("shareready", shareReady, false);
            WinJS.Application.queueEvent({ type: "shareready" });
        }
        
    });

    function shareReady(args) {
        if (_shareOperation.data.contains(_dt.StandardDataFormats.uri)) {
            _shareOperation.data.getUriAsync().done(function (uri) {
                document.querySelector("#results").innerText =
                    "Uri: " + uri.absoluteUri;
            });
        }
    }

    function btnReportCompleted() {
        _shareOperation.reportCompleted();
    }

    WinJS.Application.start();

})();