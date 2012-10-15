(function () {
    "use strict";

    var results;

    var page = WinJS.UI.Pages.define("/pages/DisplayOrientations.html", {
        ready: function (element, options) {
            results = document.getElementById("resultText");

            var dispProp = Windows.Graphics.Display.DisplayProperties;
            dispProp.addEventListener("orientationchanged", updateDisplayOrientation, false);

            updateDisplayOrientation(); // call to set the inital state
        }
    });

    function updateDisplayOrientation() {

        switch (Windows.Graphics.Display.DisplayProperties.currentOrientation) {

            case Windows.Graphics.Display.DisplayOrientations.landscape:
                results.innerText = "Landscape";
                break;

            case Windows.Graphics.Display.DisplayOrientations.portrait:
                results.innerText = "Portrait";
                break;

            case Windows.Graphics.Display.DisplayOrientations.landscapeFlipped:
                results.innerText = "Landscape (flipped)";
                break;

            case Windows.Graphics.Display.DisplayOrientations.portraitFlipped:
                results.innerText = "Portrait (flipped)";
                break;

            default:
                results.innerText = "Unknown";
                break;
        }
    }

})();
