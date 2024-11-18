using Microsoft.AspNetCore.Mvc;
using subastasProjectFinal.Models;
using subastasProjectFinal.Services;

namespace subastasProjectFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentsController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Comment>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Comment>>> Get()
        {
            var comments = await _commentService.GetAllAsync();
            return Ok(comments);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(Comment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Comment>> Get(string id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpGet("auction/{auctionId}")]
        [ProducesResponseType(typeof(List<Comment>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Comment>>> GetByAuctionId(string auctionId)
        {
            var comments = await _commentService.GetByAuctionIdAsync(auctionId);
            return Ok(comments);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(List<Comment>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Comment>>> GetByUserId(string userId)
        {
            var comments = await _commentService.GetByUserIdAsync(userId);
            return Ok(comments);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Comment), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Comment>> Post([FromBody] Comment comment)
        {
            await _commentService.CreateAsync(comment);
            return CreatedAtAction(nameof(Get), new { id = comment.Id }, comment);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(string id, [FromBody] Comment comment)
        {
            var existingComment = await _commentService.GetByIdAsync(id);
            if (existingComment == null)
            {
                return NotFound();
            }
            await _commentService.UpdateAsync(id, comment);
            return NoContent();
        }

        [HttpPut("{id:length(24)}/like")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> IncrementLikes(string id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            await _commentService.IncrementLikesAsync(id);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            await _commentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
