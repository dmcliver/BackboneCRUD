using System.Collections.Generic;

namespace BackboneCRUD.Models
{
    public class Job
    {
        public string Name { get; set; }
        public int Quote { get; set; }
        public IList<TimeEntry> TimeEntries { get; set; }
    }
}