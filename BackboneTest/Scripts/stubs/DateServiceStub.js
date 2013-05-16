var DateServiceStub = function() {
    "use strict";

    this.onGetHoursReturn = function(h) {
        self.hours = h;
    };

    this.onGetMinutesReturn = function (m) {
        self.min = m;
    };

    this.getHours = function () {

        return self.hours;
    };

    this.getMinutes = function () {

        return self.min;
    };
}