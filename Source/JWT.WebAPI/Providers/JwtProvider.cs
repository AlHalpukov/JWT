using JWT.DataAccess.MSSQL.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.WebAPI.Providers;

public class JwtProvider : IJwtProvider
{
    private readonly JWTOptions _options;

    public JwtProvider(IOptions<JWTOptions> options)
    {
        _options = options.Value;
    }
    public string GenerateToken(User user)
    {
        var token = new JwtSecurityToken(
            claims: [new Claim("userRole", user.Role)],
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                    SecurityAlgorithms.HmacSha256),
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
