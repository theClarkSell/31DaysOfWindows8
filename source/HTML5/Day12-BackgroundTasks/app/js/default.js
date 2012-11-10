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

    function registerBackgroundTasks() {

        var _isRegistered = false,
            _bgTaskName = "our task",
            _appModel = Windows.ApplicationModel,
            _background = _appModel.Background,
            _registeredTasks = _background.BackgroundTaskRegistration.allTasks.first(),
            task;

        //loop throught all of the tasks and find out who is already registered
        while (_registeredTasks.hasCurrent) {
            var task = _registeredTasks.current.value;
            if (task.name === _bgTaskName) {
                _isRegistered = true;
                break;
            }

            _registeredTasks.moveNext();
        }

        if (!_isRegistered) {
            var taskBuilder = new _background.BackgroundTaskBuilder();

            var taskTrigger = new _background.SystemTrigger(
                _background.SystemTriggerType.timeZoneChange, false);

            taskBuilder.name = _bgTaskName;
            //taskBuilder.taskEntryPoint = "js\\backgroundTask.js";
            taskBuilder.setTrigger(taskTrigger);
        
            /*
                do we add any conditions here...

                taskBuilder.addConditions(
                    new _background.SystemCondition(
                    _background.SystemConditionType.userPresent));
            */

            task = taskBuilder.register();
            
        }

        WinJS.log && WinJS.log("background task registered", "31 days", "status");

        task.addEventListener("completed",
            function (task) {
                WinJS.log && WinJS.log("background task completed", "31 days", "status");
        });
    }

    app.onready = function () {
        WinJS.Utilities.startLog();
        registerBackgroundTasks();
    }
    
    app.start();
})();
