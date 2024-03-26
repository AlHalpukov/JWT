using JWT.DataAccess.MSSQL.Configurations;
using JWT.DataAccess.MSSQL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace JWT.DataAccess.MSSQL;

public class JWTDbContext(DbContextOptions<JWTDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
