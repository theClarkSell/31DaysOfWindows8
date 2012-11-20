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

    var _imageCapture, _result, _videoCapture;
    var _capture = Windows.Media.Capture;

    function getDomElements() {
        _imageCapture = document.querySelector("#imageCapture");
        _videoCapture = document.querySelector("#videoCapture");
        _result = document.querySelector("#result");
    }
    
    function wireEventHandlers() {
        _imageCapture.addEventListener("click", imageCapture, false);
        _videoCapture.addEventListener("click", videoCapture, false);
    }

    function imageCapture() {

        var captureUI = new _capture.CameraCaptureUI();

        captureUI.photoSettings.format = _capture.CameraCaptureUIPhotoFormat.png;
        captureUI.photoSettings.croppedAspectRatio = { height: 4, width: 3 };

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

    function videoCapture() {

        var captureUI = new _capture.CameraCaptureUI();

        captureUI.videoSettings.format
            = _capture.CameraCaptureUIVideoFormat.mp4;

        captureUI.videoSettings.allowTrimming = true;

        captureUI.videoSettings.maxDurationInSeconds = 15;

        captureUI.videoSettings.maxResolution =
            _capture.CameraCaptureUIMaxVideoResolution.highestAvailable;

        captureUI.captureFileAsync(_capture.CameraCaptureUIMode.video)
            .then(function (capturedItem) {
                if (capturedItem) {
                    
                    var url = URL.createObjectURL(
                        capturedItem,
                        { oneTimeOnly: true });

                    var imageElement = document.createElement("video");
                    imageElement.setAttribute("src", url);
                    imageElement.setAttribute("autoplay");
                    imageElement.setAttribute("controls");

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
