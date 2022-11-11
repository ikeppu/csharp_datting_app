using System;
namespace c_sharp_angular.Entities
{
    public class AppUser
    {
        // Entity it datatable for Database
        // Id have to be Id cause that need for entity framework
        public int Id { get; set; }

        public string? UserName { get; set; }

    }
}

