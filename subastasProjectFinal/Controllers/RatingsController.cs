using Microsoft.AspNetCore.Mvc;
using subastasProjectFinal.Models;
using subastasProjectFinal.Services;

namespace subastasProjectFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingsController : ControllerBase
    {
        private readonly RatingService _ratingService;

        public RatingsController(RatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Rating>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Rating>>> Get()
        {
            var ratings = await _ratingService.GetAllAsync();
            return Ok(ratings);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(Rating), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Rating>> Get(string id)
        {
            var rating = await _ratingService.GetByIdAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            return Ok(rating);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(List<Rating>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Rating>>> GetByUserId(string userId)
        {
            var ratings = await _ratingService.GetByFromUserIdAsync(userId);
            return Ok(ratings);
        }

        [HttpGet("user/{userId}/average")]
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        public async Task<ActionResult<double>> GetAverageRating(string userId)
        {
            var average = await _ratingService.GetAverageRatingForUserAsync(userId);
            return Ok(average);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Rating), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Rating>> Post([FromBody] Rating rating)
        {
            await _ratingService.CreateAsync(rating);
            return CreatedAtAction(nameof(Get), new { id = rating.Id }, rating);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(string id, [FromBody] Rating rating)
        {
            var existingRating = await _ratingService.GetByIdAsync(id);
            if (existingRating == null)
            {
                return NotFound();
            }
            await _ratingService.UpdateAsync(id, rating);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var rating = await _ratingService.GetByIdAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            await _ratingService.DeleteAsync(id);
            return NoContent();
        }
    }
}
