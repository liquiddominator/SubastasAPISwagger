using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SubastasAPI.Dtos;
using SubastasAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SubastasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SubastasContext _context;
        private readonly IConfiguration _config;

        public AuthController(SubastasContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(Dtos.LoginRequest request)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Rols)
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.Estado == "activo");

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.Password))
            {
                return Unauthorized(new { mensaje = "Credenciales inválidas" });
            }

            var roles = usuario.Rols.Select(r => r.Nombre).ToList();
            var accessToken = GenerateJwtToken(usuario, roles);

            // 🔐 Crear refresh token
            var refreshToken = new RefreshToken
            {
                UsuarioId = usuario.UsuarioId,
                Token = Guid.NewGuid().ToString(),
                Expira = DateTime.UtcNow.AddDays(7),
                IpCreacion = HttpContext.Connection.RemoteIpAddress?.ToString()
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            // 🧾 Respuesta con access y refresh tokens
            var response = new LoginResponse
            {
                Email = usuario.Email,
                Username = usuario.Username,
                Roles = roles,
                Token = accessToken,
                RefreshToken = refreshToken.Token
            };

            return Ok(response);
        }


        private string GenerateJwtToken(Usuario usuario, List<string> roles)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Dtos.RegisterRequest request)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest(new { mensaje = "El email ya está registrado" });
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var usuario = new Usuario
            {
                Username = request.Username,
                Email = request.Email,
                Password = hashedPassword,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Telefono = request.Telefono,
                Estado = "activo",
                Saldo = 0,
                SaldoBloqueado = 0,
                FechaRegistro = DateTime.Now,
                UrlImagen = "https://cxihohhitbhrqqwhnbru.supabase.co/storage/v1/object/public/subastas-assets/users/user_default.png"
            };


            // Obtener el rol antes de agregar a la BD
            var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Nombre == request.Rol);
            if (rol == null)
            {
                return BadRequest(new { mensaje = "Rol no válido" });
            }

            usuario.Rols.Add(rol); // Asignar el rol antes de guardar

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Usuario registrado exitosamente" });
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] Dtos.RefreshRequest request)
        {
            var oldToken = await _context.RefreshTokens
                .Include(rt => rt.Usuario)
                .ThenInclude(u => u.Rols)
                .FirstOrDefaultAsync(t => t.Token == request.RefreshToken);

            if (oldToken == null || oldToken.Revocado || oldToken.Expira < DateTime.UtcNow)
            {
                return Unauthorized(new { mensaje = "Token de actualización inválido o expirado" });
            }

            var usuario = oldToken.Usuario;
            var roles = usuario.Rols.Select(r => r.Nombre).ToList();

            // Revocar el token viejo y generar uno nuevo
            oldToken.Revocado = true;

            var newRefreshToken = new RefreshToken
            {
                UsuarioId = usuario.UsuarioId,
                Token = Guid.NewGuid().ToString(),
                Expira = DateTime.UtcNow.AddDays(7),
                IpCreacion = HttpContext.Connection.RemoteIpAddress?.ToString(),
                ReemplazadoPor = null
            };

            oldToken.ReemplazadoPor = newRefreshToken.Token;

            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            var newAccessToken = GenerateJwtToken(usuario, roles);

            return Ok(new
            {
                token = newAccessToken,
                refreshToken = newRefreshToken.Token
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token == request.RefreshToken);

            if (token == null)
            {
                return NotFound(new { mensaje = "Token no encontrado" });
            }

            if (token.Revocado)
            {
                return BadRequest(new { mensaje = "El token ya está revocado" });
            }

            token.Revocado = true;
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Sesión cerrada correctamente" });
        }
    }
}
