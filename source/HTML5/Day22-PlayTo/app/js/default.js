// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            args.setPromise(WinJS.UI.processAll());
        }
    };

    var ptm = Windows.Media.PlayTo.PlayToManager.getForCurrentView();
    ptm.addEventListener("sourcerequested", sourceRequestHandler, false);

    function sourceRequestHandler(e) {
        try {
            
            e.sourceRequest.setSource(document.querySelector("#audioTag").msPlayToSource);

        } catch (ex) {
            
                
        }
    }

    app.start();
})();
