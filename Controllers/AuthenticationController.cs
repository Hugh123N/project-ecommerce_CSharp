using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using proyecto_ecommerce_.NET_MVC_.Models;
using proyecto_ecommerce_.NET_MVC_.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace proyecto_ecommerce_.NET_MVC_.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly string secretKey;
        private readonly EcommerceNetContext _context;

        public AuthenticationController(IConfiguration config, EcommerceNetContext context) {
            secretKey = config.GetSection("Jwt").GetValue<string>("key")!;
            _context = context;
        }

        public IActionResult Validar(LoginVM request) {
            var usuario = _context.Usuarios.SingleOrDefault(u => u.Username == request.UserName);
            if (usuario != null && usuario.Password == request.Clave) { 
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.UserName));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenCongif = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenCongif);

                return StatusCode(StatusCodes.Status200OK, new { token = tokenCreado });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { TokenContext = "" });
            }
        }
    }
}
