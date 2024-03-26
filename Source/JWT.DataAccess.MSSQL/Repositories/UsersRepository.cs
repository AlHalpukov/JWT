using JWT.DataAccess.MSSQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWT.DataAccess.MSSQL.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly JWTDbContext _context;

    public UsersRepository(JWTDbContext context) =>
        _context = context;

    public async Task Add(User user)
    {
        _context.Users.AddAsync(user);
        _context.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _context.Users.Where(_ => _.Email == email).FirstOrDefaultAsync();
    }
}
