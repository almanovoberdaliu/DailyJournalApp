using DailyJournal.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyJournal.Controllers
{
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;

        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEntry(string title, string content,DateTime createdDate)
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

        [HttpPut("get/{id}")]
        public async Task<IActionResult> UpdateEntry(int id,string title,string content, DateTime CreatedDate)
        {
            return null;
        }


        [HttpGet("filter-by-date")]
        public async Task<IActionResult> FilterByDate(DateTime date)
        {
            return null;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            return null;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            return null;
        }

    }
}
