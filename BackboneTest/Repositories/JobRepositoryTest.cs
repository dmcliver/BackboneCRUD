using System;
using System.Collections.Generic;
using System.Linq;
using BackboneCRUD.Models;
using BackboneCRUD.Repositories;
using NUnit.Framework;

namespace BackboneTest.Repositories
{
    [TestFixture]
    public class JobRepositoryTest
    {
         [Test]
         public void RetrieveAll_MapsAssociationsProperly()
         {
             var repo = new JobRepository();
             IEnumerable<Job> actualJobs = repo.RetrieveJobs();

             Assert.That(actualJobs,Is.Not.Null);
             var jobs = actualJobs as IList<Job> ?? actualJobs.ToList();

             Assert.That(jobs.Count(),Is.GreaterThan(2));
             Assert.That(jobs[0].TimeEntries, Is.Not.Null);
             Assert.That(jobs[0].TimeEntries.Count, Is.GreaterThan(2));
             Assert.That(jobs[1].TimeEntries.Count, Is.EqualTo(0));
         }

         [Test]
         [ExpectedException(typeof(NullReferenceException))]
         public void RetrieveAll_WhenErrorOccurs_LogsToFile()
         {
             var repo = new JobRepository(new JobMapperStub());
             repo.RetrieveJobs();
         }
    }
}