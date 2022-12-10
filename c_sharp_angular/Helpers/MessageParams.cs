using System;
namespace c_sharp_angular.Helpers
{
    public class MessageParams : PaginationParams
    {
        public string? Username { get; set; }

        public string Container { get; set; } = "unread";
    }
}

