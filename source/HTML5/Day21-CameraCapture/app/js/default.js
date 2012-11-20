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


    var _capture = Windows.Media.Capture;

    function imageCapture() {

        var captureUI = new _capture.CameraCaptureUI();
        captureUI.captureFileAsync(_capture.CameraCaptureUIMode.photo)
            .then(function (capturedItem) {
                if (capturedItem) {
    
                    var photoBlobUrl = URL.createObjectURL(
                        capturedItem,
                        { oneTimeOnly: true });

                    var imageElement = document.createElement("img");
                    imageElement.setAttribute("src", photoBlobUrl);

                    document.querySelector("#result").appendChild(imageElement);
                }
                else {
                    document.querySelector("#result").innerText= "User Cancelled"
                }
            }
        );

    }

    app.onloaded = function () {
        getDomElements();
        wireEventHandlers();
    }

    app.start();
})();
