// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
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


    function requestBackgroundAccess() {
        var background = Windows.ApplicationModel.Background;

        var promise = background.BackgroundExecutionManager.requestAccessAsync().then(
            function (result) {
                switch (result) {
                    case background.BackgroundAccessStatus.denied:
                        // Background activity and updates DENIED 
                        break;

                    case background.BackgroundAccessStatus.allowedWithAlwaysOnRealTimeConnectivity:
                        // Added to list of background apps.
                        // Set up background tasks
                        // CAN use the network connectivity broker.
                        break;

                    case background.BackgroundAccessStatus.allowedMayUseActiveRealTimeConnectivity:
                        // Added to list of background apps.
                        // Set up background tasks
                        // CANNOT use the network connectivity broker.
                        break;

                    case background.BackgroundAccessStatus.unspecified:
                        // The user didn't explicitly disable or enable access and updates. 
                        break;
                }
            });
    }

    function getBackgroundStatus() {
        var result = Background.BackgroundExecutionManager.getAccessStatus();
    }

    function sendBadgeUpdateXML() {
        var badgeXmlString = "<badge value='alert'/>";

        var badgeDOM = new Windows.Data.Xml.Dom.XmlDocument();
        badgeDOM.loadXml(badgeXmlString);

        var notifications = Windows.UI.Notifications;
        var badge = new notifications.BadgeNotification(badgeDOM);
        notifications.BadgeUpdateManager.createBadgeUpdaterForApplication().update(badge);

    }

    function sendBadgeUpdateNumber() {
        
        var notifications = Windows.UI.Notifications;
        
        var template = notifications.BadgeTemplateType.badgeNumber;
        var templateContent = notifications.BadgeUpdateManager.getTemplateContent(template);

        var element = templateContent.selectSingleNode("/badge");
        element.setAttribute("value", "200");
       
        var update = new notifications.BadgeNotification(templateContent);
        notifications.BadgeUpdateManager.createBadgeUpdaterForApplication().update(update);
    }

    function sendBadgeUpdateGlyph() {
        
        var notifications = Windows.UI.Notifications;

        var template = notifications.BadgeTemplateType.badgeGlyph;
        var templateContent = notifications.BadgeUpdateManager.getTemplateContent(template);

        var element = templateContent.selectSingleNode("/badge");
        element.setAttribute("value", "alert");

        var update = new notifications.BadgeNotification(templateContent);
        notifications.BadgeUpdateManager.createBadgeUpdaterForApplication().update(update);
    }

    function sendTileUpdate() {
        var notifications = Windows.UI.Notifications;

        // Small Template
        var smallTemplate = notifications.TileTemplateType.tileSquareImage;
        var smallTileXml = notifications.TileUpdateManager.getTemplateContent(smallTemplate);

        //Wide Template
        var wideTemplate = notifications.TileTemplateType.tileWideText01;
        var wideTileXml = notifications.TileUpdateManager.getTemplateContent(wideTemplate);
        
        var wideTileTextElements = wideTileXml.getElementsByTagName("text");
        wideTileTextElements[0].innerText = "31 Days of Windows 8";

        var tileNotification = new notifications.TileNotification(wideTileXml);
        notifications.TileUpdateManager.createTileUpdaterForApplication()
            .update(tileNotification);
    }

    function clearNotifications() {
        var notifications = Windows.UI.Notifications;
        notifications.TileUpdateManager.createTileUpdaterForApplication().clear();
        notifications.BadgeUpdateManager.createBadgeUpdaterForApplication().clear();
    }

    var _btnFireMessage, _numberOfSomething;

    function getDOMElemements() {
        _btnFireMessage = document.querySelector("#btnFireMessage");
    }

    function wireHandlers() {
        _btnFireMessage.addEventListener("click", send, false);
    }

   function send() {
       clearNotifications();

       setTimeout(function () {
           sendBadgeUpdateXML();
       }, 5 * 1000);

       setTimeout(function () {
           sendTileUpdate();
       }, 6 * 1000);

    }

    app.onready = function () {
        requestBackgroundAccess();

        getDOMElemements();
        wireHandlers();
    }
    
    app.start();
})();
