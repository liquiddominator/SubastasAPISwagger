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
    public class WalletTransaccionesController : ControllerBase
    {
        private readonly SubastasContext _context;

        public WalletTransaccionesController(SubastasContext context)
        {
            _context = context;
        }

        // GET: api/WalletTransacciones
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WalletTransaccione>>> GetWalletTransacciones()
        {
            return await _context.WalletTransacciones.ToListAsync();
        }

        // GET: api/WalletTransacciones/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<WalletTransaccione>> GetWalletTransaccione(int id)
        {
            var walletTransaccione = await _context.WalletTransacciones.FindAsync(id);

            if (walletTransaccione == null)
            {
                return NotFound();
            }

            return walletTransaccione;
        }

        // PUT: api/WalletTransacciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWalletTransaccione(int id, WalletTransaccione walletTransaccione)
        {
            if (id != walletTransaccione.TransaccionId)
            {
                return BadRequest();
            }

            _context.Entry(walletTransaccione).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalletTransaccioneExists(id))
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

        // POST: api/WalletTransacciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<WalletTransaccione>> PostWalletTransaccione(WalletTransaccione walletTransaccione)
        {
            _context.WalletTransacciones.Add(walletTransaccione);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWalletTransaccione", new { id = walletTransaccione.TransaccionId }, walletTransaccione);
        }

        // DELETE: api/WalletTransacciones/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWalletTransaccione(int id)
        {
            var walletTransaccione = await _context.WalletTransacciones.FindAsync(id);
            if (walletTransaccione == null)
            {
                return NotFound();
            }

            _context.WalletTransacciones.Remove(walletTransaccione);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WalletTransaccioneExists(int id)
        {
            return _context.WalletTransacciones.Any(e => e.TransaccionId == id);
        }
    }
}
