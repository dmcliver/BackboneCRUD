using System;
using System.Transactions;
using BackbaneVsKoVsKback.Repositories;
using BackboneCRUD.Models;
using Npgsql;
using log4net;

namespace BackboneCRUD.Repositories
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TimeEntryRepository));

        /// <summary>
        /// Save the current time entry down to the repository
        /// </summary>
        public void Save(TimeEntry timeEntry)
        {
            try
            {
                var scope = new TransactionScope();
                var connection = new NpgsqlConnection(Constants.SqlConnection);
                using (connection)
                {
                    connection.Open();
                    using (scope)
                    {
                        var command = new NpgsqlCommand(Constants.InsertTimeEntrySqlStatement, connection);
                        command.Parameters.AddWithValue(Constants.EntryDateSqlParam, timeEntry.EntryDate);
                        command.Parameters.AddWithValue(Constants.HoursSqlParam, timeEntry.Hours);
                        command.Parameters.AddWithValue(Constants.TimeEntryJobNameSqlParam, timeEntry.JobId);
                        command.ExecuteNonQuery();
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(Resources.TimeEntryRepository_ErrorSavingTimeEntry, ex);
                throw;
            }
        }
    }
}