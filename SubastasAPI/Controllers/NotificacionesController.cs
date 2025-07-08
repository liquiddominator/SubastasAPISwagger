using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubastasAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubastasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : ControllerBase
    {
        private readonly SubastasContext _context;

        public NotificacionesController(SubastasContext context)
        {
            _context = context;
        }

        // GET: api/Notificaciones
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notificacione>>> GetNotificaciones()
        {
            return await _context.Notificaciones.ToListAsync();
        }

        // GET: api/Notificaciones/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Notificacione>> GetNotificacione(int id)
        {
            var notificacione = await _context.Notificaciones.FindAsync(id);

            if (notificacione == null)
            {
                return NotFound();
            }

            return notificacione;
        }

        // PUT: api/Notificaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotificacione(int id, Notificacione notificacione)
        {
            if (id != notificacione.NotificacionId)
            {
                return BadRequest();
            }

            _context.Entry(notificacione).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificacioneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Notificaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin,moderador")]
        [HttpPost]
        public async Task<ActionResult<Notificacione>> PostNotificacione(Notificacione notificacione)
        {
            _context.Notificaciones.Add(notificacione);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotificacione", new { id = notificacione.NotificacionId }, notificacione);
        }

        // DELETE: api/Notificaciones/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificacione(int id)
        {
            var notificacione = await _context.Notificaciones.FindAsync(id);
            if (notificacione == null)
            {
                return NotFound();
            }

            _context.Notificaciones.Remove(notificacione);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificacioneExists(int id)
        {
            return _context.Notificaciones.Any(e => e.NotificacionId == id);
        }
    }
}
