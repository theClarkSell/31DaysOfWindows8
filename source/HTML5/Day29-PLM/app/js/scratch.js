

var app = WinJS.Application;

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