using Aplication.Abstraction;
using Aplication.Interfaces.InterfacesProduct;
using Domain.Models.Models;
using Infrastructure.Services;

namespace Aplication.Services.ServiceProduct;

public class CategoryRepository : Repository<Categories>, ICategoryRepository
{
   
    public CategoryRepository(IAplicationDbContext context) : base(context)
    {
        
    }

   
}
