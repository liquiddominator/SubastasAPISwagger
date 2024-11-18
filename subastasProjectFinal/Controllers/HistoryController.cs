using Microsoft.AspNetCore.Mvc;
using subastasProjectFinal.Models;
using subastasProjectFinal.Services;

namespace subastasProjectFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly HistoryService _historyService;

        public HistoryController(HistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<History>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<History>>> Get()
        {
            var history = await _historyService.GetAllAsync();
            return Ok(history);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(History), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<History>> Get(string id)
        {
            var historyItem = await _historyService.GetByIdAsync(id);
            if (historyItem == null)
            {
                return NotFound();
            }
            return Ok(historyItem);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(List<History>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<History>>> GetByUserId(string userId)
        {
            var history = await _historyService.GetByUserIdAsync(userId);
            return Ok(history);
        }

        [HttpPost]
        [ProducesResponseType(typeof(History), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<History>> Post([FromBody] History history)
        {
            await _historyService.CreateAsync(history);
            return CreatedAtAction(nameof(Get), new { id = history.Id }, history);
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var history = await _historyService.GetByIdAsync(id);
            if (history == null)
            {
                return NotFound();
            }
            await _historyService.DeleteAsync(id);
            return NoContent();
        }

        [HttpDelete("older-than/{date}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteOlderThan(DateTime date)
        {
            await _historyService.DeleteOlderThanAsync(date);
            return NoContent();
        }
    }
}
