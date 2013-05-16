namespace BackbaneVsKoVsKback.Repositories
{
    public static class Constants
    {
        public const string RetrieveAllJobs = "SELECT * FROM Job j LEFT OUTER JOIN TimeEntry t on t.JobName = j.Name";
        public const string SqlConnection = "Server = localhost; Port = 5432; Database = TManDb; User Id = postgres; Password = root;";
        public const string JobExistsQuery = "SELECT * FROM Job WHERE Name = ";
        public const string DeleteJobById = "DELETE FROM Job WHERE Name = ";
        public const string DeleteTimeEntryByJobName = "DELETE FROM TimeEntry WHERE JobName = ";
        public const string JobQuoteSqlParam = "@Quote";
        public const string JobNameSqlParam = "@Name";
        public const string HoursSqlParam = "@Hours";
        public const string TimeEntryJobNameSqlParam = "@JobName";
        public const string EntryDateSqlParam = "@EntryDate";

        public static string InsertJobStatement 
        {
            get { return string.Format("INSERT INTO Job(Name,Quote) VALUES ({0},{1})", JobNameSqlParam, JobQuoteSqlParam); }
        }

        public static string UpdateJobSqlStatement
        {
            get { return string.Format("UPDATE Job SET Quote = {0} WHERE Name = {1}", JobQuoteSqlParam, JobNameSqlParam); }
        }

        public static string InsertTimeEntrySqlStatement
        {
            get { return string.Format("INSERT INTO TimeEntry(EntryDate,Hours,JobName) values ({0},{1},{2})",EntryDateSqlParam,HoursSqlParam,TimeEntryJobNameSqlParam); }
        }
    }
}