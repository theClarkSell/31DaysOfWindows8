(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;
    var nav = WinJS.Navigation;
    var ui = WinJS.UI;
    var appModel = Windows.ApplicationModel;
    var search = Windows.ApplicationModel.Search;

    var searchPageURI = "/pages/search/searchResults.html";

    app.addEventListener("activated", function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                // TODO: This application has been newly launched. Initialize your application here.
            } else {
                // TODO: This application has been reactivated from suspension. Restore application state here.
            }

            if (app.sessionState.history) {
                nav.history = app.sessionState.history;
            }

            args.setPromise(WinJS.UI.processAll().then(function () {
                if (nav.location) {
                    nav.history.current.initialPlaceholder = true;
                    return nav.navigate(nav.location, nav.state);
                } else {
                    return nav.navigate(Application.navigator.home);
                }
            }));
        }

        if (args.detail.kind === appModel.Activation.ActivationKind.search) {
            args.setPromise(ui.processAll().then(function () {
                if (!nav.location) {
                    nav.history.current = {
                        location: Application.navigator.home,
                        initialState: {}
                    };
                }

                return nav.navigate(
                        "/pages/search/searchResults.html",
                        { queryText: args.detail.queryText });
            }));
        }

    });

    app.oncheckpoint = function (args) {
        app.sessionState.history = nav.history;
    };

    search.SearchPane.getForCurrentView().onquerysubmitted = function (args) {
        nav.navigate("/pages/search/searchResults.html", args);
    };

    app.start();
})();
