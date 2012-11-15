// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;

    var _btnCopyText, _btnCopyHtml, _btnCopyImage, _btnCopyFile, _btnPaste,
        _clipboard = Windows.ApplicationModel.DataTransfer.Clipboard,
        _dataTransfer = Windows.ApplicationModel.DataTransfer,
        _storageFile = Windows.Storage.StorageFile;

    function getDomElements() {
        _btnCopyText = document.querySelector("#btnCopyText");
        _btnCopyHtml = document.querySelector("#btnCopyHtml");
        _btnCopyImage = document.querySelector("#btnCopyImage");
        _btnCopyFile = document.querySelector("#btnCopyFile");
        _btnPaste = document.querySelector("#btnPaste");
    }

    function wireHandlers() {
        _btnCopyText.addEventListener("click", copyTextToClipboard, false);
        _btnCopyHtml.addEventListener("click", copyHtmlToClipboard, false);
        _btnCopyImage.addEventListener("click", copyImageToClipboard, false);
        _btnCopyFile.addEventListener("click", copyFileToClipboard, false);
        _btnPaste.addEventListener("click", getClipboardContent, false);
    }

    function copyTextToClipboard() {
        var selectedText = window.getSelection();

        var dataPackage = new _dataTransfer.DataPackage();
        dataPackage.setText(selectedText);

        _clipboard.setContent(dataPackage);
    }

    function copyHtmlToClipboard() {
        var htmlContent = document.querySelector("#content").innerHTML;
        var htmlContentFormated = _dataTransfer.HtmlFormatHelper.createHtmlFormat(htmlContent);

        var dataPackage = new _dataTransfer.DataPackage();
        dataPackage.setHtmlFormat(htmlContentFormated);
        dataPackage.setText(htmlContent);

        _clipboard.setContent(dataPackage);
    }

    function copyImageToClipboard() {

        var imageSrc = document.getElementById("myImage").src;
        var imageUri = new Windows.Foundation.Uri(imageSrc);
        var streamRef = Windows.Storage.Streams.RandomAccessStreamReference.createFromUri(imageUri);
        
        var dataPackage = new _dataTransfer.DataPackage();
        dataPackage.setStorageItems(streamRef);

        _clipboard.setContent(dataPackage);
    }

    function copyFileToClipboard () {
        var imageSrc = document.getElementById("myImage").src;
        var imageUri = new Windows.Foundation.Uri(imageSrc);

        var splashScreenSource = document.getElementById("splashScreen").src;
        var splashUri = new Windows.Foundation.Uri(splashScreenSource);
        
        var files = [];
        var promises = [];

        promises.push(_storageFile.getFileFromApplicationUriAsync(imageUri)
            .then(function (file) {
                files.push(file);
        }));

        promises.push(_storageFile.getFileFromApplicationUriAsync(splashUri)
                .then(function (file) {
                    files.push(file);
        }));

        WinJS.Promise.join(promises)
            .then(function () {
                var dataPackage = new _dataTransfer.DataPackage();
                dataPackage.setStorageItems(files);
                _clipboard.setContent(dataPackage);
        });
        
        /* single file
        storageFile.getFileFromApplicationUriAsync(imageUri)
            .done(function (file) {
                var dataPackage = new _dataTransfer.DataPackage();
                dataPackage.setStorageItems([file]);
                _clipboard.setContent(dataPackage);
            });
        */
        
    }

    function getFile(uri) {
        return new WinJS.Promise(function (complete, error, progress) {
            storageFile.getFileFromApplicationUriAsync(uri).done(file)
        });
    }

    function getClipboardContent() {

        var dataPackage = _clipboard.getContent(),
            sdFormats = _dataTransfer.StandardDataFormats;

        if (dataPackage.contains(sdFormats.text)) {
            dataPackage.getTextAsync().then(function (text) {
                document.querySelector("#pasteResults").innerText = text;
            });
        }

        if (dataPackage.contains(sdFormats.html)) {
        }

        if (dataPackage.contains(sdFormats.bitmap)) {
        }

        if (dataPackage.contains(sdFormats.storageItems)) {
        }
    }


    app.onloaded = function () {
        getDomElements();
        wireHandlers();
    }

    app.start();
})();
