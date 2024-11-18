using Microsoft.AspNetCore.Mvc;
using subastasProjectFinal.Models;
using subastasProjectFinal.Services;

namespace subastasProjectFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuctionsController : ControllerBase
    {
        private readonly AuctionService _auctionService;

        public AuctionsController(AuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Auction>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Auction>>> Get()
        {
            var auctions = await _auctionService.GetAllAsync();
            return Ok(auctions);
        }

        [HttpGet("active")]
        [ProducesResponseType(typeof(List<Auction>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Auction>>> GetActive()
        {
            var auctions = await _auctionService.GetActiveAuctionsAsync();
            return Ok(auctions);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(Auction), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Auction>> Get(string id)
        {
            var auction = await _auctionService.GetByIdAsync(id);
            if (auction == null)
            {
                return NotFound();
            }
            return Ok(auction);
        }

        [HttpGet("seller/{sellerId}")]
        [ProducesResponseType(typeof(List<Auction>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Auction>>> GetBySellerId(string sellerId)
        {
            var auctions = await _auctionService.GetBySellerIdAsync(sellerId);
            return Ok(auctions);
        }

        [HttpGet("category/{category}")]
        [ProducesResponseType(typeof(List<Auction>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Auction>>> GetByCategory(string category)
        {
            var auctions = await _auctionService.GetByCategoryAsync(category);
            return Ok(auctions);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Auction), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Auction>> Post([FromBody] Auction auction)
        {
            if (auction.EndDate <= auction.StartDate)
            {
                return BadRequest("End date must be after start date");
            }

            await _auctionService.CreateAsync(auction);
            return CreatedAtAction(nameof(Get), new { id = auction.Id }, auction);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(string id, [FromBody] Auction auction)
        {
            var existingAuction = await _auctionService.GetByIdAsync(id);
            if (existingAuction == null)
            {
                return NotFound();
            }

            if (auction.EndDate <= auction.StartDate)
            {
                return BadRequest("End date must be after start date");
            }

            await _auctionService.UpdateAsync(id, auction);
            return NoContent();
        }

        [HttpPut("{id:length(24)}/price")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePrice(string id, [FromBody] decimal newPrice)
        {
            var auction = await _auctionService.GetByIdAsync(id);
            if (auction == null)
            {
                return NotFound();
            }

            if (newPrice <= auction.CurrentPrice)
            {
                return BadRequest("New price must be higher than current price");
            }

            await _auctionService.UpdateCurrentPriceAsync(id, newPrice);
            return NoContent();
        }

        [HttpPut("{id:length(24)}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] string status)
        {
            var auction = await _auctionService.GetByIdAsync(id);
            if (auction == null)
            {
                return NotFound();
            }

            await _auctionService.UpdateStatusAsync(id, status);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var auction = await _auctionService.GetByIdAsync(id);
            if (auction == null)
            {
                return NotFound();
            }
            await _auctionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
