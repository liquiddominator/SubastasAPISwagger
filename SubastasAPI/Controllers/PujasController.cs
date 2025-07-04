using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubastasAPI.Models;

namespace SubastasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PujasController : ControllerBase
    {
        private readonly SubastasContext _context;

        public PujasController(SubastasContext context)
        {
            _context = context;
        }

        // GET: api/Pujas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Puja>>> GetPujas()
        {
            return await _context.Pujas.ToListAsync();
        }

        // GET: api/Pujas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Puja>> GetPuja(int id)
        {
            var puja = await _context.Pujas.FindAsync(id);

            if (puja == null)
            {
                return NotFound();
            }

            return puja;
        }

        // PUT: api/Pujas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPuja(int id, Puja puja)
        {
            if (id != puja.PujaId)
            {
                return BadRequest();
            }

            _context.Entry(puja).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PujaExists(id))
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

        // POST: api/Pujas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Puja>> PostPuja(Puja puja)
        {
            _context.Pujas.Add(puja);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPuja", new { id = puja.PujaId }, puja);
        }

        // DELETE: api/Pujas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePuja(int id)
        {
            var puja = await _context.Pujas.FindAsync(id);
            if (puja == null)
            {
                return NotFound();
            }

            _context.Pujas.Remove(puja);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PujaExists(int id)
        {
            return _context.Pujas.Any(e => e.PujaId == id);
        }
    }
}
