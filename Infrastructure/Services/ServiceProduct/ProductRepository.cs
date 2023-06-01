using Aplication.Abstraction;
using Aplication.Interfaces.InterfacesProduct;
using Domain.Models.Models;
using Infrastructure.Services;

namespace Aplication.Services.ServiceProduct;

public class ProductRepository : Repository<Products>, IProductRepository
{

    public ProductRepository(IAplicationDbContext context) : base(context)
    {
       
    }

   
}
