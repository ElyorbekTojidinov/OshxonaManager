using Aplication.Abstraction;
using Aplication.Interfaces.InterfacesJwt;
using Domain.Models.ModelsJwt;
using Infrastructure.Services;
using System.Security.Cryptography;
using System.Text;


namespace Aplication.Services.ServicesJwt;

public class UserRepository :Repository<Users>, IUserRepository
{
    public UserRepository(IAplicationDbContext context) : base(context)
    {
       
    }

    public override async Task<Users> CreateAsync(Users entity)
    {
        entity.Password = await ComputeHashAsync(entity.Password);
        return await base.CreateAsync(entity);
    }
    public Task<string> ComputeHashAsync(string input)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = sha256.ComputeHash(inputBytes);
        input = Convert.ToBase64String(hashBytes);
        return Task.FromResult(input);
    }


}
