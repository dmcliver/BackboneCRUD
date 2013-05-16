using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BackbaneVsKoVsKback.Repositories;
using BackboneCRUD.Models;
using BackboneCRUD.Repositories;

namespace BackboneCRUD.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository;

        public JobController(IJobRepository jobRepository)
        {
            if (jobRepository == null) throw new ArgumentNullException("jobRepository");
            _jobRepository = jobRepository;
        }

        public JobController():this(new JobRepository()){}

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Retrieves jobs from the database
        /// </summary>
        [HttpGet]
        public JsonResult RetrieveData()
        {
            IEnumerable<Job> retrievedJobs = _jobRepository.RetrieveJobs();
            IEnumerable<JobHours> jobHours = retrievedJobs.Select(j => new JobHours(j.Name, j.Quote, j.TimeEntries.Sum(t => t.Hours)));
            return Json(jobHours, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Saves a newly created job down to the database
        /// </summary>
        /// <param name="j">The job</param>
        /// <returns>Success message</returns>
        [HttpPut]
        public JsonResult AddData(JobHours j)
        {
            try
            {
                if (_jobRepository.Exists(j.Name))
                    _jobRepository.Update(j);
                else
                    _jobRepository.Save(new Job { Name = j.Name, Quote = j.Quote });
                return Json(Resources.JobController_AddData_JobAddedSuccessfully, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                return Json(Resources.JobController_AddData_CouldntCompleteOperation, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpDelete]
        public JsonResult AddData(string id)
        {
            string message;
            if (id == null)
            {
                Response.StatusCode = 400; // Replace .AddHeader
                message = Resources.JobController_AddData_WasAsked2DeleteJobWithNoId;
            }
            else
            {
                _jobRepository.Remove(id);
                message = Resources.Success;
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}
