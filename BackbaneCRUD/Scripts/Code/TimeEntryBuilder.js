var TimeEntryBuilder = function() {
    "use strict";

    this.build = function(hours, jobId) {
        return new TimeEntry({Hours: hours, JobId: jobId, EntryDate: new Date()});
    };
};