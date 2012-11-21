// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            args.setPromise(WinJS.UI.processAll());
        }
    };

    var _audioElement, _toggleReceiver, _audioReceive, _receiver,
        _playToMgr = Windows.Media.PlayTo.PlayToManager.getForCurrentView();


    /* SENDING AUDIO */
    function sourceRequestHandler(e) {
        if (e)
            e.sourceRequest.setSource(_audioElement.msPlayToSource);
    }
    

    /* RECEIVING AUDIO */
    function toggleReceiveHandler() {
        try {
            
            if (_receiver === undefined)
                _receiver = new Windows.Media.PlayTo.PlayToReceiver();
            
            _receiver.friendlyName = "31 Days Play To Receiver";
            _receiver.supportsAudio = true;
            _receiver.supportsVideo = false;
            _receiver.supportsImage = false;

            /* start implementing the events */          
            _receiver.addEventListener("sourcechangerequested", receiver_SourceChangeRequested);
            _receiver.addEventListener("pauserequested", receiver_PauseRequested);
            _receiver.addEventListener("currenttimechangerequested", receiver_CurrentTimeChangeRequested);
            _receiver.addEventListener("mutechangerequested", receiver_MuteChangeRequested);
            _receiver.addEventListener("playbackratechangerequested", receiver_PlaybackRateChangeRequested);
            _receiver.addEventListener("playrequested", receiver_PlayRequested);
            _receiver.addEventListener("stoprequested", receiver_StopRequested);
            _receiver.addEventListener("timeupdaterequested", receiver_TimeUpdateRequested);
            _receiver.addEventListener("volumechangerequested", receiver_VolumeChangeRequested);
            
            _audioReceive.addEventListener("durationchange", audioPlayer_DurationChange);
            _audioReceive.addEventListener("ended", audioPlayer_Ended);
            _audioReceive.addEventListener("error", audioPlayer_Error);
            _audioReceive.addEventListener("loadedmetadata", audioPlayer_LoadedMetadata);
            _audioReceive.addEventListener("pause", audioPlayer_Pause);
            _audioReceive.addEventListener("playing", audioPlayer_Playing);
            _audioReceive.addEventListener("ratechange", audioPlayer_RateChange);
            _audioReceive.addEventListener("seeked", audioPlayer_Seeked);
            _audioReceive.addEventListener("seeking", audioPlayer_Seeking);
            _audioReceive.addEventListener("volumechange", audioPlayer_VolumeChange);


            // start the receiver
            _receiver.startAsync().done(function () {
                // good place to think about locking the display on.
            });
        }
        catch (e) {
            _receiver = undefined;
            var error = e.message;
        }
    }


    /* functions required to map the receiver */
    function receiver_CurrentTimeChangeRequested(args) {
        if (_audioReceive.currentTime !== 0 || args.time !== 0) {
            _audioReceive.currentTime = args.time / 1000;
        }
    }

    function receiver_MuteChangeRequested(args) {
        _audioReceive.muted = args.mute;
    }

    function receiver_PauseRequested() {
        _audioReceive.pause();
    }


    function receiver_PlaybackRateChangeRequested(args) {
        _audioReceive.playbackRate = args.rate;
    }

    function receiver_PlayRequested() {
        _audioReceive.play();
    }

    function receiver_SourceChangeRequested(args) {
        if (args.stream != null) {
            var mediaStream = MSApp.createBlobFromRandomAccessStream(args.stream.contentType, args.stream);
            _audioReceive.src = URL.createObjectURL(mediaStream, { oneTimeOnly: true });
        }
    }

    function receiver_StopRequested() {
        if (_audioReceive.readyState != 0) {
            _audioReceive.pause();
            _audioReceive.currentTime = 0;
        }
    }

    function receiver_TimeUpdateRequested() {
        _receiver.notifyTimeUpdate(_audioReceive.currentTime * 1000);
    }

    function receiver_VolumeChangeRequested(args) {
        _audioReceive.volume = args.volume;
    }

    /* audio player function */
    function audioPlayer_DurationChange() {
        _receiver.notifyDurationChange(_audioReceive.duration * 1000);
    }

    function audioPlayer_LoadedMetadata() {
        _receiver.notifyLoadedMetadata();
    }

    function audioPlayer_Ended() {
        _receiver.notifyEnded();
    }

    function audioPlayer_Error() {
        _receiver.notifyError();
        _receiver.notifyStopped();
    }

    function audioPlayer_Pause() {
        _receiver.notifyPaused();
    }

    function audioPlayer_Playing() {
        _receiver.notifyPlaying();
    }

    function audioPlayer_RateChange() {
        _receiver.notifyRateChange(_audioReceive.playbackRate);
    }

    function audioPlayer_Seeked() {
        _receiver.notifySeeked();
    }

    function audioPlayer_Seeking() {
        _receiver.notifySeeking();
    }

    function audioPlayer_VolumeChange() {
        _receiver.notifyVolumeChange(_audioReceive.volume, _audioReceive.muted);
    }


    /* STANDARD FUNCTIONS */

    function getDomElements() {
        _audioElement = document.querySelector("#audioTag");

        _toggleReceiver = document.querySelector("#toggleReceiver");
        _audioReceive = document.querySelector("#audioReceive");
    }

    function wireEventHandlers() {
        _playToMgr.addEventListener("sourcerequested", sourceRequestHandler, false);
        _toggleReceiver.addEventListener("click", toggleReceiveHandler, false);
    }

    app.onloaded = function () {
        getDomElements();
        wireEventHandlers();
    }

    app.start();
})();
