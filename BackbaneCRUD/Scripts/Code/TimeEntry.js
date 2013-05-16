"use strict";

var TimeEntry = Backbone.Model.extend({

    default: function () {

        return {

            EntryDate: "",
            Hours: 0,
            JobId:0
        };
    },
    
    url: "http://localhost:54722/TimeEntry/Add"
    
});