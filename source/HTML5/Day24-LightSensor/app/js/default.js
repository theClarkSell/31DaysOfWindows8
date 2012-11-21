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

    var _light;

    function onReadingChanged(e) {
        _light.innerText = e.reading.illuminanceInLux.toFixed(2);
    }

    function startLightSensor() {
        var lightSensor = Windows.Devices.Sensors.LightSensor.getDefault();

        if (lightSensor) {
            var minimumReportInterval = lightSensor.minimumReportInterval;
            var reportInterval = minimumReportInterval > 16 ? minimumReportInterval : 16;
            lightSensor.reportInterval = reportInterval;

            lightSensor.addEventListener("readingchanged", onReadingChanged);
        }
    }

    function getDomElements() {
        _light = document.querySelector("#light");
    }

    app.onready = function () {
        getDomElements();
        startLightSensor();
    }

    app.start();
})();
