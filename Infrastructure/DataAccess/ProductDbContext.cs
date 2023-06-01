using Aplication.Abstraction;
using Domain.Models.Models;
using Domain.Models.ModelsJwt;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public class ProductDbContext : DbContext, IAplicationDbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }
        //Products
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Waiter> Waiter { get; set; }

        // JWT 
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasIndex(x => x.UserName).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}
