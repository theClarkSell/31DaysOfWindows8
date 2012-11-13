
function shareReady(args) {
    if (_shareOperation.data.contains(_dt.StandardDataFormats.uri)) {
        _shareOperation.data.getUriAsync().done(function (uri) {
            document.querySelector("#results").innerText =
                "Uri: " + uri.absoluteUri;
        });
    }
}