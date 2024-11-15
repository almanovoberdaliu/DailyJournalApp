using DailyJournal.Interfaces;
using DailyJournal.Models;

namespace DailyJournal.Services
{
    public class JournalService : IJournalService
    {
        public Task<Journal> AddEntryAsync(string title, string content)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Journal>> GetAllEntriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Journal> GetEntryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UpdateEntryAsync(int id, string title, string content)
        {
            throw new NotImplementedException();
        }
    }
}
