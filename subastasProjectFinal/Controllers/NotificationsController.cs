using Microsoft.AspNetCore.Mvc;
using subastasProjectFinal.Models;
using subastasProjectFinal.Services;

namespace subastasProjectFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationsController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Notification>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Notification>>> Get()
        {
            var notifications = await _notificationService.GetAllAsync();
            return Ok(notifications);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Notification>> Get(string id)
        {
            var notification = await _notificationService.GetByIdAsync(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(List<Notification>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Notification>>> GetByUserId(string userId)
        {
            var notifications = await _notificationService.GetByUserIdAsync(userId);
            return Ok(notifications);
        }

        [HttpGet("user/{userId}/unread")]
        [ProducesResponseType(typeof(List<Notification>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Notification>>> GetUnreadByUserId(string userId)
        {
            var notifications = await _notificationService.GetUnreadByUserIdAsync(userId);
            return Ok(notifications);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Notification), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Notification>> Post([FromBody] Notification notification)
        {
            await _notificationService.CreateAsync(notification);
            return CreatedAtAction(nameof(Get), new { id = notification.Id }, notification);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(string id, [FromBody] Notification notification)
        {
            var existingNotification = await _notificationService.GetByIdAsync(id);
            if (existingNotification == null)
            {
                return NotFound();
            }
            await _notificationService.UpdateAsync(id, notification);
            return NoContent();
        }

        [HttpPut("{id:length(24)}/read")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkAsRead(string id)
        {
            var notification = await _notificationService.GetByIdAsync(id);
            if (notification == null)
            {
                return NotFound();
            }
            await _notificationService.MarkAsReadAsync(id);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var notification = await _notificationService.GetByIdAsync(id);
            if (notification == null)
            {
                return NotFound();
            }
            await _notificationService.DeleteAsync(id);
            return NoContent();
        }
    }
}
