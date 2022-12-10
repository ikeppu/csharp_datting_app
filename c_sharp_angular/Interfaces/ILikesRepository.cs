using System;
using c_sharp_angular.DTOs;
using c_sharp_angular.Entities;
using c_sharp_angular.Helpers;

namespace c_sharp_angular.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);

        Task<AppUser> GetUserWithLikes(int userId);

        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}

