using System;
namespace c_sharp_angular.Entities
{
    public class UserLike
    {
        public AppUser? SourceUser { get; set; }

        public int? SourceUserId { get; set; }

        public AppUser? TargetUser { get; set; }

        public int? TargetUserId { get; set; }
    }
}

