using Microsoft.AspNetCore.Mvc;
using subastasProjectFinal.Models;
using subastasProjectFinal.Services;

namespace subastasProjectFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportsController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Report>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Report>>> Get()
        {
            var reports = await _reportService.GetAllAsync();
            return Ok(reports);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(Report), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Report>> Get(string id)
        {
            var report = await _reportService.GetByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        [HttpGet("reporter/{reporterId}")]
        [ProducesResponseType(typeof(List<Report>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Report>>> GetByReporterId(string reporterId)
        {
            var reports = await _reportService.GetByReporterIdAsync(reporterId);
            return Ok(reports);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Report), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Report>> Post([FromBody] Report report)
        {
            await _reportService.CreateAsync(report);
            return CreatedAtAction(nameof(Get), new { id = report.Id }, report);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(string id, [FromBody] Report report)
        {
            var existingReport = await _reportService.GetByIdAsync(id);
            if (existingReport == null)
            {
                return NotFound();
            }
            await _reportService.UpdateAsync(id, report);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var report = await _reportService.GetByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            await _reportService.DeleteAsync(id);
            return NoContent();
        }
    }
}
