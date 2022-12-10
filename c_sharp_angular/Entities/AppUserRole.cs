using System;
using Microsoft.AspNetCore.Identity;

namespace c_sharp_angular.Entities
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public AppUser User { get; set; }

        public AppRole Role { get; set; }
    }
}

