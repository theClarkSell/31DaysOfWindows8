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

    var _printThis,
        _printing = Windows.Graphics.Printing,
        _printManager = _printing.PrintManager,
        _printView = _printManager.getForCurrentView();

    function getDomElements() {
        _printThis = document.querySelector("#btnPrintThis");
    }

    function wireEventHandlers() {
        _printView.onprinttaskrequested = function (eventArgs){
            var printTask = eventArgs.request.createPrintTask("31 Days", function (args) {
                
                printTask.oncompleted = printCompleted;

                //printTask.options.displayedOptions.clear();
                printTask.options.displayedOptions
                    .append(_printing.StandardPrintTaskOptions.duplex);

                printTask.options.colorMode = _printing.PrintColorMode.grayscale;
                
                args.setSource(MSApp.getHtmlPrintDocumentSource(document));
            });

        };

        _printThis.addEventListener("click", function () {
            Windows.Graphics.Printing.PrintManager.showPrintUIAsync();
        }, false);
    }

    function printCompleted(eventArgs) {

        switch (eventArgs.completion) {
            case _printing.PrintTaskCompletion.failed:
                Windows.UI.Popups.MessageDialog("fail wail").showAsync();
                break;
            case _printing.PrintTaskCompletion.submitted:
                Windows.UI.Popups.MessageDialog("Submitted").showAsync();
                break;
            case _printing.PrintTaskCompletion.abandoned:
                Windows.UI.Popups.MessageDialog("Abandoned").showAsync();
                break;
            case _printing.PrintTaskCompletion.canceled:
                Windows.UI.Popups.MessageDialog("Cancelled").showAsync();
                break;
            default:
                break;
        }    
    }


    app.onloaded = function () {
        getDomElements();
        wireEventHandlers();
    }

    app.start();
})();
