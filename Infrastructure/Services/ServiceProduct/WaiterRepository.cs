using Aplication.Abstraction;
using Aplication.Interfaces.InterfacesProduct;
using Domain.Models.Models;
using Infrastructure.Services;

namespace Aplication.Services.ServiceProduct;
public class WaiterRepository : Repository<Waiter>,  IWaiterRepository
{
    
    public WaiterRepository(IAplicationDbContext context) : base(context)
    {
       
    }
    
}
