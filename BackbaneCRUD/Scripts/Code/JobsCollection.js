var Jobs = Backbone.Collection.extend({
    model: Job,

    url: "http://localhost:54722/Job/RetrieveData",

});