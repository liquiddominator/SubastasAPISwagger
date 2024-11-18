using Microsoft.AspNetCore.Mvc;
using subastasProjectFinal.Models;
using subastasProjectFinal.Services;

namespace subastasProjectFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsletterController : ControllerBase
    {
        private readonly NewsletterService _newsletterService;

        public NewsletterController(NewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Newsletter>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Newsletter>>> Get()
        {
            var newsletters = await _newsletterService.GetAllAsync();
            return Ok(newsletters);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(Newsletter), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Newsletter>> Get(string id)
        {
            var newsletter = await _newsletterService.GetByIdAsync(id);
            if (newsletter == null)
            {
                return NotFound();
            }
            return Ok(newsletter);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Newsletter), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Newsletter>> Post([FromBody] Newsletter newsletter)
        {
            await _newsletterService.CreateAsync(newsletter);
            return CreatedAtAction(nameof(Get), new { id = newsletter.Id }, newsletter);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(string id, [FromBody] Newsletter newsletter)
        {
            var existingNewsletter = await _newsletterService.GetByIdAsync(id);
            if (existingNewsletter == null)
            {
                return NotFound();
            }
            await _newsletterService.UpdateAsync(id, newsletter);
            return NoContent();
        }

        [HttpPost("unsubscribe")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Unsubscribe([FromBody] string email)
        {
            await _newsletterService.UnsubscribeAsync(email);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var newsletter = await _newsletterService.GetByIdAsync(id);
            if (newsletter == null)
            {
                return NotFound();
            }
            await _newsletterService.DeleteAsync(id);
            return NoContent();
        }
    }
}
