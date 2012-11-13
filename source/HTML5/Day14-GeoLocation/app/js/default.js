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


    var _geolocator;

    var _lat, _long, _accuracy, _status, _btnGetLocation,
        _btnGetHtml5Location, _btnGetPositionConstant, _btnStopPositionConstant;

    function getDomElements() {
        _lat = document.querySelector("#lat");
        _long = document.querySelector("#long");
        _accuracy = document.querySelector("#accuracy");
        _status = document.querySelector("#status");

        _btnGetLocation = document.querySelector("#btnGetLocation");
        _btnGetHtml5Location = document.querySelector("#btnGetHtml5Location");

        _btnGetPositionConstant = document.querySelector("#btnGetPositionConstant");
        _btnStopPositionConstant = document.querySelector("#btnStopPositionConstant");
    }

    function wireEventHandlers() {
        _btnGetLocation.addEventListener("click", getlocation, false);
        _btnGetHtml5Location.addEventListener("click", getHtml5Location, false);

        _btnGetPositionConstant.addEventListener("click", getLocationEventBased, false);
        _btnStopPositionConstant.addEventListener("click", stopGetPosition, false);
    }

    function getLocator() {
        if (_geolocator == null) {
            _geolocator = new Windows.Devices.Geolocation.Geolocator();
        }
    }

    function getlocation() {
        getLocator();

        if (_geolocator != null) {
            //this call will also ask the user for permission to their location
            _geolocator.getGeopositionAsync().then(getPosition);
        } 
    }

    function getLocationEventBased() {
        getLocator();
        stopGetPosition();

        if (_geolocator != null) {
            //this call will also ask the user for permission to their location
            _geolocator.addEventListener("positionchanged", onPositionChanged)

            _geolocator.addEventListener("statuschanged", onStatusChanged);
        }
    }

    function stopGetPosition() {
        if (_geolocator) {
            _geolocator.removeEventListener("positionChanged", onPositionChanged);
            _geolocator.removeEventListener("statuschanged", onStatusChanged);
        }
    }

    function onPositionChanged(args) {
        displayPosition(args.position);
    }

    function getPosition(position) {
        displayPosition(position);
    }

    function displayPosition(position) {
        _lat.innerText = position.coordinate.latitude;
        _long.innerText = position.coordinate.longitude;
        _accuracy.innerText = position.coordinate.accuracy;
    }
 
    function onStatusChanged(args) {
        _status.innerText = getStatusString(args.status);
    }

    function getStatusString(locationStatus) {
        var posStatus = Windows.Devices.Geolocation.PositionStatus;

        switch (locationStatus) {
            case posStatus.ready:
                return "ready";
                break;

            case posStatus.initializing:
                return "initializing";
                break;

            case posStatus.noData:
                return "noData";
                break;

            case posStatus.disabled:
                return "disabled";
                break;

            case posStatus.notInitialized:
                return "notInitialized";
                break;

            case posStatus.notAvailable:
                return "notAvailable";
                break;

            default:
                return "Unknown status.";
        }
    }



    //Doing things the HTML5 way
    function getHtml5Location() {
        if (navigator == null)
            navigator = window.navigator;
    
        var geolocation = navigator.geolocation;
        if (geolocation != null)
            //this call with prompt for access.
            geolocation.getCurrentPosition(positionCallback);
    }
    
    function positionCallback(position) {
        _lat.innerText = position.coords.latitude;
        _long.innerText = position.coords.longitude;
        _accuracy.innerText = position.coords.accuracy;
    }

    app.onloaded = function () {
        getDomElements();
        wireEventHandlers();
    }
    
    app.start();
})();
