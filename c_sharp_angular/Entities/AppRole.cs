using System;
using Microsoft.AspNetCore.Identity;

namespace c_sharp_angular.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }

    }
}

