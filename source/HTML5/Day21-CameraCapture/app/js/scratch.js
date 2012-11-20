
var _capture = Windows.Media.Capture;
var captureUI = new _capture.CameraCaptureUI();
captureUI.captureFileAsync(_capture.CameraCaptureUIMode.photo)
    .then(function (capturedItem) {
        // now what ?
    }
);