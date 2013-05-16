$(function() {

    "use strict";
    
    var jobs = new Jobs();
    
    var AppView = Backbone.View.extend({
        el: "body",

        events: {
            "click #submit": "createJob",
            "click #edit": "editJob"
        },
        
        editJob: function () {
            
            var jobId = $("#jobId").val();
            var quoteVal = $("#jobQuote").val();
            
            var mod = jobs.where({ Name: jobId })[0];

            mod.set({ Name: jobId, Quote: quoteVal });
            if (!mod.isValid()) {
                alert(mod.validationError);
                return;
            }
            mod.save();

            $("#jobs").html(new JobsView({model:jobs}).render().el);

            $("#editBox").hide(1000);
        },

        initialize: function () {
            
            this.listenTo(jobs, 'add', this.addOne);
            jobs.fetch();
        },
        
        addOne: function (job) {
            if (!job.isValid()) {
                alert(job.validationError);
                return;
            }
            var elHtml = new JobView({ model: job }).render().el;
            $("#jobs").append(elHtml);
        },

        createJob: function (e) {
            var name = $("#Name").val();
            var quote = $("#Quote").val();
            jobs.create({ Name: name, Quote: quote, Sum: 0 });
        }     
    });

    var jobsView = new JobsView({model:jobs});
    var appView = new AppView;
    
    var router = new TimeEntryRouter({

        dateService: new DateService(),
        timeEntryBuilder: new TimeEntryBuilder(),
        view: new JobTimeEntryView(),
        allJobs: jobs
    });
    
    Backbone.history.start();
});