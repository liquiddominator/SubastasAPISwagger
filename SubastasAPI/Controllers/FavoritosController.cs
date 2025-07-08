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
    public class FavoritosController : ControllerBase
    {
        private readonly SubastasContext _context;

        public FavoritosController(SubastasContext context)
        {
            _context = context;
        }

        // GET: api/Favoritos
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorito>>> GetFavoritos()
        {
            return await _context.Favoritos.ToListAsync();
        }

        // GET: api/Favoritos/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Favorito>> GetFavorito(int id)
        {
            var favorito = await _context.Favoritos.FindAsync(id);

            if (favorito == null)
            {
                return NotFound();
            }

            return favorito;
        }

        // PUT: api/Favoritos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "comprador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavorito(int id, Favorito favorito)
        {
            if (id != favorito.FavoritoId)
            {
                return BadRequest();
            }

            _context.Entry(favorito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoritoExists(id))
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

        // POST: api/Favoritos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "comprador")]
        [HttpPost]
        public async Task<ActionResult<Favorito>> PostFavorito(Favorito favorito)
        {
            _context.Favoritos.Add(favorito);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavorito", new { id = favorito.FavoritoId }, favorito);
        }

        // DELETE: api/Favoritos/5
        [Authorize(Roles = "comprador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorito(int id)
        {
            var favorito = await _context.Favoritos.FindAsync(id);
            if (favorito == null)
            {
                return NotFound();
            }

            _context.Favoritos.Remove(favorito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoritoExists(int id)
        {
            return _context.Favoritos.Any(e => e.FavoritoId == id);
        }
    }
}
