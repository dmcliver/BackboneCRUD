using System;
using System.Collections.Generic;
using BackbaneVsKoVsKback.Repositories;
using BackboneCRUD.Models;
using Npgsql;

namespace BackboneCRUD.Repositories
{
    public class JobMapper : IJobMapper
    {
        public Job MapJob(NpgsqlDataReader reader)
        {
            return new Job
            {
                Name = reader["Name"].ToString(),
                Quote = (int) reader["Quote"],
                TimeEntries = new List<TimeEntry>()
            };
        }

        public void MapTimeEntry(string jobId, IList<TimeEntry> timeEntries, NpgsqlDataReader reader)
        {
            object id =reader["Id"];
            if (id != null && id != DBNull.Value)
            {
                TimeEntry entry = new TimeEntry();
                entry.Hours = (int) reader["Hours"];
                entry.EntryDate = (DateTime) reader["EntryDate"];
                entry.Id = (int) id;
                entry.JobId = jobId;
                timeEntries.Add(entry);
            }
        }
    }
}