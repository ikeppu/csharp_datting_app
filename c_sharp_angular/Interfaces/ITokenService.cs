using System;
using c_sharp_angular.Entities;

namespace c_sharp_angular.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}

