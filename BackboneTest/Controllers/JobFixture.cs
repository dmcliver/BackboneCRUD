using System.Collections.Generic;
using BackboneCRUD.Models;

namespace BackboneTest.Controllers
{
    public class JobFixture
    {
        public static Job Create(string name, int quote, List<TimeEntry> timeEntries)
        {
            return new Job {Name = name, Quote = quote, TimeEntries = timeEntries};
        }
    }
}