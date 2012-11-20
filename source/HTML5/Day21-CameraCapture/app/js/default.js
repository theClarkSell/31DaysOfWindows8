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
    
                var photoBlobUrl = URL.createObjectURL(capturedItem, { oneTimeOnly: true });

                
                var imageElement = document.createElement("img");
                imageElement.setAttribute("src", photoBlobUrl);

                document.querySelector("#result").appendChild(imageElement);

                //document.querySelector("#imgResult").src = photoBlobUrl;
            }
            else {
                document.querySelector("message").innerHTML = "User didn't capture a photo."
            }
        });
    }

    app.onloaded = function () {
        getDomElements();
        wireEventHandlers();
    }

    app.start();
})();
