using Domain.Models.Models;
using Domain.Models.ModelsJwt;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Abstraction
{
    public interface IAplicationDbContext
    {
        //Product
        DbSet<Categories> Categories { get; }
        DbSet<Products> Products { get; }
        DbSet<Orders> Orders { get; }
        DbSet<Waiter> Waiter { get; }

        // JWT 
        DbSet<Users> Users { get; }
        DbSet<Roles> Roles { get; }
        DbSet<Permission> Permissions { get; }
        DbSet<UserRefreshToken> UserRefreshToken { get; }
        Task<int> SaveChangesAsync(CancellationToken token = default);
        DbSet<T> Set<T>() where T : class;
    }
}
