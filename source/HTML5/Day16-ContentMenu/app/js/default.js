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
            } else {
            }
            args.setPromise(WinJS.UI.processAll());
        }
    };

    app.oncheckpoint = function (args) {
    };

    function getDomElements() {

    }

    function wireEventHandlers() {
        //document.getElementById("myImage").addEventListener("contextmenu", imageContentHandler, false);

        document.getElementById("myInputBox").addEventListener(
            "contextmenu", inputHandler, false);
    }
    
    function showWhereOver(clickArgs) {
        WinJS.log && WinJS.log(clickArgs.pageX, "x of click", "Day 16");
        WinJS.log && WinJS.log(clickArgs.pageY, "y of click", "Day 16");

        var zoomFactor = document.documentElement.msContentZoomFactor;
        var position = {
            x: (clickArgs.pageX - window.pageXOffset) * zoomFactor,
            y: (clickArgs.pageY - window.pageYOffset) * zoomFactor
        }

        return position;
    }

    function showWhereAbove(clickArgs) {
        WinJS.log && WinJS.log(clickArgs.pageX, "x of click", "Day 16");
        WinJS.log && WinJS.log(clickArgs.pageY, "y of click", "Day 16");

        WinJS.log && WinJS.log(clickArgs.offsetX, "x offset click", "Day 16");
        WinJS.log && WinJS.log(clickArgs.offsetY, "y offset click", "Day 16");

        var zoomFactor = document.documentElement.msContentZoomFactor;
        var position = {
            x: (clickArgs.pageX - clickArgs.offsetX - window.pageXOffset + (clickArgs.target.width / 2)) * zoomFactor,
            y: (clickArgs.pageY - clickArgs.offsetY - window.pageYOffset) * zoomFactor
        }

        return position;
    }

    function showForSelectionWhere(boundingRect) {
        var zoomFactor = document.documentElement.msContentZoomFactor;

        var position = {
            x: (boundingRect.left + document.documentElement.scrollLeft - window.pageXOffset) * zoomFactor,
            y: (boundingRect.top + document.documentElement.scrollTop - window.pageYOffset) * zoomFactor,
            width: boundingRect.width * zoomFactor,
            height: boundingRect.height * zoomFactor
        }

        return position;
    }

    function somethingHandler(args) {
        WinJS.log && WinJS.log("somethingHandler was called", "status", "Day 16");
    }

    function imageContentHandler(args) {
        //Create the context menu that we want to show
        var contextMenu = Windows.UI.Popups.PopupMenu();

        contextMenu.commands.append(new Windows.UI.Popups.UICommand("Clark Sell", somethingHandler));
        contextMenu.commands.append(new Windows.UI.Popups.UICommandSeparator());
        contextMenu.commands.append(new Windows.UI.Popups.UICommand("Jeff Blankenburg", somethingHandler));
        contextMenu.commands.append(new Windows.UI.Popups.UICommand("31 Days of Windows 8", somethingHandler));
        contextMenu.commands.append(new Windows.UI.Popups.UICommand("Edit", somethingHandler));
        contextMenu.commands.append(new Windows.UI.Popups.UICommand("Delete", somethingHandler));

        //Show the context menu
        //contextMenu.showAsync(showWhereOver(args));
        contextMenu.showAsync(showWhereAbove(args));
    }

    function somethingTextHandler(args) {
        //get the text selected
        var selectedText = window.getSelection();
        WinJS.log && WinJS.log(selectedText.toString(), "Selected Text", "Day 16");
    }

    function inputHandler(args) {

        args.preventDefault(); // Prevent the default context menu.

        // Only show a context menu if text is selected
        if (isTextSelected()) {

            var contextMenu = Windows.UI.Popups.PopupMenu();

            contextMenu.commands.append(new Windows.UI.Popups.UICommand("Something", somethingTextHandler));
            contextMenu.commands.append(new Windows.UI.Popups.UICommandSeparator());
            contextMenu.commands.append(new Windows.UI.Popups.UICommand("Something Else", somethingTextHandler));

            //Show the context menu
            contextMenu.showForSelectionAsync(showForSelectionWhere(document.selection.createRange().getBoundingClientRect())).then(function (commandInvoked) {
                if (commandInvoked) {
                    //We could do something here
                    WinJS.log && WinJS.log("command was invoked", "status", "Day 16");

                    var selectedText = window.getSelection();
                    WinJS.log && WinJS.log(selectedText.toString(), "Selected Text", "Day 16");
                }
            });
        }
    }

    function isTextSelected() {
        return (document.getSelection().toString().length > 0);
    }

    app.onloaded = function () {
        getDomElements();
        wireEventHandlers();

        WinJS.Utilities.startLog();
    }

    app.start();
})();
