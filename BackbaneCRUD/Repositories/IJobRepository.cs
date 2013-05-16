using System.Collections.Generic;
using BackboneCRUD.Models;

namespace BackboneCRUD.Repositories
{
    public interface IJobRepository
    {
        /// <summary>
        /// Retrieves all jobs (eager fetch)
        /// </summary>
        IEnumerable<Job> RetrieveJobs();

        /// <summary>
        /// Saves a job (but associations)
        /// </summary>
        void Save(Job job);

        /// <summary>
        /// Removes the job by id
        /// </summary>
        void Remove(string id);

        /// <summary>
        /// Finds if job with matching name identifier exists in database
        /// </summary>
        bool Exists(string name);

        /// <summary>
        /// Updates the job
        /// </summary>
        void Update(JobHours jobHours);
    }
}