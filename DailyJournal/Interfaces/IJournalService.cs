using DailyJournal.Models;

namespace DailyJournal.Interfaces
{
    // konstruktori
    public interface IJournalService
    {
        Task<Journal> AddEntryAsync(string title, string content);

        Task<bool> DeleteEntryAsync(int id);

        Task<Journal> GetEntryByIdAsync(int id);

        Task<IEnumerable<Journal>> GetAllEntriesAsync();

    }
}
