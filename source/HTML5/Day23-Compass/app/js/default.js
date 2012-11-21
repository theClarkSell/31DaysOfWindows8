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

    var _trueNorth, _magNorth;

    function onReadingChanged(e) {
        var newReading = e.reading;
        _magNorth.innerText = newReading.headingMagneticNorth.toFixed(2);

        if (newReading.headingTrueNorth != null) {
            _trueNorth.innerText = reading.headingTrueNorth.toFixed(2);
        }
    }

    function startCompass() {
        var compass = Windows.Devices.Sensors.Compass.getDefault();

        if (compass) {
            var minimumReportInterval = compass.minimumReportInterval;
            var reportInterval = minimumReportInterval > 16 ? minimumReportInterval : 16;
            compass.reportInterval = reportInterval;

            compass.addEventListener("readingchanged", onReadingChanged);
        }
    }

    function getDomElements() {
        _trueNorth = document.querySelector("#trueNorth");
        _magNorth = document.querySelector("#magNorth");
    }

    app.onloaded = function () {
        getDomElements();
        startCompass();
    }

    app.start();
})();
