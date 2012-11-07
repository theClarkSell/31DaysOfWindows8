// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    var _applicationData = Windows.Storage.ApplicationData.current;
    var _localSettings = _applicationData.roamingSettings;
    var _tempFolder = _applicationData.temporaryFolder;

    var _inputA, _labelA, _inputB, _labelB, _saveButton, _saveFileButton, _filePath;

    var _dataFile = {
        firstName: "Clark",
        lastName: "Sell",
        address: {
            street: "123 crazy lane",
            city: "chicago",
            state: "il"
        }
    };

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
            } else {
            }
            args.setPromise(WinJS.UI.processAll());
        }
    };

    app.oncheckpoint = function (args) {
        saveLocal();
    };

    function saveLocal() {
        _localSettings.values["inputA"] = _inputA.value;
        _localSettings.values["inputB"] = _inputB.value;

        writeFile();
    }

    function getLocal() {
        if (_localSettings.values.size > 0) {
            _inputA.value = _localSettings.values["inputA"];
            _inputB.value = _localSettings.values["inputB"];
        }
    }

    function getDomElements() {
        _inputA = document.querySelector("#inputA");
        _labelA = document.querySelector("#labelA");
        _inputB = document.querySelector("#inputB");
        _labelB = document.querySelector("#labelB");

        _saveButton = document.querySelector("#saveButton");

        _filePath = document.querySelector("#filePath");
        _saveFileButton = document.querySelector("#saveFileButton");

    }

    function wireUpHandlers() {
        _inputA.addEventListener("change", inputALeave);
        _inputB.addEventListener("change", inputBLeave);
        _saveButton.addEventListener("click", saveLocal);
        _saveFileButton.addEventListener("click", writeFile);

        WinJS.Application.addEventListener("forceA", inputALeave, false);
        WinJS.Application.addEventListener("forceB", inputBLeave, false);

        _applicationData.addEventListener("datachanged", datachangeHandler);
    }

    function inputALeave() {
        _labelA.innerText+= _inputA.value;
    }

    function inputBLeave() {
        _labelB.innerText += _inputB.value;
    }

    function datachangeHandler(eventArgs) {
      getLocal();

        WinJS.Application.queueEvent({ type: "forceA" });
        WinJS.Application.queueEvent({ type: "forceB" });
    }

    function writeFile() {

        _filePath.innerText = _tempFolder.path;

        _tempFolder.createFileAsync("31DaysOfWindows8.txt",
            Windows.Storage.CreationCollisionOption.replaceExisting)
                .then(function (sampleFile) {
                    var contents = JSON.stringify(_dataFile);

                    return Windows.Storage.FileIO.writeTextAsync(sampleFile, contents);
                }).done(function () {
        });
    }

    app.onready = function () {
        getDomElements();
        wireUpHandlers();
        getLocal();
    }

    app.start();

})();
