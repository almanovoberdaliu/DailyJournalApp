using DailyJournal.Interfaces;
using DailyJournal.Models;
using Microsoft.AspNetCore.Mvc;

namespace DailyJournal.Controllers
{
    [ApiController]
    [Route("api/journal")]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;

        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetEntryById(int id)
        {
            var entry = await _journalService.GetEntryByIdAsync(id);
            if (entry == null)
                return NotFound($"Journal entry with ID {id} not found.");

            return Ok(entry);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEntry(string title, string content, DateTime createdDate)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
                return BadRequest("Title and content cannot be empty.");

            var entry = await _journalService.AddEntryAsync(title, content, createdDate);
            return CreatedAtAction(nameof(GetEntryById), new { id = entry.Id }, entry);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            var isDeleted = await _journalService.DeleteEntryAsync(id);
            if (!isDeleted)
                return StatusCode(500, "Failed to delete the journal entry.");

            return NoContent();
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllEntries()
        {
            var entries = await _journalService.GetAllEntriesAsync();
            return Ok(entries);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateEntry(int id, string title, string content, DateTime createdDate)
        {
            var isUpdated = await _journalService.UpdateEntryAsync(id, title, content, createdDate);
            if (!isUpdated)
                return StatusCode(500, "Failed to update the journal entry.");

            return NoContent();
        }

        [HttpGet("filter-by-date")]
        public async Task<IActionResult> FilterByDate(DateTime date)
        {
            var entry = await _journalService.FilterByDateAsync(date);
            if (entry == null)
                return NotFound("No journal entries found for the specified date range.");

            return Ok(entry);
        }
    }
}
