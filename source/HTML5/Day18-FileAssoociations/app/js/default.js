// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                // TODO: This application has been newly launched. Initialize
                // your application here.
            } else {
                // TODO: This application has been reactivated from suspension.
                // Restore application state here.
            }
            args.setPromise(WinJS.UI.processAll());
        }

        if (args.detail.kind === activation.ActivationKind.file) {

            Windows.Storage.FileIO.readTextAsync(args.detail.files[0]).then(function (contents) {
                
                var myObject = JSON.parse(contents);
                ko.applyBindings(myObject);

            });

        }

    };

    app.oncheckpoint = function (args) {
        // TODO: This application is about to be suspended. Save any state
        // that needs to persist across suspensions here. You might use the
        // WinJS.Application.sessionState object, which is automatically
        // saved and restored across suspension. If you need to complete an
        // asynchronous operation before your application is suspended, call
        // args.setPromise().
    };




    var _btnOpenExcel,
        _launcher = Windows.System.Launcher,
        _current = Windows.ApplicationModel.Package.current;

    function getDomElements() {
        _btnOpenExcel = document.querySelector("#btnOpenExcel");
    }

    function wireHandlers() {
        _btnOpenExcel.addEventListener("click", function () {
            var excelFile = "data\\sampleData.xlsx";
            _current.installedLocation.getFileAsync(excelFile)
                .then(function (file) {

                    var launchOptions = new Windows.System.LauncherOptions();
                    launchOptions.treatAsUntrusted = true;

                    _launcher.launchFileAsync(file, launchOptions)
                        .then (function (isSuccess) {
                            if ( isSuccess ) { /* Rock on Garth! */ }
                            else { /* failWail */ }
                        });
                });
        });
    }

    app.onloaded = function () {
        getDomElements();
        wireHandlers();
    }

    app.start();
})();
