(function () {
    "use strict";

    var backgroundTaskInstance = Windows.UI.WebUI.WebUIBackgroundTaskInstance.current;
    var canceled = false;

    // my "background task"
    function run() {
        var key = null,
            settings = Windows.Storage.ApplicationData.current.localSettings;

        // aka "background work"
        clearNotifications();
        sendTileUpdate();
        sendBadgeUpdateGlyph();
       
        // Record information in LocalSettings to communicate with the app.
        key = backgroundTaskInstance.task.taskId.toString();
        settings.values[key] = "Succeeded";

        close();        
    }

    function clearNotifications() {
        var notifications = Windows.UI.Notifications;
        notifications.TileUpdateManager.createTileUpdaterForApplication().clear();
        notifications.BadgeUpdateManager.createBadgeUpdaterForApplication().clear();
    }

    function sendTileUpdate() {
        var notifications = Windows.UI.Notifications;

        var smallTemplate = notifications.TileTemplateType.tileSquareImage;
        var smallTileXml = notifications.TileUpdateManager.getTemplateContent(smallTemplate);

        var wideTemplate = notifications.TileTemplateType.tileWideText01;
        var wideTileXml = notifications.TileUpdateManager.getTemplateContent(wideTemplate);

        var wideTileTextElements = wideTileXml.getElementsByTagName("text");
        wideTileTextElements[0].innerText = new Date().toTimeString();

        //merge tiles..
        var node = wideTileXml.importNode(smallTileXml.getElementsByTagName("binding").item(0), true);
        wideTileXml.getElementsByTagName("visual").item(0).appendChild(node);

        var tileNotification = new notifications.TileNotification(wideTileXml);

        var currentTime = new Date();
        tileNotification.expirationTime = new Date(currentTime.getTime() + 10 * 1000);

        notifications.TileUpdateManager.createTileUpdaterForApplication()
            .update(tileNotification);
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

    //event handlers
    function onCanceled(cancelSender, cancelReason) {
        canceled = true;
        close();
    }
    
    if (!canceled) {
        run();
    } else {
        key = backgroundTaskInstance.task.taskId.toString();
        settings.values[key] = "Canceled";

        //Must Call Close
        close();
    }
    
    //register event handlers
    backgroundTaskInstance.addEventListener("canceled", onCanceled);

})();