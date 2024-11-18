using Microsoft.AspNetCore.Mvc;
using subastasProjectFinal.Models;
using subastasProjectFinal.Services;

namespace subastasProjectFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly FavoriteService _favoriteService;

        public FavoritesController(FavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Favorite>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Favorite>>> Get()
        {
            var favorites = await _favoriteService.GetAllAsync();
            return Ok(favorites);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(Favorite), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Favorite>> Get(string id)
        {
            var favorite = await _favoriteService.GetByIdAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }
            return Ok(favorite);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(List<Favorite>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Favorite>>> GetByUserId(string userId)
        {
            var favorites = await _favoriteService.GetByUserIdAsync(userId);
            return Ok(favorites);
        }

        [HttpGet("check/{userId}/{auctionId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> CheckFavorite(string userId, string auctionId)
        {
            var isFavorite = await _favoriteService.IsFavoriteAsync(userId, auctionId);
            return Ok(isFavorite);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Favorite), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Favorite>> Post([FromBody] Favorite favorite)
        {
            await _favoriteService.CreateAsync(favorite);
            return CreatedAtAction(nameof(Get), new { id = favorite.Id }, favorite);
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var favorite = await _favoriteService.GetByIdAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }
            await _favoriteService.DeleteAsync(id);
            return NoContent();
        }

        [HttpDelete("user/{userId}/auction/{auctionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteByUserAndAuction(string userId, string auctionId)
        {
            await _favoriteService.DeleteByUserAndAuctionAsync(userId, auctionId);
            return NoContent();
        }
    }
}
