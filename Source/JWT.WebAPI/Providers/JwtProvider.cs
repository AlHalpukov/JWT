using JWT.DataAccess.MSSQL.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.WebAPI.Providers;

public class JwtProvider : IJwtProvider
{
    private readonly IConfiguration _configuration;
    public JwtProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(User user)
    {
        var token = new JwtSecurityToken(
            claims: [new Claim("userRole", user.Role)],
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTOptions:SecretKey"])),
                    SecurityAlgorithms.HmacSha256),
            expires: DateTime.UtcNow.AddHours(Double.Parse(_configuration["JWTOptions:ExpiresHours"])));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
