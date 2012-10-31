(function () {
    'use script';

    var app = WinJS.Application;


    function activated(eventObject) {
        if (eventObject.detail.kind === Windows.ApplicationModel.Activation.ActivationKind.launch) {
            // Retrieve splash screen object
            splash = eventObject.detail.splashScreen;

            // Retrieve the window coordinates of the splash screen image.
            SdkSample.coordinates = splash.imageLocation;

            // Register an event handler to be executed when the splash screen has been dismissed.
            splash.addEventListener("dismissed", onSplashScreenDismissed, false);

            // Create and display the extended splash screen using the splash screen object.
            ExtendedSplash.show(splash);

            // Listen for window resize events to reposition the extended splash screen image accordingly.
            // This is important to ensure that the extended splash screen is formatted properly in response to snapping, unsnapping, rotation, etc...
            window.addEventListener("resize", onResize, false);

            // Use setPromise to indicate to the system that the splash screen must not be torn down
            // until after processAll and navigate complete asynchronously.
            eventObject.setPromise(WinJS.UI.processAll().then(function () {
                // Navigate to either the first scenario or to the last running scenario
                // before suspension or termination.
                var url = WinJS.Application.sessionState.lastUrl || scenarios[0].url;
                return WinJS.Navigation.navigate(url);
            }));
        }
    }


    // Displays the extended splash screen. Pass the splash screen object retrieved during activation.
    function show(splash) {
        var extendedSplashImage = document.getElementById("extendedSplashImage");

        // Position the extended splash screen image in the same location as the system splash screen image.
        extendedSplashImage.style.top = splash.imageLocation.y + "px";
        extendedSplashImage.style.left = splash.imageLocation.x + "px";
        extendedSplashImage.style.height = splash.imageLocation.height + "px";
        extendedSplashImage.style.width = splash.imageLocation.width + "px";

        // Position the extended splash screen's progress ring. Note: In this sample, the progress ring is not used.
        /*
        var extendedSplashProgress = document.getElementById("extendedSplashProgress");
        extendedSplashProgress.style.marginTop = splash.imageLocation.y + splash.imageLocation.height + 32 + "px";
        */

        // Once the extended splash screen is setup, apply the CSS style that will make the extended splash screen visible.
        var extendedSplashScreen = document.getElementById("extendedSplashScreen");
        WinJS.Utilities.removeClass(extendedSplashScreen, "hidden");
    }
    
    function onResize() {
        // Safely update the extended splash screen image coordinates. This function will be fired in response to snapping, unsnapping, rotation, etc...
        if (splash) {
            // Update the coordinates of the splash screen image.
            SdkSample.coordinates = splash.imageLocation;
            ExtendedSplash.updateImageLocation(splash);
        }
    }

    // Listen for window resize events to reposition the extended splash screen image accordingly.
    // This is important to ensure that the extended splash screen is formatted properly in response to snapping, unsnapping, rotation, etc...


    function loadEventHandlers() {
        window.addEventListener("resize", onResize, false);
    }
    



    WinJS.UI.Pages.define("splash", {
        ready: function (element, options) {
            loadEventHandlers();
        },
        foo: foo(),
        
    });


})();