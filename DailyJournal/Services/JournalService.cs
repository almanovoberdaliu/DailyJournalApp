using DailyJournal.Interfaces;
using DailyJournal.Models;

namespace DailyJournal.Services
{
    public class JournalService : IJournalService
    {
        private readonly List<Journal> _journalEntries = new();
        private int _currentId = 1;

        public async Task<Journal> AddEntryAsync(string title, string content, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Title and content cannot be empty.");

            var newEntry = new Journal
            {
                Id = _currentId++,
                Title = title,
                Description = content,
                CreatedDate = date
            };

            _journalEntries.Add(newEntry);
            return await Task.FromResult(newEntry);
        }

        public async Task<bool> DeleteEntryAsync(int id)
        {
            var entry = _journalEntries.FirstOrDefault(e => e.Id == id);
            if (entry == null) return await Task.FromResult(false);

            _journalEntries.Remove(entry);
            return await Task.FromResult(true);
        }

        public async Task<Journal> GetEntryByIdAsync(int id)
        {
            var entry = _journalEntries.FirstOrDefault(e => e.Id == id);
            return await Task.FromResult(entry);
        }
        public async Task<IEnumerable<Journal>> GetAllEntriesAsync()
        {
            return await Task.FromResult(_journalEntries);
        }

        public async Task<bool> UpdateEntryAsync(int id, string title, string content, DateTime date)
        {
            var entry = _journalEntries.FirstOrDefault(e => e.Id == id);
            if (entry == null) return await Task.FromResult(false);

            entry.Title = title;
            entry.Description = content;
            entry.CreatedDate = date;

            return await Task.FromResult(true);
        }

        public async Task<Journal> FilterByDateAsync(DateTime date)
        {
            var entry = _journalEntries.FirstOrDefault(e => e.CreatedDate.Date == date.Date);
            return await Task.FromResult(entry);
        }

        //public Task RegisterUserAsync(string username, string password)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> LoginAsync(string username, string password)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
