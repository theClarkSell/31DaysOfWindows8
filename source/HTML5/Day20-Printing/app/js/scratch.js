
var _printManager = Windows.Graphics.Printing.PrintManager,
    _printView = _printManager.getForCurrentView();

_printView.onprinttaskrequested = function (eventArgs) {
    eventArgs.request.createPrintTask("31 Days", function (args) {
        args.setSource(MSApp.getHtmlPrintDocumentSource(document));
    });
};