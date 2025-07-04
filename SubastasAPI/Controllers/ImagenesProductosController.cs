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
    public class ImagenesProductosController : ControllerBase
    {
        private readonly SubastasContext _context;

        public ImagenesProductosController(SubastasContext context)
        {
            _context = context;
        }

        // GET: api/ImagenesProductos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagenesProducto>>> GetImagenesProductos()
        {
            return await _context.ImagenesProductos.ToListAsync();
        }

        // GET: api/ImagenesProductos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImagenesProducto>> GetImagenesProducto(int id)
        {
            var imagenesProducto = await _context.ImagenesProductos.FindAsync(id);

            if (imagenesProducto == null)
            {
                return NotFound();
            }

            return imagenesProducto;
        }

        // PUT: api/ImagenesProductos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagenesProducto(int id, ImagenesProducto imagenesProducto)
        {
            if (id != imagenesProducto.ImagenId)
            {
                return BadRequest();
            }

            _context.Entry(imagenesProducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagenesProductoExists(id))
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

        // POST: api/ImagenesProductos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImagenesProducto>> PostImagenesProducto(ImagenesProducto imagenesProducto)
        {
            _context.ImagenesProductos.Add(imagenesProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImagenesProducto", new { id = imagenesProducto.ImagenId }, imagenesProducto);
        }

        // DELETE: api/ImagenesProductos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagenesProducto(int id)
        {
            var imagenesProducto = await _context.ImagenesProductos.FindAsync(id);
            if (imagenesProducto == null)
            {
                return NotFound();
            }

            _context.ImagenesProductos.Remove(imagenesProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImagenesProductoExists(int id)
        {
            return _context.ImagenesProductos.Any(e => e.ImagenId == id);
        }
    }
}
