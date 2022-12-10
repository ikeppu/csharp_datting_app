using System;
namespace c_sharp_angular.Helpers
{
    public class LikesParams : PaginationParams
    {
        public int UserId { get; set; }

        public string Predicate { get; set; }
    }
}

