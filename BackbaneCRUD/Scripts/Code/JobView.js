var JobView = Backbone.View.extend({

    template: _.template($('#joblist_template').html()),
    
    render: function (eventName) {

        var html = this.template(this.model.toJSON());
        $(this.el).append(html);
        
        return this;
    },

    initialize:function() {
        this.listenTo(this.model, 'destroy', this.remove);
    }
});