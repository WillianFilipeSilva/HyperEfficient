using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Entities;
using Microsoft.IdentityModel.Tokens;

namespace HyperEfficient.Infrastructure.Autentication;

public class Autentication : IAutentication
{
    private readonly IConfiguration _configuration;

    public Autentication(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public string GenerateToken(UsuarioEntity usuarioEntity)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuarioEntity.Nome),
                new Claim(ClaimTypes.Email, usuarioEntity.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}