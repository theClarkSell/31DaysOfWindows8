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


    var _sendBasicToast, _toastMessage;

    function getDomElements() {
        _sendBasicToast = document.querySelector("#sendToast");
        _toastMessage = document.querySelector("#toastMessage");
    }

    function wireHandlers() {
        _sendBasicToast.addEventListener("click", sendBasicToast, false);
    }

    /*
    sample toast we're going to send for sendBasicToast()
        
        <toast>
            <visual>
                <binding template="ToastImageAndText01">
                    <image id="1" src="image1" alt="image1"/>
                    <text id="1">bodyText</text>
                </binding>  
            </visual>
        </toast>
    */

    function sendBasicToast() {
        var notifications = Windows.UI.Notifications;

        var template = notifications.ToastTemplateType.toastImageAndText02;
        var toastXml = notifications.ToastNotificationManager.getTemplateContent(template);

        var toastTextElements = toastXml.getElementsByTagName("text");
        toastTextElements[0].innerText = "31 Days of Windows 8";
        toastTextElements[1].innerText = _toastMessage.value; //taking from screen

        var toastImageElements = toastXml.getElementsByTagName("image");
        toastImageElements[0].setAttribute("src", "ms-appx:///images/clarkHeadShot.jpg");
        toastImageElements[0].setAttribute("alt", "Clark's Head");

        var toastNode = toastXml.selectSingleNode("/toast");
        toastNode.setAttribute("duration", "long");

        var toastNode = toastXml.selectSingleNode("/toast");

        var audio = toastXml.createElement("audio");
        audio.setAttribute("src", "ms-winsoundevent:Notification.IM");
        toastNode.appendChild(audio);
        

        /*
            toastXml.selectSingleNode("/toast").setAttribute("launch", '{
                            "type":"toast",
                            "args1":"31days",
                            "args2":"#10"}' );
        */

        var toast = new notifications.ToastNotification(toastXml);
        var toastNotifier = notifications.ToastNotificationManager.createToastNotifier();
        toastNotifier.show(toast);
    }

    app.onready = function () {
        getDomElements();
        wireHandlers();
    }

    app.start();
})();
