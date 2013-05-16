using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BackbaneVsKoVsKback.Repositories;
using BackboneCRUD.Models;
using Npgsql;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace BackboneCRUD.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IJobMapper _jobMapper;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(JobRepository));

        public JobRepository(IJobMapper jobMapper)
        {
            if (jobMapper == null) throw new ArgumentNullException(typeof(JobMapper).Name);
            _jobMapper = jobMapper;
        }

        public JobRepository():this(new JobMapper()){}

        /// <summary>
        /// Retrieves all jobs (eager fetch)
        /// </summary>
        public IEnumerable<Job> RetrieveJobs()
        {
            var jobs = new List<Job>();

            try
            {
                var connection = new NpgsqlConnection(Constants.SqlConnection);

                using (connection)
                {
                    connection.Open();
                    var command = new NpgsqlCommand(Constants.RetrieveAllJobs, connection);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    using (reader)
                    {
                        while (reader.Read())
                        {
                            var name = reader["Name"].ToString();
                            Job job;
                            if (jobs.Any(j => j.Name == name))
                                job = jobs.Single(j => j.Name == name);
                            else
                            {
                                job = _jobMapper.MapJob(reader);
                                jobs.Add(job);
                            }
                            _jobMapper.MapTimeEntry(job.Name, job.TimeEntries, reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(Resources.JobRepository_RetrieveJobs_Error, ex);
                throw;
            }
            return jobs;
        }

        /// <summary>
        /// Saves a job (but associations)
        /// </summary>
        public void Save(Job job)
        {
            var scope = new TransactionScope();
            try
            {
                var connection = new NpgsqlConnection(Constants.SqlConnection);
                using (connection)
                {
                    connection.Open();
                    using (scope)
                    {
                        var command = new NpgsqlCommand(Constants.InsertJobStatement, connection);
                        command.Parameters.AddWithValue(Constants.JobNameSqlParam, job.Name);
                        command.Parameters.AddWithValue(Constants.JobQuoteSqlParam, job.Quote);
                        command.ExecuteNonQuery();
                        scope.Complete();
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Error(Resources.JobRepository_Save_Error, ex);
                throw;
            }
        }

        /// <summary>
        /// Removes the job by id
        /// </summary>
        public void Remove(string id)
        {
            var scope = new TransactionScope();
            try
            {
                var connection = new NpgsqlConnection(Constants.SqlConnection);
                using (connection)
                {
                    connection.Open();
                    using (scope)
                    {
                        var command = new NpgsqlCommand(Constants.DeleteTimeEntryByJobName + Constants.JobNameSqlParam, connection);
                        command.Parameters.AddWithValue(Constants.JobNameSqlParam, id);
                        command.ExecuteNonQuery();

                        command = new NpgsqlCommand(Constants.DeleteJobById + Constants.JobNameSqlParam, connection);
                        command.Parameters.AddWithValue(Constants.JobNameSqlParam, id);
                        command.ExecuteNonQuery();
                        
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(Resources.JobRepository_Save_Error, ex);
                throw;
            }
        }

        /// <summary>
        /// Finds if job with matching name identifier exists in database
        /// </summary>
        public bool Exists(string name)
        {
            try
            {
                var connection = new NpgsqlConnection(Constants.SqlConnection);
                using (connection)
                {
                    connection.Open();
                    
                    var command = new NpgsqlCommand(Constants.JobExistsQuery + Constants.JobNameSqlParam, connection);
                    command.Parameters.AddWithValue(Constants.JobNameSqlParam, name);

                    NpgsqlDataReader reader = command.ExecuteReader();
                    bool read = reader.Read();
                    reader.Close();
                    return read;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(Resources.JobRepository_Save_Error, ex);
                throw;
            }
        }

        /// <summary>
        /// Updates the job
        /// </summary>
        public void Update(JobHours jobHours)
        {
            var scope = new TransactionScope();
            try
            {
                var connection = new NpgsqlConnection(Constants.SqlConnection);
                using (connection)
                {
                    connection.Open();
                    using (scope)
                    {
                        var command = new NpgsqlCommand(Constants.UpdateJobSqlStatement, connection);
                        command.Parameters.AddWithValue(Constants.JobQuoteSqlParam, jobHours.Quote);
                        command.Parameters.AddWithValue(Constants.JobNameSqlParam, jobHours.Name);
                        command.ExecuteNonQuery();

                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(Resources.JobRepository_Save_Error, ex);
                throw;
            }
        }
    }
}

