(function () {
    "use strict";

    var results;

    function sensorOrientationChanged(e) {

        console.log("Sensor Fired: " + Date.now());

        switch (e.orientation) {
            case Windows.Devices.Sensors.SimpleOrientation.notRotated:
                results.innerText = "Not Rotated";
                break;

            case Windows.Devices.Sensors.SimpleOrientation.rotated90DegreesCounterclockwise:
                results.innerText = "Rotated 90";
                break;

            case Windows.Devices.Sensors.SimpleOrientation.rotated180DegreesCounterclockwise:
                results.innerText = "Rotated 180";
                break;

            case Windows.Devices.Sensors.SimpleOrientation.rotated270DegreesCounterclockwise:
                results.innerText = "Rotated 270";
                break;

            case Windows.Devices.Sensors.SimpleOrientation.faceup:
                results.innerText = "Face Up";
                break;

            case Windows.Devices.Sensors.SimpleOrientation.facedown:
                results.innerText = "Face Down";
                break;

            default:
                results.innerText = "Undefined orientation " + e.orientation;
                break;
        }
    }

    var page = WinJS.UI.Pages.define("/pages/SimpleOrientationSensor.html", {
        ready: function (element, options) {
            results = document.getElementById("resultText");
            var sensor = Windows.Devices.Sensors.SimpleOrientationSensor.getDefault();

            if (sensor) {
                sensor.addEventListener("orientationchanged", sensorOrientationChanged);
                //sensor.onorientationchanged = sensorOrientationChanged;

                results.innerText = "Sensor Found";
            }
            else {
                results.innerText = "No Sensor Found";
            }

        }
    });

})();
