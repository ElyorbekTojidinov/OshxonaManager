using Aplication.Abstraction;
using Aplication.Interfaces.InterfacesJwt;
using Aplication.Interfaces.InterfacesProduct;
using Aplication.Services.ServiceProduct;
using Aplication.Services.ServicesJwt;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ProductDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection")));

            service.AddScoped<IAplicationDbContext, ProductDbContext>();
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IOrdersRepository, OrderRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<IWaiterRepository, WaiterRepository>();

            service.AddScoped<IPermissionRepository, PermissionRepository>();
            service.AddScoped<IRoleRepository, RoleRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddTransient<IJwtService, JwtService>();
            service.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
            return service;
        }
    }
}
