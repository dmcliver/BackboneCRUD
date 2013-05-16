using System;
using System.Collections.Generic;
using BackboneCRUD.Models;
using BackboneCRUD.Repositories;
using Npgsql;

namespace BackboneTest.Repositories
{
    public class JobMapperStub : IJobMapper
    {
        private Exception _mapJobEx;
        private Exception _mapTimeEntryEx;
        private readonly Job _job = null;

        public void OnMapJobThrowError(Exception ex)
        {
            _mapJobEx = ex;
        }

        public void OnMapTimeEntryThrowError(Exception ex)
        {
            _mapTimeEntryEx = ex;
        }

        public Job MapJob(NpgsqlDataReader reader)
        {
            if (_mapJobEx != null)
                throw _mapJobEx;
            return _job;
        }

        public void MapTimeEntry(string jobId, IList<TimeEntry> timeEntries, NpgsqlDataReader reader)
        {
            if (_mapTimeEntryEx != null)
                throw _mapTimeEntryEx;
        }
    }
}