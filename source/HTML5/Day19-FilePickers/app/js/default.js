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


    var _btnOpenFile, _btnSaveFile, _btnFolderPicker,
        _pickers = Windows.Storage.Pickers,
        _fileManager = Windows.Storage.CachedFileManager,
        _fileIO = Windows.Storage.FileIO,
        _updateStatus = Windows.Storage.Provider.FileUpdateStatus;

    function getDomElements() {
        _btnOpenFile = document.querySelector("#btnOpenFile");
        _btnSaveFile = document.querySelector("#btnSaveFile");
        _btnFolderPicker = document.querySelector("#btnFolderPicker");
    }

    function wireHandlers () {
        _btnOpenFile.addEventListener("click", openFile);
        _btnSaveFile.addEventListener("click", saveFile);
        _btnFolderPicker.addEventListener("click", folderPicker);
    }

    function openFile () {
        var openPicker = new _pickers.FileOpenPicker();
        //openPicker.fileTypeFilter.append(".png");
        openPicker.fileTypeFilter.replaceAll([".png", ".jpg"])
        openPicker.suggestedStartLocation = _pickers.PickerLocationId.picturesLibrary;

        openPicker.pickSingleFileAsync().then(function (file) {
            //do something awesome here
        });
    }

    function saveFile () {
        var savePicker = new _pickers.FileSavePicker();

        savePicker.fileTypeChoices.insert("Plain Text", [".txt"]);
        savePicker.fileTypeChoices.insert("31 Days", [".31"]);
        savePicker.fileTypeChoices.insert("Excel", [".xlsx"]);

        savePicker.suggestedFileName = "31DaysOfWindows8";
        savePicker.suggestedStartLocation = _pickers.PickerLocationId.picturesLibrary;

        savePicker.pickSaveFileAsync().then(function (file) {
            if (file) {
                _fileManager.deferUpdates(file);
                _fileIO.writeTextAsync(file, "file contents").done(function () {
                    _fileManager.completeUpdatesAsync(file).done(function (updateStatus) {
                        if (updateStatus === _updateStatus.complete) {
                            //saved
                        } else {
                            //opps
                        }
                    });
                });
            } else {
                //user cancelled
            }
        });
    }

    function folderPicker () {
        var folderPicker = new _pickers.FolderPicker(),
            accessCache = Windows.Storage.AccessCache;

        folderPicker.fileTypeFilter.replaceAll([".txt", ".31"]);

        folderPicker.pickSingleFolderAsync().then(function (folder) {
            if (folder) {
                accessCache.StorageApplicationPermissions.futureAccessList.addOrReplace
                    ("PickedFolderToken", folder);
            }
        });
    }


    app.onloaded = function () {
        getDomElements();
        wireHandlers();
    }

    app.start();
})();
