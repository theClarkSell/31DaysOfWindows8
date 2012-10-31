// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    var splash = null; // Variable to hold the splash screen object.
    var dismissed = false; // Variable to track splash screen dismissal status.
    var coordinates = { x: 0, y: 0, width: 0, height: 0 }; // Object to store splash screen image coordinates. It will be initialized during activation.


    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {

            // Retrieve splash screen object
            splash = args.detail.splashScreen;

            // Retrieve the window coordinates of the splash screen image.
            SdkSample.coordinates = splash.imageLocation;

            // Register an event handler to be executed when the splash screen has been dismissed.
            splash.addEventListener("dismissed", onSplashScreenDismissed, false);

            // Create and display the extended splash screen using the splash screen object.
            ExtendedSplash.show(splash);

            // Listen for window resize events to reposition the extended splash screen image accordingly.
            // This is important to ensure that the extended splash screen is formatted properly in response to snapping, unsnapping, rotation, etc...
            window.addEventListener("resize", onResize, false);




            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                // TODO: This application has been newly launched. Initialize
                // your application here.
            } else {
                // TODO: This application has been reactivated from suspension.
                // Restore application state here.
            }
            args.setPromise(WinJS.UI.processAll());
        }
    };

    app.oncheckpoint = function (args) {
        // TODO: This application is about to be suspended. Save any state
        // that needs to persist across suspensions here. You might use the
        // WinJS.Application.sessionState object, which is automatically
        // saved and restored across suspension. If you need to complete an
        // asynchronous operation before your application is suspended, call
        // args.setPromise().
    };

    WinJS.Navigation.addEventListener("navigated", function (eventObject) {
        var url = eventObject.detail.location;
        var host = document.getElementById("contentHost");
        // Call unload method on current scenario, if there is one
        host.winControl && host.winControl.unload && host.winControl.unload();
        WinJS.Utilities.empty(host);
        eventObject.detail.setPromise(WinJS.UI.Pages.render(url, host, eventObject.detail.state).then(function () {
            WinJS.Application.sessionState.lastUrl = url;
        }));
    });

    function onSplashScreenDismissed() {
        // Include code to be executed when the system has transitioned from the splash screen to the extended splash screen (application's first view).
        SdkSample.dismissed = true;

        // Tear down the app's extended splash screen after completing setup operations here...
        // In this sample, the extended splash screen is torn down when the "Learn More" button is clicked.
        document.getElementById("learnMore").addEventListener("click", ExtendedSplash.remove, false);

        // The following operation is only applicable to this sample to ensure that UI has been updated appropriately.
        // Update scenario 1's output if scenario1.html has already been loaded before this callback executes.
        if (document.getElementById("dismissalOutput")) {
            document.getElementById("dismissalOutput").innerText = "Received the splash screen dismissal event.";
        }
    }

    function onResize() {
        // Safely update the extended splash screen image coordinates. This function will be fired in response to snapping, unsnapping, rotation, etc...
        if (splash) {
            // Update the coordinates of the splash screen image.
            SdkSample.coordinates = splash.imageLocation;
            ExtendedSplash.updateImageLocation(splash);
        }
    }

    WinJS.Namespace.define("SdkSample", {
        dismissed: dismissed,
        coordinates: coordinates
    });

    app.start();
})();
