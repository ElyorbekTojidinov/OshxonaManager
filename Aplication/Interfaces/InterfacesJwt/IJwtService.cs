using Domain.Models.JwtNotCreateDb;
using Domain.Models.ModelsJwt;
using System.Security.Claims;

namespace Aplication.Interfaces.InterfacesJwt
{
    public interface IJwtService
    {
        Task<Token> GenerateTokenAsync(Users user);
        Task<string> GenerateRefreshTokenAsync(Users user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }
}
