using Microsoft.AspNetCore.Mvc;
using subastasProjectFinal.Models;
using subastasProjectFinal.Services;

namespace subastasProjectFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FAQController : ControllerBase
    {
        private readonly FAQService _faqService;

        public FAQController(FAQService faqService)
        {
            _faqService = faqService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<FAQ>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FAQ>>> Get()
        {
            var faqs = await _faqService.GetAllAsync();
            return Ok(faqs);
        }

        [HttpGet("active")]
        [ProducesResponseType(typeof(List<FAQ>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FAQ>>> GetActive()
        {
            var faqs = await _faqService.GetActiveFAQsAsync();
            return Ok(faqs);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(FAQ), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FAQ>> Get(string id)
        {
            var faq = await _faqService.GetByIdAsync(id);
            if (faq == null)
            {
                return NotFound();
            }
            return Ok(faq);
        }

        [HttpGet("category/{category}")]
        [ProducesResponseType(typeof(List<FAQ>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FAQ>>> GetByCategory(string category)
        {
            var faqs = await _faqService.GetByCategoryAsync(category);
            return Ok(faqs);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FAQ), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FAQ>> Post([FromBody] FAQ faq)
        {
            await _faqService.CreateAsync(faq);
            return CreatedAtAction(nameof(Get), new { id = faq.Id }, faq);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(string id, [FromBody] FAQ faq)
        {
            var existingFaq = await _faqService.GetByIdAsync(id);
            if (existingFaq == null)
            {
                return NotFound();
            }
            await _faqService.UpdateAsync(id, faq);
            return NoContent();
        }

        [HttpPut("{id:length(24)}/toggle-active")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ToggleActive(string id)
        {
            var faq = await _faqService.GetByIdAsync(id);
            if (faq == null)
            {
                return NotFound();
            }
            await _faqService.ToggleActiveStatusAsync(id);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var faq = await _faqService.GetByIdAsync(id);
            if (faq == null)
            {
                return NotFound();
            }
            await _faqService.DeleteAsync(id);
            return NoContent();
        }
    }
}
