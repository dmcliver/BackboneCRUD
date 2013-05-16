using System;
using System.Web.Mvc;
using BackbaneVsKoVsKback.Repositories;
using BackboneCRUD.Models;
using BackboneCRUD.Repositories;

namespace BackboneCRUD.Controllers
{
    public class TimeEntryController : Controller
    {
        private readonly ITimeEntryRepository _timeEntryRepository;

        public TimeEntryController(ITimeEntryRepository timeEntryRepository)
        {
            if (timeEntryRepository == null) throw new ArgumentNullException("timeEntryRepository");
            _timeEntryRepository = timeEntryRepository;
        }

        public TimeEntryController():this(new TimeEntryRepository()){}

        /// <summary>
        /// Add a time entry
        /// </summary>
        public JsonResult Add(TimeEntry timeEntry)
        {
            _timeEntryRepository.Save(timeEntry);
            return Json(Resources.TimeEntryController_SuccessfullyAdded,JsonRequestBehavior.AllowGet);
        }
    }
}
