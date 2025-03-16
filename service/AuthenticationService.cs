using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using proyecto_ecommerce_.NET_MVC_.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace proyecto_ecommerce_.NET_MVC_.service
{
    public class AuthenticationService
    {
        private readonly string secretKey;

        public AuthenticationService(IConfiguration config) {
            secretKey = config.GetSection("Jwt").GetValue<string>("key")!;
        }

        public string GenerateToken(Usuario usuario)
        {
            var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            //modif
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim("UserName", usuario.Username),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim("UsuarioCod", usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Tipo)
                };

            var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            ///-----------------

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenCongif = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenCongif);

            return tokenCreado;
        }
    }
}



