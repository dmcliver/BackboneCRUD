using System.Collections.Generic;
using BackboneCRUD.Models;
using Npgsql;

namespace BackboneCRUD.Repositories
{
    public interface IJobMapper
    {
        Job MapJob(NpgsqlDataReader reader);
        void MapTimeEntry(string jobId, IList<TimeEntry> timeEntries, NpgsqlDataReader reader);
    }
}