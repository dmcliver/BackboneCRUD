using System;
using System.Collections.Generic;
using BackboneCRUD.Models;

namespace BackboneTest.Controllers
{
    public class TimeEntriesFixture
    {
        private readonly int _hours;
        private readonly DateTime _dateTime;
        private readonly int _id;

        readonly List<TimeEntry> timeEntries = new List<TimeEntry>(); 
        
        public TimeEntriesFixture(int hours, DateTime dateTime,int id)
        {
            _hours = hours;
            _dateTime = dateTime;
            _id = id;
        }

        public TimeEntriesFixture Add(int hours, DateTime now, int id)
        {
            timeEntries.Add(new TimeEntry{Hours = hours,EntryDate = now,Id = id});
            return this;
        }

        public List<TimeEntry> BuildAll()
        {
            timeEntries.Add(new TimeEntry{EntryDate = _dateTime, Hours = _hours, Id = _id});
            return timeEntries;
        }
    }
}