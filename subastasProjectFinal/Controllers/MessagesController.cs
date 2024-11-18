using Microsoft.AspNetCore.Mvc;
using subastasProjectFinal.Models;
using subastasProjectFinal.Services;

namespace subastasProjectFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessagesController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Message>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Message>>> Get()
        {
            var messages = await _messageService.GetAllAsync();
            return Ok(messages);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Message>> Get(string id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        [HttpGet("sender/{senderId}")]
        [ProducesResponseType(typeof(List<Message>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Message>>> GetBySenderId(string senderId)
        {
            var messages = await _messageService.GetBySenderIdAsync(senderId);
            return Ok(messages);
        }

        [HttpGet("receiver/{receiverId}")]
        [ProducesResponseType(typeof(List<Message>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Message>>> GetByReceiverId(string receiverId)
        {
            var messages = await _messageService.GetByReceiverIdAsync(receiverId);
            return Ok(messages);
        }

        [HttpGet("conversation/{user1Id}/{user2Id}")]
        [ProducesResponseType(typeof(List<Message>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Message>>> GetConversation(string user1Id, string user2Id)
        {
            var messages = await _messageService.GetConversationAsync(user1Id, user2Id);
            return Ok(messages);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Message), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Message>> Post([FromBody] Message message)
        {
            await _messageService.CreateAsync(message);
            return CreatedAtAction(nameof(Get), new { id = message.Id }, message);
        }

        [HttpPut("{id:length(24)}/read")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkAsRead(string id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            await _messageService.MarkAsReadAsync(id);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            await _messageService.DeleteAsync(id);
            return NoContent();
        }
    }
}
