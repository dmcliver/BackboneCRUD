    
var Job = Backbone.Model.extend({

    validate: function (model, options) {
        if (model.Name == "") {

            return 'Job name not valid!';
        }
        else if (model.Quote === "" || isNaN(model.Quote) || parseInt(model.Quote) < 0) {

            return 'Job quote not valid!';
        }
    },

    initialize: function (attr, opts) {
        this.set({ id: attr.Name });
        
    },

    defaults: function () {
        
        return {

            id:"",
            Name: "",
            Quote: 0,
            Sum: 0
        };
    },
    
    notify:null,

    urlRoot: "http://localhost:54722/Job/AddData",

});
