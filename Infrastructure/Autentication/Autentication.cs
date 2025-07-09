using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HyperEfficient.Infrastructure.Autentication
{
    public class Autentication : IAutentication
    {
        private readonly IConfiguration _configuration;

        public Autentication(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GenerateToken(Usuario usuarioEntity, TimeSpan tempoExpiracao)
        {
            byte[] key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);
            SecurityTokenDescriptor tokenDescriptor = new() { Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, usuarioEntity.Nome), new Claim(ClaimTypes.Email, usuarioEntity.Email) }), Expires = DateTime.UtcNow.Add(tempoExpiracao), SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}