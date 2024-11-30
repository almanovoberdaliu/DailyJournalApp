using DailyJournal.Models;

namespace DailyJournal.Interfaces
{
    // konstruktori
    public interface IJournalService
    {
        Task<Journal> AddEntryAsync(string title, string content,DateTime date);

        Task<bool> DeleteEntryAsync(int id);

        Task<Journal> GetEntryByIdAsync(int id);

        Task<IEnumerable<Journal>> GetAllEntriesAsync();

        Task<bool> UpdateEntryAsync(int id, string title, string content,DateTime date);

        Task<Journal> FilterByDateAsync(DateTime date);

        //Task RegisterUserAsync(string username, string password);

        //Task<bool> LoginAsync(string username, string password);


    }
}
