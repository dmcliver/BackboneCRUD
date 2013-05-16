using BackboneCRUD.Models;

namespace BackboneCRUD.Repositories
{
    public interface ITimeEntryRepository
    {
        /// <summary>
        /// Save the current time entry down to the repository
        /// </summary>
        void Save(TimeEntry timeEntry);
    }
}