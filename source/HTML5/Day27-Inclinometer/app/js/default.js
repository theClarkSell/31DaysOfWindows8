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

    var _pitch, _yaw, _roll, _timestamp;

    function onReadingChanged(e) {
        _pitch.innerText = e.reading.pitchDegrees.toFixed(2);
        _yaw.innerText = e.reading.yawDegrees.toFixed(2);
        _roll.innerText = e.reading.rollDegrees.toFixed(2);
        _timestamp.innerText = e.reading.timestamp;
    }

    function onShaken(e) {
        _wasShaken.innerText = e.timestamp;
    }

    function getDomElements() {
        _pitch = document.querySelector("#pitch");
        _yaw = document.querySelector("#yaw");
        _roll = document.querySelector("#roll");
        _timestamp = document.querySelector("#timestamp");
    }

    function startAccelerometer() {
        var inclinometer = Windows.Devices.Sensors.Inclinometer.getDefault()

        if (inclinometer) {
            var minimumReportInterval = inclinometer.minimumReportInterval;
            var reportInterval = minimumReportInterval > 16 ? minimumReportInterval : 16;
            inclinometer.reportInterval = reportInterval;

            inclinometer.addEventListener("readingchanged", onReadingChanged);
        }

    }

    app.onloaded = function () {
        getDomElements();
        startAccelerometer();
    }

    app.start();
})();
