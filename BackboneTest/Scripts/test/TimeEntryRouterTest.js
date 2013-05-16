describe("TimeEntryRouter", function () {
    
    it("should round if the difference is 0.5 or above", function () {

        var dateServiceStub = new DateServiceStub();
        dateServiceStub.onGetHoursReturn(12);
        dateServiceStub.onGetMinutesReturn(15);
        
        var timeEntryBuilderMock = new TimeEntryBuilderMock();
        timeEntryBuilderMock.onBuildReturn({ save: function() {} });

        var viewStub = new JobTimeEntryViewStub();
        viewStub.onGetElReturn({ innerText: "Start" });

        var router = new TimeEntryRouter({ dateService: dateServiceStub, timeEntryBuilder: timeEntryBuilderMock, view: viewStub });
        router.startTimer("sdfafa");

        dateServiceStub.onGetHoursReturn(13);
        dateServiceStub.onGetMinutesReturn(45);
        viewStub.onGetElReturn({ innerText: "Stop" });

        router.startTimer("adfa");
        var hours = timeEntryBuilderMock.getHourArgForBuild();

        expect(hours).toEqual(2);
    });
    
    it("should return not round if difference is less than 0.5", function () {

        var dateServiceStub = new DateServiceStub();
        dateServiceStub.onGetHoursReturn(12);
        dateServiceStub.onGetMinutesReturn(15);

        var timeEntryBuilderMock = new TimeEntryBuilderMock();
        timeEntryBuilderMock.onBuildReturn({ save: function () { } });

        var viewStub = new JobTimeEntryViewStub();
        viewStub.onGetElReturn({ innerText: "Start" });

        var router = new TimeEntryRouter({ dateService: dateServiceStub, timeEntryBuilder: timeEntryBuilderMock, view: viewStub });
        router.startTimer("sdfafa");

        dateServiceStub.onGetHoursReturn(13);
        dateServiceStub.onGetMinutesReturn(20);
        viewStub.onGetElReturn({ innerText: "Stop" });

        router.startTimer("adfa");
        var hours = timeEntryBuilderMock.getHourArgForBuild();

        expect(hours).toEqual(1);
    });
});