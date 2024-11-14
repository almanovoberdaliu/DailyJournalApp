using DailyJournal.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyJournal.Controllers
{
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;

        // IJournalService journalService - ne parameter te const
        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEntry(string title, string content)
        {
            return null;
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            return null;
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetEntryById(int id)
        {
            return null;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllEntries()
        {
            return null;
        }
    }
}
