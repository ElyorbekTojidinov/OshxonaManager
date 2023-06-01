using Aplication.Interfaces.InterfacesProduct;
using Domain.Models.ModelsJwt;

namespace Aplication.Interfaces.InterfacesJwt
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<string> ComputeHashAsync(string v);
        
    }
}
