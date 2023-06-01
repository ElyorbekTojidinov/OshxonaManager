using Aplication.Abstraction;
using Aplication.Interfaces.InterfacesJwt;
using Domain.Models.ModelsJwt;
using Infrastructure.Services;

namespace Aplication.Services.ServicesJwt;

public class PermissionRepository : Repository<Permission>, IPermissionRepository
{

    public PermissionRepository(IAplicationDbContext context) : base(context)
    {
        
    }

   
}