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
    public class ReportesController : ControllerBase
    {
        private readonly SubastasContext _context;

        public ReportesController(SubastasContext context)
        {
            _context = context;
        }

        // GET: api/Reportes
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reporte>>> GetReportes()
        {
            return await _context.Reportes.ToListAsync();
        }

        // GET: api/Reportes/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Reporte>> GetReporte(int id)
        {
            var reporte = await _context.Reportes.FindAsync(id);

            if (reporte == null)
            {
                return NotFound();
            }

            return reporte;
        }

        // PUT: api/Reportes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReporte(int id, Reporte reporte)
        {
            if (id != reporte.ReporteId)
            {
                return BadRequest();
            }

            _context.Entry(reporte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReporteExists(id))
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

        // POST: api/Reportes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Reporte>> PostReporte(Reporte reporte)
        {
            _context.Reportes.Add(reporte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReporte", new { id = reporte.ReporteId }, reporte);
        }

        // DELETE: api/Reportes/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReporte(int id)
        {
            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte == null)
            {
                return NotFound();
            }

            _context.Reportes.Remove(reporte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReporteExists(int id)
        {
            return _context.Reportes.Any(e => e.ReporteId == id);
        }
    }
}
