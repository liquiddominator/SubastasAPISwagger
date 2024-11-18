using Microsoft.AspNetCore.Mvc;
using subastasProjectFinal.Models;
using subastasProjectFinal.Services;

namespace subastasProjectFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidsController : ControllerBase
    {
        private readonly BidService _bidService;

        public BidsController(BidService bidService)
        {
            _bidService = bidService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Bid>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Bid>>> Get()
        {
            var bids = await _bidService.GetAllAsync();
            return Ok(bids);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(Bid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Bid>> Get(string id)
        {
            var bid = await _bidService.GetByIdAsync(id);
            if (bid == null)
            {
                return NotFound();
            }
            return Ok(bid);
        }

        [HttpGet("auction/{auctionId}")]
        [ProducesResponseType(typeof(List<Bid>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Bid>>> GetByAuctionId(string auctionId)
        {
            var bids = await _bidService.GetByAuctionIdAsync(auctionId);
            return Ok(bids);
        }

        [HttpGet("bidder/{bidderId}")]
        [ProducesResponseType(typeof(List<Bid>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Bid>>> GetByBidderId(string bidderId)
        {
            var bids = await _bidService.GetByBidderIdAsync(bidderId);
            return Ok(bids);
        }

        [HttpGet("auction/{auctionId}/highest")]
        [ProducesResponseType(typeof(Bid), StatusCodes.Status200OK)]
        public async Task<ActionResult<Bid>> GetHighestBid(string auctionId)
        {
            var bid = await _bidService.GetHighestBidForAuctionAsync(auctionId);
            if (bid == null)
            {
                return NotFound();
            }
            return Ok(bid);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Bid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Bid>> Post([FromBody] Bid bid)
        {
            await _bidService.CreateAsync(bid);
            return CreatedAtAction(nameof(Get), new { id = bid.Id }, bid);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(string id, [FromBody] Bid bid)
        {
            var existingBid = await _bidService.GetByIdAsync(id);
            if (existingBid == null)
            {
                return NotFound();
            }
            await _bidService.UpdateAsync(id, bid);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var bid = await _bidService.GetByIdAsync(id);
            if (bid == null)
            {
                return NotFound();
            }
            await _bidService.DeleteAsync(id);
            return NoContent();
        }
    }
}
