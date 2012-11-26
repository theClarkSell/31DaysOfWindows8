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

    var _x, _y, _z;

    function onReadingChanged(e) {
        _x.innerText = e.reading.angularVelocityX.toFixed(2);
        _y.innerText = e.reading.angularVelocityY.toFixed(2);
        _z.innerText = e.reading.angularVelocityZ.toFixed(2);
    }

    function onShaken(e) {
        _wasShaken.innerText = e.timestamp;
    }

    function getDomElements() {
        _x = document.querySelector("#x");
        _y = document.querySelector("#y");
        _z = document.querySelector("#z");
    }

    function startAccelerometer() {
        var gyro = Windows.Devices.Sensors.Gyrometer.getDefault()

        if (gyro) {
            var minimumReportInterval = gyro.minimumReportInterval;
            var reportInterval = minimumReportInterval > 16 ? minimumReportInterval : 25;
            gyro.reportInterval = reportInterval;

            gyro.addEventListener("readingchanged", onReadingChanged);
        }

    }

    app.onloaded = function () {
        getDomElements();
        startAccelerometer();
    }

    app.start();
})();
