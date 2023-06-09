﻿using Domain.Models.ModelsJwt;

namespace Aplication.Interfaces.InterfacesJwt
{
    public interface IUserRefreshTokenRepository
    {
        Task<bool> IsValidUserAsync(Users user);
        Task<UserRefreshToken> AddUserRefreshTokens(UserRefreshToken user);

        Task<UserRefreshToken> GetSavedRefreshTokens(string username, string refreshtoken);

        Task<bool> DeleteUserRefreshTokens(string username, string refreshToken);

        Task<int> SaveCommit();

        Task<UserRefreshToken> UpdateUserRefreshToken(UserRefreshToken user);

    }
}
