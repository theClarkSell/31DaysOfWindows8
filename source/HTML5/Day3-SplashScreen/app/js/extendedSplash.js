
(function () {
    "use strict";

    function show(splash) {
        var extendedSplashImage = document.querySelector("#extendedSplashImage");
        position(extendedSplashImage, splash);

        WinJS.Utilities.removeClass(extendedSplashScreen, "hidden");
    }

    function remove() {
        if (isVisible()) {

            var extendedSplashScreen = document.querySelector("#extendedSplashScreen")
            WinJS.Utilities.addClass(extendedSplashScreen, "hidden");
        }
    }

    function updateImageLocation(splash) {

        if (isVisible()) {
            var extendedSplashImage = document.querySelector("#extendedSplashImage");

            position(extendedSplashImage, splash);
        }
    }

    function position(element, splash) {
        element.style.top = splash.imageLocation.y + "px";
        element.style.left = splash.imageLocation.x + "px";
        element.style.height = splash.imageLocation.height + "px";
        element.style.width = splash.imageLocation.width + "px";
    }

    function isVisible() {
        var extendedSplashScreen = document.querySelector("#extendedSplashScreen");
        return !(WinJS.Utilities.hasClass(extendedSplashScreen, "hidden"));
    }

    WinJS.Namespace.define("ExtendedSplash", {
        show: show,
        updateImageLocation: updateImageLocation,
        isVisible: isVisible,
        remove: remove
    });
})();
