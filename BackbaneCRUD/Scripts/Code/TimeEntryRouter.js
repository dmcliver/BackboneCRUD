var TimeEntryRouter = Backbone.Router.extend({
    
    routes: {
        "start/:jobName": "startTimer",   // #start/Clean toilet
        "delete/:jobName" : "deleteJob",
        "edit/:jobName/:jobQuote" : "editJob"
    },
    
    startHour: 0,
    startMin: 0,
    endHour: 0,
    endMin: 0,
    dateService:null,
    timeEntryBuilder: null,
    view: null,
    jobs:null,

    initialize: function (options) {

        dateService = options.dateService;
        timeEntryBuilder = options.timeEntryBuilder;
        view = options.view;
        jobs = options.allJobs;
        jViews = options.JView;
    },

    editJob: function (jobName, jobQuote) {

        this.navigate("/");

        $("#editBox").show(1000);
        $("#jobId").val(jobName);
        $("#jobQuote").val(jobQuote);
    },

    deleteJob: function (jobName) {
        
        this.navigate("/");

        var modelToDel = jobs.where({ Name: jobName })[0];
        jobs.remove(modelToDel);
        
        $("#jobs").html(new JobsView({model:jobs}).render().el);
        modelToDel.destroy();
    },

    startTimer: function (jobName) {
        
        this.navigate("/");

        var timerButton = view.getEl(jobName);
      
        if (timerButton.innerText == "Start") {
            
            timerButton.innerText = "Stop";
            startHour = dateService.getHours();
            startMin = dateService.getMinutes();
        }
        else {
            
            timerButton.innerText = "Start";
            endHour = dateService.getHours();
            endMin = dateService.getMinutes();
            var totalTime = this.calculateTime();
            var timeModel = timeEntryBuilder.build(totalTime,jobName);
            timeModel.save();
        }
    },
    
    calculateTime: function () {
        
        var hours = endHour - startHour;
        var minutes = parseFloat(endMin - startMin);
        return Math.round(hours + minutes / 60);
    }
});