using System;

namespace BackboneCRUD.Models
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public int Hours { get; set; }
        public DateTime EntryDate { get; set; }
        public string JobId { get; set; }
    }
}