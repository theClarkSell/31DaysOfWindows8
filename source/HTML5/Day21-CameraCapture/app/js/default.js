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

    var _imageCapture, _result;

    function getDomElements() {
        _imageCapture = document.querySelector("#imageCapture");
        _result = document.querySelector("#result");
    }
    
    function wireEventHandlers() {
        _imageCapture.addEventListener("click", imageCapture, false);
    }

    function imageCapture() {
        var captureUI = new Windows.Media.Capture.CameraCaptureUI();
        captureUI.captureFileAsync(Windows.Media.Capture.CameraCaptureUIMode.photo).then(function (capturedItem) {
            if (capturedItem) {
                document.getElementById("message").innerHTML = "User captured a photo."
            }
            else {
                document.getElementById("message").innerHTML = "User didn't capture a photo."
            }
        });
    }

    app.onloaded = function () {
        getDomElements();
        wireEventHandlers();
    }

    app.start();
})();
