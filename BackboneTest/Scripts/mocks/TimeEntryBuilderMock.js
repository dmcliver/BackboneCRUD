var TimeEntryBuilderMock = function() {
    "use strict";

    var self = this;

    self.build = function(hours, jobId) {
        self.hours = hours;
        return self.model;
    };

    self.onBuildReturn = function(model) {
        self.model = model;
    };

    self.getHourArgForBuild = function() {
        return self.hours;
    };
}