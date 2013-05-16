using System.Collections.Generic;
using BackboneCRUD.Models;
using BackboneCRUD.Repositories;

namespace BackboneTest.Controllers
{
    public class JobRepositoryStub : IJobRepository
    {
        private IEnumerable<Job> _jobs;

        public void OnRetrieveJobsReturn(IEnumerable<Job> jobs)
        {
            _jobs = jobs;
        }

        public IEnumerable<Job> RetrieveJobs()
        {
            return _jobs;
        }

        public void Save(Job job)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Update(JobHours jobHours)
        {
            throw new System.NotImplementedException();
        }
    }
}