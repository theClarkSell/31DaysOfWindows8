// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;
    var webUI = Windows.UI.WebUI.WebUIApplication;

    webUI.addEventListener("resuming", onResume, false);

    function onResume() {
        WinJS.log && WinJS.log("app resuming", "31 days", "status");
    }

    app.onactivated = function (args) {
        WinJS.log && WinJS.log("app.onactivated", "31 days", "status");

        if (args.detail.kind === activation.ActivationKind.launch) {
            WinJS.log && WinJS.log("Activation Kind === Launch.", "31 days", "status");

            switch (args.detail.previousExecutionState) {

                case activation.ApplicationExecutionState.terminated:
                    WinJS.log && WinJS.log("previousExecutionState terminated", "31 days", "status");
                    break;

                case activation.ApplicationExecutionState.running:
                    WinJS.log && WinJS.log("previousExecutionState running", "31 days", "status");
                    break;

                case activation.ApplicationExecutionState.suspended:
                    WinJS.log && WinJS.log("previousExecutionState suspended", "31 days", "status");
                    break;

                case activation.ApplicationExecutionState.closedByUser:
                    WinJS.log && WinJS.log("previousExecutionState closedByUser", "31 days", "status");
                    break;

                case activation.ApplicationExecutionState.notRunning:
                    WinJS.log && WinJS.log("previousExecutionState notRunning", "31 days", "status");
                    break;
            }

            args.setPromise(WinJS.UI.processAll());
        }
    };

    app.oncheckpoint = function (args) {
        args.setPromise(finishOff());
    };

    function finishOff() {
        return new WinJS.Promise(function (complete, cancel, progress) {
            WinJS.log && WinJS.log("app.oncheckpoint called.", "31 days", "status");
            complete();
        });
    }

    app.onloaded = function () {
        WinJS.Utilities.startLog();
    }

    app.start();
})();
