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


    /*
        example schema 

        <tile>
          <visual>
            <binding template="TileSquareText04">
              <text id="1">Text Field 1</text>
            </binding>  
          </visual>
        </tile>

    */

    function sendTileUpdate() {

        var notifications = Windows.UI.Notifications;
        var template = notifications.TileTemplateType.tileSquareText04;
        var tileXml = notifications.TileUpdateManager.getTemplateContent(template);

        var tileTextAttributes = tileXml.getElementsByTagName("text");
        tileTextAttributes[0].innerText = _tileMessage.value; // taking from input box

        var tileNotification = new notifications.TileNotification(tileXml);
        notifications.TileUpdateManager.createTileUpdaterForApplication()
            .update(tileNotification);
    }

    function sendOtherTileUpdate() {
        var notifications = Windows.UI.Notifications;
        var template = notifications.TileTemplateType.tileSquareBlock;
        var tileXml = notifications.TileUpdateManager.getTemplateContent(template);

        var tileTextAttributes = tileXml.getElementsByTagName("text");
        tileTextAttributes[0].innerText = "31"; 
        tileTextAttributes[1].innerText = _tileMessage.value; // taking from input box
        

        var tileNotification = new notifications.TileNotification(tileXml);
        notifications.TileUpdateManager.createTileUpdaterForApplication()
            .update(tileNotification);
    }


    /*
    Example Tile for sendBothTiles()

        <tile>
            <visual lang="en-US>
        
                <binding template="TileSquareImage">
                    <image id="1" src="/images/clarkHeadShot.jpg" alt="Clark Sell"/>
                </binding> 

                <binding template="TileWideSmallImageAndText03">
                      <image id="1" src="/images/clarkHeadShot.jpg" alt="31 Days"/>
                      <text id="1">31 Days of Windows 8</text>
                </binding>  

            </visual>
        </tile>

    */

    function sendBothTiles() {

        var notifications = Windows.UI.Notifications;

        // Small Template
        var smallTemplate = notifications.TileTemplateType.tileSquareImage;
        var smallTileXml = notifications.TileUpdateManager.getTemplateContent(smallTemplate);

        var smallTileAttributes = smallTileXml.getElementsByTagName("image");
        smallTileAttributes[0].setAttribute("src", "ms-appx:///images/clarkHeadShot.jpg");
        smallTileAttributes[0].setAttribute("alt", "Clark's Head");
        
        //Wide Template
        var wideTemplate = notifications.TileTemplateType.tileWideSmallImageAndText03;
        var wideTileXml = notifications.TileUpdateManager.getTemplateContent(wideTemplate);

        var wideTileAttributes = wideTileXml.getElementsByTagName("image");
        var wideTileTextElements = wideTileXml.getElementsByTagName("text");

        wideTileAttributes[0].setAttribute("src", "ms-appx:///images/clarkHeadShot.jpg");
        wideTileTextElements[0].innerText = "31 Days of Windows 8";

        //Now we're going to add one of the tiles to the other tile creating one.
        var node = wideTileXml.importNode(smallTileXml.getElementsByTagName("binding").item(0), true);
        wideTileXml.getElementsByTagName("visual").item(0).appendChild(node);


        var tileNotification = new notifications.TileNotification(wideTileXml);
        notifications.TileUpdateManager.createTileUpdaterForApplication()
            .update(tileNotification);

    }


    var _sendTileUpdate, _tileMessage, _sendOtherTileUpdate, _sendDoubleNotification, _clearNotifications;

    function getDomElements() {
        _sendTileUpdate = document.querySelector("#sendNotification");
        _sendOtherTileUpdate = document.querySelector("#sendOtherNotification");
        _sendDoubleNotification = document.querySelector("#sendDoubleNotification");

        _clearNotifications = document.querySelector("#clearNotification");
        _tileMessage = document.querySelector("#tileMessage");
    }

    function registerHandlers() {
        _sendTileUpdate.addEventListener("click", sendTileUpdate, false);
        _sendOtherTileUpdate.addEventListener("click", sendOtherTileUpdate, false);

        _clearNotifications.addEventListener("click", function () {
            var notifications = Windows.UI.Notifications;
            notifications.TileUpdateManager.createTileUpdaterForApplication().clear();
        }, false);

        _sendDoubleNotification.addEventListener("click", function () {
            sendBothTiles();
        }, false)
    }




    app.onready = function () {
        getDomElements();
        registerHandlers();
    }

    app.start();
})();
