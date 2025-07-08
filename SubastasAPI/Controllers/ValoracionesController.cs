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
    public class ValoracionesController : ControllerBase
    {
        private readonly SubastasContext _context;

        public ValoracionesController(SubastasContext context)
        {
            _context = context;
        }

        // GET: api/Valoraciones
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Valoracione>>> GetValoraciones()
        {
            return await _context.Valoraciones.ToListAsync();
        }

        // GET: api/Valoraciones/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Valoracione>> GetValoracione(int id)
        {
            var valoracione = await _context.Valoraciones.FindAsync(id);

            if (valoracione == null)
            {
                return NotFound();
            }

            return valoracione;
        }

        // PUT: api/Valoraciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutValoracione(int id, Valoracione valoracione)
        {
            if (id != valoracione.ValoracionId)
            {
                return BadRequest();
            }

            _context.Entry(valoracione).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValoracioneExists(id))
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

        // POST: api/Valoraciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Valoracione>> PostValoracione(Valoracione valoracione)
        {
            _context.Valoraciones.Add(valoracione);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetValoracione", new { id = valoracione.ValoracionId }, valoracione);
        }

        // DELETE: api/Valoraciones/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValoracione(int id)
        {
            var valoracione = await _context.Valoraciones.FindAsync(id);
            if (valoracione == null)
            {
                return NotFound();
            }

            _context.Valoraciones.Remove(valoracione);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ValoracioneExists(int id)
        {
            return _context.Valoraciones.Any(e => e.ValoracionId == id);
        }
    }
}
