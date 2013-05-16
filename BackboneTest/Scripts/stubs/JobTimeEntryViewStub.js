var JobTimeEntryViewStub = function() {
    "use strict";

    var self = this;

    this.onGetElReturn = function(el) {
        self.el = el;
    };

    this.getEl = function(elId) {
        return self.el;
    };
}