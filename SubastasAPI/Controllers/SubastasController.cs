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
    public class SubastasController : ControllerBase
    {
        private readonly SubastasContext _context;

        public SubastasController(SubastasContext context)
        {
            _context = context;
        }

        // GET: api/Subastas
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subasta>>> GetSubastas()
        {
            return await _context.Subastas.ToListAsync();
        }

        // GET: api/Subastas/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Subasta>> GetSubasta(int id)
        {
            var subasta = await _context.Subastas.FindAsync(id);

            if (subasta == null)
            {
                return NotFound();
            }

            return subasta;
        }

        // PUT: api/Subastas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "vendedor,admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubasta(int id, Subasta subasta)
        {
            if (id != subasta.SubastaId)
            {
                return BadRequest();
            }

            _context.Entry(subasta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubastaExists(id))
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

        // POST: api/Subastas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "vendedor")]
        [HttpPost]
        public async Task<ActionResult<Subasta>> PostSubasta(Subasta subasta)
        {
            _context.Subastas.Add(subasta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubasta", new { id = subasta.SubastaId }, subasta);
        }

        // DELETE: api/Subastas/5
        [Authorize(Roles = "vendedor,admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubasta(int id)
        {
            var subasta = await _context.Subastas.FindAsync(id);
            if (subasta == null)
            {
                return NotFound();
            }

            _context.Subastas.Remove(subasta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubastaExists(int id)
        {
            return _context.Subastas.Any(e => e.SubastaId == id);
        }
    }
}
