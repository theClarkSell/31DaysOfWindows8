// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    var notify = Windows.UI.Notifications;
    var push = Windows.Networking.PushNotifications;
    var net = Windows.Networking.Connectivity;
   
    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                // TODO: This application has been newly launched. Initialize
                // your application here.
            } else {
                // TODO: This application has been reactivated from suspension.
                // Restore application state here.
            }

            notify.TileUpdateManager.createTileUpdaterForApplication().clear();
            notify.BadgeUpdateManager.createBadgeUpdaterForApplication().clear();

            var profile = net.NetworkInformation.getInternetConnectionProfile();

            if (profile.getNetworkConnectivityLevel() === net.NetworkConnectivityLevel.internetAccess) {
                push.PushNotificationChannelManager.createPushNotificationChannelForApplicationAsync()
                    .then(function (channel) {
                        document.querySelector("#uri").innerText = channel.uri;

                        /* 
                        this would be the place to post your uri to your cloud service.
                        */
                })
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

    app.start();
})();
