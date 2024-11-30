using DailyJournal.Interfaces;
using DailyJournal.Models;

namespace DailyJournal.Services
{
    public class JournalService : IJournalService
    {
        private readonly List<Journal> _journalEntries = new();
        private int _currentId = 1;

        // Helper method to find an entry by ID
        private Journal FindEntryById(int id) =>
            _journalEntries.FirstOrDefault(e => e.Id == id);

        // Helper method to validate input
        private void ValidateInput(string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Title and content cannot be empty.");
        }

        public async Task<Journal> AddEntryAsync(string title, string content, DateTime date)
        {
            ValidateInput(title, content);

            var newEntry = new Journal
            {
                Id = _currentId++,
                Title = title.Trim(),
                Description = content.Trim(),
                CreatedDate = date
            };

            _journalEntries.Add(newEntry);
            return await Task.FromResult(newEntry);
        }

        public async Task<bool> DeleteEntryAsync(int id)
        {
            var entry = FindEntryById(id);
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
            return await Task.FromResult(_journalEntries.OrderByDescending(e => e.CreatedDate));
        }

        public async Task<bool> UpdateEntryAsync(int id, string title, string content, DateTime date)
        {
            ValidateInput(title, content);

            var entry = FindEntryById(id);
            if (entry == null) return await Task.FromResult(false);

            entry.Title = title.Trim();
            entry.Description = content.Trim();
            entry.CreatedDate = date;

            return await Task.FromResult(true);
        }

        public async Task<Journal> FilterByDateAsync(DateTime date)
        {
            return await Task.FromResult(_journalEntries.FirstOrDefault(e => e.CreatedDate.Date == date.Date));
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
