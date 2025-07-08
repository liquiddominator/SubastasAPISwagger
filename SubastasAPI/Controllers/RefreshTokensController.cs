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
    public class RefreshTokensController : ControllerBase
    {
        private readonly SubastasContext _context;

        public RefreshTokensController(SubastasContext context)
        {
            _context = context;
        }

        // GET: api/RefreshTokens
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefreshToken>>> GetRefreshTokens()
        {
            return await _context.RefreshTokens.ToListAsync();
        }

        // GET: api/RefreshTokens/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RefreshToken>> GetRefreshToken(int id)
        {
            var refreshToken = await _context.RefreshTokens.FindAsync(id);

            if (refreshToken == null)
            {
                return NotFound();
            }

            return refreshToken;
        }
    }
}
