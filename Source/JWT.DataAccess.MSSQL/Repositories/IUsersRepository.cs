using JWT.DataAccess.MSSQL.Entities;

namespace JWT.DataAccess.MSSQL.Repositories
{
    public interface IUsersRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
    }
}