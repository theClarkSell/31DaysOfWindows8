args.setPromise(WinJS.UI.processAll()
    .done(function () {
        var ratingControl = document.querySelector("#ratings").winControl;
        ratingControl.addEventListener("change", changeRating);
    })
);