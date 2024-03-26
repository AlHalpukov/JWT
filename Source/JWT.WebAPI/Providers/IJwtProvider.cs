using JWT.DataAccess.MSSQL.Entities;

namespace JWT.WebAPI.Providers;

public interface IJwtProvider
{
    string GenerateToken(User user);
}