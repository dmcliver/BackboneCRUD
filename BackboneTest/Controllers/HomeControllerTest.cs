using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BackboneCRUD.Controllers;
using BackboneCRUD.Models;
using NUnit.Framework;

namespace BackboneTest.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
         [Test]
         public void RetrieveData_AggregatesTimeEntries()
         {
             JobRepositoryStub jobRepository = new JobRepositoryStub();

             List<TimeEntry> timeEntries = new TimeEntriesFixture(3, DateTime.Now, 1)
                 .Add(1, DateTime.Now, 2)
                 .BuildAll();

             List<TimeEntry> timeEntries2 = new TimeEntriesFixture(1, DateTime.Now, 3)
                 .Add(2, DateTime.Now, 4)
                 .BuildAll();

             Job item = JobFixture.Create("Toilet", 111, timeEntries);
             Job item2 = JobFixture.Create("Floors", 222, new List<TimeEntry>());
             Job item3 = JobFixture.Create("Floors", 222, timeEntries2);
             
             List<Job> jobs = new List<Job>{item,item2,item3};
             jobRepository.OnRetrieveJobsReturn(jobs);
             
             var controller = new JobController(jobRepository);
             JsonResult retrieveData = controller.RetrieveData();

             IEnumerable<JobHours> data = retrieveData.Data as IEnumerable<JobHours>;

             Assert.NotNull(data);
             var hours = data as IList<JobHours> ?? data.ToList();
             Assert.That(hours.ElementAt(0).Sum, Is.EqualTo(4));
             Assert.That(hours.ElementAt(1).Sum, Is.EqualTo(0));
             Assert.That(hours.ElementAt(2).Sum, Is.EqualTo(3));
         }
    }
}