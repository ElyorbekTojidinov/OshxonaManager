using Aplication.Abstraction;
using Aplication.Interfaces.InterfacesJwt;
using Domain.Models.ModelsJwt;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Aplication.Services.ServicesJwt;

public class RoleRepository : Repository<Roles>, IRoleRepository
{

    public RoleRepository(IAplicationDbContext context) : base(context)
    {
      
    }

    public override async Task<Roles?> CreateAsync(Roles entity)
    {
        _aplicationDb.Roles.Attach(entity);
        await _aplicationDb.SaveChangesAsync();
        return entity;
    }

    public override Task<Roles?> GetByIdAsync(int id)
    {
        IEnumerable<Roles> roles = _aplicationDb.Roles.Include(x => x.Permissions).ToList();
        Roles? role = roles.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(role);
    }

}
