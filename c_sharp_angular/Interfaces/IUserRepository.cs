using System;
using c_sharp_angular.DTOs;
using c_sharp_angular.Entities;
using c_sharp_angular.Helpers;

namespace c_sharp_angular.Interfaces
{
    public interface IUserRepository
    {
        // Specify the methods what you want to be
        void Update(AppUser user);

        Task<IEnumerable<AppUser>> GetUsersAsync();

        Task<AppUser> GetUserByIdAsync(int id);

        Task<AppUser> GetUserByUsername(string username);

        Task<bool> SaveAllAsync();

        // SOME SUPER LOGIC
        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);

        Task<MemberDto> GetMemeberByUsernameAsync(string username);
    }
}

