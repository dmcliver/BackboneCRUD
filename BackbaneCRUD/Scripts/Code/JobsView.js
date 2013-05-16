var JobsView = Backbone.View.extend({
    
    template: _.template($('#joblist_template').html()),

    render: function (eventName) {

        _.each(this.model.models, function (jobModel) {

            var html = this.template(jobModel.toJSON());
            $(this.el).append(html);
        }, this);

        return this;
    }
});