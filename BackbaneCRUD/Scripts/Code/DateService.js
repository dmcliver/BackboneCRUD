var DateService = function() {
    "use strict";

    this.getHours = function() {
        return new Date().getHours();
    };

    this.getMinutes = function() {
        return new Date().getMinutes();
    };
}